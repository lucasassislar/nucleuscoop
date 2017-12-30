using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    [DefaultEvent("Click")]
    public partial class PictureButton : UserControl
    {
        public Image Image
        {
            get
            {
                return button_Picture.Image;
            }
            set
            {
                button_Picture.Image = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return button_Btn.Text; }
            set
            {
                button_Btn.Text = value;
            }
        }

        public PictureButton()
        {
            InitializeComponent();
        }

        public Button PictureBtn
        {
            get { return button_Picture; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.OnClick(null);
        }
    }
}
