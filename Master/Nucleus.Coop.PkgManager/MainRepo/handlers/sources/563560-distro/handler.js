// Made by d1maxa, modified by distrolucas

// List for all the maps we have a picture for/know the name and command
var listMaps = [
    // Alien Swarm Reactive Drop campaigns
    { Name: "Jacob's Rest", Details: "1. Landing Bay", Console: "ASI-Jac1-LandingBay_01", ImageUrl: "oblandinghack.jpg" },
    { Name: "Jacob's Rest", Details: "2. Cargo Elevator", Console: "ASI-Jac1-LandingBay_02", ImageUrl: "oblandingelev.jpg" },
    { Name: "Jacob's Rest", Details: "3. Deima Surface Bridge", Console: "ASI-Jac2-Deima", ImageUrl: "obdeimaesc.jpg" },
    { Name: "Jacob's Rest", Details: "4. Rydberg Reactor", Console: "ASI-Jac3-Rydberg", ImageUrl: "obrydbergreactor.jpg" },
    { Name: "Jacob's Rest", Details: "5. SynTek Residential", Console: "ASI-Jac4-Residential", ImageUrl: "obresidentialbio.jpg" },
    { Name: "Jacob's Rest", Details: "6. Sewer Junction B5", Console: "ASI-Jac6-SewerJunction", ImageUrl: "obsewertunnel.jpg" },
    { Name: "Jacob's Rest", Details: "7. Timor Station", Console: "ASI-Jac7-TimorStation", ImageUrl: "obtimorstationnuke.jpg" },

    { Name: "Area 9800", Details: "1. Landing Zone", Console: "rd-area9800LZ", ImageUrl: "rd-area9800LZpic.jpg" },
    { Name: "Area 9800", Details: "2. Power Plant's Cooling Pump", Console: "rd-area9800PP1", ImageUrl: "rd-area9800PP1pic.jpg" },
    { Name: "Area 9800", Details: "3. Power Plant's Generator", Console: "rd-area9800PP2", ImageUrl: "rd-area9800PP2pic.jpg" },
    { Name: "Area 9800", Details: "4. Wastelands", Console: "rd-area9800WL", ImageUrl: "rd-area9800WLpic.jpg" },

    { Name: "Lana's Escape", Details: "1. Bridge", Console: "rd-lan1_bridge", ImageUrl: "rd-lan1_bridgempic.jpg" },
    { Name: "Lana's Escape", Details: "2. Sewer", Console: "rd-lan2_sewer", ImageUrl: "rd-lan2_sewermpic.jpg" },
    { Name: "Lana's Escape", Details: "3. Maintenance", Console: "rd-lan3_maintenance", ImageUrl: "rd-lan3_maintenance_obj_3.jpg" },
    { Name: "Lana's Escape", Details: "4. Platform Bay", Console: "rd-lan4_vent", ImageUrl: "rd-lan4_ventmpic.jpg" },
    { Name: "Lana's Escape", Details: "5. Complex", Console: "rd-lan5_complex", ImageUrl: "rd-lan5_complexmpic.jpg" },

    { Name: "Operation Cleansweep", Details: "1. Storage Facility", Console: "rd-ocs1storagefacility", ImageUrl: "rd_ocs1mapicon.jpg" },
    { Name: "Operation Cleansweep", Details: "2. Landing Bay 7", Console: "rd-ocs2landingbay7", ImageUrl: "rd_ocs2mapicon.jpg" },
    { Name: "Operation Cleansweep", Details: "3. U.S.C. Medusa", Console: "rd-ocs3uscmedusa", ImageUrl: "rd_ocs3mapicon.jpg" },

    { Name: "Orion's Threat", Details: "1. Niosa Refinery", Console: "rd-ori1niosarefinary", ImageUrl: "adanaxis1_access_obj_image.jpg" },
    { Name: "Orion's Threat", Details: "2. First Anomaly", Console: "rd-ori2firstanomaly", ImageUrl: "rd_ori2.jpg" },

    { Name: "Paranoia", Details: "1. Unexpected Encounter", Console: "rd-par1unexpected_encounter", ImageUrl: "rd_par_objectif1.jpg" },
    { Name: "Paranoia", Details: "2. Hostile Places", Console: "rd-par2hostile_places", ImageUrl: "rd_par_mission2.jpg" },
    { Name: "Paranoia", Details: "3. Close Contact", Console: "rd-par3close_contact", ImageUrl: "rd_par_objectif23.jpg" },
    { Name: "Paranoia", Details: "4. High Tension", Console: "rd-par4high_tension", ImageUrl: "rd_par_objectif33.jpg" },
    { Name: "Paranoia", Details: "5. Crucial Point", Console: "rd-par5crucial_point", ImageUrl: "rd_par_objectif32.jpg" },

    { Name: "Research 7", Details: "1. Transport Facility", Console: "rd-res1forestentrance", ImageUrl: "rd_res_M1MissionPic.jpg" },
    { Name: "Research 7", Details: "2. Research 7", Console: "rd-res2research7", ImageUrl: "rd_res_M2MissionPic.jpg" },
    { Name: "Research 7", Details: "3. Illyn Forest", Console: "rd-res3miningcamp", ImageUrl: "rd_res_M3MissionPic.jpg" },
    { Name: "Research 7", Details: "4. Jericho Mines", Console: "rd-res4mines", ImageUrl: "rd_res_M4MissionPic.jpg" },

    { Name: "Tears For Tarnor, Ch.1: Insertion", Details: "1. Insertion Point", Console: "rd-TFT1DesertOutpost", ImageUrl: "rd-obdesertexit.jpg" },
    { Name: "Tears For Tarnor, Ch.1: Insertion", Details: "2. Abandoned Maintenance Tunnels", Console: "rd-TFT2AbandonedMaintenance", ImageUrl: "rd-Tarnormission2.jpg" },
    { Name: "Tears For Tarnor, Ch.1: Insertion", Details: "3. Oasis Colony Spaceport", Console: "rd-TFT3Spaceport", ImageUrl: "rd-obbriefing_map3.jpg" },
    
    { Name: "Tilarus-5", Details: "1. Midnight Port", Console: "rd-til1MidnightPort", ImageUrl: "tilarus01obj1.jpg" },
    { Name: "Tilarus-5", Details: "2. Road to Dawn", Console: "rd-til2RoadToDawn", ImageUrl: "tilarus02obj1.jpg" },
    { Name: "Tilarus-5", Details: "3. Arctic Infiltration", Console: "rd-til3ArcticInfiltration", ImageUrl: "rd_til3arcticInfiltration_pic.jpg" },
    { Name: "Tilarus-5", Details: "4. Area 9800", Console: "rd-til4area9800", ImageUrl: "tilarus04obj1.jpg" },
    { Name: "Tilarus-5", Details: "5. Cold Catwalks", Console: "rd-til5ColdCatwalks", ImageUrl: "tilarus05obj1.jpg" },
    { Name: "Tilarus-5", Details: "6. Yanaurus Mine", Console: "rd-til6YanaurusMine", ImageUrl: "tilarus06obj1access.jpg" },
    { Name: "Tilarus-5", Details: "7. Forgotten Factory", Console: "rd-til7Factory", ImageUrl: "tilarus07objEggs.jpg" },
    { Name: "Tilarus-5", Details: "8. Communication Center", Console: "rd-til8ComCenter", ImageUrl: "tilarus08obj2.jpg" },
    { Name: "Tilarus-5", Details: "9. Syntek Hostpital", Console: "rd-til9SyntekHospital", ImageUrl: "tilarus09obpowerroom.jpg" },

    { Name: "Bonus Missions", Details: "1. Bonus Mission 1", Console: "rd-bonus_mission1", ImageUrl: "ad4_obj_escape_image.jpg" },
    { Name: "Bonus Missions", Details: "2. Bonus Mission 2", Console: "rd-bonus_mission2", ImageUrl: "rd_bon2_survivors.jpg" },
    { Name: "Bonus Missions", Details: "3. Bonus Mission 3", Console: "rd-bonus_mission3", ImageUrl: "rd_bon3_blueprint.jpg" },

    { Name: "Deathmatch", Details: "1. DM Desert", Console: "dm_desert", ImageUrl: "dm_desert_icon.jpg" },
    { Name: "Deathmatch", Details: "2. DM Deima Surface Bridge", Console: "dm_deima", ImageUrl: "obdeimaesc.jpg" },
    { Name: "Deathmatch", Details: "3. DM Syntek Residential", Console: "dm_residential", ImageUrl: "obresidentialbio.jpg" },
    { Name: "Deathmatch", Details: "4. DM Test lab", Console: "dm_testlab", ImageUrl: "dm_testlab_icon.jpg" },

    { Name: "City 17", Details: "1. Trainstation", Console: "as_city17_01", ImageUrl: "obc17ts_button2.jpg" },
    { Name: "City 17", Details: "2. City center", Console: "as_city17_02", ImageUrl: "obc17center_goo.jpg" },
    { Name: "City 17", Details: "3. Tunnel", Console: "as_city17_03", ImageUrl: "obc17tunnel_eggs.jpg" },
    { Name: "City 17", Details: "4. Fountain", Console: "as_city17_04", ImageUrl: "obc17f_core.jpg" },
    { Name: "City 17", Details: "5. Citadel", Console: "as_city17_05", ImageUrl: "obc17citadel_escape.jpg" },

    { Name: "Dead City", Details: "1. Omega City", Console: "dc1-omega_city", ImageUrl: "omegacity_missionpic.jpg" },
    { Name: "Dead City", Details: "2. Breaking An Entry", Console: "dc2-breaking_an_entry", ImageUrl: "Breakinganentry_missionpic.jpg" },
    { Name: "Dead City", Details: "3. Search And Rescue", Console: "dc3-search_and_rescue", ImageUrl: "Searchandrescue_missionpic.jpg" },

    { Name: "No Map (unsupported)", Details: "None", Console: "none", ImageUrl: "" }
];

