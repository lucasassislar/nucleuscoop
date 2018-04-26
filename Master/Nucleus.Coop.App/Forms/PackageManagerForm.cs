using Nucleus.Coop.Controls.Repo;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class PackageManagerForm : BaseForm
    {
        public PackageManagerForm()
        {
            InitializeComponent();

            Initialize();

            //this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            //this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);

            //SetTabHeader(tabPage1, Color.FromArgb(30, 30, 30));
            //SetTabHeader(tabPage2, Color.FromArgb(30, 30, 30));
            //SetTabHeader(tabPage3, Color.FromArgb(30, 30, 30));
            //SetTabHeader(tabPage4, Color.FromArgb(30, 30, 30));
        }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();

        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl1.Invalidate();
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawFocusRectangle();
            //e.DrawBackground();
            //using (Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]))
            //{
            //    e.Graphics.FillRectangle(br, e.Bounds);
            //    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
            //    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

            //    Rectangle rect = e.Bounds;
            //    rect.Offset(0, 1);
            //    rect.Inflate(0, -1);
            //    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
            //    e.DrawFocusRectangle();
            //}
        }

        private void Initialize()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            gameListBrowser.SelectedChanged += GameListBrowser_SelectedChanged;

            GameManager gameManager = GameManager.Instance;
            RepoManager repoManager = gameManager.RepoManager;

            var config = gameManager.Config;
            int totalHeaders = config.RepoHeaders.Count;
            for (int i = 0; i < totalHeaders; i++)
            {
                string headerUrl = config.RepoHeaders[i];
                repoManager.UpdateHeader(headerUrl, ReceiveUpdatedHeader);
            }
        }

        private void ReceiveUpdatedHeader(RequestResult<RepoHeader> result)
        {
            if (result.Success)
            {
                gameListBrowser.ShowRepo(result.Data);
            }
        }

        private GameHandlerPackageInfo selectedGame;
        private RepoHeader selectedHeader;
        private void GameListBrowser_SelectedChanged(object arg1, Control arg2)
        {
            RepoGameControl gameControl = (RepoGameControl)arg1;
            selectedGame = gameControl.Info;
            selectedHeader = gameControl.Header;

            if (selectedGame == null)
            {
                lbl_gameName.Text = "Unknown";
                lbl_gameDeveloper.Text = "Unknown Error";
                btnInstall.Enabled = false;

                comboVersions.Items.Clear();
                comboVersions.SelectedIndex = 0;
            }
            else
            {
                lbl_gameName.Text = selectedGame.Title;
                lbl_gameDeveloper.Text = selectedGame.Dev;
                btnInstall.Enabled = true;

                comboVersions.Items.Clear();
                for (int i = 1; i <= selectedGame.V; i++)
                {
                    comboVersions.Items.Add("v" + i);
                }

                if (selectedGame.V > 0)
                {
                    comboVersions.SelectedIndex = 0;
                }
            }
        }

        private void comboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboVersions.SelectedIndex == -1)
            {
                return;
            }

            GameManager gameManager = GameManager.Instance;
            RepoManager repoManager = gameManager.RepoManager;

            repoManager.RequestPackageFullInfo(selectedHeader, selectedGame, comboVersions.SelectedIndex + 1, ReceiveFullGameInfo);
        }

        private void ReceiveFullGameInfo(RequestResult<GameHandlerMetadata> result)
        {
            if (result.Success)
            {
                this.Invoke(new Action<GameHandlerMetadata>(privShowPackage), result.Data);
            }
        }

        private void privShowPackage(GameHandlerMetadata gameInfo)
        {
            lbl_GameTitle.Text = gameInfo.Title;
            lbl_NukeVersion.Text = gameInfo.PlatformVersion.ToString(CultureInfo.InvariantCulture);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            GameManager gameManager = GameManager.Instance;
            RepoManager repoManager = gameManager.RepoManager;

            repoManager.RequestPackageDownload(selectedHeader, selectedGame, ReceivePackageDownload);
        }

        private void ReceivePackageDownload(RequestResult<string> result)
        {
            if (result.Success)
            {
                GameManager gameManager = GameManager.Instance;
                RepoManager repoManager = gameManager.RepoManager;

                repoManager.InstallPackage(result.Data);
            }
        }
    }
}
