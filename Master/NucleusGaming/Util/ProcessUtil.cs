using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Nucleus.Gaming.Interop;
using System.Threading;
using System.Management;

namespace Nucleus
{
    public static class ProcessUtil
    {
        public static Process RunOrphanProcess(string path, string arguments = "")
        {
            ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = @"cmd";
            psi.Arguments = "/C \"" + path + "\" " + arguments;
            return Process.Start(psi);
            //ThreadPool.QueueUserWorkItem(KillParent, p);
        }
        private static void KillParent(object state)
        {
            Process p = (Process)state;

        }

        public static bool MutexExists(Process process, string mutexName)
        {
            // TODO: Does only 1-3 exist? I've only seen these values in the Sessions
            for (int i = 1; i <= 3; i++)
            {
                try
                {
                    string str = "\\Sessions\\" + i + "\\BaseNamedObjects\\" + mutexName;
                    var handles = Win32Processes.GetHandles(process, "Mutant", str);
                    if (handles.Count > 0)
                    {
                        return true;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }
                catch (ArgumentException)
                {
                }
            }
            return false;
        }

        public static List<int> GetChildrenProcesses(Process process)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                        "SELECT * " +
                        "FROM Win32_Process " +
                        "WHERE ParentProcessId=" + process.Id);
            ManagementObjectCollection collection = searcher.Get();

            List<int> ids = new List<int>();
            foreach (var item in collection)
            {
                uint ui = (uint)item["ProcessId"];

                ids.Add((int)ui);
            }

            return ids;
        }

        public static bool KillMutex(Process process, string mutexName)
        {
            bool killed = false;
            for (int i = 1; i <= 3; i++)
            {
                var handles = Win32Processes.GetHandles(process, "Mutant", "\\Sessions\\" + i + "\\BaseNamedObjects\\" + mutexName);
                if (handles.Count == 0)
                {
                    continue;
                }
                foreach (var handle in handles)
                {
                    IntPtr ipHandle = IntPtr.Zero;
                    if (!Win32API.DuplicateHandle(Process.GetProcessById(handle.ProcessID).Handle, handle.Handle, Win32API.GetCurrentProcess(), out ipHandle, 0, false, Win32API.DUPLICATE_CLOSE_SOURCE))
                    {
                        Debug.WriteLine("DuplicateHandle() failed, error = {0}", Marshal.GetLastWin32Error());
                    }

                    Debug.WriteLine("Mutex was killed");
                    killed = true;
                }
            }

            return killed;
        }

        public static void KillProcessesByName(string processName)
        {
            string nameWithoutExe = processName;
            if (processName.EndsWith(".exe"))
            {
                nameWithoutExe = processName.Substring(0, processName.Length - 4);
            }

            foreach (var process in Process.GetProcessesByName(nameWithoutExe))
            {
                process.Kill();
            }
        }
    }
}