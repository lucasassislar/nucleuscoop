using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static Nucleus.Gaming.Windows.Interop.User32Interop;

namespace Nucleus.Gaming.Windows
{
    public static class User32Util
    {
        public static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
            {
                EnumThreadWindows(thread.Id, (hWnd, lParam) => {
                    handles.Add(hWnd);
                    return true;
                }, IntPtr.Zero);
            }

            return handles;
        }

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }

        public static float GetDPIScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = Gdi32Interop.GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = Gdi32Interop.GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor;
        }

        public static uint GetDpiForWindow(Form form)
        {
            return User32Interop.GetDpiForWindow(form.Handle);
        }
        public static uint GetDpiForWindow(IntPtr handle)
        {
            return User32Interop.GetDpiForWindow(handle);
        }

        public static bool GetDPIForMonitor(Display display, ref Point dpi)
        {
            Version os = WindowsVersionInfo.Version;

            if (os.Major > 6 || os.Major == 6 && os.Minor >= 3)
            {
                uint dpiX = 0;
                uint dpiY = 0;
                bool result = User32Interop.GetDpiForMonitor(display.Handle, MonitorDpiType.RawDPI, ref dpiX, ref dpiY);

                dpi = new Point((int)dpiX, (int)dpiY);
                return result;
            }
            else
            {
                int mDpi = (int)(GetDPIScalingFactor() * 96);
                dpi = new Point(mDpi, mDpi);
                return true;
            }
        }


        public static int SetProcessDpiAwareness(ProcessDPIAwareness value)
        {
            //Environment.OSVersion.Version; // reports wrong values
            Version os = WindowsVersionInfo.Version;

            // Windows 8.1 or newer
            if (os.Major > 6 || os.Major == 6 && os.Minor >= 3)
            {
                // We need this, else Windows will fake
                // all the data about monitors inside the application
                return ShcoreInterop.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
            }
            return 0;
        }

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
                MonitorInfoEx info = new MonitorInfoEx();
                info.Size = Marshal.SizeOf(info);
                if (User32Interop.GetMonitorInfo(hMonitor, ref info))
                {
                    Rectangle r = lprcMonitor.ToRectangle();
                    Display display = new Display(hMonitor, r, info.DeviceName, true);
                    displays.Add(display);
                }
                return true;
            };

            bool result = User32Interop.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0);

            if (result)
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
