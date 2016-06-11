using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.SaintsRow4
{
    public class SaintsRow4Info : GenericGameInfo
    {
        public override bool SymlinkExe
        {
            get { return true; }
        }
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
            get { return "saintsrowiv.exe"; }
        }

        public override string GameName
        {
            get { return "Saints Row 4"; }
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

        public SaintsRow4Info()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
        }

        public override string GUID
        {
            get { return "206420"; }
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
            get { return "206420"; }
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
                        "SRE4"
                    };
            }
        }

        public override string LauncherExe
        {
            get { return ""; }
        }

        public override string LauncherTitle
        {
            get { return ""; }
        }
    }
}
