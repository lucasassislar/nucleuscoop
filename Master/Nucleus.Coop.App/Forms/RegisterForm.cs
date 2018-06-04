using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            string result = apiConnection.Register(txtBox_userName.Text, txtBox_email.Text, txtBox_password.Text);
            if (result.ToLower().Contains("error"))
            {
                MessageBox.Show(result);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
