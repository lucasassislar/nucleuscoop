using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus.Gaming;

namespace Games
{
    public class TeamFortress2Info : IGameInfo
    {
        public string[] ExecutableContext
        {
            get { return null; }
        }
        public  bool SupportsKeyboard
        {
            get { return true; }
        }
        public  Type[] Steps
        {
            get { return null; }
        }
        public  string ExecutableName
        {
            get { return "tf2____hl2.exe"; }
        }

        public  string GameName
        {
            get { return "Team Fortress 2"; }
        }

        public  Type HandlerType
        {
            get { return typeof(object); }
        }

        public  int MaxPlayers
        {
            get { return 16; }
        }

        protected Dictionary<string, GameOption> options;
        public  Dictionary<string, GameOption> Options
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
        public  string GUID
        {
            get { return "b4476592-8a70-4700-b205-bbbb7ceeaad2"; }
        }

        public  int MaxPlayersOneMonitor
        {
            get { return 4; }
        }
    }
}
