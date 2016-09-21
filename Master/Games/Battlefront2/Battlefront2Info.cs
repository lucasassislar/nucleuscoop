using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Battlefront2
{
    public class Battlefront2Info : IGenericGameInfo
    {
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
            get { return "__battlefrontii.exe"; }
        }

        public string GameName
        {
            get { return "Battlefront 2"; }
        }

        public Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public int MaxPlayers
        {
            get { return 4; }
        }

        public int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        protected GameOption[] options;
        public Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public Battlefront2Info()
        {
            options = new Dictionary<string, GameOption>();
            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer, "KeyboardPlayer"));
        }

        public string GUID
        {
            get { return "6060"; }//"46a03ad4-7f8a-447e-ac05-1801bb831a8c"; }
        }

        public GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.None; }
        }

        public string SavePath
        {
            get { return @""; }
        }

        public Dictionary<string, string> ModifySave
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }

        public string StartArguments
        {
            get { return "\"/win /resolution \" + Width + \" \" + Height"; }
        }

        public string BinariesFolder
        {
            get { return @"GameData"; }
        }

        public string SteamID
        {
            get { return "6060"; }
        }

        public bool NeedsSteamEmulation
        {
            get { return true; }
        }

        public string[] KillMutex
        {
            get { return new string[0]; }
        }

        public string LauncherExe
        {
            get { return ""; }
        }
        public string LauncherTitle
        {
            get { return ""; }
        }

        public bool SymlinkExe
        {
            get { return true; }
        }
    }
}
