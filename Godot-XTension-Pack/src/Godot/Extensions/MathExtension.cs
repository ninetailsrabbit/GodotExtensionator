using Godot;
using Godot.Collections;
using System.Collections;
using System.Numerics;

namespace Godot_XTension_Pack {
    public static partial class MathExtension {

        public static readonly float PI = 3.141593f;
        public static readonly float DEG2RAD = 57.29578f;
        public static readonly float COMMON_EPSILON = 0.000001f;  // 1.0e-6
        public static readonly float PRECISE_EPSILON = 0.00000001f; // 1.0e-8
        public static readonly float E = 2.71828182845904523536f;
        public static readonly float Delta = 4.6692016091f;  // FEIGENBAUM CONSTANT
        public static readonly float FeigenbaumAlpha = 2.5029078750f;
        public static readonly float AperyConstant = 1.2020569031f;
        public static readonly float GoldenRatio = 1.6180339887f;
        public static readonly float EulerMascheroniConstant = 0.5772156649f;
        public static readonly float KhinchinsConstant = 2.6854520010f;
        public static readonly float GaussKuzminWirsingConstant = 0.3036630028f;
        public static readonly float BernsteinsConstant = 0.2801694990f;
        public static readonly float HafnerSarnakMcCurleyConstant = 0.3532363718f;
        public static readonly float MeisselMertensConstant = 0.2614972128f;
        public static readonly float GlaisherKinkelinConstant = 1.2824271291f;
        public static readonly float OmegaConstant = 0.5671432904f;
        public static readonly float GolombDickmanConstant = 0.6243299885f;
        public static readonly float CahensConstant = 0.6434105462f;
        public static readonly float TwinPrimeConstant = 0.6601618158f;
        public static readonly float LaplaceLimit = 0.6627434193f;
        public static readonly float LandauRamanujanConstant = 0.7642236535f;
        public static readonly float CatalansConstant = 0.9159655941f;
        public static readonly float ViswanathsConstant = 1.13198824f;
        public static readonly float ConwaysConstant = 1.3035772690f;
        public static readonly float MillsConstant = 1.3063778838f;
        public static readonly float PlasticConstant = 1.3247179572f;
        public static readonly float RamanujanSoldnerConstant = 1.4513692348f;
        public static readonly float BackhouseConstant = 1.4560749485f;
        public static readonly float PortersConstant = 1.4670780794f;
        public static readonly float LiebsSquareIceConstant = 1.5396007178f;
        public static readonly float ErdosBorweinConstant = 1.6066951524f;
        public static readonly float NivensConstant = 1.7052111401f;
        public static readonly float UniversalParabolicConstant = 2.2955871493f;
        public static readonly float SierpinskisConstant = 2.5849817595f;
        public static readonly float FransenRobinsonConstant = 2.807770f;

        public static bool IsZero(this int number) => number == 0;
        public static bool IsZero(this float number) => number == 0;
        public static bool IsNotZero(this int number) => number != 0;
        public static bool IsNotZero(this float number) => number != 0;
        public static bool IsGreaterThanZero(this int number) => number > 0;
        public static bool IsGreaterThanZero(this float number) => number > 0;
        public static bool IsBelowZero(this int number) => number < 0;
        public static bool IsBelowZero(this float number) => number < 0;
        public static bool IsEven(this int number) => number % 2 == 0;
        public static bool IsOdd(this int number) => number % 2 != 0;


        /// <summary>
        /// Normalizes an angle in degrees to be between 0 and 360 degrees, assuming the angle is represented in degrees.
        /// </summary>
        /// <param name="degreesAngle">The angle in degrees to normalize.</param>
        /// <returns>The normalized angle between 0 and 360 degrees.</returns>
        public static float NormalizeDegreesAngle(this float degreesAngle) {
            float fullCircleAngle = Mathf.RadToDeg(Mathf.Tau);

            return (degreesAngle % fullCircleAngle + fullCircleAngle) % fullCircleAngle;
        }


        /// <summary>
        /// Normalizes an angle in radians to be between 0 and 2 * PI radians, assuming the angle is represented in radians.
        /// </summary>
        /// <param name="angle">The angle in radians to normalize.</param>
        /// <returns>The normalized angle between 0 and 2 * PI radians.</returns>
        public static float NormalizeRadiansAngle(this float angle) {

            return (angle % Mathf.Tau + Mathf.Tau) % Mathf.Tau;
        }

