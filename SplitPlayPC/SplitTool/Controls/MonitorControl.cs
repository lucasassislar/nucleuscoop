using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using System.Runtime.InteropServices;
using System.Management;

namespace SplitTool.Controls
{
    public partial class MonitorControl : UserControl, ICanProceed
    {
        /// <summary>
        /// The screen the user is currently with the mouse over
        /// </summary>
        private ScreenControl hover;

        private int playerCount;
        private GameInfo info;

        private List<ScreenControl> loaded_screens;
        private ContextMenuStrip screenStrip;
        private List<PlayerInfo> players;

        private bool reset = false;
        private UserGameInfo userGameInfo;
        private Label posLabel;

        public List<PlayerInfo> Players
        {
            get { return players; }
        }

        public MonitorControl()
        {
            InitializeComponent();

            loaded_screens = new List<ScreenControl>();
            players = new List<PlayerInfo>();

            MakeContextStrip();
            MakeMonitors();
        }

        public void Restart()
        {
            reset = true;
        }

        public void UpdatePlayerCount(int player, GameInfo info, UserGameInfo userGame)
        {
            this.info = info;
            this.userGameInfo = userGame;
            if (info == null)
            {
                return;
            }

            if (player != playerCount || reset)
            {
                // Clean players!
                playerCount = player;
                players.Clear();
                for (int i = 0; i < loaded_screens.Count; i++)
                {
                    var mscreen = loaded_screens[i];
                    for (int j = 0; j < mscreen.Controls.Count; j++)
                    {
                        Control con = mscreen.Controls[j];

                        if (con is PlayerControl)
                        {
                            mscreen.Controls.Remove(con);
                            j--;
                        }
                    }
                }

                // remake monitors
                MakeContextStrip();
                MakeMonitors();
            }

            for (int i = 0; i < screenStrip.Items.Count; i++)
            {
                var item = screenStrip.Items[i];
                item.Enabled = i < player;
            }

            if (reset)
            {
                if (info.NeedPositioning)
                {
                    if (posLabel != null && panel1.Controls.Contains(posLabel))
                    {
                        panel1.Controls.Remove(posLabel);
                    }
                }
                else
                {
                    panel1.Controls.Clear();

                    posLabel = new Label();
                    posLabel.Text = "The selected game does not support positioning";
                    posLabel.Width = 500;
                    posLabel.Font = this.Font;

                    panel1.Controls.Add(posLabel);
                }
            }
            reset = false;
        }

        protected void MakeContextStrip()
        {
            screenStrip = new ContextMenuStrip();
            for (int i = 0; i < playerCount; i++)
            {
                int player = i + 1;

                ToolStripMenuItem item = new ToolStripMenuItem("Player " + player);
                var fullscreen = item.DropDownItems.Add("Fullscreen");
                fullscreen.Click += ScreenClick;

                ToolStripMenuItem horizontal = new ToolStripMenuItem("Horizontal");
                var halfTop = horizontal.DropDownItems.Add("Half Top");
                var halfBottom = horizontal.DropDownItems.Add("Half Bottom");
                halfTop.Click += ScreenClick;
                halfBottom.Click += ScreenClick;
                item.DropDownItems.Add(horizontal);

                ToolStripMenuItem vertical = new ToolStripMenuItem("Vertical");
                var halfLeft = vertical.DropDownItems.Add("Half Left");
                var halfRight = vertical.DropDownItems.Add("Half Right");
                halfLeft.Click += ScreenClick;
                halfRight.Click += ScreenClick;
                item.DropDownItems.Add(vertical);

                ToolStripMenuItem fourPlayers = new ToolStripMenuItem("3+ Players on 1 Screen");
                var topLeft = fourPlayers.DropDownItems.Add("Top Left");
                var topRight = fourPlayers.DropDownItems.Add("Top Right");
                var bottomLeft = fourPlayers.DropDownItems.Add("Bottom Left");
                var bottomRight = fourPlayers.DropDownItems.Add("Bottom Right");
                topLeft.Click += ScreenClick;
                topRight.Click += ScreenClick;
                bottomLeft.Click += ScreenClick;
                bottomRight.Click += ScreenClick;
                item.DropDownItems.Add(fourPlayers);

                ScreenHolder holda = new ScreenHolder(ScreenType.Fullscreen, player);
                fullscreen.Tag = holda;

                holda = new ScreenHolder(ScreenType.HorizontalTop, player);
                halfTop.Tag = holda;
                holda = new ScreenHolder(ScreenType.HorizontalBottom, player);
                halfBottom.Tag = holda;
                holda = new ScreenHolder(ScreenType.VerticalLeft, player);
                halfLeft.Tag = holda;
                holda = new ScreenHolder(ScreenType.VerticalRight, player);
                halfRight.Tag = holda;

                holda = new ScreenHolder(ScreenType.TopLeft, player);
                topLeft.Tag = holda;
                holda = new ScreenHolder(ScreenType.TopRight, player);
                topRight.Tag = holda;
                holda = new ScreenHolder(ScreenType.BottomLeft, player);
                bottomLeft.Tag = holda;
                holda = new ScreenHolder(ScreenType.BottomRight, player);
                bottomRight.Tag = holda;

                screenStrip.Items.Add(item);
            }
        }
        protected void ScreenClick(object sender, EventArgs e)
        {
            if (hover == null)
            {
                return;
            }
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            ScreenHolder holda = (ScreenHolder)item.Tag;

            for (int i = 0; i < loaded_screens.Count; i++)
            {
                var mscreen = loaded_screens[i];
                for (int j = 0; j < mscreen.Controls.Count; j++)
                {
                    Control con = mscreen.Controls[j];

                    if (con is PlayerControl)
                    {
                        PlayerControl player = (PlayerControl)con;
                        if (player.Player.Player == holda.Player)
                        {
                            player.Dispose();
                            hover.Controls.Remove(player);
                            players.Remove(player.Player);
                        }
                    }
                }
            }

            for (int i = 0; i < hover.Controls.Count; i++)
            {
                Control con = hover.Controls[i];

                if (con is PlayerControl)
                {
                    PlayerControl player = (PlayerControl)con;
                    PlayerInfo otha = player.Player;

                    // if one coincides with the other, remove the other one
                    if (otha.ScreenType == ScreenType.Fullscreen ||
                        otha.ScreenType == holda.Position ||
                        holda.Position == ScreenType.HorizontalTop &&
                        (otha.ScreenType == ScreenType.TopLeft || otha.ScreenType == ScreenType.TopRight || otha.ScreenType == ScreenType.VerticalLeft || otha.ScreenType == ScreenType.VerticalRight) ||
                        holda.Position == ScreenType.HorizontalBottom &&
                        (otha.ScreenType == ScreenType.BottomLeft || otha.ScreenType == ScreenType.BottomRight || otha.ScreenType == ScreenType.VerticalLeft || otha.ScreenType == ScreenType.VerticalRight) ||
                        holda.Position == ScreenType.VerticalLeft &&
                        (otha.ScreenType == ScreenType.TopLeft || otha.ScreenType == ScreenType.BottomLeft || otha.ScreenType == ScreenType.HorizontalTop || otha.ScreenType == ScreenType.HorizontalBottom) ||
                        holda.Position == ScreenType.VerticalRight &&
                        (otha.ScreenType == ScreenType.TopRight || otha.ScreenType == ScreenType.BottomRight || otha.ScreenType == ScreenType.HorizontalTop || otha.ScreenType == ScreenType.HorizontalBottom))
                    {
                        // Love boolean Math XD
                        player.Dispose();
                        hover.Controls.Remove(player);
                        players.Remove(otha);
                    }
                }
            }

            PlayerInfo playa = new PlayerInfo();
            playa.ScreenIndex = this.loaded_screens.IndexOf(hover);
            playa.ScreenType = holda.Position;
            playa.Player = holda.Player;

            PlayerControl theplayer = new PlayerControl(playa);
            theplayer.MouseEnter += scr_MouseEnter;
            theplayer.ChangeName("Player " + holda.Player);
            hover.Controls.Add(theplayer);
            theplayer.BringToFront();
            theplayer.Accomodate();
            players.Add(playa);
        }

