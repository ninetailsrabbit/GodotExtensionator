using Godot;

namespace Godot_XTension_Pack {
    public static class SceneTreeExtension {

        /// <summary>
        /// Retrieves an autoloaded node by its name.
        /// </summary>
        /// <typeparam name="T">The type of the autoloaded node to retrieve. Must be a reference type (class).</typeparam>
        /// <param name="name">The name of the autoloaded node in the scene tree.</param>
        /// <returns>The autoloaded node of type T, or null if no node is found.</returns>
        /// <example>
        /// This example retrieves an autoloaded AudioManager node and casts it to the appropriate type:
        /// <code>csharp
        /// AudioManager audioManager = GetAutoloadNode&lt;AudioManager&gt;("AudioManager");
        /// </code>
        /// </example>
        public static T GetAutoloadNode<T>(this SceneTree tree, string? name = null) where T : class {
            return tree.Root.GetNode<T>(name is not null ? name : typeof(T).Name);
        }

        public static T GetAutoloadSingleton<T>(string? name = null) where T : class {
            return ((SceneTree)Engine.GetMainLoop()).GetAutoloadNode<T>(name);
        }

        /// <summary>
        /// Removes all nodes from a specified group within the SceneTree.
        /// </summary>
        /// <param name="tree">The SceneTree containing the group.</param>
        /// <param name="group">The name of the group to remove nodes from.</param>
        /// <remarks>
        /// This function iterates through all nodes belonging to the specified group and removes them from the SceneTree.
        /// </remarks>
        public static void RemoveNodesFromGroup(this SceneTree tree, string group) {
            foreach (Node node in tree.GetNodesInGroup(group))
                node.Remove();
        }

        /// <summary>
        /// Waits asynchronously until the next idle frame in the scene tree.
        /// </summary>
        /// <param name="sceneTree">The SceneTree instance to wait for the idle frame on.</param>
        /// <returns>An awaitable task that completes when the next idle frame is reached.</returns>
        public static async Task NextIdle(this SceneTree sceneTree) => await sceneTree.ToSignal(sceneTree, SceneTree.SignalName.ProcessFrame);

        /// <summary>
        /// Waits asynchronously until the next physic idle frame in the scene tree.
        /// </summary>
        /// <param name="sceneTree">The SceneTree instance to wait for the idle frame on.</param>
        /// <returns>An awaitable task that completes when the next physic idle frame is reached.</returns>
        public static async Task NextPhysicsIdle(this SceneTree sceneTree) => await sceneTree.ToSignal(sceneTree, SceneTree.SignalName.PhysicsFrame);


        /// <summary>
        /// Pauses the scene by setting the time scale for a specified duration, optionally scaling audio playback as well.
        /// </summary>
        /// <param name="tree">The SceneTree to operate on.</param>
        /// <param name="timeScale">The time scale to apply during the freeze (0.0 effectively pauses the scene).</param>
        /// <param name="duration">The duration (in seconds) to keep the scene paused.</param>
        /// <param name="scaleAudio">True to also scale audio playback speed along with the time scale (default: true).</param>
        /// <returns>An awaitable task that completes when the freeze duration is over.</returns>
        /// <remarks>
        /// This function provides a way to temporarily pause the scene's update logic and optionally audio playback for a specific duration. It achieves this by setting the `Engine.TimeScale` and optionally the `AudioServer.PlaybackSpeedScale` properties. After the specified `duration`, the original time scale and audio playback speed are restored.
        /// </remarks>
        public static async void FrameFreeze(this SceneTree tree, float timeScale, float duration, bool scaleAudio = true) {
            double originalTimeScale = Engine.TimeScale;
            float originalPlaybackSpeed = AudioServer.PlaybackSpeedScale;

            Engine.TimeScale = timeScale;

            if (scaleAudio)
                AudioServer.PlaybackSpeedScale = timeScale;

            await tree.ToSignal(tree.CreateTimer(duration, true, false, true), Godot.Timer.SignalName.Timeout);

            Engine.TimeScale = originalTimeScale;
            AudioServer.PlaybackSpeedScale = originalPlaybackSpeed;
        }

        /// <summary>
        /// Gets the final transformation applied to the root node of the SceneTree, Useful to fix mouse handling when viewport it's stretched.
        /// </summary>
        /// <param name="tree">The SceneTree to get the final transform of.</param>
        /// <returns>A Transform2D representing the final transformation applied to the root node.</returns>
        /// <remarks>
        /// This extension method retrieves the final transformation that is applied to the root node of the SceneTree. The final transformation incorporates the node's own transform, its parent's transform, and so on, all the way up to the root. This can be useful for calculating the absolute position and orientation of elements within the scene.
        /// </remarks>
        public static Transform2D FinalTransform(this SceneTree tree) => tree.Root.GetFinalTransform();

        /// <summary>
        /// Quits the game application with a specified exit code (platform dependent behavior).
        /// </summary>
        /// <param name="tree">The SceneTree of the running game.</param>
        /// <param name="exitCode">The exit code to return to the operating system (interpretation varies by platform).</param>
        /// <remarks>
        /// This extension method provides a platform-aware way to quit the game application. It checks the current operating system and only calls the `tree.Quit(exitCode)` method if it's not running on iOS. This is because iOS application termination is typically handled differently.
        /// Instead, as recommended by the iOS Human Interface Guidelines, the user is expected to close apps via the Home button
        /// </remarks>
        public static void QuitGame(this SceneTree tree, int exitCode) {
            if (OS.GetName() != "iOS")
                tree.Quit(exitCode);
        }


        /// <summary>
        /// Checks if the SceneTree is currently paused.
        /// </summary>
        /// <param name="tree">The SceneTree to check the pause state of.</param>
        /// <returns>True if the SceneTree is paused, false otherwise.</returns>
        public static bool IsPaused(this SceneTree tree) => tree.Paused;

        /// <summary>
        /// Pauses the game by setting the SceneTree's paused state and emitting a signal for game pause events.
        /// </summary>
        /// <param name="tree">The SceneTree to pause.</param>
        /// <remarks>
        /// This function pauses the game by setting the `Paused` property of the SceneTree to true. Additionally, it retrieves the autoloaded `GlobalGameEvents` node and emits the `GlobalGameEvents.SignalName.GamePaused` signal to notify other parts of the game about the pause state change.
        /// </remarks>
        public static void PauseGame(this SceneTree tree) {
            if (!tree.Paused) {
                tree.Paused = true;
            }
        }

        /// <summary>
        /// Resumes the game by setting the SceneTree's paused state to false and emitting a signal for game resume events.
        /// </summary>
        /// <param name="tree">The SceneTree to resume.</param>
        /// <remarks>
        /// This function resumes the game by setting the `Paused` property of the SceneTree to false. Similar to pausing, it retrieves the autoloaded `GlobalGameEvents` node and emits the `GlobalGameEvents.SignalName.GameResumed` signal to notify other parts of the game about the pause state change.
        /// </remarks>
        public static void ResumeGame(this SceneTree tree) {
            if (tree.Paused) {
                tree.Paused = false;
            }
        }

    }

}