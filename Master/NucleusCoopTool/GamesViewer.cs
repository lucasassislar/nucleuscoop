using Nucleus;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using Nucleus.Coop.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SplitTool
{
    // BETA 1.3
    public partial class GamesViewer : Form
    {
        private GameManager gameManager;
        private GameInfo gameInfo;
        private UserGameInfo info;

        private Dictionary<int, Control> steps;
        private int step;
        private List<Control> addSteps;

        private IGameHandler handler;

        /// <summary>
        /// The amount of Default Steps the app has
        /// </summary>
        private static readonly int DefaultSteps = 3;
#if TEST
        public static string LogsFolder;
#endif

        public GamesViewer()
        {
            FormUtil.MainForm = this;
#if TEST
            Assembly ass = Assembly.GetEntryAssembly();
            LogsFolder = Path.Combine(Path.GetDirectoryName(ass.Location), "Logs");
            if (!Directory.Exists(LogsFolder))
            {
                Directory.CreateDirectory(LogsFolder);
            }

            // log all the information we can
            string allLogPath = Path.Combine(LogsFolder, "Log.log");
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(allLogPath)))
            {
                writer.WriteLine("Nucleus Log");
                writer.WriteLine();
                writer.WriteLine("Monitors found");
                Screen[] allScreens = Screen.AllScreens;
                for (int i = 0; i < allScreens.Length; i++)
                {
                    Screen s = allScreens[i];
                    writer.WriteLine(s.DeviceName + " " + s.BitsPerPixel + "BPP  Bounds" + s.Bounds.ToString());
                }

                writer.WriteLine();
                writer.WriteLine("System");
                writer.WriteLine(Environment.OSVersion);
                writer.WriteLine(Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits");
                writer.WriteLine(Environment.ProcessorCount + " Logical Processors");
            }
#endif

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            this.ControlAdded += GamesViewer_ControlAdded;
            this.MouseHover += GamesViewer_MouseHover;

            addSteps = new List<Control>();

            InitializeComponent();

            this.toolTip1.SetToolTip(this.pic_Keyboard, "Is Keyboard Supported?");
            this.toolTip1.SetToolTip(this.pic_gamePad, "How Many Gamepads Are Supported?");

            this.ActiveControl = this.label_Library;

            gameManager = new GameManager();

            steps = new Dictionary<int, Control>();
            steps.Add(0, this.playerCount1);
            steps.Add(1, this.monitorControl1);
            steps.Add(2, this.playerOptions1);

            list_Games.DataSource = gameManager.User.Games;
            label_title.Text = this.playerCount1.StepTitle;

            list_Games.SelectedIndex = -1;
            if (list_Games.Items.Count > 0)
            {
                list_Games.SelectedIndex = 0;
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());

            try
            {
                gameManager.End();
                if (handler != null)
                {
                    handler.End();
                }
            }
            catch { }

#if DEBUG
            try
            {
                if (e.ExceptionObject is Exception)
                {
                    SaveException((Exception)e.ExceptionObject);
                }
            }
            catch { }
#endif
        }

        private void SaveException(Exception e)
        {
            // Save Log of the Call Stack
            string log = GetLog(e);
            Assembly myAss = Assembly.GetEntryAssembly();
            string dir = Path.GetDirectoryName(myAss.Location);
            DateTime time = DateTime.Now;
            string logLoc = Path.Combine(dir, "Log " + time.Hour + "-" + time.Minute + "-" + time.Second) + ".log";

            using (FileStream stream = new FileStream(logLoc, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(log);
                writer.Flush();
            }
        }

        public string GetLog(Exception ex)
        {
            string log = "";
            Assembly myAss = Assembly.GetEntryAssembly();

            DateTime now = DateTime.Now;

            log += "// Nucleus Exception log";
            log += Environment.NewLine;
            log += now.ToShortDateString();
            log += Environment.NewLine;
            log += now.ToShortTimeString();
            log += Environment.NewLine;

            log += myAss.FullName;
            log += Environment.NewLine;

            log += ex.ToString();
            log += Environment.NewLine;
            log += ex.GetType().FullName;
            log += Environment.NewLine;
#if WINRT
            log += ex.GetType().GetTypeInfo().Assembly.FullName;
#else
            log += ex.GetType().Assembly.FullName;
#endif

            return log;
        }

        void GamesViewer_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.MouseHover += Control_MouseHover;
        }

        void GamesViewer_MouseHover(object sender, EventArgs e)
        {
            ClickedControl(this);
        }
        void Control_MouseHover(object sender, EventArgs e)
        {
            ClickedControl((Control)sender);
        }

        private void txt_Search_MouseEnter(object sender, EventArgs e)
        {
            label_Search.ForeColor = Color.FromArgb(200, 200, 200);
        }
        private void txt_Search_MouseLeave(object sender, EventArgs e)
        {
            label_Search.ForeColor = Color.FromArgb(150, 150, 150);
        }

        private void txt_Search_MouseClick(object sender, MouseEventArgs e)
        {
            label_Search.Visible = false;
        }


        protected void ClickedControl(Control c)
        {
            if (c != txt_Search && !txt_Search.Focused)
            {
                label_Search.Visible = true;
            }
        }

        private void btn_AddGame_Click(object sender, EventArgs e)
        {
            FindGameForm form = new FindGameForm(gameManager);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Update the game list?
                list_Games.DataSource = null;
                list_Games.DataSource = gameManager.User.Games;
            }
        }

        /// <summary>
        /// Handles updating the game's image and info when the user selects a different game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_Games_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(list_Games.SelectedItem is UserGameInfo))
            {
                return;
            }

            ResetSteps();
            step = 0;
            UpdateStep();

            info = (UserGameInfo)list_Games.SelectedItem;
            gameInfo = gameManager.Games[info.GameGuid];

            // Remove game from library if not found
            if (!File.Exists(info.ExecutablePath))
            {
                MessageBox.Show("Game Executable not Found. Deleting game from your library. TO-DO: User might have game on a disconnected hard-drive. Handle that.");

                gameManager.User.Games.Remove(info);
                gameManager.UpdateUserProfile();

                info = null;
                gameInfo = null;

                list_Games.DataSource = null;
                list_Games.SelectedIndex = -1;
                list_Games.DataSource = gameManager.User.Games;

                if (list_Games.Items.Count > 0)
                {
                    list_Games.SelectedIndex = 0;
                }
                return;
            }

            label_GameName.Text = info.GameName;
            playerCount1.MaxPlayers = gameInfo.MaxPlayers;
            label_maxPlayas.Text = gameInfo.MaxPlayers.ToString();
            if (gameInfo.SupportsKeyboard)
            {
                pic_Keyboard.Image = Resources.keyboard;
            }
            else
            {
                pic_Keyboard.Image = Resources.no_keyboard;
            }


            monitorControl1.UpdatePlayerCount(playerCount1.Players, gameInfo, info);
            playerOptions1.UpdateItems(gameInfo);

            addSteps.Clear();
            if (gameInfo.Steps != null)
            {
                // Add custom steps the game might need
                int count = steps.Count;
                for (int i = 0; i < gameInfo.Steps.Length; i++)
                {
                    Control s = (Control)Activator.CreateInstance(gameInfo.Steps[i]);
                    s.Location = this.monitorControl1.Location;
                    s.Size = this.monitorControl1.Size;
                    s.Anchor = this.monitorControl1.Anchor;
                    borderPanel1.Controls.Add(s);
                    steps.Add(count + i, s);

                    addSteps.Add(s);
                }
            }
            else
            {
                // Delete any old files
                if (steps.Count > DefaultSteps)
                {
                    int count = steps.Count;
                    for (int i = DefaultSteps; i < count; i++)
                    {
                        borderPanel1.Controls.Remove(steps[i]);
                        steps.Remove(i);
                    }
                }
            }

            // Extract icon from game executable
            if (this.game_Box.Image != null)
            {
                this.game_Box.Image.Dispose();
            }
            using (Icon sysicon = Icon.ExtractAssociatedIcon(info.ExecutablePath))
            {
                using (MemoryStream str = new MemoryStream())
                {
                    sysicon.Save(str);
                    str.Position = 0;
                    game_Box.Image = Image.FromStream(str);
                }
            }
        }

        /// <summary>
        /// Handles navigating Forward on the steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Next_Click(object sender, EventArgs e)
        {
            if (info != null)
            {
                step++;
                step = Math.Min(step, this.steps.Count - 1);
                UpdateStep();
            }
        }

        /// <summary>
        /// Handles navigating Backward on the steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Previous_Click(object sender, EventArgs e)
        {
            if (info != null)
            {
                step--;
                step = Math.Max(0, step);
                UpdateStep();
            }
        }

        /// <summary>
        /// Reset all the steps values
        /// </summary>
        private void ResetSteps()
        {
            foreach (var s in steps)
            {
                ((ICanProceed)s.Value).Restart();
            }
        }

        /// <summary>
        /// Updates steps
        /// </summary>
        private void UpdateStep()
        {
            btn_Next.Visible = step < steps.Count - 1;
            btn_Previous.Visible = step > 0;
            label_title.Text = ((ICanProceed)steps[step]).StepTitle;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (handler != null && handler.Ended)
            {
                EndPlay();
            }


            var st = steps[step];

            int proceed = 0;
            foreach (var ste in steps)
            {
                ICanProceed pro = (ICanProceed)ste.Value;
                if (ste.Key == step)
                {
                    if (pro.AutoProceed)
                    {
                        button_Next_Click(null, EventArgs.Empty);
                        pro.AutoProceeded();
                    }
                }

                if (pro.CanProceed)
                {
                    proceed++;
                }
                ste.Value.Visible = ste.Key == step;
            }
            button_Play.Enabled = (proceed == steps.Count && info != null) || this.handler != null;
            if (info != null)
            {
                monitorControl1.UpdatePlayerCount(playerCount1.Players, gameInfo, info);
            }

            ICanProceed proc = (ICanProceed)st;
            if (step != steps.Count - 1 && info != null)
            {
                btn_Next.Enabled = proc.CanProceed;
                btn_Next.Image = proc.CanProceed ? Resources.right_arrow : Resources.right_arrow_disabled;
            }

            if (handler != null)
            {
                timer1.Interval = handler.TimerInterval;
#if DEBUG
                stopWatch.Restart();
#endif

                handler.Update(timer1.Interval);

#if DEBUG
                stopWatch.Stop();
                if (stopWatch.Elapsed.TotalMilliseconds > 1)
                {
                    Debug.WriteLine("Handler Update's method is taking more than 1 ms to update, consider revising it");
                }
#endif
            }
        }

        private Stopwatch stopWatch = new Stopwatch();

        private void button_Play_Click(object sender, EventArgs e)
        {
            if (handler == null)
            {
                if (this.info == null)
                {
                    return;
                }

                string starter = this.info.ExecutablePath;

                Type t = gameInfo.HandlerType;
                handler = (IGameHandler)Activator.CreateInstance(t);

                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
                int titleHeight = screenRectangle.Top - this.Top;
                bool worked = handler.Initialize(starter, this.monitorControl1.Players, gameInfo.Options, addSteps, titleHeight);

                if (worked)
                {
                    string error = handler.Play();

                    if (string.IsNullOrEmpty(error))
                    {
                        button_Play.Text = "    STOP";
                        gameManager.Play(handler);
                    }
                    else
                    {
                        handler = null;
                        MessageBox.Show(error);
                    }
                }
                else
                {
                    handler = null;
                }
            }
            else
            {
                EndPlay();   
            }
        }

        private void EndPlay()
        {
            // Ends the game
            handler.End();
            handler = null;
            gameManager.End();

            button_Play.Text = "    PLAY";
        }

        /// <summary>
        /// Closes the handler and the gameManager, so we can properly dispose of streams and everything else
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GamesViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (handler != null)
            {
                handler.End();
                this.handler = null;
            }
            gameManager.End();
        }

        private void btn_Presets_Click(object sender, EventArgs e)
        {
            if (info != null)
            {
                //PresetsForm presets = new PresetsForm(info);
                //presets.ShowDialog();
            }
        }
    }
}
