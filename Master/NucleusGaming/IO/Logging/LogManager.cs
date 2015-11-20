using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class LogManager
    {
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


        public LogManager()
        {
            instance = this;
            logPath = GetLogPath();
            logStream = File.OpenWrite(logPath);
            writer = new StreamWriter(logStream);
        }
        private string logPath;
        private Stream logStream;
        private StreamWriter writer;

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

            try
            {
                writer.WriteLine(str);
                //logStream.Flush();
            }
            catch
            {
            }
        }

        public static void Log(string str)
        {
            Instance.PLog(str);
        }
    }
}
