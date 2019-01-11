using Nucleus.Gaming;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Nucleus.Gaming.Coop;
using System.Threading;
using Nucleus.Coop.App.Forms;
using Nucleus.Gaming.Coop.Interop;

namespace Nucleus.Coop
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args) {
            new Log(true);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionEventHandler);

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEventHandler);

            // initialize DPIManager BEFORE setting 
            // the application to be DPI aware,
            // or else Windows will give us pre-scaled monitor sizes
            DPIManager.PreInitialize();
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameManager gameManager = new GameManager();

            StartMainForm(args, gameManager);
        }

        private static void StartMainForm(string[] args, GameManager gameManager)
        {
            MainForm form = new MainForm(args, gameManager);

            Application.Run(form);
        }

#if DEBUG
        private static void ThreadExceptionEventHandler(object sender, ThreadExceptionEventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }

        private static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }
#endif
    }


}
