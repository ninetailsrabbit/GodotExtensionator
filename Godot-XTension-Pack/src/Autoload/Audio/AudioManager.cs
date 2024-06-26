using Extensionator;
using Godot;

namespace Godot_XTension_Pack;

public sealed class AudioManager : SingletonBase<AudioManager> {
    public List<string> AvailableBuses = EnumerateAvailableBuses();
    public const int MasterBusIndex = 0;

    /// <summary>
    /// Checks if a bus with the specified name exists.
    /// </summary>
    /// <param name="bus">The name of the audio bus to check.</param>
    /// <returns>True if the bus exists, false otherwise.</returns>
    /// <remarks>
    /// This function uses the `AvailableBuses` list (presumably containing known bus names) to determine if the provided `bus` name is present. It utilizes the `Contains` method for efficient lookup.
    /// </remarks>
    public bool BusExists(string bus) => AvailableBuses.Contains(bus);

    /// <summary>
    /// Checks if a bus with the specified index exists.
    /// </summary>
    /// <param name="bus">The index of the audio bus to check.</param>
    /// <returns>True if the bus exists, false otherwise.</returns>
    /// <remarks>
    /// This function retrieves the bus name for the given `bus` index using `AudioServer.GetBusName`. It then calls the other `BusExists` function (overloaded for string) to check if the retrieved name exists in the `AvailableBuses` list.
    /// </remarks>
    public bool BusExists(int bus) => AvailableBuses.Contains(AudioServer.GetBusName(bus));

    /// <summary>
    /// Changes the volume of an audio bus by index.
    /// </summary>
    /// <param name="bus">The name of the audio bus to adjust.</param>
    /// <param name="volume">The new volume level (0.0 to 1.0) for the bus.</param>
    /// <remarks>
    /// If the bus index is -1 (meaning the bus doesn't exist), it logs an error message and exits. Otherwise, it converts the linear `volume_value` to logarithmic decibel (dB) using `Mathf.LinearToDb` and sets the bus volume using `AudioServer.SetBusVolumeDb`.
    /// </remarks>
    public static void ChangeVolume(int bus, float volume) {
        if (bus == -1) {
            GD.PushError($"AudioManager: The bus with the name {bus} does not exists in this project");
            return;
        }

        AudioServer.SetBusVolumeDb(bus, Mathf.LinearToDb(volume));
    }

    /// <summary>
    /// Changes the volume of an audio bus by name.
    /// </summary>
    /// <param name="bus">The name of the audio bus to adjust.</param>
    /// <param name="volume">The new volume level (0.0 to 1.0) for the bus.</param>
    /// <remarks>
    /// This function first gets the index of the bus with the provided `bus` using `AudioServer.GetBusIndex`. If the bus index is -1 (meaning the bus doesn't exist), it logs an error message and exits. Otherwise, it converts the linear `volume_value` to logarithmic decibel (dB) using `Mathf.LinearToDb` and sets the bus volume using `AudioServer.SetBusVolumeDb`.
    /// </remarks>
    public static void ChangeVolume(string bus, float volume) {
        ChangeVolume(AudioServer.GetBusIndex(bus), volume);
    }


    /// <summary>
    /// Gets the actual volume in decibels (dB) of an audio bus by name.
    /// </summary>
    /// <param name="bus">The name of the audio bus to query.</param>
    /// <returns>The actual volume level in dB (0.0f if the bus doesn't exist).</returns>
    /// <remarks>
    /// This function first retrieves the index of the bus with the provided `bus` using `AudioServer.GetBusIndex`. If the bus index is -1 (not found), it logs an error message and returns 0.0f. Otherwise, it delegates the actual volume retrieval to the overloaded `GetActualVolumeDbFromBus` function with the bus index for efficiency.
    /// </remarks>
    public float GetActualVolumeDbFromBus(string bus) {
        int busIndex = AudioServer.GetBusIndex(bus);

        if (busIndex == -1) {
            GD.PushError($"AudioManager: The bus with the name {bus} does not exists in this project");
            return 0.0f;
        }

        return GetActualVolumeDbFromBus(busIndex);
    }

    /// <summary>
    /// Gets the actual volume in decibels (dB) of an audio bus by index (internal function).
    /// </summary>
    /// <param name="bus">The index of the audio bus to query.</param>
    /// <returns>The actual volume level in dB.</returns>
    /// <remarks>
    /// This function assumes the bus index is valid (obtained from other functions) and directly uses `AudioServer.GetBusVolumeDb` to retrieve the volume in dB. It then converts the dB value back to linear scale using `Mathf.DbToLinear` and returns the result.
    /// </remarks>
    public static float GetActualVolumeDbFromBus(int bus) => Mathf.DbToLinear(AudioServer.GetBusVolumeDb(bus));

