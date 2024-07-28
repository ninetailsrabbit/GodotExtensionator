using Godot;

namespace GodotExtensionator {
    public static class CharacterBodyExtension {

        /// <summary>
        /// Calculates the current speed of the CharacterBody3D in kilometers per hour based on its velocity vector.
        /// Ignores the Y component of the velocity (vertical movement) and clamps the absolute value of the Y component to prevent negative speeds.
        /// </summary>
        /// <param name="body">The CharacterBody3D object to calculate the speed for.</param>
        /// <returns>The current speed of the CharacterBody3D in kilometers per hour.</returns>

        public static float CurrentSpeed(this CharacterBody3D body)
            => Mathf.Max(new Vector3(body.Velocity.X, 0f, body.Velocity.Z).Length(), Mathf.Clamp(Mathf.Abs(body.Velocity.Y), 0f, float.MaxValue));
    }
}
