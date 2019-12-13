using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using EasyHook;
using Nucleus.Gaming;
using Nucleus.Gaming.Platform.Windows.Interop;

namespace Nucleus.Inject
{
    class Program
    {
		class Injector32
		{
			[DllImport("EasyHook32.dll", CharSet = CharSet.Ansi)]
			public static extern int RhInjectLibrary(
				uint InTargetPID,
				uint InWakeUpTID,
				uint InInjectionOptions,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x86,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x64,
				IntPtr InPassThruBuffer,
				uint InPassThruSize
				);

			[DllImport("EasyHook32.dll", CharSet = CharSet.Ansi)]
			public static extern int RhCreateAndInject(
				[MarshalAs(UnmanagedType.LPWStr)] string InEXEPath,
				[MarshalAs(UnmanagedType.LPWStr)] string InCommandLine,
				uint InProcessCreationFlags,
				uint InInjectionOptions,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x86,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x64,
				IntPtr InPassThruBuffer,
				uint InPassThruSize,
				IntPtr OutProcessId //Pointer to a UINT (the PID of the new process)
				);
		}

		class Injector64
		{
			[DllImport("EasyHook64.dll", CharSet = CharSet.Ansi)]
			public static extern int RhInjectLibrary(
				uint InTargetPID,
				uint InWakeUpTID,
				uint InInjectionOptions,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x86,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x64,
				IntPtr InPassThruBuffer,
				uint InPassThruSize
				);

			[DllImport("EasyHook64.dll", CharSet = CharSet.Ansi)]
			public static extern int RhCreateAndInject(
				[MarshalAs(UnmanagedType.LPWStr)] string InEXEPath,
				[MarshalAs(UnmanagedType.LPWStr)] string InCommandLine,
				uint InProcessCreationFlags,
				uint InInjectionOptions,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x86,
				[MarshalAs(UnmanagedType.LPWStr)] string InLibraryPath_x64,
				IntPtr InPassThruBuffer,
				uint InPassThruSize,
				IntPtr OutProcessId //Pointer to a UINT (the PID of the new process)
				);
		}

		private static readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        private static void Log(string logMessage)
        {
            if (ini.IniReadValue("Misc", "DebugLog") == "True")
            {
                using (StreamWriter writer = new StreamWriter("debug-log.txt", true))
                {
                    writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]INJECT: {logMessage}");
                    writer.Close();
                }
            }
        }

        static void Main(string[] args)
        {
	        bool is64 = Environment.Is64BitProcess;

            int i = 0;
            int.TryParse(args[i++], out int Tier);

            if (Tier == 0)
            {
				string InEXEPath = args[i++];
				string InCommandLine = args[i++];
				uint.TryParse(args[i++], out uint InProcessCreationFlags);
				uint.TryParse(args[i++], out uint InInjectionOptions);
				string InLibraryPath_x86 = args[i++];
				string InLibraryPath_x64 = args[i++];
				bool.TryParse(args[i++], out bool hookWindow); // E.g. FindWindow, etc
				bool.TryParse(args[i++], out bool renameMutex);
				string mutexToRename = args[i++];
				bool.TryParse(args[i++], out bool setWindow);
				bool.TryParse(args[i++], out bool isDebug);
				string nucleusFolderPath = args[i++];
				bool.TryParse(args[i++], out bool blockRaw);

				//IntPtr InPassThruBuffer = Marshal.StringToHGlobalUni(args[i++]);
				//uint.TryParse(args[i++], out uint InPassThruSize);

				var logPath = Encoding.Unicode.GetBytes(nucleusFolderPath);
				int logPathLength = logPath.Length;

				var targetsBytes = Encoding.Unicode.GetBytes(mutexToRename);
				int targetsBytesLength = targetsBytes.Length;

				int size = 27 + logPathLength + targetsBytesLength;
				var data = new byte[size];
				data[0] = hookWindow == true ? (byte)1 : (byte)0;
				data[1] = renameMutex == true ? (byte)1 : (byte)0;
				data[2] = setWindow == true ? (byte)1 : (byte)0;
				data[3] = isDebug == true ? (byte)1 : (byte)0;
				data[4] = blockRaw == true ? (byte)1 : (byte)0;

				data[10] = (byte)(logPathLength >> 24);
				data[11] = (byte)(logPathLength >> 16);
				data[12] = (byte)(logPathLength >> 8);
				data[13] = (byte)logPathLength;

				data[14] = (byte)(targetsBytesLength >> 24);
				data[15] = (byte)(targetsBytesLength >> 16);
				data[16] = (byte)(targetsBytesLength >> 8);
				data[17] = (byte)targetsBytesLength;

				Array.Copy(logPath, 0, data, 18, logPathLength);

				Array.Copy(targetsBytes, 0, data, 19 + logPathLength, targetsBytesLength);

				IntPtr ptr = Marshal.AllocHGlobal(size);
				Marshal.Copy(data, 0, ptr, size);



				IntPtr pid = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));

