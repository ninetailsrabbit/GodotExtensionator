using Extensionator;
using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack {
    public class SavedGameResource : Resource {

        public static readonly string DefaultPath = OS.GetUserDataDir();

        [Export] public string Filename = string.Empty;
        [Export] public string VersionControl = (string)ProjectSettings.GetSetting("application/config/version");
        [Export] public string EngineVersion = Engine.GetVersionInfo().ToString();
        [Export] public string Device = OS.GetModelName();
        [Export] public string Platform = OS.GetName();
        [Export] public string LastDatetime = string.Empty;
        [Export] public double Timestamp;
        [Export] public GameSettingsResource GameSettings = new();

        public void UpdateLastDatetime() {
            Dictionary datetime = Time.GetDatetimeDictFromSystem();
            LastDatetime = $"{datetime["day"].ToString().PadZeros(2)}/" +
                               $"{datetime["month"].ToString().PadZeros(2)}/" +
                               $"{datetime["year"].ToString().PadZeros(2)} " +
                               $"{datetime["hour"].ToString().PadZeros(2)}:" +
                               $"{datetime["minute"].ToString().PadZeros(2)}";

            Timestamp = Time.GetUnixTimeFromSystem();
        }

        public Error WriteSavegame(string? newFilename = null) {
            EngineVersion = $"Godot {Engine.GetVersionInfo()}";
            Device = OS.GetModelName();
            Platform = OS.GetName();

            if (Filename.IsEmpty()) {
                if (string.IsNullOrEmpty(newFilename)) {
                    GD.PushError($"SavedGame: To write this resource for the first time needs a valid filename {newFilename}, the write operation was aborted");
                    return Error.CantCreate;
                }

                Filename = newFilename.GetBaseName();
            }

            UpdateLastDatetime();

            return ResourceSaver.Save(this, GetSavePath(Filename));
        }

        public Error Delete() {
            Error error = DirAccess.RemoveAbsolute(GetSavePath(Filename));

            if (error != Error.Ok)
                GD.PushError($"SavedGame: An error happened trying to delete the file {Filename} with code {error}");

            return error;
        }

        public static string GetSavePath(string filename) {
            return $"{DefaultPath}/{filename.GetBaseName()}.{GetSaveExtension()}";
        }

        public static string GetSaveExtension() {
            return OS.IsDebugBuild() ? "tres" : "res";
        }
    }
}
