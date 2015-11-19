using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;

namespace Games.Portal2
{
    public class Portal2Info : GameInfo
    {
        public override bool SupportsKeyboard
        {
            get { return true; }
        }
        public override Type[] Steps
        {
            get { return null; }
        }
        public override string ExecutableName
        {
            get { return "portal2.exe"; }
        }

        public override string GameName
        {
            get { return "Portal 2"; }
        }

        public override Type HandlerType
        {
            get { return typeof(Portal2Handler); }
        }

        public override int MaxPlayers
        {
            get { return 2; }
        }

        protected Dictionary<string, GameOption> options;
        public override Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public override string SteamID
        {
            get { return "620"; }
        }

        public Portal2Info()
        {
            options = new Dictionary<string, GameOption>();
        }

        /// <summary>
        /// Generated with http://www.random-guid.com/
        /// </summary>
        public override string GUID
        {
            get { return "FF530292-7FC2-45A3-8D8D-C63E6139785A"; }
        }

        public override SplitScreenType SupportedTypes
        {
            get { return SplitScreenType.Fullscreen | SplitScreenType.LeftRight | SplitScreenType.TopBottom; }
        }

        public override bool NeedPositioning
        {
            get { return false; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 2; }
        }
    }
}
