using Ionic.Zip;
using Newtonsoft.Json;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.IO;
using Nucleus.Gaming.Properties;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Nucleus.Gaming.Coop.IO;
using Nucleus.Gaming.Coop.Handler;

namespace Nucleus.Gaming.Coop {
    /// <summary>
    /// Manages games information, so we can know what games are supported 
    /// and how to support it
    /// </summary>
    public class GameManager {
        private static GameManager instance;

        private UserProfile user;
        private List<BackupFile> backupFiles;
        private string error;
        private PackageManager repoManager;

        private GameMetadataManager metadataManager;
        private ModuleManager moduleManager;

        public string Error { get { return error; } }
        public ModuleManager ModuleManager { get { return moduleManager; } }

        /// <summary>
        /// Manages getting the name of games
        /// </summary>
        public GameMetadataManager MetadataManager { get { return metadataManager; } }

        public UserProfile User { get { return user; } }
        public PackageManager RepoManager { get { return repoManager; } }

        public static GameManager Instance { get { return instance; } }

        public GameManager() {
            instance = this;
            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOrderedEnumerable<KeyValuePair<string, List<UserGameInfo>>> GetInstalledGamesOrdered() {
            List<UserGameInfo> games = user.Games;
            Dictionary<string, List<UserGameInfo>> allGames = new Dictionary<string, List<UserGameInfo>>();

            for (int i = 0; i < games.Count; i++) {
                UserGameInfo game = games[i];
                if (!game.IsGamePresent()) {
                    continue;
                }

                List<UserGameInfo> allSame;
                if (!allGames.TryGetValue(game.GameID, out allSame)) {
                    allSame = new List<UserGameInfo>();
                    allGames.Add(game.GameID, allSame);
                }

                allSame.Add(game);
            }

            // <3 linq
            return allGames.OrderBy(c => GameManager.Instance.MetadataManager.GetGameName(c.Key));
        }

        /// <summary>
        /// Tests if there's any game with the named exe
        /// </summary>
        /// <param name="gameExe"></param>
        /// <returns></returns>
        public bool AnyGame(string gameExe) {
            return user.InstalledHandlers.Any(c => string.Equals(c.ExeName.ToLower(), gameExe, StringComparison.OrdinalIgnoreCase) ||
                                                    string.Equals(c.ExeName.ToLower() + ".exe", gameExe, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Tries to find the game
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public GameHandlerMetadata GetGame(string exePath) {
            string lower = exePath.ToLower();
            string fileName = Path.GetFileName(exePath).ToLower();
            string dir = Path.GetDirectoryName(exePath);

            var possibilities = user.InstalledHandlers.Where(c => string.Equals(c.ExeName, fileName, StringComparison.OrdinalIgnoreCase));

            foreach (GameHandlerMetadata game in possibilities) {
                // check if the Context matches
                string[] context = game.ExeContext;
                bool notAdd = false;
                if (context != null) {
                    for (int j = 0; j < context.Length; j++) {
                        string con = Path.Combine(dir, context[j]);
                        if (!File.Exists(con) &&
                            !Directory.Exists(con)) {
                            notAdd = true;
                            break;
                        }
                    }
                }

                if (notAdd) {
                    continue;
                }

                // search for the same exe on the user profile
                return game;
            }

            return null;
        }

        /// <summary>
        /// Returns all the possible games with the exe path
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public List<GameHandlerMetadata> GetAllHandlers(string exePath) {
            string lower = exePath.ToLower();
            string fileName = Path.GetFileName(exePath).ToLower();
            string dir = Path.GetDirectoryName(exePath);

            var possibilities = user.InstalledHandlers.Where(c => string.Equals(c.ExeName.ToLower(), fileName, StringComparison.OrdinalIgnoreCase)
                                                                    || string.Equals(c.ExeName.ToLower() + ".exe", fileName, StringComparison.OrdinalIgnoreCase));
            List<GameHandlerMetadata> games = new List<GameHandlerMetadata>();

            foreach (GameHandlerMetadata game in possibilities) {
                // check if the Context matches
                string[] context = game.ExeContext;
                bool notAdd = false;
                if (context != null) {
                    for (int j = 0; j < context.Length; j++) {
                        string con = Path.Combine(dir, context[j]);
                        if (!File.Exists(con) &&
                            !Directory.Exists(con)) {
                            notAdd = true;
                            break;
                        }
                    }
                }

                if (notAdd) {
                    continue;
                }

                // search for the same exe on the user profile
                games.Add(game);
            }

            return games;
        }

        /// <summary>
        /// Tries adding a game to the collection with the provided IGameInfo
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public UserGameInfo TryAddGame(string exePath, GameHandlerMetadata metadata) {
            string lower = exePath.ToLower();
            string dir = Path.GetDirectoryName(exePath);

            if (user.Games.Any(c => c.ExePath.ToLower() == lower)) {
                return null;
            }

            Log.WriteLine($"Added game: {metadata.Title}, on path: {exePath}");
            UserGameInfo uinfo = new UserGameInfo();
            uinfo.InitializeDefault(metadata, exePath);
            user.Games.Add(uinfo);
            user.Save();

            return uinfo;

        }

        /// <summary>
        /// Tries adding a game to the collection with the provided executable path
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public UserGameInfo TryAddGame(string exePath) {
            string lower = exePath.ToLower();
            string dir = Path.GetDirectoryName(exePath);

            var possibilities = GetAllHandlers(exePath);

            foreach (GameHandlerMetadata metadata in possibilities) {
                // check if the Context matches
                string[] context = metadata.ExeContext;
                bool notAdd = false;
                if (context != null) {
                    for (int j = 0; j < context.Length; j++) {
                        string con = Path.Combine(dir, context[j]);
                        if (!File.Exists(con) &&
                            !Directory.Exists(con)) {
                            notAdd = true;
                            break;
                        }
                    }
                }

                if (notAdd ||
                    user.Games.Any(c => c.ExePath.ToLower() == lower)) {
                    continue;
                }

                Log.WriteLine($"Found game: {metadata.Title}, on path: {exePath}");
                UserGameInfo uinfo = new UserGameInfo();
                uinfo.InitializeDefault(metadata, exePath);
                user.Games.Add(uinfo);
                user.Save();

                return uinfo;
            }

            return null;
        }

        #region Initialize

        public static bool IsGameTasksApp() {
            string entryApp = Assembly.GetEntryAssembly().Location;
            return entryApp.ToLower().Contains("startgame");
        }

        public static string GetAppDataPath() {
#if ALPHA
            string entryApp = Assembly.GetEntryAssembly().Location;
            string local = Path.GetDirectoryName(entryApp);

            if (IsGameTasksApp()) {
                // game tasks application, move to correct folder
                return Path.Combine(local, "..", "data");
            } else {
                return Path.Combine(local, "data");
            }
#else
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Nucleus Coop");
#endif
        }

        public string GetPackageTmpPath() {
            return Path.Combine(GetAppDataPath(), "pkg\\tmp");
        }

        public string GetInstalledPackagePath() {
            return Path.Combine(GetAppDataPath(), "pkg\\installed");
        }

        public string GetJsGamesPath() {
            return Path.Combine(GetAppDataPath(), "games");
        }

        protected string GetUserProfilePath() {
            return Path.Combine(GetAppDataPath(), "userprofile.json");
        }

        public void BeginBackup(HandlerData game) {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GameID);
            Directory.CreateDirectory(gamePath);

            backupFiles = new List<BackupFile>();
        }

        public static string GetTempFolder(HandlerData game) {
            string appData = GetAppDataPath();
            return Path.Combine(appData, "temp", game.GameID);
        }


        public static string GetTempFolder(string gameId) {
            string appData = GetAppDataPath();
            return Path.Combine(appData, gameId);
        }

        public BackupFile BackupFile(HandlerData game, string path) {
            string appData = GetAppDataPath();
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

        public void ExecuteBackup(HandlerData game) {
            // we didnt backup anything
            if (backupFiles == null) {
                return;
            }

            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GameID);

            for (int i = 0; i < backupFiles.Count; i++) {
                BackupFile bkp = backupFiles[i];
                if (File.Exists(bkp.BackupPath)) {
                    File.Delete(bkp.Source);
                    File.Move(bkp.BackupPath, bkp.Source);
                }
            }
        }

        private void LoadUser() {
            string userProfile = GetUserProfilePath();

            if (File.Exists(userProfile)) {
                try {
                    user = new UserProfile(userProfile);
                    user.Load();

                    if (user.Games == null || user.InstalledHandlers == null) {
                        // json doesn't save empty lists, and user didn't add any game
                        user.InitializeDefault();
                    } else {
                        // delete invalid games
                        // we will rebuild the gamedb later, so commented for now
                        //RebuildGameDb();
                    }
                } catch {
                    makeDefaultUserFile();
                }
            } else {
                makeDefaultUserFile();
            }
        }

        public void RebuildGameDb() {
            string installedDir = GetInstalledPackagePath();
            DirectoryInfo dir = new DirectoryInfo(installedDir);
            if (!dir.Exists) {
                // nothing installed
                return;
            }

            DirectoryInfo[] installedHandlers = dir.GetDirectories();

            user.InstalledHandlers = new List<GameHandlerMetadata>();
            for (int i = 0; i < installedHandlers.Length; i++) {
                DirectoryInfo handlerDir = installedHandlers[i];

                // read the game's json
                string metadataPath = Path.Combine(handlerDir.FullName, repoManager.InfoFileName + repoManager.JsonFormat);
                GameHandlerMetadata metadata = repoManager.ReadMetadataFromFile(metadataPath);
                user.InstalledHandlers.Add(metadata);
            }

            //if (installedHandlers.Length == 0)
            //{
            //    user.Games.Clear(); // TODO: better clear
            //}

            user.Save();
        }

        private void makeDefaultUserFile() {
            string userProfile = GetUserProfilePath();
            string split = Path.GetDirectoryName(userProfile);
            if (!Directory.Exists(split)) {
                Directory.CreateDirectory(split);
            }

            user = new UserProfile(userProfile);
            user.InitializeDefault();

            user.Save(userProfile);
        }

        private void Initialize() {
            metadataManager = new GameMetadataManager();

            string appData = GetAppDataPath();
            Directory.CreateDirectory(appData);

            // create all default directories
            Directory.CreateDirectory(GetInstalledPackagePath());
            Directory.CreateDirectory(GetPackageTmpPath());

            repoManager = new PackageManager();
            moduleManager = new ModuleManager();

            LoadUser();

            // TODO: Right now, build the DB each time we start up, later change
            RebuildGameDb();
        }
        #endregion

        public void Play(GameHandler handler) {
            // Start the Play method in another thread, so the
            // handler can update while it's still loading
            error = null;
            ThreadPool.QueueUserWorkItem(play, handler);
        }

        private void play(object state) {
#if RELEASE
            try {
                ((GameHandler)state).Play();
            } catch (Exception ex) {
                error = ex.Message;
                try {
                    // try to save the exception
                    Log.Instance.LogExceptionFile(ex);
                } catch {
                    error = "We failed so hard we failed while trying to record the reason we failed initially. Sorry.";
                    return;
                }
            }
#else
            ((GameHandler)state).Play();
#endif
        }
    }
}
