using Ionic.Zip;
using Newtonsoft.Json;
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
    [AppDomainShared]
    public class HandlerContext
    {
        private GameProfile profile;

        public PlayerInfo PlayerInfo;

        [Dynamic(AutoHandles = true)]
        public string InstallFolder;
        [Dynamic(AutoHandles = true)]
        public string InstanceFolder;
        [Dynamic(AutoHandles = true)]
        public string InstancedExePath;
        [Dynamic(AutoHandles = true)]
        public string InstancedWorkingPath;

        [Dynamic(AutoHandles = true)]
        public string PackageFolder;



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
        public bool IsFullscreen;
        public UserInfo User = new UserInfo();
        public DPIHandling DPIHandling = DPIHandling.True;
        public Dictionary<string, string> AdditionalData;
        public int PlayerID;

        public bool bHasKeyboardPlayer;
        public string OverrideStartProcess { get; set; }
       

        public Dictionary<string, object> Options { get; set; }

        [JsonIgnore]
        public int Width
        {
            get
            {
                switch (DPIHandling)
                {
                    case DPIHandling.Scaled:
                        return (int)((PlayerInfo.MonitorBounds.Width * DPIManager.Scale) + 0.5);
                    case DPIHandling.InvScaled:
                        return (int)((PlayerInfo.MonitorBounds.Width * (1 / DPIManager.Scale)) + 0.5);
                    case DPIHandling.True:
                    default:
                        return PlayerInfo.MonitorBounds.Width;
                }
            }
        }

        [JsonIgnore]
        public int Height
        {
            get
            {
                switch (DPIHandling)
                {
                    case DPIHandling.Scaled:
                        return (int)((PlayerInfo.MonitorBounds.Height * DPIManager.Scale) + 0.5);
                    case DPIHandling.InvScaled:
                        return (int)((PlayerInfo.MonitorBounds.Height * (1 / DPIManager.Scale)) + 0.5);
                    case DPIHandling.True:
                    default:
                        return PlayerInfo.MonitorBounds.Height;
                }
            }
        }

        [JsonConstructor]
        private HandlerContext()
        {
        }

        public HandlerContext(GameProfile prof, PlayerInfo info, bool hasKeyboard)
        {
            profile = prof;
            PlayerInfo = info;

            Options = prof.Options;
            bHasKeyboardPlayer = hasKeyboard;
        }

        public bool HasKeyboardPlayer() {
            return bHasKeyboardPlayer;
        }

        public void SetProcessToStart(string procName)
        {
            OverrideStartProcess = procName;
        }



        public string CombinePath(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1) ||
                string.IsNullOrEmpty(path2))
            {
                System.Diagnostics.Debugger.Break();
                throw new NotImplementedException();
            }
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

        public SaveInfo NewSaveInfo(string section, string key, string value)
        {
            return new SaveInfo(section, key, value);
        }

        public SaveInfo NewScrSaveInfo(string key, params string[] values)
        {
            SaveInfo info = new SaveInfo();
            info.Add("Key", key);
            info.Add("Parameters", values.Length.ToString());
            for (int i = 0; i < values.Length; i++)
            {
                string paramKey = "Param" + (i + 1);
                info.Add(paramKey, values[i]);
            }

            return info;
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
                            cfg.ChangeProperty(save);
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
                            file.IniWriteValue(save["Section"], save["Key"], save["Value"]);
                        }
                    }
                    break;
                case SaveType.SCR:
                    {
                        if (!installSavePath.Equals(saveFullPath))
                        {
                            File.Copy(installSavePath, saveFullPath);
                        }
                        ScrConfigFile file = new ScrConfigFile(saveFullPath);
                        for (int j = 0; j < info.Length; j++)
                        {
                            SaveInfo save = info[j];
                            file.ChangeProperty(save);
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Extracts the SmartSteamEmu and returns the folder its on
        /// </summary>
        /// <returns></returns>
        public bool ExtractZip(string zipPath, string outputFolder)
        {
            try
            {
                Directory.CreateDirectory(outputFolder);

                string path = Path.Combine(PackageFolder, "assets", zipPath);

                using (FileStream stream = File.OpenRead(path))
                {
                    using (ZipFile zip1 = ZipFile.Read(stream))
                    {
                        foreach (ZipEntry e in zip1)
                        {
                            e.Extract(outputFolder, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void LogLine(string str) {
            Log.WriteLine(str);
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
