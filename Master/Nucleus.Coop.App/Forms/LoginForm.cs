using Nucleus.Gaming.Coop.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Forms
{
    public partial class LoginForm : BaseForm
    {
        private DomainWebApiConnection apiConnection;

        public LoginForm(DomainWebApiConnection con)
        {
            InitializeComponent();

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.apiConnection = con;
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            using (RegisterForm form = new RegisterForm(apiConnection))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string result = apiConnection.Login(txt_userName.Text, txt_password.Text);
            if (result.ToLower().Contains("forbidden"))
            {
                MessageBox.Show(result);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