        /// <summary>
        /// Checks if a random chance occurs based on a probability value.
        /// </summary>
        /// <param name="chance">
        /// The probability of success (a value between 0.0 and 1.0, where 0.0 indicates no chance and 1.0 indicates guaranteed success).
        /// </param>
        /// <returns>
        /// True if the random chance occurs, False otherwise.
        /// </returns>
        /// <remarks>
        /// This function uses the `GD.Randf()` method to generate a random floating-point number between 0 (inclusive) and 1 (exclusive).
        /// It then compares this random value with the provided `chance` value, which is clamped to the valid range of 0 to 1.
        /// If the random value is less than or equal to the clamped `chance`, the function returns True, indicating a successful chance occurrence.
        /// Otherwise, it returns False.
        /// </remarks>
        public static bool Chance(float chance) => GD.Randf() <= Mathf.Clamp(chance, 0f, 1f);

        /// <summary>
        /// Checks if an integer value falls between a specified minimum and maximum range.
        /// </summary>
        /// <param name="value">The integer value to check.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <param name="inclusive">Optional flag indicating whether the range includes the minimum and maximum values (default: true).</param>
        /// <returns>True if the value is between min and max (inclusive or exclusive based on the flag), False otherwise.</returns>
        public static bool IsBetween(this int value, int min, int max, bool inclusive = true) {
            int minValue = Mathf.Min(min, max);
            int maxValue = Mathf.Max(min, max);

            return inclusive ? value >= minValue && value <= maxValue : value > minValue && value < maxValue;
        }

        /// <summary>
        /// Checks if a float value falls between a specified minimum and maximum range, considering a small precision offset.
        /// </summary>
        /// <param name="value">The float value to check.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <param name="inclusive">Optional flag indicating whether the range includes the minimum and maximum values (default: true).</param>
        /// <param name="precision">Optional precision value to account for floating-point rounding errors (default: 0.00001f).</param>
        /// <returns>True if the value is between min and max (inclusive or exclusive based on the flag), False otherwise.</returns>
        public static bool IsBetween(this float value, float min, float max, bool inclusive = true, float precision = 0.00001f) {
            float minValue = Mathf.Min(min, max) - precision;
            float maxValue = Mathf.Max(min, max) + precision;

            return inclusive ? value >= minValue && value <= maxValue : value > minValue && value < maxValue;
        }

        /// <summary>
        /// Converts an integer value to its hexadecimal string representation.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>The hexadecimal string representation of the integer.</returns>
        public static string Hexadecimal(this int value) {
            int remaining = value;
            string hexString = string.Empty;

            while (remaining > 0) {
                int remainderHex = remaining % 16;
                hexString = StringExtension.HEX_CHARACTERS[remainderHex] + hexString;
                remaining /= 16;
            }

            return hexString;
        }

        /// <summary>
        /// Formats an integer value by adding a thousand separator (e.g., comma) for readability.
        /// </summary>
        /// <param name="value">The integer value to format.</param>
        /// <param name="separator">Optional character to use as the thousand separator (default: comma).</param>
        /// <returns>The formatted string with thousand separators.</returns>
        public static string ThousandSeparator(this int value, string separator = ",") {
            string numberAsText = value.ToString();
            float mod = numberAsText.Length % 3;
            string result = string.Empty;

            foreach (int index in Enumerable.Range(0, numberAsText.Length)) {
                if (index != 0 && index % 3 == mod)
                    result += separator;

                result += numberAsText[index];
            }

            return result;
        }

        /// <summary>
        /// Rounds a int value to the nearest significant billion, million, thousand, or the value itself (for smaller numbers).
        /// </summary>
        /// <param name="number">The int value to be rounded.</param>
        /// <returns>The rounded int value based on its magnitude.</returns>
        public static BigInteger BigRound(this BigInteger number) {
            if (number >= 1000000000000) {
                return (int)((int)Math.Floor((double)number / 1000000000000) * 1000000000000);
            }
            else if (number >= 1000000000) {
                return (int)Math.Floor((double)number / 1000000000) * 1000000000;
            }
            else if (number >= 10000000) {
                return (int)Math.Floor((double)number / 1000000) * 1000000;
            }
            else if (number >= 10000) {
                return (int)Math.Floor((double)number / 1000) * 1000;
            }
            else {
                return number;
            }
        }

