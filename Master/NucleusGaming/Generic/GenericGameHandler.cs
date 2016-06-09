using Jint;
using Nucleus.Gaming.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowScrape.Types;

namespace Nucleus.Gaming
{
    public class GenericGameHandler : IGameHandler
    {
        protected bool hasEnded;
        protected int timerInterval = 1000;

        public virtual bool HasEnded
        {
            get { return hasEnded; }
        }

        public int TimerInterval
        {
            get { return timerInterval; }
        }

        public event Action Ended;

        public void End()
        {
            hasEnded = true;
        }

        private UserGameInfo userGame;
        private GameProfile profile;
        private Engine engine;
        private GenericGameInfo gen;
        private Dictionary<string, string> data;

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

            engine = new Engine();
            engine.SetValue("Options", profile.Options);

            data = new Dictionary<string, string>();
            data.Add(NucleusFolderEnum.GameFolder.ToString(), Path.GetDirectoryName(game.ExePath));

            if (gen.SaveType == GenericGameSaveType.None)
            {
                return true;
            }

            string saveFile = ProcessPath(gen.SavePath);
            GameManager.Instance.BeginBackup(game.Game);
            GameManager.Instance.BackupFile(game.Game, saveFile);

            return true;
        }

        public string ProcessPath(string str)
        {
            string s = str;

            for (int index = s.IndexOf("&"); index != -1; index = s.IndexOf("&"))
            {
                int end = s.IndexOf("&", index + 1);
                if (end == -1)
                {
                    // !! error !!
                    return string.Empty;
                }

                string toQuery = s.Substring(index + 1, end - index - 1);

                NucleusFolderEnum folder;
                string toReplace = "";
                if (Enum.TryParse<NucleusFolderEnum>(toQuery, true, out folder))
                {
                    toReplace = data[folder.ToString()];
                }
                else
                {
                    Environment.SpecialFolder special;
                    if (Enum.TryParse(toQuery, true, out special))
                    {
                        toReplace = Environment.GetFolderPath(special);
                    }
                }

                s = s.Remove(index, end - index + 1);
                s = s.Insert(index, toReplace);
            }

            return s;
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

        static string GetRootFolder(string path)
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

        public string Play()
        {
            List<PlayerInfo> players = profile.PlayerData;

            Screen[] all = Screen.AllScreens;

            string backupDir = GameManager.Instance.GetBackupFolder(this.userGame.Game);
            string binFolder = Path.GetDirectoryName(userGame.ExePath);
            string rootFolder = ReplaceCaseInsensitive(binFolder, gen.BinariesFolder, "");

            int gamePadId = 0;

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];
                if (i > 0 && gen.KillMutex.Length > 0)
                {
                    PlayerInfo before = players[i - 1];
                    for (;;)
                    {
                        if (exited > 0)
                        {
                            return "";
                        }
                        Thread.Sleep(100);
                        if (before.screenData.KilledMutexes)
                        {
                            break;
                        }
                    }
                }

                Rectangle playerBounds = player.monitorBounds;

                // find the monitor that has this screen
                Screen owner = null;
                for (int j = 0; j < all.Length; j++)
                {
                    Screen s = all[j];
                    if (s.Bounds.Contains(playerBounds))
                    {
                        owner = s;
                        break;
                    }
                }

                int width = playerBounds.Width;
                int height = playerBounds.Height;
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

                engine.SetValue("Id", i.ToString(CultureInfo.InvariantCulture));
                engine.SetValue("Width", width.ToString(CultureInfo.InvariantCulture));
                engine.SetValue("Height", height.ToString(CultureInfo.InvariantCulture));
                engine.SetValue("IsFullscreen", isFullscreen);

                // symlink the game folder
                // find out the folder that contains the game executable
                string root = GetRootFolder(gen.BinariesFolder);

                string linkFolder = Path.Combine(backupDir, "Instance" + i);
                Directory.CreateDirectory(linkFolder);
                int exitCode;
                CmdUtil.LinkDirectories(rootFolder, linkFolder, out exitCode, root.ToLower());

                string linkBin = Path.Combine(linkFolder, gen.BinariesFolder);

                if (!string.IsNullOrEmpty(gen.BinariesFolder))
                {
                    // this needs fixing, if there are several folder to the exe and they have important files inside, this won't work! TODO
                    Directory.CreateDirectory(linkBin);
                    CmdUtil.LinkDirectories(binFolder, linkBin, out exitCode);
                }

                CmdUtil.LinkFiles(binFolder, linkBin, out exitCode, "xinput", Path.GetFileNameWithoutExtension(gen.ExecutableName.ToLower()));
                string exePath = Path.Combine(linkBin, this.userGame.Game.ExecutableName);
                File.Copy(userGame.ExePath, exePath, true);


                // some games have save files inside their game folder, so we need to access them inside the loop
                this.data[NucleusFolderEnum.GameFolder.ToString()] = linkFolder;

                string saveFile = ProcessPath(gen.SavePath);
                IniFile file = new IniFile(saveFile);

                switch (gen.SaveType)
                {
                    case GenericGameSaveType.INI:
                        foreach (var modPair in gen.ModifySave)
                        {
                            string key = modPair.Key;
                            string[] keys = key.Split('/');

                            engine.Execute(modPair.Value);
                            string val = engine.GetCompletionValue().ToString();

                            file.IniWriteValue(keys[0], keys[1], val);
                        }
                        break;
                }

                string startArgs = gen.StartArguments;

                byte[] xdata = null;

                if (gen.SupportsKeyboard && i == (int)gen.Options["KeyboardPlayer"].Value)
                {
                    engine.SetValue("Keyboard", true);

                    // need to make an xinput that answers to no gamepad?
                    xdata = Properties.Resources.xinput4;
                }
                else
                {
                    engine.SetValue("Keyboard", false);

                    switch (gamePadId)
                    {
                        case 0:
                            xdata = Properties.Resources.xinput1;
                            break;
                        case 1:
                            xdata = Properties.Resources.xinput2;
                            break;
                        case 2:
                            xdata = Properties.Resources.xinput3;
                            break;
                        case 3:
                            xdata = Properties.Resources.xinput4;
                            break;
                        default:
                            xdata = Properties.Resources.xinput4;
                            break;
                    }
                    gamePadId++;
                }

                using (Stream str = File.OpenWrite(Path.Combine(linkBin, "xinput1_3.dll")))
                {
                    str.Write(xdata, 0, xdata.Length);
                }

                if (!string.IsNullOrEmpty(startArgs))
                {
                    startArgs = engine.Execute(startArgs).GetCompletionValue().AsString();
                }

                if (gen.NeedsSteamEmulation)
                {
                    string steamEmu = GameManager.Instance.ExtractSteamEmu();
                    string emuExe = Path.Combine(steamEmu, "SmartSteamLoader.exe");

                    string emuIni = Path.Combine(steamEmu, "SmartSteamEmu.ini");
                    IniFile emu = new IniFile(emuIni);

                    emu.IniWriteValue("Launcher", "Target", exePath);
                    emu.IniWriteValue("Launcher", "StartIn", Path.GetDirectoryName(exePath));
                    emu.IniWriteValue("Launcher", "CommandLine", startArgs);
                    emu.IniWriteValue("Launcher", "SteamClientPath", Path.Combine(steamEmu, "SmartSteamEmu.dll"));
                    emu.IniWriteValue("Launcher", "SteamClientPath64", Path.Combine(steamEmu, "SmartSteamEmu64.dll"));
                    emu.IniWriteValue("Launcher", "InjectDll", "0");
                    emu.IniWriteValue("SmartSteamEmu", "AppId", gen.SteamID);

                    Process proc;
                    if (gen.KillMutex.Length > 0)
                    {
                        // to kill the mutexes we need to orphanize the process
                        proc = ProcessUtil.RunOrphanProcess(emuExe);
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = emuExe;
                        proc = Process.Start(startInfo);
                    }

                    player.Process = proc;
                    player.SteamEmu = true;
                }
                else
                {
                    Process proc;
                    if (gen.KillMutex.Length > 0)
                    {
                        proc = Process.GetProcessById(StartGameUtil.StartGame(exePath, startArgs));
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = exePath;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = startArgs;
                        startInfo.UseShellExecute = true;
                        startInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
                        proc = Process.Start(startInfo);
                    }
                    player.Process = proc;
                }

                ScreenData data = new ScreenData();
                data.Position = new Point(playerBounds.X, playerBounds.Y);
                data.Size = new Size(playerBounds.Width, playerBounds.Height);
                data.KilledMutexes = gen.KillMutex.Length == 0;
                player.screenData = data;
            }

