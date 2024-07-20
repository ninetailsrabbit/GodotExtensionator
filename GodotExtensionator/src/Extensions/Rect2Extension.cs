using Godot;

namespace GodotExtensionator {
    public static class Rect2Extension {
        /// <summary>
        /// Generates a random point within the bounds of a rectangle.
        /// </summary>
        /// <param name="rect">The rectangle defining the area for the random point.</param>
        /// <returns>A random Vector2 point located inside the rectangle.</returns>
        public static Vector2 RandomPoint(this Rect2 rect) {
            return new(GD.Randf() * rect.Size.X, GD.Randf() * rect.Size.Y);
        }

        /// <summary>
        /// Gets the top edge position of the Rect2.
        /// </summary>
        /// <param name="rect">The Rect2 to get the top position from.</param>
        /// <returns>The y-coordinate of the top edge.</returns>
        public static float Top(this Rect2 rect) => rect.Position.Y;

        /// <summary>
        /// Gets the bottom edge position of the Rect2.
        /// </summary>
        /// <param name="rect">The Rect2 to get the bottom position from.</param>
        /// <returns>The y-coordinate of the bottom edge.</returns>
        public static float Bottom(this Rect2 rect) => rect.Position.Y + rect.Size.Y;

        /// <summary>
        /// Gets the left edge position of the Rect2.
        /// </summary>
        /// <param name="rect">The Rect2 to get the left position from.</param>
        /// <returns>The x-coordinate of the left edge.</returns>
        public static float Left(this Rect2 rect) => rect.Position.X;

        /// <summary>
        /// Gets the right edge position of the Rect2.
        /// </summary>
        /// <param name="rect">The Rect2 to get the right position from.</param>
        /// <returns>The x-coordinate of the right edge.</returns>
        public static float Right(this Rect2 rect) => rect.Position.X + rect.Size.X;
    }
}
