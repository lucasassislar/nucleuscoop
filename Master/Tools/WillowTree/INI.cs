using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WillowTree
{
    /// <summary>

    /// Create a New INI file to store or load data

    /// </summary>

    public class IniFile
    {
        
        public string path;
        private string[] arrListSectionNames = null;


        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileSectionNames", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, int nSize, string lpFileName);

        [DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileSection", CharSet = CharSet.Auto)]
        private static extern int WritePrivateProfileSectionNames(string lpAppName, string lpString, string lpFileName);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>

        /// INIFile Constructor.

        /// </summary>

        /// <PARAM name="INIPath"></PARAM>

        public IniFile(string INIPath)
        {
            path = INIPath;
            arrListSectionNames = null;

            // open file als read all sections (should be faster than using windows ini functions)
            if (!string.IsNullOrEmpty(path))
            {
                string line = null;
                StringBuilder temp = new StringBuilder(255);
                // Read the file and display it line by line.
                System.IO.StreamReader file =  new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    // Section
                    if (line.Length > 0) // no empty lines
                    {
                        if (line.StartsWith("[", StringComparison.Ordinal) && line.EndsWith("]", StringComparison.Ordinal))
                        {
                            // Section

                            temp.Append(line.Substring(1,line.Length-2));
                            temp.Append('\n');
                        }
                    }
                }
                temp.Length = temp.Length - 1; //remove last element
                arrListSectionNames = temp.ToString().Split('\n');
            }
        }

        public void SetFile(string INIPath)
        {
            path = INIPath;
        }

        // Rebuild the array (if changes are made to the ini (unlikely))
        public void SetReload()
        {
            arrListSectionNames = null;
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

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>

        /// Read Data Value From the Ini File

        /// </summary>

        /// <PARAM name="Section"></PARAM>

        /// <PARAM name="Key"></PARAM>

        /// <PARAM name="Path"></PARAM>

        /// <returns></returns>

        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, string.Empty, temp,
                                            255, this.path);
            return temp.ToString(0, i);

        }


        public string[] ListSectionNames()
        {
            if (arrListSectionNames == null)
            {
                try
                {
                    byte[] buffer = new byte[65535];
                    GetPrivateProfileSectionNames(buffer, 65535, path);
                    arrListSectionNames = Encoding.ASCII.GetString(buffer).Trim('\0').Split('\0');
                    //return parts;
                }
                catch { }
            }
            return arrListSectionNames;
        }

        public void WriteSectionNames(string selectionName, string typeString)
        {

                WritePrivateProfileSectionNames(selectionName, "Type="+typeString , path);
 
        }

    }
}