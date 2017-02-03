Game.Options = [
    new Nucleus.GameOption(
        "Keyboard Player",
        "The player that will be playing on keyboard and mouse (if any)",
        Nucleus.KeyboardPlayer.NoKeyboardPlayer,
        "KeyboardPlayer"),
    new Nucleus.GameOption(
        "Save ID - Player 1",
        "Save ID to use for Player 1 (default 0)",
        0,
        "saveid0"),
    new Nucleus.GameOption(
        "Save ID - Player 2",
        "Save ID to use for Player 2 (default 1)",
        1,
        "saveid1"),
    new Nucleus.GameOption(
        "Save ID - Player 3",
        "Save ID to use for Player 3 (default 2)",
        2,
        "saveid2"),
    new Nucleus.GameOption(
        "Save ID - Player 4",
        "Save ID to use for Player 4 (default 3)",
        3,
        "saveid3")
];
Game.KillMutex = [];


Game.BinariesFolder = "";

Game.Debug = true;
Game.SymlinkExe = false;
Game.SupportsKeyboard = false;
Game.ExecutableName = "edf41.exe";
Game.SteamID = "410320";
Game.GUID = "410320";
Game.GameName = "EARTH DEFENSE FORCE 4.1: The Shadow of New Despair";
Game.MaxPlayers = 4;
Game.MaxPlayersOneMonitor = 4;
Game.NeedsSteamEmulation = true;
Game.Is64Bit = true;
Game.LauncherTitle = "";
Game.SaveType = null;
Game.SupportsPositioning = true;
Game.HideTaskbar = false;
Game.CustomXinput = true;
Game.XInputFiles = ["xinput9_1_0.dll", "x360ce.ini"];

//TODO:THIS GAME WILL REQUIRE THE USER TO CREATE THE XINPUT FILES ABOVE. 
//     xinput9_1_0.dll - THIS IS 64 bit
//     x360ce.ini - THIS FILE IS A CONFIG FILE FOR x360ce, AND SHOULD BE SET DIFFERENTLY
//                  FOR EACH PLAYER, SO THAT EACH PLAYER'S CONTROLLER, IS SEEN AS PLAYER 1
//TODO: THE PLAYER WILL ALSO HAVE TO SET THE RESOLUTION FOR EACH SCREEN SEPARATELY AND SET
//      EACH SCREEN TO WINDOWED, BECAUSE NOT ONLY IS THE CONFIG FILE, BINARY, BUT IT ALSO
//      SEEMS TO BE CHANGING SOME VALUES ALMOST ABITARILY.
//TODO: UNTIL THESE PROBLEMS CAN BE FIXED WE SHOULD DISPLAY A MESSAGE TO THE USER WHEN
//      THEY START UP THERE GAMES.

Game.Play = function () {}