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
    }
}
