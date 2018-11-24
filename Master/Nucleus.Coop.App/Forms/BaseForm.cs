using Nucleus.Coop.App;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Platform.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop {
    /// <summary>
    /// Form that all other forms inherit from. Has all
    /// the default design parameters to have the Nucleus Coop look and feel
    /// </summary>
    public class BaseForm : Form, IDynamicSized {
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;
        private MouseMessageFilter filter;

        protected override CreateParams CreateParams {
            get {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public BaseForm() {
            // Default DPI = 96 = 100%
            // 1 pt = 1/72 inch
            // 12pt = 1/6 inch
            // 12 * 300% = 36
            // 12 * 125% = 15
            // 12 * 150% = 18
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(54, 57, 63);
            ForeColor = Color.FromArgb(240, 240, 240);
            Margin = new Padding(4, 4, 4, 4);
            Name = "BaseForm";
            Text = "BaseForm";

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts

            // create it here, else the designer will show the default windows font
            Font = new Font("Segoe UI", 12, GraphicsUnit.Point);

            DPIManager.Register(this);

            //this.ControlAdded += BaseForm_ControlAdded;
            filter = new MouseMessageFilter();
            filter.StartFiltering();

            //this.MouseMove += BaseForm_MouseMove;
            //this.MouseDown += BaseForm_MouseDown;
            //this.MouseUp += BaseForm_MouseUp;

            filter.MouseMove += BaseForm_MouseMove;
            filter.MouseDown += BaseForm_MouseDown;
            filter.MouseUp += BaseForm_MouseUp;
        }


        private void BaseForm_ControlAdded(object sender, ControlEventArgs e) {
            e.Control.ControlAdded += BaseForm_ControlAdded;
            e.Control.MouseMove += BaseForm_MouseMove;
            e.Control.MouseDown += BaseForm_MouseDown;
            e.Control.MouseUp += BaseForm_MouseUp;

            // TODO: find a better way to get mouse events everywhere
            //RecursiveSetMouseEvents(e.Control);
        }

        private void RecursiveSetMouseEvents(Control c) {
            foreach (Control child in c.Controls) {
                child.ControlAdded += BaseForm_ControlAdded;
                child.MouseMove += BaseForm_MouseMove;
                child.MouseDown += BaseForm_MouseDown;
                child.MouseUp += BaseForm_MouseUp;

                RecursiveSetMouseEvents(child);
            }
        }

        ~BaseForm() {
            DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale) {
            if (IsDisposed) {
                DPIManager.Unregister(this);
                return;
            }

            //SuspendLayout();

            //Font = DPIManager.Font;

            //Size defaultSize = DefaultSize;
            //int wid = DPIManager.Adjust(defaultSize.Width, scale);
            //int hei = DPIManager.Adjust(defaultSize.Height, scale);
            ////Size = new Size(wid, hei);
            //Console.WriteLine("Changed to {0}x{1}", wid, hei);

            //ResumeLayout();
        }

        /// <summary>
        /// Removes the flickering from constantly painting, if needed
        /// </summary>
        public void RemoveFlicker() {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);

            SetUpForm();
        }

        /// <summary>
        /// Position the form on the same monitor the user has put our app!
        /// (by default, forms are open on the primary monitor, but if the user dragged
        /// our form to another monitor the child forms still get created on the main monitor.
        /// This is a small quality of life fix)
        /// </summary>
        /// <param name="f">The form to move</param>
        protected void SetUpForm() {
            UserScreen[] screens = ScreensUtil.AllScreens();
            Point pos = Cursor.Position;

            Rectangle cursorScreen = Rectangle.Empty;
            for (int i = 0; i < screens.Length; i++) {
                UserScreen user = screens[i];
                Rectangle bounds = user.MonitorBounds;

                if (bounds.Contains(pos)) {
                    cursorScreen = bounds;
                    break;
                }
            }

            this.SetDesktopLocation(cursorScreen.X + 100, cursorScreen.Y + 100);
        }

        int borderSpace = 20;
        int borderDiameter = 6;
        int topBorderDiameter = 3;

        enum MovementDirection {
            None, Right, Left, Top, Bottom,
            TopRight, TopLeft, BottomRight, BottomLeft
        }
        MovementDirection mouseCorner;
        bool mouseDown;

        private void BaseForm_MouseUp(object sender, MouseEventArgs e) {
            ResetResizer();
        }

        private void BaseForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                mouseDown = true;
            }
        }

        private void BaseForm_MouseMove(object sender, MouseEventArgs e) {
            if (WindowState == FormWindowState.Maximized) {
                return;
            }

            if (mouseCorner == MovementDirection.None || !mouseDown) {
                Point cursor = Cursor.Position;

                if ((cursor.X > ((this.Location.X + this.Width) - borderDiameter)) &&
                    (cursor.Y > (this.Location.Y + borderSpace)) &&
                    (cursor.Y < ((this.Location.Y + this.Height) - borderSpace))) {
                    this.Cursor = Cursors.SizeWE;
                    mouseCorner = MovementDirection.Right;
                } else if ((cursor.X < (this.Location.X + borderDiameter)) &&
                    (cursor.Y > (this.Location.Y + borderSpace)) &&
                    (cursor.Y < ((this.Location.Y + this.Height) - borderSpace))) {
                    this.Cursor = Cursors.SizeWE;
                    mouseCorner = MovementDirection.Left;
                } else if ((cursor.Y < (this.Location.Y + topBorderDiameter)) &&
                    (cursor.X > (this.Location.X + borderSpace)) &&
                    (cursor.X < ((this.Location.X + this.Width) - borderSpace))) {
                    this.Cursor = Cursors.SizeNS;
                    mouseCorner = MovementDirection.Top;
                } else if ((cursor.Y > ((this.Location.Y + this.Height) - borderDiameter)) &&
                    (cursor.X > (this.Location.X + borderSpace)) &&
                    (cursor.X < ((this.Location.X + this.Width) - borderSpace))) {
                    this.Cursor = Cursors.SizeNS;
                    mouseCorner = MovementDirection.Bottom;
                } else if ((cursor.X > ((this.Location.X + this.Width) - borderDiameter)) &&
                    (cursor.Y < (this.Location.Y + borderDiameter))) {
                    this.Cursor = Cursors.SizeNESW;
                    mouseCorner = MovementDirection.TopRight;
                } else if ((cursor.X < (this.Location.X + borderDiameter)) &&
                    (cursor.Y < (this.Location.Y + borderDiameter))) {
                    this.Cursor = Cursors.SizeNWSE;
                    mouseCorner = MovementDirection.TopLeft;
                } else if ((cursor.X > ((this.Location.X + this.Width) - borderDiameter)) &&
                    (cursor.Y > ((this.Location.Y + this.Height) - borderDiameter))) {
                    this.Cursor = Cursors.SizeNWSE;
                    mouseCorner = MovementDirection.BottomRight;
                } else if ((cursor.X < (this.Location.X + borderDiameter)) &&
                    (cursor.Y > ((this.Location.Y + this.Height) - borderDiameter))) {
                    this.Cursor = Cursors.SizeNESW;
                    mouseCorner = MovementDirection.BottomLeft;
                } else {
                    this.Cursor = Cursors.Default;
                    mouseCorner = MovementDirection.None;
                }
            } else {
                startResizer();
            }
        }

        private void startResizer() {
            if (!mouseDown) {
                return;
            }

            Point cursor = Cursor.Position;
            Size minimum = this.MinimumSize;
            switch (mouseCorner) {
                case MovementDirection.Right:
                    this.Width = Math.Max(cursor.X - this.Location.X, minimum.Width);
                    break;
                case MovementDirection.Left:
                    this.Width = Math.Max(((this.Width + this.Location.X) - cursor.X), minimum.Width);
                    this.Location = new Point(cursor.X, this.Location.Y);
                    break;
                case MovementDirection.Top:
                    this.Height = Math.Max(((this.Height + this.Location.Y) - cursor.Y), minimum.Height);
                    this.Location = new Point(this.Location.X, cursor.Y);
                    break;
                case MovementDirection.Bottom:
                    this.Height = Math.Max((cursor.Y - this.Location.Y), minimum.Height);
                    break;
                case MovementDirection.TopLeft:
                    this.Width = Math.Max(((this.Width + this.Location.X) - cursor.X), minimum.Width);
                    this.Location = new Point(cursor.X, this.Location.Y);
                    this.Height = Math.Max(((this.Height + this.Location.Y) - cursor.Y), minimum.Height);
                    this.Location = new Point(this.Location.X, cursor.Y);
                    break;
                case MovementDirection.TopRight:
                    this.Width = Math.Max((cursor.X - this.Location.X), minimum.Width);
                    this.Height = ((this.Location.Y - cursor.Y) + this.Height);
                    this.Location = new Point(this.Location.X, cursor.Y);
                    break;
                case MovementDirection.BottomLeft:
                    this.Width = Math.Max(((this.Width + this.Location.X) - cursor.X), minimum.Width);
                    this.Height = Math.Max((cursor.Y - this.Location.Y), minimum.Height);
                    this.Location = new Point(cursor.X, this.Location.Y);
                    break;
                case MovementDirection.BottomRight:
                    this.Size = new Size(cursor.X - this.Location.X,
                                         cursor.Y - this.Location.Y);
                    break;
            }
        }

        private void ResetResizer() {
            mouseDown = false;
            mouseCorner = MovementDirection.None;
            this.Cursor = Cursors.Default;
            Thread.Sleep(300);
        }
    }
}
