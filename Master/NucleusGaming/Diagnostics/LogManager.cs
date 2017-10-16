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
        private object locker;
        private List<ILogNode> logCallbacks;

        public LogManager()
        {
            locker = new object();

            instance = this;
            logPath = GetLogPath();

            logCallbacks = new List<ILogNode>();

            logStream = new FileStream(GetLogPath(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            logStream.Position = logStream.Length; // keep writing from where we left

            writer = new StreamWriter(logStream);
        }

        public static void RegisterForLogCallback(ILogNode node)
        {
            instance.logCallbacks.Add(node);
        }

        public static void UnregisterForLogCallback(ILogNode node)
        {
            instance.logCallbacks.Remove(node);
        }

        private static string GetAppDataPath()
        {
#if ALPHA
            string local = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(local, "Data");
#else
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Nucleus Coop");
#endif
        }

        protected static string GetLogPath()
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
            lock (locker)
            {
                string str = (string)s;
                writer.WriteLine(str);
                writer.Flush();

                if (logStream.Position > MaxSize)
                {
                    logStream.Position = 0;// write on top
                }
            }
        }

        public static void Log(string str)
        {
            Instance.PLog(str);
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
                            node.Log(writer);
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