				try
				{
					int result = -1;
					int attempts = 0; // 5 attempts to inject

					while (result != 0)
					{
						if (is64)
							result = Injector64.RhCreateAndInject(InEXEPath, InCommandLine, InProcessCreationFlags, InInjectionOptions, "", InLibraryPath_x64, ptr, (uint)size, pid);
						else
							result = Injector32.RhCreateAndInject(InEXEPath, InCommandLine, InProcessCreationFlags, InInjectionOptions, InLibraryPath_x86, "", ptr, (uint)size, pid);
						
						Thread.Sleep(1000);
						attempts++;

						if (attempts == 4)
							break;
					}
					Marshal.FreeHGlobal(pid);

					Console.WriteLine(Marshal.ReadInt32(pid).ToString());
				}
				catch (Exception ex)
				{
					Log(string.Format("ERROR - {0}", ex.Message));
				}

				/**
				Outdated. Need the CreateAndInject method originally from Inject32

                 
				string InEXEPath = args[i++];
                string InCommandLine = args[i++];
                uint.TryParse(args[i++], out uint InProcessCreationFlags);
                uint.TryParse(args[i++], out uint InInjectionOptions);
                string InLibraryPath_x86 = args[i++];
                string InLibraryPath_x64 = args[i++];
                IntPtr InPassThruBuffer = Marshal.StringToHGlobalUni(args[i++]);
                uint.TryParse(args[i++], out uint InPassThruSize);
                IntPtr pid = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(uint)));

                try
                {
	                int result = -1; //RhCreateAndInject(InEXEPath, InCommandLine, InProcessCreationFlags, InInjectionOptions, InLibraryPath_x86, InLibraryPath_x64, InPassThruBuffer, InPassThruSize, pid);
                    int attempts = 0; // 5 attempts to inject
                    while (result != 0)
                    {
						if (is64)
							result = Injector64.RhCreateAndInject(InEXEPath, InCommandLine, InProcessCreationFlags, InInjectionOptions, "", InLibraryPath_x64, InPassThruBuffer, InPassThruSize, pid);
						else
							result = Injector32.RhCreateAndInject(InEXEPath, InCommandLine, InProcessCreationFlags, InInjectionOptions, InLibraryPath_x86, "", InPassThruBuffer, InPassThruSize, pid);

						Thread.Sleep(1000);
                        attempts++;

						if (attempts == 4)
                            break;
                    }
                    Marshal.FreeHGlobal(pid);

                    Console.WriteLine(Marshal.ReadInt32(pid).ToString());
                }
                catch (Exception ex)
                {
                    Log("ERROR - " + ex.Message);
                }*/
			}
            else if (Tier == 1)
            {
                int.TryParse(args[i++], out int InTargetPID);
                int.TryParse(args[i++], out int InWakeUpTID);
                int.TryParse(args[i++], out int InInjectionOptions);
                string InLibraryPath_x86 = args[i++];
                string InLibraryPath_x64 = args[i++];
                //IntPtr InPassThruBuffer = Marshal.StringToHGlobalUni(args[i++]);
                int.TryParse(args[i++], out int hWnd);
                bool.TryParse(args[i++], out bool hookFocus);
                bool.TryParse(args[i++], out bool hideCursor);
                bool.TryParse(args[i++], out bool isDebug);
                string nucleusFolderPath = args[i++];
                bool.TryParse(args[i++], out bool setWindow);
				bool.TryParse(args[i++], out bool preventWindowDeactivation);															 

                var logPath = Encoding.Unicode.GetBytes(nucleusFolderPath);
                int logPathLength = logPath.Length;
                //int.TryParse(args[i++], out int InPassThruSize);

                int size = 42 + logPathLength;
                IntPtr intPtr = Marshal.AllocHGlobal(size);
                byte[] dataToSend = new byte[size];

                dataToSend[0] = (byte)(hWnd >> 24);
                dataToSend[1] = (byte)(hWnd >> 16);
                dataToSend[2] = (byte)(hWnd >> 8);
                dataToSend[3] = (byte)(hWnd);

				dataToSend[4] = preventWindowDeactivation == true ? (byte)1 : (byte)0;																  
                dataToSend[5] = setWindow == true ? (byte)1 : (byte)0;
                dataToSend[6] = isDebug == true ? (byte)1 : (byte)0;
                dataToSend[7] = hideCursor == true ? (byte)1 : (byte)0;
                dataToSend[8] = hookFocus == true ? (byte)1 : (byte)0;

                dataToSend[9] = (byte)(logPathLength >> 24);
                dataToSend[10] = (byte)(logPathLength >> 16);
                dataToSend[11] = (byte)(logPathLength >> 8);
                dataToSend[12] = (byte)logPathLength;

                Array.Copy(logPath, 0, dataToSend, 13, logPathLength);

                Marshal.Copy(dataToSend, 0, intPtr, size);                

                try
                {
	                if (is64)
	                {
		                Injector64.RhInjectLibrary((uint)InTargetPID, (uint)InWakeUpTID, (uint)InInjectionOptions, "", InLibraryPath_x64, intPtr, (uint)size);
	                }
					else
	                {
						Injector32.RhInjectLibrary((uint)InTargetPID, (uint)InWakeUpTID, (uint)InInjectionOptions, InLibraryPath_x86, "", intPtr, (uint)size);
					}
                }
                catch (Exception ex)
                {
                    Log("ERROR - " + ex.Message);
                }
            }
        }
    }
}
