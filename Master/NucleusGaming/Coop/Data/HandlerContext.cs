using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.IO;
using Nucleus.Gaming.Platform.Windows.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nucleus.Gaming.Coop
{
    public class HandlerContext
    {
        private GameProfile profile;
        private PlayerInfo pInfo;

        [Dynamic(AutoHandles = true)]
        public string ExePath;
        [Dynamic(AutoHandles = true)]
        public string RootInstallFolder;
        [Dynamic(AutoHandles = true)]
        public string RootFolder;

        public GameHookData Hook = new GameHookData();
        public double HandlerInterval;
        public bool Debug;
        public string Error;
        public int Interval;
        public bool SymlinkExe;
        public bool SupportsKeyboard;
        public string[] ExecutableContext;
        public string ExecutableName;
        public string SteamID;
        public string GUID;
        public string GameName;
        public int MaxPlayers;
        public int MaxPlayersOneMonitor;
        public SaveType SaveType;
        public string SavePath;
        public string StartArguments;
        public string BinariesFolder;
        public string WorkingFolder;
        public bool NeedsSteamEmulation;
        public string[] KillMutex;
        public string LauncherExe;
        public string LauncherTitle;
        public int PlayerID;
        public bool IsFullscreen;
        public UserInfo User = new UserInfo();
        public DPIHandling DPIHandling = DPIHandling.True;
        public Dictionary<string, string> AdditionalData;

        public Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public Dictionary<string, object> Options
        {
            get { return profile.Options; }
        }

        public int Width
        {
            get
            {
                switch (DPIHandling)
                {
                    case DPIHandling.Scaled:
                        return (int)((pInfo.MonitorBounds.Width * DPIManager.Scale) + 0.5);
                    case DPIHandling.InvScaled:
                        return (int)((pInfo.MonitorBounds.Width * (1 / DPIManager.Scale)) + 0.5);
                    case DPIHandling.True:
                    default:
                        return pInfo.MonitorBounds.Width;
                }
            }
        }

        public int Height
        {
            get
            {
                switch (DPIHandling)
                {
                    case DPIHandling.Scaled:
                        return (int)((pInfo.MonitorBounds.Height * DPIManager.Scale) + 0.5);
                    case DPIHandling.InvScaled:
                        return (int)((pInfo.MonitorBounds.Height * (1 / DPIManager.Scale)) + 0.5);
                    case DPIHandling.True:
                    default:
                        return pInfo.MonitorBounds.Height;
                }
            }
        }

        
        public HandlerContext(GameProfile prof, PlayerInfo info)
        {
            profile = prof;
            pInfo = info;
        }

        public string CombinePath(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public void CopyFile(string fileSource, string fileDestination, bool overwrite)
        {
            File.Copy(fileSource, fileDestination, overwrite);
        }

        public string GetFolder(Folder folder)
        {
            return AdditionalData[folder.ToString()];
        }

        public void WriteTextFile(string path, string[] lines)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllLines(path, lines);
        }

        public CfgSaveInfo NewCfgSaveInfo(string section, string key, string value)
        {
            return new CfgSaveInfo(section, key, value);
        }

        public IniSaveInfo NewIniSaveInfo(string section, string key, string value)
        {
            return new IniSaveInfo(section, key, value);
        }

        public void ModifySaveFile(string installSavePath, string saveFullPath, SaveType type, params SaveInfo[] info)
        {
            // this needs to be dynamic someday
            switch (type)
            {
                case SaveType.CFG:
                    {
                        SourceCfgFile cfg = new SourceCfgFile(installSavePath);
                        for (int j = 0; j < info.Length; j++)
                        {
                            SaveInfo save = info[j];
                            if (save is CfgSaveInfo)
                            {
                                CfgSaveInfo option = (CfgSaveInfo)save;
                                cfg.ChangeProperty(option.Section, option.Key, option.Value);
                            }
                        }
                        cfg.Save(saveFullPath);
                    }
                    break;
                case SaveType.INI:
                    {
                        if (!installSavePath.Equals(saveFullPath))
                        {
                            File.Copy(installSavePath, saveFullPath);
                        }
                        IniFile file = new IniFile(saveFullPath);
                        for (int j = 0; j < info.Length; j++)
                        {
                            SaveInfo save = info[j];
                            if (save is IniSaveInfo)
                            {
                                IniSaveInfo ini = (IniSaveInfo)save;
                                file.IniWriteValue(ini.Section, ini.Key, ini.Value);
                            }
                        }
                    }
                    break;
            }
        }

        public void PatchFile(string originalFile, string patchedFile, byte[] patchFind, byte[] patchReplace)
        {
            // Read file bytes.
            byte[] fileContent = File.ReadAllBytes(originalFile);

            int patchCount = 0;
            // Detect and patch file.
            for (int p = 0; p < fileContent.Length; p++)
            {
                if (p + patchFind.Length > fileContent.Length)
                    continue;
                var toContinue = false;
                for (int i = 0; i < patchFind.Length; i++)
                {
                    if (patchFind[i] != fileContent[p + i])
                    {
                        toContinue = true;
                        break;
                    }
                }
                if (toContinue) continue;

                patchCount++;
                if (patchCount > 1)
                {
                    Log.WriteLine($"PatchFind pattern is not unique in {originalFile}");
                }
                else
                {
                    for (int w = 0; w < patchReplace.Length; w++)
                    {
                        fileContent[p + w] = patchReplace[w];
                    }
                }
            }

            if (patchCount == 0)
            {
                Log.WriteLine("PatchFind pattern was not found in " + originalFile);
            }

            // Save it to another location.
            File.WriteAllBytes(patchedFile, fileContent);
        }

        //XPath syntax
        //https://www.w3schools.com/xml/xpath_syntax.asp
        public void ChangeXmlAttributeValue(string path, string xpath, string attributeName, string attributeValue)
        {
            path = Environment.ExpandEnvironmentVariables(path);

            var doc = new XmlDocument();
            doc.Load(path);
            var nodes = doc.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                node.Attributes[attributeName].Value = attributeValue;
            }
            doc.Save(path);
        }

        public void ChangeXmlNodeValue(string path, string xpath, string nodeValue)
        {
            path = Environment.ExpandEnvironmentVariables(path);

            var doc = new XmlDocument();
            doc.Load(path);
            var nodes = doc.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                node.Value = nodeValue;
            }
            doc.Save(path);
        }
    }
}
