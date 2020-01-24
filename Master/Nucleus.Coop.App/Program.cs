using Nucleus.Coop.App.Forms;
using Nucleus.Diagnostics;
using Nucleus.DPI;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Platform.Windows;
using SplitScreenMe.Core;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop {
    static class Program {
        [STAThread]
        static void Main(string[] args) {
            new Log(true);

            ThreadUtil.Initialize();

            // Add the event handler for handling UI thread exceptions to the event.
            //Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionEventHandler);

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEventHandler);

            // initialize DPIManager BEFORE setting 
            // the application to be DPI aware,
            // or else Windows will give us pre-scaled monitor sizes
            DPI.DPIManager.PreInitialize();
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameManager gameManager = new GameManager();

            StartMainForm(args, gameManager);
        }

        private static void StartMainForm(string[] args, GameManager gameManager) {
            MainForm form = new MainForm(args, gameManager);

            Application.Run(form);
        }

        private static void ThreadExceptionEventHandler(object sender, ThreadExceptionEventArgs e) {
            System.Diagnostics.Debugger.Break();
        }

        private static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e) {
            System.Diagnostics.Debugger.Break();
        }
    }


}
