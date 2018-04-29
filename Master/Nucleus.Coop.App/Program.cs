using Nucleus.Gaming;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Nucleus.Gaming.Coop;
using Squirrel;
using System.Threading;

namespace Nucleus.Coop
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Log(true);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionEventHandler);

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEventHandler);

            // initialize DPIManager BEFORE setting 
            // the application to be DPI aware
            DPIManager.PreInitialize();
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm(args);
            DPIManager.AddForm(form);
            DPIManager.ForceUpdate();

            Application.Run(form);
        }

        private static void ThreadExceptionEventHandler(object sender, ThreadExceptionEventArgs e)
        {
            int x = 0;
        }

        private static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            int x = 0;
        }
    }


}
