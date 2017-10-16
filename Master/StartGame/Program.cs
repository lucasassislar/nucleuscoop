using Nucleus;
using Nucleus.Gaming;
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
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i];
                    ConsoleU.WriteLine("Parsing line " + i + ": " + arg, Palette.Feedback);

                    string argument = "";
                    for (int j = i; j < args.Length; j++)
                    {
                        string skey = args[j];
                        if (!skey.Contains("monitors")
                             && !skey.Contains("game")
                             && !skey.Contains("mutex")
                             && !skey.Contains("proc")
                             && !skey.Contains("output"))
                        {
                            i++;
                            if (string.IsNullOrEmpty(argument))
                            {
                                argument = skey;
                            }
                            else
                            {
                                argument = argument + " " + skey;
                            }
                        }
                    }
                    ConsoleU.WriteLine("Extra arguments:" + argument, Palette.Feedback);

                    string[] splited = (arg + argument).Split(':');
                    string key = splited[0].ToLower();

                    if (key.Contains("monitors"))
                    {

                    }
                    else if (key.Contains("game"))
                    {
                        string data = splited[1];
                        string[] subArgs = data.Split(';');
                        string path = subArgs[0];

                        string argu = null;
                        if (subArgs.Length > 1)
                        {
                            argu = subArgs[1];
                        }

                        string workingDir = null;
                        if (path.Contains("|"))
                        {
                            string[] div = path.Split('|');
                            path = div[0];
                            workingDir = div[1];
                        }

                        ConsoleU.WriteLine($"Start game: EXE: {path} ARGS: {argu} WORKDIR: {workingDir}", Palette.Feedback);
                        StartGame(path, argu, workingDir);
                    }
                    else if (key.Contains("mutex"))
                    {
                        string[] mutex = splited[1].Split(';');
                        ConsoleU.WriteLine("Trying to kill mutexes", Palette.Wait);
                        for (int j = 0; j < mutex.Length; j++)
                        {
                            string m = mutex[j];
                            ConsoleU.WriteLine("Trying to kill mutex: " + m, Palette.Feedback);

                            if (!ProcessUtil.KillMutex(proc, m))
                            {
                                ConsoleU.WriteLine("Mutex " + m + " could not be killed", Palette.Error);
                            }
                            else
                            {
                                ConsoleU.WriteLine("Mutex killed " + m, Palette.Success);
                            }
                            Thread.Sleep(150);
                        }
                    }
                    else if (key.Contains("proc"))
                    {
                        string procId = splited[1];
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
                    else if (key.Contains("output"))
                    {
                        string[] mutex = splited[1].Split(';');
                        bool all = true;

                        for (int j = 0; j < mutex.Length; j++)
                        {
                            string m = mutex[j];
                            ConsoleU.WriteLine("Requested mutex: " + m, Palette.Error);
                            bool exists = ProcessUtil.MutexExists(proc, m);
                            if (!exists)
                            {
                                all = false;
                            }
                            
                            Thread.Sleep(500);
                        }
                        Console.WriteLine(all.ToString());
                    }
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
