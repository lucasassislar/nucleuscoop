using Nucleus.Coop.App.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.App.Controls;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Controls;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Package;
using Nucleus.Gaming.Platform.Windows;
using Nucleus.Gaming.Platform.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls {
    public partial class GamePageControl : BasePageControl {
        public override int RequiredTitleBarWidth {
            get { return base.RequiredTitleBarWidth; }
            set {
                // only set if not on design mode
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) {
                    base.RequiredTitleBarWidth = value;
                }
            }
        }

        private UserInputControl currentStep;
        private int currentStepIndex;
        private UserGameInfo userGame;
        private ControlListBox list_handlers;
        private GameHandler handler;
        private HandlerDataManager handlerDataManager;
        private GameHandlerMetadata selectedHandler;
        private HandlerData handlerData;
        private Thread handlerThread;

        private GameProfile currentProfile;
        private List<UserInputControl> stepsList;

        private JSUserInputControl jsControl;
        private PositionsControl positionsControl;
        private PlayerOptionsControl optionsControl;

        private GameHandlerMetadata[] currentHandlers;

        private MainForm mainForm;
        private GamePageBrowserControl BrowserBtns { get { return mainForm.BrowserBtns; } }

        public GamePageControl() {
            InitializeComponent();

            list_handlers = new ControlListBox();
            list_handlers.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            positionsControl = new PositionsControl();
            optionsControl = new PlayerOptionsControl();
            jsControl = new JSUserInputControl();

            positionsControl.OnCanPlayUpdated += StepCanPlay;
            optionsControl.OnCanPlayUpdated += StepCanPlay;
            jsControl.OnCanPlayUpdated += StepCanPlay;
        }

        public void ChangeSelectedGame(UserGameInfo userGame) {
            this.userGame = userGame;

            panel_steps.Controls.Clear();
            string gameId = userGame.GameID;

            if (mainForm == null) {
                mainForm = MainForm.Instance;
                mainForm.BrowserBtns.OnBrowse += BrowserBtns_OnBrowse;
            }

            currentHandlers = GameManager.Instance.PackageManager.GetInstalledHandlers(gameId);
            if (currentHandlers.Length == 0) {
                // uninstalled package perhaps?
                return;
            }

            if (currentHandlers.Length > 1) {
                MainForm.Instance.ChangeTitle("Choose one handler to run game",
                    FormGraphicsUtil.BuildCharToBitmap(new Size(40, 40), 30, Color.FromArgb(240, 240, 240), "⤷"));

                panel_steps.Controls.Add(list_handlers);

                list_handlers.Width = panel_steps.Width;
                list_handlers.Height = panel_steps.Height;
                list_handlers.Left = 1;
                list_handlers.Controls.Clear();

                for (var i = 0; i < currentHandlers.Length; i++) {
                    GameHandlerMetadata metadata = currentHandlers[i];

                    GameControl handlerControl = new GameControl();
                    handlerControl.Width = list_handlers.Width;
                    handlerControl.Click += HandlerControl_Click;

                    handlerControl.SetHandlerMetadata(metadata);
                    list_handlers.Controls.Add(handlerControl);
                }
            } else {
                // create first step
                SetupSteps(currentHandlers[0]);
                GoToStep(0);
            }

            DPIManager.ForceUpdate();
        }

        private void SetupSteps(GameHandlerMetadata metadataSelected) {
            KillCurrentStep();

            if (handlerDataManager != null) {
                // dispose
                handlerDataManager.Dispose();
                handlerDataManager = null;
            }

            selectedHandler = metadataSelected;
            handlerDataManager = GameManager.Instance.PackageManager.ReadHandlerDataFromInstalledPackage(selectedHandler);
            handlerData = handlerDataManager.HandlerData;

            BrowserBtns.SetPlayButtonState(false);

            stepsList = new List<UserInputControl>();
            stepsList.Add(positionsControl);
            stepsList.Add(optionsControl);
            if (handlerData.CustomSteps != null) {
                for (int i = 0; i < handlerData.CustomSteps.Count; i++) {
                    stepsList.Add(jsControl);
                }
            }

            currentProfile = new GameProfile();
            currentProfile.InitializeDefault(handlerData);

            MainForm.Instance.ChangeGameInfo(userGame);
        }

        private void HandlerControl_Click(object sender, EventArgs e) {
            GameControl handlerControl = (GameControl)sender;
            SetupSteps((GameHandlerMetadata)handlerControl.HandlerMetadata);
            GoToStep(0);
        }

        private bool DoesntNeedOptions() {
            // TODO better if too sleepy
            return currentProfile.Options.Count == handlerData.CustomSteps.Count;
        }

        private void GoToStep(int step) {
            if (step == 1 &&
                DoesntNeedOptions()) {
                step++;
            }

            BrowserBtns.SetPreviousButtonState(step > 0);
            if (step >= stepsList.Count) {
                return;
            }
            else if (step >= 2) {
                // Custom steps
                List<CustomStep> customSteps = handlerData.CustomSteps;
                int customStepIndex = step - 2;
                CustomStep customStep = customSteps[0];

                if (customStep.Required) {
                    jsControl.CustomStep = customStep;
                    jsControl.DataManager = handlerDataManager;
                } else {
                    BrowserBtns.SetPlayButtonState(true);
                    return;
                }
            }

            KillCurrentStep();

            currentStepIndex = step;
            currentStep = stepsList[step];
            currentStep.Size = panel_steps.Size;
            currentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            currentStep.Initialize(handlerData, userGame, currentProfile);

            BrowserBtns.SetNextButtonState(currentStep.CanProceed && step != stepsList.Count - 1);

            panel_steps.Controls.Add(currentStep);
            currentStep.Size = panel_steps.Size; // for some reason this line must exist or the PositionsControl get messed up

            MainForm.Instance.ChangeTitle(currentStep.Title, currentStep.Image);
        }

        private void KillCurrentStep() {
            currentStep?.Ended();
            this.panel_steps.Controls.Clear();
        }

        private void StepCanPlay(UserControl obj, bool canProceed, bool autoProceed) {
            if (!canProceed) {
                BrowserBtns.SetNextButtonState(false);
                return;
            }

            bool needs = DoesntNeedOptions();
            if (needs && currentStepIndex + 1 >= stepsList.Count - 1 ||
                currentStepIndex + 1 > stepsList.Count - 1) {
                BrowserBtns.SetPlayButtonState(true);
                return;
            }

            if (autoProceed) {
                GoToStep(currentStepIndex + 1);
            } else {
                BrowserBtns.SetNextButtonState(true);
            }
        }

        private void BrowserBtns_OnBrowse(bool previous, bool next, bool play) {
            if (previous) {
                currentStepIndex--;
                if (currentStepIndex < 0) {
                    currentStepIndex = 0;
                    return;
                }
                GoToStep(currentStepIndex);
            } else if (next) {
                currentStepIndex = Math.Min(++currentStepIndex, stepsList.Count - 1);
                GoToStep(currentStepIndex);
            } else if (play) {
                //this.overlay.EnableOverlay(this);

                handler = new GameHandler();
                handler.Initialize(handlerDataManager, userGame, GameProfile.CleanClone(currentProfile));
                handler.Ended += handler_Ended;

                GameManager.Instance.Play(handler);
                if (handlerData.HandlerInterval > 0) {
                    handlerThread = new Thread(UpdateGameManager);
                    handlerThread.Start();
                }
                //WindowState = FormWindowState.Minimized;
            }
        }

        private void handler_Ended() {
            handler = null;
            if (handlerThread != null) {
                handlerThread.Abort();
                handlerThread = null;
            }
        }

        private void UpdateGameManager(object state) {
            for (; ; )
            {
                try {
                    if (GameManager.Instance == null || handler == null || this.IsDisposed) {
                        break;
                    }

                    string error = GameManager.Instance.Error;
                    if (!string.IsNullOrEmpty(error)) {
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        handler_Ended();
                        return;
                    }

                    handler.Tick(handlerData.HandlerInterval);
                    Thread.Sleep(TimeSpan.FromMilliseconds(handlerData.HandlerInterval));
                } catch (ThreadAbortException) {
                    return;
                } catch { }
            }
        }
    }
}
