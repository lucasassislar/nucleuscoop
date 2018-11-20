using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop {
    /// <summary>
    /// Form that all other forms inherit from. Has all
    /// the default design parameters to have the Nucleus Coop look and feel
    /// </summary>
    public class BaseForm : Form, IDynamicSized {
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

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

            this.ControlAdded += BaseForm_ControlAdded;

            this.MouseMove += BaseForm_MouseMove;
            this.MouseDown += BaseForm_MouseDown;
            this.MouseUp += BaseForm_MouseUp;
        }


        private void BaseForm_ControlAdded(object sender, ControlEventArgs e) {
            e.Control.ControlAdded += BaseForm_ControlAdded;
            e.Control.MouseMove += BaseForm_MouseMove;
            e.Control.MouseDown += BaseForm_MouseDown;
            e.Control.MouseUp += BaseForm_MouseUp;
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

        bool on_MinimumSize;
        short minimumWidth = 350;
        short minimumHeight = 26;
        short borderSpace = 20;
        short borderDiameter = 6;

        bool onBorderRight;
        bool onBorderLeft;
        bool onBorderTop;
        bool onBorderBottom;
        bool onCornerTopRight;
        bool onCornerTopLeft;
        bool onCornerBottomRight;
        bool onCornerBottomLeft;

        bool movingRight;
        bool movingLeft;
        bool movingTop;
        bool movingBottom;
        bool movingCornerTopRight;
        bool movingCornerTopLeft;
        bool movingCornerBottomRight;
        bool movingCornerBottomLeft;

        private void BaseForm_MouseUp(object sender, MouseEventArgs e) {
            stopResizer();
        }

        private void BaseForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (onBorderRight) { movingRight = true; } else { movingRight = false; }
                if (onBorderLeft) { movingLeft = true; } else { movingLeft = false; }
                if (onBorderTop) { movingTop = true; } else { movingTop = false; }
                if (onBorderBottom) { movingBottom = true; } else { movingBottom = false; }
                if (onCornerTopRight) { movingCornerTopRight = true; } else { movingCornerTopRight = false; }
                if (onCornerTopLeft) { movingCornerTopLeft = true; } else { movingCornerTopLeft = false; }
                if (onCornerBottomRight) { movingCornerBottomRight = true; } else { movingCornerBottomRight = false; }
                if (onCornerBottomLeft) { movingCornerBottomLeft = true; } else { movingCornerBottomLeft = false; }
            }
        }

        private void BaseForm_MouseMove(object sender, MouseEventArgs e) {
            //if (maximized) { return; }

            if (this.Width <= minimumWidth) { this.Width = (minimumWidth + 5); on_MinimumSize = true; }
            if (this.Height <= minimumHeight) { this.Height = (minimumHeight + 5); on_MinimumSize = true; }
            if (on_MinimumSize) { stopResizer(); } else { startResizer(); }

            if ((Cursor.Position.X > ((this.Location.X + this.Width) - borderDiameter))
                & (Cursor.Position.Y > (this.Location.Y + borderSpace))
                & (Cursor.Position.Y < ((this.Location.Y + this.Height) - borderSpace))) {
                this.Cursor = Cursors.SizeWE;
                onBorderRight = true;
            } else if ((Cursor.Position.X < (this.Location.X + borderDiameter))
                & (Cursor.Position.Y > (this.Location.Y + borderSpace))
                & (Cursor.Position.Y < ((this.Location.Y + this.Height) - borderSpace))) {
                this.Cursor = Cursors.SizeWE;
                onBorderLeft = true;
            } else if ((Cursor.Position.Y < (this.Location.Y + borderDiameter)) 
                & (Cursor.Position.X > (this.Location.X + borderSpace)) 
                & (Cursor.Position.X < ((this.Location.X + this.Width) - borderSpace))) {
                this.Cursor = Cursors.SizeNS;
                onBorderTop = true;
            } else if ((Cursor.Position.Y > ((this.Location.Y + this.Height) - borderDiameter)) & 
                (Cursor.Position.X > (this.Location.X + borderSpace)) & 
                (Cursor.Position.X < ((this.Location.X + this.Width) - borderSpace))) {
                this.Cursor = Cursors.SizeNS;
                onBorderBottom = true;
            } else if ((Cursor.Position.X == ((this.Location.X + this.Width) - 1)) & 
                (Cursor.Position.Y == this.Location.Y)) {
                this.Cursor = Cursors.SizeNESW;
                onCornerTopRight = true;
            } else if ((Cursor.Position.X == this.Location.X) 
                & (Cursor.Position.Y == this.Location.Y)) {
                this.Cursor = Cursors.SizeNWSE;
                onCornerTopLeft = true;
            } else if ((Cursor.Position.X == ((this.Location.X + this.Width) - 1)) &
                (Cursor.Position.Y == ((this.Location.Y + this.Height) - 1))) {
                this.Cursor = Cursors.SizeNWSE;
                onCornerBottomRight = true;
            } else if ((Cursor.Position.X == this.Location.X) & 
                (Cursor.Position.Y == ((this.Location.Y + this.Height) - 1))) {
                this.Cursor = Cursors.SizeNESW;
                onCornerBottomLeft = true;
            } else {
                onBorderRight = false;
                onBorderLeft = false;
                onBorderTop = false;
                onBorderBottom = false;
                onCornerTopRight = false;
                onCornerTopLeft = false;
                onCornerBottomRight = false;
                onCornerBottomLeft = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void startResizer() {
            if (movingRight) {
                this.Width = Cursor.Position.X - this.Location.X;
            } else if (movingLeft) {
                this.Width = ((this.Width + this.Location.X) - Cursor.Position.X);
                this.Location = new Point(Cursor.Position.X, this.Location.Y);
            } else if (movingTop) {
                this.Height = ((this.Height + this.Location.Y) - Cursor.Position.Y);
                this.Location = new Point(this.Location.X, Cursor.Position.Y);
            } else if (movingBottom) {
                this.Height = (Cursor.Position.Y - this.Location.Y);
            } else if (movingCornerTopRight) {
                this.Width = (Cursor.Position.X - this.Location.X);
                this.Height = ((this.Location.Y - Cursor.Position.Y) + this.Height);
                this.Location = new Point(this.Location.X, Cursor.Position.Y);
            } else if (movingCornerTopLeft) {
                this.Width = ((this.Width + this.Location.X) - Cursor.Position.X);
                this.Location = new Point(Cursor.Position.X, this.Location.Y);
                this.Height = ((this.Height + this.Location.Y) - Cursor.Position.Y);
                this.Location = new Point(this.Location.X, Cursor.Position.Y);
            } else if (movingCornerBottomRight) {
                this.Size = new Size(Cursor.Position.X - this.Location.X,
                                     Cursor.Position.Y - this.Location.Y);
            } else if (movingCornerBottomLeft) {
                this.Width = ((this.Width + this.Location.X) - Cursor.Position.X);
                this.Height = (Cursor.Position.Y - this.Location.Y);
                this.Location = new Point(Cursor.Position.X, this.Location.Y);
            }
        }

        private void stopResizer() {
            movingRight = false;
            movingLeft = false;
            movingTop = false;
            movingBottom = false;
            movingCornerTopRight = false;
            movingCornerTopLeft = false;
            movingCornerBottomRight = false;
            movingCornerBottomLeft = false;
            this.Cursor = Cursors.Default;
            System.Threading.Thread.Sleep(300);
            on_MinimumSize = false;
        }
    }
}
