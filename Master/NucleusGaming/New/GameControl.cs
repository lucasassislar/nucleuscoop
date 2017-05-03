using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming;

namespace Nucleus.Coop
{
    public class GameControl : UserControl
    {
        public UserGameInfo Game { get; set; }
        public GenericGameInfo GameInfo { get; set; }

        private PictureBox picture;
        private Label title;

        public GameControl()
        {
            picture = new PictureBox();
            picture.Location = new Point(4, 4);
            picture.Size = new Size(44, 44);
            picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            title = new Label();
            title.Location = new Point(48, 16);
            title.Text = "Name";
            title.AutoSize = true;

            BackColor = Color.FromArgb(30, 30, 30);

            Size = new Size(200, 52);

            Controls.Add(picture);
            Controls.Add(title);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;
            c.MouseEnter += C_MouseEnter;
            c.MouseLeave += C_MouseLeave;
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void C_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void C_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }


        public override string Text
        {
            get { return this.title.Text; }
            set
            {
                this.title.Text = value;
            }
        }

        public Image Image
        {
            get { return this.picture.Image; }
            set { this.picture.Image = value; }
        }

        public override string ToString()
        {
            return Text;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!ContainsFocus)
            {
                BackColor = Color.FromArgb(60, 60, 60);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!ContainsFocus)
            {
                BackColor = Color.FromArgb(30, 30, 30);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            BackColor = Color.FromArgb(80, 80, 80);
        }
    }
}
