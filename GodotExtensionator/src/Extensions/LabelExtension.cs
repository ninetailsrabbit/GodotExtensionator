using Godot;

namespace GodotExtensionator {
    public static class LabelExtension {

        /// <summary>
        /// Adjusts the text size and wrapping mode of a Label to fit within a maximum width.
        /// </summary>
        /// <param name="label">The Label to adjust.</param>
        /// <param name="maxSize">The maximum width of the Label (default: 200).</param>
        public static void AdjustText(this Label label, int maxSize = 200) {
            label.CustomMinimumSize = label.CustomMinimumSize with { X = Mathf.Min(label.Size.X, maxSize) };

            if (label.Size.X > maxSize) {
                label.AutowrapMode = TextServer.AutowrapMode.Word;
                label.CustomMinimumSize = label.CustomMinimumSize with { Y = label.Size.Y };
            }
        }
    }
}
