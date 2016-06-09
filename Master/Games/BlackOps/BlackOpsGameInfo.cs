using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.BlackOps
{
    public class BlackOpsGameInfo : GenericGameInfo
    {
        //t6zm
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
            get { return "__blackops.exe"; }
        }

        public override string GameName
        {
            get { return "Call of Duty - Black Ops: Zombies"; }
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

        public BlackOpsGameInfo()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
        }

        /// <summary>
        /// Generated with https://www.guidgenerator.com/online-guid-generator.aspx
        /// </summary>
        public override string GUID
        {
            get { return "cc1b45a0-09f0-4144-b56f-2710afd2e234"; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        public override GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.None; }
        }

        public override string SavePath
        {
            get { return ""; } // return @"&MyDocuments&\My Games\Borderlands 2\WillowGame\Config\WillowEngine.ini"; }
        }

        public override Dictionary<string, string> ModifySave
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

        public override string StartArguments
        {
            get { return ""; }//"if(Keyboard){\"-AlwaysFocus -NoController -SaveDataId=\"+Id}else{\"-AlwaysFocus -nostartupmovies -SaveDataId=\"+Id}"; }
        }

        public override string BinariesFolder
        {
            get { return @""; }
        }

        public override string SteamID
        {
            get { return "42700"; }
        }

        public override bool NeedsSteamEmulation
        {
            get { return true; }
        }
        public override string[] KillMutex
        {
            get { return new string[0]; }
        }
    }
}
