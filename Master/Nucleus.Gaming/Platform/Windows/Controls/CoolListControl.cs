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

namespace Nucleus.Gaming.Windows.Controls
{
    public class CoolListControl : UserControl
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
        public object Data { get; set; }
        public event Action<object> OnSelected;

        public bool EnableClicking { get; set; }

        public CoolListControl(bool enableHightlighting)
        {
            EnableHighlighting = enableHightlighting;

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

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;

            if (EnableHighlighting)
            {
                c.MouseEnter += C_MouseEnter;
                c.MouseLeave += C_MouseLeave;
            }
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void C_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void C_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        public Color ColorSelected { get; set; } = Color.FromArgb(66, 70, 77);
        public Color ColorUnselected { get; set; } = Color.FromArgb(47, 49, 54);
        public Color ColorMouseOver { get; set; } = Color.FromArgb(54, 57, 63);

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            BackColor = ColorUnselected;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!ContainsFocus)
            {
                BackColor = ColorMouseOver;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!ContainsFocus)
            {
                BackColor = ColorUnselected;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (EnableClicking) {
                BackColor = ColorSelected;
                if (OnSelected != null) {
                    OnSelected(Data);
                }
            }
        }
    }
}
