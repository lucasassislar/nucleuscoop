using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    public class NButton : Button
    {
        protected Color defaultForeColor = Color.Black;
        protected Color disabledForeColor = Color.Black;
        public Color DefaultForeColor
        {
            get { return defaultForeColor; }
            set { defaultForeColor = value; }
        }
        public Color DisabledForeColor
        {
            get { return disabledForeColor; }
            set { disabledForeColor = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (base.Enabled)
            {
                base.OnPaint(pevent);
                base.ForeColor = defaultForeColor;
            }
            else
            {
                base.OnPaint(pevent);
                SizeF sf = pevent.Graphics.MeasureString(this.Text, this.Font, this.Width);
                Point ThePoint = new Point();
                ThePoint.X = (int)((this.Width / 2) - (sf.Width / 2));
                ThePoint.Y = (int)((this.Height / 2) - (sf.Height / 2));
                Brush brush = new SolidBrush(disabledForeColor);
                pevent.Graphics.DrawString(this.Text, this.Font, brush, ThePoint);
                brush.Dispose();
            }
        }
    }
}
