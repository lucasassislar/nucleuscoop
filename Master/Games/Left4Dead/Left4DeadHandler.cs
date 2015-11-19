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

namespace Games.Left4Dead
{
    public class Left4DeadHandler : IGameHandler
    {
        public static readonly string CFGFolder = @"left4dead\cfg";
        public static readonly string BackupFolder = @"left4dead\backup";

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
        protected bool firstKeyboard;
        protected Size originalSize;
        private int fpsLock;
        private string ip;

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
            instances = true;
            //instances = (bool)options["instance"].Value;
            firstKeyboard = (bool)options["keyboard"].Value;
            fpsLock = (int)options["engine_no_focus_sleep"].Value;
            ip = options["ip_connect"].Value.ToString();

            if (!instances && players.Count > 2)
            {
                MessageBox.Show("For now you cannot innitialize Left 4 Dead without the instances property set to true when there are more than 2 players. Changing...");
                instances = true;
            }

            levelCommand = ((Left4DeadLevelSelection)addSteps[0]).LevelName;
            gameMode = ((Left4DeadModeSelection)addSteps[1]).ModeCommand;

            // Search for video file
            string exeFolder = Path.GetDirectoryName(executablePlace);
            binFolder = Path.Combine(exeFolder, "bin");
            string l4d = Path.Combine(exeFolder, "left4dead");
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
                return "Steam must be opened to play Left 4 Dead splitscreen";
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
                    string l4dBinFolder = Path.Combine(l4dFolder, "bin");

                    if (i != 0)
                    {
                        string l4d = Path.Combine(folder, "L4D_" + (i + 1));
                        Directory.CreateDirectory(l4d);

                        FolderUtil.MkLink(l4dFolder, l4d, "bin", "left4dead.exe");

                        // copy executable
                        execPlace = Path.Combine(l4d, "left4dead.exe");
                        File.Copy(Path.Combine(l4dFolder, "left4dead.exe"), execPlace, true);

                        // make bin folder now
                        l4dBinFolder = Path.Combine(l4d, "bin");
                        string originalBinFolder = Path.Combine(l4dFolder, "bin");
                        Directory.CreateDirectory(l4dBinFolder);

                        FolderUtil.MkLink(originalBinFolder, l4dBinFolder, "xinput1_3.dll");

                        // add exec to firewall
                        FirewallUtil.AuthorizeProgram("Left 4 Dead", execPlace);
                    }

