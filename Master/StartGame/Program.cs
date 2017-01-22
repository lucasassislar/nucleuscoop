using Nucleus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartGame
{
    class Program
    {
        private static int tries = 5;
        private static Process proc;

        static void StartGame(string path, string args = "")
        {
            int tri = 0;
            ProcessStartInfo startInfo;
            startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.Arguments = args;

            try
            {
                proc = Process.Start(startInfo);
                Console.WriteLine("ID:" + proc.Id);
            }
            catch
            {
                tri++;
                if (tri < tries)
                {
                    Console.WriteLine("Failed to start process. Retrying...");
                    StartGame(path, args);
                }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid usage! Need arguments to proceed!");
                return;
            }

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i];
                    string[] splited = arg.Split(':');

                    string key = splited[0].ToLower();

                    if (key == "game")
                    {
                        string[] arguments = (splited[1] + ":" + splited[2]).Split(';');
                        string path = arguments[0];

                        string argu = "";
                        if (arguments.Length > 1)
                        {
                            argu = arguments[1];
                        }
                        StartGame(path, argu);
                    }
                    else if (key == "mutex")
                    {
                        string[] mutex = splited[1].Split(';');
                        for (int j = 0; j < mutex.Length; j++)
                        {
                            string m = mutex[j];
                            if (!ProcessUtil.KillMutex(proc, m))
                            {
                                Console.WriteLine("Mutex " + m + " could not be killed");
                            }
                            Thread.Sleep(500);
                        }
                    }
                    else if (key == "proc")
                    {
                        string procId = splited[1];
                        int id = int.Parse(procId);
                        proc = Process.GetProcessById(id);
                    }
                    else if (key == "output")
                    {
                        string[] mutex = splited[1].Split(';');
                        bool all = true;

                        for (int j = 0; j < mutex.Length; j++)
                        {
                            string m = mutex[j];
                            bool exists = ProcessUtil.MutexExists(proc, m);
                            if (!exists)
                            {
                                all = false;
                            }

                            Console.WriteLine("Mutex " + m + (exists ? " exists" : " doesn't exist"));
                            Thread.Sleep(500);
                        }
                        Console.WriteLine(all.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
