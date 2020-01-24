using Nucleus.DPI;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using SplitScreenMe.Core;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls {
    /// <summary>
    /// 
    /// </summary>
    public class GameNameControl : UserControl, IDynamicSized {
        public UserGameInfo GameInfo {
            get { return gameInfo; }
            set {
                if (gameInfo != value && value != null) {
                    picture.Image = value.Icon;
#if false
                    title.Text = GameManager.Instance.MetadataManager.GetGameName(value.GameID);
#endif
                    DPI.DPIManager.Update(this);
                }
                gameInfo = value;
            }
        }

        public Image Image {
            set { picture.Image = value; }
        }

        private UserGameInfo gameInfo;
        private PictureBox picture;
        private Label title;
        private int border;
        private float lastScale;

        public GameNameControl() {
            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            //picture.BorderStyle = BorderStyle.FixedSingle;

            title = new Label();
            title.Text = "Nothing selected";
            title.AutoSize = true;

            BackColor = Color.FromArgb(30, 30, 30);

            Controls.Add(picture);
            Controls.Add(title);

            UpdateSize(1);
            DPI.DPIManager.Register(this);
        }

        public void UpdateText(string txt) {
            title.Text = txt;
            UpdateSize(lastScale);
        }

        ~GameNameControl() {
            DPI.DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale) {
            if (IsDisposed) {
                DPI.DPIManager.Unregister(this);
                return;
            }
            lastScale = scale;

            SuspendLayout();

            border = DPI.DPIManager.Adjust(5, scale);
            int dborder = border * 2;
            picture.Location = new Point(border, border);

            Height = DPI.DPIManager.Adjust(46, scale);
            picture.Size = new Size(Size.Height - dborder, Height - dborder);

            Width = picture.Width + dborder + title.Width;

            float height = this.Height / 2.0f;
            float lheight = title.Size.Height / 2.0f;
            title.Location = new Point(picture.Width + picture.Left + border, (int)(height - lheight));

            ResumeLayout();
        }
    }
}
