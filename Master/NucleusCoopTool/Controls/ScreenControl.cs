using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitTool.Controls
{
    public partial class ScreenControl : UserControl
    {
        public ScreenControl()
        {
            InitializeComponent();
        }

        public void ChangeID(string text)
        {
            idLabel.Text = text;
        }

        public void ChangeName(string text)
        {
            label_name.Text = text;
        }

        public void Highlight()
        {
            this.BackColor = Color.FromArgb(25, 25, 25);
        }
        public void Darken()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
        }

        private void ScreenControl_SizeChanged(object sender, EventArgs e)
        {
            // recalculate font's size
            Font font = idLabel.Font;
            int height = this.Height;
            int width = this.Width;
            int size = Math.Min(height, width);
            float fontSize = size * 0.7f;

            if (fontSize > 0)
            {
                idLabel.Font = new Font(font.FontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            }

            int height2 = height / 2;
            int width2 = width / 2;

            idLabel.Location = new Point(width2 - idLabel.Width / 2, height2 - idLabel.Height / 2);
            label_name.Location = new Point(width2 - label_name.Width / 2, this.Height - label_name.Height);
        }
    }
}
