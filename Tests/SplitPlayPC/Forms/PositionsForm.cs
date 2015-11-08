using SplitPlayPC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitPlayPC
{
    public partial class PositionsForm : BaseForm, IUserInputForm
    {
        public bool CanProceed
        {
            get
            {
                return canProceed;
            }
        }
        private bool canProceed;

        // array of users's screens
        private UserScreen[] screens;

        // the factor to scale all screens to match the edit area
        private float scale;

        // data for game configuration
        private GameConfig config;

        private Font playerFont;
        private Font playerTextFont;

        private RectangleF playersArea;

        public PositionsForm()
        {
            config = new GameConfig();//testcode

            InitializeComponent();

            playerFont = new Font("Segoe UI", 40);
            playerTextFont = new Font("Segoe UI", 18);

            RemoveFlicker();

            float playersWidth = this.Width * 0.5f;

            int playerCount = 4;
            float playerWidth = (playersWidth * 0.9f) / (float)playerCount;
            float playerHeight = playerWidth * 0.5625f;
            float offset = (playersWidth * 0.1f) / (float)playerCount;
            playersArea = new RectangleF(50, 100, playersWidth, playerHeight);


            for (int i = 0; i < playerCount; i++)
            {
                Rectangle r = new Rectangle((int)(50 + ((playerWidth + offset) * i)), 100, (int)playerWidth, (int)playerHeight);
                PlayerInfo playa = new PlayerInfo();
                playa.editBounds = r;
                config.Players.Add(playa);
            }

            screens = ScreensUtil.AllScreens();
            Rectangle totalBounds = RectangleUtil.Union(ScreensUtil.AllScreensRec());

            // see if most screens are either vertical or horizontal
            int vertical = 0;
            int horizontal = 0;
            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen s = screens[i];
                if (s.bounds.Width > s.bounds.Height)
                {
                    horizontal++;
                }
                else
                {
                    vertical++;
                }
            }


            if (horizontal > vertical)
            {
                // horizontal setup
                scale = (this.Width * 0.9f) / (float)totalBounds.Width;
            }
            else
            {
                // vertical setup
                scale = (this.Height * 0.6f) / (float)totalBounds.Height;
                //scale = (this.Width * 0.9f) / (float)totalBounds.Width;
            }

            totalBounds = new Rectangle(
                (int)(totalBounds.X * scale),
                (int)(totalBounds.Y * scale),
                (int)(totalBounds.Width * scale),
                (int)(totalBounds.Height * scale));
            int offsetViewsX = totalBounds.X;
            int offsetViewsY = totalBounds.Y;
            totalBounds = RectangleUtil.Center(totalBounds, new Rectangle(0, (int)(this.Height * 0.25f), this.Width, (int)(this.Height * 0.7f)));

            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen screen = screens[i];

                Rectangle s = screen.bounds;
                int width = (int)(s.Width * scale);
                int height = (int)(s.Height * scale);
                int x = (int)(s.X * scale);
                int y = (int)(s.Y * scale);
                screen.bounds = new Rectangle(x + totalBounds.X - offsetViewsX, y + totalBounds.Y - offsetViewsY, width, height);
                screen.swapTypeRect = new Rectangle(screen.bounds.X, screen.bounds.Y, (int)(screen.bounds.Width * 0.1f), (int)(screen.bounds.Width * 0.1f));
            }
        }

        private bool dragging = false;
        private int draggingIndex = -1;
        private Point draggingOffset;
        private Point mousePos;
        private int draggingScreen = -1;
        private Rectangle draggingScreenRec;
        private Rectangle draggingScreenBounds;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mousePos = e.Location;

            if (dragging)
            {
                var players = config.Players;

                PlayerInfo player = players[draggingIndex];
                Rectangle p = player.editBounds;
                if (draggingScreen == -1)
                {
                    for (int i = 0; i < screens.Length; i++)
                    {
                        UserScreen screen = screens[i];
                        Rectangle s = screen.bounds;
                        float pc = RectangleUtil.PcInside(p, s);

                        // bigger than 60% = major part inside this screen
                        if (pc > 0.6f)
                        {
                            float offset = s.Width * 0.05f;

                            // check if there's space available on this screen
                            var playas = config.Players;
                            Rectangle? editor;
                            Rectangle? monitor;
                            GetFreeSpace(i, out editor, out monitor);

                            if (editor != null)
                            {
                                draggingScreenRec = editor.Value;
                                draggingScreenBounds = monitor.Value;
                                draggingScreen = i;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    Rectangle s = screens[draggingScreen].bounds;
                    float pc = RectangleUtil.PcInside(p, s);
                    if (pc < 0.6f)
                    {
                        draggingScreen = -1;
                    }
                }

                p = new Rectangle(mousePos.X + draggingOffset.X, mousePos.Y + draggingOffset.Y, p.Width, p.Height);
                players[draggingIndex].editBounds = p;

                Invalidate();
            }
        }

        private void GetFreeSpace(int screenIndex, out Rectangle? editorBounds, out Rectangle? monitorBounds)
        {
            editorBounds = null;
            monitorBounds = null;

            var players = config.Players;
            UserScreen screen = screens[screenIndex];
            Rectangle bounds = screen.monitorBounds;
            Rectangle ebounds = screen.bounds;

            switch (screen.type)
            {
                case UserScreenType.FullScreen:
                    for (int i = 0; i < players.Count; i++)
                    {
                        PlayerInfo p = players[i];
                        if (p.screenIndex == screenIndex)
                        {
                            return;
                        }
                    }

                    monitorBounds = screen.monitorBounds;
                    editorBounds = screen.bounds;
                    break;
                case UserScreenType.DualHorizontal:
                    {
                        int playersUsing = 0;
                        Rectangle areaUsed = new Rectangle();

                        for (int i = 0; i < players.Count; i++)
                        {
                            PlayerInfo p = players[i];
                            if (p.screenIndex == screenIndex)
                            {
                                playersUsing++;
                                areaUsed = Rectangle.Union(areaUsed, p.monitorBounds);
                            }
                        }

                        if (playersUsing == 2)
                        {
                            return;
                        }

                        int half = (int)(bounds.Height / 2.0f);

                        for (int i = 0; i < 2; i++)
                        {
                            Rectangle area = new Rectangle(bounds.X, bounds.Y + (half * i), bounds.Width, half);
                            if (!areaUsed.Contains(area))
                            {
                                monitorBounds = area;

                                int halfe = (int)(ebounds.Height / 2.0f);
                                editorBounds = new Rectangle(ebounds.X, ebounds.Y + (halfe * i), ebounds.Width, halfe);
                                return;
                            }
                        }
                    }
                    break;
                case UserScreenType.DualVertical:
                    {
                        int playersUsing = 0;
                        Rectangle areaUsed = new Rectangle();

                        for (int i = 0; i < players.Count; i++)
                        {
                            PlayerInfo p = players[i];
                            if (p.screenIndex == screenIndex)
                            {
                                playersUsing++;
                                areaUsed = Rectangle.Union(areaUsed, p.monitorBounds);
                            }
                        }

                        if (playersUsing == 2)
                        {
                            return;
                        }

                        int half = (int)(bounds.Width / 2.0f);

                        for (int i = 0; i < 2; i++)
                        {
                            Rectangle area = new Rectangle(bounds.X + (half * i), bounds.Y, half, bounds.Height);
                            if (!areaUsed.Contains(area))
                            {
                                monitorBounds = area;
                                int halfe = (int)(ebounds.Width / 2.0f);
                                editorBounds = new Rectangle(ebounds.X + (halfe * i), ebounds.Y, halfe, ebounds.Height);
                                return;
                            }
                        }
                    }
                    break;
                case UserScreenType.FourPlayers:
                    {
                        int playersUsing = 0;
                        Rectangle areaUsed = new Rectangle();

                        for (int i = 0; i < players.Count; i++)
                        {
                            PlayerInfo p = players[i];
                            if (p.screenIndex == screenIndex)
                            {
                                playersUsing++;
                                areaUsed = Rectangle.Union(areaUsed, p.monitorBounds);
                            }
                        }

                        if (playersUsing == 4)
                        {
                            return;
                        }

                        int halfw = (int)(bounds.Width / 2.0f);
                        int halfh = (int)(bounds.Height / 2.0f);

                        for (int x = 0; x < 2; x++)
                        {
                            for (int y = 0; y < 2; y++)
                            {
                                Rectangle area = new Rectangle(bounds.X + (halfw * x), bounds.Y + (halfh * y), halfw, halfh);

                                bool goNext = false;
                                // check if there's any player with the area's x,y coord
                                for (int i = 0; i < players.Count; i++)
                                {
                                    PlayerInfo p = players[i];
                                    if (p.screenIndex == screenIndex)
                                    {
                                        if (p.monitorBounds.X == area.X &&
                                            p.monitorBounds.Y == area.Y)
                                        {
                                            goNext = true;
                                            break;
                                        }
                                    }
                                }

                                if (goNext)
                                {
                                    continue;
                                }
                                monitorBounds = area;
                                int halfwe = (int)(ebounds.Width / 2.0f);
                                int halfhe = (int)(ebounds.Height / 2.0f);
                                editorBounds = new Rectangle(ebounds.X + (halfwe * x), ebounds.Y + (halfhe * y), halfwe, halfhe);
                                return;
                            }
                        }
                    }
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            var players = config.Players;

            if (e.Button == MouseButtons.Left)
            {
                if (dragging)
                {
                }
                else
                {
                    for (int i = 0; i < screens.Length; i++)
                    {
                        UserScreen screen = screens[i];
                        if (screen.swapTypeRect.Contains(e.Location))
                        {
                            if (screen.type == UserScreenType.FourPlayers)
                            {
                                screen.type = 0;
                            }
                            else
                            {
                                screen.type++;
                            }

                            // invalidate all players inside screen
                            for (int j = 0; j < players.Count; j++)
                            {
                                // return to default position
                                PlayerInfo p = players[j];
                                if (p.screenIndex == i)
                                {
                                    p.editBounds = getDefaultBounds(j);
                                    p.screenIndex = -1;
                                }
                            }

                            Invalidate();
                            return;
                        }
                    }

                    for (int i = 0; i < players.Count; i++)
                    {
                        Rectangle r = players[i].editBounds;
                        if (r.Contains(e.Location))
                        {
                            dragging = true;
                            draggingIndex = i;
                            draggingOffset = new Point(r.X - e.X, r.Y - e.Y);
                            Rectangle newBounds = getDefaultBounds(draggingIndex);
                            config.Players[draggingIndex].editBounds = newBounds;

                            if (draggingOffset.X < -newBounds.Width ||
                                draggingOffset.Y < -newBounds.Height)
                            {
                                draggingOffset = new Point(0, 0);
                            }

                            break;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right ||
                     e.Button == MouseButtons.Middle)
            {
                // if over a player on a screen, change the type
                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo p = players[i];
                    Rectangle r = p.editBounds;
                    if (r.Contains(e.Location))
                    {
                        if (p.screenIndex != -1)
                        {
                            UserScreen screen = screens[p.screenIndex];
                            int halfWidth = screen.monitorBounds.Width / 2;
                            int halfHeight = screen.monitorBounds.Height / 2;

                            Rectangle bounds = p.monitorBounds;
                            if (screen.type == UserScreenType.FourPlayers)
                            {
                                // check if the size is 1/4th of screen
                                if (bounds.Width == halfWidth &&
                                    bounds.Height == halfHeight)
                                {
                                    bool hasLeftRightSpace = true;
                                    bool hasTopBottomSpace = true;

                                    // check if we have something left/right or top/bottom
                                    for (int j = 0; j < players.Count; j++)
                                    {
                                        if (i == j)
                                        {
                                            continue;
                                        }

                                        PlayerInfo other = players[j];
                                        if (other.screenIndex != p.screenIndex)
                                        {
                                            continue;
                                        }

                                        if (other.monitorBounds.Y == p.monitorBounds.Y)
                                        {
                                            hasLeftRightSpace = false;
                                        }
                                        if (other.monitorBounds.X == p.monitorBounds.X)
                                        {
                                            hasTopBottomSpace = false;
                                        }

                                        if (other.monitorBounds.X == screen.monitorBounds.X + halfWidth &&
                                            other.monitorBounds.Height == screen.monitorBounds.Height)
                                        {
                                            hasLeftRightSpace = false;
                                        }
                                        if (other.monitorBounds.X == screen.monitorBounds.X &&
                                            other.monitorBounds.Width == screen.monitorBounds.Width)
                                        {
                                            hasTopBottomSpace = false;
                                        }
                                    }

                                    if (hasLeftRightSpace)
                                    {
                                        Rectangle edit = p.editBounds;
                                        if (bounds.X == screen.monitorBounds.X + bounds.Width)
                                        {
                                            bounds.X -= bounds.Width;
                                            edit.X -= edit.Width;
                                        }

                                        bounds.Width *= 2;
                                        edit.Width *= 2;

                                        p.editBounds = edit;
                                        p.monitorBounds = bounds;


                                        Invalidate();
                                    }
                                    else if (hasTopBottomSpace)
                                    {
                                        bounds.Height *= 2;
                                        p.monitorBounds = bounds;
                                        Rectangle edit = p.editBounds;
                                        edit.Height *= 2;
                                        p.editBounds = edit;

                                        Invalidate();
                                    }
                                }
                                else
                                {
                                    bounds.Width = screen.monitorBounds.Width / 2;
                                    bounds.Height = screen.monitorBounds.Height / 2;
                                    p.monitorBounds = bounds;

                                    Rectangle edit = p.editBounds;
                                    edit.Width = screen.bounds.Width / 2;
                                    edit.Height = screen.bounds.Height / 2;
                                    p.editBounds = edit;

                                    Invalidate();
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                if (dragging)
                {
                    PlayerInfo p = config.Players[draggingIndex];
                    dragging = false;

                    if (draggingScreen != -1)
                    {
                        p.screenIndex = draggingScreen;
                        p.monitorBounds = draggingScreenBounds;
                        p.editBounds = draggingScreenRec;

                        draggingScreen = -1;
                    }
                    else
                    {
                        // return to default position
                        p.editBounds = getDefaultBounds(draggingIndex);
                        p.screenIndex = -1;
                    }


                    bool allReady = true;
                    for (int i = 0; i < config.Players.Count; i++)
                    {
                        PlayerInfo player = config.Players[i];
                        if (player.screenIndex == -1)
                        {
                            allReady = false;
                            canProceed = false;
                            break;
                        }
                    }

                    if (allReady)
                    {
                        canProceed = true;
                    }
                    

                    Invalidate();
                }
            }
        }

        private Rectangle getDefaultBounds(int index)
        {
            float playersWidth = this.Width * 0.5f;
            float playerWidth = (playersWidth * 0.9f) / (float)config.Players.Count;
            float playerHeight = playerWidth * 0.5625f;
            float offset = (playersWidth * 0.1f) / (float)config.Players.Count;
            return new Rectangle((int)(50 + ((playerWidth + offset) * index)), 100, (int)playerWidth, (int)playerHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen s = screens[i];
                g.DrawRectangle(Pens.White, s.bounds);
                g.DrawRectangle(Pens.White, s.swapTypeRect);


                switch (s.type)
                {
                    case UserScreenType.FullScreen:
                        g.DrawImage(Resources.fullscreen, s.swapTypeRect);
                        break;
                    case UserScreenType.DualHorizontal:
                        g.DrawImage(Resources.horizontal, s.swapTypeRect);
                        break;
                    case UserScreenType.DualVertical:
                        g.DrawImage(Resources.vertical, s.swapTypeRect);
                        break;
                    case UserScreenType.FourPlayers:
                        g.DrawImage(Resources._4players, s.swapTypeRect);
                        break;
                }
            }

            var players = config.Players;
            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo info = players[i];
                Rectangle s = info.editBounds;

                if (info.screenIndex == -1)
                {
                    g.DrawRectangle(Pens.White, s);
                }
                else
                {
                    g.DrawRectangle(Pens.Green, s);
                }

                string str = (i + 1).ToString();
                SizeF size = g.MeasureString(str, playerFont);
                PointF loc = RectangleUtil.Center(size, s);
                g.DrawString((i + 1).ToString(), playerFont, Brushes.White, loc);
            }

            if (dragging && draggingScreen != -1)
            {
                g.DrawRectangle(Pens.Red, draggingScreenRec);
            }

            g.DrawString("Drag each player to\ntheir respective screen", playerTextFont, Brushes.White, new PointF(470, 100));
            g.DrawString("Players", playerTextFont, Brushes.White, new PointF(50, 50));

            g.DrawString("Right click player to change size", playerTextFont, Brushes.White, new PointF(20, 450));
            g.DrawString("Click on screen's top-left corner to change players on that screen", playerTextFont, Brushes.White, new PointF(20, 490));
            //g.DrawRectangle(Pens.Red, playersArea.X, playersArea.Y, playersArea.Width, playersArea.Height);
        }
    }
}
