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
    public partial class PlayerCountControl : UserControl, IUserInputForm
    {
        public PlayerCountControl()
        {
            InitializeComponent();
        }

        private bool go;

        public bool CanProceed
        {
            get { return go; }
        }
        public string Title
        {
            get { return "Player Count"; }
        }
        public event Action Proceed;

        public bool CanPlay
        {
            get { return false; }
        }

        private Button MkButton()
        {
            Button btn = new Button();
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = this.Font;
            btn.Click += btn_Click;

            return btn;
        }

        void btn_Click(object sender, EventArgs e)
        {
            go = true;

            profile.PlayerCount = int.Parse(((Button)sender).Text);

            if (Proceed != null)
            {
                Proceed();
            }
        }

        private GameProfile profile;

        public void Initialize(UserGameInfo game, GameProfile profile)
        {
            this.profile = profile;

            this.Controls.Clear();
            go = false;

            int maxPlayers = game.Game.MaxPlayers;
            int half = (int)Math.Round(maxPlayers / 2.0);
            int width = Size.Width / half;
            int height = Size.Height / 2;
            int player = 2;

            width = Size.Width / (half - 1);
            for (int i = 0; i < half - 1; i++)
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
