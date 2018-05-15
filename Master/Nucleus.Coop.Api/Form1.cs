using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Nucleus.Coop.Api
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var answer = ApiController.Register(textBoxRegUsername.Text, textBoxRegEmail.Text, textBoxRegPassword.Text);
            richTextBox1.Text = answer;
        }

        private void buttonLogSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.Login(textBoxLogEmail.Text, textBoxLogPassword.Text);
            richTextBox1.Text = answer;
        }

        private void buttonSearchSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.Search(textBoxSearchGameName.Text);
            richTextBox1.Text = answer;
        }

        private void buttonSetToken_Click(object sender, EventArgs e)
        {
            ApiController.Token = textBoxToken.Text;
            richTextBox1.Text = "Token set: " + ApiController.Token;
        }
    }
}
