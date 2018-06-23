using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Api;
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

        private void ChangeLoginButtonStates(bool state)
        {
            btn_Login.Enabled = state;
            btn_useOffline.Enabled = state;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            ChangeLoginButtonStates(false);

            Task.Run(async () =>
            {
                try
                {
                    RequestResult<LoginData> result = await apiConnection.Login(txt_email.Text, txt_password.Text);
                    if (result.Success)
                    {
                        this.Invoke((Action)(() =>
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }));
                    }
                    else
                    {
                        this.Invoke((Action)(() =>
                        {
                            MessageBox.Show(result.LogData);
                            ChangeLoginButtonStates(true);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        ChangeLoginButtonStates(true);
                    }));
                }
            });
        }

        private void btn_useOffline_Click(object sender, EventArgs e)
        {
            apiConnection.EnableOfflineMode();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
