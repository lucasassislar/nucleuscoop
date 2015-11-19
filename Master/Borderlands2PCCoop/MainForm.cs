using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using Nucleus.Gaming;
using PCCoop;

namespace Borderlands2PCCoop
{
    public partial class MainForm : Form
    {
        public List<Panel> Panels;
        public List<PlayerInfo> Players;
        private List<ScreenControl> Screens;

        public GameManager GameManager;

        protected GameInfo SelectedGame;

        public MainForm()
        {
            InitializeComponent();
            this.groupBox2.Enabled = false;

            GameManager = new GameManager();
            Dictionary<string, GameInfo> allGames = GameManager.Games;
            for (int i = 0; i < allGames.Count; i++)
            {
                GameInfo info = allGames[i];
                this.combo_Games.Items.Add(info);
            }

            Panels = new List<Panel>();
            Players = new List<PlayerInfo>();
            Screens = new List<ScreenControl>();

            Screen[] screens = Screen.AllScreens;
            int MinX = 0;
            int MinY = 0;
            int factor = 10;
            for (int i = 0; i < screens.Length; i++)
            {
                Screen scr = screens[i];

                int width = scr.Bounds.Width;
                int height = scr.Bounds.Height;
                int X = scr.Bounds.X;
                int Y = scr.Bounds.Y;
                if (X < MinX)
                {
                    MinX += Math.Abs(X);
                }
                if (Y < MinY)
                {
                    MinY += Math.Abs(Y);
                }

                width /= factor;
                height /= factor;
                X /= factor;
                Y /= factor;

                ScreenControl scrPanel = new ScreenControl();
                Screens.Add(scrPanel);
                scrPanel.Size = new Size(width, height);
                scrPanel.Location = new Point(X, Y);
                scrPanel.SetName(scr.DeviceName.Remove(0, scr.DeviceName.Length - 1));
                this.panel1.Controls.Add(scrPanel);
            }

            for (int i = 0; i < Screens.Count; i++)
            {
                ScreenControl con = Screens[i];
                con.Location = new Point(con.Location.X + (MinX / factor), con.Location.Y + (MinY / factor));
            }

            num_Players_ValueChanged(null, null);
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Game Starter|*.exe";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    this.txt_File.Text = fileName;
                }
            }
        }

        private void num_Players_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Panels.Count; i++)
            {
                Panels[i].Dispose();
            }

            for (int i = 0; i < Players.Count; i++)
            {
                PlayerInfo p = Players[i];
                if (p.Control != null)
                {
                    p.Control.Dispose();
                }
            }
            Players.Clear();
            int players = (int) this.num_Players.Value;
            for (int i = 0; i < players; i++)
            {
                PlayerInfo p = new PlayerInfo();
                Players.Add(p);
            }

            for (int z = 0; z < Screens.Count; z++)
            {
                ScreenControl scr = Screens[z];

                ContextMenu context = new System.Windows.Forms.ContextMenu();
                for (int i = 0; i < players; i++)
                {
                    MenuItem player1 = new MenuItem("Set Player " + (i + 1).ToString(CultureInfo.InvariantCulture));
                    context.MenuItems.Add(player1);

                    for (int x = 0; x < 5; x++)
                    {
                        ScreenType screen = (ScreenType)x;

                        MenuItem SetPlayer = new MenuItem("To " + screen.ToString());
                        Holder holder = new Holder();
                        holder.Player = i;
                        holder.Control = scr;
                        holder.Screen = screen;
                        holder.ScreenIndex = z;
                        SetPlayer.Tag = holder;

                        SetPlayer.Click += new EventHandler(SetPlayer_Click);
                        player1.MenuItems.Add(SetPlayer);
                    }
                }

                if (scr.ContextMenu != null)
                {
                    scr.ContextMenu.Dispose();
                }

                scr.ContextMenu = context;
            }
        }

        void SetPlayer_Click(object sender, EventArgs e)
        {
            MenuItem send = (MenuItem)sender;
            Holder holder = (Holder)send.Tag;

            ScreenType scr = holder.Screen;

            PlayerControl panel = new PlayerControl();
            panel.Location = holder.Control.Location;
            panel.SetName("Player " + (holder.Player + 1).ToString());

            switch (scr)
            {
                case ScreenType.FullScreen:
                    {
                        panel.Size = holder.Control.Size;
                    }
                    break;
                case ScreenType.HalfHorizontalLeft:
                    {
                        panel.Size = new Size(holder.Control.Size.Width / 2, holder.Control.Size.Height);
                    }
                    break;
                case ScreenType.HalfHorizontalRight:
                    {
                        panel.Size = new Size(holder.Control.Size.Width / 2, holder.Control.Size.Height);
                        panel.Location = new Point(panel.Location.X + panel.Size.Width, panel.Location.Y);
                    }
                    break;
                case ScreenType.HalfVerticalBottom:
                    {
                        panel.Size = new Size(holder.Control.Size.Width, holder.Control.Size.Height / 2);
                        panel.Location = new Point(panel.Location.X, panel.Location.Y + panel.Size.Height);
                    }
                    break;
                case ScreenType.HalfVerticalTop:
                    {
                        panel.Size = new Size(holder.Control.Size.Width, holder.Control.Size.Height / 2);
                    }
                    break;
            }

            PlayerInfo player = this.Players[holder.Player];
            if (player.Control != null)
            {
                player.Control.Dispose();
            }
            player.Control = panel;
            player.ScreenType = scr;
            player.ScreenIndex = holder.ScreenIndex;

            panel.ContextMenu = send.GetContextMenu();

            this.panel1.Controls.Add(panel);
            panel.BringToFront();
        }

        string saveFile = "";
        private int biggerPlayer = -1;
        bool theresBigger;
        private void btn_Play_Click(object sender, EventArgs e)
        {
            string starter = this.txt_File.Text;
            if (string.IsNullOrEmpty(starter))
            {
                MessageBox.Show("Select a Borderlands 2 file first!");
                return;
            }

            if (string.IsNullOrEmpty(saveFile))
            {
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string myGames = Path.Combine(documents, @"My Games\Borderlands 2\WillowGame\Config");
                string willowEngine = Path.Combine(myGames, "WillowEngine.ini");

                if (File.Exists(willowEngine))
                {
                    saveFile = willowEngine;
                }
                else
                {
                    MessageBox.Show("Could not find WillowEngine.ini file!");

                    using (OpenFileDialog open = new OpenFileDialog())
                    {
                        open.Filter = "WillowEngine.ini file|WillowEngine.ini";
                        if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            saveFile = open.FileName;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            

            IniFile file = new IniFile(saveFile);
            file.IniWriteValue("SystemSettings", "WindowedFullscreen", "False");
            file.IniWriteValue("SystemSettings", "Fullscreen", "False");

            //[Engine.Engine]
            //bPauseOnLossOfFocus
            //bMuteAudioWhenNotInFocus
            file.IniWriteValue("Engine.Engine", "bMuteAudioWhenNotInFocus", "FALSE");
            file.IniWriteValue("Engine.Engine", "bPauseOnLossOfFocus", "FALSE");

            file.IniWriteValue("WillowGame.WillowGameEngine", "bPauseLostFocusWindowed", "FALSE");
            file.IniWriteValue("WillowGame.WillowGameEngine", "bMuteAudioWhenNotInFocus", "FALSE");

            Screen[] all =  Screen.AllScreens;

            Screen bigger = null;
            for (int i = 0; i < all.Length; i++)
            {
                Screen s = all[i];
                if (bigger == null)
                {
                    bigger = s;
                }
                else
                {
                    if (s.Bounds.Width > bigger.Bounds.Width)
                    {
                        bigger = s;
                    }

                    if (s.Bounds.Width != bigger.Bounds.Width)
                    {
                        theresBigger = true;
                    }
                }
            }

            for (int i = 0; i < Players.Count; i++)
            {
                PlayerInfo player = Players[i];

                // Set Borderlands 2 Resolution and stuff to run
                Screen screen = all[player.ScreenIndex];
                int Width = 0;
                int Height = 0;

                Point location = new Point();

                switch (player.ScreenType)
                {
                    case ScreenType.FullScreen:
                        {
                            Width = screen.Bounds.Width - 5;
                            Height = screen.Bounds.Height - 5;
                            location = new Point(screen.Bounds.X, screen.Bounds.Y);
                            if (theresBigger && screen == bigger ||
                                !theresBigger && this.check_WindowedFullscreen.Checked)
                            {
                                biggerPlayer = i;
                                file.IniWriteValue("SystemSettings", "WindowedFullscreen", "True");
                            }
                            else
                            {
                                file.IniWriteValue("SystemSettings", "WindowedFullscreen", "False");
                            }
                        }
                        break;
                    case ScreenType.HalfHorizontalLeft:
                        {
                            Width = screen.Bounds.Width / 2;
                            Height = screen.Bounds.Height;
                            location = new Point(screen.Bounds.X, screen.Bounds.Y);
                        }
                        break;
                    case ScreenType.HalfHorizontalRight:
                        {
                            Width = screen.Bounds.Width / 2;
                            Height = screen.Bounds.Height;
                            location = new Point(screen.Bounds.X + (screen.Bounds.Width / 2), screen.Bounds.Y);
                        }
                        break;
                    case ScreenType.HalfVerticalBottom:
                        {
                            Width = screen.Bounds.Width;
                            Height = screen.Bounds.Height / 2;
                            location = new Point(screen.Bounds.X, screen.Bounds.Y + (screen.Bounds.Height / 2));
                        }
                        break;
                    case ScreenType.HalfVerticalTop:
                        {
                            Width = screen.Bounds.Width;
                            Height = screen.Bounds.Height / 2;
                            location = new Point(screen.Bounds.X, screen.Bounds.Y);
                        }
                        break;
                }

                file.IniWriteValue("SystemSettings", "ResX", Width.ToString(CultureInfo.InvariantCulture));
                file.IniWriteValue("SystemSettings", "ResY", Height.ToString(CultureInfo.InvariantCulture));

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = starter;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (i == 0)
                {
                    if (check_Keyboard.Checked)
                    {
                        startInfo.Arguments = "-AlwaysFocus -ControllerOffset=3 -SaveDataId=1";
                    }
                    else
                    {
                        startInfo.Arguments = "-AlwaysFocus -SaveDataId=0";
                    }
                }
                else
                {
                    if (check_Keyboard.Checked)
                    {
                        startInfo.Arguments = "-AlwaysFocus -ControllerOffset=" + (i - 1).ToString(CultureInfo.InvariantCulture) + " -SaveDataId=" + (i + 1).ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        startInfo.Arguments = "-AlwaysFocus -ControllerOffset=" + i.ToString(CultureInfo.InvariantCulture) + " -SaveDataId=" + (i + 1).ToString(CultureInfo.InvariantCulture);
                    }
                }
                Process proc = Process.Start(startInfo);
                HwndObject hwnd = new HwndObject(proc.Handle);
                
                player.Location = location;
                player.HWND = hwnd;
                player.Process = proc;
                player.Size = new Size(Width, Height);

                int timeDelay = (int)this.delayUpDown.Value * 1000;
                Thread.Sleep(timeDelay);
            }

            running = true;
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);

            TitleHeight = screenRectangle.Top - this.Top;
            TitleWidth = screenRectangle.Left - this.Left;
        }
        private bool running = false;
        int TitleWidth;
        int TitleHeight;
        private int delay;

        private bool setted;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                delay += timer1.Interval;
                if (delay > (int)this.delayUpDown.Value * 1000)
                {
                    for (int i = 0; i < Players.Count; i++)
                    {
                        PlayerInfo p = Players[i];
                        HwndObject hwnd = new HwndObject(p.Process.MainWindowHandle);
                        if (theresBigger && biggerPlayer == i && p.ScreenType == ScreenType.FullScreen && this.check_WindowedFullscreen.Checked ||
                            !theresBigger && p.ScreenType == ScreenType.FullScreen && this.check_WindowedFullscreen.Checked)
                        {
                            hwnd.Location = p.Location;
                        }
                        else
                        {
                            hwnd.Location = new Point(p.Location.X - 3, p.Location.Y - TitleHeight);
                        }
                        hwnd.Title = "Borderlands 2 PC Co-Op";
                    }
                }
            }
        }

        private void gameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedGame = (GameInfo)combo_Games.SelectedItem;
            groupBox2.Enabled = SelectedGame != null;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

    }
}
