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
