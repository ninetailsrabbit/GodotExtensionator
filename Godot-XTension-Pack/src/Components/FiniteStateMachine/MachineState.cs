using Godot;

namespace Godot_XTension_Pack {
    public abstract partial class MachineState : Node {
        [Signal]
        public delegate void StateEnteredEventHandler();

        [Signal]
        public delegate void StateFinishedEventHandler(MachineState nextState);


        public FiniteStateMachine? FSM;

        public virtual void Ready() {

        }

        public virtual void Enter() {

        }

        public virtual void Exit(MachineState nextState) {

        }

        public virtual void HandleInput(InputEvent @event) {

        }

        public virtual void PhysicsUpdate(double delta) {

        }

        public virtual void Update(double delta) {

        }
    }

}