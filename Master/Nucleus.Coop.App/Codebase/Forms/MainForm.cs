using Nucleus.Coop.App.Controls;
using Nucleus.DPI;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Platform.Windows;
using Nucleus.Platform.Windows.Controls;
using Nucleus.Platform.Windows.Interop;
using SplitScreenMe.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace Nucleus.Coop.App.Forms {
    /// <summary>
    /// Central UI class to the Nucleus Coop application
    /// </summary>
    public partial class MainForm : BaseForm {
        private GameManager gameManager;
        private Dictionary<string, GameControl> controls;
        private GameRunningOverlay overlay;
        private bool refreshingGames;

        private AppPage appPage = AppPage.None;
        private GameControl pkgManagerBtn;
        private BasePageControl currentPage;

        public static MainForm Instance { get; private set; }

        protected override Size DefaultSize { get { return new Size(1070, 740); } }

        public GamePageBrowserControl BrowserBtns { get { return gamePageBrowserControl; } }
        public GameControl Selected { get; private set; }

        private Thread handlerThread;
        private bool TopMostToggle = true;

        public MainForm(string[] args, GameManager gameManager) {
            if (ApplicationUtil.OnlyOneInstance()) {
                this.Close();
                return;
            }

            this.gameManager = gameManager;
            MainForm.Instance = this;

            InitializeComponent();

            this.SetupBaseForm(this.panel_formContent);

            // control that shows over the entire application while the game is running
            overlay = new GameRunningOverlay();
            overlay.OnStop += Overlay_OnStop;

            this.titleBarControl.Text = $"SplitScreenMe RC{Globals.Version}";

            controls = new Dictionary<string, GameControl>();

            // Selects the list of games, so the buttons look equal
            list_games.Select();
            list_games.AutoScroll = false;

            // check for arguments
            //SplitScreenEngineUtil.HandleArguments(args);

            // update register if needed
            //SplitScreenEngineUtil.HandleRegisterUpdates();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            UpdatePageSizes();
        }

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            this.TopMost = true;
            this.BringToFront();
            System.Diagnostics.Debug.WriteLine("Got Focus");
        }

        public void RefreshGames() {
            if (refreshingGames) {
                return;
            }
            refreshingGames = true;

            lock (controls) {
                foreach (var con in controls) {
                    if (con.Value != null) {
                        con.Value.Dispose();
                    }
                }

                this.list_games.Controls.Clear();
                controls.Clear();

                // make menu before games
#if false
                pkgManagerBtn = new GameControl();
                pkgManagerBtn.Width = list_games.Width;
                pkgManagerBtn.UpdateTitleText("Settings");
                //pkgManagerBtn.Image = Properties.Resources.nucleus;
                pkgManagerBtn.Image = Properties.Resources.splitscreenme;
                //pkgManagerBtn.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "⚙");
                pkgManagerBtn.Click += PkgManagerBtn_Click;
                this.list_games.Controls.Add(pkgManagerBtn);

                //HorizontalLineControl line = new HorizontalLineControl();
                //line.LineHorizontalPc = 100;
                //line.Width = list_games.Width;
                //line.LineHeight = 2;
                //line.LineColor = Color.FromArgb(255, 41, 45, 47);
                //this.list_games.Controls.Add(line);

                TitleSeparator sep = new TitleSeparator();
                sep.SetTitle("GAMES");
                sep.Height = 20;
                this.list_games.Controls.Add(sep);

                var ordered = gameManager.GetInstalledGamesOrdered();
                foreach (var pair in ordered) {
                    NewUserGame(pair.Value);
                }

                if (ordered.Count() == 0) {
                    appPage = AppPage.NoGamesInstalled;
                    GameControl con = new GameControl();
                    con.Click += Con_Click;
                    con.Width = list_games.Width;
                    con.UpdateTitleText("No games");
                    this.list_games.Controls.Add(con);
                }
#endif
            }

            // TODO: double-calling fixes some issues but is non-optimal
            DPI.DPIManager.ForceUpdate();
            DPI.DPIManager.ForceUpdate();

            UpdatePage();
            list_games.UpdateSizes();

#if false
            gameManager.User.Save();
#endif

            // auto-click pkg manager to not open with nothing selected
            PkgManagerBtn_Click(pkgManagerBtn, EventArgs.Empty);
            pkgManagerBtn?.RadioSelected();

            refreshingGames = false;
        }

        public GameControl NewUserGame(List<UserGameInfo> games) {
            // get all Repository Game Infos
#if false
            GameControl con = new GameControl();
            con.SetUserGames(games);
            con.Width = list_games.Width;
            con.Click += Game_Click;
            controls.Add(games[0].GameID, con);
            this.list_games.Controls.Add(con);

            gameManager.MetadataManager.GetIcon(games[0], (Bitmap bmp) => {
                con.Image = bmp;
            });


            return con;
#endif
            throw new NotImplementedException();
        }

        private void Game_Click(object sender, EventArgs e) {
            GameControl gameCon = (GameControl)sender;
            Selected = gameCon;

#if false
            var games = gameCon.UserGames;
            if (games.Count > 1) {
                // if there's more than 1 of the same game,
                // show the different game executables we can launch
                selectGameFolderPageControl.UpdateUsers(games, gameCon.Image);
                appPage = AppPage.SelectGameFolder;
            } else {
                appPage = AppPage.GameHandler;
                gamePageControl.ChangeSelectedGame(games[0]);
            }
#endif

            UpdatePage();
        }

        private void selectGameFolderPageControl_SelectedGame(UserGameInfo obj) {
            gamePageControl.ChangeSelectedGame(obj);

            appPage = AppPage.GameHandler;
            UpdatePage();
        }

        private void Con_Click(object sender, EventArgs e) {
            appPage = AppPage.NoGamesInstalled;
            UpdatePage();
        }

        private void PkgManagerBtn_Click(object sender, EventArgs e) {
            appPage = AppPage.PackageManager;
            UpdatePage();
        }

        private void UpdatePage() {
            selectGameFolderPageControl.Visible = false;
            handlerManagerControl.Visible = false;
            gamePageControl.Visible = false;
            noGamesInstalledPageControl.Visible = false;

            BasePageControl lastPage = currentPage;

            // game btns
            gamePageBrowserControl.Visible = false;

            switch (appPage) {
                case AppPage.SelectGameFolder:
                    ChangeTitle(selectGameFolderPageControl.Title, selectGameFolderPageControl.Image);
                    selectGameFolderPageControl.Visible = true;
                    currentPage = selectGameFolderPageControl;
                    break;
                case AppPage.NoGamesInstalled:
                    ChangeTitle(noGamesInstalledPageControl.Title, noGamesInstalledPageControl.Image);
                    noGamesInstalledPageControl.Visible = true;
                    currentPage = noGamesInstalledPageControl;
                    break;
                case AppPage.GameHandler:
                    gamePageControl.Visible = true;
                    gamePageBrowserControl.Visible = true;
                    currentPage = gamePageControl;
                    break;
                case AppPage.PackageManager:
                    ChangeTitle(handlerManagerControl.Title, handlerManagerControl.Image);
                    handlerManagerControl.Visible = true;
                    currentPage = handlerManagerControl;
                    break;
            }

            if (lastPage != null && currentPage != lastPage) {
                lastPage.UserLeft();
            }

            UpdatePageSizes();
        }

        private BasePageControl GetPageControl(AppPage appPage) {
            switch (appPage) {
                case AppPage.NoGamesInstalled:
                    return this.noGamesInstalledPageControl;
                case AppPage.GameHandler:
                    return this.gamePageControl;
                case AppPage.PackageManager:
                    return this.handlerManagerControl;
                default:
                    return null;
            }
        }

        private void UpdatePageSizes() {
            // dont curse me
            BasePageControl page = GetPageControl(this.appPage);
            int listWidth = list_games.Size.Width;
            int panelWidth = panel_formContent.Size.Width;
            bool changed = false;

            // fix 1 px border looking weird with the game handler paging system
            if (appPage == AppPage.GameHandler) {
                panel_allPages.Left = listWidth - 1;
            } else {
                panel_allPages.Left = listWidth;
            }

            if (page != null) {
                page.Location = new Point(0, 0);

                if (page.RequiredTitleBarWidth > 0) {
                    changed = true;

                    // Page requested a part of our title bar area to render things,
                    // so we squish the title bar and update everyones sizes
                    panel_pageTitle.Width = panelWidth - listWidth - page.RequiredTitleBarWidth;
                    panel_pageTitle.Left = listWidth + page.RequiredTitleBarWidth;
                    panel_allPages.Width = panelWidth - listWidth;
                    panel_allPages.Height = panel_formContent.Height - titleBarControl.Height;
                    panel_allPages.Top = titleBarControl.Height;

                    // Force bring the title bar to front, so the now full size panel_allPages
                    // doesnt show on top
                    panel_pageTitle.BringToFront();
                }

                page.Size = panel_allPages.Size;
            }

            if (!changed) {
                panel_pageTitle.Width = panelWidth - listWidth;
                panel_pageTitle.Left = listWidth;
                panel_allPages.Width = panelWidth - listWidth;
                panel_allPages.Height = panel_formContent.Height - panel_pageTitle.Height - titleBarControl.Height;
                panel_allPages.Top = panel_pageTitle.Height + titleBarControl.Height;
            }
        }

        public void ChangeTitle(string newTitle, Image icon = null) {
            gameNameControl.UpdateText(newTitle);
            gameNameControl.Image = icon;
        }

        public void ChangeGameInfo(UserGameInfo userGameInfo) {
            gameNameControl.GameInfo = userGameInfo;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            RefreshGames();

            DPI.DPIManager.ForceUpdate();
        }

        private void GetIcon(object state) {
            UserGameInfo game = (UserGameInfo)state;
            Icon icon = Shell32Interop.GetIcon(game.ExePath, false);

            Bitmap bmp = icon.ToBitmap();
            icon.Dispose();
            game.Icon = bmp;

            lock (controls) {
                GameControl control;
#if false
                if (controls.TryGetValue(game.GameID, out control)) {
                    control.Invoke((Action)delegate () {
                        control.Image = game.Icon;
                    });
                }
#endif
            }
        }

        private void Overlay_OnStop() {
            overlay.DisableOverlay();
        }

        private void btnShowTaskbar_Click(object sender, EventArgs e) {
            User32Util.ShowTaskBar();
        }
    }
}
