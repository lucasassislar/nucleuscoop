using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming;

namespace Games.Left4Dead
{
    public partial class Left4DeadModeSelection : ControlListBox, ICanProceed
    {
        public void UpdateSelectedGame(int players, GameInfo info, UserGameInfo uInfo)
        {
        }
        public Left4DeadModeSelection()
        {
            InitializeComponent();

            var modes = Left4DeadInfo.GameModes;
            this.ListType = Nucleus.ControlListBoxType.Arranged;

            Font custom = new Font(this.Font.FontFamily, 18, FontStyle.Bold);
            for (int i = 0; i < modes.Length; i++)
            {
                var mode = modes[i];

                Button btn = new Button();
                btn.Width = 240;
                btn.Height = 240;
                btn.Image = mode.Image;
                btn.Font = custom;
                btn.Text = mode.Name;
                btn.Tag = mode;

                btn.Click += btn_Click;
                //btn.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                this.Controls.Add(btn);
            }
        }

        public string ModeCommand;

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var level = (L4DGameMode)btn.Tag;

            if (level.Command == "__custom__")
            {
                // Open custom TextBox for naming level
                TextMessageBox txt = new TextMessageBox();
                if (txt.ShowDialog() == DialogResult.OK)
                {
                    ModeCommand = txt.UserText;
                    selected = true;
                }
            }
            else
            {
                ModeCommand = level.Command;
                selected = true;
            }
        }
        protected bool selected;
        public bool CanProceed
        {
            get { return selected; }
        }

        public void Restart()
        {
        }

        public string StepTitle
        {
            get { return "Mode Selection"; }
        }
        public bool AutoProceed
        {
            get { return false; }
        }
        public void AutoProceeded()
        {
        }
    }
}
