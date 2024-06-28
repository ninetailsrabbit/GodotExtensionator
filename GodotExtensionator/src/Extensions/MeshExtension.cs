using Godot;

namespace GodotExtensionator {
    public static class MeshInstanceExtensions {

        private static readonly RandomNumberGenerator _rng = new();

        /// <summary>
        /// Gets a random position on the surface of a 2D mesh instance (if it has a mesh).
        /// </summary>
        /// <param name="meshInstance">The 2D mesh instance to sample a surface position from.</param>
        /// <returns>A random position within the bounds of the mesh surface, or Vector3.Zero if the mesh is null.</returns>
        public static Vector3 GetRandomSurfacePosition(this MeshInstance2D meshInstance) {
            if (meshInstance.HasMesh())
                meshInstance.Mesh.GetRandomSurfacePosition();

            return Vector3.Zero;
        }

        /// <summary>
        /// Gets a random position on the surface of the attached mesh (if available).
        /// </summary>
        /// <param name="meshInstance">The MeshInstance3D to get the random surface position from.</param>
        /// <returns>A random position on the surface of the attached mesh, or Vector3.Zero if no mesh is attached.</returns>
        /// <remarks>
        /// This extension method attempts to retrieve a random surface position from the Mesh attached to the MeshInstance3D.
        /// It returns Vector3.Zero if no mesh is attached to the MeshInstance3D.
        /// </remarks>
        public static Vector3 GetRandomSurfacePosition(this MeshInstance3D meshInstance) {
            if (meshInstance.HasMesh())
                meshInstance.Mesh.GetRandomSurfacePosition();

            return Vector3.Zero;
        }

        /// <summary>
        /// Gets a random position on the surface of a mesh.
        /// </summary>
        /// <param name="mesh">The mesh to get the random surface position from.</param>
        /// <returns>A random position on the surface of the mesh.</returns>
        /// <remarks>
        /// This method efficiently retrieves a random position on the surface of the provided mesh by:
        ///   1. Obtaining all faces of the mesh.
        ///   2. Selecting a random face.
        ///   3. Generating a random position within the bounds of the selected face.
        ///   4. Taking the absolute value of the generated position to ensure it lies on the surface.
        /// </remarks>
        public static Vector3 GetRandomSurfacePosition(this Mesh mesh) {
            Vector3[] faces = mesh.GetFaces();
            Vector3 randomFace = faces[_rng.Randi() % faces.Length].Abs();

            return new Vector3(
                _rng.RandfRange(-randomFace.X, randomFace.X),
                _rng.RandfRange(-randomFace.Y, randomFace.Y),
                _rng.RandfRange(-randomFace.Z, randomFace.Z)
            );
        }

        /// <summary>
        /// Checks if a 2D mesh instance has a valid mesh attached.
        /// </summary>
        /// <param name="meshInstance">The 2D mesh instance to check.</param>
        /// <returns>True if the mesh instance has a mesh, False otherwise.</returns>
        public static bool HasMesh(this MeshInstance2D meshInstance) => meshInstance.Mesh is not null;

        /// <summary>
        /// Checks if a ·D mesh instance has a valid mesh attached.
        /// </summary>
        /// <param name="meshInstance">The 3D mesh instance to check.</param>
        /// <returns>True if the mesh instance has a mesh, False otherwise.</returns>
        public static bool HasMesh(this MeshInstance3D meshInstance) => meshInstance.Mesh is not null;
    }

}