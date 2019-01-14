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
using Nucleus.Gaming.Platform.Windows.Controls;

namespace Nucleus.Gaming.Windows.Controls
{
    public class CoolListControl : UserControl, IRadioControl
    {
        private Label titleLabel;
        protected Label descLabel;

        protected int defaultHeight = 72;
        protected int expandedHeight = 156;

        public Font TitleFont
        {
            get { return titleLabel.Font; }
            set { titleLabel.Font = value; }
        }
        public Font DetailsFont
        {
            get { return descLabel.Font; }
            set { descLabel.Font = value; }
        }

        public string Title
        {
            get { return titleLabel.Text; }
            set { this.titleLabel.Text = value; }
        }

        public string Details
        {
            get { return descLabel.Text; }
            set { descLabel.Text = value; }
        }

        public bool EnableHighlighting { get; private set; }
        public bool EnableClicking { get; set; } = true;
        public object Data { get; set; }
        public event Action<object> OnSelected;

        public Color ColorSelected { get; set; } = Color.FromArgb(66, 70, 77);
        public Color ColorUnselected { get; set; } = Color.FromArgb(0, 0, 0, 0);//Color.FromArgb(47, 49, 54);
        public Color ColorMouseOver { get; set; } = Color.FromArgb(47, 49, 54);

        public CoolListControl(bool enableHightlighting)
        {
            EnableHighlighting = enableHightlighting;
            //this.BorderStyle = BorderStyle.FixedSingle;

            Size = new Size(400, 120);
            BackColor = ColorUnselected;

            titleLabel = new Label();
            titleLabel.Location = new Point(10, 10);
            titleLabel.AutoSize = true;
            Controls.Add(titleLabel);

            descLabel = new Label();
            descLabel.Location = new Point(10, 50);
            descLabel.AutoSize = true;
            Controls.Add(descLabel);
        }

        public void RadioSelected() {
            BackColor = ColorSelected;
            if (OnSelected != null) {
                OnSelected(Data);
            }
        }

        public void RadioUnselected() {
            BackColor = ColorUnselected;
        }

        public void UserOver() {
            BackColor = ColorMouseOver;
        }

        public void UserLeave() {
            BackColor = ColorUnselected;
        }
    }
}
