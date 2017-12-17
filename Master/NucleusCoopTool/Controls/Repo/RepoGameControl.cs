using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming.Repo;
using Nucleus.Gaming;

namespace Nucleus.Coop.Controls.Repo
{
    public partial class RepoGameControl : UserControl, IRadioControl
    {
        public RepoHeader Header { get; private set; }
        public RepoGameHandlerInfo Info { get; private set; }
        public RepoGameHandlerFullInfo FullInfo { get; private set; }

        public RepoGameControl()
        {
            InitializeComponent();
        }

        public void PreInitialize(RepoHeader header, RepoGameHandlerInfo game)
        {
            Header = header;
            Info = game;

            this.label_GameName.Text = game.Title;
        }
        public void Initialize(RepoGameHandlerFullInfo game)
        {
            FullInfo = game;

            this.label_GameName.Text = game.Title;
            this.lbl_Description.Text = game.Description;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;
            c.MouseEnter += C_MouseEnter;
            c.MouseLeave += C_MouseLeave;
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void C_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void C_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private bool isSelected;
        public void RadioSelected()
        {
            BackColor = Color.FromArgb(80, 80, 80);
            isSelected = true;
        }

        public void RadioUnselected()
        {
            BackColor = Color.FromArgb(30, 30, 30);
            isSelected = false;
        }

        public void UserOver()
        {
            BackColor = Color.FromArgb(60, 60, 60);
        }

        public void UserLeave()
        {
            if (isSelected)
            {
                BackColor = Color.FromArgb(80, 80, 80);
            }
            else
            {
                BackColor = Color.FromArgb(30, 30, 30);
            }
        }
    }
}
