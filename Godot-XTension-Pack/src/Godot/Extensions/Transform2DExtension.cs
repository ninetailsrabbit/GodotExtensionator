using Godot;

namespace Godot_XTension_Pack {
    public static class Transform2DExtension {

        /// <summary>
        /// Gets the right unit vector of the transform's local space.
        /// </summary>
        /// <param name="transform">The Transform2D instance.</param>
        /// <returns>A Vector2 representing the right unit vector (1, 0) in the transform's local space.</returns>
        public static Vector2 Right(this Transform2D transform) => transform.X;

        /// <summary>
        /// Gets the left unit vector of the transform's local space.
        /// </summary>
        /// <param name="transform">The Transform2D instance.</param>
        /// <returns>A Vector2 representing the left unit vector (-1, 0) in the transform's local space.</returns>
        public static Vector2 Left(this Transform2D transform) => -transform.X;

        /// <summary>
        /// Gets the up unit vector of the transform's local space.
        /// </summary>
        /// <param name="transform">The Transform2D instance.</param>
        /// <returns>A Vector2 representing the up unit vector (0, -1) in the transform's local space.</returns>
        public static Vector2 Up(this Transform2D transform) => -transform.Y;

        /// <summary>
        /// Gets the down unit vector of the transform's local space.
        /// </summary>
        /// <param name="transform">The Transform2D instance.</param>
        /// <returns>A Vector2 representing the down unit vector (0, 1) in the transform's local space.</returns>
        public static Vector2 Down(this Transform2D transform) => transform.Y;
    }
}
