using SplitScreenMe.Core;
using SplitScreenMe.Core.Handler;
using SplitScreenMe.Core.Modules;
using Nucleus.Gaming.Tools.GameStarter;
using Nucleus.Gaming.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using WindowScrape.Types;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Util;

namespace Nucleus.Gaming.Platform.Windows {
    public class WindowsGameProcessModule : HandlerModule, IGameProcessModule {
        private const float HWndInterval = 10000;

        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerData handlerData;
        private List<Process> attached;
        private GameHandler handler;
        private string actualExe;

        private int exited;
        private double timer;
        private List<int> attachedIds;

        public override int Order { get { return 50; } }

        public WindowsGameProcessModule(PlayerInfo player)
            : base(player) {
        }

        private bool gameIs64;
        private string garch;

        public override bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile) {
            this.handler = handler;
            this.userGame = game;
            this.profile = profile;
            this.handlerData = handlerData;
            actualExe = Path.GetFileNameWithoutExtension(game.ExePath);

            attached = new List<Process>();
            attachedIds = new List<int>() { 0 };

            garch = "x86";
            if (AssemblyUtil.Is64Bit(userGame.ExePath) == true) {
                gameIs64 = true;
                garch = "x64";
            }

            return true;
        }

        public override void PrePlayPlayer(int index, HandlerContext context) {
            if (handlerData.ForceFinishOnPlay) {
                ForceFinish(context);
            }
        }

        public static bool IsNeeded(HandlerData data) {
#if WINDOWS
            return true;
#else
            return false;
#endif
        }

        public override void PlayPlayer(int index, HandlerContext context) {
            Process proc;
            IOModule ioModule = handler.GetModule<IOModule>(Player);

            string startArgs = context.StartArguments;

            string startingApp = ioModule.LinkedExePath;

            if (!string.IsNullOrEmpty(context.OverrideStartProcess)) {
                startingApp = context.OverrideStartProcess;
            }

            if (context.KillMutex?.Length > 0) {
                int processId = StartGameUtil.StartGame(startingApp, startArgs, ioModule.LinkedWorkingDir);
                proc = Process.GetProcessById(processId);
            } else {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = startingApp;
                startInfo.Arguments = startArgs;
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Path.GetDirectoryName(ioModule.LinkedExePath);
                proc = Process.Start(startInfo);
            }

            if (proc == null) {
                for (int times = 0; times < 200; times++) {
                    Thread.Sleep(50);

                    Process[] procs = Process.GetProcesses();
                    string proceName = Path.GetFileNameWithoutExtension(context.ExecutableName).ToLower();
                    string launcherName = string.IsNullOrEmpty(context.LauncherExe) ? string.Empty : Path.GetFileNameWithoutExtension(context.LauncherExe).ToLower();

                    for (int j = 0; j < procs.Length; j++) {
                        Process p = procs[j];
                        string lowerP = p.ProcessName.ToLower();
                        if (((lowerP == proceName) || lowerP == launcherName) &&
                            !attached.Contains(p)) {
                            attached.Add(p);
                            proc = p;
                            break;
                        }
                    }

                    if (proc != null) {
                        break;
                    }
                }
            } else {
                attached.Add(proc);
            }

            ProcessInfo data = new ProcessInfo(proc);

            Rectangle playerBounds = Player.MonitorBounds;
            UserScreen owner = Player.Owner;

            int width = playerBounds.Width;
            int height = playerBounds.Height;

            data.Position = new Point(playerBounds.X, playerBounds.Y);
            data.Size = new Size(playerBounds.Width, playerBounds.Height);
            data.KilledMutexes = context.KillMutex?.Length == 0;
            Player.ProcessData = data;
        }

        public bool HasWindowSetup(PlayerInfo info) {
            ProcessInfo data = info.ProcessData;
            if (data.Setted) {
                return true;
            }
            return false;
        }