    /// <summary>
    /// Enumerates all available audio buses in the project and returns their names as an array of strings.
    /// </summary>
    /// <returns>An array of strings containing the names of all available audio buses.</returns>
    public static List<string> EnumerateAvailableBuses()
        => Enumerable.Range(0, AudioServer.BusCount).Select(AudioServer.GetBusName).ToList();

    public void UpdateAvailableBuses() {
        AvailableBuses = EnumerateAvailableBuses();
    }
    /// <summary>
    /// Checks if the provided AudioStream is configured to loop playback.
    /// </summary>
    /// <param name="stream">The AudioStream to check.</param>
    /// <returns>True if the stream is set to loop, false otherwise.</returns>
    /// <remarks>
    /// This function uses a series of type checks to determine the specific type of the `stream` object (e.g., AudioStreamMP3, AudioStreamOggVorbis, AudioStreamWav). Based on the type, it accesses the appropriate property to check the loop setting. Here's a breakdown:
    ///   - For AudioStreamMP3: Checks the `Loop` property of the casted `mp3Stream`.
    ///   - For AudioStreamOggVorbis: Checks the `Loop` property of the casted `oggStream`.
    ///   - For AudioStreamWav: Checks if the `LoopMode` property of the casted `wavStream` is equal to `AudioStreamWav.LoopModeEnum.Disabled`. If not disabled, it considers it looping.
    /// If none of the type checks match, the function returns `false` by default.
    /// </remarks>
    public static bool IsStreamLooped(AudioStream stream) {
        if (stream is AudioStreamMP3 mp3Stream)
            return mp3Stream.Loop;


        if (stream is AudioStreamOggVorbis oggStream)
            return oggStream.Loop;


        if (stream is AudioStreamWav wavStream)
            return wavStream.LoopMode.Equals(AudioStreamWav.LoopModeEnum.Disabled);

        return false;
    }

    /// <summary>
    /// Checks if the specified audio bus (by index) is currently muted.
    /// </summary>
    /// <param name="bus">The index of the audio bus to check (default: MasterBusIndex).</param>
    /// <returns>True if the bus is muted, false otherwise.</returns>
    /// <remarks>
    /// This function uses `AudioServer.IsBusMute` directly with the provided `bus` index (or the default `MasterBusIndex` if none is specified) to determine the mute state.
    /// </remarks>
    public static bool IsMuted(int bus = MasterBusIndex) => AudioServer.IsBusMute(bus);


    /// <summary>
    /// Checks if the specified audio bus (by name) is currently muted.
    /// </summary>
    /// <param name="bus">The name of the audio bus to check (default: "Master").</param>
    /// <returns>True if the bus is muted, false otherwise.</returns>
    /// <remarks>
    /// This function first retrieves the index of the bus with the provided `bus` name using `AudioServer.GetBusIndex`. If the bus is found, it delegates the mute check to the overloaded `IsMuted(int)` function using the retrieved index for efficiency.
    /// </remarks>
    public static bool IsMuted(string bus = "Master") => AudioServer.IsBusMute(AudioServer.GetBusIndex(bus));

    /// <summary>
    /// Mutes or unmutes an audio bus by index.
    /// </summary>
    /// <param name="bus">The index of the audio bus to mute/unmute.</param>
    /// <param name="muteFlag">True to mute the bus, false to unmute (default: true).</param>
    /// <remarks>
    /// This function directly calls `AudioServer.SetBusMute` with the provided `bus` index and `muteFlag` to control the mute state of the bus.
    /// </remarks>
    public static void MuteBus(int bus, bool muteFlag = true) {
        AudioServer.SetBusMute(bus, muteFlag);
    }

    /// <summary>
    /// Mutes or unmutes an audio bus by name.
    /// </summary>
    /// <param name="bus">The name of the audio bus to mute/unmute.</param>
    /// <param name="muteFlag">True to mute the bus, false to unmute (default: true).</param>
    /// <remarks>
    /// This function directly calls `AudioServer.SetBusMute` with the provided `bus` index and `muteFlag` to control the mute state of the bus.
    /// </remarks>
    public static void MuteBus(string bus, bool muteFlag = true) {
        AudioServer.SetBusMute(AudioServer.GetBusIndex(bus), muteFlag);
    }
}