using Newtonsoft.Json;
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
        private StartGameData data;

        public StartGameApp()
        {

        }

        public void BeginKillMutex(int processId, params string[] mutex)
        {
            data = new StartGameData();
            data.Task = GameStarterTask.KillMutex;

            var list = mutex.ToList();
            list.Insert(0, processId.ToString());
            data.Parameters = list.ToArray();

            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;

            startInfo.Arguments = data.GetAsArguments();
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            process = Process.Start(startInfo);
            process.OutputDataReceived += proc_OutputDataReceived;
            process.BeginOutputReadLine();
        }

        public void BeginMutexExists(int processId, params string[] mutex) {
            data = new StartGameData();
            data.Task = GameStarterTask.QueryMutex;

            var list = mutex.ToList();
            list.Insert(0, processId.ToString());
            data.Parameters = list.ToArray();

            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;

            startInfo.Arguments = data.GetAsArguments();
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            process = Process.Start(startInfo);
            process.OutputDataReceived += proc_OutputDataReceived;
            process.BeginOutputReadLine();
        }

        public void BeginSymlinkGames(SymlinkGameData[] games) {
            data = new StartGameData();
            data.Task = GameStarterTask.SymlinkFolders;

            string[] argData = new string[games.Length];
            for (int i = 0; i < games.Length; i++) {
                argData[i] = JsonConvert.SerializeObject(games[i]);
            }
            data.Parameters = argData;

            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;
            startInfo.Verb = "runas";

            startInfo.Arguments = data.GetAsArguments();
            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = true;

            // cant read output of admin process

            process = Process.Start(startInfo);
        }

        public void BeginStartGame(string pathToGame, string args, string workingDir = null)
        {
            data = new StartGameData();
            data.Task = GameStarterTask.StartGame;
            data.Parameters = new string[3];
            data.Parameters[0] = pathToGame;
            data.Parameters[1] = args;
            data.Parameters[2] = workingDir;

            string startGamePath = GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;

            startInfo.Arguments = data.GetAsArguments();
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            if (string.IsNullOrEmpty(workingDir)) {
                startInfo.WorkingDirectory = Path.GetDirectoryName(pathToGame);
            } else {
                startInfo.WorkingDirectory = workingDir;
            }

            //process = Process.Start(startInfo);
            process = ProcessUtil.RunAsDesktopUser(startInfo);
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
            return Path.Combine(AssemblyUtil.GetStartFolder(), "bin", "StartGame.exe");
        }
    }
}
