Game.DirSymlinkExclusions = [
    "bin"
];
Game.FileSymlinkExclusions = [
    "steam_api.ini",
	"steam_api.dll",
];
Game.GameName = "Don't Starve Together";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = false;
Game.ExecutableName = "dontstarve_steam.exe";
Game.SteamID = "322330";
Game.GUID = "322330";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "bin";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SupportsPositioning = true;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowName = "Don't Starve Together";
Game.Hook.DInputEnabled = false;
Game.Hook.DInputForceDisable = true;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;

Game.Play = function () {
 //   var savePkgOriginIni = System.IO.Path.Combine(Game.Folder, "steam_api.ini");
 //   var savePkgOrigin = System.IO.Path.Combine(Game.Folder, "steam_api.dll");

 //   var savePathIni = System.IO.Path.Combine(Context.RootFolder, "bin\\steam_api.ini");
 //   var savePath = System.IO.Path.Combine(Context.RootFolder, "bin\\steam_api.dll");

 //   System.IO.File.WriteAllLines(savePathIni, System.IO.File.ReadAllLines(savePkgOriginIni));
	//System.IO.File.AppendAllText(savePathIni, "AccountId=54321" + (Context.PlayerID + 1) + " " + System.Environment.NewLine);
	//System.IO.File.AppendAllText(savePathIni, "UserName=RaVeN" + Context.PlayerID);

 //   System.IO.File.Copy(savePkgOrigin, savePath, true);
	
    Context.StartArguments = "-windowed -novid -insecure -window -AlwaysFocus";
}