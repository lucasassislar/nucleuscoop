using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Nucleus.Gaming.Interop;
using System.Threading;
using System.Management;
using System.IO;

namespace Nucleus.Gaming {
    public static class ProcessUtil {
        public static Process RunAsDesktopUser(ProcessStartInfo startInfo) {
            if (string.IsNullOrWhiteSpace(startInfo.FileName))
                throw new ArgumentException("Value FileName cannot be null or whitespace.", nameof(startInfo));

            // To start process as shell user you will need to carry out these steps:
            // 1. Enable the SeIncreaseQuotaPrivilege in your current token
            // 2. Get an HWND representing the desktop shell (GetShellWindow)
            // 3. Get the Process ID(PID) of the process associated with that window(GetWindowThreadProcessId)
            // 4. Open that process(OpenProcess)
            // 5. Get the access token from that process (OpenProcessToken)
            // 6. Make a primary token with that token(DuplicateTokenEx)
            // 7. Start the new process with that primary token(CreateProcessWithTokenW)

            var hProcessToken = IntPtr.Zero;
            // Enable SeIncreaseQuotaPrivilege in this process.  (This won't work if current process is not elevated.)
            try {
                var process = GetCurrentProcess();
                if (!OpenProcessToken(process, 0x0020, ref hProcessToken))
                    return null;

                var tkp = new TOKEN_PRIVILEGES {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES[1]
                };

                if (!LookupPrivilegeValue(null, "SeIncreaseQuotaPrivilege", ref tkp.Privileges[0].Luid))
                    return null;

                tkp.Privileges[0].Attributes = 0x00000002;

                if (!AdjustTokenPrivileges(hProcessToken, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero))
                    return null;
            } finally {
                CloseHandle(hProcessToken);
            }

            // Get an HWND representing the desktop shell.
            // CAVEATS:  This will fail if the shell is not running (crashed or terminated), or the default shell has been
            // replaced with a custom shell.  This also won't return what you probably want if Explorer has been terminated and
            // restarted elevated.
            var hwnd = GetShellWindow();
            if (hwnd == IntPtr.Zero)
                return null;

            var hShellProcess = IntPtr.Zero;
            var hShellProcessToken = IntPtr.Zero;
            var hPrimaryToken = IntPtr.Zero;
            try {
                // Get the PID of the desktop shell process.
                uint dwPID;
                if (GetWindowThreadProcessId(hwnd, out dwPID) == 0)
                    return null;

                // Open the desktop shell process in order to query it (get the token)
                hShellProcess = OpenProcess(ProcessAccessFlags.QueryInformation, false, dwPID);
                if (hShellProcess == IntPtr.Zero)
                    return null;

                // Get the process token of the desktop shell.
                if (!OpenProcessToken(hShellProcess, 0x0002, ref hShellProcessToken))
                    return null;

                var dwTokenRights = 395U;

                // Duplicate the shell's process token to get a primary token.
                // Based on experimentation, this is the minimal set of rights required for CreateProcessWithTokenW (contrary to current documentation).
                if (!DuplicateTokenEx(hShellProcessToken, dwTokenRights, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hPrimaryToken))
                    return null;

                // Start the target process with the new token.
                var si = new STARTUPINFO();
                var pi = new PROCESS_INFORMATION();
                if (!CreateProcessWithTokenW(hPrimaryToken, 0, startInfo.FileName, startInfo.Arguments, 0, IntPtr.Zero, startInfo.WorkingDirectory, ref si, out pi))
                    return null;

                return Process.GetProcessById(pi.dwProcessId);
            } finally {
                CloseHandle(hShellProcessToken);
                CloseHandle(hPrimaryToken);
                CloseHandle(hShellProcess);
            }

        }

