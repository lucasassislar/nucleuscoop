using Nucleus.Gaming.Platform.Windows.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Nucleus.Gaming.Platform.Windows.Interop {
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class IniFile {
        private string path;

        public string Path {
            get { return path; }
        }

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath) {
            path = INIPath;
        }

        private Dictionary<string, string> sectionlessData;

        public void InitializeSectionless() {
            sectionlessData = File.ReadLines(path)
               .Where(line => !string.IsNullOrWhiteSpace(line)) // empty lines
               .Where(line => !line.Trim().StartsWith(";")) // commented lines
               .Where(line => !line.Trim().StartsWith("#"))
               .Select(line => line.Split(new char[] { '=' }, 2, 0))
               .ToDictionary(parts => parts[0].Trim(), parts => parts[1]);
        }

        public void IniWriteValue(string Key, string Value) {
            sectionlessData[Key] = Value;
        }


        public void SaveSectionless() {
            File.Delete(path);

            using (Stream str = File.OpenWrite(path)) {
                using (StreamWriter writer = new StreamWriter(str)) {
                    foreach (var keyPair in sectionlessData) {
                        string key = keyPair.Key;
                        string value = keyPair.Value;
                        writer.WriteLine($"{key}={value}");
                    }
                }
            }
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value) {
            Kernel32Interop.WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key) {
            StringBuilder temp = new StringBuilder(255);
            int i = Kernel32Interop.GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();

        }
    }
}