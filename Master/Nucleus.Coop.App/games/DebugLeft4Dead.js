var listMaps = [
    // Left 4 Dead 1 campaigns
    { Name: "No Mercy", Details: "1. Apartments", Console: "c8m1_apartment", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy", Details: "2. Subway", Console: "c8m2_subway", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy", Details: "3. Sewer", Console: "c8m3_sewers", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy", Details: "4. Hospital", Console: "c8m4_interior", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy", Details: "5. Rooftop", Console: "c8m5_rooftop", ImageUrl: "nomercy.jpg" },
    { Name: "Crash Course", Details: "1. The Alleys", Console: "c9m1_alleys", ImageUrl: "crashcourse.jpg" },
    { Name: "Crash Course", Details: "2. The Truck Depot Finale", Console: "c9m2_lots", ImageUrl: "crashcourse.jpg" },
    { Name: "Death Toll", Details: "1. The Turnpike", Console: "c10m1_caves", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll", Details: "2. The Drains", Console: "c10m2_drainage", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll", Details: "3. The Church", Console: "c10m3_ranchhouse", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll", Details: "4. The Town", Console: "c10m4_mainstreet", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll", Details: "5. Boathouse Finale", Console: "c10m5_houseboat", ImageUrl: "deathtoll.jpg" },
    { Name: "Dead Air", Details: "1. The Greenhouse", Console: "c11m1_greenhouse", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air", Details: "2. The Crane", Console: "c11m2_offices", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air", Details: "3. The ConstructionSite", Console: "c11m3_garage", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air", Details: "4. The Terminal", Console: "c11m4_terminal", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air", Details: "5. Runaway Finale", Console: "c11m5_runway", ImageUrl: "deadair.jpg" },
    { Name: "Blood Harvest", Details: "1. The Woods", Console: "c12m1_hilltop", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest", Details: "2. The Tunnel", Console: "c12m2_traintunnel", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest", Details: "3. The Bridge", Console: "c12m3_bridge", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest", Details: "4. The Train Station", Console: "c12m4_barn", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest", Details: "5. Farmhouse Finale", Console: "c12m5_cornfield", ImageUrl: "bloodharvest.jpg" },
    { Name: "Cold Stream", Details: "1. Alpine Creek", Console: "c13m1_alpinecreek", ImageUrl: "coldstream.png" },
    { Name: "Cold Stream", Details: "2. South Pine Stream", Console: "c13m2_southpinestream", ImageUrl: "coldstream.png" },
    { Name: "Cold Stream", Details: "3. Memorial Bridge", Console: "c13m3_memorialbridge", ImageUrl: "coldstream.png" },
    { Name: "Cold Stream", Details: "4. Cut-throat Creek", Console: "c13m4_cutthroatcreek", ImageUrl: "coldstream.png" },

    { Name: "No Map (unsupported)", Details: "None", Console: "none", ImageUrl: "" }
];
// List of all the game modes
var listGameModes = [
    "campaign",
    "realism",
    "survival",
    "versus",
    "none"
];

// List all our game options before trying to write code that uses them
Game.AddOption("Map", "The map the game will use",
    "MapID", listMaps);
Game.AddOption("Game Mode", "The game mode",
    "GameMode", listGameModes);

var MapStep = Game.ShowOptionAsStep("MapID", true, "Choose a Campaign");
// This doesn't work yet
//var CustomMapStep = Game.ShowOptionAsStep("CustomMap", false);
//CustomMapStep.UpdateRequired = function () {
//    CustomMapStep.Required = (Context.Options["MapID"].Value.Console == "custom");
//};

Game.KillMutex = [ // 2nd instance won't launch without these removed
    "hl2_singleton_mutex",
    "steam_singleton_mutext"
];
Game.DirSymlinkExclusions = [
    "left4dead\\cfg",
];
Game.FileSymlinkExclusions = [
    "autoexec.cfg",
    "video.txt",
    "config.cfg"
];

Game.Debug = true;
Game.HandlerInterval = 100; // 10 FPS handler
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "left4dead.exe";
Game.SteamID = "500";
Game.GUID = "500";
Game.GameName = "Left 4 Dead";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.CFG;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.WorkingFolder = "bin";
Game.StartArguments = "-novid -insecure -window";
Game.MaxPlayersOneMonitor = 8;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Left 4 Dead";
Game.MaxPlayers = 4;
Game.Hook.DInputEnabled = false;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;

Game.Play = function () {
    // Only enable setting the window size on the XInput hook dll
    // when its dual vertical, as it doenst work 100% of the time on DualHorizontal
    Context.Hook.SetWindowSize = Player.Owner.IsDualVertical();
    Context.Hook.ForceFocus = !Player.IsKeyboardPlayer;

    var saveSrc = System.IO.Path.Combine(Context.RootInstallFolder, "left4dead\\cfg\\video.txt");
    var savePath = System.IO.Path.Combine(Context.RootFolder, "left4dead\\cfg\\video.txt");
    Context.ModifySaveFile(saveSrc, savePath, Nucleus.SaveType.CFG, [
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.fullscreen", "0"),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultres", Math.max(640, Context.Width)),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultresheight", Math.max(360, Context.Height)),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ]);

    //copy config.cfg
    System.IO.File.Copy(System.IO.Path.Combine(Context.RootInstallFolder, "left4dead\\cfg\\config.cfg"),
        System.IO.Path.Combine(Context.RootFolder, "left4dead\\cfg\\config.cfg"),
        true);

    var autoExec = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\left4dead\\cfg\\autoexec.cfg";
    var lines = [
        "sv_lan 1",
        "sv_allow_lobby_connect_only 0",
        "net_graph 1",
        "engine_no_focus_sleep 0" // unlimited FPS on all screens
    ];

    if (Player.IsKeyboardPlayer) {
        lines.push("joystick 0");
        lines.push("exec undo360controller.cfg");
    }
    else {
        lines.push("exec 360controller.cfg");
    }

    if (Context.PlayerID == 0) {
        var map = Context.Options["MapID"].Console;
        var gameMode = Context.Options["GameMode"];
        if (map != "") {
            lines.push("map " + map + " " + gameMode);
        }
    } else {
        lines.push("connect " + Context.User.GetLocalIP());
    }

    Context.WriteTextFile(autoExec, lines);
};