namespace Godot_XTension_Pack {

    /// <summary>
    /// This Neutral transition is used whene two states does not have a transition defined so
    /// the Finite State Machine flow can continue without problems
    /// </summary>
    public partial class NeutralTransition : Transition {
        public override bool ShouldTransition() => true;
        public override void OnTransition() { }
    }

}