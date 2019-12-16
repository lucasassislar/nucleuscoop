using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SplitScreenMe.Core {
    public static class EasyHookInterop {
        [DllImport("EasyHook32.dll", CharSet = CharSet.Ansi)]
        public static extern int RhCreateAndInject(
    [MarshalAsAttribute(UnmanagedType.LPWStr)] string InEXEPath,
    [MarshalAsAttribute(UnmanagedType.LPWStr)] string InCommandLine,
    int InProcessCreationFlags,
    int InInjectionOptions,
    [MarshalAsAttribute(UnmanagedType.LPWStr)] string InLibraryPath_x86,
    [MarshalAsAttribute(UnmanagedType.LPWStr)] string InLibraryPath_x64,
    IntPtr InPassThruBuffer,
    int InPassThruSize,
    IntPtr OutProcessId //Pointer to a UINT (the PID of the new process)
    );
    }
}
