using Nucleus.Coop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    // This class is a holder for the GenericGameInfo class. It doesn't implement the IGenericGameInfo
    // because some of the elements are implemented differently to work with the JS engine
    // Comments can be found on the original class if no specific feature is implemented here
    public class GenericContext
    {
        public XInputInfo XInput = new XInputInfo();
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
        public SaveInfo[] ModifySave;
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
            get { return pInfo.MonitorBounds.Width; }
        }
        public int Height
        {
            get { return pInfo.MonitorBounds.Height; }
        }

        [Dynamic(AutoHandles = true)]
        public string ExePath;
        [Dynamic(AutoHandles = true)]
        public string RootInstallFolder;
        [Dynamic(AutoHandles = true)]
        public string RootFolder;

        private GameProfile profile;
        private PlayerInfo pInfo;
        private GenericGameHandler parent;
        public GenericContext(GameProfile prof, PlayerInfo info, GenericGameHandler handler)
        {
            this.profile = prof;
            this.pInfo = info;
            this.parent = handler;
        }

        public string GetFolder(Folder folder)
        {
            return parent.GetFolder(folder);
        }

        public void WriteTextFile(string path, string[] lines)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllLines(path, lines);
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
                                cfg.ChangeProperty(option.Key, option.Value);
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
    }
}
