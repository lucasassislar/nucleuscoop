using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    /// <summary>
    /// Form that all other forms inherit from. Has all
    /// the default design parameters to have the Nucleus Coop look and feel
    /// </summary>
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Removes the flickering from constantly painting, if needed
        /// </summary>
        public void RemoveFlicker()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
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
