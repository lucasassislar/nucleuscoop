using Jint;
using Nucleus.Coop;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming
{
    public class GenericGameInfo : IGenericGameInfo
    {
        private Engine engine;
        private string js;
        private Action play;

        private SaveInfo[] modifySave;
        private bool symlinkExe;
        private bool supportsKeyboard;
        private string[] executableContext;
        private string executableName;
        private string steamID;
        private string guid;
        private string gameName;
        private int maxPlayers;
        private GameOption[] options;
        private int maxPlayersOneMonitor;
        private SaveType saveType;
        private string savePath;
        private string startArguments;
        private string binariesFolder;
        private bool needsSteamEmulation;
        private string launcherExe;
        private string launcherTitle;
        private bool supportsPositioning;
        private bool hideTaskbar;
        private int interval = 1000;
        private bool customXinput = true;
        private bool hookNeeded = false;
        private string hookGameWindowName = "";
        private string[] killMutex;
        private bool debug;

        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        public bool HookNeeded
        {
            get { return hookNeeded; }
            set { hookNeeded = value; }
        }
        public string HookGameWindowName
        {
            get { return hookGameWindowName; }
            set { hookGameWindowName = value; }
        }

        public bool CustomXinput
        {
            get { return customXinput; }
            set { customXinput = value; }
        }
        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }
        public bool HideTaskbar
        {
            get { return hideTaskbar; }
            set { hideTaskbar = value; }
        }

        public bool SupportsPositioning
        {
            get { return supportsPositioning; }
            set { supportsPositioning = value; }
        }
        public bool SymlinkExe
        {
            get { return symlinkExe; }
            set { symlinkExe = value; }
        }
        public bool SupportsKeyboard
        {
            get { return supportsKeyboard; }
            set { supportsKeyboard = value; }
        }
        public Type[] AdditionalSteps
        {
            get
            {
                return new Type[]
                {
                    typeof(PlayerCountControl),
                    typeof(PositionsControl),
                    typeof(PlayerOptionsControl)
                };
            }
        }
        public string[] ExecutableContext
        {
            get { return executableContext; }
            set { executableContext = value; }
        }
        public string ExecutableName
        {
            get { return executableName; }
            set { executableName = value; }
        }
        public string SteamID
        {
            get { return steamID; }
            set { steamID = value; }
        }
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        public string GameName
        {
            get { return gameName; }
            set { gameName = value; }
        }

        public Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public int MaxPlayers
        {
            get { return maxPlayers; }
            set { maxPlayers = value; }
        }

        public GameOption[] Options
        {
            get { return options; }
            set { options = value; }
        }

        public int MaxPlayersOneMonitor
        {
            get { return maxPlayersOneMonitor; }
            set { maxPlayersOneMonitor = value; }
        }

        public SaveType SaveType
        {
            get { return saveType; }
            set { saveType = value; }
        }

        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }

        public SaveInfo[] ModifySave
        {
            get { return modifySave; }
            set { modifySave = value; }
        }

        public string StartArguments
        {
            get { return startArguments; }
            set { startArguments = value; }
        }

        public string BinariesFolder
        {
            get { return binariesFolder; }
            set { binariesFolder = value; }
        }

        public bool NeedsSteamEmulation
        {
            get { return needsSteamEmulation; }
            set { needsSteamEmulation = value; }
        }

        public string[] KillMutex
        {
            get { return killMutex; }
            set { killMutex = value; }
        }

        public string LauncherExe
        {
            get { return launcherExe; }
            set { launcherExe = value; }
        }
        public string LauncherTitle
        {
            get { return launcherTitle; }
            set { launcherTitle = value; }
        }

        public Action Play
        {
            get { return play; }
            set { play = value; }
        }

        public GenericGameInfo(Stream str)
        {
            StreamReader reader = new StreamReader(str);
            js = reader.ReadToEnd();

            Assembly assembly = typeof(GameOption).Assembly;

            engine = new Engine(cfg => cfg.AllowClr(assembly));
            engine.SetValue("Game", this);
            engine.Execute("var Nucleus = importNamespace('Nucleus.Gaming');");
            engine.Execute(js);
            engine.SetValue("Game", (object)null);
        }

        public void PrePlay(GenericContext context)
        {
            engine.SetValue("Context", context);

            if (play != null)
            {
                play();
            }
        }

        /// <summary>
        /// Clones this Game Info into a new Generic Context
        /// </summary>
        /// <returns></returns>
        public GenericContext CreateContext(GameProfile profile, PlayerInfo info, GenericGameHandler handler)
        {
            GenericContext context = new GenericContext(profile, info, handler);

            Type t = GetType();
            PropertyInfo[] props = t.GetProperties();

            Type c = context.GetType();
            PropertyInfo[] cprops = c.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo p = props[i];
                PropertyInfo d = cprops.FirstOrDefault(k => k.Name == p.Name);
                if (d == null)
                {
                    continue;
                }

                if (p.PropertyType != d.PropertyType ||
                    !d.CanWrite)
                {
                    continue;
                }

                object value = p.GetValue(this, null);
                d.SetValue(context, value, null);
            }

            return context;
        }
    }
}
