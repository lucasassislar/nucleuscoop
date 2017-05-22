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
        public IntPtr Handle
        {
            get { return ptr; }
        }

        private Rectangle bounds;
        private string deviceName;
        private bool primary;
        private IntPtr ptr;

        public Display(IntPtr pointer, Rectangle size, string device, bool isPrimary)
        {
            ptr = pointer;
            bounds = size;
            deviceName = device;
            primary = isPrimary;
        }

    }
}
