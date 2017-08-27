using Jint;
using Nucleus.Gaming.Interop;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using Nucleus.Interop.User32;
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
using Nucleus.Gaming.Coop.Generic.Cursor;
using WindowScrape.Constants;
using WindowScrape.Static;
using WindowScrape.Types;
using Nucleus.Gaming.Coop;
using System.Reflection;
using Nucleus.Gaming.Tools.GameStarter;

namespace Nucleus.Gaming
{
    public class GenericGameHandler : IGameHandler, ILogNode
    {
        private const float HWndInterval = 10000;

        private UserGameInfo userGame;
        private GameProfile profile;
        private GenericGameInfo gen;
        private Dictionary<string, string> jsData;

        private double timer;
        private int exited;
        private List<Process> attached = new List<Process>();

        protected bool hasEnded;
        protected double timerInterval = 1000;

        public event Action Ended;
        private CursorModule _cursorModule;

        public virtual bool HasEnded
        {
            get { return hasEnded; }
        }

        public double TimerInterval
        {
            get { return timerInterval; }
        }

        private void ForceFinish()
        {
            // search for game instances left behind
            try
            {
                Process[] procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(gen.ExecutableName.ToLower()));
                if (procs.Length > 0)
                {
                    for (int i = 0; i < procs.Length; i++)
                    {
                        procs[i].Kill();
                    }
                }
            }
            catch { }
        }

        public void End()
        {
            User32Util.ShowTaskBar();

            hasEnded = true;
            GameManager.Instance.ExecuteBackup(this.userGame.Game);

            LogManager.UnregisterForLogCallback(this);

            Cursor.Clip = Rectangle.Empty; // guarantee were not clipping anymore
            string backupDir = GameManager.Instance.GempTempFolder(this.userGame.Game);
            ForceFinish();

            if (_cursorModule != null)
                _cursorModule.Stop();

            Thread.Sleep(1000);
            // delete symlink folder
            try
            {
#if RELEASE
                for (int i = 0; i < profile.PlayerData.Count; i++)
                {
                    string linkFolder = Path.Combine(backupDir, "Instance" + i);
                    if (Directory.Exists(linkFolder))
                    {
                        Directory.Delete(linkFolder, true);
                    }
                }
#endif
            }
            catch
            {

            }

            if (Ended != null)
            {
                Ended();
            }
        }

