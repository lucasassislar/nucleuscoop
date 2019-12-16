using Microsoft.Win32;
using Nucleus.Gaming;
using SplitScreenMe.Core.Handler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitScreenMe.Core {
    /// <summary>
    /// All information the handler needs to handle a game
    /// </summary>
    public class HandlerData {
        public GameHookData Hook { get; set; } = new GameHookData();
        public List<GameOption> Options { get; set; } = new List<GameOption>();

        public string JsCode { get; set; }

        public int PlatformVersion { get; set; }

        public string[] DirSymlinkExclusions { get; set; }
        public string[] FileSymlinkExclusions { get; set; }
        public string[] FileSymlinkCopyInstead { get; set; }

        /// <summary>
        /// If Nucleus should search and close all game instances before starting a new play instance.
        /// </summary>
        public bool ForceFinishOnPlay { get; set; } = true;

        public bool WaitFullWindowLoad { get; set; } = true;

        /// <summary>
        /// The interval in milliseconds the Handler should be updated at. 
        /// Set it to 0 to disable updating 
        /// (will lose all functionality that depends on ticks).
        /// </summary>
        public double HandlerInterval { get; set; } = 1000;

        /// <summary>
        /// Debug flag. Will ignore this handler in release builds.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// If SymlinkGame is enabled, if we should copy or symbolic link the game executable
        /// (solves some conflicts).
        /// </summary>
        public bool SymlinkExe { get; set; }

        /// <summary>
        /// If we should symbolic link the game's files to a temporary directory. 
        /// If not, will launch straight from installation directory.
        /// </summary>
        public bool SymlinkGame { get; set; }

        /// <summary>
        /// NOT VERY TESTED. Instead of symlinking just straight up hard copies the entire game folder for each player.
        /// This is a massive storage hog and takes a long ass time to start the games, so use only in extreme cases for testing.
        /// </summary>
        public bool HardcopyGame { get; set; }

        /// <summary>
        /// If the game has keyboard support.
        /// </summary>
        public bool SupportsKeyboard { get; set; }

        /// <summary>
        /// Array with the name of other files found in the executable's folder (so we dont confuse different games with similar file names).
        /// </summary>
        public string[] ExecutableContext { get; set; }

        /// <summary>
        /// The name of the application executable with the extension (all lowercase).
        /// </summary>
        public string ExecutableName { get; set; }

        /// <summary>
        /// SteamID of the game. Will be used. Someday.
        /// </summary>
        public string SteamID { get; set; }

        /// <summary>
        /// Unique ID for the game. Necessary, else the game cannot start. Usually set to the same as SteamID.
        /// </summary>
        public string GameID { get; set; }

        /// <summary>
        /// Maximum amount of players this game supports (any possible configuration).
        /// </summary>
        public int MaxPlayers { get; set; }

        /// <summary>
        /// Pause between game starts in milliseconds (defaults to 1000).
        /// </summary>
        public double PauseBetweenStarts { get; set; } = 1000;

        /// <summary>
        /// The way the games handles DPI scaling. Modify this if the game is presenting different sizing behaviour after the Windows 10 Creators Update.
        /// </summary>
        public DPIHandling DPIHandling { get; set; } = DPIHandling.True;

        /// <summary>
        /// Relative path to the executable file from the root of the game installation 
        /// (essential for games where the executable ends up in a child binaries folder).
        /// </summary>
        public string ExecutablePath { get; set; }

        /// <summary>
        /// Relative path to where the games starts in.
        /// </summary>
        public string WorkingFolder { get; set; }

        /// <summary>
        /// Array of mutexes to kill before starting the next game process.
        /// </summary>
        public string[] KillMutex { get; set; }

        /// <summary>
        /// If the engine should rename and not kill the mutexes on the KillMutex list
        /// </summary>
        public bool RenameNotKillMutex { get; set; }

        /// <summary>
        /// If the game needs to go through a launcher before opening, the name of the launcher's executable.
        /// </summary>
        public string LauncherExe { get; set; }

        /// <summary>
        /// The name of the launcher's window title.
        /// </summary>
        public string LauncherTitle { get; set; }

        /// <summary>
        /// Callback events that should be called right before the game instance starts playing.
        /// </summary>
        public CallbackData OnPlay { get; set; } = new CallbackData();

        /// <summary>
        /// List with all the additional custom steps the handler could've added.
        /// </summary>
        public List<CustomStep> CustomSteps { get; set; } = new List<CustomStep>();

        /// <summary>
        /// Additional pre-loaded information available to the handler
        /// </summary>
        public Dictionary<string, string> AdditionalData { get; set; } = new Dictionary<string, string>();

        public bool KeyboardPlayerFirst { get; set; }

        /// <summary>
        /// Adds an additional step to the Custom Steps list dependent on the data from a GameOption
        /// </summary>
        /// <param name="optionKey"></param>
        /// <param name="required"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public CustomStep ShowOptionAsStep(string optionKey, bool required, string title) {
            GameOption option = Options.First(c => c.Key == optionKey);
            option.Hidden = true;

            CustomStep step = new CustomStep();
            step.Option = option;
            step.Required = required;
            step.Title = title;

            CustomSteps.Add(step);
            return step;
        }

        public void RegisterAdditional(string key, string value) {
            if (!AdditionalData.ContainsKey(key)) {
                AdditionalData.Add(key, value);
            } else {
                AdditionalData[key] = value;
            }
        }

        /// <summary>
        /// Clones this Game Info into a new Generic Context
        /// </summary>
        /// <returns></returns>
        public virtual HandlerContext CreateContext(GameProfile profile, PlayerInfo info, bool hasKeyboardPlayer) {
            HandlerContext context = new HandlerContext(profile, info, hasKeyboardPlayer);
            ObjectUtil.DeepCopy(this, context);

            return context;
        }

        public string GetSteamLanguage() {
            string result;
            if (Environment.Is64BitOperatingSystem) {
                result = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "Language", null);
            } else {
                result = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "Language", null);
            }

            return result;
        }

        /// <summary>
        /// Registers a new game option with the specified parameters, to be later shown to the end user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        public void AddOption(string name, string description, string key, object value, object defaultValue) {
            Options.Add(new GameOption(name, description, key, value, defaultValue));
        }

        /// <summary>
        /// Registers a new game option with the specified parameters, to be later
        /// shown to the end user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddOption(string name, string description, string key, object value) {
            Options.Add(new GameOption(name, description, key, value));
        }
    }
}
