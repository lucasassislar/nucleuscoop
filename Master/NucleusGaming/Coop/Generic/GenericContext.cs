using Nucleus.Coop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    // This class is a holder for the GenericGameInfo class. It doesn't implement the IGenericGameInfo
    // because some of the elements are implemented differently to work with the JS engine
    // Comments can be found on the original class if no specific feature is implemented here
    public class GenericContext
    {
        private SaveInfo[] modifySave;
        private bool symlinkExe;
        private bool supportsKeyboard;
        private string[] executableContext;
        private string executableName;
        private string steamID;
        private string guid;
        private string gameName;
        private int maxPlayers;
        private int maxPlayersOneMonitor;
        private SaveType saveType;
        private string savePath;
        private string startArguments;
        private string binariesFolder;
        private bool needsSteamEmulation;
        private string launcherExe;
        private string launcherTitle;
        private bool hideTaskbar;
        private int interval;
        private bool customXinput;
        private bool hookNeeded = false;
        private string hookGameWindowName = "";
        private string[] killMutex;
        private string error;
        private bool debug;
        private double handlerInterval;

        public double HandlerInterval
        {
            get { return handlerInterval; }
            set { handlerInterval = value; }
        }
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        public string Error
        {
            get { return error; }
            set { error = value; }
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

        public Dictionary<string, object> Options
        {
            get { return profile.Options; }
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

        public bool IsKeyboardPlayer { get; set; }
        public int Width
        {
            get { return pInfo.MonitorBounds.Width; }
        }
        public int Height
        {
            get { return pInfo.MonitorBounds.Height; }
        }
        public int PlayerID { get; set; }
        public bool IsFullscreen { get; set; }

        private GameProfile profile;
        private PlayerInfo pInfo;
        private GenericGameHandler parent;
        public GenericContext(GameProfile prof, PlayerInfo info, GenericGameHandler handler)
        {
            this.profile = prof;
            this.pInfo = info;
            this.parent = handler;
        }

        public string GetFolder(Folder folder)
        {
            return parent.GetFolder(folder);
        }
    }
}
