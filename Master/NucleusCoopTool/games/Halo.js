Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
];
//Game.ExecutableContext = [
//    "binkw32.dll"
//];
//Game.KillMutex = [
//    "SR3"
//];

Game.Debug = true;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "halo.exe";
Game.SteamID = "";
Game.GUID = "HALOCE";
Game.GameName = "Halo: Combat Evolved";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.None;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.StartArguments = "-window";
Game.Hook.DInputEnabled = true;

Game.Play = function () {
    //Context.ModifySave = [
    //   new Nucleus.IniSaveInfo("", "ResolutionWidth", Context.Width),
    //   new Nucleus.IniSaveInfo("", "ResolutionHeight", Context.Height),
    //   new Nucleus.IniSaveInfo("", "Fullscreen", false),
    //   new Nucleus.IniSaveInfo("", "VerifyResolution", false),
    //   new Nucleus.IniSaveInfo("", "SkipIntroVideo", true),
    //];
    //Context.SavePath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\display.ini";
}