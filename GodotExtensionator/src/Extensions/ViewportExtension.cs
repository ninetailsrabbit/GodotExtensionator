using Godot;

namespace GodotExtensionator {
    public static class ViewportExtension {

        /// <summary>
        /// Gets the root node of the tree containing the specified node.
        /// </summary>
        /// <param name="viewport">The viewport for which to retrieve the root node.</param>
        /// <returns>The root node of the tree containing the specified viewport, or null if the viewport is not part of a tree.</returns>
        public static Node Root(this Viewport viewport) => viewport.GetTree().Root;

        /// <summary>
        /// (Commented out) Gets the camera frame in 2D coordinates for the specified viewport.
        /// </summary>
        /// <param name="viewport">The viewport for which to retrieve the camera frame.</param>
        /// <returns>The camera frame as a Rect2 (commented out).</returns>
        public static Rect2 GetCamera2DFrame(this Viewport viewport) {
            Rect2 visibleRect = viewport.GetVisibleRect();
            Camera2D camera = viewport.GetCamera2D();

            if (camera is not null)
                visibleRect.Position += camera.GetScreenCenterPosition() - visibleRect.Size / 2.0f;


            return visibleRect;
        }

        /// <summary>
        /// Converts a screen position (in normalized coordinates) to its corresponding world space position within the viewport.
        /// </summary>
        /// <param name="viewport">The viewport to perform the conversion for.</param>
        /// <param name="screenPosition">The screen position in normalized coordinates (0,0) to (1,1).</param>
        /// <returns>The corresponding world space position for the given screen position within the viewport.</returns>
        public static Vector2 ScreenToWorld(this Viewport viewport, Vector2 screenPosition) => screenPosition * viewport.CanvasTransform;

        /// <summary>
        /// Converts a world space position to its corresponding screen position (in normalized coordinates) within the viewport.
        /// </summary>
        /// <param name="viewport">The viewport to perform the conversion for.</param>
        /// <param name="worldPosition">The world space position to convert.</param>
        /// <returns>The corresponding screen position in normalized coordinates (0,0) to (1,1) within the viewport.</returns>
        public static Vector2 WorldToScreen(this Viewport viewport, Vector2 worldPosition) => viewport.CanvasTransform * worldPosition;

        /// <summary>
        /// Captures a screenshot of the content currently displayed in the Viewport.
        /// </summary>
        /// <param name="viewport">The Viewport to capture a screenshot of.</param>
        /// <returns>An Image object representing the captured screenshot data, or null if capturing failed.</returns>
        /// <remarks>
        /// This extension method allows you to capture the current visual state of a Viewport as an Image object. The captured image can then be used for further processing or saving.
        /// </remarks>
        public static Image Screenshot(this Viewport viewport) => viewport.GetTexture().GetImage();

        /// <summary>
        /// Captures a screenshot and updates a TextureRect to use the captured image as its texture.
        /// When no texture rect is passed, a new one is created.
        /// </summary>
        /// <param name="viewport">The Viewport to capture a screenshot of.</param>
        /// <param name="textureRect">The TextureRect to update with the captured screenshot.</param>
        /// <returns>The same TextureRect object passed as a parameter, potentially with its texture updated.</returns>
        /// <remarks>
        /// This function combines capturing a screenshot of the Viewport and updating the provided TextureRect to use the captured image as its texture. It's useful if you want to dynamically update a UI element with the current viewport content.
        /// </remarks>
        public static TextureRect ScreenshotToTextureRect(this Viewport viewport, TextureRect? textureRect = null) {
            textureRect ??= new();

            Image screenshot = viewport.Screenshot();

            if (screenshot is not null)
                textureRect.Texture = ImageTexture.CreateFromImage(screenshot);

            return textureRect;
        }

        /// <summary>
        /// Gets the current screen position of the mouse relative to the top-left corner of the Viewport.
        /// </summary>
        /// <param name="viewport">The Viewport to get the mouse position for.</param>
        /// <returns>A Vector2 representing the current screen position of the mouse within the Viewport.</returns>
        /// <remarks>
        /// This extension method retrieves the current location of the mouse cursor relative to the top-left corner of the Viewport in screen coordinates. This can be useful for handling mouse interactions within the Viewport's visible area.
        /// </remarks>
        public static Vector2 MouseScreenPosition(this Viewport viewport) => viewport.GetMousePosition();

        /// <summary>
        /// Gets the current mouse position relative to the Viewport size, normalized between 0.0 and 1.0.
        /// </summary>
        /// <param name="viewport">The Viewport to get the mouse position for.</param>
        /// <returns>A Vector2 representing the current mouse position within the Viewport normalized between 0.0 (top-left corner) and 1.0 (bottom-right corner).</returns>
        /// <remarks>
        /// This function builds upon `MouseScreenPosition` by normalizing the mouse position values between 0.0 and 1.0 based on the Viewport's size. This provides a position relative to the Viewport itself, independent of screen resolution.
        /// </remarks>
        public static Vector2 MouseScreenRelativePosition(this Viewport viewport) => viewport.MouseScreenPosition() / (Vector2)viewport.GetTree().Root.Size;

        /// <summary>
        /// Gets the vector from the Viewport's center to the current mouse screen position.
        /// </summary>
        /// <param name="viewport">The Viewport to get the mouse position for.</param>
        /// <returns>A Vector2 representing the difference between the Viewport's center and the current mouse screen position.</returns>
        /// <remarks>
        /// This function calculates the vector pointing from the center of the Viewport to the current mouse location on the screen. This can be useful for implementing drag-and-drop functionality or other interactions relative to the Viewport's center.
        /// </remarks>
        public static Vector2 MousePositionFromCenter(this Viewport viewport) => (Vector2)viewport.GetTree().Root.Size / 2f - viewport.MouseScreenPosition();

        /// <summary>
        /// Gets the mouse position relative to the Viewport's center, normalized between -1.0 and 1.0 on both axes.
        /// </summary>
        /// <param name="viewport">The Viewport to get the mouse position for.</param>
        /// <returns>A Vector2 representing the mouse position relative to the Viewport's center, normalized between -1.0 (top-left corner) and 1.0 (bottom-right corner).</returns>
        /// <remarks>
        /// This function combines the functionalities of `MouseScreenPosition` and `GetTree().Root.Size` to calculate the mouse position relative to the Viewport's center. The result is a normalized vector between -1.0 and 1.0 on both axes, providing a convenient representation for various input handling scenarios.
        /// </remarks>
        public static Vector2 MouseRelativeFromCenter(this Viewport viewport) {
            Vector2 windowSize = viewport.GetTree().Root.Size;
            Vector2 windowCenter = windowSize / 2f;

            return (windowCenter - viewport.MouseScreenPosition()) / windowSize;
        }

    }
}
