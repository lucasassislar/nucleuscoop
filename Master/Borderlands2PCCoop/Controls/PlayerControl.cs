using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Borderlands2PCCoop
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
        }

        public void SetName(string name)
        {
            this.label1.Text = name;
        }
    }
}
