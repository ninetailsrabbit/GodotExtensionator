namespace Godot_XTension_Pack {
    public static class BoolExtension {

        /// <summary>
        /// Flips the boolean value to its opposite (true becomes false, false becomes true).
        /// </summary>
        /// <param name="value">The boolean value to be toggled.</param>
        /// <returns>The opposite of the provided boolean value.</returns>
        /// <remarks>
        /// This extension method provides a concise way to invert a boolean value.
        /// It utilizes the logical NOT (!) operator to negate the original value, effectively reversing its state.
        /// </remarks>
        public static bool Toggle(this bool value) => !value;

        /// <summary>
        /// Converts a boolean value to a signed integer representation (true = 1, false = -1).
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <returns>An integer representing the sign of the boolean value (1 for true, -1 for false).</returns>
        /// <remarks>
        /// This extension method offers a convenient way to translate a boolean value into a signed integer.
        /// It utilizes a ternary conditional operator to return 1 for `true` and -1 for `false`.
        /// This allows you to represent the boolean value as a signed integer for specific use cases.
        /// </remarks>
        public static int ToSign(this bool value) => value ? 1 : -1;

        /// <summary>
        /// Converts a boolean value to an integer representation (true = 1, false = 0).
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <returns>An integer representing the boolean value (1 for true, 0 for false).</returns>
        /// <remarks>
        /// This extension method is similar to `ToSign` but offers a simpler integer representation.
        /// It uses a ternary conditional operator to return 1 for `true` and 0 for `false`.
        /// This conversion allows you to represent the boolean value as a basic integer for various applications.
        /// </remarks>
        public static int ToInt(this bool value) => value ? 1 : 0;
    }
}
