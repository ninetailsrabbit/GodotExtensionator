using Extensionator;
using Godot;

namespace GodotExtensionator {

    public static class TextureExtension {

        /// <summary>
        /// Gets the actual dimensions (used rectangle) of the texture data.
        /// </summary>
        /// <param name="texture">The Texture2D to get the dimensions of.</param>
        /// <returns>A Rect2I representing the used rectangle within the texture data.</returns>
        /// <remarks>
        /// This extension method retrieves the actual used rectangle within the texture data, which might be smaller than the entire texture size. This is useful for getting the true dimensions of the content within the texture.
        /// </remarks>
        public static Rect2I Dimensions(this Texture2D texture) => texture.GetImage().GetUsedRect();

        /// <summary>
        /// Gets the scaled dimensions of a TextureRect considering the texture's used rectangle and the scale applied.
        /// </summary>
        /// <param name="textureRect">The TextureRect to get the dimensions of.</param>
        /// <returns>A Vector2 representing the scaled dimensions of the TextureRect.</returns>
        /// <remarks>
        /// This extension method retrieves the dimensions of the TextureRect by considering the used rectangle of its underlying Texture2D and the scale applied to the TextureRect itself. It provides the actual size the TextureRect will be rendered at.
        /// </remarks>
        public static Vector2 Dimensions(this TextureRect textureRect) {
            Texture2D texture = textureRect.Texture;

            if (texture is not null) {
                Rect2I usedRect = texture.Dimensions();

                return new Vector2(usedRect.Size.X, usedRect.Size.Y) * textureRect.Scale;
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Gets the scaled dimensions of a Sprite2D considering the texture's used rectangle and the scale applied.
        /// </summary>
        /// <param name="sprite">The Sprite2D to get the dimensions of.</param>
        /// <returns>A Vector2 representing the scaled dimensions of the Sprite2D.</returns>
        /// <remarks>
        /// This extension method behaves similarly to the `Dimensions(this TextureRect textureRect)` function. It retrieves the dimensions of the Sprite2D by considering the used rectangle of its underlying Texture2D and the scale applied to the Sprite2D itself. It provides the actual size the Sprite2D will be rendered at.
        /// </remarks>
        public static Vector2 Dimensions(this Sprite2D sprite) {
            Texture2D texture = sprite.Texture;

            if (texture is not null) {
                Rect2I usedRect = texture.Dimensions();

                return new Vector2(usedRect.Size.X, usedRect.Size.Y) * sprite.Scale;
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Calculates the rectangle representing the non-transparent area of a Texture2D assuming it's a PNG image.
        /// </summary>
        /// <param name="texture">The Texture2D to extract the PNG rectangle from.</param>
        /// <returns>A Rect2I representing the rectangle containing non-transparent pixels, or a zero-sized Rect2I if the texture is null or not a PNG.</returns>
        /// <remarks>
        /// This function analyzes a Texture2D to determine the rectangular area occupied by non-transparent pixels, assuming the texture data is loaded from a PNG image. It iterates through each pixel of the image and checks the alpha channel value. If the alpha is greater than zero (not transparent), it updates the boundaries of the rectangle that contains all non-transparent pixels. This approach works well for PNG images where transparency is typically encoded in the alpha channel. If the provided texture is null or not a PNG image, the function returns a zero-sized Rect2I.
        /// </remarks>
        public static Rect2I GetPngRect(this Texture2D texture) {

            Image image = texture.GetImage();

            if (image is not null && image.ResourcePath.GetExtension().EqualsIgnoreCase("png")) {
                int topPosition = image.GetHeight();
                int bottomPosition = 0;
                int rightPosition = image.GetWidth();
                int leftPosition = 0;

                foreach (int X in GD.Range(image.GetWidth())) {
                    foreach (int Y in GD.Range(image.GetHeight())) {
                        Color pixelColor = image.GetPixel(X, Y);

                        if (pixelColor.A.IsGreaterThanZero()) {
                            if (topPosition > Y)
                                topPosition = Y;

                            if (bottomPosition < Y)
                                bottomPosition = Y;

                            if (rightPosition > X)
                                rightPosition = X;

                            if (leftPosition < X)
                                leftPosition = X;
                        }
                    }
                }

                Vector2I position = new(leftPosition - rightPosition, bottomPosition - topPosition);
                Vector2I size = new(rightPosition + Mathf.RoundToInt(position.X / 2f), topPosition + Mathf.RoundToInt(position.Y / 2f));

                return new Rect2I(position, size);
            }

            return new Rect2I(0, 0, 0, 0);
        }

        /// <summary>
        /// Retrieves the color of a pixel from an image based on its UV coordinates.
        /// </summary>
        /// <param name="image">The image to sample from.</param>
        /// <param name="uv">The UV coordinates within the image (0,0 to 1,1).</param>
        /// <returns>The color of the pixel at the specified UV coordinates.</returns>
        public static Color GetPixelFromUV(this Image image, Vector2 uv) {
            Vector2 position = image.GetSize() * uv;

            return image.GetPixelv(new Vector2I(Mathf.RoundToInt(position.X), Mathf.RoundToInt(position.Y)));
        }

        /// <summary>
        /// Sets the color of a pixel in an image based on its UV coordinates.
        /// </summary>
        /// <param name="image">The image to modify.</param>
        /// <param name="uv">The UV coordinates within the image (0,0 to 1,1) where the pixel will be set.</param>
        /// <param name="color">The color to assign to the pixel.</param>
        public static void SetPixelFromUv(this Image image, Vector2 uv, Color color) {
            Vector2 position = image.GetSize() * uv;

            image.SetPixelv(new Vector2I(Mathf.RoundToInt(position.X), Mathf.RoundToInt(position.Y)), color);
        }

    }

}