        public override void Tick(double delayMs) {
            exited = 0;
            timer += delayMs;

            bool updatedHwnd = false;
            if (timer > HWndInterval) {
                updatedHwnd = true;
                timer = 0;
            }

            List<PlayerInfo> players = profile.PlayerData;
            //CursorModule cursorModule = handler.GetModule<CursorModule>();

            for (int i = 0; i < players.Count; i++) {
                PlayerInfo p = players[i];
                ProcessInfo data = p.ProcessData;
                if (data == null) {
                    continue;
                }

                if (data.Finished) {
                    if (data.Process.HasExited) {
                        exited++;
                    }
                    continue;
                }

                if (updatedHwnd) {
                    if (data.Setted) {
                        if (data.Process.HasExited) {
                            exited++;
                            continue;
                        }

                    } else {
                        data.Process.Refresh();

                        if (data.Process.HasExited) {
                            if (p.GotLauncher) {
                                if (p.GotGame) {
                                    exited++;
                                } else {
                                    List<int> children = ProcessUtil.GetChildrenProcesses(data.Process);
                                    if (children.Count > 0) {
                                        for (int j = 0; j < children.Count; j++) {
                                            int id = children[j];
                                            Process pro = Process.GetProcessById(id);

                                            if (!attached.Contains(pro)) {
                                                attached.Add(pro);
                                                data.HWnd = null;
                                                p.GotGame = true;
                                                data.AssignProcess(pro);
                                            }
                                        }
                                    }
                                }
                            } else {
                                // Steam showing a launcher, need to find our game process
                                string launcher = handlerData.LauncherExe;
                                if (!string.IsNullOrEmpty(launcher)) {
                                    if (launcher.ToLower().EndsWith(".exe")) {
                                        launcher = launcher.Remove(launcher.Length - 4, 4);
                                    }

                                    Process[] procs = Process.GetProcessesByName(launcher);
                                    for (int j = 0; j < procs.Length; j++) {
                                        Process pro = procs[j];
                                        if (!attached.Contains(pro)) {
                                            attached.Add(pro);
                                            data.AssignProcess(pro);
                                            p.GotLauncher = true;
                                        }
                                    }
                                }
                            }
                        } else {
                            if (data.HWNDRetry || data.HWnd == null || data.HWnd.NativePtr != data.Process.MainWindowHandle) {
                                data.HWnd = new HwndObject(data.Process.MainWindowHandle);
                                Point pos = data.HWnd.Location;

                                var windows = User32Util.EnumerateProcessWindowHandles(data.Process.Id);
                                foreach (IntPtr window in windows) {
                                    HwndObject obj = new HwndObject(window);

                                    if (!string.IsNullOrEmpty(handlerData.Hook.ForceFocusWindowRegex) &&
                                        new Regex(handlerData.Hook.ForceFocusWindowRegex).IsMatch(obj.Title)) {
                                        data.HWnd = obj;
                                        break;
                                    }
                                }

                                List<int> children = ProcessUtil.GetChildrenProcesses(data.Process);
                                if (children.Count > 0) {
                                    for (int j = 0; j < children.Count; j++) {
                                        int id = children[j];
                                        Process pro = Process.GetProcessById(id);

                                        var proWindows = User32Util.EnumerateProcessWindowHandles(pro.Id);
                                        foreach (IntPtr window in proWindows) {
                                            HwndObject obj = new HwndObject(window);

                                            if (!string.IsNullOrEmpty(handlerData.Hook.ForceFocusWindowRegex) &&
                                                new Regex(handlerData.Hook.ForceFocusWindowRegex).IsMatch(obj.Title)) {
                                                data.HWnd = obj;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (String.IsNullOrEmpty(data.HWnd.Title) ||
                                    pos.X == -32000 ||
                                    data.HWnd.Title.ToLower() == handlerData.LauncherTitle.ToLower()) {
                                    data.HWNDRetry = true;
                                } else if (!string.IsNullOrEmpty(handlerData.Hook.ForceFocusWindowRegex) &&
                                      // TODO: this Levenshtein distance is being used to help us around Call of Duty Black Ops, as it uses a ® icon in the title bar
                                      //       there must be a better way
                                      //StringUtil.ComputeLevenshteinDistance(data.HWnd.Title, handlerData.Hook.ForceFocusWindowRegex) > 2)
                                      !(new Regex(handlerData.Hook.ForceFocusWindowRegex).IsMatch(data.HWnd.Title))) {
                                    /// ?? redundant call??
                                    data.HWNDRetry = true;
                                } else {
                                    Size s = data.Size;
                                    data.Setted = true;
                                }
                            }
                        }
                    }
                }
            }

            if (exited == players.Count) {
                handler.End();
            }
        }

        private void ForceFinish(HandlerContext context) {
            // search for game instances left behind
            try {
                Process[] procs = Process.GetProcesses();

                foreach (Process proc in procs) {
                    try {
                        if (proc.ProcessName.ToLower() == Path.GetFileNameWithoutExtension(context.ExecutableName.ToLower()) || 
                            (proc.Id != 0 && attachedIds.Contains(proc.Id)) || 
                            new Regex(handlerData.Hook.ForceFocusWindowRegex).IsMatch(proc.MainWindowTitle)) {
                            Log.WriteLine(string.Format("Killing process {0} (pid {1})", proc.ProcessName, proc.Id));
                            proc.Kill();
                        }
                    } catch {

                    }
                }
            } catch { }
        }
    }
}