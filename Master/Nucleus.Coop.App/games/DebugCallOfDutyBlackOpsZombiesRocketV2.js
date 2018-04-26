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
Game.ExecutableName = "blackops.exe";
Game.SteamID = "42700";
Game.GUID = "42700";
Game.GameName = "Black Ops SP RV2";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "Developer Console";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.StartArguments = "";
Game.LauncherExe = "";
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowName = "Call of Duty®: BlackOps";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;
Game.Hook.BlockMouseEvents = false;
Game.Hook.BlockKeyboardEvents = false;

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