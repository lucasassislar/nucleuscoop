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

Game.KillMutex = [];

//SmartSteamEmu.ini
// steam_api.dll -> ValveApi.dll
// Moved SmartSteamEmu.dll -> steam_api.dll

Game.BinariesFolder = "Binaries\\Win64";
Game.RootGameFolderPath = "..\\..";
Game.XInputFolder = "Binaries\\Win64";
Game.ExecutablePath = "Binaries\\Win64";

Game.HandlerInterval = 16;
Game.CustomXinput = false;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "KFGame.exe";
Game.SteamID = "232090";
Game.GUID = "232090";
Game.GameName = "Killing Floor 2";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "splashscreen"; ///
Game.SaveType = null;
Game.SupportsPositioning = true;
Game.StartArguments = "";
Game.XInputFiles = [];

Game.Play = function () { }