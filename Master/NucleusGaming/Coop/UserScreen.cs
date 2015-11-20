using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming
{
    public class UserScreen
    {
        public Rectangle bounds;
        public Rectangle swapTypeRect;
        public bool vertical;

        public UserScreenType type = UserScreenType.FullScreen;
        public Rectangle monitorBounds;
    }
}
