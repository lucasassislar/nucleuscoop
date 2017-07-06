using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Nucleus.Gaming
{
    public class ControlListBox : UserControl
    {
        private int totalHeight;
        private int border = 1;

        public event Action<object, Control> SelectedChanged;
        public Size Offset { get; set; }
        public Control SelectedControl { get; protected set; }

        public int Border
        {
            get { return border; }
            set { border = value; }
        }

        public ControlListBox()
        {
            this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateSizes();
        }

        private bool updatingSize;
        public void UpdateSizes()
        {
            if (updatingSize)
            {
                return;
            }

            updatingSize = true;
            
            totalHeight = 0;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                var con = Controls[i];
                con.Width = this.Width;// - SystemInformation.VerticalScrollBarWidth;

                con.Location = new Point(0, totalHeight);
                totalHeight += con.Height + border;

                con.Invalidate();
            }

            updatingSize = false;
        }

        private void C_SizeChanged(object sender, EventArgs e)
        {
            Control con = (Control)sender;
            // this has the potential of being incredibly slow
            UpdateSizes();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (!this.DesignMode && e.Control != null)
            {
                Control c = e.Control;

                c.ControlAdded += C_ControlAdded;
                c.Click += c_Click;
                c.SizeChanged += C_SizeChanged;
                if (c is IRadioControl)
                {
                    c.MouseEnter += c_MouseEnter;
                    c.MouseLeave += c_MouseLeave;
                }

                int index = this.Controls.IndexOf(c);
                Size s = c.Size;

                c.Location = new Point(0, totalHeight);
                totalHeight += s.Height + border;
            }
        }

        

        private void C_ControlAdded(object sender, ControlEventArgs e)
        {
            Control c = e.Control;
            c.Click += c_Click;
            c.MouseEnter += c_MouseEnter;
            c.MouseLeave += c_MouseLeave;
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            UpdateSizes();
        }

        public void Deselect()
        {
            SelectedControl = null;
            c_Click(this, EventArgs.Empty);
        }

        private void c_MouseEnter(object sender, EventArgs e)
        {
            Control parent = (Control)sender;

            if (parent != SelectedControl && parent is IRadioControl)
            {
                IRadioControl high = (IRadioControl) parent;
                high.UserOver();
            }
        }

        private void c_MouseLeave(object sender, EventArgs e)
        {
            Control parent = (Control)sender;

            if (parent != SelectedControl && parent is IRadioControl)
            {
                IRadioControl high = (IRadioControl)parent;
                high.UserLeave();
            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            Control parent = (Control)sender;

            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control c = this.Controls[i];
                if (c is IRadioControl)
                {
                    IRadioControl high = (IRadioControl)c;
                    if (parent == c)
                    {
                        // highlight
                        high.RadioSelected();
                    }
                    else
                    {
                        high.RadioUnselected();
                    }
                }
            }

            if (parent != null &&
                parent != SelectedControl)
            {
                if (this.SelectedChanged != null)
                {
                    SelectedControl = parent;
                    this.SelectedChanged(SelectedControl, this);
                }
            }

            SelectedControl = parent;

            this.OnClick(e);
        }
    }
}
