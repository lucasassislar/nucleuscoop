using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;

namespace Games
{
    public class ResidentEvil5Info : GameInfo
    {
        public override Type[] Steps
        {
            get { return null; }
        }
        public override string ExecutableName
        {
            get { return "re5dx9.exe|re5dx10.exe"; }
        }

        public override string GameName
        {
            get { return "Resident Evil 5"; }
        }

        public override Type HandlerType
        {
            get { return typeof(object); }
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

        public ResidentEvil5Info()
        {
            options = new Dictionary<string, GameOption>();
        }

        /// <summary>
        /// Generated with http://www.random-guid.com/
        /// </summary>
        public override string GUID
        {
            get { return "1D4433B0-D555-462A-A225-9AF71C65B7CB"; }
        }

        public override bool SupportsKeyboard
        {
            get { return false; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 2; }
        }
    }
}
