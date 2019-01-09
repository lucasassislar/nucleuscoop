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
            var answer = ApiController.SearchExtGame(textBoxSearchGameName.Text);
            richTextBox1.Text = answer;
        }

        private void buttonSetToken_Click(object sender, EventArgs e)
        {
            ApiController.Token = textBoxToken.Text;
            richTextBox1.Text = "Token set: " + ApiController.Token;
        }

        private void buttonImportSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.ImportExtGame(textBoxImportId.Text);
            richTextBox1.Text = answer;
        }

        private void buttonListGameSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.ListIntGames();
            richTextBox1.Text = answer.Content;
            dataGridViewListGames.DataSource = answer.Data;
        }

        private void buttonSpecificGameSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.GetSpecificGameWithHandlers(Convert.ToInt32(textBoxSpecificGameId.Text));
            //richTextBox1.Text = answer;
            dataGridViewListHandlers.DataSource = answer.Data.handlers;
        }

        private void buttonAddHandlerSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.CreateHandler(Convert.ToInt32(textBoxAddHandlerGameId.Text), textBoxAddHandlerName.Text, textBoxAddHandlerDetails.Text);
            richTextBox1.Text = answer.Content;
        }

        private void textBoxAddPackageFileFullPath_Click(object sender, EventArgs e)
        {
            openFileDialogAddPackageFileFullPath.ShowDialog();
        }

        private void openFileDialogAddPackageFileFullPath_FileOk(object sender, CancelEventArgs e)
        {
            textBoxAddPackageFileFullPath.Text = openFileDialogAddPackageFileFullPath.FileName;
        }

        private void buttonAddPackageSend_Click(object sender, EventArgs e)
        {
            var answer = ApiController.CreatePackage(Convert.ToInt32(textBoxAddPackageHandlerId.Text), textBoxAddPackageFileFullPath.Text, textBoxAddPackageInfos.Text);
            richTextBox1.Text = answer.Content;
        }

        private void buttonGetPackageSend_Click(object sender, EventArgs e)
        {
            var version = 0;
            int.TryParse(textBoxGetPackageSpecificVersion.Text, out version);
            ApiController.GetPackage(Convert.ToInt32(textBoxGetPackageHandlerId.Text), version);
        }
    }
}
