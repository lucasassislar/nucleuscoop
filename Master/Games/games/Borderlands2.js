Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
    new Nucleus.GameOption(
        "Save ID - Player 1",
        "Save ID to use for Player 1 (default 0)",
        0,
        "saveid0"),
    new Nucleus.GameOption(
        "Save ID - Player 2",
        "Save ID to use for Player 2 (default 1)",
        1,
        "saveid1"),
    new Nucleus.GameOption(
        "Save ID - Player 3",
        "Save ID to use for Player 3 (default 2)",
        2,
        "saveid2"),
    new Nucleus.GameOption(
        "Save ID - Player 4",
        "Save ID to use for Player 4 (default 3)",
        3,
        "saveid3")
];

Game.HandlerInterval = 16;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "borderlands2.exe";
Game.SteamID = "49520";
Game.GUID = "49520";
Game.GameName = "Borderlands 2";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "binaries\\win32";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "splashscreen";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.HideTaskbar = true;

Game.Play = function () {
    Context.ModifySave = [
        new Nucleus.IniSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
        new Nucleus.IniSaveInfo("SystemSettings", "ResX", Context.Width),
        new Nucleus.IniSaveInfo("SystemSettings", "ResY", Context.Height),
        new Nucleus.IniSaveInfo("SystemSettings", "Fullscreen", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bMuteAudioWhenNotInFocus", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("WillowGame.WillowGameEngine", "bMuteAudioWhenNotInFocus", false),
    ];

    var playerStr = "saveid" + Context.PlayerID;
    if (Context.IsKeyboardPlayer) {
        Handler.StartPlayTick(1, function () {
            Handler.CenterCursor();
        });
        Context.StartArguments = "-windowed -AlwaysFocus -NoController -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }
    else {
        Context.StartArguments = "-windowed -AlwaysFocus -nostartupmovies -SaveDataId=" + Context.Options[playerStr];
    }

    Context.SavePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Borderlands 2\\WillowGame\\Config\\WillowEngine.ini";
}