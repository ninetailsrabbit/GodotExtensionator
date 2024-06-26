using Godot;

namespace Godot_XTension_Pack;

public partial class MusicManager : Node {

    #region Events
    public delegate void AddedMusicToBankEventHandler(string name, AudioStream stream);
    public event AddedMusicToBankEventHandler? AddedMusicToBank;

    public delegate void RemovedMusicFromBankEventHandler(string name);
    public event RemovedMusicFromBankEventHandler? RemovedMusicFromBank;

    public delegate void ChangedStreamEventHandler(AudioStream from, AudioStream to);
    public event ChangedStreamEventHandler? ChangedStream;

    public delegate void StartedStreamEventHandler(AudioStreamPlayer audioPlayer, AudioStream newStream);
    public event StartedStreamEventHandler? StartedStream;

    public delegate void FinishedStreamEventHandler(AudioStreamPlayer audioPlayer, AudioStream oldStream);
    public event FinishedStreamEventHandler? FinishedStream;
    #endregion

    public const float VOLUME_DB_INAUDIBLE = -80f;
    public const float CROSSFADE_TIME = 2f;

    public AudioStreamPlayer MainAudioStreamPlayer = null!;
    public AudioStreamPlayer SecondaryAudioStreamPlayer = null!;
    public AudioStreamPlayer CurrentAudioStreamPlayer = null!;

    public Dictionary<string, AudioStream> MusicBank = [];

    public override void _Ready() {
        CreateAudioStreamPlayers();
    }

    /// <summary>
    /// Plays a specific music stream from the MusicBank, optionally with crossfading.
    /// </summary>
    /// <param name="streamName">The name of the AudioStream object to play.</param>
    /// <param name="crossfade">A flag indicating whether to use crossfading when switching streams (default: true).</param>
    /// <param name="crossfadingTime">The duration (in seconds) for the crossfading effect (default: value of CROSSFADE_TIME constant).</param>
    public void PlayMusic(string streamName, bool crossfade = true, float crossfadingTime = CROSSFADE_TIME) {
        if (MusicBank.TryGetValue(streamName, out AudioStream? stream)) {
            if (CurrentAudioStreamPlayer.Playing && CurrentAudioStreamPlayer.Stream.Equals(stream))
                return;

            AudioStream previousStream = CurrentAudioStreamPlayer.Stream;

            if (crossfade) {
                AudioStreamPlayer nextAudioStreamPlayer = CurrentAudioStreamPlayer.Equals(MainAudioStreamPlayer) ? SecondaryAudioStreamPlayer : MainAudioStreamPlayer;


                if (previousStream is not null)
                    ChangedStream?.Invoke(previousStream, stream);

                nextAudioStreamPlayer.VolumeDb = VOLUME_DB_INAUDIBLE;
                PlayStream(nextAudioStreamPlayer, stream);

                float volume = Mathf.LinearToDb(AudioManager.Instance.GetActualVolumeDbFromBus(nextAudioStreamPlayer.Bus));

                Tween tween = GetTree().CreateTween();
                tween.SetParallel(true);
                tween.TweenProperty(CurrentAudioStreamPlayer, "volume_db", VOLUME_DB_INAUDIBLE, crossfadingTime).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Linear);
                tween.TweenProperty(nextAudioStreamPlayer, "volume_db", volume, crossfadingTime).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Linear);
                tween.Chain().TweenCallback(Callable.From(() => CurrentAudioStreamPlayer = nextAudioStreamPlayer));

                return;
            }

            if (previousStream is not null)
                ChangedStream?.Invoke(previousStream, stream);

