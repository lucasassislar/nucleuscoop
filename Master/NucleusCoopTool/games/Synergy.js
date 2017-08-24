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

Game.HandlerInterval = 100; // 10 FPS handler
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "synergy.exe";
Game.SteamID = "17520";
Game.GUID = "17520";
Game.GameName = "Synergy";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.CFG;
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

// this game will multiply the values on the creators Update
// ... but is it only in the creators update?
Game.DPIHandling = Nucleus.DPIHandling.InvScaled; 

Game.Play = function () {
    // Only enable setting the window size on the XInput hook dll
    // when its dual vertical, as it doenst work 100% of the time on DualHorizontal
    Context.Hook.SetWindowSize = Player.Owner.IsDualVertical();
    Context.Hook.ForceFocus = !Player.IsKeyboardPlayer;

    var saveSrc = System.IO.Path.Combine(Context.RootInstallFolder, "synergy\\cfg\\video.txt");
    var savePath = System.IO.Path.Combine(Context.RootFolder, "synergy\\cfg\\video.txt");
    Context.ModifySaveFile(saveSrc, savePath, Nucleus.SaveType.CFG, [
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.fullscreen", "0"),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultres", Math.max(640, Context.Width)),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultresheight", Math.max(360, Context.Height)),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ]);
    
    //copy config.cfg
    System.IO.File.Copy(System.IO.Path.Combine(Context.RootInstallFolder, "synergy\\cfg\\config.cfg"),
        System.IO.Path.Combine(Context.RootFolder, "synergy\\cfg\\config.cfg"),
        true);

    var autoExec = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\synergy\\cfg\\autoexec.cfg";
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
};