using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitPlayPC
{
    public class GameConfig
    {
        private List<PlayerInfo> players;
        public List<PlayerInfo> Players
        {
            get { return players; }
        }

        public GameConfig()
        {
            players = new List<PlayerInfo>();
        }
    }
}
