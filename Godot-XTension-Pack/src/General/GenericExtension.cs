namespace Godot_XTension_Pack.src.General {
    public static class GenericExtension {
        /// <summary>
        /// Checks if a value is present within a given set of values.
        /// </summary>
        /// <typeparam name="T">The type of the value and the elements in the values collection.</typeparam>
        /// <param name="val">The value to check for.</param>
        /// <param name="values">A collection of possible values.</param>
        /// <returns>True if the value is found in the collection; otherwise, false.</returns>
        /// <remarks>
        /// This overload uses the `Contains` method of an array to efficiently check for the presence of the value.
        /// It's limited to value types (`struct`) due to array constraints.
        /// </remarks>
        public static bool In<T>(this T val, params T[] values) where T : struct {
            return values.Contains(val);
        }

        /// <summary>
        /// Checks if a value is present within a collection of values.
        /// </summary>
        /// <typeparam name="T">The type of the value and the elements in the values collection.</typeparam>
        /// <param name="val">The value to check for.</param>
        /// <param name="values">A collection of possible values.</param>
        /// <returns>True if the value is found in the collection; otherwise, false.</returns>
        /// <remarks>
        /// This overload uses the `Contains` method of an `IEnumerable&lt;T&gt;` to allow for various collection types.
        /// It works with both value types (`struct`) and reference types (`class`).
        /// </remarks>
        public static bool In<T>(this T val, IEnumerable<T> values) where T : struct {
            return values.Contains(val);
        }
    }
}
