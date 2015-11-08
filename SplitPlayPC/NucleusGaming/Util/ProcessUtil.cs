using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Nucleus.Gaming.Interop;

namespace Nucleus
{
    public static class ProcessUtil
    {
        public static bool KillMutex(Process process, string mutexName)
        {
            // session can change ?!?!?! wtf, can have value of 1 or 2
            bool killed = false;
            for (int i = 1; i < 3; i++)
            {
                try
                {
                    var handles = Win32Processes.GetHandles(process, "Mutant", "\\Sessions\\" + i + "\\BaseNamedObjects\\" + mutexName);
                    if (handles.Count == 0)
                    {
                        throw new System.ArgumentException("NoMutex", "original");
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
                catch (IndexOutOfRangeException)
                {
                }
                catch (ArgumentException)
                {
                }
            }

            return killed;
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
