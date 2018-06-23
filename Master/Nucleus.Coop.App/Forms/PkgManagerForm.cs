using Nucleus.Coop.App.Controls;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Api;
using Nucleus.Gaming.Coop.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Forms
{
    public partial class PkgManagerForm : BaseForm
    {
        private DomainWebApiConnection apiConnection;
        private List<CancellationTokenSource> pendingTasks;

        public PkgManagerForm(DomainWebApiConnection con)
        {
            this.apiConnection = con;
            pendingTasks = new List<CancellationTokenSource>();

            InitializeComponent();
        }

        private void txt_gameName_OnTextChanged(object sender, EventArgs e)
        {
            //Search(); // lol no api cant handle
        }

        private void Search()
        {
            if (apiConnection.IsOfflineMode)
            {
                return;
            }

            string text = txt_gameName.TextBox.Text;
            if (string.IsNullOrWhiteSpace(text) ||
                text.Length < 3)
            {
                return;
            }

            var ts = new CancellationTokenSource();
            // make sure no other operation updates the screen
            pendingTasks.ForEach(c => c.Cancel());
            pendingTasks.Clear();

            Task task = Task.Run(async () =>
            {
                await SearchGame(text);
            }, ts.Token);

            pendingTasks.Add(ts);
        }

        private async Task SearchGame(string text)
        {
            try
            {
                RequestResult<IgdbGames> games = await apiConnection.SearchExtGame(text);

                if (games.Success)
                {
                    this.Invoke((Action)(() =>
                    {
                        list_left.Controls.Clear();

                        for (int i = 0; i < games.Data.Count; i++)
                        {
                            IgdbGame game = games.Data[i];
                            if (game.category != "0")
                            {
                                continue;
                            }

                            HandlerInfoControl handlerControl = new HandlerInfoControl();
                            handlerControl.SetHandler(game);
                            list_left.Controls.Add(handlerControl);
                        }
                        list_left.UpdateSizes();
                    }));
                }
                else
                {
                    // failed? 
                    if (games.LogData.Contains("403"))
                    {
                        // forbidden, need to login again!
                        GameManager.Instance.User.LastToken = "";
                        GameManager.Instance.User.Save();

                        //Application.Restart(); // that's one way to do it
                        LoginForm loginForm = new LoginForm(apiConnection);
                        DPIManager.AddForm(loginForm);
                        DPIManager.ForceUpdate();

                        if (loginForm.ShowDialog() == DialogResult.OK)
                        {
                            // retry
                            await SearchGame(text);
                        }
                        else
                        {
                            // user didnt login, do nothing?
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.Invoke((Action)(() =>
                {
                }));
            }
        }

        private void ShowBrowse()
        {

        }

        private void UpdateTabs()
        {
            panel_browse.Visible = radio_browse.Checked;
            panel_installed.Visible = radio_installed.Checked;

            list_left.Controls.Clear();
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
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
