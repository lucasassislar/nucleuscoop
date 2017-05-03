// List for all the maps we have a picture for/know the name and command
var listMaps = [
    // Left 4 Dead 2 campaigns
    { Name: "Dead Center",      Details: "1. Hotel", Console: "c1m1_hotel", ImageUrl: "deadcenter.jpg" },
    { Name: "Dead Center",      Details: "2. Streets", Console: "c1m2_streets", ImageUrl: "deadcenter.jpg" },
    { Name: "Dead Center",      Details: "3. Mall", Console: "c1m3_mall", ImageUrl: "deadcenter.jpg" },
    { Name: "Dead Center",      Details: "4. Atrium", Console: "c1m4_atrium", ImageUrl: "deadcenter.jpg" },
    { Name: "Dark Carnival",    Details: "1. Highway", Console: "c2m1_highway", ImageUrl: "darkcarnival.png" },
    { Name: "Dark Carnival",    Details: "2. Faigrounds", Console: "c2m2_fairgrounds", ImageUrl: "darkcarnival.png" },
    { Name: "Dark Carnival",    Details: "3. Coaster", Console: "c2m3_coaster", ImageUrl: "darkcarnival.png" },
    { Name: "Dark Carnival",    Details: "4. Barns", Console: "c2m4_barns", ImageUrl: "darkcarnival.png" },
    { Name: "Dark Carnival",    Details: "5. Concert", Console: "c2m5_concert", ImageUrl: "darkcarnival.png" },
    { Name: "Swamp Fever",      Details: "1. Plank Country", Console: "c3m1_plankcountry", ImageUrl: "swampfever.jpg" },
    { Name: "Swamp Fever",      Details: "2. Swamp", Console: "c3m2_swamp", ImageUrl: "swampfever.jpg" },
    { Name: "Swamp Fever",      Details: "3. Shanty Town", Console: "c3m3_shantytown", ImageUrl: "swampfever.jpg" },
    { Name: "Swamp Fever",      Details: "4. Plantation", Console: "c3m4_plantation", ImageUrl: "swampfever.jpg" },
    { Name: "Hard Rain",        Details: "1. Milltown", Console: "c4m1_milltown_a", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain",        Details: "2. Sugar Mill", Console: "c4m2_sugarmill_a", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain",        Details: "3. Mill Escape", Console: "c4m3_sugarmill_b", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain",        Details: "4. Return to Town", Console: "c4m4_milltown_b", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain",        Details: "5. Town Escape", Console: "c4m5_milltown_escape", ImageUrl: "hardrain.jpg" },
    { Name: "The Parish",       Details: "1. Waterfront", Console: "c5m1_waterfront", ImageUrl: "theparish.jpg" },
    { Name: "The Parish",       Details: "2. Park", Console: "c5m2_park", ImageUrl: "theparish.jpg" },
    { Name: "The Parish",       Details: "3. Cemetery", Console: "c5m3_cemetery", ImageUrl: "theparish.jpg" },
    { Name: "The Parish",       Details: "4. Quarter", Console: "c5m4_quarter", ImageUrl: "theparish.jpg" },
    { Name: "The Parish",       Details: "5. Bridge", Console: "c5m5_bridge", ImageUrl: "theparish.jpg" },

    // DLC
    { Name: "The Passing",      Details: "1. Riverbank", Console: "c6m1_riverbank", ImageUrl: "thepassing.jpg" },
    { Name: "The Passing",      Details: "2. Underground", Console: "c6m2_bedlam", ImageUrl: "thepassing.jpg" },
    { Name: "The Passing",      Details: "3. Port", Console: "c6m1_port", ImageUrl: "thepassing.jpg" },
    { Name: "The Sacrifice",    Details: "1. Docks", Console: "c7m1_docks", ImageUrl: "thesacrifice.jpg" },
    { Name: "The Sacrifice",    Details: "2. Barge", Console: "c7m2_barge", ImageUrl: "thesacrifice.jpg" },
    { Name: "The Sacrifice",    Details: "3. Port", Console: "c7m3_port", ImageUrl: "thesacrifice.jpg" },

    // Left 4 Dead 1 campaigns
    { Name: "No Mercy",         Details: "1. Apartments", Console: "c8m1_apartment", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy",         Details: "2. Subway", Console: "c8m2_subway", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy",         Details: "3. Sewer", Console: "c8m3_sewers", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy",         Details: "4. Hospital", Console: "c8m4_interior", ImageUrl: "nomercy.jpg" },
    { Name: "No Mercy",         Details: "5. Rooftop", Console: "c8m5_rooftop", ImageUrl: "nomercy.jpg" },
    { Name: "Crash Course",     Details: "1. The Alleys", Console: "c9m1_alleys", ImageUrl: "crashcourse.jpg" },
    { Name: "Crash Course",     Details: "2. The Truck Depot Finale", Console: "c9m2_lots", ImageUrl: "crashcourse.jpg" },
    { Name: "Death Toll",       Details: "1. The Turnpike", Console: "c10m1_caves", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll",       Details: "2. The Drains", Console: "c10m2_drainage", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll",       Details: "3. The Church", Console: "c10m3_ranchhouse", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll",       Details: "4. The Town", Console: "c10m4_mainstreet", ImageUrl: "deathtoll.jpg" },
    { Name: "Death Toll",       Details: "5. Boathouse Finale", Console: "c10m5_houseboat", ImageUrl: "deathtoll.jpg" },
    { Name: "Dead Air",         Details: "1. The Greenhouse", Console: "c11m1_greenhouse", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air",         Details: "2. The Crane", Console: "c11m2_offices", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air",         Details: "3. The ConstructionSite", Console: "c11m3_garage", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air",         Details: "4. The Terminal", Console: "c11m4_terminal", ImageUrl: "deadair.jpg" },
    { Name: "Dead Air",         Details: "5. Runaway Finale", Console: "c11m5_runway", ImageUrl: "deadair.jpg" },
    { Name: "Blood Harvest",    Details: "1. The Woods", Console: "c12m1_hilltop", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest",    Details: "2. The Tunnel", Console: "c12m2_traintunnel", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest",    Details: "3. The Bridge", Console: "c12m3_bridge", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest",    Details: "4. The Train Station", Console: "c12m4_barn", ImageUrl: "bloodharvest.jpg" },
    { Name: "Blood Harvest",    Details: "5. Farmhouse Finale", Console: "c12m5_cornfield", ImageUrl: "bloodharvest.jpg" },
    { Name: "Cold Stream",      Details: "1. Alpine Creek", Console: "c13m1_alpinecreek", ImageUrl: "coldstream.png" },
    { Name: "Cold Stream",      Details: "2. South Pine Stream", Console: "c13m2_southpinestream", ImageUrl: "coldstream.png" },
    { Name: "Cold Stream",      Details: "3. Memorial Bridge", Console: "c13m3_memorialbridge", ImageUrl: "coldstream.png" },
    { Name: "Cold Stream",      Details: "4. Cut-throat Creek", Console: "c13m4_cutthroatcreek", ImageUrl: "coldstream.png" },

    { Name: "No Map (unsupported)", Details: "None", Console: "none", ImageUrl: "" }
];
// List of all the game modes
var listGameModes = [
    "campaign",
    "scavenge",
    "realism",
    "survival",
    "versus",
    "mutation",
    "none"
];

// List all our game options before trying to write code that uses them
Game.Options = [
    // these 2 are going to be shown as steps
    new Nucleus.GameOption(
        "Map", "The map the game will use",
        "MapID", listMaps),
    new Nucleus.GameOption(
        "Game Mode", "The game mode",
        "GameMode", listGameModes)
];

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
    "left4dead2\\cfg",
];
Game.FileSymlinkExclusions = [
    "autoexec.cfg",
    "video.txt"
];

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
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultres", Math.max(640, Context.Width)),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultresheight", Math.max(360, Context.Height)),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ]);

    var autoExec = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\left4dead2\\cfg\\autoexec.cfg";
    var lines = [
        "sv_lan 1",
        "sv_allow_lobby_connect_only 0",
        "net_graph 1",
        "engine_no_focus_sleep 0" // unlimited FPS on all screens
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