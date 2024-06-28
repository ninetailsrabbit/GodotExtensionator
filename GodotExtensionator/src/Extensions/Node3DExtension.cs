using Godot;

namespace GodotExtensionator {
    public static class Node3DExtension {
        /// <summary>
        /// Checks if the calling Node3D is facing the target Node3D.
        /// </summary>
        /// <param name="node">The Node3D to check if it's facing the target.</param>
        /// <param name="target">The target Node3D to check if it's in front of the calling node.</param>
        /// <returns>True if the target is within the forward facing cone of the calling node, False otherwise.</returns>
        public static bool IsFacing(this Node3D node, Node3D target) => target.GlobalPosition.Dot(node.Basis.Z) < 0;

        /// <summary>
        /// Calculates the global distance between two Node3D objects.
        /// </summary>
        /// <param name="node">The first Node3D object.</param>
        /// <param name="target">The second Node3D object.</param>
        /// <returns>The distance between the two nodes in global coordinates.</returns>
        /// <remarks>
        /// Global distance considers the nodes' positions within the entire scene's coordinate system,
        /// including any transformations applied to their parent nodes or ancestors.
        /// </remarks>
        public static float GlobalDistanceTo(this Node3D node, Node3D target) => node.GlobalPosition.DistanceTo(target.GlobalPosition);


        /// <summary>
        /// Calculates the local distance between two Node3D objects.
        /// </summary>
        /// <param name="node">The first Node3D object.</param>
        /// <param name="target">The second Node3D object.</param>
        /// <returns>The distance between the two nodes in local coordinates.</returns>
        /// <remarks>
        /// Local distance only considers the nodes' positions relative to their common parent or root,
        /// ignoring any transformations inherited from parent nodes.
        /// </remarks>
        public static float LocalDistanceTo(this Node3D node, Node3D target) => node.Position.DistanceTo(target.Position);

        /// <summary>
        /// Calculates the global direction vector pointing from one Node3D object to another.
        /// </summary>
        /// <param name="node">The origin Node3D object.</param>
        /// <param name="target">The destination Node3D object.</param>
        /// <returns>A Vector3 representing the direction from the origin node to the target node in global coordinates.</returns>
        /// <remarks>
        /// Global direction considers the nodes' positions within the entire scene's coordinate system,
        /// accounting for any transformations applied to their parent nodes or ancestors.
        /// </remarks>
        public static Vector3 GlobalDirectionTo(this Node3D node, Node3D target) => node.GlobalPosition.DirectionTo(target.GlobalPosition);


        /// <summary>
        /// Calculates the local direction vector pointing from one Node3D object to another.
        /// </summary>
        /// <param name="node">The origin Node3D object.</param>
        /// <param name="target">The destination Node3D object.</param>
        /// <returns>A Vector3 representing the direction from the origin node to the target node in local coordinates.</returns>
        /// <remarks>
        /// Local direction only considers the nodes' positions relative to their common parent or root,
        /// ignoring any transformations inherited from parent nodes.
        /// </remarks>
        public static Vector3 LocalDirectionTo(this Node3D node, Node3D target) => node.Position.DirectionTo(target.Position);

        /// <summary>
        /// Finds the nearest Node3D within a specified distance range from a given Node3D.
        /// </summary>
        /// <param name="node">The Node3D from which to find the nearest neighbor.</param>
        /// <param name="nodes">An IEnumerable collection of Node3D objects to search through.</param>
        /// <param name="minDistance">The minimum distance threshold (inclusive) for considering a node as a neighbor (default: 0.0f).</param>
        /// <param name="maxDistance">The maximum distance threshold (inclusive) for considering a node as a neighbor (default: 9999.0f).</param>
        /// <returns>A Node3D object containing the nearest Node3D and its distance, or null if no nodes are found within the range.</returns>
        public static Node3D? GetNearestNodeByDistance(this Node3D node, IEnumerable<Node3D> nodes, float minDistance = 0.0f, float maxDistance = 9999.0f) {
            Node3D? foundNode = null;
            float previousDistance = 0.0f;

            foreach (var targetNode in nodes.Where((child) => child.IsValid() && child.IsInsideTree() && !child.Equals(node))) {
                float distanceToTarget = node.GlobalDistanceTo(targetNode);

                if (distanceToTarget >= minDistance && distanceToTarget <= maxDistance && (foundNode == null || distanceToTarget < previousDistance)) {
                    foundNode = targetNode;
                    previousDistance = distanceToTarget;
                }

            }

            return foundNode;
        }

        /// <summary>
        /// Finds the farthest Node3D within a specified distance range from a given Node3D.
        /// </summary>
        /// <param name="node">The Node3D from which to find the farthest neighbor.</param>
        /// <param name="nodes">An IEnumerable collection of Node3D objects to search through.</param>
        /// <param name="minDistance">The minimum distance threshold (inclusive) for considering a node as a neighbor (default: 0.0f).</param>
        /// <param name="maxDistance">The maximum distance threshold (inclusive) for considering a node as a neighbor (default: 9999.0f).</param>
        /// <returns>A Node3D object containing the farthest Node3D and its distance, or null if no nodes are found within the range.</returns>
        public static Node3D? GetFarthestNodeByDistance(this Node3D node, IEnumerable<Node3D> nodes, float minDistance = 0.0f, float maxDistance = 9999.0f) {
            Node3D? foundNode = null;
            float previousDistance = 0.0f;

            foreach (var targetNode in nodes.Where((child) => child.IsValid() && child.IsInsideTree() && !child.Equals(node))) {
                float distanceToTarget = node.GlobalDistanceTo(targetNode);

                if (distanceToTarget >= minDistance && distanceToTarget <= maxDistance && (foundNode == null || distanceToTarget > previousDistance)) {
                    foundNode = targetNode;
                    previousDistance = distanceToTarget;
                }

            }

            return foundNode;
        }
    }
}