            return string.Empty;
        }

        private int timer;
        private int exited;
        private List<Process> attached = new List<Process>();

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
                if (p.screenData == null || p.Process == null)
                {
                    continue;
                }

                if (p.SteamEmu)
                {
                    List<int> children = ProcessUtil.GetChildrenProcesses(p.Process);
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

                            p.Process = child;
                            p.SteamEmu = child.ProcessName.Contains("SmartSteamLoader") || child.ProcessName.Contains("cmd");
                        }
                    }
                }
                else
                {
                    ScreenData data = p.screenData;

                    if (data.Set)
                    {
                        if (p.Process.HasExited)
                        {
                            exited++;
                            continue;
                        }

                        if (!p.screenData.KilledMutexes && gen.KillMutex.Length > 0)
                        {
                            StartGameUtil.KillMutex(p.Process, gen.KillMutex);
                            p.screenData.KilledMutexes = true;
                        }

                        uint lStyle = User32Interop.GetWindowLong(data.HWND.Hwnd, User32_WS.GWL_STYLE);
                        if (lStyle != data.RegLong)
                        {
                            uint toRemove = User32_WS.WS_CAPTION;
                            lStyle = lStyle & (~toRemove);

                            User32Interop.SetWindowLong(data.HWND.Hwnd, User32_WS.GWL_STYLE, lStyle);
                            data.RegLong = lStyle;
                            data.HWND.Location = data.Position;
                        }
                    }
                    else
                    {
                        p.Process.Refresh();

                        if (p.Process.HasExited)
                        {
                            if (p.GotLauncher)
                            {
                                if (p.GotGame)
                                {
                                    exited++;
                                }
                                else
                                {
                                    List<int> children = ProcessUtil.GetChildrenProcesses(p.Process);
                                    if (children.Count > 0)
                                    {
                                        for (int j = 0; j < children.Count; j++)
                                        {
                                            int id = children[j];
                                            Process pro = Process.GetProcessById(id);

                                            if (!attached.Contains(pro))
                                            {
                                                attached.Add(pro);
                                                data.HWND = null;
                                                p.GotGame = true;
                                                p.Process = pro;
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
                                        p.Process = pro;
                                        p.GotLauncher = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (data.HWNDRetry || data.HWND == null || data.HWND.Hwnd != p.Process.MainWindowHandle)
                            {
                                data.HWND = new HwndObject(p.Process.MainWindowHandle);
                                Point pos = data.HWND.Location;

                                if (String.IsNullOrEmpty(data.HWND.Title) || pos.X == -32000 || data.HWND.Title.ToLower() == gen.LauncherTitle.ToLower())
                                {
                                    data.HWNDRetry = true;
                                }
                                else
                                {
                                    Size s = data.Size;
                                    data.Set = true;
                                    data.HWND.TopMost = true;
                                    data.HWND.Size = data.Size;
                                    data.HWND.Location = data.Position;
                                }
                            }
                        }
                    }
                }

                if (exited == players.Count)
                {
                    if (!hasEnded)
                    {
                        hasEnded = true;
                        GameManager.Instance.ExecuteBackup(this.userGame.Game);

                        if (Ended != null)
                        {
                            Ended();
                        }
                    }
                }
            }
        }
    }
}
