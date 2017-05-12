Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
];
Game.ExecutableContext = [
    "binkw32.dll"
];
Game.KillMutex = [
    "SR3"
];

Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "saintsrowthethird_dx11.exe";
Game.SteamID = "55230";
Game.GUID = "55230";
Game.GameName = "Saints Row 3 (DX11)";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.StartArguments = "";
Game.LauncherExe = "game_launcher.exe";
Game.XInput.ForceFocus = true;
Game.XInput.ForceFocusWindowName = "Saints Row: The Third";
Game.XInput.DInputEnabled = false;
Game.XInput.XInputEnabled = true;
Game.XInput.XInputReroute = true;

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