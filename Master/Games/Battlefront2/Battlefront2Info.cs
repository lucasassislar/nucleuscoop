using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Battlefront2
{
    public class Battlefront2Info : GenericGameInfo
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
            get { return "__battlefrontii.exe"; }
        }

        public override string GameName
        {
            get { return "Battlefront 2"; }
        }

        public override Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public override int MaxPlayers
        {
            get { return 4; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        protected Dictionary<string, GameOption> options;
        public override Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public Battlefront2Info()
        {
            options = new Dictionary<string, GameOption>();
            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
        }

        public override string GUID
        {
            get { return "6060"; }//"46a03ad4-7f8a-447e-ac05-1801bb831a8c"; }
        }

        public override GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.None ; }
        }

        public override string SavePath
        {
            get { return @""; }
        }

        public override Dictionary<string, string> ModifySave
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }

        public override string StartArguments
        {
            get { return "\"/win /resolution \" + Width + \" \" + Height"; }
        }

        public override string BinariesFolder
        {
            get { return @"GameData"; }
        }

        public override string SteamID
        {
            get { return "6060"; }
        }

        public override bool NeedsSteamEmulation
        {
            get { return true; }
        }

        public override string[] KillMutex
        {
            get { return new string[0]; }
        }

        public override string LauncherExe
        {
            get { return ""; }
        }
        public override string LauncherTitle
        {
            get { return ""; }
        }

        public override bool SymlinkExe
        {
            get { return true; }
        }
    }
}
