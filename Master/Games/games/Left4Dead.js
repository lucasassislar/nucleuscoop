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
Game.SymlinkIgnore = [
    "video.txt",
    "autoexec.cfg"
];

Game.Debug = true;
Game.SymlinkExe = false;
Game.SupportsKeyboard = true;
Game.ExecutableName = "left4dead.exe";
Game.SteamID = "500";
Game.GUID = "500";
Game.GameName = "Left 4 Dead";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.BinariesFolder = "";
Game.NeedsSteamEmulation = false;
Game.LauncherTitle = "";
Game.SaveType = Nucleus.SaveType.CFG;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.StartArguments = "-novid";

Game.Play = function () {
    Context.ModifySave = [
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.fullscreen", "0"),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultres", Context.Width),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.defaultresheight", Context.Height),
        new Nucleus.CfgSaveInfo("VideoConfig", "setting.nowindowborder", "0"),
    ];

    var autoExec = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\left4dead\\cfg\\autoexec.cfg";
    var lines = [
        "sv_lan 1",
        "sv_allow_lobby_connect_only 0"
    ]
    Context.WriteTextFile(autoExec, lines);

    Context.SavePath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\left4dead\\cfg\\video.txt";

    if (Context.IsKeyboardPlayer) {
        Handler.StartPlayTick(1, function () {
            Handler.CenterCursor();
        });
    }
}