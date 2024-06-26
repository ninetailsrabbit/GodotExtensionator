using Extensionator;
using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack;
public partial class SoundPoolAutoload : Node {
    public readonly Array<AudioStreamPlayer> StreamPlayersPool = [];
    public int PoolPlayersNumber {
        get => _poolPlayersNumber;
        set {
            _poolPlayersNumber = Mathf.Max(1, value);
            SetupPool();
        }
    }

    private int _poolPlayersNumber = 16;

    public override void _Ready() {
        SetupPool();
    }

    /// <summary>
    /// Plays an AudioStream using a free AudioStreamPlayer from the pool.
    /// </summary>
    /// <param name="stream">The AudioStream to play.</param>
    /// <param name="bus">The audio bus to route the sound to (default: "SFX").</param>
    /// <param name="volume">The volume level (0.0 to 1.0) to apply to the sound (default: 1.0).</param>
    public void Play(AudioStream stream, string bus = "SFX", float volume = 1f) {
        if (NextAvailableAudioStreamPlayer() is AudioStreamPlayer streamPlayer) {
            streamPlayer.Stream = stream;
            streamPlayer.Bus = bus;
            streamPlayer.VolumeDb = Mathf.LinearToDb(volume);
            streamPlayer.Play();
        }
    }

    /// <summary>
    /// Plays an AudioStream with a specified pitch using a free AudioStreamPlayer from the pool.
    /// </summary>
    /// <param name="stream">The AudioStream to play.</param>
    /// <param name="bus">The audio bus to route the sound to (default: "SFX").</param>
    /// <param name="volume">The volume level (0.0 to 1.0) to apply to the sound (default: 1.0).</param>
    /// <param name="pitch">The pitch value to apply to the playback (default: 1.0 - no pitch shift).</param>
    public void PlayWithPitch(AudioStream stream, string bus = "SFX", float volume = 1f, float pitch = 0.9f) {
        if (NextAvailableAudioStreamPlayer() is AudioStreamPlayer streamPlayer) {
            streamPlayer.Stream = stream;
            streamPlayer.Bus = bus;
            streamPlayer.VolumeDb = Mathf.LinearToDb(volume);
            streamPlayer.PlayWithPitch(pitch);
        }
    }

    /// <summary>
    /// Plays an AudioStream with a random pitch between the specified range using a free AudioStreamPlayer from the pool.
    /// </summary>
    /// <param name="stream">The AudioStream to play.</param>
    /// <param name="bus">The audio bus to route the sound to (default: "SFX").</param>
    /// <param name="volume">The volume level (0.0 to 1.0) to apply to the sound (default: 1.0).</param>
    /// <param name="minPitch">The minimum pitch value for the random range (default: 0.9).</param>
    /// <param name="maxPitch">The maximum pitch value for the random range (default: 1.3).</param>
    /// <remarks>
    /// This function extends the functionality of `PlayWithPitch` by allowing playback with a random pitch within a specified range. It follows the same pattern of finding an available AudioStreamPlayer, setting its properties, and then calling the appropriate playback method. In this case, it uses `PlayWithPitchRange` to play the audio stream with a pitch randomly chosen between `minPitch` and `maxPitch`.
    /// </remarks>
    public void PlayWithPitchRange(AudioStream stream, string bus = "SFX", float volume = 1f, float minPitch = .9f, float maxPitch = 1.3f) {
        if (NextAvailableAudioStreamPlayer() is AudioStreamPlayer streamPlayer) {
            streamPlayer.Stream = stream;
            streamPlayer.Bus = bus;
            streamPlayer.VolumeDb = Mathf.LinearToDb(volume);
            streamPlayer.PlayWithPitchRange(minPitch, maxPitch);
        }
    }

    /// <summary>
    /// Plays a random AudioStream from the provided list on the specified bus with a given volume.
    /// </summary>
    /// <param name="streams">The List containing AudioStream objects to choose from.</param>
    /// <param name="bus">The name of the audio bus to route the sound to (default: "SFX").</param>
    /// <param name="volume">The volume level (0.0 to 1.0) to apply to the sound (default: 1.0).</param>
    public void PlayRandomStream(List<AudioStream> streams, string bus = "SFX", float volume = 1f) {
        if (streams.IsEmpty() || AudioManager.IsMuted(bus) || !AudioManager.Instance.BusExists(bus))
            return;

        Play(streams.RandomElement(), bus, volume);
    }

