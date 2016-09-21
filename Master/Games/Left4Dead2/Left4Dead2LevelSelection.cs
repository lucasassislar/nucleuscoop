using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Games.Left4Dead2
{
    public partial class Left4Dead2LevelSelection : ControlListBox, ICanProceed
    {
        public Left4Dead2LevelSelection()
        {
            InitializeComponent();

            //var levels = Left4Dead2Info.Levels;

            //int height = 285;
            //Font custom = new Font(this.Font.FontFamily, 18, FontStyle.Bold);
            //for (int i = 0; i < levels.Length; i++)
            //{
            //    var level = levels[i];

            //    Button btn = new Button();
            //    btn.FlatStyle = FlatStyle.Flat;
            //    btn.Width = this.Width;
            //    btn.Height = height;
            //    btn.BackgroundImageLayout = ImageLayout.Zoom;
            //    btn.Image = level.Image;
            //    btn.Tag = level;
            //    if (level.Image == null)
            //    {
            //        btn.Text = level.Name;
            //        btn.Font = custom;
            //    }

            //    btn.Click += btn_Click;
            //    btn.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            //    this.Controls.Add(btn);
            //}
        }

        public string LevelName;

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            L4D2Level level = (L4D2Level)btn.Tag;

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
    }
}
