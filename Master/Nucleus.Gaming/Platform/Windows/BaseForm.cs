using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Platform.Windows.Controls;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
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

        public Panel FormContent { get; private set; }

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

            // Background is transparent for resizing system
            // Inheriting classes should override the OnResize function
            // and have all its control
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts

            // create it here, else the designer will show the default windows font
            Font = new Font("Segoe UI", 12, GraphicsUnit.Point);

            DPIManager.Register(this);

            filter = new MouseMessageFilter();
            filter.StartFiltering();

            filter.MouseMove += BaseForm_MouseMove;
            filter.MouseDown += BaseForm_MouseDown;
            filter.MouseUp += BaseForm_MouseUp;
        }

        ~BaseForm() {
            DPIManager.Unregister(this);
        }


        public void SetupBaseForm(Panel formContent) {
            this.FormContent = formContent;
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            // check window state
            if (FormContent != null) {
                Size thisSize = this.Size;
                if (WindowState == FormWindowState.Normal) {
                    // increase borders for easier resizing
                    this.FormContent.Size = new Size(thisSize.Width - 10, thisSize.Height - 10);
                    this.FormContent.Location = new Point(5, 5);
                } else if (WindowState == FormWindowState.Maximized) {
                    this.FormContent.Size = new Size(thisSize.Width, thisSize.Height);
                    this.FormContent.Location = new Point(0, 0);
                }
            }
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
            if (!User32Util.IsFormFocused(this)) {
                return;
            }

            if (e.Button == MouseButtons.Left) {
                mouseDown = true;
            }
        }

        private void BaseForm_MouseMove(object sender, MouseEventArgs e) {
            if (!User32Util.IsFormFocused(this) ||
                WindowState == FormWindowState.Maximized) {
                return;
            }

            Point cursor = Cursor.Position;
            Cursor newCursor = null;
            MovementDirection newMouseDirection = MovementDirection.None;

            if ((cursor.X > ((this.Location.X + this.Width) - borderDiameter)) &&
                (cursor.Y > (this.Location.Y + borderSpace)) &&
                (cursor.Y < ((this.Location.Y + this.Height) - borderSpace))) {
                newCursor = Cursors.SizeWE;
                newMouseDirection = MovementDirection.Right;
            } else if ((cursor.X < (this.Location.X + borderDiameter)) &&
                (cursor.Y > (this.Location.Y + borderSpace)) &&
                (cursor.Y < ((this.Location.Y + this.Height) - borderSpace))) {
                newCursor = Cursors.SizeWE;
                newMouseDirection = MovementDirection.Left;
            } else if ((cursor.Y < (this.Location.Y + topBorderDiameter)) &&
                (cursor.X > (this.Location.X + borderSpace)) &&
                (cursor.X < ((this.Location.X + this.Width) - borderSpace))) {
                newCursor = Cursors.SizeNS;
                newMouseDirection = MovementDirection.Top;
            } else if ((cursor.Y > ((this.Location.Y + this.Height) - borderDiameter)) &&
                (cursor.X > (this.Location.X + borderSpace)) &&
                (cursor.X < ((this.Location.X + this.Width) - borderSpace))) {
                newCursor = Cursors.SizeNS;
                newMouseDirection = MovementDirection.Bottom;
            } else if ((cursor.X > ((this.Location.X + this.Width) - borderDiameter)) &&
                (cursor.Y < (this.Location.Y + borderDiameter))) {
                newCursor = Cursors.SizeNESW;
                newMouseDirection = MovementDirection.TopRight;
            } else if ((cursor.X < (this.Location.X + borderDiameter)) &&
                (cursor.Y < (this.Location.Y + borderDiameter))) {
                newCursor = Cursors.SizeNWSE;
                newMouseDirection = MovementDirection.TopLeft;
            } else if ((cursor.X > ((this.Location.X + this.Width) - borderDiameter)) &&
                (cursor.Y > ((this.Location.Y + this.Height) - borderDiameter))) {
                newCursor = Cursors.SizeNWSE;
                newMouseDirection = MovementDirection.BottomRight;
            } else if ((cursor.X < (this.Location.X + borderDiameter)) &&
                (cursor.Y > ((this.Location.Y + this.Height) - borderDiameter))) {
                newCursor = Cursors.SizeNESW;
                newMouseDirection = MovementDirection.BottomLeft;
            } else {
                newCursor = Cursors.Default;
                newMouseDirection = MovementDirection.None;
            }

            if (!mouseDown) {
                // not holding the mouse button, we can change state
                this.Cursor = newCursor;
                this.mouseCorner = newMouseDirection;
            }

            startResizer();
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
            //Thread.Sleep(300);
        }
    }
}
