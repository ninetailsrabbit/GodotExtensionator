using Godot;

namespace Godot_XTension_Pack {
    public class AutoloadWrapper<T> : Node where T: class {
        public T? Instance { get; private set; }

        public override void _EnterTree() {
            Instance = Activator.CreateInstance(typeof(T)) as T;

            ArgumentNullException.ThrowIfNull(Instance);
        }
    }
}

