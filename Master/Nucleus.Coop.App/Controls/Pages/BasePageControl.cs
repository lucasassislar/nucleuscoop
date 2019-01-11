using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls
{
    public partial class BasePageControl : UserControl
    {
        public event Action<UserControl, int> OnRequiredTitleBarWidthChanged;
        private int requiredTitleBarWidth;
        public virtual int RequiredTitleBarWidth {
            get { return requiredTitleBarWidth; }
            set {
                if (requiredTitleBarWidth != value && OnRequiredTitleBarWidthChanged != null) {
                    OnRequiredTitleBarWidthChanged(this, value);
                }
                requiredTitleBarWidth = value;
            }
        }

        public string Title { get; protected set; }
        public Image Image { get; protected set; }

        public BasePageControl()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(54, 57, 63);
            ForeColor = Color.FromArgb(240, 240, 240);
            Margin = new Padding(8, 8, 8, 8);
            Name = "BaseForm";
            Text = "BaseForm";
            Size = new Size(782, 628); // standard starting size

            // create it here, else the designer will show the default windows font
            Font = new Font("Segoe UI", 12, GraphicsUnit.Point);
        }
    }
}
