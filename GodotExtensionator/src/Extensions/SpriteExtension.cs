using System.Drawing;
using Godot;

namespace GodotExtensionator
{
    public static class SpriteExtension
    {

        /// <summary>
        /// Snaps the Sprite2D to the nearest grid point based on its texture size.
        /// 
        /// This method calculates the grid size based on the larger dimension of the texture and then calls the `MouseGridSnap` method to perform the actual snapping.
        /// </summary>
        /// <param name="sprite">The Sprite2D to snap.</param>
        /// <param name="useLocalPosition">Whether to use local or global mouse position.</param>
        /// <returns>The calculated grid position.</returns>
        public static Vector2 MouseGridSnapByTextureSize(this Sprite2D sprite, bool useLocalPosition = false)
        {
            var textureSize = sprite.Texture.GetSize();

            return sprite.MouseGridSnap((int)Mathf.Floor(Mathf.Max(textureSize.X, textureSize.Y)), useLocalPosition);
        }

    }

}