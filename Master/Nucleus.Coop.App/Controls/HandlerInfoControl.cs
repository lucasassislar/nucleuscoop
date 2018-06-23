using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Gaming.Coop.Api;
using Nucleus.Gaming;
using Nucleus.Gaming.Platform.Windows.Controls;

namespace Nucleus.Coop.App.Controls
{
    public partial class HandlerInfoControl : UserControl, IDynamicSized, IRadioControl, IMouseHoverControl
    {
        public Color SelectedColor { get; set; } = Color.FromArgb(80, 80, 80);
        public Color NotSelectedColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Color HoverColor { get; set; } = Color.FromArgb(60, 60, 60);

        private TransparentControl mouseControl;
        public TransparentControl Mouse { get { return mouseControl; } }

        public HandlerInfoControl()
        {
            InitializeComponent();

            mouseControl = new TransparentControl();
            mouseControl.Size = this.Size;

            this.Controls.Add(mouseControl);

            this.BackColor = NotSelectedColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (mouseControl != null)
            {
                mouseControl.Size = this.Size;
                mouseControl.BringToFront();
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (mouseControl != null
                && e.Control != mouseControl)
            {
                mouseControl.BringToFront();
            }
        }

        public void SetHandler(IgdbGame game)
        {
            lbl_handlerName.Text = game.name;
            lbl_version.Text = game.release_dates?.First().date;
            lbl_description.Text = game.summary;
            lbl_devName.Text = "";
        }

        private bool isSelected;
        public void RadioSelected()
        {
            BackColor = SelectedColor;
            isSelected = true;
        }

        public void RadioUnselected()
        {
            BackColor = NotSelectedColor;
            isSelected = false;
        }

        public void UpdateSize(float scale)
        {
        }

        public void UserLeave()
        {
            BackColor = isSelected ? SelectedColor : NotSelectedColor;
        }

        public void UserOver()
        {
            BackColor = HoverColor;
        }
    }
}
