using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.SaintsRow3
{
    public class SaintsRow3Info : GenericGameInfo
    {
        public override bool SupportsKeyboard
        {
            get { return true; }
        }
        public override Type[] Steps
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
        public override string ExecutableContext
        {
            get { return ""; }
        }
        public override string ExecutableName
        {
            get { return "saintsrowthethird_dx11.exe"; }
        }

        public override string GameName
        {
            get { return "Saints Row 3"; }
        }

        public override Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public override int MaxPlayers
        {
            get { return 4; }
        }

        protected Dictionary<string, GameOption> options;
        public override Dictionary<string, GameOption> Options
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
        public override string GUID
        {
            get { return "c466f806-a6d7-4a45-be91-4afb2f91f339"; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        public override GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.INI; }
        }

        public override string SavePath
        {
            get { return @"&GameFolder&\display.ini"; }
        }

        public override Dictionary<string, string> ModifySave
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

        public override string StartArguments
        {
            get { return ""; }
        }

        public override string BinariesFolder
        {
            get { return ""; }
        }

        public override string SteamID
        {
            get { return "55230"; }
        }

        public override bool NeedsSteamEmulation
        {
            get { return true; }
        }

        public override string[] KillMutex
        {
            get
            {
                return new string[]
                    {
                        "SR3"
                    };
            }
        }

        public override string LauncherExe
        {
            get { return "game_launcher"; }
        }

        public override string LauncherTitle
        {
            get { return "Saints Row: The Launcher"; }
        }
    }
}
