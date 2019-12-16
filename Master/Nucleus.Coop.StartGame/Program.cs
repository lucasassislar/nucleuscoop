using Newtonsoft.Json;
using Nucleus.Coop.StartGame.Properties;
using Nucleus.Gaming;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Platform.Windows.IO;
using Nucleus.Gaming.Platform.Windows.IO.MFT;
using Nucleus.Gaming.Tools.GameStarter;
using Nucleus.Gaming.Windows;
using SplitScreenMe.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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

        static void RenameMutex(string procId, string[] mutexes) {
            ConsoleU.WriteLine($"Process ID {procId} request to rename mutexes", Palette.Wait);
            proc = Process.GetProcessById(int.Parse(procId));

            ConsoleU.WriteLine($"Trying to rename mutexes {mutexes.Length} mutexes", Palette.Wait);
            for (int j = 0; j < mutexes.Length; j++) {
                string m = mutexes[j];
                string prefix = $"({j + 1}/{mutexes.Length}) ";
                ConsoleU.WriteLine(prefix + "Trying to rename mutex: " + m, Palette.Feedback);

                for (; ; ) {
                    if (ProcessUtil.RenameMutex(proc, m)) {
                        ConsoleU.WriteLine($"{prefix}Mutex rename {m}", Palette.Success);
                        break;
                    } else {
                        ConsoleU.WriteLine($"{prefix}Mutex {m} could not be rename", Palette.Error);
                    }
                    Thread.Sleep(250);
                }
            }
        }

        static void QueryMutex(string procId, string[] mutexes) {
            Log.WriteLine($"Process ID {procId} request to be queried for mutexes", Palette.Wait);
            proc = Process.GetProcessById(int.Parse(procId));

            ConsoleU.WriteLine($"Trying to query for any mutex's existance", Palette.Wait);

            bool[] existence = new bool[mutexes.Length];
            for (int j = 0; j < mutexes.Length; j++) {
                string m = mutexes[j];
                string prefix = $"({j + 1}/{mutexes.Length}) ";
                ConsoleU.WriteLine($"{prefix}Trying to scan if mutex exists: {m}", Palette.Feedback);

                bool exists = ProcessUtil.MutexExists(proc, m);
                Log.WriteLine(exists);
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


        [STAThread]
        static void Main(string[] args) {
            if (args.Length == 0) {
                Log.WriteLine("Invalid usage! Need arguments to proceed!", Palette.Error);
                return;
            }

            // We need this, else Windows will fake
            // all the data about monitors inside the application
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NotifyIcon notifyIcon = new NotifyIcon();
            ContextMenu contextMenu = new ContextMenu();

            MenuItem exitMenuItem = new MenuItem();
            contextMenu.MenuItems.AddRange(new MenuItem[] { exitMenuItem });
            exitMenuItem.Index = 0;
            exitMenuItem.Text = "Exit";
            exitMenuItem.Click += new EventHandler(exitClick);

            notifyIcon.Icon = Resources.icon;
            notifyIcon.Text = "Nucleus Coop GameTasks";
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Visible = true;

            string base64 = Encoding.UTF8.GetString(Convert.FromBase64String(args[0]));
            StartGameData data = JsonConvert.DeserializeObject<StartGameData>(base64);

            ThreadPool.QueueUserWorkItem(ExecuteTaskOnThread, data);

            Application.Run();
            notifyIcon.Visible = false;
        }

        private static void exitClick(object Sender, EventArgs e) {
            Application.Exit();
        }

        private static void ExecuteTaskOnThread(object state) {
            StartGameData data = (StartGameData)state;
            ExecuteTask(data);

            Application.Exit();
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
                    Log.WriteLine($"Kill Mutex Task");
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
                case GameStarterTask.RenameMutex: {
                    Log.WriteLine($"Rename Mutex Task");
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
                    Log.WriteLine($"Scan Kill Mutex");

                    List<int> processIds = new List<int>();

                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string scanMutexDataRaw = data.Parameters[j];
                        ScanMutexData scanMutex = JsonConvert.DeserializeObject<ScanMutexData>(scanMutexDataRaw);
                        Log.WriteLine($"Kill Mutex for process {scanMutex.ProcessName}");

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
                                    if (scanMutex.ShouldRename) {
                                        StartGameUtil.RenameMutex(p, scanMutex.Mutexes);
                                    } else {
                                        StartGameUtil.KillMutex(p, scanMutex.Mutexes);
                                    }
                                    //KillMutex(p.Id.ToString(), scanMutex.Mutexes);
                                    processIds.Add(p.Id);
                                    killedMutexes = true;
                                    break;
                                }

                                if (killedMutexes) {
                                    Log.WriteLine($"Killed all mutexes for process {scanMutex.ProcessName}");
                                    WriteToDataFile(Assembly.GetEntryAssembly().Location, true.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
                case GameStarterTask.MultipleTasks: {
                    Log.WriteLine($"Multiple tasks");
                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string taskDataRaw = data.Parameters[j];
                        StartGameData taskData = JsonConvert.DeserializeObject<StartGameData>(taskDataRaw);

                        Log.WriteLine($"Executing task {j + 1}");
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
                case GameStarterTask.ScanGames: {
                    // initialize game manager to read available handlers
                    GameManager gameManager = new GameManager();

                    List<string> games = new List<string>();
                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string driveName = data.Parameters[j];
                        //SearchStorageInfo info = JsonConvert.DeserializeObject<SearchStorageInfo>(storageData);
                        DriveInfo drive = new DriveInfo(driveName);

                        if (!drive.IsReady) {
                            continue;
                        }

                        Log.WriteLine($"> Searching drive {drive.Name} for game executables");

                        Dictionary<ulong, FileNameAndParentFrn> allExes = new Dictionary<ulong, FileNameAndParentFrn>();
                        MFTReader mft = new MFTReader();
                        mft.Drive = drive.RootDirectory.FullName;

                        // TODO: search only for specific games?
                        mft.EnumerateVolume(out allExes, new string[] { ".exe" });

                        foreach (KeyValuePair<UInt64, FileNameAndParentFrn> entry in allExes) {
                            FileNameAndParentFrn file = (FileNameAndParentFrn)entry.Value;

                            string name = file.Name;
                            string lower = name.ToLower();

                            string path = mft.GetFullPath(file);
                            if (path.Contains("$Recycle.Bin") ||
                                path.Contains(@"\Instance")) {
                                // noope
                                continue;
                            }

                            if (GameManager.Instance.AnyGame(lower)) {
                                Log.WriteLine($"Found game at path: {path}");
                                games.Add(path);
                            }
                        }
                    }

                    WriteToDataFile(Assembly.GetEntryAssembly().Location, JsonConvert.SerializeObject(games));
                }
                break;
                case GameStarterTask.SymlinkFolders:
                    for (int j = 0; j < data.Parameters.Length; j++) {
                        string symData = data.Parameters[j];
                        Log.WriteLine($"Symlink game instance {j + 1}");

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
