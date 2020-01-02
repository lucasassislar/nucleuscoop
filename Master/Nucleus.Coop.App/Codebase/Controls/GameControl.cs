using Nucleus.DPI;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Package;
using Nucleus.Platform.Windows.Controls;
using SplitScreenMe.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls {
    public class GameControl : UserControl, IDynamicSized, IRadioControl {
        public UserGameInfo UserGameInfo { get; private set; }
        public List<UserGameInfo> UserGames { get; private set; }
        public GameHandlerBaseMetadata HandlerMetadata { get; private set; }

        public Color ColorSelected { get; set; } = Color.FromArgb(66, 70, 77);
        public Color ColorUnselected { get; set; } = Color.FromArgb(47, 49, 54);
        public Color ColorMouseOver { get; set; } = Color.FromArgb(54, 57, 63);
        public bool EnableClicking { get; set; } = true;

        public bool UseCheckbox { get; set; }

        private PictureBox picture;
        private Label title;
        public string TitleText { get; protected set; }

        public GameControl() {
            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            title = new Label();

            BackColor = ColorUnselected;
            Size = new Size(200, 52);

            Controls.Add(picture);
            Controls.Add(title);

            DPIManager.Register(this);
        }

        ~GameControl() {
            DPIManager.Unregister(this);
        }

        public void UpdateTitleText(string titleText) {
            TitleText = titleText;
            title.Text = titleText;
        }

        public void SetUserGame(UserGameInfo userGame) {
            UserGameInfo = userGame;
            if (userGame == null) {
                title.Text = "No games";
            } else {
                title.Text = GameManager.Instance.MetadataManager.GetGameName(userGame.GameID);
            }
            TitleText = title.Text;
        }

        public void SetUserGameExe(UserGameInfo userGame) {
            UserGameInfo = userGame;
            title.Text = userGame.ExePath;
            TitleText = title.Text;
        }

        public void SetUserGames(List<UserGameInfo> userGames) {
            UserGames = userGames;
            string gameTitle = GameManager.Instance.MetadataManager.GetGameName(userGames[0].GameID);
            title.Text = gameTitle;
            TitleText = gameTitle;
        }

        public void SetHandlerMetadata(GameHandlerBaseMetadata metadata) {
            HandlerMetadata = metadata;
            title.Text = metadata.Title;
            TitleText = title.Text;
        }

        public void UpdateSize(float scale) {
            if (IsDisposed) {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            int border = DPIManager.Adjust(8, scale);
            int dborder = border * 2;

            picture.Location = new Point(12, 11);
            picture.Size = new Size(30, 30);

            Height = DPIManager.Adjust(52, scale);

            Size labelSize = TextRenderer.MeasureText(TitleText, title.Font);
            float reservedSpaceLabel = this.Width - picture.Width;

            if (labelSize.Width > reservedSpaceLabel) {
                // make text smaller
                int charSize = TextRenderer.MeasureText("g", title.Font).Width;
                int toRemove = (int)((reservedSpaceLabel - labelSize.Width) / (float)charSize);
                toRemove = Math.Max(toRemove + 3, 7);
                title.Text = TitleText.Remove(TitleText.Length - toRemove, toRemove) + "...";
            } else {
                title.Text = TitleText;
            }
            title.Size = labelSize;

            float height = this.Height / 2.0f;
            float lheight = labelSize.Height / 2.0f;

            title.Location = new Point(picture.Width + picture.Left + border, (int)(height - lheight));

            ResumeLayout();
        }

        protected override void OnControlAdded(ControlEventArgs e) {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;
            c.MouseEnter += C_MouseEnter;
            c.MouseLeave += C_MouseLeave;
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            UpdateSize(DPIManager.Scale);
        }

        private void C_MouseEnter(object sender, EventArgs e) {
            OnMouseEnter(e);
        }

        private void C_MouseLeave(object sender, EventArgs e) {
            OnMouseLeave(e);
        }

        private void C_Click(object sender, EventArgs e) {
            OnClick(e);
        }

        public Image Image {
            get { return this.picture.Image; }
            set { this.picture.Image = value; }
        }

        public override string ToString() {
            return Text;
        }

        private bool isSelected;
        public void RadioSelected() {
            BackColor = ColorSelected;
            isSelected = true;
        }

        public void RadioUnselected() {
            BackColor = ColorUnselected;
            isSelected = false;
        }

        public void UserOver() {
            BackColor = ColorMouseOver;
        }

        public void UserLeave() {
            BackColor = isSelected ? ColorSelected : ColorUnselected;
        }
    }
}
