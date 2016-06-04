using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nucleus.Gaming
{
    public class LogManager
    {
        public static readonly long MaxSize = 1024 * 1024 * 1024; // 1mb

        private static LogManager instance;
        public static LogManager Instance
        {
            get
            {
                if (instance == null)
                {
                    new LogManager();
                }
                return instance;
            }
        }

        private string logPath;
        private Stream logStream;
        private StreamWriter writer;

        public LogManager()
        {
            instance = this;
            logPath = GetLogPath();

            logStream = new FileStream(GetLogPath(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            logStream.Position = logStream.Length; // keep writing from where we left

            writer = new StreamWriter(logStream);
        }

        private string GetAppDataPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Nucleus Coop");
        }

        protected string GetLogPath()
        {
            return Path.Combine(GetAppDataPath(), "app.log");
        }

        public void PLog(string str)
        {
            Console.WriteLine(str);
            ThreadPool.QueueUserWorkItem(doLog, str);
        }

        private void doLog(object s)
        {
            string str = (string)s;
            writer.WriteLine(str);
            writer.Flush();

            if (logStream.Length > MaxSize)
            {
                logStream.Position = 0;// write on top
            }
        }

        public static void Log(string str)
        {
            Instance.PLog(str);
        }

        public static void Log(string str, object par1)
        {
            Instance.PLog(string.Format(str, par1));
        }
        public static void Log(string str, object par1, object par2)
        {
            Instance.PLog(string.Format(str, par1, par2));
        }
        public static void Log(string str, object par1, object par2, object par3)
        {
            Instance.PLog(string.Format(str, par1, par2, par3));
        }
        public static void Log(string str, params object[] pars)
        {
            Instance.PLog(string.Format(str, pars));
        }
    }
}