// List all our game options before trying to write code that uses them
Game.AddOption("Map", "The map the game will use",
    "MapID", listMaps);

var MapStep = Game.ShowOptionAsStep("MapID", true, "Choose a Campaign");

// This doesn't work yet
//var CustomMapStep = Game.ShowOptionAsStep("CustomMap", false);
//CustomMapStep.UpdateRequired = function () {
//    CustomMapStep.Required = (Context.Options["MapID"].Value.Console == "custom");
//};

Game.KillMutex = [ // 2nd instance won't launch without these removed
    "hl2_singleton_mutex"
];
Game.DirSymlinkExclusions = [
    "reactivedrop\\cfg",
    "reactivedrop\\bin",
];
Game.FileSymlinkExclusions = [
    "autoexec.cfg",
    "video.txt",
    "steam.inf",
    "config.cfg",
    "engine.dll",
    "client.dll",
    "server.dll"
];

Game.PlatformVersion = 10;
Game.SteamID = "563560";
Game.GameID = "563560";
Game.HandlerInterval = 100; // 10 FPS handler
Game.SymlinkExe = false;
Game.SymlinkGame = true;
Game.SupportsKeyboard = true;
Game.ExecutableName = "reactivedrop.exe";
Game.GameName = "Alien Swarm: Reactive Drop";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = SaveType.CFG;
Game.SupportsPositioning = true;
Game.WorkingFolder = "bin";
Game.StartArguments = "-novid -insecure -window";
Game.MaxPlayersOneMonitor = 8;
Game.MaxPlayers = 8;
Game.Hook.ForceFocus = true;
Game.Hook.ForceFocusWindowRegex = "Alien Swarm";
Game.Hook.DInputEnabled = false;
Game.Hook.DInputForceDisable = true;
Game.Hook.XInputEnabled = true;
Game.Hook.XInputReroute = false;
Game.KeyboardPlayerFirst = true;

