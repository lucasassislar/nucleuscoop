Game.Options = [
    new Nucleus.GameOption(
        "Save ID - Player 1",
        "Save ID to use for Player 1 (default 0)",
        "saveid0", 0),
    new Nucleus.GameOption(
        "Save ID - Player 2",
        "Save ID to use for Player 2 (default 1)",
        "saveid1", 1),
    new Nucleus.GameOption(
        "Save ID - Player 3",
        "Save ID to use for Player 3 (default 2)",
        "saveid2", 2),
    new Nucleus.GameOption(
        "Save ID - Player 4",
        "Save ID to use for Player 4 (default 3)",
        "saveid3", 3)
];

Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "borderlandspresequel.exe";
Game.SteamID = "261640";
Game.GUID = "261640";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "binaries\\win32";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "splashscreen";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.HideTaskbar = true;
Game.XInput.ForceFocus = true;
Game.XInput.ForceFocusWindowName = "Borderlands Pre-Sequel";
Game.XInput.DInputEnabled = false;
Game.XInput.XInputEnabled = true;
Game.XInput.XInputReroute = true; // this is beta

Game.Play = function () {
    var savePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Borderlands The Pre-Sequel\\WillowGame\\Config\\WillowEngine.ini";
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
        Context.StartArguments = "-windowed -AlwaysFocus -NoController -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
    else {
        Context.StartArguments = "-windowed -AlwaysFocus -NoMouse -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
}