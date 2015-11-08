using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming.Controls;

namespace SplitTool.Controls
{
    public partial class PlayerCount : UserControl, ICanProceed
    {
        private int maxPlayers;
        private bool autoProceed;

        public int Players
        {
            get { return (int)numPlayers.Value; }
        }

        public int MaxPlayers
        {
            set 
            { 
                maxPlayers = value;
                this.numPlayers.Maximum = maxPlayers;
            }
        }

        public PlayerCount()
        {
            InitializeComponent();
        }

        public bool CanProceed
        {
            get { return true; }
        }

        public void Restart()
        {
            autoProceed = false;
        }

        public string StepTitle
        {
            get { return "Players"; }
        }

        public bool AutoProceed
        {
            get { return autoProceed; }
        }
        public void AutoProceeded()
        {
            autoProceed = false;
        }

        private void numPlayers_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
