using Nucleus.Gaming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.PkgMaker
{
    /// <summary>
    /// Form that all other forms inherit from. Has all
    /// the default design parameters to have the Nucleus Coop look and feel
    /// </summary>
    public class BaseForm : Form, IDynamicSized
    {
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
            ForeColor = Color.White;
            Margin = new Padding(4, 4, 4, 4);
            Name = "BaseForm";
            Text = "BaseForm";

            // create it here, else the desgienr will show the default windows font
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
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Position the form on the same monitor the user has put our app!
        /// (by default, forms are open on the primary monitor, but if the user dragged
        /// our form to another monitor the child forms still get created on the main monitor.
        /// This is a small quality of life fix)
        /// </summary>
        /// <param name="f">The form to move</param>
        public void SetUpForm(Form f)
        {
            Point desktop = this.DesktopLocation;
            f.SetDesktopLocation(desktop.X + 100, desktop.Y + 100);
        }
    }
}
