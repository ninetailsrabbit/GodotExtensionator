namespace GodotExtensionator {
    public static partial class MathExtension {

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

    }
}
