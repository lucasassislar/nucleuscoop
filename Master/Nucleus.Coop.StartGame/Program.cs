using Newtonsoft.Json;
using Nucleus;
using Nucleus.Gaming;
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

namespace StartGame
{
    class Program
    {
        private static int tries = 5;
        private static Process proc;

        static void StartGame(string path, string args = "", string workingDir = null)
        {
            if (!Path.IsPathRooted(path))
            {
                string root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                path = Path.Combine(root, path);
            }

            int tri = 0;
            ProcessStartInfo startInfo;
            startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.Arguments = args;
            if (!string.IsNullOrWhiteSpace(workingDir))
            {
                startInfo.WorkingDirectory = workingDir;
            }

#if RELEASE
            try
#endif
            {
                proc = Process.Start(startInfo);
                ConsoleU.WriteLine("Game started, process ID:" + proc.Id, Palette.Success);
            }
#if RELEASE
            catch
            {
                tri++;
                if (tri < tries)
                {
                    ConsoleU.WriteLine("Failed to start process. Retrying...");
                    StartGame(path, args);
                }
            }
#endif
        }

        static void Main(string[] args)
        {
            // We need this, else Windows will fake
            // all the data about monitors inside the application
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            if (args.Length == 0)
            {
                ConsoleU.WriteLine("Invalid usage! Need arguments to proceed!", Palette.Error);
                return;
            }

#if RELEASE
            try
#endif
            {
                string base64 = Encoding.UTF8.GetString(Convert.FromBase64String(args[0]));
                StartGameData data = JsonConvert.DeserializeObject<StartGameData>(base64);

                switch (data.Task)
                {
                    case GameStarterTask.StartGame:
                        {
                            string gamePath = data.Parameters[0];
                            string gameArgs = data.Parameters[1];
                            string gameWorkingDir = data.Parameters[2];

                            ConsoleU.WriteLine($"Start game: EXE: {gamePath} ARGS: {gameArgs} WORKDIR: {gameWorkingDir}", Palette.Feedback);
                            StartGame(gamePath, gameArgs, gameWorkingDir);
                        }
                        break;
                    case GameStarterTask.KillMutex:
                        {
                            string procId = data.Parameters[0];
                            ConsoleU.WriteLine($"Process ID {procId} request to kill mutexes", Palette.Wait);
                            proc = Process.GetProcessById(int.Parse(procId));

                            ConsoleU.WriteLine($"Trying to kill mutexes {data.Parameters.Length - 1} mutexes", Palette.Wait);
                            for (int j = 1; j < data.Parameters.Length; j++)
                            {
                                string m = data.Parameters[j];
                                string prefix = $"({j}/{data.Parameters.Length - 1}) ";
                                ConsoleU.WriteLine(prefix + "Trying to kill mutex: " + m, Palette.Feedback);

                                if (!ProcessUtil.KillMutex(proc, m))
                                {
                                    ConsoleU.WriteLine(prefix + "Mutex " + m + " could not be killed", Palette.Error);
                                }
                                else
                                {
                                    ConsoleU.WriteLine(prefix + "Mutex killed " + m, Palette.Success);
                                }
                                Thread.Sleep(150);
                            }
                        }
                        break;
                    case GameStarterTask.QueryMutex:
                        {
                            string procId = data.Parameters[0];
                            ConsoleU.WriteLine($"Process ID {procId} request to be queried for mutexes", Palette.Wait);
                            proc = Process.GetProcessById(int.Parse(procId));

                            ConsoleU.WriteLine($"Trying to query for any mutex's existance", Palette.Wait);
                            for (int j = 0; j < data.Parameters.Length; j++)
                            {
                                string m = data.Parameters[j];
                                string prefix = $"({j + 1}/{data.Parameters.Length}) ";
                                ConsoleU.WriteLine(prefix + "Trying to kill mutex: " + m, Palette.Feedback);

                                if (!ProcessUtil.KillMutex(proc, m))
                                {
                                    ConsoleU.WriteLine(prefix + "Mutex " + m + " could not be killed", Palette.Error);
                                }
                                else
                                {
                                    ConsoleU.WriteLine(prefix + "Mutex killed " + m, Palette.Success);
                                }
                                Thread.Sleep(150);
                            }
                        }
                        break;
                    case GameStarterTask.FindProcessId:
                        {
                            string procId = data.Parameters[0];
                            int id = int.Parse(procId);
                            try
                            {
                                proc = Process.GetProcessById(id);
                                ConsoleU.WriteLine($"Process ID {id} found!", Palette.Success);
                            }
                            catch
                            {
                                ConsoleU.WriteLine($"Process ID {id} not found", Palette.Error);
                            }
                        }
                        break;
                    case GameStarterTask.ListMonitors:
                        break;
                }
            }
#if RELEASE
            catch (Exception ex)
            {
                ConsoleU.WriteLine(ex.Message);
            }
#endif
        }
    }
}
