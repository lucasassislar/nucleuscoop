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
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

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
        /// </summary>
        /// <param name="f"></param>
        public void SetUpForm(Form f)
        {
            Point desktop = this.DesktopLocation;
            f.SetDesktopLocation(desktop.X + 100, desktop.Y + 100);
        }
    }
}