                    // copy the correct xinput to the bin folder
                    byte[] xdata = null;
                    if (firstKeyboard)
                    {
                        switch (gamePadId)
                        {
                            case 0:
                                xdata = GamesResources._4_xinput1_3;
                                break;
                            case 1:
                                xdata = GamesResources._1_xinput1_3;
                                break;
                            case 2:
                                xdata = GamesResources._2_xinput1_3;
                                break;
                            case 3:
                                xdata = GamesResources._3_xinput1_3;
                                break;
                        }
                    }
                    else
                    {
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
                        }
                    }
                    string xinputPath = Path.Combine(l4dBinFolder, "xinput1_3.dll");
                    using (MemoryStream stream = new MemoryStream(xdata))
                    {
                        // write to bin folder
                        using (FileStream file = new FileStream(xinputPath, FileMode.Create))
                        {
                            stream.WriteTo(file);
                        }
                    }
                    gamePadId++;


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
                }

                loaded = true;
            }
            else
            {
                int screenIndex = -1;
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
                            //twoScreens = true;
                            // Add 2nd monitor
                            fullWidth += scr.Bounds.Width;
                        }
                    }
                }

                loaded = true;

                string fWidth = fullWidth.ToString();
                string fHeight = fullHeight.ToString();
                string fullScr = (0).ToString();
                string noWindowBorderStr = (1).ToString();

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

                StartGameUtil.StartGame(executablePlace, "-novid -insecure", delayTime, "hl2_singleton_mutex", "steam_singleton_mutext");
            }

            return string.Empty;
        }

        private void MakeAutoExecSplitscreen(string splitMode)
        {
            using (FileStream stream = new FileStream(autoExec, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine("engine_no_focus_sleep " + fpsLock.ToString(CultureInfo.InvariantCulture));
                writer.WriteLine("ss_splitmode " + splitMode);
                writer.WriteLine("ss_map " + levelCommand + " " + gameMode);
                writer.WriteLine("sv_allow_lobby_connect_only 0");
                writer.WriteLine(@"name2 Player2
joystick 1
joy_advanced ""1"" // use advanced joystick options (allows for multiple axes)

joy_name ""L4D Xbox360 Joystick Configuration""
joy_inverty2 0
joy_advaxisx 3 // x-axis controls GAME_AXIS_SIDE (strafing left and right)
joy_advaxisy 1 // y-axis controls GAME_AXIS_FORWARD (move forward and back)
joy_advaxisz 0 // z-axis is treated like a button
joy_advaxisr 2 // r-axis controls GAME_AXIS_PITCH (look up and down)
joy_advaxisu 4 // u-axis controls GAME_AXIS_YAW (look left and right)
joy_advaxisv 0 // v-axis is unused
joy_forwardsensitivity -1.0 // movement sensitivity
joy_sidesensitivity 1.0
joy_forwardthreshold 0.1 // movement dead zone settings
joy_sidethreshold 0.1
joy_pitchsensitivity 1.0 // look sensitivity
joy_yawsensitivity -1.5
joy_pitchthreshold 0.1 // look dead zone settings
joy_yawthreshold 0.0

joy_variable_frametime 1
joy_autoaimdampenrange 0.85
joy_autoaimdampen 0.5
joy_lowend 0.65
joy_lowmap 0.15
joy_accelscale 3.0
joy_accelmax 4.0
joy_response_move 5
joy_response_look 1
joy_autoaimdampen 0.3
joy_autoaimdampenrange 0.85
joyadvancedupdate // advanced joystick update allows for analog control of move and look

// Alternate control 1
bind ""0"" ""slot10""
bind ""1"" ""slot1""
bind ""2"" ""slot2""
bind ""3"" ""slot3""
bind ""4"" ""slot4""
bind ""5"" ""slot5""
bind ""6"" ""slot6""
bind ""7"" ""slot7""
bind ""8"" ""slot8""
bind ""9"" ""slot9""
bind ""a"" ""+moveleft""
bind ""c"" ""+voicerecord""
bind ""d"" ""+moveright""
bind ""e"" ""+use""
bind ""f"" ""impulse 100""
bind ""h"" ""motd""
bind ""m"" ""chooseteam""
bind ""q"" ""lastinv""
bind ""r"" ""+reload""
bind ""s"" ""+back""
bind ""t"" ""impulse 201""
bind ""u"" ""messagemode2""
bind ""w"" ""+forward""
bind ""x"" ""+mouse_menu QA""
bind ""y"" ""messagemode""
bind ""z"" ""+mouse_menu Orders""
bind ""`"" ""toggleconsole""
bind ""SPACE"" ""+jump""
bind ""TAB"" ""+showscores""
bind ""ESCAPE"" ""cancelselect""
bind ""SHIFT"" ""+speed""
bind ""CTRL"" ""+duck""
bind ""F1"" ""Vote Yes""
bind ""F2"" ""Vote No""
bind ""F5"" ""jpeg""
bind ""MOUSE1"" ""+attack""
bind ""MOUSE2"" ""+attack2""
bind ""MOUSE3"" ""+zoom""
bind ""MWHEELUP"" ""invprev""
bind ""MWHEELDOWN"" ""invnext""


// controller2 bindings
cmd2 +jlook // enable joystick look
cmd2 bind ""A_BUTTON"" ""+jump;+menuAccept"" // (A) button - Jump -menuAccpt allows us to make selections on hud menus
cmd2 bind ""B_BUTTON"" ""+reload"" // (B) button - Reload
cmd2 bind ""X_BUTTON"" ""+use"" // (X) Use
cmd2 bind ""Y_BUTTON"" ""lastinv"" // (Y) button - swap pistol/rifle or z_abort -used to respawn as a ghost.
cmd2 bind ""R_TRIGGER"" ""+attack"" // RT - Main weapon - Primary trigger
cmd2 bind ""L_TRIGGER"" ""+attack2"" // LT - Melee
cmd2 bind ""R_SHOULDER"" ""+lookspin"" // RB - Fast 180 spin
cmd2 bind ""L_SHOULDER"" ""toggle_duck"" // LB - Duck - is also used to give objects to people.
cmd2 bind ""STICK1"" ""vocalize smartlook"" // LS - vocalize
cmd2 bind ""STICK2"" ""+zoom"" // RS click - Rifle Zoom

// Fixed bindings, do not change these across joystick presets
cmd2 bind ""BACK"" ""togglescores"" // (back) button - scores
cmd2 bind ""START"" ""pause"" // (start) button - pause
cmd2 bind ""S1_UP"" ""+menuUp"" // Hud menu Up
cmd2 bind ""S1_DOWN"" ""+menuDown"" // Hud menu Down
cmd2 bind ""UP"" ""impulse 100"" // DPad Up - Toggle flashlight
cmd2 bind ""LEFT"" ""slot3"" // DPad Left - grenade
cmd2 bind ""RIGHT"" ""slot4"" // DPad Right - health
cmd2 bind ""DOWN"" ""slot5"" // DPad Down - Pills"");");

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
                writer.WriteLine("engine_no_focus_sleep " + fpsLock.ToString(CultureInfo.InvariantCulture));
                writer.WriteLine("connect " + ip);
                writer.WriteLine("exec 360controller");
                writer.Flush();
            }
        }
        private void MakeAutoExecServer()
        {
            using (FileStream stream = new FileStream(autoExec, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("engine_no_focus_sleep " + fpsLock.ToString(CultureInfo.InvariantCulture));
                writer.WriteLine("sv_lan 1");
                writer.WriteLine("sv_allow_lobby_connect_only 0");
                writer.WriteLine("map " + levelCommand + " " + gameMode);
                if (firstKeyboard)
                {
                    writer.WriteLine("exec undo360controller");
                }
                else
                {
                    writer.WriteLine("exec 360controller");
                }
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

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public void Update(int delayMS)
        {
            if (!loaded)
            {
                return;
            }

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
