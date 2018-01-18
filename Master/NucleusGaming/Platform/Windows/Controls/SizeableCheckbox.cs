using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    // From http://stackoverflow.com/questions/3166244/how-to-increase-the-size-of-checkbox-in-winforms
    public class SizeableCheckbox : CheckBox
    {
        public SizeableCheckbox()
        {
            this.TextAlign = ContentAlignment.MiddleRight;
        }
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = false; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int h = this.ClientSize.Height - 2;
            Rectangle rc = new Rectangle(new Point(0, 1), new Size(h, h));
            ControlPaint.DrawCheckBox(e.Graphics, rc,
                this.Checked ? ButtonState.Checked : ButtonState.Normal);
        }
    }
}