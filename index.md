Layout of a Nucleus Handler File

Handler files are written in Javascript.

Options

## ExecutablePath (string)
Relative path to the executable file from the root of the game installation.
Essential for games where the executable ends up in a child binaries folder.
Default: ""

### Example
Borderlands 1
The main executable (borderlands.exe) is inside a folder called binaries inside the installation folder of the game. 
There are files important to the game's functionality outside of that binaries folder - if we don't specify to Nucleus, it will only clone the binaries folder and we'll be missing all the game's assets.

**Source**
```javascript
Game.ExecutableName = "borderlands.exe";
Game.ExecutablePath = "Binaries";
```

## WorkingFolder (string)
Relative path to where Nucleus should start the game's working folder to. 
Default: ""

### Example
Left 4 Dead 2
- The main executable is on the root directory (left4dead2.exe), but the game expects to be started with a working folder of 'bin' (all the game's libraries are there).

**Source** 
```javascript
Game.ExecutableName = "left4dead2.exe";
Game.WorkingFolder = "bin";
```

## ForceFinishOnPlay (bool)
If Nucleus should search and close all game instances before starting a new play instance.
Default: true

### Example
Make the app ignore all past game instances:

**Source** 
```javascript
Game.ForceFinishOnPlay = false;
```

## HandlerInterval (double)
The interval in milliseconds the Handler should be updated at. Set it to 0 to disable updating (will lose all functionality that depends on ticks).
Default: 1000 (1 Hz)

### Example
Make the handler update at 60 Hz

**Source** 
```javascript
Game.HandlerInterval = (1 / 60) * 1000;
```
Debug (bool)
Debug flag. Will ignore this handler in release builds.
Default: false

### Example **Source** 
```javascript
Game.Debug = true;
```

## SymlinkExe (bool)
If SymlinkGame is enabled, if we should copy or symbolic link the game executable
Default: false

### Example
For some games linking the executable works. If it does for yours, use it so we don't have to copy over the game executable each time we're going to play the game.

**Source** 
```javascript
Game.SymlinkExe = true;
```


## SymlinkGame (bool)
If we should symbolic link the game's files to a temporary directory. If not will launch straight from installation directory.
Default: false

### Example
Pretty much all games use symlink

**Source** 
```javascript
Game.SymlinkGame = true;
```

## HardcopyGame (bool)
NOT VERY TESTED. Instead of symlinking just straight up hard copies the entire game folder for each player. 
This is a massive storage hog and takes a long ass time to start the games, so use only in extreme cases for testing.
Default: false

### Example
Game you're testing in no god forsaken way works under symlink. Be aware if the game's size is 4gb that's a 4gb copy for each player.

**Source** 
```javascript
Game.HardcopyGame = true;
```

## SupportsKeyboard (bool)
NOT WORKING ATM. If the game has keyboard support.
Default: false

### Example
Game has keyboard support

**Source** 
```javascript
Game.SupportsKeyboard = true;
```

## ExecutableContext (string[])
Array with the name of other files found in the executable's folder (so we dont confuse different games with similar file names).
Default: null

### Example
Borderlands and Tales of the Borderlands share their executable name (borderlands.exe).
We include 1 folder and a file that's unique to Borderlands to make sure the application doesnt confuse itself.

**Source** 
```javascript
Game.ExecutableContext = [
    "PhysXLocal",
    "binkw32.dll"
];
```

## ExecutableName (string)
The name of the application executable with the extension (all lowercase).
Default: ""

### Example
All games

**Source** 
```javascript
Game.ExecutableName = "left4dead2.exe";
```

## SteamID (string)
SteamID of the game. Will be used. Someday.
Default: ""

**Source** 
```javascript
Game.SteamID = "550";
```

## GameID (string)
Unique ID for the game. Necessary, else the game cannot start. Usually set to the same as SteamID.
Default: ""

**Source** 
```javascript
Game.GameID = "550";
```

## MaxPlayers (int)
Maximum amount of players this game supports.
Default: 0

### Example
Borderlands 2 supports 4 players

**Source** 
```javascript
Game.MaxPlayers = 4;
```

## PauseBetweenStarts (double)
Pause between game starts in milliseconds.
Default: 0

### Example **Source** 
```javascript
Game.PauseBetweenStarts = 1000;
```

## DPIHandling (Enum)
The way the games handles DPI scaling. Modify this if the game is presenting different sizing behaviour after the Windows 10 Creators Update.
Default: DPIHandling.True

Values:
True: True tries to send the correct width and height to the game's window
Scaled: Scaled will scale the width and height by the DPI of the system (see that it's not per-monitor)
InvScaled: InvScaled will scale the width and height by 1 / DPI of the system (see that it's not per-monitor)

### Example
Borderlands multiplies the scale on the Creators Update.

**Source** 
```javascript
Game.DPIHandling = DPIHandling.InvScaled;
```

## KillMutex (string[])
Array of mutexes to kill before starting the next game process. See Mutex Section
Default: null

### Example
Left 4 Dead 2 won't let us open another game instance if the 2 mutexes are not killed.

**Source** 
```javascript
Game.KillMutex = [
    "hl2_singleton_mutex",
    "steam_singleton_mutext"
];
```

## LauncherExe (string)
If the game needs to go through a launcher before opening, the name of the launcher's executable.
Default: ""

**Source** 
```javascript
Game.LauncherTitle = "game_launcher.exe";
```

## LauncherTitle (string)
The name of the launcher's window title.
Default: ""

### Example
Borderlands 2 on Steam needs to go through a launcher to open. This is needed or else the application will lose the game's window.

**Source** 
```javascript
Game.LauncherTitle = "splashscreen";
```

## OnPlay (Callback)
Callback events that should be called right before the game instance starts playing

## CustomSteps

## LockMouse


# Functions

## AddOption(string name, string description, string key, object value)
Registers a new game option with the specified parameters, to be later shown to the end user

### Example
Borderlands 2 can change the SaveID of the player. We can give the user the ability to change that ID.

**Source** 
```javascript
Game.AddOption("Save ID - Player 1", "Save ID to use for Player 1 (default 0)", "saveid0", 0);
```

## AddOption(string name, string desk, string key, object value, object defaultValue)
Registers a new game option with the specified parameters, to be later shown to the end user


## ShowOptionAsStep(string optionKey, bool required, string title)
Adds an additional step to the Custom Steps list dependent on the data from a GameOption

### Example
Left 4 Dead 2 needs to show a list of maps and game modes to the user before starting the games.

**Source** 
```javascript
var listMaps = [
    { Name: "Dead Center",      Details: "1. Hotel", Console: "c1m1_hotel", ImageUrl: "deadcenter.jpg" },
    { Name: "Dark Carnival",    Details: "1. Highway", Console: "c2m1_highway", ImageUrl: "darkcarnival.png" },
    { Name: "Hard Rain",        Details: "1. Milltown", Console: "c4m1_milltown_a", ImageUrl: "hardrain.jpg" },
    { Name: "The Passing",      Details: "1. Riverbank", Console: "c6m1_riverbank", ImageUrl: "thepassing.jpg" },
];
Game.AddOption("Map", "The map the game will use", "MapID", listMaps);

var MapStep = Game.ShowOptionAsStep("MapID", true, "Choose a Campaign");
```
