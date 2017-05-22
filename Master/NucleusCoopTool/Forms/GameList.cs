using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public partial class GameList : BaseForm
    {
        private GenericGameInfo clicked;

        public GenericGameInfo Selected
        {
            get { return clicked; }
        }

        public GameList(GenericGameInfo highlight)
        {
            InitializeComponent();

            GameManager manager = GameManager.Instance;
            var games = manager.Games;
            foreach (GenericGameInfo game in games.Values)
            {
                GameControl con = new GameControl(game, null);
                con.Width = listGames.Width;
                con.Click += Con_Click;

                con.Text = game.GameName;
                listGames.Controls.Add(con);

                if (game == highlight)
                {
                    con.RadioSelected();

                    clicked = game;
                    btnOk.Enabled = true;
                }
            }
        }

        private void Con_Click(object sender, EventArgs e)
        {
            clicked = ((GameControl)sender).GameInfo;
            btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
