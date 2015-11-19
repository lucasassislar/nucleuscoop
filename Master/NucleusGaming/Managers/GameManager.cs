using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Nucleus.Gaming.Interop;
using Newtonsoft.Json;
using System.Threading;

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
        /// A dictionary containing GameInfos. The key is the game executable
        /// </summary>
        public Dictionary<string, GameInfo> Games
        {
            get { return games; }
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

            Initialize();
            LoadUser();
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

            UpdateUserProfile();

            return gInfo;
        }

        public void StartBackup(GameInfo game)
        {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);
            Directory.CreateDirectory(gamePath);

            backupFiles = new List<BackupFile>();
        }

        public BackupFile BackupFile(GameInfo game, string path)
        {
            string appData = GetAppDataPath();
            string gamePath = Path.Combine(appData, game.GUID);
            string destination = Path.Combine(gamePath, Path.GetFileName(path));
            File.Copy(path, destination);

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
                File.Delete(bkp.Source);
                File.Move(bkp.BackupPath, bkp.Source);
            }
        }

        private void UpdateUserProfile()
        {
            user.Games.Sort(Compare);

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
            IsSaving = true;
            ThreadPool.QueueUserWorkItem(saveUser, path);
        }

        private void saveUser(object p)
        {
            lock (saving)
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
                        games.Add(info.GUID, info);
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
        }
    }
}