        protected virtual void MakeMonitors()
        {
            panel1.Controls.Clear();
            loaded_screens.Clear();

            Screen[] screens = Screen.AllScreens;

            // get screens
            int minX = 0;
            int minY = 0;
            int maxWidth = 0;
            int maxHeight = 0;
            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];
                var bounds = screen.Bounds;

                minX = Math.Min(minX, bounds.X);
                minY = Math.Min(minY, bounds.Y);
                maxWidth += bounds.Width;
                maxHeight += bounds.Height;
            }

            int width = this.Width;
            int height = this.Height;
            int maxSize = Math.Max(maxWidth, maxHeight);
            int size = Math.Max(width, height);
            float factor = maxSize / (float)size;
            if (screens.Length == 1)
            {
                factor *= 1.3f;
            }

            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];
                var bounds = screen.Bounds;

                int swidth = bounds.Width;
                int sheight = bounds.Height;
                int x = bounds.X  + Math.Abs(minX);
                int y = bounds.Y;

                swidth = (int)(swidth / factor);
                sheight = (int)(sheight / factor);
                x = (int)(x / factor);
                y = (int)(y / factor);

                ScreenControl scr = new ScreenControl();
                scr.MouseEnter += scr_MouseEnter;
                scr.ContextMenuStrip = screenStrip;
                scr.Size = new Size(swidth, sheight);
                scr.Location = new Point(x, y);
                scr.ChangeID(screen.DeviceName.Remove(0, screen.DeviceName.Length - 1));
                
                scr.ChangeName(screen.DeviceName);
                scr.Tag = screen;
                panel1.Controls.Add(scr);
                loaded_screens.Add(scr);
            }
        }

        protected void scr_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PlayerControl)
            {
                PlayerControl player = (PlayerControl)sender;
                hover = (ScreenControl)player.Parent;
            }
            else
            {
                hover = (ScreenControl)sender;
            }

            for (int i = 0; i < loaded_screens.Count; i++)
            {
                var screen = loaded_screens[i];
                if (screen == hover)
                {
                    screen.Highlight();
                }
                else
                {
                    screen.Darken();
                }
            }
        }

        public bool CanProceed
        {
            get { return (playerCount != 0 && playerCount == players.Count) || (info != null && !info.NeedPositioning); }
        }

        public string StepTitle
        {
            get { return "Monitor Management"; }
        }
        public bool AutoProceed
        {
            get { return false; }
        }
        public void AutoProceeded()
        {
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
           
        }

       
    }
}
