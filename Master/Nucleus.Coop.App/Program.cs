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
using Nucleus.Coop.App.Forms;
using Nucleus.Gaming.Coop.Interop;

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

            DomainWebApiConnection apiConnection = new DomainWebApiConnection();
            apiConnection.Initialize();

            GameManager gameManager = new GameManager();

            if (string.IsNullOrWhiteSpace(gameManager.User.LastToken))
            {
                LoginForm loginForm = new LoginForm(apiConnection);
                DPIManager.AddForm(loginForm);
                DPIManager.ForceUpdate();

                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // save login credentials
                    gameManager.User.LastToken = apiConnection.Token;
                    StartMainForm(args, gameManager, apiConnection);
                }
            }
            else
            {
                // saved credentials
                apiConnection.SetToken(gameManager.User.LastToken);

                StartMainForm(args, gameManager, apiConnection);
            }
        }

        private static void StartMainForm(string[] args, GameManager gameManager, DomainWebApiConnection apiConnection)
        {
            MainForm form = new MainForm(args, gameManager, apiConnection);
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
