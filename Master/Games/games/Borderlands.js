Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
];
Game.ExecutableContext = [ // need to add these or it might conflict with Tales of the Borderlands
    "PhysXLocal",
    "binkw32.dll"
];

Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "borderlands.exe";
Game.SteamID = "8980";
Game.GUID = "8980";
Game.GameName = "Borderlands";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "binaries";
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "splashscreen";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "-windowed -NoLauncher -nostartupmovies";
Game.HookNeeded = true;
Game.HookWindows = 7;
Game.HookGameWindow = 1;

Game.Play = function () {
    Context.ModifySave = [
        new Nucleus.IniSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
        new Nucleus.IniSaveInfo("SystemSettings", "ResX", Context.Width),
        new Nucleus.IniSaveInfo("SystemSettings", "ResY", Context.Height),
        new Nucleus.IniSaveInfo("SystemSettings", "Fullscreen", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", false)
    ];

    Context.SavePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Borderlands\\WillowGame\\Config\\WillowEngine.ini";
}