using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitTool
{
    public struct ScreenHolder
    {
        public ScreenType Position;
        public int Player;

        public ScreenHolder(ScreenType pos, int player)
        {
            Position = pos;
            Player = player;
        }
    }
}
