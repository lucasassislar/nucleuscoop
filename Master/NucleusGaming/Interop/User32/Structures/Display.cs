using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public class Display
    {
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
