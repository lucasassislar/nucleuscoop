Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
];
Game.KillMutex = [ // 2nd instance won't launch without these removed
    "hl2_singleton_mutex",
    "steam_singleton_mutext"
];
Game.DirSymlinkExclusions = [
    "left4dead2\\cfg",
];
Game.FileSymlinkExclusions =[
    "autoexec.cfg",
    "video.txt"
];

Game.Debug = true;
Game.HandlerInterval = 16;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "left4dead2.exe";
Game.SteamID = "550";
Game.GUID = "550";
Game.GameName = "Left 4 Dead 2";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.CFG;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.WorkingFolder = "bin";
// -insecure will get enabled anyway, but we add it just to make sure;
// omitting it could result in VAC bans
Game.StartArguments = "-novid -insecure -window";
Game.XInput.DInputEnabled = true;
Game.XInput.XInputEnabled = false;
Game.XInput.ForceFocus = true;
Game.XInput.ForceFocusWindowName = "Left 4 Dead 2";

Game.Play = function () {
    // Only enable setting the window size on the XInput hook dll
    // when its dual vertical, as it doenst work 100% of the time on DualHorizontal
    Context.XInput.SetWindowSize = Player.Owner.IsDualVertical();

    var saveSrc = System.IO.Path.Combine(Context.RootInstallFolder, "left4dead2\\cfg\\video.txt");
    var savePath = System.IO.Path.Combine(Context.RootFolder, "left4dead2\\cfg\\video.txt");
    Context.ModifySaveFile(saveSrc, savePath, Nucleus.SaveType.CFG, [
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.fullscreen", "0"),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultres", Context.Width),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultresheight", Context.Height),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ]);

    var autoExec = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\left4dead2\\cfg\\autoexec.cfg";
    var lines = [
        "sv_lan 1",
        "sv_allow_lobby_connect_only 0"
    ];

    if (Context.IsKeyboardPlayer) {
        Handler.StartPlayTick(1, function () {
            Handler.CenterCursor();
        });
        lines.push("joystick 0");
    }
    else {
        lines.push("exec 360controller.cfg");
    }

    if (Context.PlayerID != 0) {
        lines.push("bind \"BACK\" \"connect 192.168.15.10\"");
    }

    // To play the game:
    // Open console on first player, type "map nameOfMapHere gameType"
    // Open console on other players, press Back
    Context.WriteTextFile(autoExec, lines);
};