using Games.Left4Dead2;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WindowScrape.Types;
using Nucleus;
using System.Runtime.InteropServices;

namespace Games
{
    public class Left4Dead2Handler : IGameHandler
    {
        public static readonly string CFGFolder = @"left4dead2\cfg";
        public static readonly string BackupFolder = @"left4dead2\backup";

        protected string executablePlace;
        protected List<PlayerInfo> players;
        protected Dictionary<string, GameOption> options;
        protected string videoFile;
        protected int titleHeight;
        protected string binFolder;
        protected string autoExec;
        protected string makeSplit;
        protected string pak01_000_path;
        protected string backupPak;
        protected int delayTime;

        [DllImport("TheHooker.dll")]
        static extern bool InstallHook(uint id);

        [DllImport("TheHooker.dll")]
        static extern bool RemoveHook();

        public bool HideTaskBar
        {
            get { return true; }
        }


        protected string levelCommand;
        protected string gameMode;
        private bool instances;
        protected PlayerID keyboardId;
        protected Size originalSize;


        [DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal static extern bool SetCursorPos(int X, int Y);

        public bool Initialize(string gameFilename, List<PlayerInfo> players, Dictionary<string, GameOption> options, List<Control> addSteps, int titleHeight)
        {
            this.executablePlace = gameFilename;
            this.players = players;
            this.options = options;
            this.titleHeight = titleHeight - 5;

            originalSize = FormUtil.MainForm.Size;

            delayTime = (int)options["delay"].Value * 1000;
            instances = (bool)options["instance"].Value;
            keyboardId = (PlayerID)options["keyboard"].Value;

            if (!instances)
            {
                MessageBox.Show("For now you cannot innitialize Left 4 Dead 2 without the instances property set to true. Changing...");
                instances = true;
            }

            levelCommand = ((Left4Dead2LevelSelection)addSteps[0]).LevelName;
            gameMode = ((Left4Dead2ModeSelection)addSteps[1]).ModeCommand;

            // Search for video file
            string exeFolder = Path.GetDirectoryName(executablePlace);
            binFolder = Path.Combine(exeFolder, "bin");
            string l4d2 = Path.Combine(exeFolder, "left4dead2");
            pak01_000_path = l4d2 + "\\pak01_000.vpk";
            backupPak = Path.Combine(exeFolder, BackupFolder) + "\\pak01_000.vpk";
            string cfgFolder = Path.Combine(exeFolder, CFGFolder);
            autoExec = cfgFolder + "\\autoexec.cfg";
            makeSplit = cfgFolder + "\\makesplit.cfg";
            videoFile = cfgFolder + "\\video.txt";

            if (!File.Exists(videoFile))
            {
                MessageBox.Show("Could not find Video.txt file! Should be on '" + CFGFolder + "'");

                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.Filter = "Video.txt file|Video.txt";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        videoFile = open.FileName;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected SourceCfgFile videoCfg;
        private bool loaded;

        public string Play()
        {
            if (!SteamUtil.IsSteamRunning())
            {
                return "Steam must be opened to play Left 4 Dead 2 splitScreen";
            }

            using (Stream videoStream = new FileStream(videoFile, FileMode.Open))
            {
                videoCfg = new SourceCfgFile(videoStream);
            }
            string originalCFG = String.Copy(videoCfg.RawData);

            // minimize everything
            User32.MinimizeEverything();
            Screen[] allScreens = Screen.AllScreens;

            string folder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string l4dFolder = Path.GetDirectoryName(executablePlace);
            int gamePadId = 0;


            if (instances)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo p = players[i];

                    Screen screen = allScreens[p.ScreenIndex];
                    int width = 0;
                    int height = 0;
                    Rectangle bounds = screen.Bounds;
                    Point location = new Point();

                    ViewportUtil.GetPlayerViewport(p, 0, out width, out height, out location);

                    CultureInfo c = CultureInfo.InvariantCulture;
                    UpdateVideoCfg(width.ToString(c), height.ToString(c), "0", "1");

                    if (i == 0)
                    {
                        MakeAutoExecServer();
                    }
                    else
                    {
                        MakeAutoExecClient();
                    }
                    MakeMakeSplit();

                    string execPlace = executablePlace;
                    string l4dBinFolder;
                    if (i == 0)
                    {
                        l4dBinFolder = Path.Combine(l4dFolder, "bin");
                    }
                    else
                    {
                        string l4d = Path.Combine(folder, "L4D2_" + (i + 1));
                        Directory.CreateDirectory(l4d);

                        int exitCode;
                        #region mklink
                        CmdUtil.ExecuteCommand(l4d, out exitCode,
                            "mklink /d \"" + Path.Combine(l4d, "config") + "\" \"" + Path.Combine(l4dFolder, "config") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "hl2") + "\" \"" + Path.Combine(l4dFolder, "hl2") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "left4dead2") + "\" \"" + Path.Combine(l4dFolder, "left4dead2") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "left4dead2_dlc1") + "\" \"" + Path.Combine(l4dFolder, "left4dead2_dlc1") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "left4dead2_dlc2") + "\" \"" + Path.Combine(l4dFolder, "left4dead2_dlc2") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "left4dead2_dlc3") + "\" \"" + Path.Combine(l4dFolder, "left4dead2_dlc3") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "platform") + "\" \"" + Path.Combine(l4dFolder, "platform") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "RemStorage") + "\" \"" + Path.Combine(l4dFolder, "RemStorage") + "\"",
                            "mklink /d \"" + Path.Combine(l4d, "update") + "\" \"" + Path.Combine(l4dFolder, "update") + "\"");
                        #endregion

                        // copy executable
                        File.Copy(Path.Combine(l4dFolder, "left4dead2.exe"), Path.Combine(l4d, "left4dead2.exe"), true);

                        // make bin folder now
                        l4dBinFolder = Path.Combine(l4d, "bin");
                        string originalBinFolder = Path.Combine(l4dFolder, "bin");
                        Directory.CreateDirectory(l4dBinFolder);

                        #region mklink
                        CmdUtil.ExecuteCommand(l4d, out exitCode,
                            "mklink /d \"" + Path.Combine(l4dBinFolder, "dedicated") + "\"  \"" + Path.Combine(originalBinFolder, "dedicated") + "\"",
                            "mklink /d \"" + Path.Combine(l4dBinFolder, "linux32") + "\"  \"" + Path.Combine(originalBinFolder, "linux32") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "addoninstaller.exe") + "\"  \"" + Path.Combine(originalBinFolder, "addoninstaller.exe") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "addoninstaller_osx") + "\"  \"" + Path.Combine(originalBinFolder, "addoninstaller_osx") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "adminserver.dll") + "\"  \"" + Path.Combine(originalBinFolder, "adminserver.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "binkw32.dll") + "\"  \"" + Path.Combine(originalBinFolder, "binkw32.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "bsppack.dll") + "\"  \"" + Path.Combine(originalBinFolder, "bsppack.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "bugreporter.dll") + "\"  \"" + Path.Combine(originalBinFolder, "bugreporter.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "bugreporter_public.dll") + "\"  \"" + Path.Combine(originalBinFolder, "bugreporter_public.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "chromehtml.dll") + "\"  \"" + Path.Combine(originalBinFolder, "chromehtml.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "config.SoftTHconfig") + "\"  \"" + Path.Combine(originalBinFolder, "config.SoftTHconfig") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "crashhandler.dll") + "\"  \"" + Path.Combine(originalBinFolder, "crashhandler.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "datacache.dll") + "\"  \"" + Path.Combine(originalBinFolder, "datacache.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "dxsupport.cfg") + "\"  \"" + Path.Combine(originalBinFolder, "dxsupport.cfg") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "dxsupport_episodic.cfg") + "\"  \"" + Path.Combine(originalBinFolder, "dxsupport_episodic.cfg") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "dxsupport_mac.cfg") + "\"  \"" + Path.Combine(originalBinFolder, "dxsupport_mac.cfg") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "engine.dll") + "\"  \"" + Path.Combine(originalBinFolder, "engine.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "export_entity_group.pl") + "\"  \"" + Path.Combine(originalBinFolder, "export_entity_group.pl") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "export_entity_layer.pl") + "\"  \"" + Path.Combine(originalBinFolder, "export_entity_layer.pl") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "filesystemopendialog.dll") + "\"  \"" + Path.Combine(originalBinFolder, "filesystemopendialog.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "filesystem_stdio.dll") + "\"  \"" + Path.Combine(originalBinFolder, "filesystem_stdio.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "GameOverlayRenderer.log") + "\"  \"" + Path.Combine(originalBinFolder, "GameOverlayRenderer.log") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "gameui.dll") + "\"  \"" + Path.Combine(originalBinFolder, "gameui.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "hl_ent.cnt") + "\"  \"" + Path.Combine(originalBinFolder, "hl_ent.cnt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "icudt.dll") + "\"  \"" + Path.Combine(originalBinFolder, "icudt.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "icudt42.dll") + "\"  \"" + Path.Combine(originalBinFolder, "icudt42.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "inputsystem.dll") + "\"  \"" + Path.Combine(originalBinFolder, "inputsystem.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "launcher.dll") + "\"  \"" + Path.Combine(originalBinFolder, "launcher.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "libcef.dll") + "\"  \"" + Path.Combine(originalBinFolder, "libcef.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "libmysql.dll") + "\"  \"" + Path.Combine(originalBinFolder, "libmysql.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "maplist_terror.txt") + "\"  \"" + Path.Combine(originalBinFolder, "maplist_terror.txt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "materialsystem.dll") + "\"  \"" + Path.Combine(originalBinFolder, "materialsystem.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "mdllib.dll") + "\"  \"" + Path.Combine(originalBinFolder, "mdllib.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "mss32.dll") + "\"  \"" + Path.Combine(originalBinFolder, "mss32.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "mssdolby.flt") + "\"  \"" + Path.Combine(originalBinFolder, "mssdolby.flt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "mssds3d.flt") + "\"  \"" + Path.Combine(originalBinFolder, "mssds3d.flt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "mssdsp.flt") + "\"  \"" + Path.Combine(originalBinFolder, "mssdsp.flt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "msseax.flt") + "\"  \"" + Path.Combine(originalBinFolder, "msseax.flt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "mssmp3.asi") + "\"  \"" + Path.Combine(originalBinFolder, "mssmp3.asi") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "msssrs.flt") + "\"  \"" + Path.Combine(originalBinFolder, "msssrs.flt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "msvcr71.dll") + "\"  \"" + Path.Combine(originalBinFolder, "msvcr71.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "parsifal.dll") + "\"  \"" + Path.Combine(originalBinFolder, "parsifal.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "rdmwin32.dll") + "\"  \"" + Path.Combine(originalBinFolder, "rdmwin32.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "scenefilecache.dll") + "\"  \"" + Path.Combine(originalBinFolder, "scenefilecache.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "serverbrowser.dll") + "\"  \"" + Path.Combine(originalBinFolder, "serverbrowser.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "serverplugin_empty.dll") + "\"  \"" + Path.Combine(originalBinFolder, "serverplugin_empty.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "shaderapidx10.dll") + "\"  \"" + Path.Combine(originalBinFolder, "shaderapidx10.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "shaderapidx9.dll") + "\"  \"" + Path.Combine(originalBinFolder, "shaderapidx9.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "shaderapiempty.dll") + "\"  \"" + Path.Combine(originalBinFolder, "shaderapiempty.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "soundemittersystem.dll") + "\"  \"" + Path.Combine(originalBinFolder, "soundemittersystem.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "soundsystem.dll") + "\"  \"" + Path.Combine(originalBinFolder, "soundsystem.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "stats.bin") + "\"  \"" + Path.Combine(originalBinFolder, "stats.bin") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "stdshader_dbg.dll") + "\"  \"" + Path.Combine(originalBinFolder, "stdshader_dbg.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "stdshader_dx9.dll") + "\"  \"" + Path.Combine(originalBinFolder, "stdshader_dx9.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "Steam.dll") + "\"  \"" + Path.Combine(originalBinFolder, "Steam.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "steamclient.dll") + "\"  \"" + Path.Combine(originalBinFolder, "steamclient.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "steamclient_l4d2.dll") + "\"  \"" + Path.Combine(originalBinFolder, "steamclient_l4d2.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "steam_api.dll") + "\"  \"" + Path.Combine(originalBinFolder, "steam_api.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "steam_appid.txt") + "\"  \"" + Path.Combine(originalBinFolder, "steam_appid.txt") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "Steam_l4d2.dll") + "\"  \"" + Path.Combine(originalBinFolder, "Steam_l4d2.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "studiorender.dll") + "\"  \"" + Path.Combine(originalBinFolder, "studiorender.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "texturecompile_dll.dll") + "\"  \"" + Path.Combine(originalBinFolder, "texturecompile_dll.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "tier0.dll") + "\"  \"" + Path.Combine(originalBinFolder, "tier0.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "tier0_s.dll") + "\"  \"" + Path.Combine(originalBinFolder, "tier0_s.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "unicode.dll") + "\"  \"" + Path.Combine(originalBinFolder, "unicode.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "unicows.dll") + "\"  \"" + Path.Combine(originalBinFolder, "unicows.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "unitlib.dll") + "\"  \"" + Path.Combine(originalBinFolder, "unitlib.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "valve_avi.dll") + "\"  \"" + Path.Combine(originalBinFolder, "valve_avi.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vaudio_miles.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vaudio_miles.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vaudio_speex.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vaudio_speex.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vgui2.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vgui2.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vguimatsurface.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vguimatsurface.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vidcfg.bin") + "\"  \"" + Path.Combine(originalBinFolder, "vidcfg.bin") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vphysics.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vphysics.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vscript.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vscript.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vstdlib.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vstdlib.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vstdlib_s.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vstdlib_s.dll") + "\"",
                            "mklink \"" + Path.Combine(l4dBinFolder, "vtex_dll.dll") + "\"  \"" + Path.Combine(originalBinFolder, "vtex_dll.dll") + "\"");
                        #endregion

                        execPlace = Path.Combine(l4d, "left4dead2.exe");
                    }

                    if ((int)keyboardId == i)
                    {
                        // delete if there are any xinput
                        string xinputPath = Path.Combine(l4dBinFolder, "xinput1_3.dll");
                        if (File.Exists(xinputPath))
                        {
                            File.Delete(xinputPath);
                        }


                    }
                    else
                    {
                        // copy the correct xinput to the bin folder
                        byte[] xdata = null;
                        switch (gamePadId)
                        {
                            case 0:
                                xdata = GamesResources._1_xinput1_3;
                                break;
                            case 1:
                                xdata = GamesResources._2_xinput1_3;
                                break;
                            case 2:
                                xdata = GamesResources._3_xinput1_3;
                                break;
                            case 3:
                                xdata = GamesResources._4_xinput1_3;
                                break;
                            default:
                                xdata = GamesResources._4_xinput1_3;
                                break;
                        }
                        string xinputPath = Path.Combine(l4dBinFolder, "xinput1_3.dll");
                        using (MemoryStream stream = new MemoryStream(xdata))
                        {
                            // write to bin folder
                            using (FileStream file = new FileStream(xinputPath, FileMode.Create))
                            {
                                stream.CopyTo(file);
                            }
                        }
                        gamePadId++;
                    }


                    int pid = StartGameUtil.StartGame(execPlace,
                        "-novid -insecure", delayTime, "hl2_singleton_mutex", "steam_singleton_mutext");
                    Process proc = Process.GetProcessById(pid);

                    HwndObject hwnd = new HwndObject(proc.Handle);
                    ScreenData data = new ScreenData();
                    data.Position = location;
                    data.HWND = hwnd;
                    data.Size = new Size(width, height);
                    p.Process = proc;
                    p.Tag = data;

                    Thread.Sleep(delayTime);

                    //uint processHandle;
                    //IntPtr windowHandle = proc.MainWindowHandle;
                    //uint threadID = User32.GetWindowThreadProcessId(windowHandle, out processHandle);
                    //bool installed = InstallHook(threadID);
                }
            }
            else
            {
                List<DuetPlayerInfo> duets = ViewportUtil.GetPlayerDuets(players);
                for (int i = 0; i < duets.Count; i++)
                {
                    DuetPlayerInfo duet = duets[i];
                }
            }

            int screenIndex = -1;
            bool twoScreens = false;
            int fullWidth = 0;
            int fullHeight = 0;
            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];
                Screen scr = allScreens[player.ScreenIndex];

                if (screenIndex == -1)
                {
                    screenIndex = player.ScreenIndex;
                    fullWidth = scr.Bounds.Width;
                    fullHeight = scr.Bounds.Height;
                }
                else
                {
                    if (screenIndex != player.ScreenIndex)
                    {
                        twoScreens = true;
                        // Add 2nd monitor
                        fullWidth += scr.Bounds.Width;
                    }
                }
            }

            loaded = true;

            return "";

            string fWidth = fullWidth.ToString();
            string fHeight = fullHeight.ToString();
            string fullScr = (0).ToString();
            string noWindowBorderStr = (1).ToString();

            //if (players.Count == 1)
            //{
            //    // 1 monitor
            //    Screen monitor = allScreens[players.First().Key];
            //    fWidth = monitor.Bounds.Width.ToString(CultureInfo.InvariantCulture);
            //    fHeight = monitor.Bounds.Height.ToString(CultureInfo.InvariantCulture);

            //    fullScr = (0).ToString();
            //    noWindowBorderStr = (1).ToString();
            //}
            //else
            //{
            //    // 2 or more monitors

            //    //??
            //    fullScr = (1).ToString();
            //    noWindowBorderStr = (0).ToString();
            //}

            fWidth = "1920";// 960x540
            fHeight = "540";

            string d3d9Path = binFolder + @"\d3d9.dll";
            // SoftTH
            if (twoScreens)
            {
                // Copy SoftTH to the game folder
                // Get the SoftTH stream
                if (!File.Exists(d3d9Path))
                {
                    using (MemoryStream stream = new MemoryStream(GamesResources.d3d9))
                    {
                        // write to bin folder
                        using (FileStream file = new FileStream(d3d9Path, FileMode.Create))
                        {
                            stream.CopyTo(file);
                        }
                    }
                }
            }
            else
            {
                // Delete SoftTH from the game folder
                FileInfo file = new FileInfo(d3d9Path);
                // Only delete if it's not read only
                if (File.Exists(d3d9Path))
                {
                    File.Delete(d3d9Path);
                }
            }

            // PAK hex edit
            if (twoScreens)
            {
                FileInfo f = new FileInfo(pak01_000_path);
                if (!f.IsReadOnly)
                {
                    // make a backup of pak01_000, if it isn't already made
                    string dir = Path.GetDirectoryName(backupPak);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    if (!File.Exists(backupPak))
                    {
                        File.Copy(pak01_000_path, backupPak);
                    }

                    using (FileStream str = File.OpenRead(backupPak))
                    {
                        // Now modify some stuff
                        // Eat Point that comes at 29.664.480
                        // The 8 is at position 29.664.483

                        // 29.675.904

                        byte one = 49;
                        byte six = 54;

                        using (FileStream stream = new FileStream(pak01_000_path, FileMode.Create))
                        {
                            byte[] buff = new byte[4096];

                            int point = 29664480;

                            while (str.Position < point)
                            {
                                int length = 4096;
                                if (point - str.Position < length)
                                {
                                    length = (int)(point - str.Position);
                                }

                                str.Read(buff, 0, length);
                                stream.Write(buff, 0, length);
                            }
                            // Jumps 1 byte
                            str.Position += 1;

                            str.Read(buff, 0, 2);
                            stream.Write(buff, 0, 2);

                            // Jumps the 8
                            str.Position += 1;

                            buff[0] = one;
                            buff[1] = six;
                            stream.Write(buff, 0, 2);

                            while (stream.Position < str.Length)
                            {
                                int length = 4096;
                                if (str.Length - str.Position < length)
                                {
                                    length = (int)(str.Length - str.Position);
                                }

                                str.Read(buff, 0, length);
                                stream.Write(buff, 0, length);
                            }
                        }
                    }

                    f.IsReadOnly = true;
                }
            }
            //else
            //{
            //    if (File.Exists(backupPak))
            //    {
            //        if (File.Exists(pak01_000))
            //        {
            //            FileInfo f = new FileInfo(pak01_000);
            //            f.IsReadOnly = false;
            //            File.Delete(pak01_000);
            //        }
            //        File.Copy(backupPak, pak01_000);
            //    }
            //}

            int splitMode = 1;
            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];
                if (player.ScreenType == ScreenType.HorizontalBottom ||
                    player.ScreenType == ScreenType.HorizontalTop)
                {
                    splitMode = 1;
                }
                else
                {
                    splitMode = 2;
                }
            }


            MakeAutoExecSplitscreen(splitMode.ToString(CultureInfo.InvariantCulture));
            MakeMakeSplit();


            /* if (player.ScreenType == ScreenType.VerticalLeft ||
                    player.ScreenType == ScreenType.VerticalRight)*/

            //ss_splitmode 1 = horizontal
            //ss_splitmode 2 = vertical

            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = executablePlace;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //MessageBox.Show("Press the F8 key after the game is loaded to begin splitscreen!");

            //proc = Process.Start(startInfo);


            return string.Empty;
        }

        private void MakeAutoExecSplitscreen(string splitMode)
        {
            using (FileStream stream = new FileStream(autoExec, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("ss_splitmode " + splitMode);
                writer.WriteLine("ss_map " + levelCommand + " " + gameMode);
                writer.WriteLine("sv_allow_lobby_connect_only 0");
                string aspas = '"'.ToString();
                writer.WriteLine("bind F8 " + aspas + "exec makesplit" + aspas);

                writer.Flush();
            }
        }
        private void MakeAutoExecClient()
        {
            using (FileStream stream = new FileStream(autoExec, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("connect 192.168.1.105");
                writer.WriteLine("exec 360controller");
                //writer.WriteLine("connect localhost");
                writer.Flush();
            }
        }
        private void MakeAutoExecServer()
        {
            using (FileStream stream = new FileStream(autoExec, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("sv_lan 1");
                writer.WriteLine("sv_allow_lobby_connect_only 0");
                writer.WriteLine("map " + levelCommand + " " + gameMode);
                writer.WriteLine("exec 360controller");
                writer.Flush();
            }
        }

        private void MakeMakeSplit()
        {
            using (FileStream stream = new FileStream(makeSplit, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("connect_splitscreen localhost 2");

                writer.Flush();
            }
        }



        private void UpdateVideoCfg(string width, string height, string fullscreen, string noWindowBorder)
        {
            const string defaultRes = "defaultres";
            const string defaultResHeight = "defaultresheight";
            const string def_fullscreen = "fullscreen";
            const string nowindowborder = "nowindowborder";

            videoCfg.Reset();
            videoCfg.ChangeProperty(defaultRes, width);
            videoCfg.ChangeProperty(defaultResHeight, height);
            videoCfg.ChangeProperty(def_fullscreen, fullscreen);
            videoCfg.ChangeProperty(nowindowborder, noWindowBorder);

            using (Stream videoStream = new FileStream(videoFile, FileMode.Create))
            {
                videoCfg.Write(videoStream);
            }
        }

        public static void sendKeystroke(ushort k, IntPtr window)
        {
            const int WM_KEYDOWN = 0x100;
            IntPtr result3 = User32Interop.SendMessage(window, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
        }

        public bool Ended
        {
            get { return ended; }
        }
        private bool ended;
        private int delay;

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        int focus = 0;
        bool even = false;

        public void Update(int delayMS)
        {
            if (!loaded)
            {
                return;
            }

            //Form main = FormUtil.MainForm;
            //main.Size = new Size(0, 0);
            //main.Location = new Point(10, 10);
            //main.TopMost = true;
            ////SetForegroundWindow(main.Handle);

            //if (even)
            //{
            //    SetCursorPos(100, 100);
            //    even = !even;
            //}
            //else
            //{
            //    SetCursorPos(150, 150);
            //    even = !even;
            //}

            delay += delayMS;
            if (delay > this.delayTime)
            {
                int exited = 0;
                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo p = players[i];
                    ScreenData data = (ScreenData)p.Tag;
                    if (!data.Set)
                    {
                        data.HWND = new HwndObject(p.Process.MainWindowHandle);
                        data.HWND.Location = data.Position;

                        data.Set = true;
                    }


                    if (p.Process.HasExited)
                    {
                        exited++;
                    }
                }

                if (exited == players.Count)
                {
                    ended = true;
                }
            }
        }

        public void End()
        {
            if (!string.IsNullOrEmpty(autoExec))
            {
                if (File.Exists(autoExec))
                {
                    using (FileStream stream = new FileStream(autoExec, FileMode.Create))
                    {
                        StreamWriter writer = new StreamWriter(stream);

                        // Empties auto exec so the game won't try loading the multiplayer level
                        writer.Write("");
                        writer.Flush();
                    }
                }
            }

            FormUtil.MainForm.Size = originalSize;
        }


        public int TimerInterval
        {
            get { return 16; }
        }
    }
}