        public string GetFolder(Folder folder)
        {
            string str = folder.ToString();
            string output;
            if (jsData.TryGetValue(str, out output))
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

            if (gen.LockMouse)
                _cursorModule = new CursorModule();

            jsData = new Dictionary<string, string>();
            jsData.Add(Folder.Documents.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            jsData.Add(Folder.MainGameFolder.ToString(), Path.GetDirectoryName(game.ExePath));
            jsData.Add(Folder.InstancedGameFolder.ToString(), Path.GetDirectoryName(game.ExePath));

            timerInterval = gen.HandlerInterval;

            LogManager.RegisterForLogCallback(this);

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

        private static string internalGetRelativePath(DirectoryInfo dirInfo, DirectoryInfo rootInfo, string str)
        {
            if (dirInfo.FullName == rootInfo.FullName || dirInfo == null)
            {
                return str;
            }

            if (!string.IsNullOrWhiteSpace(Path.GetExtension(dirInfo.Name)))
            {
                str = dirInfo.Name;
            }
            else
            {
                str = dirInfo.Name + "\\" + str;
            }

            dirInfo = dirInfo.Parent;
            return internalGetRelativePath(dirInfo, rootInfo, str);
        }

        public string GetRelativePath(string dirPath, string rootFolder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            DirectoryInfo rootInfo = new DirectoryInfo(rootFolder);
            return internalGetRelativePath(dirInfo, rootInfo, "");
        }

        public string Play()
        {
            ForceFinish();

            List<PlayerInfo> players = profile.PlayerData;
            for (int i = 0; i < players.Count; i++)
            {
                players[i].PlayerID = i;
            }

            UserScreen[] all = ScreensUtil.AllScreens();

            string nucleusRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string tempDir = GameManager.Instance.GempTempFolder(gen);
            string exeFolder = Path.GetDirectoryName(userGame.ExePath).ToLower();
            string rootFolder = exeFolder;
            string workingFolder = exeFolder;
            if (!string.IsNullOrEmpty(gen.BinariesFolder))
            {
                rootFolder = ReplaceCaseInsensitive(exeFolder, gen.BinariesFolder.ToLower(), "");
            }
            if (!string.IsNullOrEmpty(gen.WorkingFolder))
            {
                workingFolder = Path.Combine(exeFolder, gen.WorkingFolder.ToLower());
            }

            bool first = true;
            bool keyboard = false;
            //if (gen.SupportsKeyboard)
            //{
            //    // make sure the keyboard player is the last to be started,
            //    // so it will get the focus by default
            //    KeyboardPlayer player = (KeyboardPlayer)profile.Options["KeyboardPlayer"];
            //    if (player.Value != -1)
            //    {
            //        keyboard = true;
            //        List<PlayerInfo> newPlayers = new List<PlayerInfo>();

            //        for (int i = 0; i < players.Count; i++)
            //        {
            //            PlayerInfo p = players[i];
            //            if (i == player.Value)
            //            {
            //                continue;
            //            }

            //            newPlayers.Add(p);
            //        }
            //        newPlayers.Add(players[player.Value]);
            //        players = newPlayers;
            //    }
            //}

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];
                ProcessData procData = player.ProcessData;
                bool hasSetted = procData != null && procData.Setted;

                if (i > 0 && (gen.KillMutex?.Length > 0 || !hasSetted))
                {
                    PlayerInfo before = players[i - 1];
                    for (;;)
                    {
                        if (exited > 0)
                        {
                            return "";
                        }
                        Thread.Sleep(1000);

                        if (gen.KillMutex != null)
                        {
                            if (gen.KillMutex.Length > 0 && !before.ProcessData.KilledMutexes)
                            {
                                // check for the existence of the mutexes
                                // before invoking our StartGame app to kill them
                                ProcessData pdata = before.ProcessData;

                                if (StartGameUtil.MutexExists(pdata.Process, gen.KillMutex))
                                {
                                    // mutexes still exist, must kill
                                    StartGameUtil.KillMutex(pdata.Process, gen.KillMutex);
                                    pdata.KilledMutexes = true;
                                    break;
                                }
                                else
                                {
                                    // mutexes dont exist anymore
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Rectangle playerBounds = player.MonitorBounds;
                UserScreen owner = player.Owner;

                int width = playerBounds.Width;
                int height = playerBounds.Height;
                bool isFullscreen = owner.Type == UserScreenType.FullScreen;

                string exePath;
                string linkFolder;
                string linkBinFolder;

                if (gen.SymlinkGame || gen.HardcopyGame)
                {
                    List<string> dirExclusions = new List<string>();
                    List<string> fileExclusions = new List<string>();
                    List<string> fileCopies = new List<string>();

                    // symlink the game folder (and not the bin folder, if we have one)
                    linkFolder = Path.Combine(tempDir, "Instance" + i);
                    Directory.CreateDirectory(linkFolder);

                    linkBinFolder = linkFolder;
                    if (!string.IsNullOrEmpty(gen.BinariesFolder))
                    {
                        linkBinFolder = Path.Combine(linkFolder, gen.BinariesFolder);
                        dirExclusions.Add(gen.BinariesFolder);
                    }
                    exePath = Path.Combine(linkBinFolder, this.userGame.Game.ExecutableName);

                    if (!string.IsNullOrEmpty(gen.WorkingFolder))
                    {
                        linkBinFolder = Path.Combine(linkFolder, gen.WorkingFolder);
                        dirExclusions.Add(gen.WorkingFolder);
                    }

                    // some games have save files inside their game folder, so we need to access them inside the loop
                    jsData[Folder.InstancedGameFolder.ToString()] = linkFolder;

                    if (gen.Hook.CustomDllEnabled)
                    {
                        fileExclusions.Add("xinput1_3.dll");
                        fileExclusions.Add("ncoop.ini");
                    }
                    if (!gen.SymlinkExe)
                    {
                        fileCopies.Add(gen.ExecutableName.ToLower());
                    }

                    // additional ignored files by the generic info
                    if (gen.FileSymlinkExclusions != null)
                    {
                        string[] symlinkExclusions = gen.FileSymlinkExclusions;
                        for (int k = 0; k < symlinkExclusions.Length; k++)
                        {
                            string s = symlinkExclusions[k];
                            // make sure it's lower case
                            fileExclusions.Add(s.ToLower());
                        }
                    }
                    if (gen.FileSymlinkCopyInstead != null)
                    {
                        string[] fileSymlinkCopyInstead = gen.FileSymlinkCopyInstead;
                        for (int k = 0; k < fileSymlinkCopyInstead.Length; k++)
                        {
                            string s = fileSymlinkCopyInstead[k];
                            // make sure it's lower case
                            fileCopies.Add(s.ToLower());
                        }
                    }
                    if (gen.DirSymlinkExclusions != null)
                    {
                        string[] symlinkExclusions = gen.DirSymlinkExclusions;
                        for (int k = 0; k < symlinkExclusions.Length; k++)
                        {
                            string s = symlinkExclusions[k];
                            // make sure it's lower case
                            dirExclusions.Add(s.ToLower());
                        }
                    }

                    string[] fileExclusionsArr = fileExclusions.ToArray();
                    string[] fileCopiesArr = fileCopies.ToArray();

                    if (gen.HardcopyGame)
                    {
                        // copy the directory
                        //int exitCode;
                        //FileUtil.CopyDirectory(rootFolder, new DirectoryInfo(rootFolder), linkFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, true);
                    }
                    else
                    {
                        int exitCode;
                        CmdUtil.LinkDirectory(rootFolder, new DirectoryInfo(rootFolder), linkFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, fileCopiesArr, true);

                        if (!gen.SymlinkExe)
                        {
                            //File.Copy(userGame.ExePath, exePath, true);
                        }
                    }
                }
                else
                {
                    exePath = userGame.ExePath;
                    linkBinFolder = rootFolder;
                    linkFolder = workingFolder;
                }

                GenericContext context = gen.CreateContext(profile, player, this);
                context.PlayerID = player.PlayerID;
                context.IsFullscreen = isFullscreen;

                context.ExePath = exePath;
                context.RootInstallFolder = exeFolder;
                context.RootFolder = linkFolder;

                gen.PrePlay(context, this, player);

                string startArgs = context.StartArguments;

                if (context.Hook.CustomDllEnabled)
                {
                    byte[] xdata = Properties.Resources.xinput1_3;
                    if (context.Hook.XInputNames == null)
                    {
                        using (Stream str = File.OpenWrite(Path.Combine(linkBinFolder, "xinput1_3.dll")))
                        {
                            str.Write(xdata, 0, xdata.Length);
                        }
                    }
                    else
                    {
                        string[] xinputs = context.Hook.XInputNames;
                        for (int z = 0; z < xinputs.Length; z++)
                        {
                            string xinputName = xinputs[z];
                            using (Stream str = File.OpenWrite(Path.Combine(linkBinFolder, xinputName)))
                            {
                                str.Write(xdata, 0, xdata.Length);
                            }
                        }
                    }

                    string ncoopIni = Path.Combine(linkBinFolder, "ncoop.ini");
                    using (Stream str = File.OpenWrite(ncoopIni))
                    {
                        byte[] ini = Properties.Resources.ncoop;
                        str.Write(ini, 0, ini.Length);
                    }

                    IniFile x360 = new IniFile(ncoopIni);
                    x360.IniWriteValue("Options", "ForceFocus", gen.Hook.ForceFocus.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "ForceFocusWindowName", gen.Hook.ForceFocusWindowName.ToString(CultureInfo.InvariantCulture));

                    x360.IniWriteValue("Options", "WindowX", playerBounds.X.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "WindowY", playerBounds.Y.ToString(CultureInfo.InvariantCulture));

                    if (context.Hook.SetWindowSize)
                    {
                        x360.IniWriteValue("Options", "ResWidth", context.Width.ToString(CultureInfo.InvariantCulture));
                        x360.IniWriteValue("Options", "ResHeight", context.Height.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        x360.IniWriteValue("Options", "ResWidth", "0");
                        x360.IniWriteValue("Options", "ResHeight", "0");
                    }

                    x360.IniWriteValue("Options", "RerouteInput", context.Hook.XInputReroute.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "RerouteJoystickTemplate", JoystickDatabase.GetID(player.GamepadProductGuid.ToString()).ToString(CultureInfo.InvariantCulture));

                    x360.IniWriteValue("Options", "EnableMKBInput", player.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture));

                    // windows events
                    x360.IniWriteValue("Options", "BlockInputEvents", context.Hook.BlockInputEvents.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "BlockMouseEvents", context.Hook.BlockMouseEvents.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "BlockKeyboardEvents", context.Hook.BlockKeyboardEvents.ToString(CultureInfo.InvariantCulture));

                    // xinput
                    x360.IniWriteValue("Options", "XInputEnabled", context.Hook.XInputEnabled.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "XInputPlayerID", player.GamepadId.ToString(CultureInfo.InvariantCulture));

                    // dinput
                    x360.IniWriteValue("Options", "DInputEnabled", context.Hook.DInputEnabled.ToString(CultureInfo.InvariantCulture));
                    x360.IniWriteValue("Options", "DInputGuid", player.GamepadGuid.ToString().ToUpper());
                    x360.IniWriteValue("Options", "DInputForceDisable", context.Hook.DInputForceDisable.ToString());

                }

                Process proc;
                if (context.NeedsSteamEmulation)
                {
                    string steamEmu = GameManager.Instance.ExtractSteamEmu(Path.Combine(linkFolder, "SmartSteamLoader"));
                    //string steamEmu = GameManager.Instance.ExtractSteamEmu();
                    if (string.IsNullOrEmpty(steamEmu))
                    {
                        return "Extraction of SmartSteamEmu failed!";
                    }

                    string emuExe = Path.Combine(steamEmu, "SmartSteamLoader.exe");
                    string emuIni = Path.Combine(steamEmu, "SmartSteamEmu.ini");
                    IniFile emu = new IniFile(emuIni);

                    emu.IniWriteValue("Launcher", "Target", exePath);
                    emu.IniWriteValue("Launcher", "StartIn", Path.GetDirectoryName(exePath));
                    emu.IniWriteValue("Launcher", "CommandLine", startArgs);
                    emu.IniWriteValue("Launcher", "SteamClientPath", Path.Combine(steamEmu, "SmartSteamEmu.dll"));
                    emu.IniWriteValue("Launcher", "SteamClientPath64", Path.Combine(steamEmu, "SmartSteamEmu64.dll"));
                    emu.IniWriteValue("Launcher", "InjectDll", "1");

                    emu.IniWriteValue("SmartSteamEmu", "AppId", context.SteamID);
                    //emu.IniWriteValue("SmartSteamEmu", "SteamIdGeneration", "Static");

                    //string userName = $"Player { context.PlayerID }";

                    //emu.IniWriteValue("SmartSteamEmu", "PersonaName", userName);
                    //emu.IniWriteValue("SmartSteamEmu", "ManualSteamId", userName);

                    //emu.IniWriteValue("SmartSteamEmu", "Offline", "False");
                    //emu.IniWriteValue("SmartSteamEmu", "MasterServer", "");
                    //emu.IniWriteValue("SmartSteamEmu", "MasterServerGoldSrc", "");

                    gen.SetupSse?.Invoke();

                    if (context.KillMutex?.Length > 0)
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

                    player.SteamEmu = true;
                }
                else
                {
                    if (context.KillMutex?.Length > 0)
                    {
                        proc = Process.GetProcessById(StartGameUtil.StartGame(
                            GetRelativePath(exePath, nucleusRootFolder), startArgs,
                            GetRelativePath(linkFolder, nucleusRootFolder)));
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = exePath;
                        //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = startArgs;
                        startInfo.UseShellExecute = true;
                        startInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
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

                first = false;

                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
            }

            return string.Empty;
        }

        struct TickThread
        {
            public double Interval;
            public Action Function;
        }

        public void StartPlayTick(double interval, Action function)
        {
            Thread t = new Thread(PlayTickThread);

            TickThread tick = new TickThread();
            tick.Interval = interval;
            tick.Function = function;
            t.Start(tick);
        }

        private void PlayTickThread(object state)
        {
            TickThread t = (TickThread)state;

            for (;;)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(t.Interval));
                t.Function();

                if (hasEnded)
                {
                    break;
                }
            }
        }

        public void CenterCursor()
        {
            List<PlayerInfo> players = profile.PlayerData;
            if (players == null)
            {
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo p = players[i];

                if (p.IsKeyboardPlayer)
                {
                    ProcessData data = p.ProcessData;
                    if (data == null)
                    {
                        continue;
                    }

                    Rectangle r = p.MonitorBounds;
                    //Cursor.Clip = r;
                    if (data.HWnd != null)
                    {
                        User32Interop.SetForegroundWindow(data.HWnd.NativePtr);
                    }
                }
            }
        }

        public void Update(double delayMS)
        {
            if (profile == null)
            {
                return;
            }

            exited = 0;
            List<PlayerInfo> players = profile.PlayerData;
            timer += delayMS;

            bool updatedHwnd = false;
            if (timer > HWndInterval)
            {
                updatedHwnd = true;
                timer = 0;
            }

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo p = players[i];
                ProcessData data = p.ProcessData;
                if (data == null)
                {
                    continue;
                }

                if (data.Finished)
                {
                    if (data.Process.HasExited)
                    {
                        exited++;
                    }
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
                    if (updatedHwnd)
                    {
                        if (data.Setted)
                        {
                            if (data.Process.HasExited)
                            {
                                exited++;
                                continue;
                            }

                            data.HWnd.TopMost = true;

                            if (data.Status == 2)
                            {
                                uint lStyle = User32Interop.GetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_STYLE);
                                lStyle = lStyle & ~User32_WS.WS_CAPTION;
                                lStyle = lStyle & ~User32_WS.WS_THICKFRAME;
                                lStyle = lStyle & ~User32_WS.WS_MINIMIZE;
                                lStyle = lStyle & ~User32_WS.WS_MAXIMIZE;
                                lStyle = lStyle & ~User32_WS.WS_SYSMENU;
                                User32Interop.SetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_STYLE, lStyle);

                                lStyle = User32Interop.GetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_EXSTYLE);
                                lStyle = lStyle & ~User32_WS.WS_EX_DLGMODALFRAME;
                                lStyle = lStyle & ~User32_WS.WS_EX_CLIENTEDGE;
                                lStyle = lStyle & ~User32_WS.WS_EX_STATICEDGE;
                                User32Interop.SetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_EXSTYLE, lStyle);
                                User32Interop.SetWindowPos(data.HWnd.NativePtr, IntPtr.Zero, 0, 0, 0, 0, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));
                                //User32Interop.SetForegroundWindow(data.HWnd.NativePtr);

                                data.Finished = true;
                                Debug.WriteLine("State 2");

                                if (i == players.Count - 1 && gen.LockMouse)
                                {
                                    //last screen setuped
                                    _cursorModule.SetActiveWindow();
                                }
                            }
                            else if (data.Status == 1)
                            {
                                data.HWnd.Location = data.Position;
                                data.Status++;
                                Debug.WriteLine("State 1");

                                if (gen.LockMouse)
                                {
                                    if (p.IsKeyboardPlayer)
                                    {
                                        _cursorModule.Setup(data.Process, p.MonitorBounds);
                                    }
                                    else
                                    {
                                        _cursorModule.AddOtherGameHandle(data.Process.MainWindowHandle);
                                    }
                                }
                            }
                            else if (data.Status == 0)
                            {
                                data.HWnd.Size = data.Size;

                                data.Status++;
                                Debug.WriteLine("State 0");
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
                                    if (!string.IsNullOrEmpty(launcher))
                                    {
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
                                    else if (!string.IsNullOrEmpty(gen.Hook.ForceFocusWindowName) &&
                                        // TODO: this Levenshtein distance is being used to help us around Call of Duty Black Ops, as it uses a ® icon in the title bar
                                        //       there must be a better way
                                        StringUtil.ComputeLevenshteinDistance(data.HWnd.Title, gen.Hook.ForceFocusWindowName) > 2) 
                                    {
                                        data.HWNDRetry = true;
                                    }
                                    else
                                    {
                                        Size s = data.Size;
                                        data.Setted = true;
                                    }
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

        public void Log(StreamWriter writer)
        {
        }
    }
}