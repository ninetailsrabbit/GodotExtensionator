using Godot;
using System.Text.RegularExpressions;

namespace Godot_XTension_Pack {
    public static partial class StringExtension {
        public static readonly string HEX_CHARACTERS = "0123456789ABCDEF";
        public static readonly string ASCII_ALPHANUMERIC = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static readonly string ASCII_LETTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string ASCII_LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
        public static readonly string ASCII_UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string ASCII_DIGITS = "0123456789";
        public static readonly string ASCII_PUNCTUATION = "!\"#$%&'()*+, -./:;<=>?@[\\]^_`{|}~";

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
                value = HEX_CHARACTERS.IndexOf(c.ToString(), System.StringComparison.CurrentCultureIgnoreCase);

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
        /// Checks if a string represents a valid absolute URL (HTTP or HTTPS).
        /// </summary>
        /// <param name="url">The string to validate as a URL.</param>
        /// <returns>True if the string is a valid absolute URL with scheme http or https, False otherwise.</returns>
        public static bool IsValidUrl(this string url) {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Removes BBCode tags from a string using a pre-compiled regular expression.
        /// </summary>
        /// <param name="bbcode">The string containing BBCode tags.</param>
        /// <returns>A new string with the BBCode tags stripped.</returns>
        public static string StripBBcode(this string bbcode) => StripBBCodeRegex().Replace(bbcode, "");

        /// <summary>
        /// Removes Godot-specific path information from a string using a pre-compiled regular expression.
        /// </summary>
        /// <param name="path">The string containing a Godot path.</param>
        /// <returns>A new string with the Godot path information stripped (assumed format).</returns>
        public static string StripGodotPath(this string path) => AbsoluteGodotPathRegex().Replace(path, "");

        [GeneratedRegex(@"\[.+?\]")]
        private static partial Regex StripBBCodeRegex();

        [GeneratedRegex("res://([^ ])+")]
        private static partial Regex AbsoluteGodotPathRegex();

        public static bool IsNullOrEmpty(this string text) => string.IsNullOrEmpty(text);
        public static bool IsNullOrWhitespace(this string text) => string.IsNullOrWhiteSpace(text);


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

        /// <summary>
        /// Compares two strings for equality, ignoring case differences.
        /// </summary>
        /// <param name="source">The first string to compare (extended on).</param>
        /// <param name="other">The second string to compare.</param>
        /// <returns>True if the strings are equal ignoring case, false otherwise.</returns>
        /// <remarks>
        /// This extension method provides a convenient way to compare strings without regard to case sensitivity. It leverages the `Equals` method with the `StringComparison.OrdinalIgnoreCase` flag. This comparison mode performs a case-insensitive ordinal (character-by-character) comparison.
        /// </remarks>
        public static bool EqualsIgnoreCase(this string source, string other)
            => source.Equals(other, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Compares two strings for equality, ignoring case differences.
        /// </summary>
        /// <param name="source">The first string to compare (extended on).</param>
        /// <param name="other">The second string to compare.</param>
        /// <returns>True if the strings are equal ignoring case, false otherwise.</returns>
        /// <remarks>
        /// This extension method offers a case-insensitive string comparison that considers the casing rules of the current culture. It achieves this by using the `Equals` method with the `StringComparison.CurrentCultureIgnoreCase` flag. This comparison mode takes into account the culture-specific casing rules, which might differ from `StringComparison.OrdinalIgnoreCase`.
        /// </remarks>
        public static bool EqualsCultureIgnoreCase(this string source, string other)
           => source.Equals(other, StringComparison.CurrentCultureIgnoreCase);
    }

}