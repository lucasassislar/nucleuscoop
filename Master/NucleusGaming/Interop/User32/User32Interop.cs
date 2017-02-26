using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nucleus.Interop.User32
{
    public static class User32Interop
    {
        /// <summary>
        /// The EnumDisplayMonitors function enumerates display monitors
        /// (including invisible pseudo-monitors associated with the mirroring drivers) that intersect a region formed by 
        /// the intersection of a specified clipping rectangle and the visible region of a device context. 
        /// EnumDisplayMonitors calls an application-defined MonitorEnumProc callback function once for each monitor that is enumerated. 
        /// Note that GetSystemMetrics (SM_CMONITORS) counts only the display monitors.
        /// </summary>
        /// <param name="hdc">A handle to a display device context that defines the visible region of interest.
        /// If this parameter is NULL, the hdcMonitor parameter passed to the callback function will be NULL, 
        /// and the visible region of interest is the virtual screen that encompasses all the displays on the desktop.</param>
        /// <param name="lpRect">A pointer to a RECT structure that specifies a clipping rectangle. 
        /// The region of interest is the intersection of the clipping rectangle with the visible region specified by hdc. 
        /// If hdc is non-NULL, the coordinates of the clipping rectangle are relative to the origin of the hdc.If hdc is NULL, 
        /// the coordinates are virtual-screen coordinates.
        /// This parameter can be NULL if you don't want to clip the region specified by hdc.</param>
        /// <param name="callback">A pointer to a MonitorEnumProc application-defined callback function.</param>
        /// <param name="dwData">Application-defined data that EnumDisplayMonitors passes directly to the MonitorEnumProc function.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("user32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        public delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref Rect pRect, int dwData);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SetActiveWindow(IntPtr hWnd);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        /// directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        /// priority to the thread that created the foreground window than it does to other threads.
        /// <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs..</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer. To set any other value, specify one of the following values: GWL_EXSTYLE, GWL_HINSTANCE, GWL_ID, GWL_STYLE, GWL_USERDATA, GWL_WNDPROC </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. 
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        /// <summary>Shows a Window</summary>
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, WindowShowStyle command);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
