using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public class Display
    {
        public Rectangle Bounds
        {
            get { return bounds; }
        }
        public string DeviceName
        {
            get { return deviceName; }
        }
        public bool Primary
        {
            get { return primary; }
        }

        private Rectangle bounds;
        private string deviceName;
        private bool primary;

        public Display(Rectangle size, string device, bool isPrimary)
        {
            bounds = size;
            deviceName = device;
            primary = isPrimary;
        }

    }
}
