Game.AddOption("Keyboard Player",
    "The player that will be playing on keyboard and mouse (if any)",
    Nucleus.KeyboardPlayer.NoKeyboardPlayer,
    "KeyboardPlayer");

Game.Debug = true;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "battlefrontii.exe";
Game.SteamID = "6060";
Game.GUID = "6060";
Game.GameName = "Star Wars: Battlefront 2";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "/win";
Game.HookNeeded = true;
Game.HookGameWindowName = "Star Wars Battlefront II";
Game.LauncherExe = "";

Game.Play = function () {
    //Context.SavePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Borderlands\\WillowGame\\Config\\WillowEngine.ini";
}