using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Repo;

namespace Nucleus.Coop.Controls.Repo
{
    public partial class RepoGameListControl : ControlListBox
    {
        public RepoGameListControl()
        {
            InitializeComponent();
        }

        public void ShowRepo(RepoHeader header)
        {
            this.Invoke(new Action<RepoHeader>(privShowRepo), header);
        }

        private void ReceiveUpdatedHeader(RequestResult<RepoHeader> result)
        {
            if (result.Success)
            {
                this.Invoke(new Action<RepoHeader>(privShowRepo), result.Data);
            }
        }

        private void privShowRepo(RepoHeader header)
        {
            for (int i = 0; i < header.Games.Length; i++)
            {
                RepoGameHandlerInfo game = header.Games[i];

                RepoGameControl gameCon = new RepoGameControl();
                this.Controls.Add(gameCon);

                gameCon.PreInitialize(header, game);
            }
        }
    }
}
