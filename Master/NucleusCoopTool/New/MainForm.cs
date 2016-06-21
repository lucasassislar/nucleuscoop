using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public partial class MainForm : BaseForm
    {
        private Size defaultSize = new Size(1070, 740);
        private Size startSize = new Size(275, 740);

        private GameManager gameManager;
        private Dictionary<UserGameInfo, GameControl> controls;

        private SearchDisksForm form;

        private GameControl currentControl;
        private UserGameInfo currentGameInfo;
        private GameInfo currentGame;
        private GameProfile currentProfile;
        private Control currentStep;
        private int currentStepIndex;
        private IUserInputForm currentInputStep;

        public MainForm()
        {
            InitializeComponent();

            controls = new Dictionary<UserGameInfo, GameControl>();
            gameManager = new GameManager();

            this.Size = startSize;
        }

        private bool noGamesPresent;

        public void RefreshGames()
        {
            foreach (var con in controls)
            {
                if (con.Value != null)
                {
                    con.Value.Dispose();
                }
            }

            controls.Clear();
            this.list_Games.Controls.Clear();

            List<UserGameInfo> games = gameManager.User.Games;
            for (int i = 0; i < games.Count; i++)
            {
                UserGameInfo game = games[i];
                NewUserGame(game);
            }

            if (games.Count == 0)
            {
                noGamesPresent = true;
                GameControl con = new GameControl();
                con.Width = list_Games.Width;
                con.Text = "No games";
                this.list_Games.Controls.Add(con);
            }
        }

        public void NewUserGame(UserGameInfo game)
        {
            if (noGamesPresent)
            {
                this.list_Games.Controls.Clear();
                noGamesPresent = false;
            }

            GameControl con = new GameControl();
            con.Game = game;
            con.Width = list_Games.Width;

            controls.Add(game, con);

            con.Text = game.Game.GameName;
            ThreadPool.QueueUserWorkItem(GetIcon, game);

            this.list_Games.Controls.Add(con);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RefreshGames();
        }

        private void GetIcon(object state)
        {
            UserGameInfo game = (UserGameInfo)state;
            Icon icon = Shell32.GetIcon(game.ExePath, false);

            Bitmap bmp = icon.ToBitmap();
            icon.Dispose();
            game.Icon = bmp;

            GameControl control = controls[game];
            control.Image = game.Icon;
        }

        private bool setSize = false;
        private void list_Games_SelectedChanged(object arg1, Control arg2)
        {
            currentControl = (GameControl)arg1;
            currentGameInfo = currentControl.Game;
            if (currentGameInfo == null)
            {
                return;
            }

            if (!setSize)
            {
                this.Size = defaultSize;
                setSize = true;
            }

            panelGameName.Visible = true;
            label_StepTitle.Visible = true;
            StepPanel.Visible = true;
            btnBack.Visible = true;
            btnNext.Visible = true;

            currentGame = currentGameInfo.Game;

            btn_Play.Enabled = false;

            if (currentGame.Steps == null ||
                currentStepIndex == currentGame.Steps.Length)
            {
                // can play
                btn_Play.Enabled = true;

                // remove the current step if there's one
                KillCurrentStep();

                btnBack.Visible = false;
                btnNext.Visible = false;
            }

            currentProfile = new GameProfile();
            currentProfile.InitializeDefault(currentGame);

            this.label_GameTitle.Text = currentGame.GameName;
            this.pic_Game.Image = currentGameInfo.Icon;

            Type[] steps = currentGame.Steps;
            if (steps != null && steps.Length > 0)
            {
                GoToStep(0);
            }
        }

        private void KillCurrentStep()
        {
            if (currentStep != null)
            {
                currentStep.Dispose();
                this.StepPanel.Controls.Clear();
            }
        }


        private void GoToStep(int step)
        {
            btnBack.Enabled = step > 0;

            currentStepIndex = step;
            if (step >= currentGame.Steps.Length)
            {
                btnNext.Enabled = false;
                return;
            }

            KillCurrentStep();

            Type[] steps = currentGame.Steps;

            Type s = steps[step];
            currentStep = (Control)Activator.CreateInstance(s);
            this.StepPanel.Controls.Add(currentStep);

            currentStep.Size = StepPanel.Size;

            currentInputStep = (IUserInputForm)currentStep;
            currentInputStep.Initialize(currentGameInfo, currentProfile);
            currentInputStep.Proceed += inputs_Proceed;

            label_StepTitle.Text = currentInputStep.Title;

            btnNext.Enabled = currentInputStep.CanPlay;
        }

        void inputs_Proceed()
        {
            currentStepIndex++;
            GoToStep(currentStepIndex);

            if (currentStepIndex == currentGame.Steps.Length ||
                currentInputStep.CanPlay)
            {
                // can play
                btn_Play.Enabled = true;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            formClosing = true;
        }

        private bool formClosing;
        private IGameHandler handler;

        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (handler != null)
            {
                return;
            }

            handler = gameManager.MakeHandler(currentGame);
            handler.Initialize(currentGameInfo, currentProfile);
            handler.Ended += handler_Ended;

            if (handler.TimerInterval > 0)
            {
                Thread t = new Thread(UpdateGameManager);
                t.Start();
            }

            gameManager.Play(handler);
        }

        private void handler_Ended()
        {
            handler = null;
        }

        private void UpdateGameManager(object state)
        {
            for (;;)
            {
                try
                {
                    if (gameManager == null || formClosing || handler == null)
                    {
                        break;
                    }

                    handler.Update(handler.TimerInterval);
                    Thread.Sleep(handler.TimerInterval);
                }
                catch
                {
                }
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

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Game Executable Files|*.exe";
                if (open.ShowDialog() == DialogResult.OK)
                {

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
            form.FormClosed += Form_FormClosed;
            form.Show();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            form = null;
        }
    }
}
