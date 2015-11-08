using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming;

namespace Borderlands2PCCoop
{
    public partial class CoopScreenControl : UserControl
    {
        public List<Panel> Panels;
        public List<PlayerInfo> Players;
        private List<ScreenControl> Screens;

        public CoopScreenControl()
        {
            InitializeComponent();
        }
    }
}
