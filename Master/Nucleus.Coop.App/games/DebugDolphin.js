Game.GameName = "Dolphin";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "dolphin.exe";
Game.SteamID = "666777";
Game.GUID = "666777";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.Hook.ForceFocus = false;

Game.Play = function () {
    Context.StartArguments = "-e";
}