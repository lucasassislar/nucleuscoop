Game.AddOption("Save ID - Player 1", "Save ID to use for Player 1 (default 0)",
    "saveid0", 0);
Game.AddOption("Save ID - Player 2", "Save ID to use for Player 2 (default 1)",
    "saveid1", 1);
Game.AddOption("Save ID - Player 3", "Save ID to use for Player 3 (default 2)",
    "saveid2", 2);
Game.AddOption("Save ID - Player 4", "Save ID to use for Player 4 (default 3)",
    "saveid3", 3);

Game.PlatformVersion = 10; // Nucleus Coop 10
Game.SteamID = "261640";
Game.GameID = "261640";
Game.GameName = "Borderlands Pre-Sequel";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "borderlandspresequel.exe";
Game.SteamID = "261640";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.ExecutablePath = "binaries\\win32";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "splashscreen";
Game.SaveType = SaveType.INI;
Game.SupportsPositioning = true;
Game.HideTaskbar = true;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Borderlands: The Pre-Sequel (32-bit, DX9)";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;// true; // this is beta/not working at all probably
Game.Hook.XInputNames = ["xinput1_3.dll"];

Game.OnPlay.Callback(function () {
    var savePath = Context.GetFolder(Folder.Documents) + "\\My Games\\Borderlands The Pre-Sequel\\WillowGame\\Config\\WillowEngine.ini";
    Context.ModifySaveFile(savePath, savePath, SaveType.INI, [
       Context.NewSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
       Context.NewSaveInfo("SystemSettings", "ResX", Context.Width),
       Context.NewSaveInfo("SystemSettings", "ResY", Context.Height),
       Context.NewSaveInfo("SystemSettings", "Fullscreen", false),
       Context.NewSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
       Context.NewSaveInfo("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", false),
       Context.NewSaveInfo("Engine.Engine", "bMuteAudioWhenNotInFocus", false),
       Context.NewSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
       Context.NewSaveInfo("WillowGame.WillowGameEngine", "bMuteAudioWhenNotInFocus", false),
    ]);

    var playerStr = "saveid" + Context.PlayerID;
    if (Context.IsKeyboardPlayer) {
        Context.StartArguments = "-windowed -AlwaysFocus -NoController -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
    else {
        Context.StartArguments = "-windowed -AlwaysFocus -NoMouse -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
});