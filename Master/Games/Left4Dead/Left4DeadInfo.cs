using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;
using System.Drawing;
using Games.Left4Dead.resources;
using Nucleus;

namespace Games.Left4Dead
{
    public struct L4DLevel
    {
        public string Command;
        public string Name;
        public Image Image;

        public L4DLevel(string name, string cmd, Image img)
        {
            Name = name;
            Command = cmd;
            Image = img;
        }
    }
    public struct L4DGameMode
    {
        public string Name;
        public string Command;
        public string Description;
        public Image Image;

        public L4DGameMode(string name, string cmd, string desc, Image image)
        {
            Name = name;
            Command = cmd;
            Description = desc;
            Image = image;
        }
    }
    public class Left4DeadInfo : GameInfo
    {
        protected Type[] steps = new Type[]
        {
            typeof (Left4DeadLevelSelection),
            typeof (Left4DeadModeSelection)
        };
        public override Type[] Steps
        {
            get { return steps; }
        }
        // Default Levels
        public static readonly L4DLevel[] Levels = new L4DLevel[]
        {
            new L4DLevel("Dead Air", "l4d_airport01_greenhouse", L4DResources.deadair),
            new L4DLevel("Blood Harvest", "l4d_farm01_hilltop", L4DResources.bloodharvest),
            new L4DLevel("Crash Course", "l4d_garage01_alleys", L4DResources.crashcourse),
            new L4DLevel("No Mercy", "l4d_hospital01_apartment", L4DResources.nomercy),
            new L4DLevel("The Sacrifice", "l4d_river01_docks", L4DResources.sacrifice),
            new L4DLevel("No Mercy", "l4d_smalltown01_caves", L4DResources.nomercy),
            new L4DLevel("The Last Stand", "l4d_sv_lighthouse", L4DResources.thelaststand),
        };

        public static readonly L4DGameMode[] GameModes = new L4DGameMode[]
        {
            new L4DGameMode("Default", "", "?Default Mode?", null),
            new L4DGameMode("Survival", "survival", "Survival Mode", null),
            new L4DGameMode("Versus", "versus", "Versus Mode", null),
            new L4DGameMode("Scavenge", "scavenge", "Scavenge Mode", null),
            new L4DGameMode("Custom", "__custom__", "?Default Mode?", null),
        };

        public override string ExecutableName
        {
            get { return "left4dead.exe"; }
        }

        public override string GameName
        {
            get { return "Left 4 Dead"; }
        }

        public override Type HandlerType
        {
            get { return typeof(Left4DeadHandler); }
        }

        public override int MaxPlayers
        {
            get { return 4; }
        }

        public override bool SupportsKeyboard
        {
            get { return true; }
        }
        protected Dictionary<string, GameOption> options;
        public override Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public override string SteamID
        {
            get { return "500"; }
        }

        public Left4DeadInfo()
        {
            options = new Dictionary<string, GameOption>();
            options.Add("delay", new GameOption("Delay Time", "Time to wait for the game to load (in seconds)", 20));
            //options.Add("instance", new GameOption("1 Player Per Instance", "If the app should make 1 instance per player", true));
            options.Add("keyboard", new GameOption("1st Player Keyboard and Mouse", "If the first player should be using Keyboard and Mouse", false));
            options.Add("engine_no_focus_sleep", new GameOption("Engine No Focus Sleep Timing", "The fps to lock to. 10 = 74~ FPS", 10));
            options.Add("ip_connect", new GameOption("Server IP", "Usually your local IP", NetworkUtil.GetLocalIP()));
        }

        /// <summary>
        /// Generated with http://www.random-guid.com/
        /// </summary>
        public override string GUID
        {
            get { return "3479BA52-A1F8-4629-84A2-D98EDCB8F76A"; }
        }

        public override SplitScreenType SupportedTypes
        {
            get { return SplitScreenType.LeftRight | SplitScreenType.TopBottom | SplitScreenType.Fullscreen; }
        }

        public override bool NeedPositioning
        {
            get { return true; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 4; }
        }
    }
}
