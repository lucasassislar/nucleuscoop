using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public static class CmdUtil
    {
        public static void ExecuteCommand(string workingDirectory, out int exitCode, params string[] commands)
        {
            exitCode = 0;
            for (int i = 0; i < commands.Length; i++)
            {
                ExecuteCommand(workingDirectory, out exitCode, commands[i]);
            }
        }
        public static void ExecuteCommand(string workingDirectory, out int exitCode, string command)
        {
            ProcessStartInfo processInfo;

            processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.WorkingDirectory = workingDirectory;
            processInfo.Arguments = "/c " + command;

            processInfo.CreateNoWindow = true;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            process.WaitForExit();

            exitCode = process.ExitCode;
        }

    }
}
