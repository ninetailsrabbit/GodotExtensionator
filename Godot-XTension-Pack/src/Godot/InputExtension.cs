using Godot;

namespace Godot_XTension_Pack {
    public static class InputExtension {
        /// <summary>
        /// An array containing the keycodes for numeric keys (0-9 and numpad 0-9).
        /// </summary>
        public static readonly int[] NumericKeys = [48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
        public static bool IsMouseButton(this InputEvent @event) => @event is InputEventMouseButton;

        /// <summary>
        /// Checks if the provided InputEvent is a left mouse button click.
        /// </summary>
        /// <param name="event">The InputEvent to check.</param>
        /// <returns>True if the event is a left mouse button click, false otherwise.</returns>
        public static bool IsMouseLeftClick(this InputEvent @event) {
            
            if (@event is InputEventMouseButton button)
                return button.ButtonIndex == MouseButton.Left && button.Pressed;

            return false;
        }

        /// <summary>
        /// Checks if the provided InputEvent is a right mouse button click.
        /// </summary>
        /// <param name="event">The InputEvent to check.</param>
        /// <returns>True if the event is a right mouse button click, false otherwise.</returns>
        public static bool IsMouseRightClick(this InputEvent @event) {
            if (@event is InputEventMouseButton button)
                return button.ButtonIndex == MouseButton.Right && button.Pressed;

            return false;
        }

        /// <summary>
        /// Checks if a provided InputEvent object is a mouse button press event for the right mouse button.
        /// </summary>
        /// <param name="event">The InputEvent object to check.</param>
        /// <returns>True if the event is a right mouse button press, false otherwise.</returns>
        public static bool IsMouseRightButtonPressed(this InputEvent @event) => @event is InputEventMouse && Input.IsMouseButtonPressed(MouseButton.Right);

        /// <summary>
        /// Checks if a provided InputEvent object is a mouse button press event for the left mouse button.
        /// </summary>
        /// <param name="event">The InputEvent object to check.</param>
        /// <returns>True if the event is a left mouse button press, false otherwise.</returns>
        public static bool IsMouseLefttButtonPressed(this InputEvent @event) => @event is InputEventMouse && Input.IsMouseButtonPressed(MouseButton.Left);

        /// <summary>
        /// Checks if a provided InputEvent object is a gamepad button press event.
        /// </summary>
        /// <param name="event">The InputEvent object to check.</param>
        /// <returns>True if the event is a gamepad button press, false otherwise.</returns>
        public static bool IsControllerButton(this InputEvent @event) => @event is InputEventJoypadButton;

        /// <summary>
        /// Checks if a provided InputEvent object is a gamepad axis movement event.
        /// </summary>
        /// <param name="event">The InputEvent object to check.</param>
        /// <returns>True if the event is a gamepad axis movement, false otherwise.</returns>
        public static bool IsControllerAxis(this InputEvent @event) => @event is InputEventJoypadMotion;


        /// <summary>
        /// Checks if a provided InputEvent object is a gamepad button press or axis movement event.
        /// </summary>
        /// <param name="event">The InputEvent object to check.</param>
        /// <returns>True if the event is a gamepad input (button or axis), false otherwise.</returns>
        public static bool IsGamepadInput(this InputEvent @event) => @event.IsControllerButton() || @event.IsControllerAxis();


        /// <summary>
        /// Makes the mouse cursor visible on the screen.
        /// </summary>
        public static void ShowMouseCursor() {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }

        /// <summary>
        /// Makes the mouse cursor visible and confines it to the game window.
        /// </summary>
        public static void ShowMouseCursorConfined() {
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }

        /// <summary>
        /// Hides the mouse cursor from the screen.
        /// </summary>
        public static void HideMouseCursor() {
            Input.MouseMode = Input.MouseModeEnum.Hidden;
        }

        /// <summary>
        /// Hides the mouse cursor while keeping it confined to the game window.
        /// </summary>
        public static void HideMouseCursorConfined() {
            Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;
        }


        /// <summary>
        /// Hides the mouse cursor while keeping it confined to the game window.
        /// </summary>
        public static void CaptureMouse() {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        /// <summary>
        /// Checks if the mouse cursor is currently visible on the screen.
        /// </summary>
        /// <returns>True if the mouse cursor is visible (either normally or confined), false otherwise.</returns>
        public static bool IsMouseVisible() => Input.MouseMode.Equals(Input.MouseModeEnum.Visible) || Input.MouseMode.Equals(Input.MouseModeEnum.Confined);

        /// <summary>
        /// Checks if the provided InputEvent is a key press event for a numeric key.
        /// </summary>
        /// <param name="event">The InputEvent to check.</param>
        /// <returns>True if the event is a key press event for a numeric key, false otherwise.</returns>
        public static bool NumericKeyPressed(this InputEvent @event) {

            return @event is InputEventKey key && key.Pressed && (NumericKeys.Contains((int)key.Keycode) || NumericKeys.Contains((int)key.PhysicalKeycode));
        }


        /// <summary>
        /// Converts an InputEvent object into a human-readable string representation with modifiers.
        /// </summary>
        /// <param name="event">The InputEventKey object to convert.</param>
        /// <returns>A string representing the key and its modifiers (e.g., "Ctrl+Shift+A").</returns>
        /// <remarks>
        /// This extension method takes an `InputEventKey` object and transforms it into a more user-friendly string format. It handles two cases:
        ///   - If the `Keycode` property of the `InputEventKey` is `Key.None`, it assumes the key represents a physical key with potential modifiers. It then calls `GetPhysicalKeycodeWithModifiers` to retrieve the actual keycode and modifiers.
        ///   - If the `Keycode` property is not `Key.None`, it assumes the key represents a logical key with modifiers already included. It directly calls `GetKeycodeWithModifiers` to get the combined representation.
        /// 
        /// In both cases, the retrieved keycode with modifiers is then converted to a string using `OS.GetKeycodeString`. Finally, it replaces all "+" characters with " + " (space added after the plus) to improve readability by separating modifiers.
        /// </remarks>
        /// 
        public static string ReadableKey(this InputEvent @event) {
            if (@event is InputEventKey eventKey)
                return eventKey.ReadableKey();

            return "";
        }

        /// <summary>
        /// Converts an InputEventKey object into a human-readable string representation with modifiers.
        /// </summary>
        /// <param name="key">The InputEventKey object to convert.</param>
        /// <returns>A string representing the key and its modifiers (e.g., "Ctrl+Shift+A").</returns>
        /// <remarks>
        /// This extension method takes an `InputEventKey` object and transforms it into a more user-friendly string format. It handles two cases:
        ///   - If the `Keycode` property of the `InputEventKey` is `Key.None`, it assumes the key represents a physical key with potential modifiers. It then calls `GetPhysicalKeycodeWithModifiers` to retrieve the actual keycode and modifiers.
        ///   - If the `Keycode` property is not `Key.None`, it assumes the key represents a logical key with modifiers already included. It directly calls `GetKeycodeWithModifiers` to get the combined representation.
        /// 
        /// In both cases, the retrieved keycode with modifiers is then converted to a string using `OS.GetKeycodeString`. Finally, it replaces all "+" characters with " + " (space added after the plus) to improve readability by separating modifiers.
        /// </remarks>
        /// 
        public static string ReadableKey(this InputEventKey key) {
            Key keyWithModifiers = key.Keycode is Key.None ? key.GetPhysicalKeycodeWithModifiers() : key.GetKeycodeWithModifiers();

            return OS.GetKeycodeString(keyWithModifiers).Replace("+", "+ ").StripEdges();
        }

        /// <summary>
        /// Retrieves all InputEvents associated with a specified action name.
        /// </summary>
        /// <param name="action">The StringName representing the action to query.</param>
        /// <returns>An IEnumerable collection containing all InputEvents bound to the specified action.</returns>
        /// <remarks>
        /// This function utilizes the `InputMap.ActionGetEvents` method to retrieve a collection of InputEvents related to the provided action name. 
        /// </remarks>
        public static IEnumerable<InputEvent> GetAllInputsForAction(StringName action)
            => InputMap.ActionGetEvents(action);


        /// <summary>
        /// Retrieves keyboard and mouse InputEvents associated with a specified action name.
        /// </summary>
        /// <param name="action">The StringName representing the action to query.</param>
        /// <returns>An IEnumerable collection containing keyboard and mouse InputEvents bound to the specified action.</returns>
        /// <remarks>
        /// This function first calls `InputMap.ActionGetEvents` to get all InputEvents for the action. Then, it uses `Where` with a lambda expression to filter the collection and return only events that are either `InputEventKey` (keyboard) or `InputEventMouse`.
        /// </remarks>
        public static IEnumerable<InputEvent> GetKeyboardInputsForAction(StringName action) {

            return InputMap.ActionGetEvents(action)
                .Where((inputEvent) => inputEvent is InputEventKey || inputEvent is InputEventMouse);
        }

        /// <summary>
        /// Retrieves gamepad button and axis movement InputEvents associated with a specified action name.
        /// </summary>
        /// <param name="action">The StringName representing the action to query.</param>
        /// <returns>An IEnumerable collection containing gamepad button and axis movement InputEvents bound to the specified action.</returns>
        /// <remarks>
        /// Similar to `GetKeyboardInputsForAction`, this function retrieves all events using `InputMap.ActionGetEvents` and then filters the collection using `Where` to keep only `InputEventJoypadButton` (buttons) or `InputEventJoypadMotion` (axis movements).
        /// </remarks>
        public static IEnumerable<InputEvent> GetJoypadInputsForAction(StringName action) {
            return InputMap.ActionGetEvents(action)
                .Where((inputEvent) => inputEvent is InputEventJoypadButton || inputEvent is InputEventJoypadMotion);
        }

    }

}