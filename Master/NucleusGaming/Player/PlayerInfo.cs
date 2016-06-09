using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Nucleus.Gaming
{
    public class PlayerInfo
    {
        public Rectangle monitorBounds;
        public int screenIndex = -1;

        public Rectangle editBounds;

        public object Tag;
        public Process Process;

        public ScreenData screenData;

        public bool SteamEmu;
    }
}
