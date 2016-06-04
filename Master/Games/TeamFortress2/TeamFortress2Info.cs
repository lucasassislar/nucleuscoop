using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;

namespace Games
{
    public class TeamFortress2Info : GameInfo
    {
        public override string ExecutableContext
        {
            get { return ""; }
        }
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
            get { return "tf2____hl2.exe"; }
        }

        public override string GameName
        {
            get { return "Team Fortress 2"; }
        }

        public override Type HandlerType
        {
            get { return typeof(object); }
        }

        public override int MaxPlayers
        {
            get { return 16; }
        }

        protected Dictionary<string, GameOption> options;
        public override Dictionary<string, GameOption> Options
        {
            get { return options; }
        }

        public TeamFortress2Info()
        {
            options = new Dictionary<string, GameOption>();
        }

        /// <summary>
        /// Generated with https://www.guidgenerator.com/online-guid-generator.aspx
        /// </summary>
        public override string GUID
        {
            get { return "b4476592-8a70-4700-b205-bbbb7ceeaad2"; }
        }

        public override int MaxPlayersOneMonitor
        {
            get { return 4; }
        }
    }
}
