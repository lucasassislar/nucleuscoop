using Nucleus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartGame
{
    class Program
    {
        static int tries = 5;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid usage!");
                return;
            }

            string game = args[0];
            Process proc = null;
            int tri = 0;
            ProcessStartInfo startInfo;
            startInfo = new ProcessStartInfo();
            startInfo.FileName = game;
            if (args.Length >= 2)
            {
                startInfo.Arguments = args[1];
            }

        retry:
            try
            {
                proc = Process.Start(startInfo);
            }
            catch
            {
                tri++;
                if (tri < tries)
                {
                    Console.WriteLine("Failed to start process. Retrying...");
                    Thread.Sleep(1000);
                    goto retry;
                }
            }

            if (args.Length >= 3)
            {
                int timeToWait = int.Parse(args[2]);
                Thread.Sleep(timeToWait);

                if (proc.HasExited)
                {
                    tri++;
                    if (tri < tries)
                    {
                        Console.WriteLine("Failed to start process. Retrying...");
                        Thread.Sleep(1000);
                        goto retry;
                    }
                }
            }

            if (args.Length >= 3)
            {
                string[] mutex = args[3].Split(';');
                for (int i = 0; i < mutex.Length; i++)
                {
                    if (!ProcessUtil.KillMutex(proc, mutex[i]))
                    {
                        Console.WriteLine("Mutex " + mutex[i] + " could not be killed");
                    }
                    Thread.Sleep(500);
                }
            }

            Console.WriteLine(proc.Id);
        }
    }
}
