Game.AddOption("Save ID - Player 1", "Save ID to use for Player 1 (default 0)",
    "saveid0", 0);
Game.AddOption("Save ID - Player 2", "Save ID to use for Player 2 (default 1)",
    "saveid1", 1);
Game.AddOption("Save ID - Player 3", "Save ID to use for Player 3 (default 2)",
    "saveid2", 2);
Game.AddOption("Save ID - Player 4", "Save ID to use for Player 4 (default 3)",
    "saveid3", 3);

Game.SteamID = "239140";
Game.GameID = "239140";
Game.GameName = "Dying Light";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "borderlandspresequel.exe";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "binaries\\win32";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "splashscreen";
Game.SaveType = SaveType.INI;
Game.SupportsPositioning = true;
Game.HideTaskbar = true;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowName = "Dying Light";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;// true; // this is beta
Game.Hook.XInputNames = ["xinput1_3.dll"];

Game.OnPlay.Callback(function () {
    var savePath = Context.GetFolder(Folder.Documents) + "\\My Games\\Borderlands The Pre-Sequel\\WillowGame\\Config\\WillowEngine.ini";
    Context.ModifySaveFile(savePath, savePath, SaveType.INI, [
       Context.NewIniSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
       Context.NewIniSaveInfo("SystemSettings", "ResX", Context.Width),
       Context.NewIniSaveInfo("SystemSettings", "ResY", Context.Height),
       Context.NewIniSaveInfo("SystemSettings", "Fullscreen", false),
       Context.NewIniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
       Context.NewIniSaveInfo("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", false),
       Context.NewIniSaveInfo("Engine.Engine", "bMuteAudioWhenNotInFocus", false),
       Context.NewIniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
       Context.NewIniSaveInfo("WillowGame.WillowGameEngine", "bMuteAudioWhenNotInFocus", false),
    ]);

    var playerStr = "saveid" + Context.PlayerID;
    if (Context.IsKeyboardPlayer) {
        Context.StartArguments = "-windowed -AlwaysFocus -NoController -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
    else {
        Context.StartArguments = "-windowed -AlwaysFocus -NoMouse -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
});