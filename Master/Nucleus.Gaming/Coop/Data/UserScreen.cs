using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop
{
    [AppDomainShared]
    public class UserScreen
    {
        public Rectangle SwapTypeBounds { get; set; }

        public Rectangle UIBounds { get; set; }

        public UserScreenType Type { get; set; }

        public Rectangle MonitorBounds { get; set; }

        private UserScreen()
        {

        }

        public UserScreen(Rectangle display)
        {
            this.MonitorBounds = display;

            Type = UserScreenType.FullScreen;
        }

        public int GetPlayerCount()
        {
            switch (Type)
            {
                case UserScreenType.DualHorizontal:
                case UserScreenType.DualVertical:
                    return 2;
                case UserScreenType.FourPlayers:
                    return 4;
                case UserScreenType.SixteenPlayers:
                    return 16;
                default:
                    return -1;
            }
        }

        public bool IsFullscreen()
        {
            return Type == UserScreenType.FullScreen;
        }

        public bool IsDualHorizontal()
        {
            return Type == UserScreenType.DualHorizontal;
        }

        public bool IsDualVertical()
        {
            return Type == UserScreenType.DualVertical;
        }

        public bool IsFourPlayers()
        {
            return Type == UserScreenType.FourPlayers;
        }
    }
}
