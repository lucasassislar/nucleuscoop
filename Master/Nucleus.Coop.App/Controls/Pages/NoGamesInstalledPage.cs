using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Gaming.Platform.Windows;

namespace Nucleus.Coop.App.Controls.Pages {
    public partial class NoGamesInstalledPageControl : BasePageControl {
        public NoGamesInstalledPageControl() {
            InitializeComponent();

            this.Title = "No games installed";
            this.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🗋");
        }
    }
}
