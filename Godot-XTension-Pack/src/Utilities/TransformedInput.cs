using Godot;
using System.Diagnostics;

namespace Godot_XTension_Pack {
    public sealed partial class TransformedInput : RefCounted {
        public readonly string MoveRightAction = "move_right";
        public readonly string MoveLeftAction = "move_left";
        public readonly string MoveForwardAction = "move_forward";
        public readonly string MoveBackAction = "move_back";

        public Node Actor;

        float Deadzone { get => _deadzone; set { _deadzone = Mathf.Clamp(value, 0.0f, 1f); } }
        float _deadzone = 0.5f;

        #region CurrentInput
        public Vector2 InputDirection { get; set; }
        public Vector2 InputDirectionDeadzoneSquareShape { get; set; }
        public float InputDirectionHorizontalAxis { get; set; }
        public float InputDirectionVerticalAxis { get; set; }
        public float InputDirectionHorizontalAxisAppliedDeadzone { get; set; }
        public float InputDirectionVerticalAxisAppliedDeadzone { get; set; }
        public Vector3 WorldCoordinateSpaceDirection { get; set; }
        #endregion

        #region PreviousInput
        public Vector2 PreviousInputDirection { get; set; }
        public Vector2 PreviousInputDirectionDeadzoneSquareShape { get; set; }
        public float PreviousInputDirectionHorizontalAxis { get; set; }
        public float PreviousInputDirectionVerticalAxis { get; set; }
        public float PreviousInputDirectionHorizontalAxisAppliedDeadzone { get; set; }
        public float PreviousInputDirectionVerticalAxisAppliedDeadzone { get; set; }
        public Vector3 PreviousWorldCoordinateSpaceDirection { get; set; }
        #endregion

        public TransformedInput(Node actor, float deadzone = 0.5f) {
            Debug.Assert(actor is Node2D || actor is Node3D, "TransformedInput:  The actor needs to inherit from Node2D or Node3D");

            Actor = actor;
            Deadzone = deadzone;
        }

        /// <summary>
        /// Updates the movement-related properties for the current frame.
        /// </summary>
        public void Update() {
            UpdatePreviousDirections();

            InputDirection = Input.GetVector(MoveLeftAction, MoveRightAction, MoveForwardAction, MoveBackAction);

            if (Actor is Node3D actor)
                WorldCoordinateSpaceDirection = actor.Transform.Basis * new Vector3(InputDirection.X, 0, InputDirection.Y).Normalized();

            InputDirectionDeadzoneSquareShape = new Vector2(
                Input.GetActionStrength(MoveLeftAction) - Input.GetActionStrength(MoveLeftAction),
                Input.GetActionStrength(MoveForwardAction) - Input.GetActionStrength(MoveBackAction)
            ).LimitLength(Deadzone);

            InputDirectionHorizontalAxis = Input.GetAxis(MoveLeftAction, MoveRightAction);
            InputDirectionVerticalAxis = Input.GetAxis(MoveForwardAction, MoveBackAction);

            InputDirectionHorizontalAxisAppliedDeadzone = InputDirectionHorizontalAxis * (1f - Deadzone);
            InputDirectionVerticalAxisAppliedDeadzone = InputDirectionVerticalAxis * (1f - Deadzone);

        }

        /// <summary>
        /// Stores the current movement-related properties as their previous versions for comparison.
        /// </summary>
        private void UpdatePreviousDirections() {
            PreviousInputDirection = InputDirection;
            PreviousInputDirectionDeadzoneSquareShape = InputDirectionDeadzoneSquareShape;
            PreviousInputDirectionHorizontalAxis = InputDirectionHorizontalAxis;
            PreviousInputDirectionVerticalAxis = InputDirectionVerticalAxis;
            PreviousInputDirectionHorizontalAxisAppliedDeadzone = InputDirectionHorizontalAxisAppliedDeadzone; ;
            PreviousInputDirectionVerticalAxisAppliedDeadzone = InputDirectionVerticalAxisAppliedDeadzone;
            PreviousWorldCoordinateSpaceDirection = WorldCoordinateSpaceDirection;
        }
    }
}