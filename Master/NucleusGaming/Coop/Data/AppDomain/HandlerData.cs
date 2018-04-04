using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Coop.JS;

namespace Nucleus.Gaming.Coop
{
    /// <summary>
    /// All information the handler needs to handle a game
    /// </summary>
    public class HandlerData
    {
        public GameHookData Hook { get; set; } = new GameHookData();
        public List<GameOption> Options { get; set; } = new List<GameOption>();

        public string JsCode { get; set; }

        public string[] DirSymlinkExclusions { get; set; }
        public string[] FileSymlinkExclusions { get; set; }
        public string[] FileSymlinkCopyInstead { get; set; }

        public bool ForceFinishOnPlay { get; set; } = true;
        public double HandlerInterval { get; set; }
        public bool Debug { get; set; }
        public bool SupportsPositionin { get; set; }
        public bool SymlinkExe { get; set; }
        public bool SymlinkGame { get; set; }
        public bool HardcopyGame { get; set; }

        public bool SupportsKeyboard { get; set; }
        public string[] ExecutableContext { get; set; }
        public string ExecutableName { get; set; }
        public string SteamID { get; set; }
        public string GameID { get; set; }
        public string GameName { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxPlayersOneMonitor { get; set; }
        public int PauseBetweenStarts { get; set; }
        public DPIHandling DPIHandling { get; set; } = DPIHandling.True;

        public string StartArguments { get; set; }
        //public string PathToRoot { get; set; }

        /// <summary>
        /// Relative path to the executable file from the root of the game installation 
        /// (essential for games where the executable ends up in a child binaries folder)
        /// </summary>
        public string ExecutablePath { get; set; }

        /// <summary>
        /// The relative path to where the games starts in
        /// </summary>
        public string WorkingFolder { get; set; }
        public string[] KillMutex { get; set; }
        public string LauncherExe { get; set; }
        public string LauncherTitle { get; set; }

        public bool OverrideStartupBehavior { get; set; }
        public CallbackData OnPlay { get; set; } = new CallbackData();
        public CallbackData OnStartApp { get; set; } = new CallbackData();
        public List<CustomStep> CustomSteps { get; set; } = new List<CustomStep>();
        public bool LockMouse { get; set; }

        public Dictionary<string, string> AdditionalData { get; set; } = new Dictionary<string, string>();

        //private void ParseJs(string jsCode)
        //{
        //    // get the Nucleus.Gaming assembly

        //    //engine = new Engine();

        //    //engine.SetValue("SaveType", TypeReference.CreateTypeReference(engine, typeof(SaveType)));
        //    //engine.SetValue("DPIHandling", TypeReference.CreateTypeReference(engine, typeof(DPIHandling)));
        //    //engine.SetValue("Folder", TypeReference.CreateTypeReference(engine, typeof(Folder)));
        //    //engine.SetValue("SaveType", TypeReference.CreateTypeReference(engine, typeof(SaveType)));

        //    //engine.SetValue("Game", this);
        //    //engine.Execute(jsCode);
        //}

        //private void Execute()
        //{
        //    executing = true;
        //    engine.Execute(jsCode);
        //    executing = false;
        //}

        public CustomStep ShowOptionAsStep(string optionKey, bool required, string title)
        {
            GameOption option = Options.First(c => c.Key == optionKey);
            option.Hidden = true;

            CustomStep step = new CustomStep();
            step.Option = option;
            step.Required = required;
            step.Title = title;

            CustomSteps.Add(step);
            return step;
        }

        public void RegisterAdditional(string key, string value)
        {
            if (!AdditionalData.ContainsKey(key))
            {
                AdditionalData.Add(key, value);
            }
            else
            {
                AdditionalData[key] = value;
            }
        }

        /// <summary>
        /// Clones this Game Info into a new Generic Context
        /// </summary>
        /// <returns></returns>
        public virtual HandlerContext CreateContext(GameProfile profile, PlayerInfo info)
        {
            HandlerContext context = new HandlerContext(profile, info);
            ObjectUtil.DeepCopy(this, context);

            return context;
        }

        public string GetSteamLanguage()
        {
            string result;
            if (Environment.Is64BitOperatingSystem)
            {
                result = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "Language", null);
            }
            else
            {
                result = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "Language", null);
            }

            return result;
        }

        public void AddOption(string name, string desc, string key, object value, object defaultValue)
        {
            Options.Add(new GameOption(name, desc, key, value, defaultValue));
        }

        public void AddOption(string name, string desc, string key, object value)
        {
            Options.Add(new GameOption(name, desc, key, value));
        }
    }
}
