using Nucleus.Coop.App.Controls;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Forms {
    public partial class GameList : BaseForm
    {
        private GameHandlerMetadata clicked;

        public GameHandlerMetadata Selected
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

        public GameList(List<GameHandlerMetadata> games)
        {
            InitializeComponent();

            GameManager manager = GameManager.Instance;
            foreach (GameHandlerMetadata game in games)
            {
                HandlerControl con = new HandlerControl(game);
                con.Width = listGames.Width;
                con.Click += Con_Click;

                listGames.Controls.Add(con);
            }
        }

        private void Con_Click(object sender, EventArgs e)
        {
            clicked = ((HandlerControl)sender).Metadata;
            btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
