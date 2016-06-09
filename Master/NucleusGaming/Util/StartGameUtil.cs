using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus
{
    public static class StartGameUtil
    {
        private static string lastLine;

        public static string GetStartGamePath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "StartGame.exe");
        }

        public static string GetArguments(string pathToGame, string args, int waitTime, params string[] mutex)
        {
            string mu = "";
            for (int i = 0; i < mutex.Length; i++)
            {
                mu += mutex[i];

                if (i != mutex.Length - 1)
                {
                    mu += ";";
                }
            }

            return "\"" + pathToGame + "\" \"" + args + "\" \"" + waitTime + "\" \"" + mu + "\"";
        }

        public static void KillMutex(Process p, params string[] mutex)
        {
            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;

            string mu = "";
            for (int i = 0; i < mutex.Length; i++)
            {
                mu += mutex[i];

                if (i != mutex.Length - 1)
                {
                    mu += ";";
                }
            }

            startInfo.Arguments = "\"proc:" + p.Id.ToString() + "\" \"mutex:" + mu + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process proc = Process.Start(startInfo);
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.BeginOutputReadLine();

            proc.WaitForExit();
        }

        /// <summary>
        /// NOT THREAD SAFE
        /// </summary>
        /// <param name="pathToGame"></param>
        /// <param name="args"></param>
        /// <param name="waitTime"></param>
        /// <param name="mutex"></param>
        /// <returns></returns>
        public static int StartGame(string pathToGame, string args)
        {
            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;

            startInfo.Arguments = "\"game:" + pathToGame + ";" + args + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process proc = Process.Start(startInfo);
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.BeginOutputReadLine();

            proc.WaitForExit();

            // parse the last line for the process ID
            return int.Parse(lastLine.Split(':')[1]);
        }
        public static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                return;
            }
            lastLine = e.Data;
        }
    }
}
