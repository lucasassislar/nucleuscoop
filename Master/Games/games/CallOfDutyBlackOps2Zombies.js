Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
];

Game.Debug = true;
Game.NeedsSteamEmulation = true;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "t6zm.exe";
Game.SteamID = "212910";
Game.GUID = "212910";
Game.GameName = "Black Ops 2 - Zombies";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "";
Game.HookNeeded = true;
Game.HookGameWindowName = "Call of Duty®: Black Ops II - Zombies";

Game.Play = function () {
}