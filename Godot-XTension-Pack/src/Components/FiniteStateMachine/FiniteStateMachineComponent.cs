using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack {

    public partial class FiniteStateMachineComponent : Node {
        #region Events
        public delegate void StateChangedEventHandler(MachineState from, MachineState to);
        public event StateChangedEventHandler? StateChanged;

        public delegate void StateChangeFailedEventHandler(MachineState from, MachineState to);
        public event StateChangeFailedEventHandler? StateChangeFailed;

        public delegate void StackPushedEventHandler(MachineState newState, Array<MachineState> stack);
        public event StackPushedEventHandler? StackPushed;

        public delegate void StackFlushedEventHandler(Array<MachineState> stack);
        public event StackFlushedEventHandler? StackFlushed;
        #endregion

        #region Exports
        [Export] public MachineState CurrentState = null!;
        [Export] public bool EnableStack = true;
        [Export] public int StackCapacity = 3;
        [Export] public bool FlushStackWhenReachCapacity = false;

        #endregion

        public readonly Godot.Collections.Dictionary<string, MachineState> States = [];
        public readonly Godot.Collections.Dictionary<string, Transition> Transitions = [];
        public readonly Array<MachineState> StatesStack = [];

        public bool IsTransitioning = false;
        public bool Locked = false;


        #region Public
        public override void _Ready() {
            InitializeStateNodes();

            if (CurrentState == null) {
                GD.PushError("This Finite state machine does not have an initial state defined");
                return;
            }

            StateChanged += OnStateChanged;
            StateChangeFailed += OnStateChangeFailed;


            EnterState(CurrentState);
        }


        public void ChangeStateTo(MachineState nextState, System.Collections.Generic.Dictionary<string, object>? parameters = null) {
            if (!StateExists(nextState)) {
                GD.PushError($"The change of state cannot be done because the state {nextState} does not exits in this Finite State Machine");
                return;
            }

            if (CurrentStateIs(nextState))
                return;

            if (CurrentState is not null)
                RunTransition(CurrentState, nextState, parameters);

        }

        public void ChangeStateTo(string nextState, System.Collections.Generic.Dictionary<string, object>? parameters = null) {
            if (!StateExists(nextState)) {
                GD.PushError($"The change of state cannot be done because the state {nextState} does not exits in this Finite State Machine");
                return;
            }

            if (CurrentStateIs(nextState))
                return;

            if (CurrentState is not null && GetStateByName(nextState) is MachineState nextTransitionState)
                RunTransition(CurrentState, nextTransitionState, parameters);
        }
        public void ChangeStateTo<T>(System.Collections.Generic.Dictionary<string, object>? parameters = null) {
            string nextState = typeof(T).Name;

            if (!StateExists(nextState)) {
                GD.PushError($"The change of state cannot be done because the state {nextState} does not exits in this Finite State Machine");
                return;
            }

            if (CurrentStateIs(nextState))
                return;

            if (CurrentState is not null && !IsTransitioning && GetStateByName(nextState) is MachineState nextTransitionState)
                RunTransition(CurrentState, nextTransitionState, parameters);
        }

        public void RunTransition(MachineState from, MachineState to, System.Collections.Generic.Dictionary<string, object>? parameters = null) {
            IsTransitioning = true;

            string transitionName = BuildTransitionName(CurrentState, to);

            if (!Transitions.ContainsKey(transitionName))
                Transitions[transitionName] = new NeutralTransition();

            Transition transition = Transitions[transitionName];
            transition.FromState = CurrentState;
            transition.ToState = to;
            transition.Parameters = parameters;

            if (transition.ShouldTransition()) {
                transition.OnTransition();

                PushStateToStack(from);
                ExitState(from, to);

                States.Values.ToList().ForEach(state => state.Disable());
                CurrentState = to;
                States.Values.ToList().ForEach(state => state.Enable());

                StateChanged?.Invoke(transition.FromState, transition.ToState);

                EnterState(to);
            }
            else {
                StateChangeFailed?.Invoke(transition.FromState, transition.ToState);
            }

        }

        public bool CurrentStateIs(string name) {
            return name.Trim().ToLower().Equals(CurrentState.Name.ToString().Trim().ToLower());
        }

        public bool CurrentStateIs(MachineState state) {
            return state.Equals(CurrentState);
        }

        public bool CurrentStateIsNot(string[] states) {
            return !states.Any(state => CurrentStateIs(state));
        }

        public bool StateExists(MachineState state) {
            return States.ContainsKey(state.Name);
        }
        public bool StateExists(string name) {
            return States.ContainsKey(name);
        }

        public void EnterState(MachineState state) {
            state.Enter();
        }

        public void ExitState(MachineState state, MachineState nextState) {
            state.Exit(nextState);
        }

        public MachineState? GetStateByName(string name) {
            if (States.TryGetValue(name, out MachineState? value))
                return value;

            return null;
        }

        public MachineState? LastState() {
            return StatesStack.Count > 0 ? StatesStack.Last() : null;
        }

        public void PushStateToStack(MachineState state) {
            if (EnableStack && StackCapacity > 0) {
                if (StatesStack.Count >= StackCapacity) {
                    if (FlushStackWhenReachCapacity) {
                        StatesStack.Clear();
                        StackFlushed?.Invoke(StatesStack);
                    }
                    else {
                        StatesStack.RemoveAt(0);
                    }
                }

                StatesStack.Add(state);
                StackPushed?.Invoke(state, StatesStack);
            }
        }

        public override void _UnhandledInput(InputEvent @event) {
            CurrentState.HandleInput(@event);
            @event.Dispose();// Avoid memory leak
        }

        public override void _PhysicsProcess(double delta) {
            CurrentState.PhysicsUpdate(delta);
        }

        public override void _Process(double delta) {
            CurrentState.Update(delta);
        }

        public void LockStateMachine() {
            this.Disable();
        }

        public void UnlockStateMachine() {
            this.Enable();
        }

        #endregion

        #region PrivateFunctions
        private void InitializeStateNodes() {
            foreach (MachineState state in this.GetNodesByClass<MachineState>()) {
                AddStateToDictionary(state);
            }
        }

        public void RegisterTransitions(params Transition[] transitions) {
            foreach (Transition transition in transitions)
                RegisterTransition(transition);
        }

        /// <summary>
        /// To register a new transition just use like this example: Transitions.Add(new NeutralTransition());
        /// </summary>
        public void RegisterTransition(Transition transition) {
            Transitions.Add(transition.GetType().Name, transition);
        }

        private string BuildTransitionName(MachineState from, MachineState to) {
            var transitionName = $"{from.Name.ToString().Trim()}To{to.Name.ToString().Trim()}Transition";

            if (!Transitions.ContainsKey(transitionName))
                transitionName = $"AnyTo{to.Name.ToString().Trim()}Transition";

            return transitionName;
        }

        private void AddStateToDictionary(MachineState state) {
            States.Add(state.Name, state);
            state.FSM = this;
            state.Ready();
        }

        #endregion

        #region SignalCallbacks
        private void OnStateChanged(MachineState from, MachineState to) {
            IsTransitioning = false;
        }

        private void OnStateChangeFailed(MachineState from, MachineState to) {
            IsTransitioning = false;
        }

        #endregion

    }

}