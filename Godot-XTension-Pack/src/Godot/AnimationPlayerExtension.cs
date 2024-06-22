using Godot;

namespace Godot_XTension_Pack {
    public static class AnimationPlayerExtension {

        const string RESET_ANIMATION = "RESET";

        public static void Queue(this AnimationPlayer animationPlayer, string name, Action finishedCallback, float? delay = null) {
            animationPlayer.Queue(name, finishedCallback, delay);
        }
        public static void Queue(this AnimationPlayer animationPlayer, StringName name, Action finishedCallback, float? delay = null) {
            var animation = animationPlayer.GetAnimation(name);
            delay ??= animation.Length;

            ArgumentNullException.ThrowIfNull(animation);

            var timer = animationPlayer.GetTree().CreateTimer((double)delay);
            timer.Connect(SceneTreeTimer.SignalName.Timeout, Callable.From(finishedCallback));

            animationPlayer.Queue(name);
        }

        public static void QueueAndReset(this AnimationPlayer animationPlayer, string name, Action finishedCallback, float? delay = null) {
            animationPlayer.QueueAndReset(new StringName(name), finishedCallback, delay);
        }

        public static void QueueAndReset(this AnimationPlayer animationPlayer, StringName name, Action finishedCallback, float? delay = null) {
            animationPlayer.Queue(name, finishedCallback);
            animationPlayer.Reset();
        }


        public static void Reset(this AnimationPlayer animationPlayer) {
            if (animationPlayer.HasAnimation(RESET_ANIMATION))
                animationPlayer.Queue(RESET_ANIMATION);
        }

    }
}
