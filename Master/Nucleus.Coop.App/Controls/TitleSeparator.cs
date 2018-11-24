using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls
{
    public partial class TitleSeparator : UserControl
    {
        private Label label;
        private Font titleFont;

        public new string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public TitleSeparator()
        {
            InitializeComponent();

            this.BackColor = Color.Transparent;
            this.Height = 30;

            titleFont = new Font(this.Font.FontFamily, 10, FontStyle.Regular);

            label = new Label();
            label.ForeColor = Color.FromArgb(255, 104, 104, 104);
            label.Font = titleFont;
            label.Text = "GAMES";
            label.Location = new Point(10, 6);
            this.Controls.Add(label);
        }

        public void SetTitle(string title) {
            label.Text = title;
        }
    }
}
