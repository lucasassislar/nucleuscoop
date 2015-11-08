using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public class DuetPlayerInfo
    {
        public PlayerInfo PlayerInfo1;
        public PlayerInfo PlayerInfo2;
        public object Tag;
        public Size Size;
        public Process Process;

        public bool IsMultiMonitor;
        public Rectangle TotalBounds;
    }
}
