using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Nucleus.Gaming.Interop;

namespace Nucleus.Gaming
{
    /// <summary>
    /// Manages games information, so we can know what games are supported and how to support it
    /// </summary>
    public class GameManager
    {
        protected Dictionary<string, GameInfo> games;

        /// <summary>
        /// A dictionary containing GameInfos. The key is the game executable
        /// </summary>
        public Dictionary<string, GameInfo> Games
        {
            get { return games; }
        }

        protected UserProfile user;
        public UserProfile User
        {
            get { return user; }
            set { user = value; }
        }

        private static GameManager instance;
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

        #region Initialize
        public virtual void End()
        {
            User32.ShowTaskBar();
        }

        protected string GetUserProfilePath()
        {
            // search for user file
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string split = Path.Combine(appData, "SplitPlayPC");
            return split + "\\userprofile.nsplit";
        }

        public int Compare(UserGameInfo x, UserGameInfo y)
        {
            return x.GameName.CompareTo(y.GameName);
        }

        public virtual void UpdateUserProfile()
        {
            user.Games.Sort(Compare);

            string userProfile = GetUserProfilePath();

            using (FileStream stream = new FileStream(userProfile, FileMode.Create))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                UserProfile.Write(writer, user);
                writer.Flush();
            }
        }

        protected virtual void LoadUser()
        {
            string userProfile = GetUserProfilePath();

            if (File.Exists(userProfile))
            {
                try
                {
                    using (FileStream stream = new FileStream(userProfile, FileMode.Open))
                    {
                        user = UserProfile.Read(new BinaryReader(stream));
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
            string userProfile = GetUserProfilePath();
            string split = Path.GetDirectoryName(userProfile);
            if (!Directory.Exists(split))
            {
                Directory.CreateDirectory(split);
            }

            using (FileStream stream = new FileStream(userProfile, FileMode.Create))
            {
                user = new UserProfile();
                BinaryWriter writer = new BinaryWriter(stream);
                UserProfile.Write(writer, user);
                writer.Flush();
            }
        }

        protected virtual void Initialize()
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

        public virtual void Play(IGameHandler handler)
        {
            if (handler.HideTaskBar)
            {
                User32.HideTaskbar();
            }
        }
    }
}
