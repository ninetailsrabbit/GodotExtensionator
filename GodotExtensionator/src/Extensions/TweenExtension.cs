using Godot;

namespace GodotExtensionator {
    public static class TweenExtension {

        /// <summary>
        /// Applies a flip animation to a Node2D by scaling its x-axis from 0 to 1 and back.
        /// </summary>
        /// <param name="tween">The Tween instance to use for the animation.</param>
        /// <param name="node">The Node2D to apply the flip animation to.</param>
        /// <param name="time">The duration of the flip animation in seconds (default: 0.25f).</param>
        public static void FlipHorizontal(this Tween tween, Node2D node, float time = 0.25f) {
            tween.TweenProperty(node, "scale:x", 0, time).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Sine);
            tween.TweenProperty(node, "scale:x", 1f, time).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Sine);
        }

        /// <summary>
        /// Applies a flip animation to a Node2D by scaling its Y-axis from 0 to 1 and back.
        /// </summary>
        /// <param name="tween">The Tween instance to use for the animation.</param>
        /// <param name="node">The Node2D to apply the flip animation to.</param>
        /// <param name="time">The duration of the flip animation in seconds (default: 0.25f).</param>
        public static void FlipVertical(this Tween tween, Node2D node, float time = 0.25f) {
            tween.TweenProperty(node, "scale:y", 0, time).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Sine);
            tween.TweenProperty(node, "scale:y", 1f, time).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Sine);
        }

        /// <summary>
        /// Applies a bounce animation to a Node2D in a specified direction.
        /// </summary>
        /// <param name="tween">The Tween instance to use for the animation.</param>
        /// <param name="node">The Node2D to apply the bounce animation to.</param>
        /// <param name="direction">The direction of the bounce.</param>
        /// <param name="bounceHeight">The height of the bounce (default: 25f).</param>
        /// <param name="upSpeed">The speed of the upward movement (default: 0.25f).</param>
        /// <param name="downSpeed">The speed of the downward movement (default: 0.35f).</param>
        public static void Bounce(this Tween tween, Node2D node, Vector2 direction, float bounceHeight = 25f, float upSpeed = 0.25f, float downSpeed = 0.35f) {
            var originalPosition = node.Position;

            tween.TweenProperty(node, "position", node.Position + direction.Normalized() * bounceHeight, upSpeed);
            tween.TweenProperty(node, "position", originalPosition, downSpeed).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Bounce);
        }
    }
}
