Game.PlatformVersion = 10; // Nucleus Coop 10
Game.SteamID = "239140";
Game.GameID = "239140";
Game.GameName = "Dying Light";
Game.HandlerInterval = 100;
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "dyinglightgame.exe";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = false;
Game.SaveType = SaveType.SCR;
Game.SupportsPositioning = true;
Game.HideTaskbar = true;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Dying Light";
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;// true; // this is beta
Game.Hook.XInputNames = ["xinput1_3.dll"];

Game.OnPlay.Callback(function () {
    var savePath = Context.GetFolder(Folder.Documents) + "\\DyingLight\\out\\settings\\video.scr";
    Context.ModifySaveFile(savePath, savePath, SaveType.SCR, [
        Context.NewScrSaveInfo("Resolution", Context.Width, Context.Height),
        Context.NewScrSaveInfo("Fullscreen", "false"),
    ]);
});