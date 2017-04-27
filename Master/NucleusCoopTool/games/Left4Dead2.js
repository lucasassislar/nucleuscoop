// List for all the maps we have a picture for/know the name and command
var listMaps = [
    { Name: "Dead Center", Details: "Hotel", Console: "c1m1_hotel", ImageUrl: "deadcenter.jpg" },
    { Name: "Dead Center", Details: "Streets", Console: "c1m2_streets", ImageUrl: "deadcenter.jpg" },
    { Name: "Dead Center", Details: "Mall", Console: "c1m3_mall", ImageUrl: "deadcenter.jpg" },
    { Name: "Dead Center", Details: "Atrium", Console: "c1m4_atrium", ImageUrl: "deadcenter.jpg" },
    { Name: "The Passing", Details: "Riverbank", Console: "c6m1_riverbank", ImageUrl: "" },
    { Name: "The Passing", Details: "Underground", Console: "c6m2_bedlam", ImageUrl: "" },
    { Name: "The Passing", Details: "Port", Console: "c6m1_port", ImageUrl: "" },
    { Name: "Dark Carnival", Details: "Atrium", Console: "c2m1_highway", ImageUrl: "darkcarnival.jpg" },
    { Name: "Dark Carnival", Details: "Atrium", Console: "c2m2_fairgrounds", ImageUrl: "darkcarnival.jpg" },
    { Name: "Dark Carnival", Details: "Atrium", Console: "c2m3_coaster", ImageUrl: "darkcarnival.jpg" },
    { Name: "Dark Carnival", Details: "Atrium", Console: "c2m4_barns", ImageUrl: "darkcarnival.jpg" },
    { Name: "Dark Carnival", Details: "Atrium", Console: "c2m5_concert", ImageUrl: "darkcarnival.jpg" },
    { Name: "Swamp Fever", Details: "Atrium", Console: "c3m1_plankcountry", ImageUrl: "swampfever.jpg" },
    { Name: "Swamp Fever", Details: "Atrium", Console: "c3m2_swamp", ImageUrl: "swampfever.jpg" },
    { Name: "Swamp Fever", Details: "Atrium", Console: "c3m3_shantytown", ImageUrl: "swampfever.jpg" },
    { Name: "Swamp Fever", Details: "Atrium", Console: "c3m4_plantation", ImageUrl: "swampfever.jpg" },
    { Name: "Hard Rain", Details: "Atrium", Console: "c4m1_milltown_a", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain", Details: "Atrium", Console: "c4m2_sugarmill_a", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain", Details: "Atrium", Console: "c4m3_sugarmill_b", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain", Details: "Atrium", Console: "c4m4_milltown_b", ImageUrl: "hardrain.jpg" },
    { Name: "Hard Rain", Details: "Atrium", Console: "c4m5_milltown_escape", ImageUrl: "hardrain.jpg" },
    { Name: "The Parish", Details: "Atrium", Console: "c5m1_waterfront", ImageUrl: "theparish.jpg" },
    { Name: "The Parish", Details: "Atrium", Console: "c5m2_park", ImageUrl: "theparish.jpg" },
    { Name: "The Parish", Details: "Atrium", Console: "c5m3_cemetery", ImageUrl: "theparish.jpg" },
    { Name: "The Parish", Details: "Atrium", Console: "c5m4_quarter", ImageUrl: "theparish.jpg" },
    { Name: "The Parish", Details: "Atrium", Console: "c5m5_bridge", ImageUrl: "theparish.jpg" },
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
        "GameMode", listGameModes),
    new Nucleus.GameOption(
        "Keyboard Player", "The player that will be playing on keyboard and mouse (if any)",
        "KeyboardPlayer", Nucleus.KeyboardPlayer.NoKeyboardPlayer),
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

    if (Context.PlayerID == 0) {
        var map = Context.Options["MapID"].Console;
        if (map != "")
        {
            lines.push("map " + map);
        }
    } else {
        lines.push("connect 192.168.15.10");
    }

    Context.WriteTextFile(autoExec, lines);
};