    /// <summary>
    /// Plays a random AudioStream from the provided list with a specified pitch on the specified bus with a given volume.
    /// </summary>
    /// <param name="streams">The List containing AudioStream objects to choose from.</param>
    /// <param name="bus">The name of the audio bus to route the sound to (default: "SFX").</param>
    /// <param name="volume">The volume level (0.0 to 1.0) to apply to the sound (default: 1.0).</param>
    /// <param name="pitch">The pitch value to apply to the playback (default: 1.0 - no pitch shift).</param>
    /// <remarks>
    /// This function follows a similar logic to `PlayRandomStream` with additional checks to ensure the bus is not muted and exists. It then selects a random stream, and depending on the function variant (PlayRandomStreamWithPitch or PlayRandomStreamWithPitchRange), it calls `PlayWithPitch` or `PlayWithPitchRange` on the AudioManager, passing the chosen stream, bus name, volume, and pitch (or min/max pitch for the range version).
    /// </remarks>
    public void PlayRandomStreamWithPitch(List<AudioStream> streams, string bus = "SFX", float volume = 1f, float pitch = 0.9f) {
        if (streams.IsEmpty() || AudioManager.IsMuted(bus) || !AudioManager.Instance.BusExists(bus))
            return;

        PlayWithPitch(streams.RandomElement(), bus, volume, pitch);
    }


    /// <summary>
    /// Plays a random AudioStream from the provided list with a random pitch within a specified range on the specified bus with a given volume.
    /// </summary>
    /// <param name="streams">The List containing AudioStream objects to choose from.</param>
    /// <param name="bus">The name of the audio bus to route the sound to (default: "SFX").</param>
    /// <param name="volume">The volume level (0.0 to 1.0) to apply to the sound (default: 1.0).</param>
    /// <param name="minPitch">The minimum pitch value for the random range (default: 0.9).</param>
    /// <param name="maxPitch">The maximum pitch value for the random range (default: 1.3).</param>
    /// <remarks>
    /// This function follows a similar logic to `PlayRandomStream` with additional checks to ensure the bus is not muted and exists. It then selects a random stream and calls `PlayWithPitchRange` on the AudioManager, passing the chosen stream, bus name, volume, and the `minPitch` and `maxPitch` values to define the random pitch range.
    /// </remarks>
    public void PlayRandomStreamWithPitchRange(List<AudioStream> streams, string bus = "SFX", float volume = 1f, float minPitch = .9f, float maxPitch = 1.3f) {
        if (streams.IsEmpty() || AudioManager.IsMuted(bus) || !AudioManager.Instance.BusExists(bus))
            return;

        PlayWithPitchRange(streams.RandomElement(), bus, volume, minPitch, maxPitch);
    }

    /// <summary>
    /// Stops all AudioStreamPlayer instances playing on the specified audio bus by name.
    /// </summary>
    /// <param name="bus">The name of the audio bus to stop streams from.</param>
    /// <remarks>
    /// This function first checks if the target `bus` exists using `AudioManager.BusExists`. If the bus exists, it iterates through all AudioStreamPlayer objects in the `StreamPlayersPool`. For each player, it compares the player's `Bus` property converted to a string (using `ToString().EqualsIgnoreCase`) with the provided `bus` name (case-insensitive). If there's a match, it calls `Stop` on the AudioStreamPlayer to stop playback.
    /// </remarks>
    public void StopStreamsFromBus(string bus) {
        if (AudioManager.Instance.BusExists(bus)) {
            foreach (AudioStreamPlayer audioPlayer in StreamPlayersPool) {
                if (audioPlayer.Bus.ToString().EqualsIgnoreCase(bus))
                    audioPlayer.Stop();
            }
        }
    }

    /// <summary>
    /// Stops all AudioStreamPlayer instances playing on the specified audio bus by index.
    /// </summary>
    /// <param name="bus">The index of the audio bus to stop streams from.</param>
    /// <remarks>
    /// This function first checks if the target `bus` exists using `AudioManager.BusExists`. If the bus exists, it iterates through all AudioStreamPlayer objects in the `StreamPlayersPool`. For each player, it compares the player's `Bus` property converted to a string (using `ToString().EqualsIgnoreCase`) with the provided `bus` name (case-insensitive). If there's a match, it calls `Stop` on the AudioStreamPlayer to stop playback.
    /// </remarks>
    public void StopStreamsFromBus(int bus) {
        StopStreamsFromBus(AudioServer.GetBusName(bus));
    }

    private void SetupPool() {
        if (IsInsideTree()) {
            StreamPlayersPool.Clear();
            this.QueueFreeChildren();

            foreach (int index in GD.Range(PoolPlayersNumber)) {
                AudioStreamPlayer streamPlayer = new() { Name = $"PoolAudioStreamPlayer{index + 1}" };
                StreamPlayersPool.Add(streamPlayer);
                AddChild(streamPlayer);
            }
        }
    }
    private AudioStreamPlayer? NextAvailableAudioStreamPlayer() {
        AudioStreamPlayer[] availableStreamPlayers = StreamPlayersPool.
            Where((audioPlayer) => !audioPlayer.Playing && !audioPlayer.StreamPaused)
            .ToArray();

        return availableStreamPlayers.Length > 0 ? availableStreamPlayers[0] : null;
    }

}