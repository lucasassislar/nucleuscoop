using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Games
{
    public struct L4D2Level
    {
        public string Command;
        public string Name;
        public Image Image;

        public L4D2Level(string name, string cmd, Image img)
        {
            Name = name;
            Command = cmd;
            Image = img;
        }
    }
}
