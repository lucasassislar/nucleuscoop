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
using System.Threading;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Package;
using Nucleus.Gaming.Platform.Windows;
using Nucleus.Coop.App.Forms;
using System.IO;
using Nucleus.Gaming;
using Nucleus.Coop.App.Properties;

namespace Nucleus.Coop.App.Controls {
    public partial class HandlerManagerControl : BasePageControl {
        private GameHandlerBaseMetadata currentMetadata;
        private SearchStorageForm form;
        private Bitmap interrobang;

        private int titleBarWidth;
        public override int RequiredTitleBarWidth { get { return titleBarWidth; } set { } }

        public HandlerManagerControl() {
            InitializeComponent();

            titleBarWidth = list_left.Width;
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            this.Title = "Settings";
            //this.Image = Resources.nucleus;
            this.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "⚙");

            if (!designMode) {
                LoadInstalled();
            }
        }

        private void LoadInstalled() {
            var gm = GameManager.Instance;
            var handlers = gm.User.InstalledHandlers;

            list_left.Controls.Clear();

            //HorizontalLineControl line = new HorizontalLineControl();
            //list_left.Controls.Add(line);
            //line.LineHorizontalPc = 100;
            //line.Width = list_left.Width;
            //line.LineHeight = 1;
            //line.LineColor = Color.FromArgb(255, 41, 45, 47);

            TitleSeparator gamesSep = new TitleSeparator();
            gamesSep.SetTitle("GAMES");
            gamesSep.Height = 20;
            this.list_left.Controls.Add(gamesSep);

            GameControl installGame = new GameControl();
            installGame.Width = list_left.Width;
            installGame.UpdateTitleText("Install game from exe");
            installGame.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "📁");
            installGame.Click += InstallGame_Click;
            list_left.Controls.Add(installGame);

            GameControl scanGames = new GameControl();
            scanGames.Width = list_left.Width;
            scanGames.UpdateTitleText("Scan for game exes");
            scanGames.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🔍");
            scanGames.Click += ScanGames_Click;
            list_left.Controls.Add(scanGames);

            interrobang = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🗋");

            TitleSeparator sep = new TitleSeparator();
            sep.SetTitle("HANDLERS");
            sep.Height = 20;
            this.list_left.Controls.Add(sep);

            GameControl installFile = new GameControl();
            installFile.Width = list_left.Width;
            installFile.UpdateTitleText("Install handler from file");
            //installFile.Image = Resources.nucleus;
            installFile.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🔍"); ;
            installFile.Click += InstallFile_Click;
            list_left.Controls.Add(installFile);

            if (handlers.Count == 0) {
                GameControl noAvailable = new GameControl();
                noAvailable.Width = list_left.Width;
                noAvailable.UpdateTitleText("No installed game handlers");
                noAvailable.Image = interrobang;
                list_left.Controls.Add(noAvailable);
            } else {
                handlers.Sort(GameHandlerMetadata.CompareHandlerTitle);
                foreach (var handler in handlers) {
                    GameControl gameHandler = new GameControl();
                    gameHandler.Width = list_left.Width;
                    gameHandler.SetHandlerMetadata(handler);
                    gameHandler.Click += GameHandler_Click;
                    gameHandler.Image = interrobang;
                    list_left.Controls.Add(gameHandler);
                }
            }

            DPIManager.ForceUpdate();
        }

        private void ScanGames_Click(object sender, EventArgs e) {
            if (form != null) {
                return;
            }

            if (GameManager.Instance.User.InstalledHandlers.Count == 0) {
                MessageBox.Show("You have no game handlers installed. No games to search for.");
                return;
            }

            form = new SearchStorageForm();

            form.FormClosed += Form_FormClosed;
            form.Show();

            DPIManager.ForceUpdate();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e) {
            form = null;
        }

        private void InstallGame_Click(object sender, EventArgs e) {
            var gm = GameManager.Instance;
            using (OpenFileDialog open = new OpenFileDialog()) {
                open.Filter = "Game Executable Files|*.exe";
                if (open.ShowDialog() == DialogResult.OK) {
                    string path = open.FileName;

                    List<GameHandlerMetadata> allGames = gm.User.InstalledHandlers;

                    GameList list = new GameList(allGames);
                    DPIManager.ForceUpdate();

                    if (list.ShowDialog() == DialogResult.OK) {
                        GameHandlerMetadata selected = list.Selected;
                        UserGameInfo game = gm.TryAddGame(path, list.Selected);

                        if (game == null) {
                            MessageBox.Show("Game already in your library!");
                        } else {
                            MessageBox.Show("Game accepted as ID " + game.GameID);
                            MainForm.Instance.RefreshGames();
                        }
                    }
                }
            }
        }

        private void GameHandler_Click(object sender, EventArgs e) {
            GameControl gameHandler = (GameControl)sender;
            currentMetadata = gameHandler.HandlerMetadata;
            MainForm.Instance.ChangeTitle(currentMetadata.Title, gameHandler.Image);

            panel_gameData.Visible = true;

            label_developer.Text = "Developer: " + currentMetadata.Dev;
            label_version.Text = currentMetadata.V.ToString();
            label_nukeVer.Text = "Nucleus " + currentMetadata.PlatV;
        }

        private void InstallFile_Click(object sender, EventArgs e) {
            using (OpenFileDialog open = new OpenFileDialog()) {
                open.Multiselect = true;
                open.Filter = "Nucleus Package Files|*.nc";
                if (open.ShowDialog() == DialogResult.OK) {
                    string[] paths = open.FileNames;
                    for (int i = 0; i < paths.Length; i++) {
                        GameManager.Instance.RepoManager.InstallPackage(paths[i]);
                    }
                    LoadInstalled();
                }
            }
        }

        private void Installed_Handler_OnSelected(HandlerInfoControl obj) {
            GameHandlerBaseMetadata metadata = obj.Metadata;
            currentMetadata = metadata;
            if (currentMetadata == null) {
                return;
            }
        }

        private void btn_uninstall_Click(object sender, EventArgs e) {
            label_developer.Text = "Developer Name";
            label_version.Text = "0.0";
            label_nukeVer.Text = "Nucleus Version";

            panel_gameData.Visible = false;

            MainForm.Instance.ChangeTitle(this.Title, this.Image);

            string path = PackageManager.GetBaseInstallPath(this.currentMetadata);
            Directory.Delete(path, true);
            GameManager.Instance.RebuildGameDb();
            LoadInstalled();
        }
    }
}
