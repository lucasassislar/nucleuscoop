var listLanguages = [
    "english",
    "french",
    "german",
    "italian",
    "polish",
    "russian",
    "spanish",
    "hungarian",
    "turkish",
    "brazilian"
];

var defaultLanguage = Game.GetSteamLanguage();
Game.AddOption("Language",
    "The language the game will use",
    "Language", listLanguages, defaultLanguage);

Game.FileSymlinkExclusions = [
    "Engine.dll"
];
Game.ExecutableContext = [
    "physxcore.dll"
];

Game.GameName = "Gas Guzzlers Extreme";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.SteamID = "243800";
Game.GUID = "243800";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.INI;
Game.SupportsPositioning = true;
Game.ExecutableName = "gasguzzlers.exe";
Game.BinariesFolder = "bin32";
Game.Hook.ForceFocus = false;
Game.Hook.ForceFocusWindowName = "Gas Guzzlers: Extreme x64";
Game.Hook.DInputEnabled = false;
//Game.Hook.DInputForceDisable = true;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;
Game.Hook.XInputNames = [ "xinput9_1_0.dll" ];
//Game.PauseBetweenStarts = 30;
//Game.LockMouse = true;

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
    var path = "%USERPROFILE%\\Documents\\Gamepires\\GasGuzzlersExtreme\\GasGuzzlers.xml";
    Context.ChangeXmlAttributeValue(path, "//Var[contains(@name,'SkipIntro')]", "value", "1");
    Context.ChangeXmlAttributeValue(path, "//Var[contains(@name,'FullScreen')]", "value", "0");
    Context.ChangeXmlAttributeValue(path, "//Var[contains(@name,'ScreenWidth')]", "value", Context.Width);
    Context.ChangeXmlAttributeValue(path, "//Var[contains(@name,'ScreenHeight')]", "value", Context.Height);
    Context.ChangeXmlAttributeValue(path, "//Var[contains(@name,'WindowPosX')]", "value", Player.MonitorBounds.X);
    Context.ChangeXmlAttributeValue(path, "//Var[contains(@name,'WindowPosY')]", "value", Player.MonitorBounds.Y);

    //patch dll
    //patch GetRunWhileNotInFocus
    Context.PatchFile(System.IO.Path.Combine(Context.RootInstallFolder, "Engine.dll"),
        System.IO.Path.Combine(Context.RootFolder, "Bin32\\Engine.dll"),
        //8A 81 31 03 00 00 C3
        [0x8A, 0x81, 0x31, 0x03, 0x00, 0x00, 0xC3],
        [0xB0, 0x01, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC]);
}