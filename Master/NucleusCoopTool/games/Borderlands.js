Game.ExecutableContext = [ // need to add these or it might conflict with Tales of the Borderlands
    "PhysXLocal",
    "binkw32.dll"
];

// temporarily disabled keyboard support
Game.SupportsKeyboard = false;
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SteamID = "8980";
Game.GUID = "8980";
Game.GameName = "Borderlands";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "splashscreen";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.StartArguments = "-windowed -NoLauncher -nostartupmovies";
Game.ExecutableName = "borderlands.exe";
Game.BinariesFolder = "binaries";
Game.XInput.DInputEnabled = false;
Game.XInput.XInputEnabled = true;
Game.XInput.ForceFocus = true;
Game.XInput.ForceFocusWindowName = "Borderlands";

Game.Play = function () {
    // block all mouse and keyboard input for the player that
    // isnt the keyboard one
    // (Borderlands 1 NEEDS this, else it will lose focus)
    var isKeyboard = Player.IsKeyboardPlayer;
    Context.XInput.BlockMouseEvents = !isKeyboard;
    Context.XInput.BlockKeyboardEvents = !isKeyboard;

    var savePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\My Games\\Borderlands\\WillowGame\\Config\\WillowEngine.ini";
    Context.ModifySaveFile(savePath, savePath, Nucleus.SaveType.INI, [
        new Nucleus.IniSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
        new Nucleus.IniSaveInfo("SystemSettings", "ResX", Context.Width),
        new Nucleus.IniSaveInfo("SystemSettings", "ResY", Context.Height),
        new Nucleus.IniSaveInfo("SystemSettings", "Fullscreen", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", false)
    ]);
}