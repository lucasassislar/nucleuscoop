using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    public class ImageButton : UserControl
    {
        public Image Image { get; set; }

        public Button Button { get; private set; }

        public ImageButton()
        {
            Button = new Button();
            Button.FlatStyle = FlatStyle.Flat;

            Button.FlatAppearance.BorderColor = Color.FromArgb(255, 200, 200, 200);
            Button.FlatAppearance.BorderSize = 1;

            Button.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 0, 0, 0);
            Button.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 0, 0, 0);
            Button.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 0, 0, 0);

            Button.Dock = DockStyle.Fill;
            Button.BackColor = Color.FromArgb(0, 0, 0, 0);

            this.Controls.Add(Button);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            if (Image != null)
            {
                int borderSize = Button.FlatAppearance.BorderSize * 2;
                int borderSize2 = borderSize * 2;
                g.DrawImage(Image, new Rectangle(borderSize, borderSize, this.Width - borderSize2, this.Height - borderSize2), new Rectangle(Point.Empty, Image.Size), GraphicsUnit.Pixel);
            }
        }
    }
}
