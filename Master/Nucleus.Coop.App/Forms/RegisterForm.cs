using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop.Api;
using Nucleus.Gaming.Coop.Interop;

namespace Nucleus.Coop.App.Forms
{
    public partial class RegisterForm : BaseForm
    {
        private DomainWebApiConnection apiConnection;

        public RegisterForm(DomainWebApiConnection con)
        {
            InitializeComponent();

            this.apiConnection = con;
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            string password = txtBox_password.Text;
            string confirm = txtBox_passwordConfirm.Text;

            // make sure the user typed the password twice correctly
            if (!password.Equals(confirm))
            {
                MessageBox.Show("Password does not match!");
                return;
            }

            Task.Run(async () =>
            {
                // submit the data to the api
                RequestResult<User> result = await apiConnection.Register(txtBox_userName.Text, txtBox_email.Text, password);
                if (!result.Success)
                {
                    MessageBox.Show(result.LogData);
                    return;
                }

                // login
                RequestResult<LoginData> loginData = await apiConnection.Login(txtBox_email.Text, password);
                if (result.Success)
                {
                    // no error = close with ok result
                    this.Invoke((Action)(() =>
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }));
                }
                else
                {
                    MessageBox.Show("Failed to login. " + result.LogData);
                }
            });
        }
    }
}
