using Nucleus.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Borderlands
{
    public class Borderlands2Info : IGenericGameInfo
    {       
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
            get { return "borderlands2.exe"; }
        }

        public  string GameName
        {
            get { return "Borderlands 2"; }
        }

        public  Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public  int MaxPlayers
        {
            get { return 4; }
        }

        public  int MaxPlayersOneMonitor
        {
            get { return 4; }
        }

        protected Dictionary<string, GameOption> options;
        public  Dictionary<string, GameOption> Options
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

        public  string GUID
        {
            get { return SteamID; }
        }
        public  string SteamID
        {
            get { return "49520"; }
        }

        public  GenericGameSaveType SaveType
        {
            get { return GenericGameSaveType.INI; }
        }

        public  string SavePath
        {
            get { return @"&MyDocuments&\My Games\Borderlands 2\WillowGame\Config\WillowEngine.ini"; }
        }

        public  Dictionary<string, string> ModifySave
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

        public  string StartArguments
        {
            get { return "if(Keyboard){\"-AlwaysFocus -NoController -SaveDataId=\"+Id}else{\"-AlwaysFocus -nostartupmovies -SaveDataId=\"+Id}"; }
        }

        public  string BinariesFolder
        {
            get { return @"binaries\win32"; }
        }

        public  bool NeedsSteamEmulation
        {
            get { return false; }
        }

        public  string[] KillMutex
        {
            get { return new string[0]; }
        }

        public  string LauncherExe
        {
            get { return ""; }
        }

        public  string LauncherTitle
        {
            get { return "splashscreen"; }
        }

        public  bool SymlinkExe
        {
            get { return true; }
        }
    }
}
