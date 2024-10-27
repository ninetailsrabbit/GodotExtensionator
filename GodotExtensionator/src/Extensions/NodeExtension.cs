using Extensionator;
using Godot;
using Godot.Collections;
using System.Text.RegularExpressions;

namespace GodotExtensionator
{

    public static partial class NodeExtension
    {
        // A regular expression to detect original node names
        public static readonly Regex NameRegex = NameRegexGenerated();

        [GeneratedRegex("@*(?<Name>[\\w|\\d]+)@*.*")]
        private static partial Regex NameRegexGenerated();

        /// <summary>
        /// Enables processing for the Node. The node will be processed during the engine's update loop according to its inherited process mode.
        /// </summary>
        /// <param name="node">The Node to enable processing for.</param>
        public static void Enable(this Node node)
        {
            node.ProcessMode = Node.ProcessModeEnum.Inherit;
        }

        /// <summary>
        /// Disables processing for the Node. The node will not be processed during the engine's update loop.
        /// </summary>
        /// <param name="node">The Node to disable processing for.</param>
        public static void Disable(this Node node)
        {
            node.ProcessMode = Node.ProcessModeEnum.Disabled;
        }

        /// <summary>
        /// Makes the Node process even when the scene is paused. The node will be processed during every frame update, regardless of the pause state.
        /// </summary>
        /// <param name="node">The Node to configure for always processing.</param>
        public static void AlwaysProcess(this Node node)
        {
            node.ProcessMode = Node.ProcessModeEnum.Always;
        }

        /// <summary>
        /// Makes the Node process even when the scene is paused. However, unlike AlwaysProcess, this mode respects the inherited process mode 
        /// when the scene is unpaused.
        /// </summary>
        /// <param name="node">The Node to configure for processing when paused.</param>
        public static void ProcessWhenPaused(this Node node)
        {
            node.ProcessMode = Node.ProcessModeEnum.WhenPaused;
        }

        /// <summary>
        /// Enables all child nodes recursively within the current node.
        /// </summary>
        /// <param name="node">The Node whose children should be enabled.</param>
        public static void EnableChildrens(this Node node)
        {
            foreach (var child in node.GetAllChildren())
                child.Enable();
        }

        /// <summary>
        /// Disables all child nodes recursively within the current node.
        /// </summary>
        /// <param name="node">The Node whose children should be disabled.</param>
        public static void DisableChildrens(this Node node)
        {
            foreach (var child in node.GetAllChildren())
                child.Disable();
        }

        /// <summary>
        /// Remove all the groups this node it's attached
        /// </summary>
        /// <param name="node"></param>
        public static void RemoveFromAllGroups(this Node node)
        {
            foreach (var group in node.GetGroups())
                node.RemoveFromGroup(group);
        }

        /// <summary>
        /// Attempts to mark the input as handled for the viewport associated with the specified node, if the node is valid.
        /// </summary>
        /// <param name="node">The node for which to handle input.</param>
        public static void HandleInput(this Node node)
        {
            if (node.IsValid())
                node.GetViewport()?.SetInputAsHandled();
        }

        /// <summary>
        /// Gets the root node of the tree containing the specified node.
        /// </summary>
        /// <param name="node">The node for which to retrieve the root node.</param>
        /// <returns>The root node of the tree containing the specified node, or null if the node is not part of a tree.</returns>
        public static Node Root(this Node node) => node.GetTree().Root;

        /// <summary>
        /// Checks if the specified node is the root node of the scene tree.
        /// </summary>
        /// <param name="node">The node to check for root status.</param>
        /// <returns>True if the node is the root node, False otherwise.</returns>
        public static bool IsRoot(this Node node) => node.Name.ToString().EqualsIgnoreCase("root");

        /// <summary>
        /// Attempts to add a new autoload node of type T to the specified node (or its root if not the root itself).
        /// </summary>
        /// <typeparam name="T">The type of the autoload node to add.</typeparam>
        /// <param name="from">The node to add the autoload node to.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided autoloadNode is null.</exception>
        public static void AddAutoload<T>(this Node from) where T : Node
        {
            if (from.IsValid() && from.GetAutoloadNode<T>() is null)
            {

                var autoload = Activator.CreateInstance(typeof(T)) as Node;

                ArgumentNullException.ThrowIfNull(autoload);

                autoload.Name = nameof(T);

                from = from.IsRoot() ? from : from.Root();
                from.CallDeferred(Node.MethodName.AddChild, autoload);

                Engine.RegisterSingleton(autoload.Name, autoload);
            }
        }

