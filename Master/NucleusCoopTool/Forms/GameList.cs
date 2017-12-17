using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
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
        private GenericHandlerData clicked;

        public GenericHandlerData Selected
        {
            get { return clicked; }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(440, 710);
            }
        }

        public GameList(List<UserInstalledHandler> games)
        {
            InitializeComponent();

            //GameManager manager = GameManager.Instance;
            //foreach (UserInstalledHandler game in games)
            //{
            //    GameControl con = new GameControl(game, null);
            //    con.Width = listGames.Width;
            //    con.Click += Con_Click;

            //    listGames.Controls.Add(con);
            //}
        }

        private void Con_Click(object sender, EventArgs e)
        {
            //clicked = ((GameControl)sender).GameInfo;
            //btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