        /// <summary>
        /// Rounds a float value to the nearest significant billion, million, thousand, or the value itself (for smaller numbers).
        /// </summary>
        /// <param name="number">The float value to be rounded.</param>
        /// <returns>The rounded float value based on its magnitude.</returns>
        public static float BigRound(this float number) {
            if (number >= 1000000000000) {
                return (int)((int)Math.Floor((double)number / 1000000000000) * 1000000000000);
            }
            else if (number >= 1000000000) {
                return (int)Math.Floor((double)number / 1000000000) * 1000000000;
            }
            else if (number >= 10000000) {
                return (int)Math.Floor((double)number / 1000000) * 1000000;
            }
            else if (number >= 10000) {
                return (int)Math.Floor((double)number / 1000) * 1000;
            }
            else {
                return number;
            }
        }

        /// <summary>
        /// Applies a bias to a float value using a cubic function.
        /// </summary>
        /// <param name="number">The float value to be biased.</param>
        /// <param name="bias">The bias value influencing the output (0 for no bias, 1 for full bias).</param>
        /// <returns>The biased float value.</returns>
        public static float Bias(this float number, float bias) {
            float k = Mathf.Pow(1.0f - bias, 3);

            return (number * k) / (number * k - number + 1);
        }

        /// <summary>
        /// Calculates the sigmoid function (logistic function) of a float value.
        /// </summary>
        /// <param name="number">The float value to be processed by the sigmoid function.</param>
        /// <param name="scalingFactor">Optional scaling factor affecting the steepness of the curve (default: 0, no scaling).</param>
        /// <returns>The sigmoid value of the input number.</returns>
        public static float Sigmoid(this float number, float scalingFactor = 0.0f) {
            if (scalingFactor.IsZero())
                return 1 / (1 + Mathf.Exp(-number));

            return 1 - 1 / (1 + Mathf.Pow(E, -10 * (number / scalingFactor - 0.5f)));
        }

        /// <summary>
        /// Calculates the factorial of an integer (recursive implementation).
        /// </summary>
        /// <param name="number">The non-negative integer for which to calculate the factorial.</param>
        /// <returns>The factorial value of the number (0! = 1, 1! = 1, n! = n * (n-1)!).</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the number is negative.</exception>
        public static int Factorial(this int number) {
            if (number.IsZero() || number == 1)
                return 1;

            return number * Factorial(number - 1);
        }

        public static float Factorial(this float number) {
            if (number.IsZero() || number == 1)
                return 1;

            return number * Factorial(number - 1);
        }

        /// <summary>
        /// Generates an array containing factorials of all integers from a given starting point (inclusive).
        /// </summary>
        /// <param name="number">The starting integer from which to calculate factorials.</param>
        /// <returns>An array containing factorials for all integers from 'number' onwards.</returns>
        public static Array<float> FactorialsFrom(this int number) {
            Array<float> result = [];

            foreach (float index in GD.Range(number + 1))
                result.Add(index.Factorial());

            return result;
        }