// this game will multiply the values on the creators Update
// ... but is it only in the creators update?
Game.DPIHandling = DPIHandling.InvScaled; 

Game.OnPlay.Callback(function () {
    // Only enable setting the window size on the XInput hook dll
    // when its dual vertical, as it doenst work 100% of the time on DualHorizontal
    //Context.Hook.SetWindowSize = Player.Owner.IsDualVertical();
    //Context.Hook.ForceFocus = false;//!Player.IsKeyboardPlayer;

    var saveSrc = Context.CombinePath(Context.InstallFolder, "reactivedrop\\cfg\\video.txt");
    var savePath = Context.CombinePath(Context.InstanceFolder, "reactivedrop\\cfg\\video.txt");
    Context.ModifySaveFile(saveSrc, savePath, SaveType.CFG, [
        Context.NewSaveInfo("VideoConfig", "setting.fullscreen", "0"),
        Context.NewSaveInfo("VideoConfig", "setting.defaultres", Math.max(640, Context.Width)),
        Context.NewSaveInfo("VideoConfig", "setting.defaultresheight", Math.max(360, Context.Height)),
        Context.NewSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ]);
    
    //copy config.cfg
    Context.CopyFile(Context.CombinePath(Context.InstallFolder, "reactivedrop\\cfg\\config.cfg"),
        Context.CombinePath(Context.InstanceFolder, "reactivedrop\\cfg\\config.cfg"),
        true);

    //copy steam.inf
    Context.CopyFile(Context.CombinePath(Context.InstallFolder, "reactivedrop\\steam.inf"),
        Context.CombinePath(Context.InstanceFolder, "reactivedrop\\steam.inf"),
        true);

    // TODO: how to update these if it comes the case?

    //patch dlls
    //patch engine no sleep
    Context.PatchFile(Context.CombinePath(Context.InstallFolder, "bin\\engine.dll"),
        Context.CombinePath(Context.InstanceFolder, "bin\\engine.dll"), 
        [ 0x8B, 0x01, 0x8B, 0x50, 0x3C, 0xFF, 0xD2, 0x84, 0xC0, 0x75, 0x1A ],
        [ 0x8B, 0x01, 0x8B, 0x50, 0x3C, 0x90, 0x90 ]);
    //patch select weapon for players in briefing
    Context.PatchFile(Context.CombinePath(Context.InstallFolder, "reactivedrop\\bin\\client.dll"),
        Context.CombinePath(Context.InstanceFolder, "reactivedrop\\bin\\client.dll"),
        [ 0x0F, 0x84, 0xA8, 0x00, 0x00, 0x00, 0x53, 0x8D ],
        [ 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 ]);
    //patch select weapon for players in briefing
    Context.PatchFile(Context.CombinePath(Context.InstallFolder, "reactivedrop\\bin\\server.dll"),
        Context.CombinePath(Context.InstanceFolder, "reactivedrop\\bin\\server.dll"),
        [ 0x74, 0x12, 0x46, 0x83, 0xFE, 0x20 ],
        [ 0xEB ]);

    var autoExec = Context.InstanceFolder + "\\reactivedrop\\cfg\\autoexec.cfg";
    var lines = [
        "sv_lan 1",
        "sv_allow_lobby_connect_only 0",
        "rd_ready_mark_override 1",
        //"cl_showfps 1",
        //"fps_max 999"
    ];

    map = Context.Options["MapID"].Console;
    if (Player.IsKeyboardPlayer) {
        lines.push("joystick 0");
        lines.push("sk_autoaim_mode 1");
        //lines.push("exec undo360controller.cfg");

        if (map !== "") {
            lines.push("map " + map);
        }
    }
    else {
        lines.push("exec 360controller_pc.cfg");
        lines.push("alias selectmarine selectmarine0");
        lines.push("alias selectmarine0 \"cl_selectsinglem 0 -1; alias selectmarine selectmarine1\"");
        lines.push("alias selectmarine1 \"cl_selectsinglem 1 -1; alias selectmarine selectmarine2\"");
        lines.push("alias selectmarine2 \"cl_selectsinglem 2 -1; alias selectmarine selectmarine3\"");
        lines.push("alias selectmarine3 \"cl_selectsinglem 3 -1; alias selectmarine selectmarine4\"");
        lines.push("alias selectmarine4 \"cl_selectsinglem 4 -1; alias selectmarine selectmarine5\"");
        lines.push("alias selectmarine5 \"cl_selectsinglem 5 -1; alias selectmarine selectmarine6\"");
        lines.push("alias selectmarine6 \"cl_selectsinglem 6 -1; alias selectmarine selectmarine7\"");
        lines.push("alias selectmarine7 \"cl_selectsinglem 7 -1; alias selectmarine selectmarine0\"");
        lines.push("cl_timeout 999");
        lines.push("bind \"JOY8\" \"selectmarine\"");
        lines.push("bind \"START\" \"selectmarine\"");
        lines.push("bind \"POV_DOWN\" \"asw_squad_hotbar 4\"");
        lines.push("bind \"DOWN\" \"asw_squad_hotbar 4\"");
        //lines.push("");

        if (Context.HasKeyboardPlayer()) {
            lines.push("connect " + Context.User.GetLocalIP());
        } else {
            if (Context.PlayerID == 0) {
                if (map !== "") {
                    lines.push("map " + map);
                }
            } else {
                lines.push("connect " + Context.User.GetLocalIP());
            }
        }
    }

    Context.LogLine(map);
    //if (map.indexOf("dm_") === 0 && !Player.IsKeyboardPlayer) {
    //    //for deathmatch map use BACK button to show/close choose marine panel
    //    lines.push("bind \"JOY7\" \"cl_select_loadout\"");
    //    lines.push("bind \"BACK\" \"cl_select_loadout\"");
    //}

    Context.WriteTextFile(autoExec, lines);
});