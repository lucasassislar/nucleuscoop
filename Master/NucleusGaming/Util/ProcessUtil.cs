using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Nucleus.Gaming.Interop;
using System.Management;

namespace Nucleus
{
    public static class ProcessUtil
    {
        public static Process RunOrphanProcess(string path, string arguments = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"cmd";
            psi.Arguments = "/C \"" + path + "\" " + arguments;
            return Process.Start(psi);
        }
        private static void KillParent(object state)
        {
            Process p = (Process)state;
        }



        public static string GetFullMutexName(Process process, string mutexName)
        {
            List<Win32API.SYSTEM_HANDLE_INFORMATION> handles = Win32Processes.GetHandles(process, "Mutant", mutexName);

            foreach (var handle in handles)
            {
                return Win32Processes.getObjectName(handle, process);
            }

            return "";
        }

        public static bool MutexExists(Process process, string mutexName)
        {
            string fullMutexName = GetFullMutexName(process, mutexName);
            if (string.IsNullOrEmpty(fullMutexName))
            {
                return false;
            }
            else
            {
                return true;
            }
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

        public static Process GetChildProcess(Process proc, List<string> childProcessPath)
        {
            if (childProcessPath.Count <= 0) return null;

            Process childProcess = null;
            bool firstFound = false;
            List<string> newProcessPath = new List<string>();
            
            childProcessPath.ForEach((procName) =>
            {
                if (!firstFound)
                {
                    firstFound = true;
                }
                else
                {
                    newProcessPath.Add(procName);
                }
            });

            string nextProcName = childProcessPath[0];
            List<int> childrenIds = GetChildrenProcesses(proc);
            if (childrenIds.Count > 0)
            {
                childrenIds.ForEach((id) =>
                {
                    Process child = Process.GetProcessById(id);
                    try
                    {
                        string withoutExe = nextProcName.Replace(".exe", "");
                        if (child.ProcessName.ToLower() == withoutExe.ToLower())
                        {
                            if(newProcessPath.Count == 0)
                            {
                                childProcess = child;
                            }
                            else
                            {
                                childProcess = GetChildProcess(child, newProcessPath);
                            }
                            return;
                        }
                    }
                    catch
                    {
                        return;
                    }

                });
            }
            return childProcess;
        }

        public static bool KillMutex(Process process, string mutexName)
        {
            bool killed = false;
            string fullMutexName = GetFullMutexName(process, mutexName);
            var handles = Win32Processes.GetHandles(process, "Mutant", fullMutexName);
            if (string.IsNullOrEmpty(fullMutexName))
            {
                return false;
            }
            foreach (var handle in handles)
            {
                IntPtr ipHandle = new IntPtr(handle.Handle);
                if (!Win32API.DuplicateHandle(Process.GetProcessById(handle.ProcessID).Handle, handle.Handle, Win32API.GetCurrentProcess(), out ipHandle, 0, false, Win32API.DUPLICATE_CLOSE_SOURCE))
                {
                    Debug.WriteLine("DuplicateHandle() failed, error = {0}", Marshal.GetLastWin32Error());
                }
                else
                {
                    Win32API.CloseHandle(ipHandle);
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