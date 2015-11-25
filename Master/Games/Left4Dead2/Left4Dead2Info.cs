using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;
using Games.Left4Dead2;
using System.Drawing;
using Nucleus.Coop.Games.Left4Dead2.resources;

namespace Games
{
    /// <summary>
    /// Class that declares all Left 4 Dead 2 important stuff
    /// </summary>
    public class Left4Dead2Info : GameInfo
    {
        /// <summary>
        /// Custom steps for the user to pass to be able to start the game
        /// </summary>
        public override Type[] Steps
        {
            get { return steps; }
        }

        public override string ExecutableContext
        {
            get { return ""; }
        }

        // Default Levels
        public static readonly L4D2Level[] Levels = new L4D2Level[]
        {
            new L4D2Level("Dead Center", "c1m1_hotel", L4D2Resources.deadcenter),
            new L4D2Level("Dark Carnival", "c2m1_highway", L4D2Resources.darkcarnival),
            new L4D2Level("Swamp Fever", "c3m1_plankcountry", L4D2Resources.swampfever),
            new L4D2Level("Hard Rain", "c4m1_milltown_a", L4D2Resources.hardrain),
            new L4D2Level("The Parish", "c5m1_waterfront", L4D2Resources.theparish),
            new L4D2Level("No Mercy", "l4d_hospital01_apartment", L4D2Resources.nomercy),
            new L4D2Level("Death Toll", "l4d_smalltown01_caves", L4D2Resources.deathtoll),
            new L4D2Level("Dead Air", "l4d_airport01_greenhouse", L4D2Resources.deadair),
            new L4D2Level("Arena of The Dead 2", "jsarena201_town ", null),
            new L4D2Level("Blood Harvest", "l4d_farm01_hilltop", L4D2Resources.bloodharvest),
            new L4D2Level("Crash Course", "l4d_garage01_alleys", L4D2Resources.crashcourse),
            new L4D2Level("Death Aboard", "l4d_deathaboard01_prison", L4D2Resources.deathaboard),
            new L4D2Level("Death Row", "l4d_deathrow01_streets", null),
            new L4D2Level("Die Screaming", "l4d_scream01_yards", null),
            new L4D2Level("Dead Echo", "l4d_de01_sewers", null),
            new L4D2Level("Hellhouse 2", "l4d2_sv_hell_house", null),
            new L4D2Level("Lego", "lg_surv", null),
            new L4D2Level("Left 4 Cake", "left4cake01_start", null),
            new L4D2Level("Night of the Living Dead", "l4d_sv_notld", null),
            new L4D2Level("The Palace V2", "g14cinema", null),
            new L4D2Level("Custom Level", "__custom__", null),
        };

        public static readonly L4D2GameMode[] GameModes = new L4D2GameMode[]
        {
            new L4D2GameMode("Survival", "survival", "Survival Mode", null),
            new L4D2GameMode("Versus", "versus", "Versus Mode", null),
            new L4D2GameMode("Scavenge", "scavenge", "Scavenge Mode", null),
            new L4D2GameMode("Default", "", "?Default Mode?", null),
            new L4D2GameMode("Custom", "__custom__", "?Default Mode?", null),
        };

        public override string ExecutableName
        {
            get { return "left4dead2.exe"; }
        }

        public override string GameName
        {
            get { return "Left 4 Dead 2"; }
        }

        public override Type HandlerType
        {
            get { return typeof(object); }
        }

        public override int MaxPlayers
        {
            get { return 32; }
        }

        public override bool SupportsKeyboard
        {
            get { return true; }
        }

        public override Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        /// types of the steps, all inhereting from the ICanProceed interface
        protected Type[] steps = new Type[]
        {
            typeof (Left4Dead2LevelSelection),
            typeof (Left4Dead2ModeSelection)
        };
        protected Dictionary<string, GameOption> options;

        public Left4Dead2Info()
        {
            options = new Dictionary<string, GameOption>();
            options.Add("delay", new GameOption("Delay Time", "Time to wait for the game to load (in seconds)", 20));
            options.Add("instance", new GameOption("1 Player Per Instance", "If the app should make 1 instance per player", true));
            options.Add("keyboard", new GameOption("Keyboard and Mouse Player", "The player that will be using Keyboard and Mouse", PlayerID.None));
        }

        /// <summary>
        /// Generated with http://www.random-guid.com/
        /// </summary>
        public override string GUID
        {
            get { return "E82367CC-29DC-4BF6-BBA5-96A2AF601354"; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 16; }
        }
    }
}
