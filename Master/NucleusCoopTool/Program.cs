using CliverSoft;
using Games.Left4Dead2;
using Nucleus;
using Nucleus.Gaming.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SplitTool
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
    }
    static class Program
    { 
        [DllImport("TheHooker.dll")]
        static extern bool InstallHook(uint id);

        [DllImport("TheHooker.dll")]
        static extern bool RemoveHook();

        static WindowInterceptor fuck;
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern System.IntPtr FindWindowByCaption(int ZeroOnly, string lpWindowName);

        [STAThread]
        static void Main()
        {
            //uint processHandle;
            ////IntPtr windowHandle = FindWindowByCaption(0, "Untitled - Notepad");
            ////uint threadID = GetWindowThreadProcessId(windowHandle, out processHandle);
            ////IntPtr hMod = Marshal.GetHINSTANCE(typeof(GamesViewer).Module);

            //string l4d = @"E:\Games\Steam\steamapps\common\Left 4 Dead 2\left4dead2.exe";
            //Process p = Process.Start(l4d, "-novid");

            //Thread.Sleep(10000);

            //IntPtr windowHandle = p.MainWindowHandle;
            //uint threadID = GetWindowThreadProcessId(windowHandle, out processHandle);

            //if (threadID == 0)
            //{
            //    return;
            //}

            //bool installed = InstallHook(threadID);

            //while (true)
            //{

            //}
            //RemoveHook();

            //return;

            //Thread.Sleep(10000);

            //ProcessThreadCollection threads = p.Threads;
            //uint id = (uint)threads[0].Id;
            //Console.WriteLine("Thread ID: " + id);
            //uint id = GetWindowThreadProcessId(p.MainWindowHandle, IntPtr.Zero);

            //fuck = new WindowInterceptor(hMod, threadID);

            //while (true)
            //{
            //    SetForegroundWindow(p.MainWindowHandle);
            //}

            //StartGameUtil.StartGame(@"E:\Games\Steam\steamapps\common\Left 4 Dead 2\left4dead2.exe",
            //    "-novid", 5000, "hl2_singleton_mutex", "steam_singleton_mutext");

            //StartGameUtil.StartGame(@"E:\Games\Steam\steamapps\common\Left 4 Dead 2\left4dead2.exe",
            //    "-novid", 5000, "hl2_singleton_mutex", "steam_singleton_mutext");

            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = @"E:\Games\Steam\steamapps\common\Left 4 Dead 2\left4dead2.exe";
            //startInfo.Arguments = "-novid";
            //Process proc = Process.Start(startInfo);

            //int x = -1;
            //ProcessUtil.KillMutex(proc, "hl2_singleton_mutex");
            //ProcessUtil.KillMutex(proc, "steam_singleton_mutext");
            //x = -1;

            //startInfo = new ProcessStartInfo();
            //startInfo.Arguments = "-novid";
            //startInfo.FileName = @"E:\Games\Steam\steamapps\common\Left 4 Dead 2\left4dead2.exe";
            //proc = Process.Start(startInfo);

            //int w = 960;
            //int h = 360;
            //int gcd = MathUtil.GCD(w, h);
            //int k = w / gcd;
            //int j = h / gcd;

            //int i = 16;
            //byte[] d = BitConverter.GetBytes(i);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GamesViewer());
            //Application.Run(new L4D2Form());
            //p.Kill();
        }
    }
}
