using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    /// <summary>
    /// Reads and modifies parameters in a Source Engine configuration
    /// file (*.cfg)
    /// </summary>
    public class SourceCfgFile
    {
        protected string path;
        protected string rawData;
        protected string backupData;

        public string RawData
        {
            get { return rawData; }
        }

        public SourceCfgFile(string filePath)
        {
            path = filePath;

            if (File.Exists(path))
            {
                rawData = File.ReadAllText(path);
                backupData = string.Copy(rawData);
            }
            else
            {
                rawData = "";
                backupData = "";
            }
        }

        /// <summary>
        /// Flushes all changes to disk
        /// </summary>
        public void Save(string newPath = "")
        {
            if (string.IsNullOrEmpty(newPath))
            {
                newPath = path;
            }

            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }

            using (Stream str = File.OpenWrite(newPath))
            {
                using (StreamWriter writer = new StreamWriter(str))
                {
                    writer.Write(rawData);

                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Reverts the data to the initially read data
        /// </summary>
        public void RevertToBackup()
        {
            rawData = string.Copy(backupData);
        }

        public bool ChangeProperty(string propertyName, string value)
        {
            int start;
            int end;
            if (GetPosition(rawData, propertyName, out start, out end))
            {
                rawData = rawData.Remove(start, end - start);
                rawData = rawData.Insert(start, value);

                return true;
            }
            return false;
        }

        private bool GetPosition(string text, string word, out int start, out int end)
        {
            start = -1;
            end = 0;

            int def = text.IndexOf(word);
            if (def == -1)
            {
                return false;
            }

            int aspas = 0;
            bool firstNumber = true;
            for (int i = def; i < text.Length; i++)
            {
                char c = text[i];
                if (start == -1)
                {
                    if (c == '"')
                    {
                        aspas++;
                        if (aspas == 2)
                        {
                            start = i;
                        }
                    }
                }
                else
                {
                    if (StringUtil.IsNumber(c) && firstNumber)
                    {
                        firstNumber = false;
                        start = i;
                    }
                    if (!StringUtil.IsNumber(c))
                    {
                        end = i;
                        break;
                    }
                }
            }

            return true;
        }
    }
}
