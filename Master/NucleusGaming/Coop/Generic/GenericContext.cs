using Nucleus.Coop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
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

        public Type[] Steps
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
            get { return new string[0]; }
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
            get { return pInfo.monitorBounds.Width; }
        }
        public int Height
        {
            get { return pInfo.monitorBounds.Height; }
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
