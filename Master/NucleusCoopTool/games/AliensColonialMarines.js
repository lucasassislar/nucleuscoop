var listLanguages = [
    "english",
    "french",
    "german",
    "italian",
    "polish",
    "russian",
    "spanish"
];

var defaultLanguage = Game.GetSteamLanguage();
// List all our game options before trying to write code that uses them
Game.AddOption("Language", "The language the game will use",
    "Language", listLanguages, defaultLanguage);

Game.SupportsKeyboard = true;
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SteamID = "49540";
Game.GUID = "49540";
Game.GameName = "Aliens: Colonial Marines";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "splashscreen";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.ExecutableName = "acm.exe";
Game.BinariesFolder = "binaries\\win32";
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowName = "Aliens: Colonial Marines - PC";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;
Game.PauseBetweenStarts = 30;
Game.LockMouse = true;

Game.SetupSse = function() {
    var sseIni = System.IO.Path.Combine(Context.RootFolder, "SmartSteamLoader\\SmartSteamEmu.ini");
    var language = Context.Options["Language"];
    var isKeyboard = Player.IsKeyboardPlayer;
    var name = isKeyboard ? "AccountName" : "Player " + (Context.PlayerID + 1)
    Context.ModifySaveFile(sseIni, sseIni, Nucleus.SaveType.INI, [
        //new Nucleus.IniSaveInfo("Launcher", "ParanoidMode", 1),
        new Nucleus.IniSaveInfo("SmartSteamEmu", "Language", language),
        new Nucleus.IniSaveInfo("SSEOverlay", "DisableOverlay", 1),
        new Nucleus.IniSaveInfo("SSEOverlay", "OnlineMode", 0),
        new Nucleus.IniSaveInfo("SSEOverlay", "Language", language),
        new Nucleus.IniSaveInfo("SmartSteamEmu", "SteamIdGeneration", "PersonaName"),
        new Nucleus.IniSaveInfo("SmartSteamEmu", "PersonaName", name),
        new Nucleus.IniSaveInfo("SmartSteamEmu", "SeparateStorageByName", 1),
        //new Nucleus.IniSaveInfo("Debug", "EnableLog", 1)
    ]);
}

Game.Play = function () {
    var isKeyboard = Player.IsKeyboardPlayer;
    //to mouse work only in main window
    Context.Hook.ForceFocus = isKeyboard;
    Context.Hook.BlockMouseEvents = !isKeyboard;
    Context.Hook.BlockKeyboardEvents = !isKeyboard;

    var args = isKeyboard ? "-NoController" : "-nomouse";
    
    Context.StartArguments = System.String.Format("-windowed -AlwaysFocus {0} -ResX={1} -ResY={2}", args, Context.Width, Context.Height);
    
    var savePath = Context.GetFolder(Nucleus.Folder.Documents) + "\\my games\\Aliens Colonial Marines\\PecanGame\\Config\\\PecanEngine.ini";
    Context.ModifySaveFile(savePath, savePath, Nucleus.SaveType.INI, [
        new Nucleus.IniSaveInfo("SystemSettings", "WindowedFullscreen", Context.IsFullscreen),
        new Nucleus.IniSaveInfo("SystemSettings", "ResX", Context.Width),
        new Nucleus.IniSaveInfo("SystemSettings", "ResY", Context.Height),
        new Nucleus.IniSaveInfo("SystemSettings", "Fullscreen", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bPauseOnLossOfFocus", false),
        new Nucleus.IniSaveInfo("PecanGame.PecanGameEngine", "bPauseLostFocusWindowed", false),
        new Nucleus.IniSaveInfo("Engine.Engine", "bMuteAudioWhenNotInFocus", false),
        new Nucleus.IniSaveInfo("PecanGame.PecanGameEngine", "bMuteAudioWhenNotInFocus", false),
        new Nucleus.IniSaveInfo("FullScreenMovie", "bForceNoMovies", true),
    ]);
}