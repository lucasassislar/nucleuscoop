using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Gaming;

namespace SplitTool.Controls
{
    public partial class PlayerControl : UserControl
    {
        protected PlayerInfo player;
        public PlayerInfo Player
        {
            get { return player; }
        }
        public PlayerControl(PlayerInfo player)
        {
            this.player = player;
            InitializeComponent();
        }

        public void ChangeName(string text)
        {
            idLabel.Text = text;
        }

        public void Accomodate()
        {
            Rectangle parent = Parent.Bounds;
            switch (player.ScreenType)
            {
                case ScreenType.Fullscreen:
                    {
                        this.Bounds = new Rectangle(0, 0, parent.Width, parent.Height);
                    }
                    break;
                case ScreenType.HorizontalTop:
                    {
                        this.Bounds = new Rectangle(0, 0, parent.Width, parent.Height / 2);
                    }
                    break;
                case ScreenType.HorizontalBottom:
                    {
                        this.Bounds = new Rectangle(0, parent.Height / 2, parent.Width, parent.Height / 2);
                    }
                    break;
                case ScreenType.VerticalLeft:
                    {
                        this.Bounds = new Rectangle(0, 0, parent.Width / 2, parent.Height);
                    }
                    break;
                case ScreenType.VerticalRight:
                    {
                        this.Bounds = new Rectangle(parent.Width / 2, 0, parent.Width / 2, parent.Height);
                    }
                    break;
                case ScreenType.TopLeft:
                    {
                        this.Bounds = new Rectangle(0, 0, parent.Width / 2, parent.Height / 2);
                    }
                    break;
                case ScreenType.TopRight:
                    {
                        this.Bounds = new Rectangle(parent.Width / 2, 0, parent.Width / 2, parent.Height / 2);
                    }
                    break;
                case ScreenType.BottomLeft:
                    {
                        this.Bounds = new Rectangle(0, parent.Height / 2, parent.Width / 2, parent.Height / 2);
                    }
                    break;
                case ScreenType.BottomRight:
                    {
                        this.Bounds = new Rectangle(parent.Width / 2, parent.Height / 2, parent.Width / 2, parent.Height / 2);
                    }
                    break;
            }
        }

        private void PlayerControl_SizeChanged(object sender, EventArgs e)
        {
            // recalculate font's size
            Font font = idLabel.Font;
            int height = this.Height;
            int width = this.Width;
            int size = Math.Min(height, width);
            float fontSize = size * 0.20f;

            idLabel.Font = new Font(font.FontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
        }
    }
}
