using Godot;

namespace Godot_XTension_Pack {
    public static class Camera3DExtension {
        /// <summary>
        /// Gets the world-space origin of the ray projected from the center of the camera's viewport.
        /// This method is useful for determining the center of the camera's viewable area.
        /// </summary>
        /// <param name="camera">The Camera3D object to use.</param>
        /// <returns>The origin of the projected ray in world space.</returns>
        public static Vector3 CenterByRayOrigin(this Camera3D camera) => camera.ProjectRayOrigin(Vector2.Zero);

        /// <summary>
        /// Gets the origin (position) of the camera's global transformation in world space.
        /// This method provides the camera's absolute position in the world.
        /// </summary>
        /// <param name="camera">The Camera3D object to use.</param>
        /// <returns>The origin of the camera's global transformation in world space.</returns>
        public static Vector3 CenterByOrigin(this Camera3D camera) => camera.GlobalTransform.Origin;

        /// <summary>
        /// (Commented out) Gets the forward direction of the camera in world space.
        /// </summary>
        /// <param name="camera">The camera for which to retrieve the forward direction.</param>
        /// <returns>The forward direction of the camera as a Vector3 (commented out).</returns>
        public static Vector3 ForwardDirection(this Camera3D camera) => Vector3.Forward.Z * camera.GlobalTransform.Basis.Z.Normalized();

        /// <summary>
        /// (Commented out) Checks if a node is facing the camera based on its Z-axis direction.
        /// </summary>
        /// <param name="camera">The camera to check against.</param>
        /// <param name="node">The node to check for facing the camera.</param>
        /// <returns>True if the node's Z-axis points away from the camera, False otherwise (commented out).</returns>
        public static bool IsFacingCamera(this Camera3D camera, Node3D node) => camera.GlobalPosition.Dot(node.Basis.Z) < 0;
    }

}