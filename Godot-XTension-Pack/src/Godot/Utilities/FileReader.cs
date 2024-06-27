using Extensionator;
using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack {

    public static class FileHelper {

        /// <summary>
        /// Gets all file paths recursively within a directory, optionally filtering by a regular expression.
        /// </summary>
        /// <param name="path">The path to the directory to search.</param>
        /// <param name="regex">Optional regular expression to filter files based on their names (default: null, includes all files).</param>
        /// <returns>An array of strings containing the full paths of all files found recursively within the directory.</returns>
        public static Array<string> GetFilesPathRecursive(string path, RegEx? regex = null) {
            Array<string> files = [];

            if (path.DirPathIsValid()) {
                DirAccess directory = DirAccess.Open(path);

                if (directory is not null) {
                    directory.ListDirBegin();
                    string currentFile = directory.GetNext();

                    while (!currentFile.IsNullOrEmpty()) {
                        string filePath = directory.GetCurrentDir().PathJoin(currentFile);

                        if (directory.CurrentIsDir()) {
                            files.AddRange(GetFilesPathRecursive(filePath, regex));
                        }
                        else {
                            if (regex is not null) {
                                if (regex.Search(filePath) is not null)
                                    files.Add(filePath);
                            }
                            else {
                                files.Add(filePath);
                            }
                        }

                        currentFile = directory.GetNext();
                    }
                }
            }
            else {
                GD.PushError($"FileHelper::GetFilesRecursive -> An error {DirAccess.GetOpenError()} occured when trying to open directory: {path}");
            }

            return files;
        }

        /// <summary>
        /// Gets all PCK file paths recursively within a directory.
        /// </summary>
        /// <param name="path">The path to the directory to search.</param>
        /// <returns>An array of strings containing the full paths of all PCK files found recursively within the directory.</returns>
        public static Array<string> GetPCKFiles(string path) {
            RegEx regex = new();
            regex.Compile(".pck$");

            return GetFilesPathRecursive(path, regex);
        }


        /// <summary>
        /// Removes files recursively within a directory, optionally filtering by a regular expression.
        /// </summary>
        /// <param name="path">The path to the directory containing files to remove.</param>
        /// <param name="regex">Optional regular expression to filter files based on their names (default: null, removes all files).</param>
        public static void RemoveFilesRecursive(string path, RegEx? regex = null) {
            foreach (string filePath in GetFilesPathRecursive(path, regex)) {
                Error error = DirAccess.RemoveAbsolute(filePath);

                if (!error.Equals(Error.Ok))
                    GD.PushError($"FileHelper::RemoveFilesRecursive -> An Error {error} happened trying to remove {filePath}");
            }

        }

        /// <summary>
        /// Loads a CSV or TSV file into a 2D array of strings, optionally attempting type conversion for numeric values.
        /// </summary>
        /// <param name="path">The path to the CSV or TSV file to load.</param>
        /// <returns>A 2D array of strings representing the parsed CSV/TSV data, or an empty array if the file could not be loaded.</returns>
        public static Array<Array<string>> LoadCSV(string path) {
            if (path.FilePathIsValid() && Godot.FileAccess.FileExists(path)) {
                string delimiter = "";

                switch (path.GetExtension().ToLower()) {
                    case "csv":
                        delimiter = ",";
                        break;
                    case "tsv":
                        delimiter = "\t";
                        break;
                }

                Array<Array<string>> lines = [];

                Godot.FileAccess currentFile = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
                Error openError = Godot.FileAccess.GetOpenError();

                if (!openError.Equals(Error.Ok)) {
                    GD.PushError($"FileHelper::LoadCSV -> Error {openError} opening file {path}");
                    return [];
                }

                while (!currentFile.EofReached()) {
                    string[] csvLine = currentFile.GetCsvLine(delimiter);
                    bool isLastLine = csvLine.Length == 0 || (csvLine.Length == 1 && csvLine.First().IsEmpty());

                    if (isLastLine) {
                        continue;
                    }

                    Array<string> parsedLine = [];

                    foreach (string field in csvLine) {
                        var fieldToAdd = field.StripEdges();

                        if (fieldToAdd.IsValidInt())
                            parsedLine.Add(fieldToAdd.ToInt().ToString());
                        else if (fieldToAdd.IsValidFloat())
                            parsedLine.Add(fieldToAdd.ToFloat().ToString());
                        else
                            parsedLine.Add(fieldToAdd);
                    }

                    lines.Add(parsedLine);
                }

                currentFile.Close();

                if (!lines.IsEmpty() && lines.Last().Count == 1 && lines.Last().First().IsNullOrEmpty())
                    lines.RemoveAt(lines.Count - 1);

                return lines;
            }

            return [];
        }

        /// <summary>
        /// Loads a CSV file into an array of dictionaries, using the first line as column headers.
        /// </summary>
        /// <param name="path">The path to the CSV file to load.</param>
        /// <returns>An array of dictionaries representing the parsed CSV data, or an empty array if the file could not be loaded or there are errors.</returns>
        public static Array<Dictionary> LoadCSVAsDictionary(string path) {
            Array<Array<string>> content = LoadCSV(path);
            Array<Dictionary> result = [];

            if (content.IsEmpty())
                return result;

            Array<string> columnHeaders = content.First();

            foreach (int index in GD.Range(1, content.Count)) {
                Dictionary currentDictionary = [];
                Array<string> currentFields = content[index];

                if (currentFields.Count > columnHeaders.Count) {
                    GD.PushError($"FileHelper::LoadCSVAsDictionary -> The csv {path} fields size is greater than column headers");
                    break;
                }

                foreach (int headerIndex in GD.Range(columnHeaders.Count))
                    currentDictionary[columnHeaders[headerIndex]] = headerIndex < currentFields.Count ? currentFields[headerIndex] : null;

                result.Add(currentDictionary);
            }

            return result;
        }

    }

}