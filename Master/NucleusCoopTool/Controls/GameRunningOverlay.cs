using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Coop.Controls
{
    public partial class GameRunningOverlay : UserControl
    {
        public event Action OnStop;

        public GameRunningOverlay()
        {
            InitializeComponent();
        }

        public void EnableOverlay(Form parent)
        {
            parent.Controls.Add(this);
            this.Dock = DockStyle.Fill;
            this.BringToFront();
        }

        public void DisableOverlay()
        {
            this.Parent.Controls.Remove(this);
        }

        private void GameRunningOverlay_SizeChanged(object sender, EventArgs e)
        {
            btn_Stop.Left = (this.DisplayRectangle.Width / 2) - (btn_Stop.Width / 2);
            btn_Stop.Top = (this.DisplayRectangle.Height / 2) - (btn_Stop.Height / 2);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            DisableOverlay();
            if (OnStop != null)
            {
                OnStop();
            }
        }
    }
}
