using Godot;

namespace GodotExtensionator {
    public static class AudioStreamPlayer3DExtension {
        private static readonly RandomNumberGenerator _rng = new();

        public static readonly float VolumeDBInaudible = -80f;

        /// <summary>
        /// Plays the AudioStreamPlayer3D's stream with a specified pitch.
        /// </summary>
        /// <param name="audioPlayer">The AudioStreamPlayer3D to play with a set pitch.</param>
        /// <param name="pitch">The pitch value to apply to the audio playback.</param>
        /// <remarks>
        /// This extension method plays the audio stream associated with the AudioStreamPlayer3D after setting a specific pitch. It first checks if the AudioStreamPlayer3D has a loaded stream (`HasStream`). If it does, it sets the `PitchScale` property to the provided `pitch` value. Finally, it calls the `Play` method to start playback with the adjusted pitch.
        /// </remarks>
        public static void PlayWithPitch(this AudioStreamPlayer3D audioPlayer, float pitch) {
            if (audioPlayer.HasStream()) {
                audioPlayer.PitchScale = pitch;
                audioPlayer.Play();
            }
        }
        /// <summary>
        /// Plays the AudioStreamPlayer3D's stream with a random pitch between the specified range.
        /// </summary>
        /// <param name="audioPlayer">The AudioStreamPlayer3D to play with a random pitch.</param>
        /// <param name="minPitch">The minimum pitch value (default: 0.9).</param>
        /// <param name="maxPitch">The maximum pitch value (default: 1.3).</param>
        /// <remarks>
        /// This extension method plays the audio stream associated with the AudioStreamPlayer3D after setting a random pitch within the provided range. It first checks if the AudioStreamPlayer3D has a loaded stream (`HasStream`). If it does, it sets the `PitchScale` property to a random value between `minPitch` and `maxPitch` (inclusive). Finally, it calls the `Play` method to start playback with the adjusted pitch.
        /// </remarks>
        public static void PlayWithPitchRange(this AudioStreamPlayer3D audioPlayer, float minPitch = .9f, float maxPitch = 1.3f) {
            if (audioPlayer.HasStream()) {
                audioPlayer.PitchScale = _rng.RandfRange(minPitch, maxPitch);
                audioPlayer.Play();
            }
        }

        /// <summary>
        /// Plays the AudioStreamPlayer3D's stream with a volume fade-in effect over a specified duration.
        /// </summary>
        /// <param name="audioPlayer">The AudioStreamPlayer3D to play with a volume fade-in.</param>
        /// <param name="duration">The duration of the volume fade-in effect (default: 1 second).</param>
        /// <remarks>
        /// This extension method plays the audio stream associated with the AudioStreamPlayer3D with a smooth volume fade-in effect. It first checks if the AudioStreamPlayer3D has a loaded stream (`HasStream`). If it does, it sets the initial volume to a very low level (`VOLUME_DB_INAUDIBLE`). Then, it creates a Tween object from the AudioStreamPlayer3D's SceneTree. The Tween animates the `volume_db` property of the AudioStreamPlayer3D from the initial level to 0 dB (full volume) over the specified `duration`. It also sets the transition type to Cubic for a smooth curve and the ease type to Out for an accelerating fade-in effect. Finally, it adds a callback to the Tween that calls the `Play` method of the AudioStreamPlayer3D once the fade-in is complete, ensuring the audio starts after the volume reaches a reasonable level.
        /// </remarks>
        public static void PlayEase(this AudioStreamPlayer3D audioPlayer, float duration = 1f) {

            if (audioPlayer.HasStream()) {
                float originalVolumeDb = audioPlayer.VolumeDb;
                audioPlayer.VolumeDb = VolumeDBInaudible;

                Tween tween = audioPlayer.GetTree().CreateTween();
                tween.TweenProperty(audioPlayer, "volume_db", originalVolumeDb, Mathf.Max(.1f, Mathf.Abs(duration)))
                    .SetTrans(Tween.TransitionType.Cubic)
                    .SetEase(Tween.EaseType.Out);
                tween.TweenCallback(Callable.From(() => audioPlayer.Play()));
            }
        }


        /// <summary>
        /// Plays the AudioStreamPlayer3D's stream with a volume fade-in effect and a specified pitch.
        /// </summary>
        /// <param name="audioPlayer">The AudioStreamPlayer3D to play with a volume fade-in and set pitch.</param>
        /// <param name="duration">The duration of the volume fade-in effect (default: 1 second).</param>
        /// <param name="pitch">The pitch value to apply to the audio playback.</param>
        /// <remarks>
        /// This extension method combines the functionalities of `PlayEase` and `PlayWithPitch` with a single pitch value instead of a random range. It plays the audio stream with a volume fade-in effect and sets the pitch to the provided value. Similar to `PlayEase`, it performs volume manipulation using a Tween and sets the initial volume to inaudible. It then creates a callback that calls `PlayWithPitch` with the specified `pitch` to ensure the audio starts playing after the fade-in with the desired pitch adjustment.
        /// </remarks>
        public static void PlayEaseWithPitch(this AudioStreamPlayer3D audioPlayer, float duration = 1f, float pitch = 0.9f) {
            if (audioPlayer.HasStream()) {
                float originalVolumeDb = audioPlayer.VolumeDb;
                audioPlayer.VolumeDb = VolumeDBInaudible;

                Tween tween = audioPlayer.GetTree().CreateTween();
                tween.TweenProperty(audioPlayer, "volume_db", originalVolumeDb, Mathf.Max(.1f, Mathf.Abs(duration)))
                    .SetTrans(Tween.TransitionType.Cubic)
                    .SetEase(Tween.EaseType.Out);
                tween.TweenCallback(Callable.From(() => audioPlayer.PlayWithPitch(pitch)));
            }
        }

        /// <summary>
        /// Plays the AudioStreamPlayer3D's stream with a volume fade-in effect and a random pitch between the specified range.
        /// </summary>
        /// <param name="audioPlayer">The AudioStreamPlayer3D to play with a volume fade-in and random pitch.</param>
        /// <param name="duration">The duration of the volume fade-in effect (default: 1 second).</param>
        /// <param name="minPitch">The minimum pitch value (default: 0.9).</param>
        /// <param name="maxPitch">The maximum pitch value (default: 1.3).</param>
        public static void PlayEaseWithPitchRange(this AudioStreamPlayer3D audioPlayer, float duration = 1f, float minPitch = .9f, float maxPitch = 1.3f) {
            if (audioPlayer.HasStream()) {
                float originalVolumeDb = audioPlayer.VolumeDb;

                audioPlayer.VolumeDb = VolumeDBInaudible;

                Tween tween = audioPlayer.GetTree().CreateTween();
                tween.TweenProperty(audioPlayer, "volume_db", originalVolumeDb, Mathf.Max(.1f, Mathf.Abs(duration)))
                    .SetTrans(Tween.TransitionType.Cubic)
                    .SetEase(Tween.EaseType.Out);
                tween.TweenCallback(Callable.From(() => audioPlayer.PlayWithPitchRange(minPitch, maxPitch)));
            }
        }

        /// <summary>
        /// Checks if the AudioStreamPlayer3D has a loaded audio stream.
        /// </summary>
        /// <param name="audioPlayer">The AudioStreamPlayer3D to check.</param>
        /// <returns>True if the AudioStreamPlayer3D has a loaded stream, false otherwise.</returns>
        public static bool HasStream(this AudioStreamPlayer3D audioPlayer) => audioPlayer.Stream is not null;
    }

}