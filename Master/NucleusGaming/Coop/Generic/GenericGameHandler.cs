using Nucleus.Gaming.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowScrape.Types;

namespace Nucleus.Gaming
{
    public class GenericGameHandler : IGameHandler
    {
        private UserGameInfo userGame;
        private GameProfile profile;
        private GenericGameInfo gen;
        private Dictionary<string, string> data;

        private int timer;
        private int exited;
        int gamePadId;

        private List<Process> attached = new List<Process>();

        protected bool hasEnded;
        protected int timerInterval = 1000;

        public event Action Ended;

        public virtual bool HasEnded
        {
            get { return hasEnded; }
        }

        public int TimerInterval
        {
            get { return gen.Interval; }
        }
        
        //Gets the XInput file for each of the players
        private Dictionary<string, byte[]> GetXInputFiles(GenericContext context)
        {
            string playerXInputDir = GameManager.Instance.GetXInputFolder(gen);
            playerXInputDir = Path.Combine(playerXInputDir, context.PlayerID.ToString());
            Directory.CreateDirectory(playerXInputDir);

            DirectoryInfo di = new DirectoryInfo(playerXInputDir);

            Dictionary<string, byte[]> xinputFiles = new Dictionary<string, byte[]>();

            //This if statemet will attempt to grab prexisting xinput files
            if (gen.XInputFiles != null)
            {
                //Loop through all the xinput files labeled in the js config file
                foreach (string xinputFileIdentifier in gen.XInputFiles)
                {
                    byte[] xinputFile;
                    FileInfo[] xinputFileInfos = di.GetFiles(xinputFileIdentifier);

                    //If no files matching description found, then look for a resource with the same name
                    if (xinputFileInfos.Length == 0)
                    {
                        int dotIndex = xinputFileIdentifier.LastIndexOf(".");
                        if(dotIndex != -1)
                        {
                            //Find and replace last dot with underscore
                            string xinputResourceName = xinputFileIdentifier.Remove(dotIndex, 1).Insert(dotIndex, "_");
                            xinputFile = Properties.Resources.ResourceManager.GetObject(xinputResourceName) as byte[];
                            if(xinputFile != null)
                            {
                                xinputFiles.Add(xinputFileIdentifier, xinputFile);

                                //Store the file in the player's xinput directory
                                using (Stream str = File.OpenWrite(Path.Combine(playerXInputDir, xinputFileIdentifier)))
                                {
                                    str.Write(xinputFile, 0, xinputFile.Length);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (FileInfo xinputFileInfo in xinputFileInfos)
                        {
                            xinputFile = File.ReadAllBytes(xinputFileInfo.FullName);
                            //If no xinputFile is found, then it will be null
                            xinputFiles.Add(xinputFileInfo.Name, xinputFile);
                        }
                    }
                }
            }
            //Create default files
            else
            {
                xinputFiles.Add("xinput1_3.dll", Properties.Resources.xinput1_3_dll);
            }

             return xinputFiles;
        }

        public void End()
        {
            if (hidetaskbar)
            {
                User32.ShowTaskBar();
            }

            hasEnded = true;
            GameManager.Instance.ExecuteBackup(this.userGame.Game);

            string backupDir = GameManager.Instance.GetBackupFolder(this.userGame.Game);
            
            if (Ended != null)
            {
                Ended();
            }
        }

        public string GetFolder(Folder folder)
        {
            string str = folder.ToString();
            string output;
            if (data.TryGetValue(str, out output))
            {
                return output;
            }
            return "";
        }

        public bool Initialize(UserGameInfo game, GameProfile profile)
        {
            this.userGame = game;
            this.profile = profile;

            // see if we have any save game to backup
            gen = game.Game as GenericGameInfo;
            if (gen == null)
            {
                // you fucked up
                return false;
            }

            data = new Dictionary<string, string>();
            data.Add(Folder.Documents.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            data.Add(Folder.GameFolder.ToString(), Path.GetDirectoryName(game.ExePath));

            return true;
        }

        public string ReplaceCaseInsensitive(string str, string toFind, string toReplace)
        {
            string lowerOriginal = str.ToLower();
            string lowerFind = toFind.ToLower();
            string lowerRep = toReplace.ToLower();

            int start = lowerOriginal.IndexOf(lowerFind);
            if (start == -1)
            {
                return str;
            }

            string end = str.Remove(start, toFind.Length);
            end = end.Insert(start, toReplace);

            return end;
        }

        private static string GetRootFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            int failsafe = 20;
            for (;;)
            {
                failsafe--;
                if (failsafe < 0)
                {
                    break;
                }

                string temp = Path.GetDirectoryName(path);
                if (String.IsNullOrEmpty(temp))
                {
                    break;
                }
                path = temp;
            }
            return path;
        }

        private bool hidetaskbar;

        private void CreateLinkDirectory(string linkDirectory, string binFolder, string rootFolder, GenericContext context)
        {
            Directory.CreateDirectory(linkDirectory);

            int exitCode;
            string root = GetRootFolder(binFolder);
            //TODO: NEEDS LinkFiles HERE, AS WELL
            CmdUtil.LinkDirectories(rootFolder, linkDirectory, out exitCode, root.ToLower());

            //This is where the bin folder will go in the Instance<n> folder
            string linkExeDir = Path.Combine(linkDirectory, gen.ExecutablePath);
            if (!string.IsNullOrEmpty(gen.ExecutablePath))
            {
                // this needs fixing, if there are several folder to the exe and they have important files inside, this won't work! TODO
                Directory.CreateDirectory(linkExeDir);
                CmdUtil.LinkDirectories(binFolder, linkExeDir, out exitCode);
            }


            if (context.SymlinkExe)
            {
                CmdUtil.LinkFiles(binFolder, linkExeDir, out exitCode, "xinput", "ncoop");
            }
            else
            {
                CmdUtil.LinkFiles(binFolder, linkExeDir, out exitCode, "xinput", "ncoop", Path.GetFileNameWithoutExtension(gen.ExecutableName.ToLower()));
                string exePath = Path.Combine(linkExeDir, this.userGame.Game.ExecutableName);
                File.Copy(userGame.ExePath, exePath, true);
            }

            // some games have save files inside their game folder, so we need to access them inside the loop
            this.data[Folder.GameFolder.ToString()] = linkDirectory;


            string saveFile = context.SavePath;
            switch (context.SaveType)
            {
                case SaveType.INI:
                    IniFile file = new IniFile(saveFile);
                    foreach (SaveInfo save in context.ModifySave)
                    {
                        if (save is IniSaveInfo)
                        {
                            IniSaveInfo ini = (IniSaveInfo)save;
                            file.IniWriteValue(ini.Section, ini.Key, ini.Value);
                        }
                    }
                    break;
                case SaveType.CFG:
                    //TODO: THERE NEEDS TO BE A SEPARATE FILE FOR EACH INSTANCE, TO SUPPORT DIFFERENT RESOLUTIONS
                    SourceCfgFile cfg;
                    using (Stream str = File.OpenRead(saveFile))
                    {
                        cfg = new SourceCfgFile(str);
                    }

                    foreach (var save in context.ModifySave)
                    {
                        CfgSaveInfo info = save as CfgSaveInfo;
                        if (info == null) continue;
                        CfgSaveInfo option = info;
                        cfg.ChangeProperty(option.Key, option.Value);
                    }


                    using (Stream str = File.OpenWrite(saveFile))
                    {
                        cfg.Write(str);
                    }
                    break;
            }

            string startArgs = context.StartArguments;

            if (context.CustomXinput)
            {
                string linkXinputDir = Path.Combine(linkDirectory, gen.XInputFolder);
                if (!string.IsNullOrEmpty(gen.XInputFolder))
                {
                    //Deleting the symlink directory, if it exists
                    Directory.Delete(linkXinputDir);
                    Directory.CreateDirectory(linkXinputDir);
                    string xinputDir = Path.Combine(rootFolder, gen.XInputFolder);
                    CmdUtil.LinkDirectories(xinputDir, linkXinputDir, out exitCode);
                    CmdUtil.LinkFiles(xinputDir, linkXinputDir, out exitCode, "xinput", "ncoop");
                }

                foreach (KeyValuePair<string, byte[]> xdata in GetXInputFiles(context))
                {
                    //TODO:NEEDS TO USE A DIFFERENT .dll FOR 32 VS 64 BIT GAMES
                    using (Stream str = File.OpenWrite(Path.Combine(linkXinputDir, xdata.Key)))
                    {
                        str.Write(xdata.Value, 0, xdata.Value.Length);
                    }
                }

                string ncoopIni = Path.Combine(linkXinputDir, "ncoop.ini");
                using (Stream str = File.OpenWrite(ncoopIni))
                {
                    byte[] ini = Properties.Resources.ncoop;
                    str.Write(ini, 0, ini.Length);
                }

                IniFile x360 = new IniFile(ncoopIni);
                x360.IniWriteValue("Options", "HookNeeded", context.HookNeeded.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "GameWindowName", context.HookGameWindowName.ToString(CultureInfo.InvariantCulture));

                if (context.IsKeyboardPlayer)
                {
                    x360.IniWriteValue("Options", "PlayerOverride", "3");
                }
                else
                {
                    x360.IniWriteValue("Options", "PlayerOverride", gamePadId.ToString(CultureInfo.InvariantCulture));
                    gamePadId++;
                }
            }
        }

        private string CreateSteamEmuDirectory(string emuDir, string linkExe, GenericContext context)
        {
            string steamEmu = GameManager.Instance.ExtractSteamEmu(emuDir);
            if (string.IsNullOrEmpty(steamEmu))
            {
                return "";
            }
            
            IniFile emu = new IniFile(Path.Combine(steamEmu, "SmartSteamEmu.ini"));

            emu.IniWriteValue("Launcher", "Target", linkExe);
            emu.IniWriteValue("Launcher", "StartIn", Path.GetDirectoryName(linkExe));
            emu.IniWriteValue("Launcher", "CommandLine", context.StartArguments);
            emu.IniWriteValue("Launcher", "SteamClientPath", Path.Combine(steamEmu, "SmartSteamEmu.dll"));
            emu.IniWriteValue("Launcher", "SteamClientPath64", Path.Combine(steamEmu, "SmartSteamEmu64.dll"));
            emu.IniWriteValue("Launcher", "InjectDll", "0");
            emu.IniWriteValue("SmartSteamEmu", "AppId", context.SteamID);
            emu.IniWriteValue("SmartSteamEmu", "SteamIdGeneration", "Manual");

            string userName = $"Player{ context.PlayerID }";

            emu.IniWriteValue("SmartSteamEmu", "PersonaName", userName);
            emu.IniWriteValue("SmartSteamEmu", "ManualSteamId", "7656119796028793" + context.PlayerID);

            emu.IniWriteValue("SmartSteamEmu", "Offline", "0");
            emu.IniWriteValue("SmartSteamEmu", "MasterServer", "");
            emu.IniWriteValue("SmartSteamEmu", "MasterServerGoldSrc", "");

            return Path.Combine(steamEmu, "SmartSteamLoader.exe");
        }



        public string Play()
        {
            List<PlayerInfo> players = profile.PlayerData;

            Screen[] all = Screen.AllScreens;
            gamePadId = 0;


            string backupDir = GameManager.Instance.GetBackupFolder(this.userGame.Game);
            string binFolder = Path.GetDirectoryName(userGame.ExePath);
            string rootFolder = Path.Combine(binFolder, gen.RootGameFolderPath);
            bool first = true;
            hidetaskbar = false;

            bool keyboard = false;
            
            ProcessUtil.KillProcessesByName(gen.ExecutableName);

            if (gen.SupportsKeyboard)
            {
                // make sure the keyboard player is the last to be started,
                // so it will get the focus by default
                KeyboardPlayer player = (KeyboardPlayer)profile.Options["KeyboardPlayer"];
                if (player.Value != -1)
                {
                    keyboard = true;
                    List<PlayerInfo> newPlayers = new List<PlayerInfo>();

                    for (int i = 0; i < players.Count; i++)
                    {
                        PlayerInfo p = players[i];
                        if (i == player.Value)
                        {
                            continue;
                        }

                        newPlayers.Add(p);
                    }
                    newPlayers.Add(players[player.Value]);
                    players = newPlayers;
                }
            }

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];
                ProcessData procData = player.ProcessData;
                bool hasSetted = procData != null && procData.Setted;

                if (i > 0 && (gen.KillMutex?.Length > 0 || !hasSetted))
                {
                    PlayerInfo before = players[i - 1];
                    while (gen.KillMutex?.Length > 0)
                    {
                        Thread.Sleep(1000);
                        if (!before.ProcessData.KilledMutexes)
                        {
                            ProcessData pdata = before.ProcessData;
                            StartGameUtil.KillMutexes(pdata.Process, gen.KillMutex);
                            break;
                        }
                    }
                }

                Rectangle playerBounds = player.MonitorBounds;

                // find the monitor that has this screen
                var owner = all.FirstOrDefault(s => s.Bounds.Contains(playerBounds));
                bool isFullscreen = false;

                if (owner != null)
                {
                    Rectangle ob = owner.Bounds;
                    if (playerBounds.X == ob.X &&
                        playerBounds.Y == ob.Y &&
                        playerBounds.Width == ob.Width &&
                        playerBounds.Height == ob.Height)
                    {
                        isFullscreen = true;
                    }
                }

                GenericContext context = gen.CreateContext(profile, player, this);
                context.PlayerID = i;
                context.IsFullscreen = isFullscreen;
                context.IsKeyboardPlayer = keyboard && i == players.Count - 1;
                gen.PrePlay(context);

                player.IsKeyboardPlayer = context.IsKeyboardPlayer;

                string saveFile = context.SavePath;
                if (gen.SaveType != SaveType.None && first)
                {
                    GameManager.Instance.BeginBackup(gen);
                    GameManager.Instance.BackupFile(gen, saveFile);
                }

                string linkDirectory = Path.Combine(backupDir, "Instance" + i);
                if(!Directory.Exists(linkDirectory)){
                    CreateLinkDirectory(linkDirectory, binFolder, rootFolder, context);
                }

                Process proc;
                string startArgs = context.StartArguments;
                string linkExe = Path.Combine(linkDirectory, gen.ExecutableName);
                if (context.NeedsSteamEmulation)
                {
                    string emuDir = Path.Combine(linkDirectory, "SmartSteamLoader");
                    string emuExe;
                    if (Directory.Exists(emuDir))
                    {
                        emuExe = Path.Combine(emuDir, "SmartSteamLoader");
                    }
                    else
                    {
                        emuExe = CreateSteamEmuDirectory(emuDir, linkExe, context);
                        if (string.IsNullOrEmpty(emuExe))
                        {
                            return "Extraction of SmartSteamEmu failed!";
                        }
                    }

                    if (context.KillMutex?.Length > 0)
                    {
                        // to kill the mutexes we need to orphanize the process
                        proc = ProcessUtil.RunOrphanProcess(emuExe);
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo() {FileName = emuExe};
                        proc = Process.Start(startInfo);
                    }

                    player.SteamEmu = true;
                }
                else
                {
                    if (context.KillMutex?.Length > 0)
                    {
                        proc = Process.GetProcessById(StartGameUtil.StartGame(linkExe, startArgs));
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = linkExe;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = startArgs;
                        startInfo.UseShellExecute = true;
                        startInfo.WorkingDirectory = Path.GetDirectoryName(linkExe);
                        proc = Process.Start(startInfo);
                    }

                    if (proc == null)
                    {
                        for (int times = 0; times < 200; times++)
                        {
                            Thread.Sleep(50);

                            Process[] procs = Process.GetProcesses();
                            string proceName = Path.GetFileNameWithoutExtension(context.ExecutableName).ToLower();
                            string launcherName = Path.GetFileNameWithoutExtension(context.LauncherExe).ToLower();

                            for (int j = 0; j < procs.Length; j++)
                            {
                                Process p = procs[j];
                                string lowerP = p.ProcessName.ToLower();
                                if (((lowerP == proceName) || lowerP == launcherName) &&
                                    !attached.Contains(p))
                                {
                                    attached.Add(p);
                                    proc = p;
                                    break;
                                }
                            }

                            if (proc != null)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        attached.Add(proc);
                    }
                }

                ProcessData data = new ProcessData(proc);

                data.Position = new Point(playerBounds.X, playerBounds.Y);
                data.Size = new Size(playerBounds.Width, playerBounds.Height);
                data.KilledMutexes = context.KillMutex?.Length == 0;
                player.ProcessData = data;

                if (first)
                {
                    if (context.HideTaskbar)
                    {
                        hidetaskbar = true;
                    }
                }

                first = false;
            }

            //if (hidetaskbar)
            //{
            //    User32.HideTaskbar();
            //}

            return string.Empty;
        }

        public void Update(int delayMS)
        {
            if (profile == null)
            {
                return;
            }

            exited = 0;
            List<PlayerInfo> players = profile.PlayerData;
            timer += delayMS;

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo p = players[i];
                ProcessData data = p.ProcessData;
                if (data == null)
                {
                    continue;
                }

                if (p.SteamEmu)
                {
                    List<int> children = ProcessUtil.GetChildrenProcesses(data.Process);

                    // catch the game process, that was spawned from Smart Steam Emu
                    if (children.Count > 0)
                    {
                        for (int j = 0; j < children.Count; j++)
                        {
                            int id = children[j];
                            Process child = Process.GetProcessById(id);
                            try
                            {
                                if (child.ProcessName.Contains("conhost"))
                                {
                                    continue;
                                }
                            }
                            catch
                            {
                                continue;
                            }

                            data.AssignProcess(child);
                            p.SteamEmu = child.ProcessName.Contains("SmartSteamLoader") || child.ProcessName.Contains("cmd");
                        }
                    }
                }
                else
                {
                    if (data.Setted)
                    {
                        if (data.Process.HasExited)
                        {
                            exited++;
                            continue;
                        }

                        uint lStyle = User32Interop.GetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_STYLE);
                        if (lStyle != data.RegLong)
                        {
                            uint toRemove = User32_WS.WS_CAPTION;
                            lStyle = lStyle & (~toRemove);

                            User32Interop.SetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_STYLE, lStyle);
                            data.RegLong = lStyle;
                            data.HWnd.Location = data.Position;
                        }

                        data.HWnd.TopMost = true;

                        if (p.IsKeyboardPlayer)
                        {
                            Rectangle r = p.MonitorBounds;
                            Cursor.Clip = r;
                            User32Interop.SetForegroundWindow(data.HWnd.NativePtr);
                        }
                    }
                    else
                    {
                        data.Process.Refresh();

                        if (data.Process.HasExited)
                        {
                            if (p.GotLauncher)
                            {
                                if (p.GotGame)
                                {
                                    exited++;
                                }
                                else
                                {
                                    List<int> children = ProcessUtil.GetChildrenProcesses(data.Process);
                                    if (children.Count > 0)
                                    {
                                        for (int j = 0; j < children.Count; j++)
                                        {
                                            int id = children[j];
                                            Process pro = Process.GetProcessById(id);

                                            if (!attached.Contains(pro))
                                            {
                                                attached.Add(pro);
                                                data.HWnd = null;
                                                p.GotGame = true;
                                                data.AssignProcess(pro);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Steam showing a launcher, need to find our game process
                                string launcher = gen.LauncherExe;
                                if (launcher.ToLower().EndsWith(".exe"))
                                {
                                    launcher = launcher.Remove(launcher.Length - 4, 4);
                                }

                                Process[] procs = Process.GetProcessesByName(launcher);
                                for (int j = 0; j < procs.Length; j++)
                                {
                                    Process pro = procs[j];
                                    if (!attached.Contains(pro))
                                    {
                                        attached.Add(pro);
                                        data.AssignProcess(pro);
                                        p.GotLauncher = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (data.HWNDRetry || data.HWnd == null || data.HWnd.NativePtr != data.Process.MainWindowHandle)
                            {
                                data.HWnd = new HwndObject(data.Process.MainWindowHandle);
                                Point pos = data.HWnd.Location;

                                if (String.IsNullOrEmpty(data.HWnd.Title) ||
                                    pos.X == -32000 ||
                                    data.HWnd.Title.ToLower() == gen.LauncherTitle.ToLower())
                                {
                                    data.HWNDRetry = true;
                                }
                                else
                                {
                                    Size s = data.Size;
                                    data.Setted = true;
                                    data.HWnd.TopMost = true;
                                    data.HWnd.Size = data.Size;
                                    data.HWnd.Location = data.Position;
                                }
                            }
                        }
                    }
                }
            }

            if (exited == players.Count)
            {
                if (!hasEnded)
                {
                    End();
                }
            }
        }
    }
}
