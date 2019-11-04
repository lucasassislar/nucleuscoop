using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.IO {
    /// <summary>
    /// Reads and modifies parameters in a SCR configuration
    /// file (*.scr) Dying Light
    /// </summary>
    public class ScrConfigFile {
        protected string path;
        protected string rawData;
        protected string backupData;
        private List<SaveInfo> infos;

        public string RawData {
            get { return rawData; }
        }

        public ScrConfigFile(string filePath) {
            path = filePath;
            infos = new List<SaveInfo>();

            if (File.Exists(path)) {
                rawData = File.ReadAllText(path);
                backupData = string.Copy(rawData);

                Parse(backupData);
            } else {
                rawData = "";
                backupData = "";
            }
        }

        private void Parse(string data) {
            string[] lines = data.Split('\n');

            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                if (line.StartsWith("!")) {
                    continue;
                }

                int nextParen = line.IndexOf('(');
                if (nextParen == -1) {
                    continue;
                }

                int nextEndParen = line.IndexOf(')');
                SaveInfo info = new SaveInfo();

                string key = line.Remove(nextParen, line.Length - nextParen);
                info.Add("Key", key);

                string sub = line.Substring(nextParen + 1, nextEndParen - nextParen - 1);
                string[] parameters = sub.Split(',');

                info.Add("Parameters", parameters.Length.ToString());
                for (int j = 0; j < parameters.Length; j++) {
                    info.Add("Param" + (j + 1), parameters[j]);
                }

                infos.Add(info);
            }
        }

        /// <summary>
        /// Flushes all changes to disk
        /// </summary>
        public void Save(string newPath = "") {
            if (string.IsNullOrEmpty(newPath)) {
                newPath = path;
            }

            if (File.Exists(newPath)) {
                File.Delete(newPath);
            }

            using (Stream str = File.OpenWrite(newPath)) {
                using (StreamWriter writer = new StreamWriter(str)) {
                    writer.Write(rawData);

                    //foreach (var info in infos) {
                    //    string line = $"{info["Key"]}(";

                    //    string parameters = info["Parameters"];
                    //    int total = int.Parse(parameters);
                    //    for (int i = 0; i < total; i++) {
                    //        string strValue = info["Param" + (i + 1)];
                    //        if (strValue.ToLower().Contains("false")) {
                    //            line = "!" + line;
                    //        } else {
                    //            line += strValue;
                    //            if (i != total - 1) {
                    //                line += ",";
                    //            }
                    //        }
                    //    }
                    //    line += ")";

                    //    writer.WriteLine(line);
                    //}

                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Reverts the data to the initially read data
        /// </summary>
        public void RevertToBackup() {
            rawData = string.Copy(backupData);
        }

        public void ChangeProperty(SaveInfo source) {
            string key = source["Key"];

            string parameters = source["Parameters"];
            int total = int.Parse(parameters);

            bool remove = false;

            string actualValue = "(";
            for (int i = 0; i < total; i++) {
                string paramKey = "Param" + (i + 1);
                string paramData = source[paramKey];
                if (paramData.Contains("false")) {
                    remove = true;
                } else {
                    actualValue += paramData;
                    if (i != total - 1) {
                        actualValue += ",";
                    }
                }
            }
            actualValue += ")";

            int start;
            int end;
            int keyPos = GetPosition(key, out start, out end);
            if (keyPos != -1) {
                if (remove) {
                    rawData = rawData.Insert(keyPos, "!");
                } else {
                    rawData = rawData.Remove(start, end - start);
                    rawData = rawData.Insert(start, actualValue);
                }
            } else {
                int why = -1;
            }
        }

        public int GetPosition(string propName, out int start, out int end) {
            start = -1;
            end = -1;

            int keyPos = 0;
            for (; ; ) {
                keyPos = rawData.IndexOf(propName, keyPos + 1);
                if (keyPos == -1 || 
                    keyPos == 0 || 
                    rawData[keyPos - 1] != '!') {
                    break;
                }
            }
            if (keyPos == -1 ||
                keyPos > 0 && rawData[keyPos - 1] == '!') {
                return -1;
            }

            start = rawData.IndexOf('(', keyPos);
            end = rawData.IndexOf(')', start) + 1;

            return keyPos;
        }
    }
}
