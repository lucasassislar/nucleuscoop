using Ionic.Zip;
using Newtonsoft.Json;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.IO;
using Nucleus.Gaming.Properties;
using Nucleus.Gaming.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Nucleus.Gaming.Coop
{
    /// <summary>
    /// Manages games information, so we can know what games are supported 
    /// and how to support it
    /// </summary>
    public class GameManager
    {
        private static GameManager instance;
        private CoopConfigInfo config;

        // object instance so we can thread-safe save the user profile
        private object savingLock = new object();

        private UserProfile user;
        private List<BackupFile> backupFiles;
        private string error;
        private bool isSaving;
        private RepoManager repoManager;

        private Dictionary<string, GenericHandlerData> games;
        private Dictionary<string, GenericHandlerData> gameInfos;
        private GameNameManager nameManager;

        public string Error { get { return error; } }

        public bool IsSaving { get { return isSaving; } }

        /// <summary>
        /// A dictionary containing GameInfos. The key is the game's guid
        /// </summary>
        public Dictionary<string, GenericHandlerData> Games { get { return games; } }
        public Dictionary<string, GenericHandlerData> GameInfos { get { return gameInfos; } }
        public GameNameManager NameManager { get { return nameManager; } }


        public UserProfile User { get { return user; } }
        public RepoManager RepoManager { get { return repoManager; } }
        public CoopConfigInfo Config { get { return config; } }

        public static GameManager Instance { get { return instance; } }

        public GameManager(CoopConfigInfo config)
        {
            this.config = config;
            instance = this;

            Initialize();
            LoadUser();
        }

        /// <summary>
        /// Tests if there's any game with the named exe
        /// </summary>
        /// <param name="gameExe"></param>
        /// <returns></returns>
        public bool AnyGame(string gameExe)
        {
            return user.InstalledHandlers.Any(c => string.Equals(c.HandlerInfo.ExeName, gameExe, StringComparison.OrdinalIgnoreCase));
            //return Games.Values.Any(c => c.ExecutableName == gameExe);
        }

        /// <summary>
        /// Tries to find the game
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public UserInstalledHandler GetGame(string exePath)
        {
            string lower = exePath.ToLower();
            string fileName = Path.GetFileName(exePath).ToLower();
            string dir = Path.GetDirectoryName(exePath);

            //var possibilities = Games.Values.Where(c => string.Equals(c.ExecutableName, fileName, StringComparison.OrdinalIgnoreCase));
            var possibilities = user.InstalledHandlers.Where(c => string.Equals(c.HandlerInfo.ExeName, fileName, StringComparison.OrdinalIgnoreCase));

            //foreach (GenericGameInfo game in possibilities)
            foreach (UserInstalledHandler game in possibilities)
            {
                // check if the Context matches
                string[] context = game.HandlerInfo.ExeContext;
                bool notAdd = false;
                if (context != null)
                {
                    for (int j = 0; j < context.Length; j++)
                    {
                        string con = Path.Combine(dir, context[j]);
                        if (!File.Exists(con) &&
                            !Directory.Exists(con))
                        {
                            notAdd = true;
                            break;
                        }
                    }
                }

                if (notAdd)
                {
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
        public List<UserInstalledHandler> GetAllHandlers(string exePath)
        {
            string lower = exePath.ToLower();
            string fileName = Path.GetFileName(exePath).ToLower();
            string dir = Path.GetDirectoryName(exePath);

            //var possibilities = Games.Values.Where(c => c.ExecutableName == fileName);
            var possibilities = user.InstalledHandlers.Where(c => string.Equals(c.HandlerInfo.ExeName, fileName, StringComparison.OrdinalIgnoreCase));
            List<UserInstalledHandler> games = new List<UserInstalledHandler>();

            foreach (UserInstalledHandler game in possibilities)
            {
                // check if the Context matches
                string[] context = game.HandlerInfo.ExeContext;
                bool notAdd = false;
                if (context != null)
                {
                    for (int j = 0; j < context.Length; j++)
                    {
                        string con = Path.Combine(dir, context[j]);
                        if (!File.Exists(con) &&
                            !Directory.Exists(con))
                        {
                            notAdd = true;
                            break;
                        }
                    }
                }

                if (notAdd)
                {
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
        public UserGameInfo TryAddGame(string exePath, RepoGameHandlerFullInfo game)
        {
            //string lower = exePath.ToLower();

            //// search for the same exe on the user profile
            //if (GameManager.Instance.User.Games.Any(c => c.ExePath.ToLower() == lower))
            //{
            //    return null;
            //}

            //Log.WriteLine($"Found game: {game.GameName}, full path: {exePath}");
            //UserGameInfo uinfo = new UserGameInfo();
            //uinfo.InitializeDefault(game, exePath);
            //GameManager.Instance.User.Games.Add(uinfo);
            //GameManager.Instance.SaveUserProfile();

            //return uinfo;
            return null;
        }

        /// <summary>
        /// Tries adding a game to the collection with the provided executable path
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public UserGameInfo TryAddGame(string exePath)
        {
//            string lower = exePath.ToLower();
//            string fileName = Path.GetFileName(exePath).ToLower();
//            string dir = Path.GetDirectoryName(exePath);

//            var possibilities = Games.Values.Where(c => c.ExecutableName == fileName);

//            foreach (GenericGameInfo game in possibilities)
//            {
//                // check if the Context matches
//                string[] context = game.ExecutableContext;
//                bool notAdd = false;
//                if (context != null)
//                {
//                    for (int j = 0; j < context.Length; j++)
//                    {
//                        string con = Path.Combine(dir, context[j]);
//                        if (!File.Exists(con) &&
//                            !Directory.Exists(con))
//                        {
//                            notAdd = true;
//                            break;
//                        }
//                    }
//                }

//                if (notAdd)
//                {
//                    continue;
//                }

//                // search for the same exe on the user profile
//                if (GameManager.Instance.User.Games.Any(c => c.ExePath.ToLower() == lower))
//                {
//                    continue;
//                }

//#if RELEASE
//                if (game.Debug)
//                {
//                    continue;
//                }
//#endif

//                Log.WriteLine($"Found game: {game.GameName}, full path: {exePath}");
//                UserGameInfo uinfo = new UserGameInfo();
//                uinfo.InitializeDefault(game, exePath);
//                GameManager.Instance.User.Games.Add(uinfo);
//                GameManager.Instance.SaveUserProfile();

//                return uinfo;
//            }

            return null;
        }

        /// <summary>
        /// Extracts the SmartSteamEmu and returns the folder its on
        /// </summary>
        /// <returns></returns>
        public string ExtractSteamEmu(string outputFolder = null)
        {
            string steamEmu;

            if (string.IsNullOrEmpty(outputFolder))
            {
                string app = GetAppDataPath();
                steamEmu = Path.Combine(app, "SteamEmu");
            }
            else
            {
                steamEmu = outputFolder;
            }

            try
            {
                //if (!Directory.Exists(steamEmu))
                {
                    Log.WriteLine("Extracting SmartSteamEmu");

                    Directory.CreateDirectory(steamEmu);
                    using (MemoryStream stream = new MemoryStream(Resources.SmartSteamEmu))
                    {
                        using (ZipFile zip1 = ZipFile.Read(stream))
                        {
                            foreach (ZipEntry e in zip1)
                            {
                                e.Extract(steamEmu, ExtractExistingFileAction.OverwriteSilently);
                            }
                        }
                    }
                }
            }
            catch
            {
                Log.WriteLine("Extraction of SmartSteamEmu failed");
                return string.Empty;
            }

            return steamEmu;
        }

        public void WaitSave()
        {
            while (IsSaving)
            {
            }
        }

        #region Initialize

        private string GetAppDataPath()
        {
#if ALPHA
            string local = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(local, "Data");
#else
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Nucleus Coop");
#endif
        }

        public string GetPackageTmpPath()
        {
            string local = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(local, "pkg\\tmp");
        }

        public string GetInstalledPackagePath()
        {
            string local = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(local, "pkg\\installed");
        }

        public string GetJsGamesPath()
        {
            string local = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(local, "games");
        }

        protected string GetUserProfilePath()
        {
            return Path.Combine(GetAppDataPath(), "userprofile.json");
        }

        //public int Compare(UserGameInfo x, UserGameInfo y)
        //{
        //    if (x.Game == null || y.Game == null)
        //    {
        //        return 0;
        //    }
        //    return x.Game.GameName.CompareTo(y.Game.GameName);
        //}

        public UserGameInfo AddGame(GenericHandlerData game, string exePath)
        {
            UserGameInfo gInfo = new UserGameInfo();
            throw new NotImplementedException();
            //gInfo.InitializeDefault(game, exePath);
            user.Games.Add(gInfo);

            SaveUserProfile();

            return gInfo;
        }

        public void BeginBackup(GenericHandlerData game)
        {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);
            Directory.CreateDirectory(gamePath);

            backupFiles = new List<BackupFile>();
        }

        public IGameHandler MakeHandler(GenericHandlerData game)
        {
            return (IGameHandler)Activator.CreateInstance(game.HandlerType);
        }

        public string GempTempFolder(GenericHandlerData game)
        {
            string appData = GetAppDataPath();
            return Path.Combine(appData, game.GUID);
        }

        public BackupFile BackupFile(GenericHandlerData game, string path)
        {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);
            string destination = Path.Combine(gamePath, Path.GetFileName(path));

            if (!File.Exists(path))
            {
                if (File.Exists(destination))
                {
                    // we fucked up and the backup exists? maybe, so restore
                    File.Copy(destination, path);
                }
            }
            else
            {
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }
                File.Copy(path, destination);
            }

            BackupFile bkp = new BackupFile(path, destination);
            backupFiles.Add(bkp);

            return bkp;
        }

        public void ExecuteBackup(GenericHandlerData game)
        {
            // we didnt backup anything
            if (backupFiles == null)
            {
                return;
            }

            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);

            for (int i = 0; i < backupFiles.Count; i++)
            {
                BackupFile bkp = backupFiles[i];
                if (File.Exists(bkp.BackupPath))
                {
                    File.Delete(bkp.Source);
                    File.Move(bkp.BackupPath, bkp.Source);
                }
            }
        }

        public void SaveUserProfile()
        {
            lock (user.Games)
            {
                //user.Games.Sort(Compare);
            }

            string userProfile = GetUserProfilePath();
            asyncSaveUser(userProfile);
        }

        private void LoadUser()
        {
            string userProfile = GetUserProfilePath();

            if (File.Exists(userProfile))
            {
                try
                {
                    using (FileStream stream = new FileStream(userProfile, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string json = reader.ReadToEnd();
                            user = JsonConvert.DeserializeObject<UserProfile>(json);

                            if (user.Games == null || user.InstalledHandlers == null)
                            {
                                // json doesn't save empty lists, and user didn't add any game
                                user.InitializeDefault();
                            }
                            else
                            {
                                // delete invalid games
                                //for (int i = 0; i < user.Games.Count; i++)
                                //{
                                //    UserGameInfo gameInfo = user.Games[i];
                                //    if (gameInfo.Game == null)
                                //    {
                                //        user.Games.RemoveAt(i);
                                //        i--;
                                //    }
                                //}
                                throw new NotImplementedException();

                                DateTime latest = user.LatestMod;
                                string installedDir = GetInstalledPackagePath();

                                // get all the installed games and see if we need to rebuild the game database
                                DirectoryInfo dir = new DirectoryInfo(installedDir);
                                if (dir.Exists)
                                {
                                    FileInfo[] files = dir.GetFiles();
                                    if (files.Length > 0)
                                    {
                                        FileInfo first = files.OrderBy(c => c.CreationTime).First();
                                        if (first.CreationTime > latest)
                                        {
                                            // rebuild game db
                                            RebuildGameDb();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    makeDefaultUserFile();
                }
            }
            else
            {
                makeDefaultUserFile();
            }
        }

        private void RebuildGameDb()
        {
            string installedDir = GetInstalledPackagePath();
            DirectoryInfo dir = new DirectoryInfo(installedDir);
            FileInfo[] files = dir.GetFiles();

            user.InstalledHandlers.Clear();
            for (int i = 0; i < files.Length; i++)
            {
                string pkgPath = files[i].FullName;
                RepoGameHandlerFullInfo info = repoManager.ReadInfoFromPackageFile(pkgPath);
                UserInstalledHandler handler = new UserInstalledHandler();
                handler.HandlerInfo = info;
                handler.PackagePath = pkgPath;

                user.InstalledHandlers.Add(handler);
            }
            SaveUserProfile();
        }

        

        private void makeDefaultUserFile()
        {
            user = new UserProfile();
            user.InitializeDefault();

            string userProfile = GetUserProfilePath();
            string split = Path.GetDirectoryName(userProfile);
            if (!Directory.Exists(split))
            {
                Directory.CreateDirectory(split);
            }

            saveUser(userProfile);
        }

        private void asyncSaveUser(string path)
        {
            if (!IsSaving)
            {
                isSaving = true;
                Log.WriteLine("> Saving user profile....");
                ThreadPool.QueueUserWorkItem(saveUser, path);
            }
        }

        private void saveUser(object p)
        {
            lock (savingLock)
            {
                try
                {
                    isSaving = true;
                    string path = (string)p;
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            string json = JsonConvert.SerializeObject(user);
                            writer.Write(json);
                            stream.Flush();
                        }
                    }
                    Log.WriteLine("Saved user profile");
                }
                catch { }
                isSaving = false;
            }
        }

        private void Initialize()
        {
            games = new Dictionary<string, GenericHandlerData>();
            gameInfos = new Dictionary<string, GenericHandlerData>();

            nameManager = new GameNameManager();

            string appData = GetAppDataPath();
            Directory.CreateDirectory(appData);

            repoManager = new RepoManager(config);

            // Search for Javascript games-infos
            //string jsfolder = GetJsGamesPath();
            //DirectoryInfo jsFolder = new DirectoryInfo(jsfolder);
            //FileInfo[] files = jsFolder.GetFiles("*.js");
            //for (int i = 0; i < files.Length; i++)
            //{
            //    FileInfo f = files[i];
            //    using (Stream str = f.OpenRead())
            //    {
            //        string ext = Path.GetFileNameWithoutExtension(f.Name);
            //        string pathBlock = Path.Combine(f.Directory.FullName, ext);

            //        GenericGameInfo info = new GenericGameInfo(f.Name, pathBlock, str);
            //        Log.WriteLine("Found game info: " + info.GameName);

            //        games.Add(info.GUID, info);
            //        gameInfos.Add(info.ExecutableName, info);
            //    }
            //}
        }
        #endregion

        public void Play(IGameHandler handler)
        {
            // Start the Play method in another thread, so the
            // handler can update while it's still loading
            error = null;
            ThreadPool.QueueUserWorkItem(play, handler);
        }

        private void play(object state)
        {
#if RELEASE
            try
            {
                error = ((IGameHandler)state).Play();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                try
                {
                    // try to save the exception
                    LogManager.Instance.LogExceptionFile(ex);
                }
                catch
                {
                    error = "We failed so hard we failed while trying to record the reason we failed initially. Sorry.";
                    return;
                }
            }
#else
            error = ((IGameHandler)state).Play();
#endif
        }
    }
}
