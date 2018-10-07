using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    /// <summary>
    /// Form that all other forms inherit from. Has all
    /// the default design parameters to have the Nucleus Coop look and feel
    /// </summary>
    public class BaseForm : Form, IDynamicSized
    {
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public BaseForm()
        {
            // Default DPI = 96 = 100%
            // 1 pt = 1/72 inch
            // 12pt = 1/6 inch
            // 12 * 300% = 36
            // 12 * 125% = 15
            // 12 * 150% = 18
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(50, 50, 50);
            ForeColor = Color.FromArgb(240, 240, 240);
            Margin = new Padding(4, 4, 4, 4);
            Name = "BaseForm";
            Text = "BaseForm";

            // create it here, else the designer will show the default windows font
            Font = new Font("Segoe UI", 12, GraphicsUnit.Point);

            DPIManager.Register(this);
        }
        ~BaseForm()
        {
            DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
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
        public void RemoveFlicker()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
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
        protected void SetUpForm()
        {
            UserScreen[] screens = ScreensUtil.AllScreens();
            Point pos = Cursor.Position;

            Rectangle cursorScreen = Rectangle.Empty;
            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen user = screens[i];
                Rectangle bounds = user.MonitorBounds;

                if (bounds.Contains(pos))
                {
                    cursorScreen = bounds;
                    break;
                }
            }

            this.SetDesktopLocation(cursorScreen.X + 100, cursorScreen.Y + 100);
        }
    }
}
