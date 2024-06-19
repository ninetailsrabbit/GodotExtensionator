namespace Godot_XTension_Pack {

    public static class RandomExtension {

        /// <summary>
        /// Generates a random float value within a specified range (inclusive).
        /// </summary>
        /// <param name="random">The <see cref="Random"/> object to use for generating random numbers.</param>
        /// <param name="minValue">The minimum value (inclusive) of the desired range. Defaults to 1.0f.</param>
        /// <param name="maxValue">The maximum value (inclusive) of the desired range. Defaults to 1.0f.</param>
        /// <returns>
        /// A random float value between <paramref name="minValue"/> (inclusive) and <paramref name="maxValue"/> (inclusive).
        /// </returns>
        /// <remarks>
        /// This extension method provides a way to generate random floats within a specific range, similar to how `Random.Next` works with integers. It ensures the generated value falls within the provided range and handles cases where the minimum value is greater than the maximum value.
        /// 
        /// **Note:** This approach uses a different algorithm than `Random.NextDouble` and might not be suitable for all scenarios where a uniformly distributed random float is needed. Refer to the linked Stack Overflow discussion for details on alternative approaches.
        /// 
        /// https://stackoverflow.com/questions/63650648/random-float-in-c-sharp
        /// </remarks>
        public static float NextFloat(this Random random, float minValue = float.MinValue, float maxValue = float.MaxValue) {
            double range = (double)maxValue - (double)minValue;
            double sample = random.NextDouble();
            double scaled = (sample * range) + minValue;

            return (float)scaled;
        }

        /// <summary>
        /// Generates a random angle in radians between 0 (inclusive) and 2π (exclusive).
        /// </summary>
        /// <param name="random">The Random object used for generating random numbers.</param>
        /// <returns>A random floating-point value representing an angle in radians.</returns>
        /// <remarks>
        /// This extension method leverages the `NextDouble` method of the `Random` object to generate a random double-precision floating-point number between 0.0 (inclusive) and 1.0 (exclusive).
        /// It then multiplies this value by 2π (PI * 2) to obtain a random value within the desired range of 0 to 2π radians.
        /// The result is cast to a float to represent the generated random angle.
        /// </remarks>
        public static float NextAngle(this Random random) => (float)(random.NextDouble() * Math.PI * 2);

        /// <summary>
        /// Determines whether a random event occurs based on a specified probability (percentage).
        /// </summary>
        /// <param name="random">The Random object used for generating random numbers.</param>
        /// <param name="percent">The probability (percentage) of the event occurring (0.0 to 1.0).</param>
        /// <returns>True if the event occurs based on the random number and probability, False otherwise.</returns>
        /// <remarks>
        /// This extension method utilizes the `NextFloat` method of the `Random` object to generate a random single-precision floating-point number between 0.0 (inclusive) and 1.0 (exclusive).
        /// It then uses the `Math.Clamp` method to ensure the provided `percent` value is within the valid range of 0.0 to 1.0.
        /// Finally, it compares the generated random number with the clamped probability.
        /// If the random number is less than the probability, the method returns `true` indicating the event occurs. Otherwise, it returns `false`.
        /// </remarks>
        public static bool Chance(this Random random, float percent) => random.NextFloat() < Math.Clamp(percent, 0, 1f);

        /// <summary>
        /// Determines whether a random event occurs based on a specified probability (represented as an integer).
        /// </summary>
        /// <param name="random">The Random object used for generating random numbers.</param>
        /// <param name="value">The probability (represented as an integer between 0 and 100).</param>
        /// <returns>True if the event occurs based on the random number and probability, False otherwise.</returns>
        /// <remarks>
        /// This extension method provides an alternative way to specify probability using an integer value between 0 and 100.
        /// It first casts the `value` to an integer to ensure it's treated as a whole number.
        /// Then, it generates a random integer between 0 (inclusive) and 100 (exclusive) using `NextDouble` multiplied by 100 and cast to an integer.
        /// Similar to the previous overload, it compares the generated random integer with the provided `value`.
        /// If the random integer is less than the `value`, the method returns `true`. Otherwise, it returns `false`.
        /// </remarks>
        public static bool Chance(this Random random, int value) => (int)(random.NextDouble() * 100) < Math.Clamp(0, 100, value);

        /// <summary>
        /// Selects a random element from a provided array of values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the values array.</typeparam>
        /// <param name="random">The Random object used for generating the random index.</param>
        /// <param name="values">An array of values of type T from which to select a random element.</param>
        /// <returns>A random element from the provided values array.</returns>
        /// <remarks>
        /// This extension method utilizes the `Random.Next` method to generate a random index within the bounds of the `values` array.
        /// It then uses the generated index to access and return a random element from the array.
        /// This approach offers a convenient way to pick a random value from a predefined set.
        /// 
        /// However, it's important to note that:
        /// * Passing an empty array will result in an ArgumentOutOfRangeException.
        /// * All elements in the `values` array must be of the same type `T` to avoid type mismatches.
        /// </remarks>
        public static T OneOf<T>(this Random random, params T[] values) => values[random.Next(values.Length)];
        
    }
}