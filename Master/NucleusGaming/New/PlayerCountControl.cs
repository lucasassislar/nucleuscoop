using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    public partial class PlayerCountControl : UserInputControl
    {
        private bool canProceed;

        public override bool CanProceed
        {
            get { return canProceed; }
        }
        public override string Title
        {
            get { return "Player Count"; }
        }

        public override bool CanPlay
        {
            get { return false; }
        }

        public PlayerCountControl()
        {
            InitializeComponent();
            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        private Button MkButton()
        {
            Button btn = new Button();
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = this.Font;
            btn.Click += btn_Click;

            return btn;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            canProceed = true;

            profile.PlayerCount = int.Parse(((Button)sender).Text);
            OnCanPlayTrue(true);
        }

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);

            this.Controls.Clear();
            canProceed = false;

            int maxPlayers = game.Game.MaxPlayers;
            int half = (int)Math.Round(maxPlayers / 2.0);
            int width = Size.Width / half;
            int height = Size.Height / 2;
            int player = 2;

            int left = Math.Max(half - 1, 1);
            width = Size.Width / left;
            for (int i = 0; i < left; i++)
            {
                Button btn = MkButton();
                btn.Text = player.ToString();
                player++;

                btn.SetBounds(i * width, 0, width, height);
                this.Controls.Add(btn);
            }

            half = maxPlayers - half;
            width = Size.Width / half;
            for (int i = 0; i < half; i++)
            {
                Button btn = MkButton();
                btn.Text = player.ToString();
                player++;

                btn.SetBounds(i * width, height, width, height);
                this.Controls.Add(btn);
            }
        }
    }
}
