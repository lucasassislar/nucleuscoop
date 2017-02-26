using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static Nucleus.Interop.User32.User32Interop;

namespace Nucleus.Interop.User32
{
    public static class User32Util
    {
        /// <summary>
        /// Loops through all connected monitors and caches their display information
        /// into an array
        /// </summary>
        /// <returns>Display array</returns>
        public static Display[] GetDisplays()
        {
            List<Display> displays = new List<Display>();
            MonitorEnumProc callback = (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, int d) =>
            {
                Display display = new Display(lprcMonitor.ToRectangle(), "", true);
                displays.Add(display);

                return true;
            };

            if (User32Interop.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0))
            {
                return displays.ToArray();
            }
            return null;
        }

        public static void HideBorder(IntPtr handle)
        {
            uint lStyle = User32Interop.GetWindowLong(handle, User32_WS.GWL_STYLE);
            lStyle &= ~(User32_WS.WS_CAPTION | User32_WS.WS_BORDER | User32_WS.WS_DLGFRAME | User32_WS.WS_SIZEBOX | User32_WS.WS_THICKFRAME);
            User32Interop.SetWindowLong(handle, User32_WS.GWL_STYLE, lStyle);
        }
        public static void HideTaskbar()
        {
            IntPtr hwnd = User32Interop.FindWindow("Shell_TrayWnd", "");
            User32Interop.ShowWindow(hwnd, WindowShowStyle.Hide);

            IntPtr hwndOrb = User32Interop.FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            User32Interop.ShowWindow(hwndOrb, WindowShowStyle.Hide);
        }
        public static void MinimizeEverything()
        {
            IntPtr lHwnd = User32Interop.FindWindow("Shell_TrayWnd", null);
            User32Interop.SendMessage(lHwnd, User32_WS.WM_COMMAND, (IntPtr)User32_WS.MIN_ALL, IntPtr.Zero);
        }
        public static void ShowTaskBar()
        {
            IntPtr hwnd = User32Interop.FindWindow("Shell_TrayWnd", "");
            User32Interop.ShowWindow(hwnd, WindowShowStyle.Show);

            IntPtr hwndOrb = User32Interop.FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            User32Interop.ShowWindow(hwndOrb, WindowShowStyle.Show);
        }
    }
}
