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
    public partial class CoolListControl : UserControl, IHighlightControl
    {
        public string Text
        {
            get { return label1.Text; }
            set { this.label1.Text = value; }
        }
        protected string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public CoolListControl()
        {
            InitializeComponent();
            connectMouse = true;
        }

        protected bool connectMouse;
        public void AddControl(Control c, bool connectMouse)
        {
            this.connectMouse = connectMouse;
            this.Controls.Add(c);
            connectMouse = true;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Control c = e.Control;
            if (connectMouse)
            {
                c.MouseMove += c_MouseMove;
                c.MouseLeave += c_MouseLeave;
                c.MouseDown += c_MouseDown;
            }
        }

        void c_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        void c_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        void c_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        protected int defaultHeight = 52;
        protected int expandedHeight = 156;
        protected bool expanded;
        private void CoolListControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                expanded = !expanded;
                if (expanded)
                {
                    Expand();
                }
                else
                {
                    Dispand();
                }
            }
        }

        protected void Dispand()
        {
            //?? what's the contrary of expand XD
            this.Height = defaultHeight;
            
            this.Controls.Remove(descLabel);
        }
        protected void Expand()
        {
            this.Height = expandedHeight;

            if (descLabel == null)
            {
                int offset = 10;
                descLabel = new Label();
                descLabel.Location = new Point(offset, (int)(defaultHeight * 1.2f));
                descLabel.Text = description;
                descLabel.Width = this.Width - (offset*2);
                descLabel.AutoSize = true;
                descLabel.MaximumSize = new Size(descLabel.Width, this.Height - descLabel.Location.Y);
            }
            this.Controls.Add(descLabel);
        }

        protected Label descLabel;

        public void Highlight()
        {
            this.BackColor = Color.FromArgb(25, 25, 25);
        }

        public void SoftHighlight()
        {
            this.BackColor = Color.FromArgb(40,40,40);
        }

        public void Darken()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);

            if (expanded)
            {
                expanded = !expanded;
            }
            Dispand();
        }
    }
}