        /// <summary>
        /// Converts a non-negative integer to its Roman numeral representation.
        /// </summary>
        /// <param name="number">The non-negative integer to convert (negative values are converted to absolute values).</param>
        /// <returns>The Roman numeral string representation of the input number, or an empty string if the input is zero.</returns>
        public static string ToRomanNumber(this int number) {
            number = Mathf.Abs(number);

            string[] romanDigits = ["", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"];
            string[] tensPlace = ["", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"];
            string[] hundredsPlace = ["", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"];
            string[] thousandsPlace = ["", "M", "MM", "MMM"];


            int thousands = number / 1000;
            int hundreds = number % 1000 / 100;
            int tens = number % 100 / 10;
            int ones = number % 10;


            return $"{thousandsPlace[thousands]}{hundredsPlace[hundreds]}{tensPlace[tens]}{romanDigits[ones]}";
        }

        /// <summary>
        /// Converts an integer to its ordinal representation (e.g., 1st, 2nd, 3rd, etc.).
        /// </summary>
        /// <param name="number">The integer to convert.</param>
        /// <returns>A string representing the ordinal form of the number.</returns>
        public static string ToOrdinal(this int number) {
            int middle = number % 100;
            string suffix;

            System.Collections.Generic.Dictionary<int, string> suffixMap = new() { { 1, "st" }, { 2, "nd" }, { 3, "rd" } };

            if (middle >= 11 && middle <= 13)
                suffix = "th";
            else
                suffix = suffixMap.GetValueOrDefault(number % 10, "th");


            return $"{number}{suffix}";
        }

        /// <summary>
        /// Converts a number to a human-readable format with optional magnitude suffixes (e.g., 1000 becomes 1K).
        /// </summary>
        /// <param name="number">The integer to convert.</param>
        /// <param name="suffixes">Optional array of suffixes for different magnitude levels (defaults to ["", "K", "M", "B", "T"]). 
        ///  If null, default suffixes are used.</param>
        /// <returns>A string representing the number in a human-readable format with a magnitude suffix (if applicable).</returns>
        public static string PrettyNumber(this int number, string[]? suffixes = null) => PrettyNumber((float)number, suffixes);

        /// <summary>
        /// Converts a number to a human-readable format with optional magnitude suffixes (e.g., 1000 becomes 1K).
        /// </summary>
        /// <param name="number">The float to convert.</param>
        /// <param name="suffixes">Optional array of suffixes for different magnitude levels (defaults to ["", "K", "M", "B", "T"]). 
        /// If null, default suffixes are used.</param>
        /// <returns>A string representing the number in a human-readable format with a magnitude suffix (if applicable).</returns>
        public static string PrettyNumber(this float number, string[]? suffixes = null) {
            suffixes ??= ["", "K", "M", "B", "T"];
            number = Mathf.Abs(number);

            string prefixSign = Mathf.Sign(number) == -1 ? "-" : "";
            int exponent = 0;

            while (number >= 1000) {
                number /= 1000.0f;
                exponent += 1;
            }

            return $"{prefixSign}{Mathf.Snapped(number, 0.001f)}{suffixes[exponent]}";
        }

        /// <summary>
        /// Converts an integer to its binary string representation.
        /// </summary>
        /// <param name="number">The integer to convert.</param>
        /// <returns>A string representing the binary form of the integer.</returns>
        public static string ToBinary(this int number) {

            string binaryString = string.Empty;
            int numberToTransform = number;


            while (numberToTransform.IsGreaterThanZero()) {
                binaryString = $"{numberToTransform & 1}" + binaryString;
                numberToTransform >>= 1;
            }

            return binaryString;
        }

        /// <summary>
        /// Formats a floating-point time value (in seconds) into minutes:seconds or minutes:seconds:milliseconds format.
        /// </summary>
        /// <param name="time">The time value in seconds.</param>
        /// <param name="useMilliseconds">True to include milliseconds in the output format, False for minutes and seconds only (default).</param>
        /// <returns>A string representing the formatted time in minutes:seconds or minutes:seconds:milliseconds.</returns>
        public static string ToFormattedSeconds(this float time, bool useMilliseconds = false) {
            int minutes = (int)Math.Floor(time / 60);
            int seconds = (int)(time % 60);

            if (!useMilliseconds)
                return string.Format("{0:00}:{1:00}", minutes, seconds);

            int milliseconds = (int)(Math.Floor(time * 100) % 100);

            return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Converts a delta time value (typically from a game engine's frame time) to seconds.
        /// Delta time represents the time elapsed between frames, which can vary slightly
        /// depending on the frame rate. This function converts delta time to a consistent
        /// time unit (seconds) suitable for game logic calculations.
        /// </summary>
        /// <param name="delta">The delta time value to convert (usually in units per frame).</param>
        /// <returns>The delta time converted to seconds.</returns>
        public static float DeltaToTime(this float delta) => 1f / delta * 0.001f;

        /// <summary>
        /// Converts a delta time value (typically from a game engine's frame time) to seconds.
        /// Delta time represents the time elapsed between frames, which can vary slightly
        /// depending on the frame rate. This function converts delta time to a consistent
        /// time unit (seconds) suitable for game logic calculations.
        /// </summary>
        /// <param name="delta">The delta time value to convert (usually in units per frame).</param>
        /// <returns>The delta time converted to seconds.</returns>
        public static double DeltaToTime(this double delta) => 1d / delta * 0.001d;

        /// <summary>
        /// Returns the larger of two byte values.
        /// </summary>
        /// <param name="first">The first byte value to compare.</param>
        /// <param name="second">The second byte value to compare.</param>
        /// <returns>The larger of the two provided byte values.</returns>
        /// <remarks>
        /// This extension method leverages the static `Math.Max` method to determine the larger value between the two `byte` arguments (`first` and `second`).
        /// The `Math.Max` method is a generic function that can handle different numerical types, and in this case, it is specifically used for bytes.
        /// This approach provides a concise way to find the maximum byte value without writing explicit comparison logic.
        /// </remarks>
        public static byte Max(this byte first, byte second) => Math.Max(first, second);

        /// <summary>
        /// Returns the smaller of two byte values.
        /// </summary>
        /// <param name="first">The first byte value to compare.</param>
        /// <param name="second">The second byte value to compare.</param>
        /// <returns>The smaller of the two provided byte values.</returns>
        /// <remarks>
        /// This extension method is similar to `Max` but utilizes the `Math.Min` static method.
        /// The `Math.Min` method finds the minimum value between the two `byte` arguments (`first` and `second`).
        /// This method provides a convenient way to identify the smaller byte value without writing conditional statements.
        /// </remarks>
        public static byte Min(this byte first, byte second) => Math.Min(first, second);

        /// <summary>
        /// Converts an integer value to a byte array representation.
        /// </summary>
        /// <param name="value">The integer value to be converted.</param>
        /// <returns>A byte array containing the bytes representing the integer value.</returns>
        /// <remarks>
        /// This extension method employs the `BitConverter.GetBytes` static method to convert the provided `int` value (`value`) into a byte array.
        /// The `BitConverter` class offers various methods for converting between different data types and their byte representations.
        /// In this case, `GetBytes` specifically handles the conversion of an integer to a byte array based on the system's endianness (byte order).
        /// The resulting byte array can be used for network communication, file storage, or other scenarios where byte representations are required.
        /// </remarks>
        public static byte[] GetBytes(this int value) => BitConverter.GetBytes(value);


        /// <summary>
        /// Converts a BitArray with a length of 8 to a single byte.
        /// </summary>
        /// <param name="bits">The BitArray to convert. (Must have a length of 8)</param>
        /// <returns>A byte representing the value of the BitArray.</returns>
        /// <exception cref="ArgumentException">Thrown if the BitArray length is not 8.</exception>
        /// <remarks>
        /// This method validates the BitArray length to ensure it's 8 bits. If not, an ArgumentException is thrown.
        /// Otherwise, it creates a byte array of size 1 and copies the BitArray bits into it using CopyTo.
        /// Finally, the first element (index 0) of the byte array is returned as the resulting byte.
        /// </remarks>
        /// 
        public static byte ToByte(this BitArray bits) {
            if (bits.Count != 8)
                throw new ArgumentException("BitsToByte: Bits needs to have a length of 8");

            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);

            return bytes[0];
        }

        /// <summary>
        /// Converts an array of up to 8 booleans to a single byte.
        /// </summary>
        /// <param name="bools">The boolean array to convert. (Maximum length of 8)</param>
        /// <returns>A byte representing the value of the boolean array.</returns>
        /// <exception cref="ArgumentException">Thrown if the boolean array length is greater than 8.</exception>
        /// <remarks>
        /// This method validates the boolean array length to ensure it's less than or equal to 8. 
        /// If not, an ArgumentException is thrown. Otherwise, it creates a new BitArray and sets each bit based on the corresponding boolean value.
        /// Finally, the BitsToByte method is called to convert the BitArray to a byte.
        /// </remarks>
        public static byte ToByte(this bool[] bools) {
            if (bools.Length > 8)
                throw new ArgumentException("BoolsToByte: This method only support 8 bools at a time");

            BitArray total = new(new byte[1]);

            for (int i = 0; i < bools.Length; i++)
                total.Set(i, bools[i]);


            return total.ToByte();
        }

        /// <summary>
        /// Converts a single byte to a BitArray representing its individual bits.
        /// </summary>
        /// <param name="b">The byte to convert.</param>
        /// <returns>A BitArray containing the bits of the byte.</returns>
        /// <remarks>
        /// This method creates a byte array containing the single byte and then converts it to a BitArray using the built-in constructor.
        /// </remarks>
        public static BitArray ToBits(this byte b) {
            byte[] bytes = [b];

            return new BitArray(bytes);
        }

        /// <summary>
        /// Converts a single byte to an array of booleans with a specified length.
        /// </summary>
        /// <param name="b">The byte to convert.</param>
        /// <param name="length">The desired length of the boolean array. (Must be less than or equal to 8)</param>
        /// <returns>An array of booleans representing the bits of the byte, padded with false if the length is less than 8.</returns>
        /// <exception cref="ArgumentException">Thrown if the desired length is greater than 8.</exception>
        public static bool[] ByteToBools(this byte b, int length) {
            bool[] bools = new bool[length];

            BitArray bits = b.ToBits();

            for (int i = 0; i < length; i++)
                bools[i] = bits[i];

            return bools;
        }

    }
}
