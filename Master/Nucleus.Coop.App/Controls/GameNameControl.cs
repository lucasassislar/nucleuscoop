using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;

namespace Nucleus.Coop.Controls
{
    public class GameNameControl : UserControl, IDynamicSized
    {
        private UserGameInfo gameInfo;
        public UserGameInfo GameInfo
        {
            get { return gameInfo; }
            set
            {
                if (gameInfo != value && value != null)
                {
                    picture.Image = value.Icon;
                    title.Text = GameManager.Instance.NameManager.GetGameName(value.GameID);
                    DPIManager.Update(this);
                }
                gameInfo = value;
            }
        }

        private PictureBox picture;
        private Label title;
        private int border;

        public GameNameControl()
        {
            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            title = new Label();
            title.Text = "Nothing selected";
            title.AutoSize = true;

            BackColor = Color.FromArgb(30, 30, 30);

            Controls.Add(picture);
            Controls.Add(title);

            UpdateSize(1);
            DPIManager.Register(this);
        }

        ~GameNameControl()
        {
            DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            border = DPIManager.Adjust(4, scale);
            int dborder = border * 2;
            picture.Location = new Point(border, border);

            Height = DPIManager.Adjust(46, scale);
            picture.Size = new Size(Size.Height - dborder, Height - dborder);

            Width = picture.Width + dborder + title.Width;

            float height = this.Height / 2.0f;
            float lheight = title.Size.Height / 2.0f;
            title.Location = new Point(picture.Width + picture.Left + border, (int)(height - lheight));

            ResumeLayout();
        }

    }
}