        /// <summary>
        /// Retrieves an autoloaded node by its name.
        /// </summary>
        /// <typeparam name="T">The type of the autoloaded node to retrieve. Must be a reference type (class).</typeparam>
        /// <param name="name">The name of the autoloaded node in the scene tree. 
        ///  If null (default), the name of type T will be used for retrieval.</param>
        /// <returns>The autoloaded node of type T, or null if no node is found.</returns>
        public static T GetAutoloadNode<T>(this Node node, string? name = null) where T : class
        {
            return node.GetTree().GetAutoloadNode<T>(name is not null ? name : typeof(T).Name);
        }

        /// <summary>
        /// Finds all child nodes of a type assignable to T recursively within the node hierarchy.
        /// </summary>
        /// <typeparam name="T">The base type to search for (can be any class).</typeparam>
        /// <param name="node">The starting node for the search.</param>
        /// <returns>A list containing all child nodes of a type assignable to T found recursively within the hierarchy.</returns>
        public static List<T> GetNodesByClass<T>(this Node node) where T : class
        {
            if (node.GetChildCount().IsZero())
                return [];

            List<T> result = [];

            foreach (Node child in node.GetChildren(true))
            {
                if (child is T nodeFound && typeof(T).IsAssignableFrom(child.GetType()))
                    result.Add(nodeFound);
                else
                    result.AddRange(GetNodesByClass<T>(child));
            }

            return result;
        }

        /// <summary>
        /// Finds all child nodes of a specified type recursively within the node hierarchy.
        /// </summary>
        /// <typeparam name="T">The type of node to search for (must inherit from Node).</typeparam>
        /// <param name="node">The starting node for the search.</param>
        /// <returns>A list containing all child nodes of type T found recursively within the hierarchy.</returns>
        public static List<T> GetNodesByType<T>(this Node node) where T : Node
        {
            if (node.GetChildCount().IsZero())
                return [];

            List<T> result = [];

            foreach (Node child in node.GetChildren(true))
            {
                if (child is T nodeFound && child.GetType() == typeof(T))
                    result.Add(nodeFound);
                else
                    result.AddRange(GetNodesByType<T>(child));
            }

            return result;
        }

        /// <summary>
        /// Finds the first child node of the given node that is of the specified type `T`.
        /// </summary>
        /// <typeparam name="T">The type of child node to search for.</typeparam>
        /// <param name="node">The node to search within.</param>
        /// <returns>The first child node of type `T`, or null if no such child is found.</returns>
        /// <remarks>
        /// This method uses type equality (`==`) to check if the child node's runtime type is exactly equal to `T`.
        /// If you require a more flexible search that includes derived types of `T`, use the `FirstNodeOfClass` method instead.
        /// </remarks>
#nullable enable
        public static T? FirstNodeOfType<T>(this Node node) where T : Node
        {
            if (node.GetChildCount().IsZero())
                return null;

            foreach (Node child in node.GetChildren(true))
            {
                if (child is T nodeFound && child.GetType() == typeof(T))
                    return nodeFound;
            }

            return null;
        }

        /// <summary>
        /// Finds the first child node of the given node that is of the same class or a derived class of the specified type `T`.
        /// </summary>
        /// <typeparam name="T">The base type of the child node to search for.</typeparam>
        /// <param name="node">The node to search within.</param>
        /// <returns>The first child node of type `T` or a derived type, or null if no such child is found.</returns>
        /// <remarks>
        /// This method uses `IsAssignableFrom` to check if the child node's type is assignable from `T`. 
        /// This allows finding both instances of `T` and its derived classes.
        /// </remarks>
        /// 
#nullable enable
        public static T? FirstNodeOfClass<T>(this Node node) where T : Node
        {
            if (node.GetChildCount().IsZero())
                return null;

            foreach (Node child in node.GetChildren(true))
            {
                if (child is T nodeFound && typeof(T).IsAssignableFrom(child.GetType()))
                    return nodeFound;
            }

            return null;
        }

