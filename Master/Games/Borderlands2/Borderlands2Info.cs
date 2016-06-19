using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Borderlands
{
    public class Borderlands2Info : GenericGameInfo
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
            get { return "borderlands2.exe"; }
        }

        public override string GameName
        {
            get { return "Borderlands 2"; }
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

        public Borderlands2Info()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("KeyboardPlayer", new GameOption("Keyboard Player", "The player that will be playing on keyboard and mouse", KeyboardPlayer.NoKeyboardPlayer));
            for (int i = 0; i < MaxPlayers; i++)
            {
                int playerID = i + 1;
                options.Add("saveid" + i, new GameOption("Save ID - Player " + playerID, "Save ID to use for Player " + playerID + " (default " + i + ")", i));
            }
        }

        public override string GUID
        {
            get { return SteamID; }
        }
        public override string SteamID
        {
            get { return "49520"; }
        }

        public override GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.INI; }
        }

        public override string SavePath
        {
            get { return @"&MyDocuments&\My Games\Borderlands 2\WillowGame\Config\WillowEngine.ini"; }
        }

        public override Dictionary<string, string> ModifySave
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "SystemSettings/WindowedFullscreen", "IsFullscreen" },
                    { "SystemSettings/ResX", "Width" },
                    { "SystemSettings/ResY", "Height" },
                    { "SystemSettings/Fullscreen", "false" },
                    { "Engine.Engine/bMuteAudioWhenNotInFocus", "false" },
                    { "Engine.Engine/bPauseOnLossOfFocus", "false" },
                    { "WillowGame.WillowGameEngine/bPauseLostFocusWindowed", "false" },
                    { "WillowGame.WillowGameEngine/bMuteAudioWhenNotInFocus", "false" },
                };
            }
        }

        public override string StartArguments
        {
            get { return "if(Keyboard){\"-AlwaysFocus -NoController -SaveDataId=\"+Id}else{\"-AlwaysFocus -nostartupmovies -SaveDataId=\"+Id}"; }
        }

        public override string BinariesFolder
        {
            get { return @"binaries\win32"; }
        }

        public override bool NeedsSteamEmulation
        {
            get { return false; }
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
            get { return "splashscreen"; }
        }

        public override bool SymlinkExe
        {
            get { return true; }
        }
    }
}
