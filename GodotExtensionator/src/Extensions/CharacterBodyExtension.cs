using Godot;

namespace GodotExtensionator {
    public static class CharacterBodyExtension {

        public static float CurrentSpeed(this CharacterBody3D body)
            => Mathf.Max(new Vector3(body.Velocity.X, 0f, body.Velocity.Z).Length(), Mathf.Clamp(Mathf.Abs(body.Velocity.Y), 0f, float.MaxValue));
    }
}
