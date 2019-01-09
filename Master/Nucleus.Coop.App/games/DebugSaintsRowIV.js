Game.AddOption("Keyboard Player",
    "The player that will be playing on keyboard and mouse (if any)",
    "KeyboardPlayer",
    Nucleus.KeyboardPlayer.NoKeyboardPlayer);
Game.ExecutableContext = [
    "binkw32.dll"
];
Game.KillMutex = [
    "SRE4"
];
Game.FileSymlinkExclusions = [
    "display.ini"
];

Game.Debug = true;
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "saintsrowiv.exe";
Game.SteamID = "206420";
Game.GUID = "206420";
Game.GameName = "Saints Row IV";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.StartArguments = "";
Game.LauncherExe = "";
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Saints Row IV";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;

Game.Play = function () {
    var savePath = Context.SavePath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\display.ini";
    Context.ModifySaveFile(savePath, savePath, Nucleus.SaveType.INI, [
       new Nucleus.IniSaveInfo("", "ResolutionWidth", Context.Width),
       new Nucleus.IniSaveInfo("", "ResolutionHeight", Context.Height),
       new Nucleus.IniSaveInfo("", "Fullscreen", false),
       new Nucleus.IniSaveInfo("", "VerifyResolution", false),
       new Nucleus.IniSaveInfo("", "SkipIntroVideo", true),
    ]);
}