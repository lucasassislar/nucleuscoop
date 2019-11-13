using Nucleus.Gaming.Platform.Windows;
using System.Drawing;

namespace Nucleus.Coop.App.Controls.Pages {
    public partial class NoGamesInstalledPageControl : BasePageControl {
        public NoGamesInstalledPageControl() {
            InitializeComponent();

            this.Title = "No games installed";
            this.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🗋");
        }
    }
}
