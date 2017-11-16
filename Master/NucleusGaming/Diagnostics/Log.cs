using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Gaming.Diagnostics
{
    public class Log
    {
        private static Log instance;
        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    new Log(true);
                }
                return instance;
            }
        }

        public static readonly long MaxSize = 1024 * 1024 * 1024; // 16mb
        private string logPath;
        private Stream logStream;
        private StreamWriter writer;
        private object locker;
        private OutputLevel consoleLevel;
        private bool enableLogging;
        private List<ILogNode> logCallbacks;

        public Log(bool enableLogging)
        {
            this.enableLogging = enableLogging;
            locker = new object();

            instance = this;

            if (enableLogging)
            {
                logPath = GetLogPath();

                logStream = new FileStream(GetLogPath(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                logStream.Position = logStream.Length; // keep writing from where we left

                writer = new StreamWriter(logStream);
                consoleLevel = OutputLevel.Low;
            }
        }

        public static void RegisterForLogCallback(ILogNode node)
        {
            Instance.logCallbacks.Add(node);
        }

        public static void UnregisterForLogCallback(ILogNode node)
        {
            Instance.logCallbacks.Remove(node);
        }

        public void LogExceptionFile(Exception ex)
        {
            string local = GetAppDataPath();
            DateTime now = DateTime.Now;
            string file = string.Format("{0}{1}{2}_{3}{4}{5}", now.Day.ToString("00"), now.Month.ToString("00"), now.Year.ToString("0000"), now.Hour.ToString("00"), now.Minute.ToString("00"), now.Second.ToString("00")) + ".log";
            string path = Path.Combine(local, file);

            using (Stream stream = File.OpenWrite(path))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("[Header]");
                    writer.WriteLine(now.ToLongDateString());
                    writer.WriteLine(now.ToLongTimeString());
                    writer.WriteLine("Nucleus Coop Alpha v" + Globals.Version);
                    writer.WriteLine("[PC Specs]");

                    writer.WriteLine("[Message]");
                    writer.WriteLine(ex.Message);
                    writer.WriteLine("[Stacktrace]");
                    writer.WriteLine(ex.StackTrace);

                    for (int i = 0; i < logCallbacks.Count; i++)
                    {
                        ILogNode node = logCallbacks[i];
                        try
                        {
                            node.OnFailureLog(writer);
                        }
                        catch
                        {
                            writer.WriteLine("LogNode failed to log: " + node.ToString());
                        }
                    }
                }
            }

            MessageBox.Show("Application crash. Log generated at Data/" + file);
            Application.Exit();
        }

        public static void SetConsoleOutputLevel(OutputLevel level)
        {
            instance.consoleLevel = level;
        }

        private static string GetAppDataPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        protected static string GetLogPath()
        {
            return Path.Combine(GetAppDataPath(), "app.log");
        }

        private object writeLineLock = new object();
        private void WriteLine(string str, ConsoleColor color)
        {
            lock (writeLineLock)
            {
                DateTime now = DateTime.Now;
                ConsoleColor startColor = Console.ForegroundColor;

                Console.ForegroundColor = color;
                Console.Write($"[{now.ToLongTimeString()}] ");
                Console.Write(str + Environment.NewLine);
                Console.ForegroundColor = startColor;
            }
        }

        public void PLog(string str, ConsoleColor color, OutputLevel displayLevel)
        {
            if (displayLevel >= consoleLevel)
            {
                WriteLine(str, color);
            }

            if (enableLogging)
            {
                LogData log = new LogData(str, color, displayLevel);
                ThreadPool.QueueUserWorkItem(doLog, log);
            }
        }

        public struct LogData
        {
            public string String { get; set; }
            public ConsoleColor Color { get; set; }
            public OutputLevel OutputLevel { get; set; }

            public LogData(string str, ConsoleColor color, OutputLevel displayLevel)
            {
                String = str;
                Color = color;
                OutputLevel = displayLevel;
            }
        }

        private void doLog(object s)
        {
            lock (locker)
            {
                LogData data = (LogData)s;

                //, ConsoleColor color, OutputLevel displayLevel
                writer.WriteLine(data.String);
                writer.Flush();

                if (logStream.Position > MaxSize)
                {
                    logStream.Position = 0;// write on top
                }
            }
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        public static void WriteLine()
        {
            Instance.PLog("", ConsoleColor.Gray, OutputLevel.Low);
        }
        public static void WriteLine(string str, ConsoleColor color = ConsoleColor.Gray, OutputLevel displayLevel = OutputLevel.Low)
        {
            Instance.PLog(str, color, displayLevel);
        }
    }
}
