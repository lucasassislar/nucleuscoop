using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Games
{
    public struct L4D2GameMode
    {
        public string Name;
        public string Command;
        public string Description;
        public Image Image;

        public L4D2GameMode(string name, string cmd, string desc, Image image)
        {
            Name = name;
            Command = cmd;
            Description = desc;
            Image = image;
        }
    }
}
