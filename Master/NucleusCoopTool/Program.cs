using Nucleus.Interop.User32;
using System;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class Program
    { 
        [STAThread]
        static void Main()
        {
            Version os = Environment.OSVersion.Version;
            // Windows 8.1 or newer
            if (os.Major > 6 || os.Major == 6 && os.Minor >= 3)
            {
                // We need this, else Windows will fake
                // all the data about monitors inside the application
                User32Interop.SetProcessDpiAwareness(User32Interop.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
