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
    public partial class GameControl : UserControl, IHighlightControl
    {
        public UserGameInfo Game { get; set; }
        public IGameInfo GameInfo { get; set; }

        public GameControl()
        {
            InitializeComponent();

            this.pictureBox1.MouseMove += pictureBox1_MouseMove;
            this.label_Title.MouseMove += pictureBox1_MouseMove;

            this.pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            this.label_Title.MouseLeave += pictureBox1_MouseLeave;

            this.pictureBox1.Click += pictureBox1_Click;
            this.label_Title.Click += pictureBox1_Click;
        }

        void pictureBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }


        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        public override string Text
        {
            get
            {
                return this.label_Title.Text;
            }
            set
            {
                this.label_Title.Text = value;
            }
        }

        public Image Image
        {
            get { return this.pictureBox1.Image; }
            set { this.pictureBox1.Image = value; }
        }

        public void Highlight()
        {
            this.BackColor = Color.FromArgb(90, 90, 90);
        }

        public void SoftHighlight()
        {
            this.BackColor = Color.FromArgb(80,80,80);
        }

        public void Darken()
        {
            this.BackColor = Color.FromArgb(70, 70, 70);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
