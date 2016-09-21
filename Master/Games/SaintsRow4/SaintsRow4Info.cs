using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.SaintsRow4
{
    public class SaintsRow4Info : IGenericGameInfo
    {
        public bool SymlinkExe
        {
            get { return true; }
        }
        public bool SupportsKeyboard
        {
            get { return true; }
        }
        public Type[] Steps
        {
            get
            {
                return new Type[]
                {
                    typeof(PlayerCountControl),
                    typeof(PositionsForm),
                    typeof(PlayerOptions)
                };
            }
        }
        public string[] ExecutableContext
        {
            get { return null; }
        }
        public string ExecutableName
        {
            get { return "saintsrowiv.exe"; }
        }

        public string GameName
        {
            get { return "Saints Row 4"; }
        }

        public Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public int MaxPlayers
        {
            get { return 4; }
        }

        protected Dictionary<string, GameOption> options;
        public Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public SaintsRow4Info()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
        }

        public string GUID
        {
            get { return "206420"; }
        }

        public int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        public GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.INI; }
        }

        public string SavePath
        {
            get { return @"&GameFolder&\display.ini"; }
        }

        public Dictionary<string, string> ModifySave
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "/ResolutionWidth", "Width" },
                    { "/ResolutionHeight", "Height" },
                    { "/Fullscreen", "false" },
                };
            }
        }

        public string StartArguments
        {
            get { return ""; }
        }

        public string BinariesFolder
        {
            get { return ""; }
        }

        public string SteamID
        {
            get { return "206420"; }
        }

        public bool NeedsSteamEmulation
        {
            get { return true; }
        }

        public string[] KillMutex
        {
            get
            {
                return new string[]
                    {
                        "SRE4"
                    };
            }
        }

        public string LauncherExe
        {
            get { return ""; }
        }

        public string LauncherTitle
        {
            get { return ""; }
        }
    }
}
