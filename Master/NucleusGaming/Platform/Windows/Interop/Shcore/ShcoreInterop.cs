using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nucleus.Gaming.Windows.Interop
{
    /// <summary>
    /// Interop functionality for Windows 8.1+
    /// </summary>
    internal static class ShcoreInterop
    {
        [DllImport("shcore.dll")]
        internal static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);
    }
}