        private struct TOKEN_PRIVILEGES {
            public UInt32 PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct LUID_AND_ATTRIBUTES {
            public LUID Luid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID {
            public uint LowPart;
            public int HighPart;
        }

        [Flags]
        private enum ProcessAccessFlags : uint {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        private enum SECURITY_IMPERSONATION_LEVEL {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        private enum TOKEN_TYPE {
            TokenPrimary = 1,
            TokenImpersonation
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct STARTUPINFO {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref LUID pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TOKEN_PRIVILEGES newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
               
        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, uint processId);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL impersonationLevel, TOKEN_TYPE tokenType, out IntPtr phNewToken);

        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CreateProcessWithTokenW(IntPtr hToken, int dwLogonFlags, string lpApplicationName, string lpCommandLine, int dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
               
        public static Process RunOrphanProcess(string path, string arguments = "") {
            ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = @"cmd";
            psi.Arguments = "/C \"" + path + "\" " + arguments;
            return Process.Start(psi);
            //ThreadPool.QueueUserWorkItem(KillParent, p);
        }
        private static void KillParent(object state) {
            Process p = (Process)state;

        }

        public static void ForceKill(string name) {
            try {
                Process[] procs = Process.GetProcessesByName(name.ToLower());
                if (procs.Length > 0) {
                    for (int i = 0; i < procs.Length; i++) {
                        procs[i].Kill();
                    }
                }
            } catch { }
        }

        public static bool KillMutex(Process process, string mutexName) {
            // 4 tries
            for (int i = 1; i < 4; i++) {
                Console.WriteLine("Loop " + i);
                var handles = Win32Processes.GetHandles(process, "Mutant", "\\Sessions\\", mutexName);
                if (handles.Count == 0) {
                    continue;
                }

                foreach (var handle in handles) {
                    IntPtr ipHandle = IntPtr.Zero;
                    if (!Win32API.DuplicateHandle(Process.GetProcessById(handle.ProcessID).Handle, handle.Handle, Win32API.GetCurrentProcess(), out ipHandle, 0, false, Win32API.DUPLICATE_CLOSE_SOURCE)) {
                        Console.WriteLine("DuplicateHandle() failed, error = {0}", Marshal.GetLastWin32Error());
                    }

                    return true;
                }
            }

            return false;
        }

        public static bool MutexExists(Process process, string mutexName) {
            // 4 tries
            for (int i = 0; i < 4; i++) {
                try {
                    var handles = Win32Processes.GetHandles(process, "Mutant", "\\Sessions\\", mutexName);
                    if (handles.Count > 0) {
                        return true;
                    }
                } catch (IndexOutOfRangeException) {
                } catch (ArgumentException) {
                }
            }
            return false;
        }

        public static List<int> GetChildrenProcesses(Process process) {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                        "SELECT * " +
                        "FROM Win32_Process " +
                        "WHERE ParentProcessId=" + process.Id);
            ManagementObjectCollection collection = searcher.Get();

            List<int> ids = new List<int>();
            foreach (var item in collection) {
                uint ui = (uint)item["ProcessId"];

                ids.Add((int)ui);
            }

            return ids;
        }


    }
}
//        [DllImport("ntdll.dll")]
//        public static extern uint NtQuerySystemInformation(int
//            SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength,
//            ref int returnLength);

//        [DllImport("kernel32.dll", EntryPoint = "RtlCopyMemory")]
//        static extern void CopyMemory(byte[] Destination, IntPtr Source, uint Length);

//        [StructLayout(LayoutKind.Sequential, Pack = 1)]
//        public struct SYSTEM_HANDLE_INFORMATION
//        { // Information Class 16
//            public int ProcessID;
//            public byte ObjectTypeNumber;
//            public byte Flags; // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
//            public ushort Handle;
//            public int Object_Pointer;
//            public UInt32 GrantedAccess;
//        }

//        const int CNST_SYSTEM_HANDLE_INFORMATION = 16;
//        const uint STATUS_INFO_LENGTH_MISMATCH = 0xc0000004;

//        public static List<SYSTEM_HANDLE_INFORMATION> GetHandles(Process process)
//        {
//            uint nStatus;
//            int nHandleInfoSize = 0x10000;
//            IntPtr ipHandlePointer = Marshal.AllocHGlobal(nHandleInfoSize);
//            int nLength = 0;
//            IntPtr ipHandle = IntPtr.Zero;

//            while ((nStatus = NtQuerySystemInformation(CNST_SYSTEM_HANDLE_INFORMATION, ipHandlePointer, nHandleInfoSize, ref nLength)) == STATUS_INFO_LENGTH_MISMATCH)
//            {
//                nHandleInfoSize = nLength;
//                Marshal.FreeHGlobal(ipHandlePointer);
//                ipHandlePointer = Marshal.AllocHGlobal(nLength);
//            }

//            byte[] baTemp = new byte[nLength];
//            CopyMemory(baTemp, ipHandlePointer, (uint)nLength);

//            long lHandleCount = 0;
//            if (Is64Bits())
//            {
//                lHandleCount = Marshal.ReadInt64(ipHandlePointer);
//                ipHandle = new IntPtr(ipHandlePointer.ToInt64() + 8);
//            }
//            else
//            {
//                lHandleCount = Marshal.ReadInt32(ipHandlePointer);
//                ipHandle = new IntPtr(ipHandlePointer.ToInt32() + 4);
//            }

//            SYSTEM_HANDLE_INFORMATION shHandle;
//            List<SYSTEM_HANDLE_INFORMATION> lstHandles = new List<SYSTEM_HANDLE_INFORMATION>();

//            for (long lIndex = 0; lIndex < lHandleCount; lIndex++)
//            {
//                shHandle = new SYSTEM_HANDLE_INFORMATION();
//                if (Is64Bits())
//                {
//                    shHandle = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
//                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle) + 8);
//                }
//                else
//                {
//                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle));
//                    shHandle = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
//                }
//                if (shHandle.ProcessID != process.Id) continue;
//                lstHandles.Add(shHandle);
//            }
//            return lstHandles;

//        }

//        static bool Is64Bits()
//        {
//            return Marshal.SizeOf(typeof(IntPtr)) == 8 ? true : false;
//        }
//    }
//}
