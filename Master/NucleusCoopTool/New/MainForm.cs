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
        private GameManager gameManager;
        private Dictionary<UserGameInfo, GameControl> controls;

        public MainForm()
        {
            InitializeComponent();

            controls = new Dictionary<UserGameInfo, GameControl>();
            gameManager = new GameManager();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (gameManager.User.Games.Count == 0)
            {
                if (MessageBox.Show("You have no games on your list. Would you like to automatically search your disk?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ScanExes();
                }
            }

            List<UserGameInfo> games = gameManager.User.Games;
            for (int i = 0; i < games.Count; i++)
            {
                UserGameInfo game = games[i];

                GameControl con = new GameControl();
                con.Game = game;
                con.Width = list_Games.Width;

                controls.Add(game, con);

                con.Text = game.Game.GameName;
                ThreadPool.QueueUserWorkItem(GetIcon, game);

                this.list_Games.Controls.Add(con);
            }
        }

        private void GetIcon(object state)
        {
            UserGameInfo game = (UserGameInfo)state;
            Icon icon = Shell32.GetIcon(game.ExePath, false);

            Bitmap bmp = icon.ToBitmap();
            icon.Dispose();
            game.Icon = bmp;

            // this aint pretty
            GameControl control = controls[game];
            control.Image = game.Icon;
        }

        public void ScanExes()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Stopwatch stop = new Stopwatch();

            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo d = drives[i];

                if (d.DriveFormat != "NTFS")
                {
                    continue;
                }

                LogManager.Log("> Searching drive {0} for game executables", d.Name);

                stop.Reset();
                stop.Start();

                Dictionary<ulong, FileNameAndParentFrn> mDict = new Dictionary<ulong, FileNameAndParentFrn>();
                MFTReader mft = new MFTReader();
                mft.Drive = d.RootDirectory.FullName;

                mft.EnumerateVolume(out mDict, new string[] { ".exe" });
                foreach (KeyValuePair<UInt64, FileNameAndParentFrn> entry in mDict)
                {
                    FileNameAndParentFrn file = (FileNameAndParentFrn)entry.Value;

                    string name = file.Name;
                    string lower = name.ToLower();

                    GameInfo game;
                    if (gameManager.GameInfos.TryGetValue(lower, out game))
                    {
                        string path = mft.GetFullPath(file);
                        LogManager.Log("Found game: {0}, full path: {1}", game.GameName, path);

                        UserGameInfo info = new UserGameInfo();
                        info.InitializeDefault(game, path);
                        gameManager.User.Games.Add(info);
                    }
                }

                stop.Stop();
                LogManager.Log("> Took {0} seconds to search drive {1}", stop.Elapsed.TotalSeconds.ToString("0.00"), d.Name);
            }

            gameManager.SaveUserProfile();
            gameManager.WaitSave();
        }

        private GameControl currentControl;
        private UserGameInfo currentGameInfo;
        private GameInfo currentGame;
        private GameProfile currentProfile;
        private Control currentStep;
        private int currentStepIndex;
        private IUserInputForm currentInputStep;

        private void list_Games_SelectedChanged(object arg1, Control arg2)
        {
            currentControl = (GameControl)arg1;
            currentGameInfo = currentControl.Game;
            currentGame = currentGameInfo.Game;

            btn_Play.Enabled = false;

            if (currentGame.Steps == null ||
                currentStepIndex == currentGame.Steps.Length)
            {
                // can play
                btn_Play.Enabled = true;
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

        private void GoToStep(int step)
        {
            currentStepIndex = step;
            if (step >= currentGame.Steps.Length)
            {
                return;
            }

            if (currentStep != null)
            {
                currentStep.Dispose();
                this.StepPanel.Controls.Remove(currentStep);
            }

            Type[] steps = currentGame.Steps;

            Type s = steps[step];
            currentStep = (Control)Activator.CreateInstance(s);
            this.StepPanel.Controls.Add(currentStep);

            currentStep.Size = StepPanel.Size;

            currentInputStep = (IUserInputForm)currentStep;
            currentInputStep.Initialize(currentGameInfo, currentProfile);
            currentInputStep.Proceed += inputs_Proceed;

            label_StepTitle.Text = currentInputStep.Title;
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

        private IGameHandler handler;

        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (handler != null)
            {
                return;
            }

            handler = gameManager.MakeHandler(currentGame);
            handler.Initialize(currentGameInfo, currentProfile);

            timer1.Interval = handler.TimerInterval;
            timer1.Enabled = true; 

            gameManager.Play(handler);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            handler.Update(timer1.Interval);
        }

    }
}
