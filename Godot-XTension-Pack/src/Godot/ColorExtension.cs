using Godot;

namespace Godot_XTension_Pack {
    public static class ColorExtension {

        /// <summary>
        /// Converts a Color object to a Vector3 containing its red, green, and blue channel values.
        /// </summary>
        /// <param name="color">The Color object to convert.</param>
        /// <returns>A new Vector3 with R, G, and B components corresponding to the color channels.</returns>
        public static Vector3 Vector3(this Color color) => new(color.R, color.G, color.B);

        /// <summary>
        /// Converts a Color object to a Vector4 containing its red, green, blue, and alpha channel values.
        /// </summary>
        /// <param name="color">The Color object to convert.</param>
        /// <returns>A new Vector4 with R, G, B, and A components corresponding to the color channels.</returns>
        public static Vector4 Vector4(this Color color) => new(color.R, color.G, color.B, color.A);

        /// <summary>
        /// Converts a Color object to a Vector3 containing its hue, saturation, and value components in HSV color space.
        /// </summary>
        /// <param name="color">The Color object to convert.</param>
        /// <returns>A new Vector3 with H, S, and V components representing the color in HSV.</returns>
        public static Vector3 Vector3Hsv(this Color color) => new(color.H, color.S, color.V);

        /// <summary>
        /// Converts a Color object to a Vector4 containing its hue, saturation, value, and alpha channel values in HSV color space.
        /// </summary>
        /// <param name="color">The Color object to convert.</param>
        /// <returns>A new Vector4 with H, S, V, and A components representing the color in HSV.</returns>
        public static Vector4 Vector4Hsv(this Color color) => new(color.H, color.S, color.V, color.A);

        /// <summary>
        /// Checks if two colors are considered similar within a specified tolerance.
        /// </summary>
        /// <param name="color">The Color to compare.</param>
        /// <param name="otherColor">The other Color to compare against.</param>
        /// <param name="tolerance">The threshold for considering colors similar (default: 100f, higher values allow for more variation).</param>
        /// <returns>True if the distance between the colors in vector space is less than or equal to the tolerance divided by 255, false otherwise.</returns>
        /// <remarks>
        /// This extension method provides a way to compare two colors for similarity based on a tolerance value. Colors are represented as Vector4 internally, and the distance between their vectors is used to determine similarity. The higher the tolerance, the more variation is allowed for colors to be considered similar.
        /// </remarks>
        public static bool SimilarTo(this Color color, Color otherColor, float tolerance = 100f)
            => color.Vector4().DistanceTo(otherColor.Vector4()) <= (tolerance / 255f);


        /// <summary>
        /// Generates a random Color using Unity's RandomNumberGenerator class.
        /// </summary>
        /// <param name="alpha">The alpha transparency value for the color (0.0 to 1.0, default: 255 for fully opaque).</param>
        /// <returns>A random Color object with red, green, and blue components ranging from 0.0 to 1.0 and the specified alpha value.</returns>
        public static Color RandomColor(float alpha = 255f) {
            RandomNumberGenerator rng = new();

            return new Color(rng.RandfRange(0, 1f), rng.RandfRange(0, 1f), rng.RandfRange(0, 1f), alpha / 255f);
        }
    }
