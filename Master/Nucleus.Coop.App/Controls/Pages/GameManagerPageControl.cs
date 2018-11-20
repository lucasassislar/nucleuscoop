using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Gaming.Coop;

namespace Nucleus.Coop.App.Controls.Pages {
    public partial class GameManagerPageControl : BasePageControl {

        private int titleBarWidth;
        public override int RequiredTitleBarWidth { get { return titleBarWidth; } set { } }

        public GameManagerPageControl() {
            InitializeComponent();

            titleBarWidth = list_left.Width;
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (!designMode) {
                LoadInstalled();
            }
        }

        private void LoadInstalled() {
            var gm = GameManager.Instance;
            List<UserGameInfo> userGames = gm.User.Games;

            list_left.Controls.Clear();

            foreach (var userGame in userGames) {
                GameControl gameInfo = new GameControl();
                gameInfo.Width = list_left.Width;
                gameInfo.SetUserGame(userGame);
                list_left.Controls.Add(gameInfo);
            }
        }
    }
}
