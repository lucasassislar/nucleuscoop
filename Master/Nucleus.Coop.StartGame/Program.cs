using Newtonsoft.Json;
using Nucleus;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Platform.Windows.IO;
using Nucleus.Gaming.Tools.GameStarter;
using Nucleus.Gaming.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartGame {
    class Program {
        private static Process proc;

        static void StartGame(string path, string args = "", string workingDir = null) {
            if (!Path.IsPathRooted(path)) {
                string root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                path = Path.Combine(root, path);
            }

            ProcessStartInfo startInfo;
            startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.Arguments = args;
            if (!string.IsNullOrWhiteSpace(workingDir)) {
                startInfo.WorkingDirectory = workingDir;
            }

            proc = Process.Start(startInfo);
            ConsoleU.WriteLine("Game started, process ID:" + proc.Id, Palette.Success);
            WriteToDataFile(path, proc.Id.ToString());
        }

        static void KillMutex(string procId, string[] mutexes) {
            ConsoleU.WriteLine($"Process ID {procId} request to kill mutexes", Palette.Wait);
            proc = Process.GetProcessById(int.Parse(procId));

            ConsoleU.WriteLine($"Trying to kill mutexes {mutexes.Length} mutexes", Palette.Wait);
            for (int j = 0; j < mutexes.Length; j++) {
                string m = mutexes[j];
                string prefix = $"({j + 1}/{mutexes.Length}) ";
                ConsoleU.WriteLine(prefix + "Trying to kill mutex: " + m, Palette.Feedback);

                for (; ; ) {
                    if (ProcessUtil.KillMutex(proc, m)) {
                        ConsoleU.WriteLine($"{prefix}Mutex killed {m}", Palette.Success);
                        break;
                    } else {
                        ConsoleU.WriteLine($"{prefix}Mutex {m} could not be killed", Palette.Error);
                    }
                    Thread.Sleep(250);
                }
            }
        }

        static void QueryMutex(string procId, string[] mutexes) {
            ConsoleU.WriteLine($"Process ID {procId} request to be queried for mutexes", Palette.Wait);
            proc = Process.GetProcessById(int.Parse(procId));

            ConsoleU.WriteLine($"Trying to query for any mutex's existance", Palette.Wait);

            bool[] existence = new bool[mutexes.Length];
            for (int j = 0; j < mutexes.Length; j++) {
                string m = mutexes[j];
                string prefix = $"({j + 1}/{mutexes.Length}) ";
                ConsoleU.WriteLine($"{prefix}Trying to scan if mutex exists: {m}", Palette.Feedback);

                bool exists = ProcessUtil.MutexExists(proc, m);
                Console.WriteLine(exists);
            }
            Thread.Sleep(250);

            string json = JsonConvert.SerializeObject(existence);
            // no game path, save to startgame directory/Nucleus folder
            WriteToDataFile(Assembly.GetEntryAssembly().Location, json);
        }

        static void WriteToDataFile(string path, string data) {
            string folder = Path.GetDirectoryName(path);
            string dataFile = Path.Combine(folder, "startgame.data");
            if (File.Exists(dataFile)) {
                File.Delete(dataFile);
            }
            File.WriteAllText(dataFile, data);
        }

        static void Main(string[] args) {
            if (args.Length == 0) {
                ConsoleU.WriteLine("Invalid usage! Need arguments to proceed!", Palette.Error);
                return;
            }

            // We need this, else Windows will fake
            // all the data about monitors inside the application
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            string base64 = Encoding.UTF8.GetString(Convert.FromBase64String(args[0]));
            StartGameData data = JsonConvert.DeserializeObject<StartGameData>(base64);

            ExecuteTask(data);
        }

        private static void ExecuteTask(StartGameData data) {
            switch (data.Task) {
                case GameStarterTask.StartGame: {
                    string gamePath = data.Parameters[0];
                    string gameArgs = data.Parameters[1];
                    string gameWorkingDir = data.Parameters[2];

                    ConsoleU.WriteLine($"Start game: EXE: {gamePath} ARGS: {gameArgs} WORKDIR: {gameWorkingDir}", Palette.Feedback);
                    StartGame(gamePath, gameArgs, gameWorkingDir);
                }
                break;
                case GameStarterTask.KillMutex: {
                    Console.WriteLine($"Kill Mutex Task");
                    string procId = data.Parameters[0];
                    string[] mutexes = new string[data.Parameters.Length - 1];
                    for (int j = 1; j < data.Parameters.Length; j++) {
                        string m = data.Parameters[j];
                        mutexes[j - 1] = m;
                    }
                    KillMutex(procId, mutexes);
                    WriteToDataFile(Assembly.GetEntryAssembly().Location, true.ToString());
                }
                break;
                case GameStarterTask.ScanKillMutex: {
                    Console.WriteLine($"Scan Kill Mutex");

                    List<int> processIds = new List<int>();

                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string scanMutexDataRaw = data.Parameters[j];
                        ScanMutexData scanMutex = JsonConvert.DeserializeObject<ScanMutexData>(scanMutexDataRaw);
                        Console.WriteLine($"Kill Mutex for process {scanMutex.ProcessName}");

                        for (; ; ) {
                            Process[] procs = Process.GetProcessesByName(scanMutex.ProcessName);
                            if (procs == null || procs.Length == 0) {
                                Thread.Sleep(250);
                            } else {
                                // kill mutexes
                                bool killedMutexes = false;
                                for (int k = 0; k < procs.Length; k++) {
                                    Process p = procs[k];
                                    if (processIds.Contains(p.Id)) {
                                        continue;
                                    }

                                    // start other process, as the mutexes are only truly killed
                                    // when the process is ended
                                    StartGameUtil.KillMutex(p, scanMutex.Mutexes);
                                    //KillMutex(p.Id.ToString(), scanMutex.Mutexes);
                                    processIds.Add(p.Id);
                                    killedMutexes = true;
                                    break;
                                }

                                if (killedMutexes) {
                                    Console.WriteLine($"Killed all mutexes for process {scanMutex.ProcessName}");
                                    WriteToDataFile(Assembly.GetEntryAssembly().Location, true.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
                case GameStarterTask.MultipleTasks: {
                    Console.WriteLine($"Multiple tasks");
                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string taskDataRaw = data.Parameters[j];
                        StartGameData taskData = JsonConvert.DeserializeObject<StartGameData>(taskDataRaw);

                        Console.WriteLine($"Executing task {j + 1}");
                        ExecuteTask(taskData);
                    }
                }
                break;
                case GameStarterTask.QueryMutex: {
                    string procId = data.Parameters[0];
                    string[] mutexes = new string[data.Parameters.Length - 1];
                    for (int j = 1; j < data.Parameters.Length; j++) {
                        string m = data.Parameters[j];
                        mutexes[j - 1] = m;
                    }
                }
                break;
                case GameStarterTask.ListMonitors:
                    break;
                case GameStarterTask.SymlinkFolders:
                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string symData = data.Parameters[j];
                        Console.WriteLine($"Symlink game instance {j + 1}");

                        SymlinkGameData gameData = JsonConvert.DeserializeObject<SymlinkGameData>(symData);
                        int exitCode;
                        WinDirectoryUtil.LinkDirectory(gameData.SourcePath, new DirectoryInfo(gameData.SourcePath), gameData.DestinationPath, out exitCode, gameData.DirExclusions, gameData.FileExclusions, gameData.FileCopies, true);
                    }
                    WriteToDataFile(Assembly.GetEntryAssembly().Location, true.ToString());
                    break;
            }
        }
    }
}
