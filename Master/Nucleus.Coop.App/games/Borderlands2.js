Game.AddOption("Save ID - Player 1",
    "Save ID to use for Player 1 (default 0)",
    "saveid0", 0);
Game.AddOption("Save ID - Player 2",
    "Save ID to use for Player 2 (default 1)",
    "saveid1", 1);
Game.AddOption("Save ID - Player 3",
    "Save ID to use for Player 3 (default 2)",
    "saveid2", 2);
Game.AddOption("Save ID - Player 4",
    "Save ID to use for Player 4 (default 3)",
    "saveid3", 3);

Game.GameName = "Borderlands 2";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "borderlands2.exe";
Game.SteamID = "49520";
Game.GUID = "49520";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "binaries\\win32";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "splashscreen";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Borderlands 2 (32-bit, DX9)";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;//true; // this is beta
Game.Hook.XInputNames = [ "xinput1_3.dll" ];

Game.Play = function () {
    var savePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Borderlands 2\\WillowGame\\Config\\WillowEngine.ini";
    Context.ModifySaveFile(savePath, savePath, Nucleus.SaveType.INI, [
        new Nucleus.IniSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
        new Nucleus.IniSaveInfo("SystemSettings", "ResX", Context.Width),
        new Nucleus.IniSaveInfo("SystemSettings", "ResY", Context.Height),
        new Nucleus.IniSaveInfo("SystemSettings", "Fullscreen", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bMuteAudioWhenNotInFocus", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("WillowGame.WillowGameEngine", "bMuteAudioWhenNotInFocus", false),
    ]);

    var playerStr = "saveid" + Context.PlayerID;
    if (Context.IsKeyboardPlayer) {
        Context.StartArguments = "-windowed -AlwaysFocus -NoController -SaveDataId=" + Context.Options[playerStr];
    }
    else {
        Context.StartArguments = "-windowed -AlwaysFocus -SaveDataId=" + Context.Options[playerStr];
    }

}