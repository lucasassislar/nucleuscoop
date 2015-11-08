using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;

namespace WillowTree
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //SplashScreen splash = new SplashScreen("Resources\\loading.png");
            //splash.Show(true);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            System.Windows.Forms.Application.Run(new WillowTreeMain());
        }
    }
}
