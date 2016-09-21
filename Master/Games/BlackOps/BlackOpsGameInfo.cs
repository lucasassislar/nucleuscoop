using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.BlackOps
{
    public class BlackOpsGameInfo : IGenericGameInfo
    {
        public  bool SymlinkExe
        {
            get { return true; }
        }
        public  string LauncherExe
        {
            get { throw new NotImplementedException(); }
        }
        public  string LauncherTitle
        {
            get { throw new NotImplementedException(); }
        }
        //t6zm
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
            get { return "__blackops.exe"; }
        }

        public  string GameName
        {
            get { return "Call of Duty - Black Ops: Zombies"; }
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

        public BlackOpsGameInfo()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
        }

        /// <summary>
        /// Generated with https://www.guidgenerator.com/online-guid-generator.aspx
        /// </summary>
        public  string GUID
        {
            get { return "cc1b45a0-09f0-4144-b56f-2710afd2e234"; }
        }

        public  int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        public  GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.None; }
        }

        public  string SavePath
        {
            get { return ""; } // return @"&MyDocuments&\My Games\Borderlands 2\WillowGame\Config\WillowEngine.ini"; }
        }

        public  Dictionary<string, string> ModifySave
        {
            get
            {
                return new Dictionary<string, string>();
                //{
                //    { "SystemSettings/WindowedFullscreen", "IsFullscreen" },
                //    { "SystemSettings/ResX", "Width" },
                //    { "SystemSettings/ResY", "Height" },
                //    { "SystemSettings/Fullscreen", "false" },
                //    { "Engine.Engine/bMuteAudioWhenNotInFocus", "false" },
                //    { "Engine.Engine/bPauseOnLossOfFocus", "false" },
                //    { "WillowGame.WillowGameEngine/bPauseLostFocusWindowed", "false" },
                //    { "WillowGame.WillowGameEngine/bMuteAudioWhenNotInFocus", "false" },
                //};
            }
        }

        public  string StartArguments
        {
            get { return ""; }//"if(Keyboard){\"-AlwaysFocus -NoController -SaveDataId=\"+Id}else{\"-AlwaysFocus -nostartupmovies -SaveDataId=\"+Id}"; }
        }

        public  string BinariesFolder
        {
            get { return @""; }
        }

        public  string SteamID
        {
            get { return "42700"; }
        }

        public  bool NeedsSteamEmulation
        {
            get { return true; }
        }
        public  string[] KillMutex
        {
            get { return new string[0]; }
        }
    }
}
