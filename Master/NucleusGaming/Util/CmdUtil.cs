using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public static void MkLinkDirectory(string fromDir, string toDir, out int exitCode)
        {
            string cmd = string.Format("mklink /d \"{0}\" \"{1}\"", toDir, fromDir);
            ExecuteCommand(fromDir, out exitCode, cmd);
        }
        public static void MkLinkFile(string fromFile, string toFile, out int exitCode)
        {
            string cmd = string.Format("mklink \"{0}\" \"{1}\"", toFile, fromFile);
            ExecuteCommand(fromFile, out exitCode, cmd);
        }

        public static void LinkDirectories(string rootFolder, string destination, out int exitCode, params string[] exclusions)
        {
            exitCode = 1;

            DirectoryInfo[] dirs = new DirectoryInfo(rootFolder).GetDirectories();

            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                if (exclusions.Contains(dir.Name.ToLower()))
                {
                    continue;
                }

                string relative = dir.FullName.Replace(rootFolder + @"\", "");
                string linkPath = Path.Combine(destination, relative);
                CmdUtil.MkLinkDirectory(dir.FullName, linkPath, out exitCode);
            }
        }

        public static void LinkFiles(string rootFolder, string destination, out int exitCode, params string[] exclusions)
        {
            exitCode = 1;

            FileInfo[] files = new DirectoryInfo(rootFolder).GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                string lower = file.Name.ToLower();
                bool cont = false;
                for (int j = 0; j < exclusions.Length; j++)
                {
                    string exc = exclusions[j];
                    if (lower.Contains(exc))
                    {
                        cont = true;
                        break;
                    }
                }

                if (cont)
                {
                    continue;
                }

                string relative = file.FullName.Replace(rootFolder + @"\", "");
                string linkPath = Path.Combine(destination, relative);
                CmdUtil.MkLinkFile(file.FullName, linkPath, out exitCode);
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
