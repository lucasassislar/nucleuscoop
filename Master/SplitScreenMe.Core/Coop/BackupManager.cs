using Nucleus.Gaming.Util;
using SplitScreenMe.Core.IO;
using System.Collections.Generic;
using System.IO;

namespace SplitScreenMe.Core {
    /// <summary>
    /// Manages backing up game files/save data before modifying it for a play session
    /// </summary>
    public class BackupManager {
        private List<BackupFile> backupFiles;

        public BackupManager() {
        }

        /// <summary>
        /// Begins a game session for backup
        /// </summary>
        /// <param name="game"></param>
        public void BeginBackup(HandlerData game) {
            string appData = ApplicationUtil.GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GameID);
            Directory.CreateDirectory(gamePath);

            backupFiles = new List<BackupFile>();
        }

        /// <summary>
        /// Backups a file at the specified path for later retrieval
        /// </summary>
        /// <param name="game"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public BackupFile BackupFile(HandlerData game, string path) {
            string appData = ApplicationUtil.GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GameID);
            string destination = Path.Combine(gamePath, Path.GetFileName(path));

            if (!File.Exists(path)) {
                if (File.Exists(destination)) {
                    // we fucked up and the backup exists? maybe, so restore
                    File.Copy(destination, path);
                }
            } else {
                if (File.Exists(destination)) {
                    File.Delete(destination);
                }
                File.Copy(path, destination);
            }

            BackupFile bkp = new BackupFile(path, destination);
            backupFiles.Add(bkp);

            return bkp;
        }

        /// <summary>
        /// Do a backup revert
        /// </summary>
        /// <param name="game"></param>
        public void ExecuteBackup(HandlerData game) {
            // we didnt backup anything
            if (backupFiles == null) {
                return;
            }

            string appData = ApplicationUtil.GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GameID);

            for (int i = 0; i < backupFiles.Count; i++) {
                BackupFile bkp = backupFiles[i];
                if (File.Exists(bkp.BackupPath)) {
                    File.Delete(bkp.Source);
                    File.Move(bkp.BackupPath, bkp.Source);
                }
            }
        }
    }
}
