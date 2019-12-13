using SplitScreenMe.Core.Handler;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Package;
using Nucleus.Gaming.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace SplitScreenMe.Core {
    /// <summary>
    /// Manages games information, so we can know what games are supported 
    /// and how to support it
    /// </summary>
    public class GameManager {
        /// <summary>
        /// Result Error message from last play's session
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// A reference to the manager of modules (modules handle a part of splitscreen initialization)
        /// </summary>
        public ModuleManager ModuleManager { get; private set; }

        /// <summary>
        /// Manages getting the name of games
        /// </summary>
        public GameMetadataManager MetadataManager { get; private set; }

        /// <summary>
        /// The current user's (the one using the software) options
        /// </summary>
        public UserProfile User { get; private set; }

        /// <summary>
        /// Package Manager installs and uninstalls packages
        /// </summary>
        public PackageManager PackageManager { get; private set; }

        /// <summary>
        /// Manages backing up game files/save data before modifying it for a play session
        /// </summary>
        public BackupManager BackupManager { get; private set; }

        /// <summary>
        /// Singleton reference
        /// </summary>
        public static GameManager Instance { get; private set; }

        public GameManager() {
            Instance = this;
            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOrderedEnumerable<KeyValuePair<string, List<UserGameInfo>>> GetInstalledGamesOrdered() {
            List<UserGameInfo> games = User.Games;
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
            return User.InstalledHandlers.Any(c => string.Equals(c.ExeName.ToLower(), gameExe, StringComparison.OrdinalIgnoreCase) ||
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

            var possibilities = User.InstalledHandlers.Where(c => string.Equals(c.ExeName, fileName, StringComparison.OrdinalIgnoreCase));

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

            var possibilities = User.InstalledHandlers.Where(c => string.Equals(c.ExeName.ToLower(), fileName, StringComparison.OrdinalIgnoreCase)
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

            if (User.Games.Any(c => c.ExePath.ToLower() == lower)) {
                return null;
            }

            Log.WriteLine($"Added game: {metadata.Title}, on path: {exePath}");
            UserGameInfo uinfo = new UserGameInfo();
            uinfo.InitializeDefault(metadata, exePath);
            User.Games.Add(uinfo);
            User.Save();

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
                    User.Games.Any(c => c.ExePath.ToLower() == lower)) {
                    continue;
                }

                Log.WriteLine($"Found game: {metadata.Title}, on path: {exePath}");
                UserGameInfo uinfo = new UserGameInfo();
                uinfo.InitializeDefault(metadata, exePath);
                User.Games.Add(uinfo);
                User.Save();

                return uinfo;
            }

            return null;
        }

        #region Initialize

        public string GetPackageTmpPath() {
            return Path.Combine(ApplicationUtil.GetAppDataPath(), "pkg\\tmp");
        }

        public string GetInstalledPackagePath() {
            return Path.Combine(ApplicationUtil.GetAppDataPath(), "pkg\\installed");
        }

        public string GetJsGamesPath() {
            return Path.Combine(ApplicationUtil.GetAppDataPath(), "games");
        }

        protected string GetUserProfilePath() {
            return Path.Combine(ApplicationUtil.GetAppDataPath(), "userprofile.json");
        }

        public static string GetTempFolder(HandlerData game) {
            string appData = ApplicationUtil.GetAppDataPath();
            return Path.Combine(appData, "temp", game.GameID);
        }

        public static string GetTempFolder(string gameId) {
            string appData = ApplicationUtil.GetAppDataPath();
            return Path.Combine(appData, gameId);
        }

        private void LoadUser() {
            string userProfile = GetUserProfilePath();

            if (File.Exists(userProfile)) {
                try {
                    User = new UserProfile(userProfile);
                    User.Load();

                    if (User.Games == null || User.InstalledHandlers == null) {
                        // json doesn't save empty lists, and user didn't add any game
                        User.InitializeDefault();
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

            User.InstalledHandlers = new List<GameHandlerMetadata>();
            for (int i = 0; i < installedHandlers.Length; i++) {
                DirectoryInfo handlerDir = installedHandlers[i];

                // read the game's json
                string metadataPath = Path.Combine(handlerDir.FullName, PackageManager.InfoFileName + PackageManager.JsonFormat);
                GameHandlerMetadata metadata = PackageManager.ReadMetadataFromFile(metadataPath);
                User.InstalledHandlers.Add(metadata);
            }

            //if (installedHandlers.Length == 0)
            //{
            //    user.Games.Clear(); // TODO: better clear
            //}

            User.Save();
        }

        private void makeDefaultUserFile() {
            string userProfile = GetUserProfilePath();
            string split = Path.GetDirectoryName(userProfile);
            if (!Directory.Exists(split)) {
                Directory.CreateDirectory(split);
            }

            User = new UserProfile(userProfile);
            User.InitializeDefault();

            User.Save(userProfile);
        }

        private void Initialize() {
            MetadataManager = new GameMetadataManager();

            string appData = ApplicationUtil.GetAppDataPath();
            Directory.CreateDirectory(appData);

            // create all default directories
            Directory.CreateDirectory(GetInstalledPackagePath());
            Directory.CreateDirectory(GetPackageTmpPath());

            PackageManager = new PackageManager();
            ModuleManager = new ModuleManager();
            BackupManager = new BackupManager();

            LoadUser();

            // TODO: Right now, build the DB each time we start up, later change
            RebuildGameDb();
        }
        #endregion

        public void Play(GameHandler handler) {
            // Start the Play method in another thread, so the
            // handler can update while it's still loading
            Error = null;
            ThreadPool.QueueUserWorkItem(play, handler);
        }

        private void play(object state) {
#if RELEASE
            try {
                ((GameHandler)state).Play();
            } catch (Exception ex) {
                Error = ex.Message;
                try {
                    // try to save the exception
                    Log.Instance.LogExceptionFile(ex);
                } catch {
                    Error = "We failed so hard we failed while trying to record the reason we failed initially. Sorry.";
                    return;
                }
            }
#else
            ((GameHandler)state).Play();
#endif
        }
    }
}
