using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SplitTool
{
    public partial class PresetsForm : Form
    {
        private UserGameInfo info;
        public PresetsForm(UserGameInfo info, List<PlayerInfo> playas, Dictionary<string, GameOption> options)
        {
            this.info = info;
            this.players = playas;
            this.options = options;
            InitializeComponent();
        }

        private List<PlayerInfo> players;
        private Dictionary<string, GameOption> options;

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save splitscreen positions and options with name " + combo_Presets.Text + " for game " + info.GameName + "?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                // save the preset
                GameManager manager = GameManager.Instance;

                UserGameProfile profile = new UserGameProfile();
                profile.Options = options;
                profile.Preset = players;
                info.Profiles.Add(profile);
                manager.UpdateUserProfile();
            }
        }
    }
}
