Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
];

Game.Debug = true;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "blackops.exe";
Game.SteamID = "42700";
Game.GUID = "42700";
Game.GameName = "Black Ops";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "";
Game.HookNeeded = true;
Game.HookGameWindowName = "Call of Duty®: BlackOps";
Game.SupportsXInput = true;

Game.Play = function () {
}