        /// <summary>
        /// Retrieves the last child node from the specified node.
        /// </summary>
        /// <param name="node">The node from which to retrieve the last child.</param>
        /// <returns>The last child node of the target node, or null if the target node has no children.</returns>
        /// 

#nullable enable
        public static Node? GetLastChild(this Node node)
        {
            int count = node.GetChildCount();

            if (count.IsZero())
                return null;

            return node.GetChild(count - 1);
        }

        /// <summary>
        /// Retrieves all ancestor Nodes of a given Node up to the scene root, 
        /// enforcing the `T` type to be compatible with a Godot Variant (likely for editor functionality).
        /// </summary>
        /// <param name="node">The Node from which to retrieve the ancestor chain.</param>
        /// <returns>An Array containing all ancestor Nodes of the specified type `T`.</returns>
        public static Array<T> GetAllAncestors<[MustBeVariant] T>(this Node node) where T : Node
        {
            T parent = node.GetParentOrNull<T>();

            if (parent == null)
                return [];

            Array<T> ancestors = [];

            while (parent is not null)
            {
                ancestors.Add(parent);
                parent = parent.GetParentOrNull<T>();
            }

            return ancestors;
        }

        /// <summary>
        /// Retrieves all ancestor Nodes of a given Node up to the scene root.
        /// </summary>
        /// <param name="node">The Node from which to retrieve the ancestor chain.</param>
        /// <returns>An Array containing all ancestor Nodes.</returns>

        public static Array<Node> GetAllAncestors(this Node node)
        {
            Node parent = node.GetParentOrNull<Node>();

            if (parent == null)
            {
                return [];
            }

            Array<Node> ancestors = [];

            while (parent is not null)
            {
                ancestors.Add(parent);
                parent = parent.GetParentOrNull<Node>();
            }

            return ancestors;
        }


        /// <summary>
        /// Retrieves all child Nodes (including nested children) of a given Node, 
        /// enforcing the `T` type to be compatible with a Godot Variant (likely for editor functionality).
        /// </summary>
        /// <param name="node">The Node from which to retrieve all child Nodes.</param>
        /// <returns>An Array containing all child Nodes of the specified type `T`.</returns>

        public static Array<T> GetAllChildren<[MustBeVariant] T>(this Node node) where T : Node
        {
            Array<T> childrens = [];

            foreach (T child in node.GetChildren(true).OfType<T>())
            {
                childrens.Add(child);

                if (child.GetChildCount() > 0)
                {
                    childrens.AddRange(GetAllChildren<T>(child));
                }
            }

            return childrens;
        }

        /// <summary>
        /// Retrieves all child Nodes (including nested children) of a given Node.
        /// </summary>
        /// <param name="node">The Node from which to retrieve all child Nodes.</param>
        /// <returns>An Array containing all child Nodes.</returns>
        public static Array<Node> GetAllChildren(this Node node)
        {
            Array<Node> childrens = [];

            foreach (Node child in node.GetChildren(true))
            {
                childrens.Add(child);

                if (child.GetChildCount() > 0)
                {
                    childrens.AddRange(GetAllChildren(child));
                }
            }

            return childrens;
        }

        /// <summary>
        /// Safely removes a Node from the scene hierarchy, considering its current state.
        /// </summary>
        /// <param name="node">The Node to be removed.</param>
        public static void Remove(this Node node)
        {
            if (node.IsValid())
            {
                if (node.IsInsideTree())
                    node.QueueFree();
                else
                    node.CallDeferred(GodotObject.MethodName.Free);
            }
        }

        /// <summary>
        /// Retrieves the first node of the specified type from a given group in the scene tree.
        /// </summary>
        /// <typeparam name="T">The type of node to search for.</typeparam>
        /// <param name="group">The name of the group to search within.</param>
        /// <returns>
        /// The first node of type T found in the specified group, or null if no node of type T is found in the group.
        /// </returns>
        /// <remarks>
        /// This function uses the Godot `GetFirstNodeInGroup` method and attempts to cast the returned value to the specified type T.
        /// If the cast fails, the function returns null.
        /// </remarks>

#nullable enable
        public static T? GetFirstNodeInGroup<T>(this Node node, string group) where T : Node
            => node.GetTree().GetFirstNodeInGroup(group) as T;


