using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack {

    public sealed partial class GamepadControllerAutoload : Node {

        #region
        public delegate void ControllerConnectedEventHandler(long device, string controllerName);
        public event ControllerConnectedEventHandler? ControllerConnected;

        public delegate void ControllerDisconnectedEventHandler(long device, string previousControllerName, string controllerName);
        public event ControllerDisconnectedEventHandler? ControllerDisconnected;
        #endregion

        public const float DEFAULT_VIBRATION_STRENGTH = 0.5f;
        public const float DEFAULT_VIBRATION_DURATION = 0.65f;

        public const string DEVICE_GENERIC = "generic";
        public const string DEVICE_KEYBOARD = "keyboard";
        public const string DEVICE_XBOX_CONTROLLER = "xbox";
        public const string DEVICE_SWITCH_CONTROLLER = "switch";
        public const string DEVICE_SWITCH_JOYCON_LEFT_CONTROLLER = "switch_left_joycon";
        public const string DEVICE_SWITCH_JOYCON_RIGHT_CONTROLLER = "switch_right_joycon";
        public const string DEVICE_PLAYSTATION_CONTROLLER = "playstation";
        public const string DEVICE_LUNA_CONTROLLER = "luna";

        public static readonly string[] XBOX_BUTTON_LABELS = ["A", "B", "X", "Y", "Back", "Home", "Menu", "Left Stick", "Right Stick", "Left Shoulder", "Right Shoulder", "Up", "Down", "Left", "Right", "Share"];
        public static readonly string[] SWITCH_BUTTON_LABELS = ["B", "A", "Y", "X", "-", "", "+", "Left Stick", "Right Stick", "Left Shoulder", "Right Shoulder", "Up", "Down", "Left", "Right", "Capture"];
        public static readonly string[] PLAYSTATION_BUTTON_LABELS = ["Cross", "Circle", "Square", "Triangle", "Select", "PS", "Options", "L3", "R3", "L1", "R1", "Up", "Down", "Left", "Right", "Microphone"];

        public string CurrentControllerGUID = "";
        public string CurrentControllerName = DEVICE_KEYBOARD;
        public int CurrentDeviceId = 0;
        public bool Connected = false;

        public override void _Notification(int what) {
            if (what == NotificationPredelete)
                Input.StopJoyVibration(CurrentDeviceId);
        }

        public override void _EnterTree() {
            Input.JoyConnectionChanged += OnJoyConnectionChanged;
        }

        public static bool HasJoypads() => Joypads().Count > 0;

        public static Array<int> Joypads() => Input.GetConnectedJoypads();

        public void StartControllerVibration(float weakStrength = DEFAULT_VIBRATION_STRENGTH, float strongStrength = DEFAULT_VIBRATION_STRENGTH, float duration = DEFAULT_VIBRATION_DURATION) {
            if (!CurrentControllerIsKeyboard() && HasJoypads())
                Input.StartJoyVibration(CurrentDeviceId, weakStrength, strongStrength, duration);
        }

        public void StopControllerVibration(float weakStrength = DEFAULT_VIBRATION_STRENGTH, float strongStrength = DEFAULT_VIBRATION_STRENGTH, float duration = DEFAULT_VIBRATION_DURATION) {
            if (!CurrentControllerIsKeyboard() && HasJoypads())
                Input.StopJoyVibration(CurrentDeviceId);
        }

        public void UpdateCurrentController(int device, string controllerName) {
            // https://github.com/mdqinc/SDL_GameControllerDB
            CurrentControllerGUID = Input.GetJoyGuid(device);
            CurrentDeviceId = device;

            CurrentControllerName = controllerName switch {
                "Luna Controller" => DEVICE_LUNA_CONTROLLER,
                "XInput Gamepad" or "Xbox Series Controller" or "Xbox 360 Controller" or "Xbox One Controller" => DEVICE_XBOX_CONTROLLER,
                "Sony DualSense" or "Nacon Revolution Unlimited Pro Controller" or "PS3 Controller" or "PS4 Controller" or "PS5 Controller" => DEVICE_PLAYSTATION_CONTROLLER,
                "Steam Virtual Gamepad" => DEVICE_GENERIC,
                "Switch" or "Switch Controller" or "Nintendo Switch Pro Controller" or "Faceoff Deluxe Wired Pro Controller for Nintendo Switch" => DEVICE_SWITCH_CONTROLLER,
                "Joy-Con (L)" => DEVICE_SWITCH_JOYCON_LEFT_CONTROLLER,
                "Joy-Con (R)" => DEVICE_SWITCH_JOYCON_RIGHT_CONTROLLER,
                _ => DEVICE_KEYBOARD,
            };
        }

        public bool CurrentControllerIsKeyboard() => CurrentControllerName.EqualsIgnoreCase(DEVICE_KEYBOARD);
        public bool CurrentControllerIsGeneric() => CurrentControllerName.EqualsIgnoreCase(DEVICE_GENERIC);
        public bool CurrentControllerIsLuna() => CurrentControllerName.EqualsIgnoreCase(DEVICE_LUNA_CONTROLLER);
        public bool CurrentControllerIsPlaystation() => CurrentControllerName.EqualsIgnoreCase(DEVICE_PLAYSTATION_CONTROLLER);
        public bool CurrentControllerIsXbox() => CurrentControllerName.EqualsIgnoreCase(DEVICE_XBOX_CONTROLLER);
        public bool CurrentControllerIsSwitch() => CurrentControllerName.EqualsIgnoreCase(DEVICE_SWITCH_CONTROLLER);
        public bool CurrentControllerIsSwitchJoycon() => CurrentControllerIsSwitchJoyconRight() || CurrentControllerIsSwitchJoyconLeft();
        public bool CurrentControllerIsSwitchJoyconLeft() => CurrentControllerName.EqualsIgnoreCase(DEVICE_SWITCH_JOYCON_LEFT_CONTROLLER);
        public bool CurrentControllerIsSwitchJoyconRight() => CurrentControllerName.EqualsIgnoreCase(DEVICE_SWITCH_JOYCON_RIGHT_CONTROLLER);

        private void OnJoyConnectionChanged(long device, bool connected) {
            string previousControllerName = CurrentControllerName;

            UpdateCurrentController((int)device, connected ? Input.GetJoyName((int)device) : "");

            if (connected)
                ControllerConnected?.Invoke(device, CurrentControllerName);
            else
                ControllerDisconnected?.Invoke(device, previousControllerName, CurrentControllerName);
        }
    }
}