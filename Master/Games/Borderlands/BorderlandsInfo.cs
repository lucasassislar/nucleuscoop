using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;

namespace Games
{
    public class BorderlandsInfo : GameInfo
    {
        public override bool SupportsKeyboard
        {
            get { return true; }
        }
        public override Type[] Steps
        {
            get { return null; }
        }
        public override string ExecutableContext
        {
            get { return ""; }
        }
        public override string ExecutableName
        {
            get { return "borderlands.exe"; }
        }

        public override string GameName
        {
            get { return "Borderlands"; }
        }

        public override Type HandlerType
        {
            get { return typeof(object); }
        }

        public override int MaxPlayers
        {
            get { return 4; }
        }

        protected Dictionary<string, GameOption> options;
        public override Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public BorderlandsInfo()
        {
            options = new Dictionary<string, GameOption>();

            options.Add("keyboardPlayer", new GameOption("Keyboard Player", "If Player 1 should be using keyboard", false));
            options.Add("delay", new GameOption("Delay Time", "Time to wait for the game to load (in seconds)", 10));
            options.Add("saveid0", new GameOption("Save ID - Player 1", "Save ID to use for Player 1 (default 0)", 0));
            options.Add("saveid1", new GameOption("Save ID - Player 2", "Save ID to use for Player 2 (default 1)", 1));
            options.Add("saveid2", new GameOption("Save ID - Player 3", "Save ID to use for Player 3 (default 2)", 2));
            options.Add("saveid3", new GameOption("Save ID - Player 4", "Save ID to use for Player 4 (default 3)", 3));
            options.Add("instructions", new GameOption("Instructions", @"Press A on sequence, from player 1 to player 4 to make sure the game starts up correctly.", "@NOVAR"));
        }

        /// <summary>
        /// Generated with http://www.random-guid.com/
        /// </summary>
        public override string GUID
        {
            get { return "5FC5EE6C-9327-46EB-83FB-F53E025E518E"; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 4; }
        }
    }
}
