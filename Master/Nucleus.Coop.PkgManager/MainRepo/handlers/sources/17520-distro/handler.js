// Made by d1maxa, modified by distrolucas

Game.KillMutex = [ // 2nd instance won't launch without these removed
    "hl2_singleton_mutex"
];
Game.DirSymlinkExclusions = [
    "synergy\\cfg"
];
Game.FileSymlinkExclusions = [
    "autoexec.cfg",
    "video.txt",
    "config.cfg"
];

Game.SteamID = "17520";
Game.GameID = "17520";
Game.HandlerInterval = 100; // 10 FPS handler
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "synergy.exe";
Game.GameName = "Synergy";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = SaveType.CFG;
Game.SupportsPositioning = true;
Game.WorkingFolder = "bin";
Game.StartArguments = "-game synergy -novid -insecure -window -maxplayers 8";
Game.MaxPlayersOneMonitor = 8;
Game.MaxPlayers = 8;
Game.Hook.ForceFocus = false;
Game.Hook.ForceFocusWindowName = "Synergy";
Game.Hook.DInputEnabled = false;
Game.Hook.DInputForceDisable = true;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;
Game.PauseBetweenStarts = 30;
Game.LockMouse = true;
Game.Hook.XInputNames = ["xinput1_4.dll"]; // having issues here

// this game will multiply the values on the creators Update
// ... but is it only in the creators update?
Game.DPIHandling = DPIHandling.InvScaled; 

Game.OnPlay.Callback(function () {
    // Only enable setting the window size on the XInput hook dll
    // when its dual vertical, as it doenst work 100% of the time on DualHorizontal
    Context.Hook.SetWindowSize = Player.Owner.IsDualVertical();
    //Context.Hook.ForceFocus = !Player.IsKeyboardPlayer; // force focus loses mouse

    var saveSrc = Context.CombinePath(Context.InstallFolder, "synergy\\cfg\\video.txt");
    var savePath = Context.CombinePath(Context.InstanceFolder, "synergy\\cfg\\video.txt");
    Context.ModifySaveFile(saveSrc, savePath, SaveType.CFG, [
        Context.NewCfgSaveInfo("VideoConfig", "setting.fullscreen", "0"),
        Context.NewCfgSaveInfo("VideoConfig", "setting.defaultres", Math.max(640, Context.Width)),
        Context.NewCfgSaveInfo("VideoConfig", "setting.defaultresheight", Math.max(360, Context.Height)),
        Context.NewCfgSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ]);

    //copy config.cfg
    Context.CopyFile(Context.CombinePath(Context.InstallFolder, "synergy\\cfg\\config.cfg"),
        Context.CombinePath(Context.RootFolder, "synergy\\cfg\\config.cfg"),
        true);

    var autoExec = Context.CombinePath(Context.InstanceFolder, "\\synergy\\cfg\\autoexec.cfg");
    var lines = [
        "sv_lan 1",
        "engine_no_focus_sleep 0" // unlimited FPS on all screens
    ];

    if (Context.PlayerID == 0) {
        lines.push("map syn_takeover");
    } else {
        lines.push("connect " + Context.User.GetLocalIP());
    }

    if (Player.IsKeyboardPlayer) {
        lines.push("exec undo360controller.cfg");
    }
    else {
        lines.push("exec 360controller.cfg");
    }

    Context.WriteTextFile(autoExec, lines);
});