            PlayStream(CurrentAudioStreamPlayer, stream);
            return;
        }

        GD.PushError($"MusicManager: The stream with name {streamName} does not exists in the current music bank");
    }

    /// <summary>
    /// Plays a specified AudioStream on a provided AudioStreamPlayer.
    /// </summary>
    /// <param name="audioPlayer">The AudioStreamPlayer object to use for playback.</param>
    /// <param name="stream">The AudioStream object to play.</param>
    /// <remarks>
    /// This function stops any ongoing playback on the `audioPlayer`. It then assigns the provided `stream` to the player's `Stream` property and starts playback using the `Play` method. Finally, it emits a signal (using `EmitSignal`) to notify other parts of the system about the stream start, providing both the `audioPlayer` and `stream`.
    /// </remarks>
    public void PlayStream(AudioStreamPlayer audioPlayer, AudioStream stream) {
        audioPlayer.Stop();
        audioPlayer.Stream = stream;
        audioPlayer.Play();

        StartedStream?.Invoke(audioPlayer, stream);
    }


    /// <summary>
    /// Adds a collection of AudioStream objects to the MusicBank dictionary, with each stream mapped to a unique name.
    /// </summary>
    /// <param name="streams">The Dictionary containing key-value pairs where the key is a string name and the value is an AudioStream object.</param>
    /// <remarks>
    /// This function iterates through each key-value pair in the provided `streams` dictionary. For each pair, it calls the `AddStreamToMusicBank(stream, name)` overload to add the individual AudioStream (`stream`) with the corresponding name (`name`) to the MusicBank.
    /// </remarks>
    public void AddStreamsToMusicBank(Dictionary<string, AudioStream> streams) {
        foreach ((string name, AudioStream stream) in streams)
            AddStreamToMusicBank(stream, name);
    }


    /// <summary>
    /// Adds a single AudioStream object to the MusicBank dictionary with a specified name.
    /// </summary>
    /// <param name="stream">The AudioStream object to add.</param>
    /// <param name="name">The unique name to associate with the AudioStream in the MusicBank.</param>
    /// <remarks>
    /// This function adds the provided `stream` to the MusicBank dictionary using the given `name` as the key. It then emits a signal (using `EmitSignal`) to notify other parts of the system about the addition, providing both the `stream` and `name` as arguments.
    /// </remarks>
    public void AddStreamToMusicBank(AudioStream stream, string name) {
        MusicBank.Add(name, stream);
        AddedMusicToBank?.Invoke(name, stream);
    }

    /// <summary>
    /// Removes AudioStream objects from the MusicBank dictionary based on a provided list of names.
    /// </summary>
    /// <param name="names">An array of strings containing the names of the AudioStream objects to remove.</param>
    /// <remarks>
    /// This function iterates through the `names` array. For each name, it delegates the removal task to the `RemoveStreamMusicFromBank(string name)` overload, ensuring consistent behavior.
    /// </remarks>
    public void RemoveStreamsFromBank(string[] names) {
        names.ToList().ForEach(RemoveStreamMusicFromBank);
    }

    /// <summary>
    /// Attempts to remove an AudioStream object from the MusicBank dictionary based on its name.
    /// </summary>
    /// <param name="name">The name of the AudioStream object to remove.</param>
    /// <remarks>
    /// This function attempts to remove a key-value pair from the MusicBank dictionary where the key matches the provided `name`. If the removal is successful (returns true), it emits a signal (using `EmitSignal`) to notify other parts of the system about the removal, providing the `name` as an argument.
    /// </remarks>
    public void RemoveStreamMusicFromBank(string name) {
        if (MusicBank.Remove(name))
            RemovedMusicFromBank?.Invoke(name);
    }

    /// <summary>
    /// Attempts to remove an AudioStream object from the MusicBank dictionary by searching for its value.
    /// </summary>
    /// <param name="stream">The AudioStream object to remove.</param>
    /// <remarks>
    /// This function uses `FirstOrDefault` to find a key-value pair in the MusicBank where the value (AudioStream) matches the provided `stream` object. If a match is found, it extracts the key (`entry.Key`) and calls the `RemoveStreamMusicFromBank(string name)` overload to remove the entry using the key. This allows removal by value (AudioStream) instead of relying solely on names.
    /// </remarks>
    public void RemoveStreamMusicFromBank(AudioStream stream) {
        KeyValuePair<string, AudioStream> entry = MusicBank.FirstOrDefault(music => music.Value.Equals(stream));

        if (entry.Key is not null)
            RemoveStreamMusicFromBank(entry.Key);
    }

    private void CreateAudioStreamPlayers() {
        MainAudioStreamPlayer = new() { Name = "MainAudioStreamPlayer", Bus = "Music", Autoplay = false };
        SecondaryAudioStreamPlayer = new() { Name = "SecondaryAudioStreamPlayer", Bus = "Music", Autoplay = false };

        CurrentAudioStreamPlayer = MainAudioStreamPlayer;

        AddChild(MainAudioStreamPlayer);
        AddChild(SecondaryAudioStreamPlayer);

        MainAudioStreamPlayer.Connect(AudioStreamPlayer.SignalName.Finished, Callable.From(() => OnFinishedAudioStreamPlayer(MainAudioStreamPlayer)));
        SecondaryAudioStreamPlayer.Connect(AudioStreamPlayer.SignalName.Finished, Callable.From(() => OnFinishedAudioStreamPlayer(SecondaryAudioStreamPlayer)));
    }

    private void OnFinishedAudioStreamPlayer(AudioStreamPlayer audioPlayer) {
        FinishedStream?.Invoke(audioPlayer, audioPlayer.Stream);
    }

}