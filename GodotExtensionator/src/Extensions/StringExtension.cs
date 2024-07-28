using Godot;
using System.Text.RegularExpressions;

namespace GodotExtensionator {
    public static partial class StringExtension {

        public static readonly string HexCharacters = "0123456789ABCDEF";

        /// <summary>
        /// Alternative version from HexToint(). Converts a hexadecimal string to its corresponding decimal value.
        /// </summary>
        /// <param name="hex">The string containing the hexadecimal representation of a number.</param>
        /// <returns>The decimal value of the input string, or -1 if the conversion fails.</returns>
        public static int DecimalFromHex(this string hex) {
            float decimalValue = 0.0f;
            float power = 0.0f;
            float value;

            foreach (char c in hex.StripEdges().Reverse()) {
                value = HexCharacters.IndexOf(c.ToString(), System.StringComparison.CurrentCultureIgnoreCase);

                if (value == -1)
                    return -1;

                decimalValue += value * (Mathf.Pow(16.0f, power));
                power += 1;
            }

            return ((int)Mathf.Ceil(decimalValue));
        }

        /// <summary>
        /// Converts a Roman numeral string to its corresponding integer value.
        /// </summary>
        /// <param name="romanNumber">The string containing a valid Roman numeral representation of a number.</param>
        /// <returns>The integer value of the Roman numeral, or 0 if the conversion fails.</returns>
        public static int RomanNumberToInteger(this string romanNumber) {
            Dictionary<char, int> map = new() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };

            int result = 0;
            int previousValue = 0;

            foreach (int index in GD.Range(romanNumber.Length - 1, -1, -1)) {

                if (map.TryGetValue(romanNumber[index], out int currentValue)) {

                    if (currentValue < previousValue)
                        result -= currentValue;
                    else
                        result += currentValue;

                    previousValue = currentValue;
                }
            }

            return result;
        }

        /// <summary>
        /// Removes Godot-specific path information from a string using a pre-compiled regular expression.
        /// </summary>
        /// <param name="path">The string containing a Godot path.</param>
        /// <returns>A new string with the Godot path information stripped (assumed format).</returns>
        public static string StripGodotPath(this string path) => AbsoluteGodotPathRegex().Replace(path, "");

        /// <summary>
        /// Checks if a provided path is a valid file path.
        /// </summary>
        /// <param name="path">The string representing the file path to validate.</param>
        /// <returns>True if the path is a valid absolute path and a file exists at that location, False otherwise.</returns>
        public static bool FilePathIsValid(this string path) {
            return !string.IsNullOrEmpty(path) && path.IsAbsolutePath() && ResourceLoader.Exists(path);
        }

        /// <summary>
        /// Checks if a provided path is a valid directory path.
        /// </summary>
        /// <param name="path">The string representing the directory path to validate.</param>
        /// <returns>True if the path is a valid absolute path and a directory exists at that location, False otherwise.</returns>
        public static bool DirPathIsValid(this string path) {
            return !string.IsNullOrEmpty(path) && path.IsAbsolutePath() && DirAccess.DirExistsAbsolute(path);
        }

        /// <summary>
        /// Checks if a directory exists relative to the executable path.
        /// </summary>
        /// <param name="path">The string representing the relative path to check.</param>
        /// <returns>Error.Ok if the directory exists, DirAccess.GetOpenError() otherwise.</returns>
        public static Error DirExistOnExecutablePath(this string path) {
            string realPath = OS.GetExecutablePath().GetBaseDir().PathJoin(path);
            DirAccess directory = DirAccess.Open(realPath);

            if (directory is not null)
                return Error.Ok;

            return DirAccess.GetOpenError();
        }

        [GeneratedRegex("res://([^ ])+")]
        private static partial Regex AbsoluteGodotPathRegex();
    }

}