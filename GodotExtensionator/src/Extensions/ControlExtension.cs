using Godot;

namespace GodotExtensionator {
    public static class ControlExtension {

        /// <summary>
        /// Centers the pivot offset of a Control to its size divided by 2.
        /// </summary>
        /// <param name="control">The Control to center the pivot offset of.</param>
        public static void CenterPivotOffset(this Control control) => control.PivotOffset = control.Size / 2f;

        /// <summary>
        /// Sets the mouse filter of a Control to ignore all mouse events.
        /// </summary>
        /// <param name="control">The Control to ignore mouse events for.</param>
        public static void IgnoreMouseEvents(this Control control) {
            control.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        /// <summary>
        /// Sets the mouse filter of a Control to stop propagating mouse events to its parent controls.
        /// </summary>
        /// <param name="control">The Control to stop propagating mouse events for.</param>
        public static void StopMouseEvents(this Control control) {
            control.MouseFilter = Control.MouseFilterEnum.Stop;
        }

        /// <summary>
        /// Sets the mouse filter of a Control to allow mouse events to pass through to its parent controls.
        /// </summary>
        /// <param name="control">The Control to allow mouse events to pass through.</param>
        public static void PassMouseEvents(this Control control) {
            control.MouseFilter = Control.MouseFilterEnum.Pass;
        }

    }

}