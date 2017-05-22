using Nucleus.Gaming;
using Nucleus.Gaming.Windows;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // initialize DPIManager BEFORE setting 
            // the application to be DPI aware
            DPIManager.PreInitialize();
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            DPIManager.Initialize(form);
            DPIManager.ForceUpdate();

            Application.Run(form);
        }
    }
}
