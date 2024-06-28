using Godot;

namespace GodotExtensionator {
    public static class AnimationPlayerExtension {

        const string RESET_ANIMATION = "RESET";

        /// <summary>
        /// Returns a `SignalAwaiter` that awaits the `AnimationStarted` signal from the specified `AnimationPlayer`.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to monitor for the start signal.</param>
        public static SignalAwaiter WaitToStart(this AnimationPlayer animationPlayer)
          => animationPlayer.ToSignal(animationPlayer, AnimationMixer.SignalName.AnimationStarted);

        /// <summary>
        /// Returns a `SignalAwaiter` that awaits the `AnimationFinished` signal from the specified `AnimationPlayer`.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to monitor for the finished signal.</param>
        /// <returns>A `SignalAwaiter` that allows waiting for the animation to finish playing.</returns>
        public static SignalAwaiter WaitToFinished(this AnimationPlayer animationPlayer)
            => animationPlayer.ToSignal(animationPlayer, AnimationMixer.SignalName.AnimationFinished);


        /// <summary>
        /// Queues an animation for playback in the specified `AnimationPlayer`.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to queue the animation on.</param>
        /// <param name="name">The name (string) of the animation to queue.</param>
        /// <param name="finishedCallback">An optional `Action` delegate to be called when the animation finishes playback.</param>
        /// <param name="delay">An optional delay (in seconds) before starting playback of the queued animation.</param>
        public static void Queue(this AnimationPlayer animationPlayer, string name, Action finishedCallback, float? delay = null) {
            animationPlayer.Queue(name, finishedCallback, delay);
        }

        /// <summary>
        /// Queues an animation for playback in the specified `AnimationPlayer`.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to queue the animation on.</param>
        /// <param name="name">The name (StringName) of the animation to queue.</param>
        /// <param name="finishedCallback">An optional `Action` delegate to be called when the animation finishes playback.</param>
        /// <param name="delay">An optional delay (in seconds) before starting playback of the queued animation.</param>
        public static void Queue(this AnimationPlayer animationPlayer, StringName name, Action finishedCallback, float? delay = null) {
            var animation = animationPlayer.GetAnimation(name);
            delay ??= animation.Length;

            ArgumentNullException.ThrowIfNull(animation);

            var timer = animationPlayer.GetTree().CreateTimer((double)delay);
            timer.Connect(SceneTreeTimer.SignalName.Timeout, Callable.From(finishedCallback));

            animationPlayer.Queue(name);
        }

        /// <summary>
        /// Queues an animation for playback in the specified `AnimationPlayer`, resetting the playback state before queuing.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to queue the animation on.</param>
        /// <param name="name">The name (string) of the animation to queue.</param>
        /// <param name="finishedCallback">An optional `Action` delegate to be called when the animation finishes playback.</param>
        /// <param name="delay">An optional delay (in seconds) before starting playback of the queued animation.</param>
        public static void QueueAndReset(this AnimationPlayer animationPlayer, string name, Action finishedCallback, float? delay = null) {
            animationPlayer.QueueAndReset(new StringName(name), finishedCallback, delay);
        }

        /// <summary>
        /// Queues an animation for playback in the specified `AnimationPlayer`, resetting the playback state before queuing.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to queue the animation on.</param>
        /// <param name="name">The name (StringName) of the animation to queue.</param>
        /// <param name="finishedCallback">An optional `Action` delegate to be called when the animation finishes playback.</param>
        /// <param name="delay">An optional delay (in seconds) before starting playback of the queued animation.</param>
        public static void QueueAndReset(this AnimationPlayer animationPlayer, StringName name, Action finishedCallback, float? delay = null) {
            animationPlayer.Queue(name, finishedCallback);
            animationPlayer.Reset();
        }


        /// <summary>
        /// Resets the playback state of the specified `AnimationPlayer`.
        /// </summary>
        /// <param name="animationPlayer">The `AnimationPlayer` instance to reset.</param>
        public static void Reset(this AnimationPlayer animationPlayer) {
            if (animationPlayer.HasAnimation(RESET_ANIMATION))
                animationPlayer.Queue(RESET_ANIMATION);
        }

    }
}
