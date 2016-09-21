using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.SaintsRow3
{
    public class SaintsRow3Info : IGenericGameInfo
    {
        public  bool SymlinkExe
        {
            get { return true; }
        }
        public  bool SupportsKeyboard
        {
            get { return true; }
        }
        public  Type[] Steps
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
        public  string ExecutableName
        {
            get { return "saintsrowthethird_dx11.exe"; }
        }

        public  string GameName
        {
            get { return "Saints Row 3"; }
        }

        public  Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public  int MaxPlayers
        {
            get { return 4; }
        }

        protected Dictionary<string, GameOption> options;
        public  Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public SaintsRow3Info()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
        }

        /// <summary>
        /// Generated with http://www.random-guid.com/
        /// </summary>
        public  string GUID
        {
            get { return "55230"; }
        }

        public  int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        public  GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.INI; }
        }

        public  string SavePath
        {
            get { return @"&GameFolder&\display.ini"; }
        }

        public  Dictionary<string, string> ModifySave
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

        public  string StartArguments
        {
            get { return ""; }
        }

        public  string BinariesFolder
        {
            get { return ""; }
        }

        public  string SteamID
        {
            get { return "55230"; }
        }

        public  bool NeedsSteamEmulation
        {
            get { return true; }
        }

        public  string[] KillMutex
        {
            get
            {
                return new string[]
                    {
                        "SR3"
                    };
            }
        }

        public  string LauncherExe
        {
            get { return "game_launcher"; }
        }

        public  string LauncherTitle
        {
            get { return "Saints Row: The Launcher"; }
        }
    }
}
