namespace Godot_XTension_Pack.src.General {
    public static class EnumExtension {

        /// <summary>
        /// Gets a random value from the specified enum type. 
        /// Example: EnumExtension.RandomEnum<Input.MouseModeEnum>()
        /// </summary>
        /// <typeparam name="T">The enum type to get a random value from.</typeparam>
        /// <param name="enumType">The enum type.</param>
        /// <returns>A random enum value of type T.</returns>
        public static T RandomEnum<T>() {
            T[] values = (T[])Enum.GetValues(typeof(T));

            int randomIndex = new Random().Next(values.Length);

            return values[randomIndex];
        }

        /// <summary>
        /// Gets a random value from an enum type.
        /// Example: Input.MouseModeEnum.Visible.Random()
        /// </summary>
        /// <typeparam name="T">The enum type to get a random value from.</typeparam>
        /// <param name="self">A placeholder parameter for the extension method (ignored).</param>
        /// <returns>A random enum value of type T.</returns>
        /// <exception cref="ArgumentException">Throws an ArgumentException if T is not a struct or an Enum.</typeparam>
        public static T RandomEnum<T>(this T _) where T : struct, Enum {
            T[] values = Enum.GetValues<T>();

            int randomIndex = new Random().Next(values.Length);

            return values[randomIndex];
        }


    }
}

