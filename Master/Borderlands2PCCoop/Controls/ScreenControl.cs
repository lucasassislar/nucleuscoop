using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Borderlands2PCCoop
{
    public partial class ScreenControl : UserControl
    {
        public ScreenControl()
        {
            InitializeComponent();
        }

        public void SetName(string name)
        {
            this.label1.Text = name;
        }
    }
}
