using Godot;

namespace GodotExtensionator {
    public static class ParallaxExtension {

        /// <summary>
        /// Adapts a ParallaxBackground to fit the horizontal size of the viewport and sets parallax motion scales for each layer.
        /// </summary>
        /// <param name="parallaxBackground">The ParallaxBackground to adapt.</param>
        /// <param name="motionScales">An array of Vector2 values representing the motion scales for each parallax layer.</param>
        /// <remarks>
        /// This extension function iterates through the layers of the ParallaxBackground, adjusts their Sprite2D scales to match the viewport's width, 
        /// sets the MotionMirroring and MotionScale properties for each layer based on the texture size and adjusted scale.
        /// </remarks>
        public static void AdaptParallaxToHorizontalViewport(this ParallaxBackground parallaxBackground, Vector2[] motionScales) {
            Rect2 viewport = parallaxBackground.GetWindow().GetVisibleRect();

            for (int i = 0; i < motionScales.Length; i++) {
                if (parallaxBackground.GetChildOrNull<ParallaxLayer>(i) is ParallaxLayer parallaxLayer) {
                    Sprite2D backgroundSprite = parallaxLayer.GetNode<Sprite2D>(nameof(Sprite2D));

                    var textureSize = backgroundSprite.Texture.GetSize();
                    backgroundSprite.Scale = Vector2.One * (viewport.Size.Y / textureSize.Y);

                    parallaxLayer.MotionMirroring = new Vector2(textureSize.X * backgroundSprite.Scale.X, 0);
                    parallaxLayer.MotionScale = motionScales[i];
                }
            }
        }

        /// <summary>
        /// Adapts a ParallaxBackground to fit the vertical size of the viewport and sets parallax motion scales for each layer.
        /// </summary>
        /// <param name="parallaxBackground">The ParallaxBackground to adapt.</param>
        /// <param name="motionScales">An array of Vector2 values representing the motion scales for each parallax layer.</param>
        /// <remarks>
        /// This extension function iterates through the layers of the ParallaxBackground, adjusts their Sprite2D scales to match the viewport's height, 
        /// sets the MotionMirroring and MotionScale properties for each layer based on the texture size and adjusted scale.
        /// </remarks>
        public static void AdaptParallaxToVerticalViewport(this ParallaxBackground parallaxBackground, Vector2[] motionScales) {
            Rect2 viewport = parallaxBackground.GetWindow().GetVisibleRect();

            for (int i = 0; i < motionScales.Length; i++) {
                if (parallaxBackground.GetChildOrNull<ParallaxLayer>(i) is ParallaxLayer parallaxLayer) {
                    Sprite2D backgroundSprite = parallaxLayer.GetNode<Sprite2D>(nameof(Sprite2D));

                    var textureSize = backgroundSprite.Texture.GetSize();
                    backgroundSprite.Scale = Vector2.One * (viewport.Size.X / textureSize.X);

                    parallaxLayer.MotionMirroring = new Vector2(0, textureSize.Y * backgroundSprite.Scale.Y);
                    parallaxLayer.MotionScale = motionScales[i];
                }
            }
        }

    }
}
