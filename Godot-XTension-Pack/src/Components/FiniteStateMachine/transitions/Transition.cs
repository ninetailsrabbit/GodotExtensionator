using Godot;

namespace Godot_XTension_Pack {

    public partial class Transition : RefCounted {
        public MachineState? FromState { get; set; }
        public MachineState? ToState { get; set; }

        public Dictionary<string, object>? Parameters = null;

        public virtual bool ShouldTransition() => true;
        public virtual void OnTransition() { }
    }

}