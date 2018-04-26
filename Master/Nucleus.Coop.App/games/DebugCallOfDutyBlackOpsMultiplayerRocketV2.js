Game.AddOption("Keyboard Player",
    "The player that will be playing on keyboard and mouse (if any)",
    "KeyboardPlayer",
    Nucleus.KeyboardPlayer.NoKeyboardPlayer);
Game.ExecutableContext = [
    "rocketv2.exe"
];
Game.FileSymlinkExclusions = [
    "v2.ini",
];
Game.FileSymlinkCopyInstead = [
    "libnp.dll"
];

Game.Debug = true;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.HardcopyGame = false;

Game.SupportsKeyboard = true;
Game.HandlerInterval = 100;
Game.ExecutableName = "blackopsmp.exe";
Game.SteamID = "42710";
Game.GUID = "42710";
Game.GameName = "Black Ops Multiplayer RV2";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "V2: Black Ops";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.StartArguments = "";
Game.LauncherExe = "";
Game.Hook.ForceFocus = false;
Game.Hook.ForceFocusWindowName = "V2: BlackOps";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;
Game.Hook.BlockMouseEvents = false;
Game.Hook.BlockKeyboardEvents = false;
Game.Hook.BlockInputEvents = false;

Game.Play = function () {
    var lines;
    if (Context.PlayerID == 0) {
        lines = [
            "",
            ""
        ]
    } else {
        lines = [
            "",
            ""
        ]
    }
    var savePath = Context.SavePath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\v2.ini";
    Context.WriteTextFile(savePath, lines);
}