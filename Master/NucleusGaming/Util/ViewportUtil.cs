//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace Nucleus.Gaming
//{
//    public static class ViewportUtil
//    {
//        public static readonly Dictionary<ScreenType, ScreenType> Complementar = new Dictionary<ScreenType, ScreenType>()
//        {
//            {ScreenType.HorizontalTop, ScreenType.HorizontalBottom},
//            {ScreenType.HorizontalBottom, ScreenType.HorizontalTop},
//            {ScreenType.VerticalLeft, ScreenType.VerticalRight},
//            {ScreenType.VerticalRight, ScreenType.VerticalLeft}
//        };

//        /// <summary>
//        /// Method useful for games that run 2 players in 1 instance
//        /// </summary>
//        public static List<DuetPlayerInfo> GetPlayerDuets(List<PlayerInfo> players)
//        {
//            List<DuetPlayerInfo> newPlayers = new List<DuetPlayerInfo>();
//            List<int> analyzed = new List<int>();

//            Screen[] allScreens = Screen.AllScreens;


//            for (int i = 0; i < players.Count; i++)
//            {
//                PlayerInfo p = players[i];
//                if (analyzed.Contains (i))
//                {
//                    continue;
//                }
//                analyzed.Add(i);

//                Screen s = allScreens[p.ScreenIndex];

//                int width = s.Bounds.Width;
//                int height = s.Bounds.Height;
//                int x = s.Bounds.X;
//                int y = s.Bounds.Y;

//                PlayerInfo other = null;

//                bool multiMonitor = false;

//                for (int j = 1; j < players.Count; j++)
//                {
//                    PlayerInfo a = players[j];
//                    if (analyzed.Contains(j))
//                    {
//                        continue;
//                    }

//                    ScreenType comp = Complementar[a.ScreenType];
//                    if (a.ScreenType == comp)
//                    {
//                        other = a;
//                        break;
//                    }
//                    else if (p.ScreenType == ScreenType.Fullscreen &&
//                             a.ScreenType == ScreenType.Fullscreen)
//                    {
//                        // check if they're next to each other
//                        if (p.ScreenIndex == a.ScreenIndex -1 ||
//                            p.ScreenIndex == a.ScreenIndex + 1)// abs should work better but whateva
//                        {
//                            other = a;
//                            multiMonitor = true;

//                            s = allScreens[other.ScreenIndex];
//                            width += s.Bounds.Width;
//                            height += s.Bounds.Height;
//                            x = Math.Min(x, s.Bounds.X);
//                            y = Math.Min(y, s.Bounds.Y);
//                            break;
//                        }
//                    }

//                }

//                DuetPlayerInfo duet = new DuetPlayerInfo();
//                duet.PlayerInfo1 = p;
//                duet.IsMultiMonitor = multiMonitor;
//                newPlayers.Add(duet);
//                duet.TotalBounds = new Rectangle(x, y, width, height);
//                if (other != null)
//                {
//                    duet.PlayerInfo2 = other;
//                    analyzed.Add(players.IndexOf(other));
//                }
//            }

//            return newPlayers;
//        }


//        public static void GetPlayerViewport(PlayerInfo player, int titleHeight, out int width, out int height, out Point location)
//        {
//            width = -1;
//            height = -1;
//            location = new Point();

//            Screen[] all = Screen.AllScreens;
//            Screen first = all[0];
//            Rectangle fbounds = first.Bounds;

//            Screen screen = all[player.ScreenIndex];
//            Rectangle bounds = screen.Bounds;

//            switch (player.ScreenType)
//            {
//                case ScreenType.Fullscreen:
//                    {
//                        width = bounds.Width;
//                        height = bounds.Height;
//                        location = new Point(screen.Bounds.X, screen.Bounds.Y);
//                    }
//                    break;
//                case ScreenType.HorizontalTop:
//                    {
//                        width = screen.Bounds.Width;
//                        height = (screen.Bounds.Height / 2) - titleHeight;
//                        location = new Point(screen.Bounds.X, screen.Bounds.Y);
//                    }
//                    break;
//                case ScreenType.HorizontalBottom:
//                    {
//                        width = screen.Bounds.Width;
//                        height = (screen.Bounds.Height / 2) - titleHeight;
//                        location = new Point(screen.Bounds.X, screen.Bounds.Y + (screen.Bounds.Height / 2));
//                    }
//                    break;
//                case ScreenType.VerticalLeft:
//                    {
//                        width = screen.Bounds.Width / 2;
//                        height = screen.Bounds.Height;
//                        location = new Point(screen.Bounds.X, screen.Bounds.Y);
//                    }
//                    break;
//                case ScreenType.VerticalRight:
//                    {
//                        width = screen.Bounds.Width / 2;
//                        height = screen.Bounds.Height;
//                        location = new Point(screen.Bounds.X + (screen.Bounds.Width / 2), screen.Bounds.Y);
//                    }
//                    break;
//                case ScreenType.TopLeft:
//                    {
//                        width = screen.Bounds.Width / 2;
//                        height = screen.Bounds.Height / 2;
//                        location = new Point(screen.Bounds.X, screen.Bounds.Y);
//                    }
//                    break;
//                case ScreenType.TopRight:
//                    {
//                        width = screen.Bounds.Width / 2;
//                        height = screen.Bounds.Height / 2;
//                        location = new Point(screen.Bounds.X + width, screen.Bounds.Y);
//                    }
//                    break;
//                case ScreenType.BottomLeft:
//                    {
//                        width = screen.Bounds.Width / 2;
//                        height = screen.Bounds.Height / 2;
//                        location = new Point(screen.Bounds.X, screen.Bounds.Y + height);
//                    }
//                    break;
//                case ScreenType.BottomRight:
//                    {
//                        width = screen.Bounds.Width / 2;
//                        height = screen.Bounds.Height / 2;
//                        location = new Point(screen.Bounds.X + width, screen.Bounds.Y + height);
//                    }
//                    break;
//            }

//        }
//    }
//}
