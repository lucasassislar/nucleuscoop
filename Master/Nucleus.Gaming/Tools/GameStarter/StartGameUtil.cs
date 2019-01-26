using Newtonsoft.Json;
using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming.Tools.GameStarter {
    /// <summary>
    /// Util class for executing and reading output from the Nucleus.Coop.StartGame application
    /// </summary>
    public static class StartGameUtil {
        public static StartGameData BuildScanKillMutexData(string gameName, int instances, params string[] mutex) {
            StartGameData data = new StartGameData();
            data.Task = GameStarterTask.ScanKillMutex;
            string[] parameters = new string[instances];
            for (int i = 0; i < instances; i++) {
                ScanMutexData d = new ScanMutexData();
                d.ProcessName = gameName;
                d.Mutexes = mutex;

                parameters[i] = JsonConvert.SerializeObject(d);
            }
            data.Parameters = parameters;
            return data;
        }

        public static StartGameData BuildSymlinkGameData(SymlinkGameData[] games) {
            StartGameData data = new StartGameData();
            data.Task = GameStarterTask.SymlinkFolders;
            string[] argData = new string[games.Length];
            for (int i = 0; i < games.Length; i++) {
                argData[i] = JsonConvert.SerializeObject(games[i]);
            }
            data.Parameters = argData;
            return data;
        }

        public static StartGameData BuildMultipleTaskData(StartGameData[] tasks) {
            StartGameData data = new StartGameData();
            data.Task = GameStarterTask.MultipleTasks;
            string[] argData = new string[tasks.Length];
            for (int i = 0; i < tasks.Length; i++) {
                argData[i] = JsonConvert.SerializeObject(tasks[i]);
            }
            data.Parameters = argData;
            return data;
        }

        public static StartGameData BuildScanGamesData(SearchStorageInfo[] storage) {
            StartGameData data = new StartGameData();
            data.Task = GameStarterTask.ScanGames;
            string[] argData = new string[storage.Length];
            for (int i = 0; i < storage.Length; i++) {
                argData[i] = storage[i].Drive.Name;
            }
            data.Parameters = argData;
            return data;
        }


        public static void KillMutex(Process p, params string[] mutex) {
            ClearStartData();

            StartGameApp app = new StartGameApp();
            app.BeginKillMutex(p.Id, mutex);
            app.WaitForExit();
        }

        public static void SymlinkGames(SymlinkGameData[] games) {
            ClearStartData();

            StartGameApp app = new StartGameApp();
            app.BeginSymlinkGames(games);
            app.WaitForExit();
        }

        public static void ScanMutex(SymlinkGameData[] games) {
            ClearStartData();

            StartGameApp app = new StartGameApp();
            app.BeginSymlinkGames(games);
            app.WaitForExit();
        }

        public static string[] ScanGames(SearchStorageInfo[] storage) {
            ClearStartData();

            StartGameApp app = new StartGameApp();
            app.BeginScanGames(storage);
            app.WaitForExit();

            string dataPath = StartGameUtil.GetStartDataPath();
            string data = File.ReadAllText(dataPath);
            return JsonConvert.DeserializeObject<string[]>(data);
        }

        public static void RunPreBuiltData(StartGameData data, bool admin) {
            ClearStartData();

            StartGameApp app = new StartGameApp();
            app.RunStartGame(data, admin);
            app.WaitForExit();
        }

        public static void RunMultipleTasks(StartGameData[] data, bool admin) {
            ClearStartData();

            StartGameApp app = new StartGameApp();
            StartGameData final = BuildMultipleTaskData(data);
            app.RunStartGame(final, admin);
        }


        public static string GetStartDataPath() {
            string folder = Path.GetDirectoryName(GetStartGamePath());
            return Path.Combine(folder, "startgame.data");
        }

        public static string GetStartDataGamePath(string gameExePath) {
            string folder = Path.GetDirectoryName(gameExePath);
            return Path.Combine(folder, "startgame.data");
        }

        private static string ClearStartData() {
            string dataFile = GetStartDataPath();
            if (File.Exists(dataFile)) {
                File.Delete(dataFile);
            }
            return dataFile;
        }

        public static int StartGame(string pathToGame, string args, string workingDir = null) {
            string dataFile = GetStartDataGamePath(pathToGame);
            if (File.Exists(dataFile)) {
                File.Delete(dataFile);
            }

            StartGameApp app = new StartGameApp();
            app.BeginStartGame(pathToGame, args, workingDir);
            app.WaitForExit();

            string appId = File.ReadAllText(dataFile);
            return int.Parse(appId);
        }

        public static string GetStartGamePath() {
            string startLoc = Assembly.GetEntryAssembly().Location;

            if (Path.GetFileNameWithoutExtension(startLoc).ToLower() == "startgame") {
                return startLoc;
            } else {
                return Path.Combine(AssemblyUtil.GetStartFolder(), "bin", "StartGame.exe");
            }
        }
    }
}
