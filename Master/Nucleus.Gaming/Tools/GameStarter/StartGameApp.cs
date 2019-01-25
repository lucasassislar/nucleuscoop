using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Tools.GameStarter {
    public class StartGameApp {
        private Process process;
        private string lastLine;
        private StartGameData data;

        public StartGameApp() {

        }

        public void RunStartGame(StartGameData data, bool admin) {
            string startGamePath = StartGameUtil.GetStartGamePath();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = startGamePath;
            startInfo.Arguments = data.GetAsArguments();

            if (admin) {
                startInfo.Verb = "runas";
            } else {
                //startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
            }

            process = Process.Start(startInfo);

            //if (!admin) {
            //    process.OutputDataReceived += proc_OutputDataReceived;
            //    process.BeginOutputReadLine();
            //}
        }

        public void BeginKillMutex(int processId, params string[] mutex) {
            data = new StartGameData();
            data.Task = GameStarterTask.KillMutex;

            var list = mutex.ToList();
            list.Insert(0, processId.ToString());
            data.Parameters = list.ToArray();

            RunStartGame(data, true);
        }

        public void BeginScanKillMutex(string gameName, int instances, params string[] mutex) {
            data = StartGameUtil.BuildScanKillMutexData(gameName, instances, mutex);
            RunStartGame(data, true);

        }

        public void BeginMutexExists(int processId, params string[] mutex) {
            data = new StartGameData();
            data.Task = GameStarterTask.QueryMutex;

            var list = mutex.ToList();
            list.Insert(0, processId.ToString());
            data.Parameters = list.ToArray();

            RunStartGame(data, true);
        }

        public void BeginSymlinkGames(SymlinkGameData[] games) {
            data = StartGameUtil.BuildSymlinkGameData(games);
            RunStartGame(data, true);
        }

        public void BeginMultipleTasks(StartGameData[] tasks, bool admin) {
            data = StartGameUtil.BuildMultipleTaskData(tasks);
            RunStartGame(data, admin);
        }

        public void BeginStartGame(string pathToGame, string args, string workingDir = null) {
            data = new StartGameData();
            data.Task = GameStarterTask.StartGame;
            data.Parameters = new string[3];
            data.Parameters[0] = pathToGame;
            data.Parameters[1] = args;
            data.Parameters[2] = workingDir;

            RunStartGame(data, false);

            //startInfo.RedirectStandardOutput = true;
            //startInfo.UseShellExecute = false;
            //process.OutputDataReceived += proc_OutputDataReceived;
            //process.BeginOutputReadLine();
        }

        public void WaitForExit() {
            process.WaitForExit();
        }

        public string GetOutputData() {
            return lastLine;
        }

        public int GetOutputAsProcessId() {
            // parse the last line for the process ID
            int result;
            if (!int.TryParse(lastLine.Split(':')[1], out result)) {
                result = -1;
            }

            return result;
        }

        public void proc_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            if (string.IsNullOrEmpty(e.Data)) {
                return;
            }
            Console.WriteLine($"Redirected output: {e.Data}");
            lastLine = e.Data;
        }
    }
}
