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
using Nucleus.Gaming.Platform.Windows;
using Nucleus.Gaming;

namespace Nucleus.Coop.App.Controls.Pages {
    public partial class SelectGameFolderPageControl : BasePageControl {
        public event Action<UserGameInfo> SelectedGame;

        public SelectGameFolderPageControl() {
            InitializeComponent();

            Title = "Select exe to launch";
            Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🔍");
        }

        public void UpdateUsers(List<UserGameInfo> users, Image newIcon) {
            Image = newIcon;

            list_gameFolders.Controls.Clear();
            for (int i = 0; i < users.Count; i++) {
                UserGameInfo game = users[i];
                GameControl gameFolder = new GameControl();
                gameFolder.Width = list_gameFolders.Width;
                gameFolder.SetUserGameExe(game);

                gameFolder.Click += GameFolder_Click;
                list_gameFolders.Controls.Add(gameFolder);
            }

            DPIManager.ForceUpdate();
        }

        private void GameFolder_Click(object sender, EventArgs e) {
            GameControl gameControl = (GameControl)sender;
            SelectedGame(gameControl.UserGameInfo);
        }
    }
}
