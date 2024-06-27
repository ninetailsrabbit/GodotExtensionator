using Extensionator;
using Godot;

namespace Godot_XTension_Pack {
    public partial class FootstepComponent : Node3D {
        [Export] public RayCast3D FloorDetectorRaycast { get; set; } = default!;
        [Export] public bool UsePitch = true;
        [Export] public float MinPitchRange = .9f;
        [Export] public float MaxPitchRange = 1.3f;

        public const float DefaultIntervalTime = .6f;
        public Dictionary<string, AudioStreamPlayer3D> SoundsMaterial = [];

        public Godot.Timer IntervalTimer { get; set; } = default!;
        public bool SfxPlaying = false;

        public override void _EnterTree() {
            ArgumentNullException.ThrowIfNull(FloorDetectorRaycast);

            CreateIntervalTimer();
        }

        public void Footstep(float interval = DefaultIntervalTime) {
            if (FloorDetectorRaycast.IsColliding() &&
                !SoundsMaterial.IsEmpty() &&
                !SfxPlaying &&
                IsInstanceValid(IntervalTimer) &&
                IntervalTimer.TimeLeft == 0
            ) {

                var collider = FloorDetectorRaycast.GetCollider();
                Godot.Collections.Array<StringName> groups = [];

                switch (collider) {
                    case CsgShape3D csgShape3D:
                        groups = csgShape3D.GetGroups();
                        break;
                    case StaticBody3D staticBody:
                        groups = staticBody.GetGroups();
                        break;
                    case Area3D area:
                        groups = area.GetGroups();
                        break;
                }

                if (groups.Count.IsGreaterThanZero()) {
                    foreach (StringName group in groups) {
                        if (SoundsMaterial.TryGetValue(group, out AudioStreamPlayer3D? audioPlayer)) {

                            if (UsePitch)
                                audioPlayer.PlayWithPitchRange(MinPitchRange, MaxPitchRange);
                            else
                                audioPlayer.Play();

                            IntervalTimer.Start(interval);
                            SfxPlaying = true;
                            break;
                        }
                    }
                }
            }
        }

        private void CreateIntervalTimer() {
            IntervalTimer ??= new() {
                Name = "FootstepManagerIntervalTimer",
                ProcessCallback = Godot.Timer.TimerProcessCallback.Physics,
                Autostart = false,
                OneShot = true
            };

            AddChild(IntervalTimer);
            IntervalTimer.Timeout += OnIntervalTimerTimeout;
        }

        private void OnIntervalTimerTimeout() {
            SfxPlaying = false;
        }

    }
}
