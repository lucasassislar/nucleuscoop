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
    "GrimDawn"
];

Game.BinariesFolder = "";

Game.Debug = false;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "grim dawn.exe";
Game.SteamID = "219990";
Game.GUID = "219990";
Game.GameName = "Grim Dawn";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.INI_NO_SECTIONS;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "";
//Game.XInputFiles = ["xinput1_3.dll", "x360ce.ini"];

Game.Play = function () {
    Context.ModifySave = [
        new Nucleus.IniSaveInfo("", "resolution", Context.Width + " " + Context.Height),
        new Nucleus.IniSaveInfo("", "inactiveUpdateRate", 1),
        new Nucleus.IniSaveInfo("", "screenMode", 1)
    ];

    Context.SavePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Grim Dawn\\Settings\\options.txt";
}