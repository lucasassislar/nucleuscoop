using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitPlayPC
{
    public static class ScreensUtil
    {
        public static Rectangle[] GetSetup_Triple4kHorizontal()
        {
            return new Rectangle[] 
            {
                new Rectangle(0, 0, 3840, 2160),
                new Rectangle(3840, 0, 3840, 2160),
                new Rectangle(7680, 0, 3840, 2160)
            };
        }

        public static Rectangle[] GetSetup_Triple4kVertical()
        {
            return new Rectangle[] 
            {
                new Rectangle(0, 0, 2160, 3840),
                new Rectangle(2160, 0, 2160, 3840),
                new Rectangle(4320, 0, 2160, 3840)
            };
        }

        public static Rectangle[] GetSetup_Four1080pHorizontal()
        {
            return new Rectangle[] 
            {
                new Rectangle(-1920, 0, 1920, 1080),
                new Rectangle(0, 0, 1920, 1080),
                new Rectangle(1920, 0, 1920, 1080),
                new Rectangle(3840, 0, 1920, 1080)
            };
        }

        public static UserScreen[] AllScreens()
        {
            Screen[] all = Screen.AllScreens;
            //UserScreen[] rects = new UserScreen[all.Length];

            Rectangle[] test = GetSetup_Triple4kHorizontal();
            UserScreen[] rects = new UserScreen[test.Length];


            for (int i = 0; i < rects.Length; i++)
            {
                UserScreen u = new UserScreen();
                //u.monitorBounds = all[i].Bounds;
                u.monitorBounds = test[i];
                u.bounds = u.monitorBounds;
                rects[i] = u;
            }

            return rects;
        }

        public static Rectangle[] AllScreensRec()
        {
            return GetSetup_Triple4kHorizontal();

            Screen[] all = Screen.AllScreens;
            Rectangle[] rects = new Rectangle[all.Length];

            for (int i = 0; i < all.Length; i++)
            {
                rects[i] = all[i].Bounds;
            }

            return rects;
        }
    }
}
