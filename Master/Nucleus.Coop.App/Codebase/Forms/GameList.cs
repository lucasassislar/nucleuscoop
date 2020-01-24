using Nucleus.Coop.App.Controls;
using Nucleus.Platform.Windows.Controls;
using SplitScreenMe.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#if false
using Nucleus.Gaming.Package;
#endif

namespace Nucleus.Coop.App.Forms {
    public partial class GameList : BaseForm {
#if false
        private GameHandlerMetadata clicked;

        public GameHandlerMetadata Selected {
            get { return clicked; }
        }
#endif

        protected override Size DefaultSize {
            get {
                return new Size(440, 710);
            }
        }

#if false
        public GameList(List<GameHandlerMetadata> games) {
#else
        public GameList() {

#endif
            InitializeComponent();

#if false
            GameManager manager = GameManager.Instance;
            foreach (GameHandlerMetadata game in games) {
                HandlerControl con = new HandlerControl(game);
                con.Width = listGames.Width;
                con.Click += Con_Click;

                listGames.Controls.Add(con);
            }
#endif
        }


#if false
        private void Con_Click(object sender, EventArgs e) {
            clicked = ((HandlerControl)sender).Metadata;
            btnOk.Enabled = true;
        }
#endif

        private void btnOk_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