        /// <summary>
        /// Retrieves all nodes of the specified type from a given group in the scene tree.
        /// </summary>
        /// <typeparam name="T">The type of node to search for.</typeparam>
        /// <param name="group">The name of the group to search within.</param>
        /// <returns>
        /// An `IEnumerable&lt;T&gt;` containing all nodes of type T found in the specified group.
        /// If no nodes of type T are found in the group, the returned collection will be empty.
        /// </returns>
        /// <remarks>
        /// This function uses the Godot `GetNodesInGroup` method and then casts each element in the returned array to the specified type T.
        /// Any element that cannot be cast is omitted from the resulting collection.
        /// </remarks>
        public static Array<T> GetNodesInGroup<T>(this Node node, string group) where T : Node =>
            (Array<T>)node.GetTree().GetNodesInGroup(group.StripEdges()).Cast<T>();

        /// <summary>
        /// Removes all child nodes from the specified target node and queues them for freeing in Godot.
        /// </summary>
        /// <param name="node">The target node from which to remove and free children.</param>
        /// <remarks>
        /// This function iterates through the children of the target node in reverse order, removing each child and queuing it for freeing.
        /// Using reverse iteration ensures that removing a child doesn't affect the index of remaining children.
        /// </remarks>
        public static void RemoveAndQueueFreeChildren(this Node node)
        {
            for (int i = node.GetChildCount() - 1; i >= 0; i--)
            {
                Node child = node.GetChild(i);

                if (child.IsValid())
                {
                    node.RemoveChild(child);
                    child.QueueFree();
                }

            }
        }

        /// <summary>
        /// Queues all child nodes of the specified target node for freeing in Godot.
        /// </summary>
        /// <param name="node">The target node whose children will be queued for freeing.</param>
        /// <remarks>
        /// This function iterates through the children of the target node and queues each child for freeing.
        /// No type check is necessary as `GetChildren()` returns a collection of nodes.
        /// </remarks>
        public static void QueueFreeChildren(this Node node)
        {
            foreach (Node child in node.GetChildren())
            {
                if (child.IsValid())
                    child.QueueFree();
            }
        }

        /// <summary>
        /// Set the owner node to edited scene root if Engine is editor hint.
        /// </summary>
        /// <param name="node">The target node</param>
        public static void SetOwnerToEditedSceneRoot(this Node node)
        {
            if (Engine.IsEditorHint())
                node.Owner = node.GetTree().EditedSceneRoot;
        }

        /// <summary>
        /// Calculates the depth of a Node within the scene hierarchy.
        /// 
        /// This function traverses the parent chain of the Node until it reaches the root node (which has no parent) and counts the number of levels in the hierarchy.
        /// 
        /// Note that this function only considers the scene hierarchy and doesn't account for dynamically attached or detached Nodes.
        /// </summary>
        /// <param name="node">The Node for which to calculate the depth.</param>
        /// <returns>The depth of the Node within the scene hierarchy (0 for the root node, increasing values for child nodes).</returns>
        public static int GetTreeDepth(this Node node)
        {
            int depth = 0;

            while (node.GetParent() is not null)
            {
                depth++;
                node = node.GetParent();
            }

            return depth;
        }

        /// <summary>
        /// Checks if a Node instance is considered valid and usable within the scene.
        /// </summary>
        /// <param name="node">The Node object to check for validity.</param>
        /// <returns>True if the node is not null, is a valid Godot object instance, and not queued for deletion, False otherwise.</returns>
        public static bool IsValid(this Node node)
            => node is not null && GodotObject.IsInstanceValid(node) && !node.IsQueuedForDeletion();


        /// <summary>
        /// Emits a signal on a Node safely, checking for node validity and signal existence before emitting.
        /// </summary>
        /// <param name="node">The Node on which to emit the signal.</param>
        /// <param name="signalName">The name of the signal to emit.</param>
        public static void EmitSignalSafe(this Node node, string signalName, params Variant[] args)
        {
            if (node.IsValid() && node.HasSignal(signalName))
                node.EmitSignal(signalName, args ?? []);
        }


    }
}
