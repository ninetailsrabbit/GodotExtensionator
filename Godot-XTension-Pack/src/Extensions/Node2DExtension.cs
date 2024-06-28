using Godot;

namespace Godot_XTension_Pack {
    public static class Node2DExtension {
        /// <summary>
        /// Get the mouse direction this node is pointing to
        /// </summary>
        /// <param name="node">The first Node2D object.</param>
        /// <returns>The mouse direction relative to this node</returns>
        /// <remarks>
        /// Global distance considers the nodes' positions within the entire scene's coordinate system,
        /// including any transformations applied to their parent nodes or ancestors.
        /// </remarks>
        public static Vector2 GetMouseDirection(this Node2D node) => node.GlobalPosition.DirectionTo(node.GetGlobalMousePosition());

        /// <summary>
        /// Calculates the global distance between two Node2D objects.
        /// </summary>
        /// <param name="node">The first Node2D object.</param>
        /// <param name="target">The second Node2D object.</param>
        /// <returns>The distance between the two nodes in global coordinates.</returns>
        /// <remarks>
        /// Global distance considers the nodes' positions within the entire scene's coordinate system,
        /// including any transformations applied to their parent nodes or ancestors.
        /// </remarks>
        public static float GlobalDistanceTo(this Node2D node, Node2D target) => node.GlobalPosition.DistanceTo(target.GlobalPosition);

        /// <summary>
        /// Calculates the local distance between two Node2D objects.
        /// </summary>
        /// <param name="node">The first Node2D object.</param>
        /// <param name="target">The second Node2D object.</param>
        /// <returns>The distance between the two nodes in local coordinates.</returns>
        /// <remarks>
        /// Local distance only considers the nodes' positions relative to their common parent or root,
        /// ignoring any transformations inherited from parent nodes.
        /// </remarks>
        public static float LocalDistanceTo(this Node2D node, Node2D target) => node.Position.DistanceTo(target.Position);

        /// <summary>
        /// Calculates the global direction vector pointing from one Node2D object to another.
        /// </summary>
        /// <param name="node">The origin Node2D object.</param>
        /// <param name="target">The destination Node2D object.</param>
        /// <returns>A Vector2 representing the direction from the origin node to the target node in global coordinates.</returns>
        /// <remarks>
        /// Global direction considers the nodes' positions within the entire scene's coordinate system,
        /// accounting for any transformations applied to their parent nodes or ancestors.
        /// </remarks>
        public static Vector2 GlobalDirectionTo(this Node2D node, Node2D target) => node.GlobalPosition.DirectionTo(target.GlobalPosition);

        /// <summary>
        /// Calculates the local direction vector pointing from one Node2D object to another.
        /// </summary>
        /// <param name="node">The origin Node2D object.</param>
        /// <param name="target">The destination Node2D object.</param>
        /// <returns>A Vector2 representing the direction from the origin node to the target node in local coordinates.</returns>
        /// <remarks>
        /// Local direction only considers the nodes' positions relative to their common parent or root,
        /// ignoring any transformations inherited from parent nodes.
        /// </remarks>
        public static Vector2 LocalDirectionTo(this Node2D node, Node2D target) => node.Position.DirectionTo(target.Position);

        /// <summary>
        /// Calculates the absolute Z-index of a Node2D by recursively summing its own Z-index and parent Node2D's Z-index as long as the parent uses relative positioning.
        /// </summary>
        /// <param name="node">The Node2D for which to calculate the absolute Z-index.</param>
        /// <returns>The absolute Z-index value, representing the node's stacking order within the scene.</returns>
        public static int GetAbsoluteZIndex(this Node2D node) {
            int absoluteZ = 0;

            while (node is not null) {
                absoluteZ += node.ZIndex;

                if (!node.ZAsRelative)
                    break;

                node = (Node2D)node.GetParent();
            }

            return absoluteZ;
        }

        /// <summary>
        /// Finds the farthest Node2D within a specified distance range from a given Node2D.
        /// </summary>
        /// <param name="node">The Node2D from which to find the farthest neighbor.</param>
        /// <param name="nodes">An IEnumerable collection of Node2D objects to search through.</param>
        /// <param name="minDistance">The minimum distance threshold (inclusive) for considering a node as a neighbor (default: 0.0f).</param>
        /// <param name="maxDistance">The maximum distance threshold (inclusive) for considering a node as a neighbor (default: 9999.0f).</param>
        /// <returns>A Node2D object containing the farthest Node2D and its distance, or null if no nodes are found within the range.</returns>
        public static Node2D? GetNearestNodeByDistance(this Node2D node, IEnumerable<Node2D> nodes, float minDistance = 0.0f, float maxDistance = 9999.0f) {
            Node2D? foundNode = null;
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
        /// Finds the nearest Node2D within a specified distance range from a given Node2D.
        /// </summary>
        /// <param name="node">The Node2D from which to find the nearest neighbor.</param>
        /// <param name="nodes">An IEnumerable collection of Node2D objects to search through.</param>
        /// <param name="minDistance">The minimum distance threshold (inclusive) for considering a node as a neighbor (default: 0.0f).</param>
        /// <param name="maxDistance">The maximum distance threshold (inclusive) for considering a node as a neighbor (default: 9999.0f).</param>
        /// <returns>A Node2D object containing the nearest Node2D and its distance, or null if no nodes are found within the range.</returns>
        public static Node2D? GetFarthestNodeByDistance(this Node2D node, IEnumerable<Node2D> nodes, float minDistance = 0.0f, float maxDistance = 9999.0f) {
            Node2D? foundNode = null;
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

        /// <summary>
        /// Gets the screen position of a Node2D in world coordinates.
        /// </summary>
        /// <param name="node">The Node2D for which to retrieve the screen position.</param>
        /// <returns>A Vector2 representing the node's position on the screen in world coordinates.</returns>
        public static Vector2 ScreenPosition(this Node2D node) => node.GetGlobalTransformWithCanvas().Origin;

        /// <summary>
        /// Checks if a Node2D is currently visible on the screen with an optional margin.
        /// </summary>
        /// <param name="node">The Node2D to check for on-screen visibility.</param>
        /// <param name="margin">An optional margin (in pixels) to consider around the screen edges (default: 16.0f).</param>
        /// <returns>True if the node's position is within the visible screen area considering the margin, False otherwise.</returns>
        public static bool OnScreen(this Node2D node, float margin = 16.0f) {
            if (node.IsValid() && node.IsInsideTree()) {

                Rect2 screenRect = node.GetViewportRect();
                screenRect.Position -= Vector2.One * margin;
                screenRect.Size += Vector2.One * margin * 2.0f;

                return screenRect.HasPoint(node.ScreenPosition());
            }

            return false;
        }
    }
}
