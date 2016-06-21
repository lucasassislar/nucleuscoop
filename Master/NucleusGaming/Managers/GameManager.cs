using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Nucleus.Gaming.Interop;
using Newtonsoft.Json;
using System.Threading;
using Ionic.Zip;
using Nucleus.Gaming.Properties;

namespace Nucleus.Gaming
{
    /// <summary>
    /// Manages games information, 
    /// so we can know what games are supported 
    /// and how to support it
    /// </summary>
    public class GameManager
    {
        private static GameManager instance;
        private Dictionary<string, GameInfo> games;
        private Dictionary<string, GameInfo> gameInfos;
        private UserProfile user;
        private List<BackupFile> backupFiles;

        /// <summary>
        /// object instance so we can thread-safe save the user profile
        /// </summary>
        private object saving = new object();

        public bool IsSaving
        {
            get;
            private set;
        }

        /// <summary>
        /// A dictionary containing GameInfos. The key is the game's info guid
        /// </summary>
        public Dictionary<string, GameInfo> Games
        {
            get { return games; }
        }


        public Dictionary<string, GameInfo> GameInfos
        {
            get { return gameInfos; }
        }

        public UserProfile User
        {
            get { return user; }
            set { user = value; }
        }

        public static GameManager Instance
        {
            get { return instance; }
        }

        public GameManager()
        {
            instance = this;
            games = new Dictionary<string, GameInfo>();
            gameInfos = new Dictionary<string, GameInfo>();

            string appData = GetAppDataPath();
            Directory.CreateDirectory(appData);

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
            return Games.Values.Any(c => c.ExecutableName == gameExe);
        }

        /// <summary>
        /// Tries adding a game to the collection with the provided executable path
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public UserGameInfo TryAddGame(string exePath)
        {
            string lower = exePath.ToLower();
            string fileName = Path.GetFileName(exePath).ToLower();
            string dir = Path.GetDirectoryName(exePath);

            var possibilities = Games.Values.Where(c => c.ExecutableName == fileName);

            foreach (GameInfo game in possibilities)
            {
                // check if the Context matches
                string[] context = game.ExecutableContext.Split(';');
                bool notAdd = false;
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

                if (notAdd)
                {
                    continue;
                }

                // search for the same exe on the user profile
                if (GameManager.Instance.User.Games.Any(c => c.ExePath.ToLower() == lower))
                {
                    continue;
                }

                LogManager.Log("Found game: {0}, full path: {1}", game.GameName, exePath);
                UserGameInfo uinfo = new UserGameInfo();
                uinfo.InitializeDefault(game, exePath);
                GameManager.Instance.User.Games.Add(uinfo);
                GameManager.Instance.SaveUserProfile();

                return uinfo;
            }

            return null;
        }

        /// <summary>
        /// Extracts the SmartSteamEmu and returns the folder its on
        /// </summary>
        /// <returns></returns>
        public string ExtractSteamEmu()
        {
            string app = GetAppDataPath();
            string steamEmu = Path.Combine(app, "SteamEmu");
            if (!Directory.Exists(steamEmu))
            {
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
            return steamEmu;
        }

        public void WaitSave()
        {
            while (IsSaving)
            {
            }
        }

        #region Initialize
        public void End()
        {
            //User32.ShowTaskBar();
        }

        private string GetAppDataPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Nucleus Coop");
        }
        protected string GetUserProfilePath()
        {
            return Path.Combine(GetAppDataPath(), "userprofile.json");
        }

        public int Compare(UserGameInfo x, UserGameInfo y)
        {
            return x.Game.GameName.CompareTo(y.Game.GameName);
        }

        public UserGameInfo AddGame(GameInfo game, string exePath)
        {
            UserGameInfo gInfo = new UserGameInfo();
            gInfo.InitializeDefault(game, exePath);
            user.Games.Add(gInfo);

            SaveUserProfile();

            return gInfo;
        }

        public void BeginBackup(GameInfo game)
        {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);
            Directory.CreateDirectory(gamePath);

            backupFiles = new List<BackupFile>();
        }

        public IGameHandler MakeHandler(GameInfo game)
        {
            return (IGameHandler)Activator.CreateInstance(game.HandlerType);
        }

        public string GetBackupFolder(GameInfo game)
        {
            string appData = GetAppDataPath();
            return Path.Combine(appData, game.GUID);
        }

        public BackupFile BackupFile(GameInfo game, string path)
        {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);
            string destination = Path.Combine(gamePath, Path.GetFileName(path));

            if (!File.Exists(path) && File.Exists(destination))
            {
                // we fucked up and the backup exists
                File.Copy(destination, path);
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

        public void ExecuteBackup(GameInfo game)
        {
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
                user.Games.Sort(Compare);
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

                            if (user.Games == null)
                            {
                                // json doesn't save empty lists, and user didn't add any game
                                user.InitializeDefault();
                            }
                        }
                    }
                }
                catch (Exception wtf)
                {
                    makeDefaultUserFile();
                }
            }
            else
            {
                makeDefaultUserFile();
            }
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
                IsSaving = true;
                LogManager.Log("> Saving user profile....");
                ThreadPool.QueueUserWorkItem(saveUser, path);
            }
        }

        private void saveUser(object p)
        {
            lock (saving)
            {
                try
                {
                    IsSaving = true;
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
                    LogManager.Log("Saved user profile");
                }
                catch
                { }
                IsSaving = false;
            }
        }

        private void Initialize()
        {
            // Type we are looking for (GameInfo)
            Type infoType = typeof(GameInfo);

            Assembly ass = Assembly.Load(new AssemblyName("Nucleus.Coop.Games"));

            // I used to hate working with assembly, and that's why it has that name :D
            Type objType = typeof(object);
            if (ass == null)
            {
                // shit's null yo
            }
            else
            {
                Type[] t = ass.GetTypes();
                for (int x = 0; x < t.Length; x++)
                {
                    Type ty = t[x];
                    Type lastParent = ty.BaseType;
                    Type parent = ty.BaseType;

                    while (parent != objType)
                    {
                        lastParent = parent;
                        parent = parent.BaseType;
                    }
                    if (lastParent == infoType)
                    {
                        // Found!
                        GameInfo info = (GameInfo)Activator.CreateInstance(ty);
                        LogManager.Log("Found game info: " + info.GameName);


                        games.Add(info.GUID, info);
                        gameInfos.Add(info.ExecutableName, info);
                    }
                }
            }
        }
        #endregion

        public void Play(IGameHandler handler)
        {
            //if (handler.HideTaskBar)
            //{
            //    User32.HideTaskbar();
            //}

            // Start the Play method in another thread, so the
            // handler can update while it's still loading
            handler.Play();
            //ThreadPool.QueueUserWorkItem(play, handler);
        }
        private void play(object state)
        {
            ((IGameHandler)state).Play();
        }
    }
}
