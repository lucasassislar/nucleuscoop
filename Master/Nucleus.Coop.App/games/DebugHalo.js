Game.AddOption("Keyboard Player",
    "The player that will be playing on keyboard and mouse (if any)",
    "KeyboardPlayer",
    Nucleus.KeyboardPlayer.NoKeyboardPlayer);
//Game.ExecutableContext = [
//    "binkw32.dll"
//];
//Game.KillMutex = [
//    "SR3"
//];

Game.Debug = true;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
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
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Halo";
Game.Hook.DInputEnabled = true;
Game.Hook.XInputEnabled = false;
Game.Hook.XInputReroute = false;

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