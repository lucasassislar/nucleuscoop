using System;

namespace Nucleus.Coop.App.Controls {
    public partial class GamePageBrowserControl : BaseControl {
        public event Action<bool, bool, bool> OnBrowse;

        public GamePageBrowserControl() {
            InitializeComponent();
        }

        public void SetNextButtonState(bool state) {
            btn_next.Enabled = state;
        }

        public void SetPreviousButtonState(bool state) {
            btn_previous.Enabled = state;
        }

        public void SetPlayButtonState(bool state) {
            btn_play.Enabled = state;
        }

        private void btn_previous_Click(object sender, EventArgs e) {
            if (OnBrowse != null) {
                OnBrowse(true, false, false);
            }
        }

        private void btn_next_Click(object sender, EventArgs e) {
            if (OnBrowse != null) {
                OnBrowse(false, true, false);
            }
        }

        private void btn_play_Click(object sender, EventArgs e) {
            if (OnBrowse != null) {
                OnBrowse(false, false, true);
            }
        }
    }
}
