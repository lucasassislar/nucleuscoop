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

namespace Nucleus.Coop
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Log(true);

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
    }
}
