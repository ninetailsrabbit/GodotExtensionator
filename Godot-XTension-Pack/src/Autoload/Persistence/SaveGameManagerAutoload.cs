using Godot;

namespace Godot_XTension_Pack {
    public partial class SaveGameManagerAutoload : Node {

        #region Events

        public delegate void CreatedSavegameEventHandler(SavedGameResource savedGame);
        public event CreatedSavegameEventHandler? CreatedSavegame;

        public delegate void WritedSavegameEventHandler(SavedGameResource savedGame);
        public event WritedSavegameEventHandler? WritedSavegame;

        public delegate void RemovedSavegameEventHandler(string filename);
        public event RemovedSavegameEventHandler? RemovedSavegame;

        public delegate void ErrorCreatingSavegameEventHandler(string filename, int error);
        public event ErrorCreatingSavegameEventHandler? ErrorCreatingSavegame;

        public delegate void ErrorWritingSavegameEventHandler(string filename, int error);
        public event ErrorWritingSavegameEventHandler? ErrorWritingSavegame;

        public delegate void ErrorDeletingSavegameEventHandler(string filename, int error);
        public event ErrorDeletingSavegameEventHandler? ErrorDeletingSavegame;

        #endregion

        public Dictionary<string, SavedGameResource> ListOfSavedGames = [];
        public SavedGameResource? CurrentSavedGame;

        public static Dictionary<string, SavedGameResource> ReadUserSavedGames() {
            string[] validExtensions = [SavedGameResource.GetSaveExtension()];

            Dictionary<string, SavedGameResource> savedGames = [];
            DirAccess directory = DirAccess.Open(SavedGameResource.DefaultPath);

            if (directory is not null) {
                directory.ListDirBegin();
                string filename = directory.GetNext();

                while (filename.Length > 0) {
                    if (!directory.CurrentIsDir() && validExtensions.Contains(filename.GetExtension())) {

                        if (LoadSaveGame<SavedGameResource>(filename.GetBaseName()) is SavedGameResource savedGame)
                            savedGames.Add(savedGame.Filename, savedGame);
                    }

                    filename = directory.GetNext();
                }

                directory.ListDirEnd();
            }

            return savedGames;
        }

        public void CreateNewSave(string filename) {
            SavedGameResource newSavedGame = new();
            Error error = newSavedGame.WriteSavegame(filename);

            if (error.Equals(Error.Ok))
                CreatedSavegame?.Invoke(newSavedGame);
            else
                ErrorCreatingSavegame?.Invoke(filename, (int)error);
        }

        public void WriteCurrentSave() {
            if (CurrentSavedGame is not null) {
                Error error = CurrentSavedGame.WriteSavegame();

                if (error.Equals(Error.Ok))
                    WritedSavegame?.Invoke(CurrentSavedGame);

                else
                    ErrorWritingSavegame?.Invoke(CurrentSavedGame.Filename, (int)error);
            }
        }

        public void RemoveSave(SavedGameResource save) {
            RemoveSave(save.Filename);
        }
        public void RemoveSave(string filename) {
            if (ListOfSavedGames.TryGetValue(filename, out var save)) {
                Error error = save.Delete();

                if (error.Equals(Error.Ok)) {
                    ListOfSavedGames.Remove(filename);
                    RemovedSavegame?.Invoke(filename);
                }
                else {
                    ErrorDeletingSavegame?.Invoke(filename, (int)error);
                }

            }
        }

        public static T? LoadSaveGame<T>(string filename) where T : SavedGameResource {
            if (SaveExists(filename))
                return GD.Load<T>(SavedGameResource.GetSavePath(filename));

            return null;
        }

        public static bool SaveExists(string filename) => ResourceLoader.Exists(SavedGameResource.GetSavePath(filename));

    }
}
