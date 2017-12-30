using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Package;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    /// <summary>
    /// Central UI class to the Nucleus Coop application
    /// </summary>
    public partial class MainForm : BaseForm
    {
        private int currentStepIndex;
        private bool formClosing;
        private ContentManager content;

        private GameManager gameManager;
        private Dictionary<UserGameInfo, GameControl> controls;

        private SearchDisksForm form;

        private GameControl selectedControl;

        private GameHandlerMetadata selectedHandler;
        private HandlerData handlerData;
        private GenericGameHandler handler;

        private GameProfile currentProfile;
        private bool noGamesPresent;
        private List<UserInputControl> stepsList;
        private UserInputControl currentStep;

        private PositionsControl positionsControl;
        private PlayerOptionsControl optionsControl;
        private JSUserInputControl jsControl;

        private Thread handlerThread;
        private CoopConfigInfo configFile;

        public MainForm(string[] args)
        {
            InitializeComponent();

            this.Text = string.Format("Nucleus Coop v{0}", Globals.Version);

            controls = new Dictionary<UserGameInfo, GameControl>();

            configFile = new CoopConfigInfo("config.json");
            gameManager = new GameManager(configFile);

            positionsControl = new PositionsControl();
            optionsControl = new PlayerOptionsControl();
            jsControl = new JSUserInputControl();

            positionsControl.OnCanPlayUpdated += StepCanPlay;
            optionsControl.OnCanPlayUpdated += StepCanPlay;
            jsControl.OnCanPlayUpdated += StepCanPlay;

            // selects the list of games, so the buttons look equal
            list_Games.Select();
            list_Games.AutoScroll = false;
            list_Games.SelectedChanged += list_Games_SelectedChanged;
            //int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            //list_Games.Padding = new Padding(0, 0, vertScrollWidth, 0);

            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];
                    if (string.IsNullOrEmpty(argument))
                    {
                        continue;
                    }

                    string extension = Path.GetExtension(argument);
                    if (extension.ToLower().EndsWith("nc"))
                    {
                        // try installing
                        gameManager.RepoManager.InstallPackage(argument);
                    }
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(1070, 740);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.TopMost = true;
            this.BringToFront();

            System.Diagnostics.Debug.WriteLine("Got Focus");
        }

        protected override void WndProc(ref Message m)
        {
            //int msg = m.Msg;
            //LogManager.Log(msg.ToString());

            base.WndProc(ref m);
        }

        public void RefreshGames()
        {
            lock (controls)
            {
                foreach (var con in controls)
                {
                    if (con.Value != null)
                    {
                        con.Value.Dispose();
                    }
                }
                this.list_Games.Controls.Clear();
                this.list_Handlers.Controls.Clear();
                controls.Clear();

                List<GameHandlerMetadata> handlers = gameManager.User.InstalledHandlers;
                for (int i = 0; i < handlers.Count; i++)
                {
                    GameHandlerMetadata handler = handlers[i];
                    NewGameHandler(handler);
                }

                List<UserGameInfo> games = gameManager.User.Games;
                for (int i = 0; i < games.Count; i++)
                {
                    UserGameInfo game = games[i];
                    NewUserGame(game);
                }

                if (games.Count == 0)
                {
                    noGamesPresent = true;
                    GameControl con = new GameControl(null);
                    con.Width = list_Games.Width;
                    con.Text = "No games";
                    this.list_Games.Controls.Add(con);
                }

                if (handlers.Count == 0)
                {
                    noGamesPresent = true;
                    HandlerControl con = new HandlerControl(null);
                    con.Width = list_Games.Width;
                    con.Text = "No handlers";
                    this.list_Handlers.Controls.Add(con);
                }
            }

            DPIManager.ForceUpdate();
            gameManager.User.Save();
        }

        public void NewGameHandler(GameHandlerMetadata metadata)
        {
            if (noGamesPresent)
            {
                noGamesPresent = false;
                RefreshGames();
                return;
            }

            // get all Repository Game Infos
            HandlerControl con = new HandlerControl(metadata);
            con.Width = list_Games.Width;
            this.list_Handlers.Controls.Add(con);
        }

        public void NewUserGame(UserGameInfo game)
        {
            if (!game.IsGamePresent())
            {
                return;
            }

            if (noGamesPresent)
            {
                noGamesPresent = false;
                RefreshGames();
                return;
            }

            // get all Repository Game Infos
            GameControl con = new GameControl(game);
            con.Width = list_Games.Width;

            controls.Add(game, con);
            this.list_Games.Controls.Add(con);

            ThreadPool.QueueUserWorkItem(GetIcon, game);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RefreshGames();

            DPIManager.ForceUpdate();
        }

        private void GetIcon(object state)
        {
            UserGameInfo game = (UserGameInfo)state;
            Icon icon = Shell32.GetIcon(game.ExePath, false);

            Bitmap bmp = icon.ToBitmap();
            icon.Dispose();
            game.Icon = bmp;

            lock (controls)
            {
                if (controls.ContainsKey(game))
                {
                    GameControl control = controls[game];
                    control.Invoke((Action)delegate ()
                    {
                        control.Image = game.Icon;
                    });
                }
            }
        }

        private GameHandlerMetadata[] currentHandlers;

        private void list_Games_SelectedChanged(Control arg1, Control arg2)
        {
            selectedControl = (GameControl)arg1;

            if (selectedControl.UserGameInfo == null)
            {
                return;
            }

            panel_Steps.Visible = true;

            UserGameInfo userGameInfo = selectedControl.UserGameInfo;
            string gameId = selectedControl.UserGameInfo.GameID;

            GameHandlerMetadata[] handlers = gameManager.RepoManager.GetInstalledHandlers(gameId);
            if (handlers.Length == 0)
            {
                // uninstalled package perhaps?
                return;
            }

            currentHandlers = handlers;

            combo_Handlers.DataSource = handlers;
            combo_Handlers.SelectedIndex = 0;
        }

        private void combo_Handlers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Handlers.SelectedIndex == -1)
            {
                return;
            }

            selectedHandler = currentHandlers[combo_Handlers.SelectedIndex];
            handlerData = gameManager.RepoManager.ReadHandlerDataFromInstalledPackage(selectedHandler);

            btn_Play.Enabled = false;

            stepsList = new List<UserInputControl>();
            stepsList.Add(positionsControl);
            stepsList.Add(optionsControl);
            if (handlerData.CustomSteps != null)
            {
                for (int i = 0; i < handlerData.CustomSteps.Count; i++)
                {
                    stepsList.Add(jsControl);
                }
            }

            currentProfile = new GameProfile();
            currentProfile.InitializeDefault(handlerData);

            gameNameControl.GameInfo = selectedControl.UserGameInfo;

            if (content != null)
            {
                content.Dispose();
            }

            // content manager is shared withing the same game
            content = new ContentManager(selectedHandler, handlerData);
            GoToStep(0);
        }


        private void EnablePlay()
        {
            btn_Play.Enabled = true;
        }

        private void StepCanPlay(UserControl obj, bool canProceed, bool autoProceed)
        {
            if (!canProceed)
            {
                btn_Next.Enabled = false;
                return;
            }

            if (currentStepIndex + 1 > stepsList.Count - 1)
            {
                EnablePlay();
                return;
            }

            if (autoProceed)
            {
                GoToStep(currentStepIndex + 1);
            }
            else
            {
                btn_Next.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GoToStep(currentStepIndex + 1);
        }

        private void KillCurrentStep()
        {
            currentStep?.Ended();
            this.panel_Steps.Controls.Clear();
        }

        private void GoToStep(int step)
        {
            btn_Previous.Enabled = step > 0;
            if (step >= stepsList.Count)
            {
                return;
            }

            if (step >= 2)
            {
                // Custom steps
                List<CustomStep> customSteps = handlerData.CustomSteps;
                int customStepIndex = step - 2;
                CustomStep customStep = customSteps[0];

                if (customStep.UpdateRequired != null)
                {
                    customStep.UpdateRequired();
                }

                if (customStep.Required)
                {
                    jsControl.CustomStep = customStep;
                    jsControl.Content = content;
                }
                else
                {
                    EnablePlay();
                    return;
                }
            }

            KillCurrentStep();

            currentStepIndex = step;
            currentStep = stepsList[step];
            currentStep.Size = panel_Steps.Size;
            currentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            currentStep.Initialize(handlerData, selectedControl.UserGameInfo, currentProfile);

            btn_Next.Enabled = currentStep.CanProceed && step != stepsList.Count - 1;

            panel_Steps.Controls.Add(currentStep);
            currentStep.Size = panel_Steps.Size; // for some reason this line must exist or the PositionsControl get messed up

            lbl_StepTitle.Text = currentStep.Title;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            formClosing = true;
            if (handler != null)
            {
                handler.End();
            }
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler.End();
                SetBtnToPlay();
                return;
            }

            btn_Play.Visible = false;
            btn_Play.Text = "S T O P";

            handler = gameManager.MakeHandler(handlerData);
            handler.Initialize(handlerData, selectedControl.UserGameInfo, GameProfile.CleanClone(currentProfile));
            handler.Ended += handler_Ended;

            gameManager.Play(handler);
            if (handler.TimerInterval > 0)
            {
                handlerThread = new Thread(UpdateGameManager);
                handlerThread.Start();
            }

            WindowState = FormWindowState.Minimized;
        }

        private void SetBtnToPlay()
        {
            btn_Play.Visible = true;
            btn_Play.Text = "P L A Y";
        }

        private void handler_Ended()
        {
            handler = null;
            if (handlerThread != null)
            {
                handlerThread.Abort();
                handlerThread = null;
            }
            Invoke(new Action(SetBtnToPlay));
        }

        private void UpdateGameManager(object state)
        {
            for (; ; )
            {
                try
                {
                    if (gameManager == null || formClosing || handler == null)
                    {
                        break;
                    }

                    string error = gameManager.Error;
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        handler_Ended();
                        return;
                    }

                    handler.Update(handler.TimerInterval);
                    Thread.Sleep(TimeSpan.FromMilliseconds(handler.TimerInterval));
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch { }
            }
        }

        private void arrow_Back_Click(object sender, EventArgs e)
        {
            currentStepIndex--;
            if (currentStepIndex < 0)
            {
                currentStepIndex = 0;
                return;
            }
            GoToStep(currentStepIndex);
        }

        private void arrow_Next_Click(object sender, EventArgs e)
        {
            currentStepIndex = Math.Min(currentStepIndex++, stepsList.Count - 1);
            GoToStep(currentStepIndex);
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Game Executable Files|*.exe";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string path = open.FileName;

                    List<GameHandlerMetadata> allGames = gameManager.User.InstalledHandlers;

                    GameList list = new GameList(allGames);
                    DPIManager.ForceUpdate();

                    if (list.ShowDialog() == DialogResult.OK)
                    {
                        GameHandlerMetadata selected = list.Selected;
                        UserGameInfo game = gameManager.TryAddGame(path, list.Selected);

                        if (game == null)
                        {
                            MessageBox.Show("Game already in your library!");
                        }
                        else
                        {
                            MessageBox.Show("Game accepted as ID " + game.GameID);
                            RefreshGames();
                        }
                    }
                }
            }
        }

        private void btnAutoSearch_Click(object sender, EventArgs e)
        {
            if (form != null)
            {
                return;
            }

            form = new SearchDisksForm(this);
            //DPIManager.AddForm(form);

            form.FormClosed += Form_FormClosed;
            form.Show();
            SetUpForm(form);

            DPIManager.ForceUpdate();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            form = null;
        }

        private void btnShowTaskbar_Click(object sender, EventArgs e)
        {
            User32Util.ShowTaskBar();
        }

        private void btn_Install_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Multiselect = true;
                open.Filter = "Nucleus Coop Package Files|*.nc";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string[] paths = open.FileNames;
                    for (int i = 0; i < paths.Length; i++)
                    {
                        gameManager.RepoManager.InstallPackage(paths[i]);
                    }

                    RefreshGames();
                }
            }
        }
    }
}
