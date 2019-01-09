using Newtonsoft.Json;
using Nucleus.Coop.App.Controls;
using Nucleus.Coop.Controls;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Coop.Interop;
using Nucleus.Gaming.Package;
using Nucleus.Gaming.Platform.Windows;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Forms {
    /// <summary>
    /// Central UI class to the Nucleus Coop application
    /// </summary>
    public partial class MainForm : BaseForm {
        private GameManager gameManager;
        private Dictionary<UserGameInfo, GameControl> controls;

        private GameRunningOverlay overlay;

        private HandlerManagerForm pkgManager;
        private AppPage appPage = AppPage.None;
        private bool noGamesPresent;

        protected override Size DefaultSize {
            get {
                return new Size(1070, 740);
            }
        }

        public GamePageBrowserControl BrowserBtns {
            get { return this.gamePageBrowserControl1; }
        }

        public static MainForm Instance { get; private set; }

        public MainForm(string[] args, GameManager gameManager) {
            this.gameManager = gameManager;
            Instance = this;

            InitializeComponent();

            this.FormContent = this.panel_formContent;

            overlay = new GameRunningOverlay();
            overlay.OnStop += Overlay_OnStop;

            this.titleBarControl1.Text = string.Format("Nucleus Coop v{0}", Globals.Version);

            controls = new Dictionary<UserGameInfo, GameControl>();

            // selects the list of games, so the buttons look equal
            list_games.Select();
            list_games.AutoScroll = false;
            //list_games.SelectedChanged += list_Games_SelectedChanged;

            if (args != null) {
                for (int i = 0; i < args.Length; i++) {
                    string argument = args[i];
                    if (string.IsNullOrEmpty(argument)) {
                        continue;
                    }

                    string extension = Path.GetExtension(argument);
                    if (extension.ToLower().EndsWith("nc")) {
                        // try installing the package in the arguments if user allows it
                        if (MessageBox.Show("Would you like to install " + argument + "?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                            gameManager.RepoManager.InstallPackage(argument);
                        }
                    }
                }
            }

            if (!gameManager.User.Options.RequestedToAssociateFormat) {
                gameManager.User.Options.RequestedToAssociateFormat = true;

                //if (MessageBox.Show("Would you like to associate Nucleus Package Files (*.nc) and nuke:// links to the application?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                string startLocation = Process.GetCurrentProcess().MainModule.FileName;
                if (!RegistryUtil.SetAssociation(".nc", "NucleusCoop", "Nucleus Package Files", startLocation)) {
                    //MessageBox.Show("Failed to set association");
                    //gameManager.User.Options.RequestedToAssociateFormat = false;
                }
                RegistryUtil.RegisterUriScheme();

                gameManager.User.Save();
            }
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

        private GameControl pkgManagerBtn;

        public void RefreshGames() {

            lock (controls) {
                foreach (var con in controls) {
                    if (con.Value != null) {
                        con.Value.Dispose();
                    }
                }

                this.list_games.Controls.Clear();
                controls.Clear();

                List<GameHandlerMetadata> handlers = gameManager.User.InstalledHandlers;
                for (int i = 0; i < handlers.Count; i++) {
                    GameHandlerMetadata handler = handlers[i];
                    NewGameHandler(handler);
                }

                // make menu before games
                pkgManagerBtn = new GameControl();
                pkgManagerBtn.Width = list_games.Width;
                pkgManagerBtn.TitleText = "Package Manager";
                pkgManagerBtn.Image = Properties.Resources.nucleus;
                pkgManagerBtn.Click += PkgManagerBtn_Click;
                this.list_games.Controls.Add(pkgManagerBtn);

                //GameControl gameManagerBtn = new GameControl(null);
                //gameManagerBtn.Width = list_games.Width;
                //gameManagerBtn.TitleText = "Game Manager";
                //gameManagerBtn.Click += GameManagerBtn_Click;
                ////gameManagerBtn.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🔍");
                //gameManagerBtn.Image = FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "🎮");
                //this.list_games.Controls.Add(gameManagerBtn);

                //HorizontalLineControl line = new HorizontalLineControl();
                //line.LineHorizontalPc = 100;
                //line.Width = list_games.Width;
                //line.LineHeight = 2;
                //line.LineColor = Color.FromArgb(255, 41, 45, 47);
                //this.list_games.Controls.Add(line);

                TitleSeparator sep = new TitleSeparator();
                sep.SetTitle("GAMES");
                this.list_games.Controls.Add(sep);

                List<UserGameInfo> games = gameManager.User.Games;
                for (int i = 0; i < games.Count; i++) {
                    UserGameInfo game = games[i];
                    GameControl gameControl = NewUserGame(game);
                }

                if (games.Count == 0) {
                    noGamesPresent = true;
                    appPage = AppPage.NoGamesInstalled;
                    GameControl con = new GameControl();
                    con.Click += Con_Click;
                    con.Width = list_games.Width;
                    con.TitleText = "No games";
                    this.list_games.Controls.Add(con);
                }
            }

            DPIManager.ForceUpdate();
            UpdatePage();

            gameManager.User.Save();

            // auto-click pkg manager to not open with nothing selected
            PkgManagerBtn_Click(pkgManagerBtn, EventArgs.Empty);
            pkgManagerBtn.RadioSelected();
        }

        public GameControl NewUserGame(UserGameInfo game) {
            if (!game.IsGamePresent()) {
                return null;
            }

            if (noGamesPresent) {
                noGamesPresent = false;
                RefreshGames();
                return null;
            }

            // get all Repository Game Infos
            GameControl con = new GameControl();
            con.SetUserGame(game);
            con.Width = list_games.Width;
            con.Click += Game_Click;
            controls.Add(game, con);
            this.list_games.Controls.Add(con);

            ThreadPool.QueueUserWorkItem(GetIcon, game);

            return con;
        }

        private void Game_Click(object sender, EventArgs e) {
            GameControl gameCon = (GameControl)sender;
            gamePageControl1.ChangeSelectedGame(gameCon.UserGameInfo);

            appPage = AppPage.GameHandler;
            UpdatePage();
        }

        private void GameManagerBtn_Click(object sender, EventArgs e) {
            appPage = AppPage.GameManager;
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
            handlerManagerControl1.Visible = false;
            gamePageControl1.Visible = false;
            noGamesInstalledPageControl1.Visible = false;
            gameManagerPageControl1.Visible = false;

            // game btns
            gamePageBrowserControl1.Visible = false;

            switch (appPage) {
                case AppPage.NoGamesInstalled:
                    ChangeTitle(noGamesInstalledPageControl1.Title, noGamesInstalledPageControl1.Image);
                    noGamesInstalledPageControl1.Visible = true;
                    break;
                case AppPage.GameHandler:
                    gamePageControl1.Visible = true;
                    gamePageBrowserControl1.Visible = true;
                    break;
                case AppPage.PackageManager:
                    ChangeTitle(handlerManagerControl1.Title, handlerManagerControl1.Image);
                    handlerManagerControl1.Visible = true;
                    break;
                case AppPage.GameManager:
                    ChangeTitle("Game Manager");
                    break;
            }

            UpdatePageSizes();
        }

        private BasePageControl GetPageControl(AppPage appPage) {
            switch (appPage) {
                case AppPage.NoGamesInstalled:
                    return this.noGamesInstalledPageControl1;
                case AppPage.GameHandler:
                    return this.gamePageControl1;
                case AppPage.PackageManager:
                    return this.handlerManagerControl1;
                case AppPage.GameManager:
                    return this.gameManagerPageControl1;
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
                    panel_allPages.Height = panel_formContent.Height - titleBarControl1.Height;
                    panel_allPages.Top = titleBarControl1.Height;

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
                panel_allPages.Height = panel_formContent.Height - panel_pageTitle.Height - titleBarControl1.Height;
                panel_allPages.Top = panel_pageTitle.Height + titleBarControl1.Height;
            }
        }

        public void ChangeTitle(string newTitle, Image icon = null) {
            gameNameControl.UpdateText(newTitle);
            gameNameControl.Image = icon;
        }

        public void ChangeGameInfo(UserGameInfo userGameInfo) {
            gameNameControl.GameInfo = userGameInfo;
        }

        public void NewGameHandler(GameHandlerMetadata metadata) {
            if (noGamesPresent) {
                noGamesPresent = false;
                RefreshGames();
                return;
            }

            // get all Repository Game Infos
            //this.combo_handlers.Items.Add(metadata);

            //HandlerControl con = new HandlerControl(metadata);
            //con.Width = list_Games.Width;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            RefreshGames();

            DPIManager.ForceUpdate();
        }

        private void GetIcon(object state) {
            UserGameInfo game = (UserGameInfo)state;
            Icon icon = Shell32Interop.GetIcon(game.ExePath, false);

            Bitmap bmp = icon.ToBitmap();
            icon.Dispose();
            game.Icon = bmp;

            lock (controls) {
                if (controls.ContainsKey(game)) {
                    GameControl control = controls[game];
                    control.Invoke((Action)delegate () {
                        control.Image = game.Icon;
                    });
                }
            }
        }

        private void Overlay_OnStop() {
            overlay.DisableOverlay();
        }

        private void btnShowTaskbar_Click(object sender, EventArgs e) {
            User32Util.ShowTaskBar();
        }

        private void imgBtn_handlers_Click(object sender, EventArgs e) {
            if (pkgManager != null) {
                if (!pkgManager.IsDisposed) {
                    return;
                }
            }

            pkgManager = new HandlerManagerForm();
            pkgManager.Show();
        }
    }
}
