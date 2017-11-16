using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Tools.GameStarter
{
    public class StartGameApp
    {
        private Process process;
        private string lastLine;

        public StartGameApp()
        {

        }

        public void BeginKillMutex(int processId, params string[] mutex)
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

            startInfo.Arguments = "\"proc:" + processId.ToString() + "\" \"mutex:" + mu + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            Process proc = Process.Start(startInfo);
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.BeginOutputReadLine();
        }

        public void BeginMutexExists(int processId, params string[] mutex)
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

            startInfo.Arguments = $"\"proc:{processId}\" \"output:{mu}\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process proc = Process.Start(startInfo);
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.BeginOutputReadLine();
        }

        public void BeginStartGame(string pathToGame, string args, string workingDir = null)
        {
            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;

            if (!string.IsNullOrWhiteSpace(workingDir))
            {
                workingDir = "|" + workingDir;
            }
            startInfo.Arguments = "\"game:" + pathToGame + workingDir + ";" + args + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            process = Process.Start(startInfo);
            process.OutputDataReceived += proc_OutputDataReceived;
            process.BeginOutputReadLine();
        }

        public void WaitForExit()
        {
            process.WaitForExit();
        }

        public string GetOutputData()
        {
            return lastLine;
        }

        public int GetOutputAsProcessId()
        {
            // parse the last line for the process ID
            int result;
            if (!int.TryParse(lastLine.Split(':')[1], out result))
            {
                result = -1;
            }

            return result;
        }

        public void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                return;
            }
            Console.WriteLine($"Redirected output: {e.Data}");
            lastLine = e.Data;
        }

        public static string GetStartGamePath()
        {
            return Path.Combine(Path.GetDirectoryName(AssemblyUtil.GetStartFolder()), "StartGame.exe");
        }
    }
}
