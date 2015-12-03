using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Games.Borderlands
{
    public class BorderlandsSaveControl : UserControl
    {
        private Label user;
        private Label save;

        public SaveFile SaveFile { get; set; }

        public string UserName
        {
            get
            {
                return user.Text;
            }
            set
            {
                user.Text = value;
            }
        }
        public string SaveName
        {
            get
            {
                return user.Text;
            }
            set
            {
                user.Text = value;
            }
        }

        public BorderlandsSaveControl()
        {
            this.Size = new Size(200, 200);
            this.BackColor = Color.FromArgb(70, 70, 70);

            user = new Label();
            user.Location = new Point(10, 10);
            user.Text = "Username";
            this.Controls.Add(user);

            save = new Label();
            save.Location = new Point(10, 30);
            save.Text = "Save";
            this.Controls.Add(save);
        }
    }
}
