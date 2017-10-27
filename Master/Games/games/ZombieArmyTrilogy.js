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

Game.KillMutex = [
    "Zombie Army Trilogy"
];

//SmartSteamEmu.ini
// steam_api.dll -> ValveApi.dll
// Moved SmartSteamEmu.dll -> steam_api.dll

Game.BinariesFolder = "bin";
Game.RootGameFolderPath = "..";
Game.XInputFolder = "bin";
Game.ExecutablePath = "bin";

Game.HandlerInterval = 16;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "zat.exe";
Game.SteamID = "301640";
Game.GUID = "301640";
Game.GameName = "Zombie Army Trilogy";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "splashscreen";
Game.SaveType = null;
Game.SupportsPositioning = true;
Game.StartArguments = "-subwindow";
Game.XInputFiles = ["xinput1_3.dll", "x360ce.ini"];

Game.Play = function () { }