using Godot;

namespace GodotExtensionator {

    public static class Transform3DExtension {

        /// <summary>
        /// Gets the forward direction vector relative to the transform's rotation (equivalent to -Z axis in its basis).
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <returns>A Vector3 representing the forward direction based on the transform's rotation.</returns>
        public static Vector3 Forward(this Transform3D transform) => -transform.Basis.Z;

        /// <summary>
        /// Gets the backward direction vector relative to the transform's rotation (equivalent to Z axis in its basis).
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <returns>A Vector3 representing the backward direction based on the transform's rotation.</returns>
        public static Vector3 Back(this Transform3D transform) => transform.Basis.Z;

        /// <summary>
        /// Gets the backward direction vector relative to the transform's rotation (equivalent to -X axis in its basis).
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <returns>A Vector3 representing the backward direction based on the transform's rotation.</returns>
        public static Vector3 Left(this Transform3D transform) {
            return -transform.Basis.X;
        }

        /// <summary>
        /// Gets the backward direction vector relative to the transform's rotation (equivalent to X axis in its basis).
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <returns>A Vector3 representing the backward direction based on the transform's rotation.</returns>
        public static Vector3 Right(this Transform3D transform) {
            return transform.Basis.X;
        }

        /// <summary>
        /// Gets the backward direction vector relative to the transform's rotation (equivalent to Y axis in its basis).
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <returns>A Vector3 representing the backward direction based on the transform's rotation.</returns>
        public static Vector3 Up(this Transform3D transform) {
            return transform.Basis.Y;
        }

        /// <summary>
        /// Gets the backward direction vector relative to the transform's rotation (equivalent to -Y axis in its basis).
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <returns>A Vector3 representing the backward direction based on the transform's rotation.</returns>
        public static Vector3 Down(this Transform3D transform) {
            return -transform.Basis.Y;
        }

        /// <summary>
        /// Aligns the transform's up direction to a new up direction vector.
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <param name="newUp">The new up direction vector.</param>
        /// <returns>A new Transform3D with its up direction aligned to the provided newUp vector.</returns>
        public static Transform3D AlignUp(this Transform3D transform, Vector3 newUp) {
            return Align(transform, transform.Basis.Y, newUp);
        }


        /// <summary>
        /// Aligns the transform's up direction to a new down direction vector.
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <param name="newDown">The new down direction vector.</param>
        /// <returns>A new Transform3D with its down direction aligned to the provided newDown vector.</returns>
        public static Transform3D AlignDown(this Transform3D transform, Vector3 newDown) {
            return Align(transform, -transform.Basis.Y, newDown);
        }


        /// <summary>
        /// Aligns the transform's up direction to a new left direction vector.
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <param name="newLeft">The new left direction vector.</param>
        /// <returns>A new Transform3D with its left direction aligned to the provided newLeft vector.</returns>
        public static Transform3D AlignLeft(this Transform3D transform, Vector3 newLeft) {
            return Align(transform, transform.Basis.X, newLeft);
        }


        /// <summary>
        /// Aligns the transform's up direction to a new right direction vector.
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <param name="newRight">The new right direction vector.</param>
        /// <returns>A new Transform3D with its right direction aligned to the provided newRight vector.</returns>
        public static Transform3D AlignRight(this Transform3D transform, Vector3 newRight) {
            return Align(transform, transform.Basis.X, newRight);
        }


        /// <summary>
        /// Aligns the transform's rotation to face a target direction from a specified current direction.
        /// </summary>
        /// <param name="transform">The Transform3D object.</param>
        /// <param name="currentDirection">The current direction vector relative to the transform (basis vector).</param>
        /// <param name="targetDirection">The target direction vector to align towards.</param>
        /// <returns>A new Transform3D with its rotation adjusted to face the target direction.</returns>
        public static Transform3D Align(this Transform3D transform, Vector3 currentDirection, Vector3 targetDirection) {
            currentDirection = currentDirection.Normalized();
            targetDirection = targetDirection.Normalized();

            if (targetDirection.DistanceSquaredTo(currentDirection) < 0.001f)
                return transform;

            var cosA = currentDirection.Dot(targetDirection);
            var alpha = Mathf.Acos(Mathf.Clamp(cosA, -1.0f, 1.0f));

            if (Math.Abs(alpha % Mathf.Pi) < 0.001f) // Vectors are parallel
                return transform.Rotated(transform.Basis.X.Normalized(), alpha);

            var axis = currentDirection.Cross(targetDirection);
            axis = axis.Normalized();

            return transform.Rotated(axis, alpha);
        }

        /// <summary>
        /// Checks if two Transform3D objects are approximately equal in terms of their rotation and position.
        /// </summary>
        /// <param name="from">The first Transform3D object.</param>
        /// <param name="to">The second Transform3D object.</param>
        /// <returns>True if the transforms' rotations and positions are approximately equal (using default precision), False otherwise.</returns>
        public static bool EqualTransformApprox(this Transform3D from, Transform3D to) {
            return from.Basis.IsEqualApprox(to.Basis) && from.Origin.EqualsPositionApprox(to.Origin);
        }

    }
}
