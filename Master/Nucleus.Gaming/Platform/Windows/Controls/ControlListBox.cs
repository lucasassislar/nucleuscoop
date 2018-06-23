using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    /// <summary>
    /// Lists controls dynamically in a list
    /// </summary>
    public class ControlListBox : UserControl
    {
        private int totalHeight;
        private int border = 1;

        public event Action<Control, Control> SelectedChanged;
        public Size Offset { get; set; }
        public Control SelectedControl { get; protected set; }


        public int Border
        {
            get { return border; }
            set { border = value; }
        }

        public ControlListBox()
        {
            //this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScroll = false;
        }

        public override bool AutoScroll
        {
            get { return base.AutoScroll; }
            set
            {
                base.AutoScroll = value;
                if (!value)
                {
                    this.HorizontalScroll.Visible = false;
                    this.HorizontalScroll.Enabled = false;
                    this.VerticalScroll.Visible = false;
                }
            }
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
            bool isVerticalVisible = VerticalScroll.Visible;
            int v = isVerticalVisible ? (1 + SystemInformation.VerticalScrollBarWidth) : 0;

            for (int i = 0; i < this.Controls.Count; i++)
            {
                var con = Controls[i];
                con.Width = this.Width - v;

                con.Location = new Point(0, totalHeight);
                totalHeight += con.Height + border;

                con.Invalidate();
            }

            updatingSize = false;

            HorizontalScroll.Visible = false;
            VerticalScroll.Visible = totalHeight > this.Height;
            if (VerticalScroll.Visible != isVerticalVisible)
            {
                UpdateSizes(); // need to update again
            }
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
                c.SizeChanged += C_SizeChanged;
                if (c is IRadioControl)
                {
                    if (c is IMouseHoverControl)
                    {
                        IMouseHoverControl mouse = (IMouseHoverControl)c;
                        mouse.Mouse.MouseClick += c_MouseClick;
                        mouse.Mouse.MouseEnter += c_MouseEnter;
                        mouse.Mouse.MouseLeave += c_MouseLeave;
                    }
                    else
                    {
                        c.MouseClick += c_MouseClick;
                        c.MouseEnter += c_MouseEnter;
                        c.MouseLeave += c_MouseLeave;
                    }
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
            if (parent is TransparentControl)
            {
                parent = parent.Parent;
            }

            if (parent != SelectedControl && parent is IRadioControl)
            {
                IRadioControl high = (IRadioControl)parent;
                high.UserOver();
            }
        }

        private void c_MouseLeave(object sender, EventArgs e)
        {
            Control parent = (Control)sender;
            if (parent is TransparentControl)
            {
                parent = parent.Parent;
            }

            if (parent != SelectedControl && parent is IRadioControl)
            {
                IRadioControl high = (IRadioControl)parent;
                high.UserLeave();
            }
        }

        private void c_MouseClick(object sender, MouseEventArgs e)
        {
            Control parent = (Control)sender;
            if (parent is TransparentControl)
            {
                parent = parent.Parent;
            }

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
