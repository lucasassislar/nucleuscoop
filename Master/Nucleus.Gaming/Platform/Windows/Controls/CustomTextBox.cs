using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    public class CustomTextBox : TextBox
    {
        private Color borderColor;
        private Pen borderPen;

        public int BorderSize { get; set; } = 2;

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    if (borderPen != null)
                    {
                        borderPen.Dispose();
                    }
                    borderPen = new Pen(value);
                }
                borderColor = value;
            }
        }

        public CustomTextBox()
        {
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;

            BorderColor = Color.Black;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //User32Interop.SetWindowRgn(this.Handle, Gdi32Interop.CreateRoundRectRgn(0, 0, this.Width - 5, this.Height, 5, 5), true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Point loc = this.Location;
            g.DrawRectangle(borderPen, new Rectangle(loc.X - BorderSize, loc.Y - BorderSize, this.Width + BorderSize, this.Height + BorderSize));

            base.OnPaint(e);
        }
    }
}
