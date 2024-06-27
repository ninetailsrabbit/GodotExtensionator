using Godot;

namespace Godot_XTension_Pack {
    /// <summary>
    /// Wraps a custom class of type T in a reference-counted class.
    /// </summary>
    /// <typeparam name="T">The type of the custom class to be wrapped. 
    /// Must be a class (reference type).</typeparam>
    public sealed partial class GenericGodotWrapper<T>(T value) : RefCounted where T : class {
        public object Value = value;
    }
}