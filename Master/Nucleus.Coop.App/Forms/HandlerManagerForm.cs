using Nucleus.Coop.App.Controls;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Api;
using Nucleus.Gaming.Coop.Interop;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Forms
{
    /// <summary>
    /// Form that manages forms, allowing installation of new handlers 
    /// and management of installed
    /// </summary>
    public partial class HandlerManagerForm : BaseForm
    {
        private List<CancellationTokenSource> pendingTasks;
        private GameHandler currentHandler;

        public HandlerManagerForm()
        {
            pendingTasks = new List<CancellationTokenSource>();

            InitializeComponent();

            UpdateTabs();
        }

        //private async Task LoadBrowseTab()
        //{
        //    if (apiConnection == null)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        RequestResult<List<Game>> games = await apiConnection.ListIntGames();

        //        this.Invoke((Action)(() =>
        //        {
        //            list_left.Controls.Clear();
        //            if (games.Success)
        //            {
        //                var data = games.Data.OrderBy(c => c.name);

        //                foreach (var game in data)
        //                {
        //                    HandlerInfoControl handlerControl = new HandlerInfoControl();
        //                    handlerControl.OnSelected += Browse_Game_OnSelected;
        //                    handlerControl.SetHandler(game);
        //                    list_left.Controls.Add(handlerControl);
        //                }
        //            }
        //            else
        //            {
        //                if (games.LogData.Contains("403"))
        //                {
        //                    // forbidden, need to login again!
        //                    Program.Login(apiConnection);
        //                }
        //            }

        //            ChangeTabBtnStates(true);
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLine(ex);

        //        this.Invoke((Action)(() =>
        //        {
        //            ChangeTabBtnStates(true);
        //        }));
        //    }
        //}

        private void LoadInstalledTab()
        {
            var gm = GameManager.Instance;
            var handlers = gm.User.InstalledHandlers;

            list_left.Controls.Clear();
            foreach (var handler in handlers)
            {
                HandlerInfoControl handlerControl = new HandlerInfoControl();
                handlerControl.OnSelected += Installed_Handler_OnSelected;
                handlerControl.SetHandler(handler);
                list_left.Controls.Add(handlerControl);
            }
        }



        private void txt_gameName_OnTextChanged(object sender, EventArgs e)
        {
            //Search(); // lol no api cant handle
        }

        /// <summary>
        /// 
        /// </summary>
        private void Search() {
            string text = txt_gameName.TextBox.Text;
            if (string.IsNullOrWhiteSpace(text) ||
                text.Length < 3) {
                return;
            }

            var ts = new CancellationTokenSource();
            // make sure no other operation updates the screen
            pendingTasks.ForEach(c => c.Cancel());
            pendingTasks.Clear();

            Task task = Task.Run(async () => {
                await SearchGame(text);
            }, ts.Token);

            pendingTasks.Add(ts);
        }

        private async Task SearchGame(string text) {
            try {
                //if (apiConnection == null) {
                //    return;
                //}

                //RequestResult<IgdbGames> games = await apiConnection.SearchExtGame(text);

                //if (games.Success) {
                //    this.Invoke((Action)(() => {
                //        list_left.Controls.Clear();

                //        for (int i = 0; i < games.Data.Count; i++) {
                //            IgdbGame game = games.Data[i];
                //            if (game.category != "0") {
                //                continue;
                //            }

                //            HandlerInfoControl handlerControl = new HandlerInfoControl();
                //            handlerControl.OnSelected += Browse_Game_OnSelected;
                //            handlerControl.SetHandler(game);
                //            list_left.Controls.Add(handlerControl);
                //        }
                //        list_left.UpdateSizes();
                //    }));
                //} else {
                //    // failed? 
                //    if (games.LogData.Contains("403")) {
                //        // forbidden, need to login again!
                //        Program.Login(apiConnection);
                //    }
                //}
            } catch (Exception exception) {

            }
        }

        //private void Browse_Game_OnSelected(HandlerInfoControl obj)
        //{
        //    Game g = obj.Game;
        //    if (g == null)
        //    {
        //        return;
        //    }

        //    // async search for available handlers for this game
        //    try
        //    {
        //        Task task = Task.Run(async () =>
        //        {
        //            RequestResult<Game> game = await apiConnection.GetSpecificGameWithHandlers(g.id.ToString(CultureInfo.InvariantCulture));

        //            if (game.Success)
        //            {
        //                this.Invoke((Action)(() =>
        //                {
        //                    // list handlers
        //                    var handlers = game.Data.handlers;
        //                    if (handlers == null)
        //                    {
        //                        return;
        //                    }

        //                    for (int i = 0; i < handlers.Count; i++)
        //                    {
        //                        var handler = handlers[i];

        //                        HandlerInfoControl handlerControl = new HandlerInfoControl();
        //                        //handlerControl.OnSelected += Browse_HandlerControl_OnSelected;
        //                        handlerControl.OnSelected += Browse_Handler_OnSelected;
        //                        handlerControl.SetHandler(handler);

        //                        list_handlers.Controls.Add(handlerControl);
        //                    }
        //                }));
        //            }
        //            else
        //            {
        //                // failed? 
        //                if (game.LogData.Contains("403"))
        //                {
        //                    // forbidden, need to login again!
        //                    Program.Login(apiConnection);
        //                }
        //            }

        //        });
        //    }
        //    catch (Exception exception)
        //    {

        //    }
        //}

        //private void Browse_Handler_OnSelected(HandlerInfoControl obj)
        //{
        //    // download handler information
        //    GameHandler h = obj.Handler;
        //    if (h == null)
        //    {
        //        return;
        //    }

        //    currentHandler = h;
        //    label_gameHandlerName.Text = h.name;
        //    label_gameHandlerDescription.Text = h.details;

        //    try
        //    {
        //        Task task = Task.Run(async () =>
        //        {
        //            RequestResult<GameHandler> handler = await apiConnection.GetGameHandler(h.id.ToString(CultureInfo.InvariantCulture));

        //            if (handler.Success)
        //            {
        //                this.Invoke((Action)(() =>
        //                {
        //                    var data = handler.Data;
        //                    var packages = data.packages.OrderBy(c => c.version);
        //                    combo_gameHandlerVersions.DataSource = packages;
        //                    btn_gameHandlerInstall.Enabled = true;
        //                }));
        //            }
        //            else
        //            {
        //                // failed? 
        //                if (handler.LogData.Contains("403"))
        //                {
        //                    // forbidden, need to login again!
        //                    Program.Login(apiConnection);
        //                }
        //            }

        //        });
        //    }
        //    catch (Exception exception)
        //    {

        //    }
        //}

        private GameHandlerBaseMetadata currentMetadata;
        private void Installed_Handler_OnSelected(HandlerInfoControl obj)
        {
            GameHandlerBaseMetadata metadata = obj.Metadata;
            currentMetadata = metadata;
            if (currentMetadata == null)
            {
                return;
            }

            label_installedGameName.Text = metadata.Title;
            btn_uninstall.Enabled = true;
        }

        private void btn_Install_Click(object sender, EventArgs e)
        {
            object val = combo_gameHandlerVersions.SelectedValue;
            if (val == null || !(val is Package))
            {
                return;
            }

            Package package = (Package)val;

        }

        private void ChangeTabBtnStates(bool state)
        {
            radio_browse.Enabled = state;
            radio_installed.Enabled = state;
        }

        private void UpdateTabs()
        {
            panel_browse.Visible = radio_browse.Checked;
            panel_installed.Visible = radio_installed.Checked;

            list_left.Controls.Clear();

            btn_gameHandlerInstall.Enabled = false;

            if (radio_browse.Checked)
            {
                //ChangeTabBtnStates(false);
                //Task.Run(LoadBrowseTab);
            }
            else
            {
                LoadInstalledTab();
            }
        }

        private void radio_browse_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTabs();
        }

        private void txt_gameName_TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();

                list_left.Focus();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_uninstall_Click(object sender, EventArgs e)
        {
            if (currentMetadata == null)
            {
                return;
            }

            string path = PackageManager.GetBaseInstallPath(this.currentMetadata);
            Directory.Delete(path, true);
            GameManager.Instance.RebuildGameDb();

            // refresh
            UpdateTabs();
        }

        private void btn_installPkg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Nucleus Package Files|*.nc";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string path = open.FileName;
                    GameManager.Instance.RepoManager.InstallPackage(path);
                    UpdateTabs();
                }
            }
        }
    }
}
