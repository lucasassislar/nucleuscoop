using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nucleus.Gaming.Interop
{
    public static class User32
    {
        public static void HideBorder(IntPtr handle)
        {
            uint lStyle = (uint)User32Interop.GetWindowLong(handle, User32_WS.GWL_STYLE);
            uint customStyle = ~(User32_WS.WS_CAPTION | User32_WS.WS_BORDER | User32_WS.WS_DLGFRAME | User32_WS.WS_SIZEBOX | User32_WS.WS_THICKFRAME);

            if ((lStyle & customStyle) == lStyle)
            {
                return;
            }

            lStyle &= ~(User32_WS.WS_CAPTION | User32_WS.WS_BORDER | User32_WS.WS_DLGFRAME | User32_WS.WS_SIZEBOX | User32_WS.WS_THICKFRAME);
            User32Interop.SetWindowLong(handle, User32_WS.GWL_STYLE, (int)lStyle);
        }
        public static void HideTaskbar()
        {
            int hwnd = User32Interop.FindWindow("Shell_TrayWnd", "").ToInt32();
            User32Interop.ShowWindow(hwnd, WindowShowStyle.Hide);

            IntPtr hwndOrb = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            ShowWindow(hwndOrb, SW_HIDE);
        }
        public static void MinimizeEverything()
        {
            IntPtr lHwnd = User32Interop.FindWindow("Shell_TrayWnd", null);
            User32Interop.SendMessage(lHwnd, User32_WS.WM_COMMAND, (IntPtr)User32_WS.MIN_ALL, IntPtr.Zero);
        }
        public static void ShowTaskBar()
        {
            int hwnd = User32Interop.FindWindow("Shell_TrayWnd", "").ToInt32();
            User32Interop.ShowWindow(hwnd, WindowShowStyle.Show);

            IntPtr hwndOrb = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            ShowWindow(hwndOrb, SW_SHOW);
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int command);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);
    }
}
