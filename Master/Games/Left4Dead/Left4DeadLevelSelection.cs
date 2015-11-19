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
    public partial class Left4DeadLevelSelection : ControlListBox, ICanProceed
    {
        public Left4DeadLevelSelection()
        {
            InitializeComponent();

            var levels = Left4DeadInfo.Levels;

            this.ListType = Nucleus.ControlListBoxType.Arranged;

            Font custom = new Font(this.Font.FontFamily, 18, FontStyle.Bold);
            for (int i = 0; i < levels.Length; i++)
            {
                var level = levels[i];

                Button btn = new Button();
                btn.FlatStyle = FlatStyle.Flat;
                btn.Width = 240;
                btn.Height = 240;

                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.Image = level.Image;
                btn.Tag = level;
                if (level.Image == null)
                {
                    btn.Text = level.Name;
                    btn.Font = custom;
                }

                btn.Click += btn_Click;
                //btn.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                this.Controls.Add(btn);
            }
        }

        public string LevelName;

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            L4DLevel level = (L4DLevel)btn.Tag;

            if (level.Command == "__custom__")
            {
                // Open custom TextBox for naming level
                TextMessageBox txt = new TextMessageBox();
                if (txt.ShowDialog() == DialogResult.OK)
                {
                    LevelName = txt.UserText;
                    selected = true;
                }
            }
            else
            {
                LevelName = level.Command;
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
            get { return "Level Selection"; }
        }


        public bool AutoProceed
        {
            get { return false; }
        }


        public void AutoProceeded()
        {
        }


        public void UpdateSelectedGame(int players, GameInfo info, UserGameInfo uInfo)
        {
        }
    }
}
