namespace Nucleus.Coop.App.Forms
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.titleBarControl1 = new Nucleus.Gaming.Platform.Windows.Controls.TitleBarControl();
            this.btn_Register = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_passwordConfirm = new Nucleus.Coop.App.Controls.NCTextBox();
            this.txtBox_password = new Nucleus.Coop.App.Controls.NCTextBox();
            this.txtBox_email = new Nucleus.Coop.App.Controls.NCTextBox();
            this.txtBox_userName = new Nucleus.Coop.App.Controls.NCTextBox();
            this.SuspendLayout();
            // 
            // titleBarControl1
            // 
            this.titleBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBarControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.titleBarControl1.EnableMaximize = false;
            this.titleBarControl1.Location = new System.Drawing.Point(0, 0);
            this.titleBarControl1.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarControl1.Name = "titleBarControl1";
            this.titleBarControl1.Size = new System.Drawing.Size(510, 21);
            this.titleBarControl1.TabIndex = 12;
            // 
            // btn_Register
            // 
            this.btn_Register.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Register.Location = new System.Drawing.Point(12, 322);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(486, 66);
            this.btn_Register.TabIndex = 4;
            this.btn_Register.Text = "Register";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password (Confirm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // txtBox_passwordConfirm
            // 
            this.txtBox_passwordConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtBox_passwordConfirm.BorderColor = System.Drawing.Color.Red;
            this.txtBox_passwordConfirm.BorderSize = 1;
            this.txtBox_passwordConfirm.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txtBox_passwordConfirm.Location = new System.Drawing.Point(12, 227);
            this.txtBox_passwordConfirm.Name = "txtBox_passwordConfirm";
            this.txtBox_passwordConfirm.Size = new System.Drawing.Size(486, 33);
            this.txtBox_passwordConfirm.TabIndex = 15;
            this.txtBox_passwordConfirm.TextBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txtBox_passwordConfirm.TextBoxForeColor = System.Drawing.Color.Empty;
            this.txtBox_passwordConfirm.UsePasswordChar = false;
            this.txtBox_passwordConfirm.WaterMarkColor = System.Drawing.Color.Empty;
            this.txtBox_passwordConfirm.WaterMarkText = null;
            // 
            // txtBox_password
            // 
            this.txtBox_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtBox_password.BorderColor = System.Drawing.Color.Red;
            this.txtBox_password.BorderSize = 1;
            this.txtBox_password.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txtBox_password.Location = new System.Drawing.Point(12, 170);
            this.txtBox_password.Name = "txtBox_password";
            this.txtBox_password.Size = new System.Drawing.Size(486, 33);
            this.txtBox_password.TabIndex = 14;
            this.txtBox_password.TextBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txtBox_password.TextBoxForeColor = System.Drawing.Color.Empty;
            this.txtBox_password.UsePasswordChar = false;
            this.txtBox_password.WaterMarkColor = System.Drawing.Color.Empty;
            this.txtBox_password.WaterMarkText = null;
            // 
            // txtBox_email
            // 
            this.txtBox_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtBox_email.BorderColor = System.Drawing.Color.Red;
            this.txtBox_email.BorderSize = 1;
            this.txtBox_email.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txtBox_email.Location = new System.Drawing.Point(12, 114);
            this.txtBox_email.Name = "txtBox_email";
            this.txtBox_email.Size = new System.Drawing.Size(486, 33);
            this.txtBox_email.TabIndex = 13;
            this.txtBox_email.TextBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txtBox_email.TextBoxForeColor = System.Drawing.Color.Empty;
            this.txtBox_email.UsePasswordChar = false;
            this.txtBox_email.WaterMarkColor = System.Drawing.Color.Empty;
            this.txtBox_email.WaterMarkText = null;
            // 
            // txtBox_userName
            // 
            this.txtBox_userName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtBox_userName.BorderColor = System.Drawing.Color.Red;
            this.txtBox_userName.BorderSize = 1;
            this.txtBox_userName.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txtBox_userName.Location = new System.Drawing.Point(12, 54);
            this.txtBox_userName.Name = "txtBox_userName";
            this.txtBox_userName.Size = new System.Drawing.Size(486, 33);
            this.txtBox_userName.TabIndex = 11;
            this.txtBox_userName.TextBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txtBox_userName.TextBoxForeColor = System.Drawing.Color.Empty;
            this.txtBox_userName.UsePasswordChar = false;
            this.txtBox_userName.WaterMarkColor = System.Drawing.Color.Empty;
            this.txtBox_userName.WaterMarkText = null;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(510, 400);
            this.Controls.Add(this.txtBox_passwordConfirm);
            this.Controls.Add(this.txtBox_password);
            this.Controls.Add(this.txtBox_email);
            this.Controls.Add(this.titleBarControl1);
            this.Controls.Add(this.txtBox_userName);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterForm";
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Register;
        private Controls.NCTextBox txtBox_userName;
        private Gaming.Platform.Windows.Controls.TitleBarControl titleBarControl1;
        private Controls.NCTextBox txtBox_email;
        private Controls.NCTextBox txtBox_password;
        private Controls.NCTextBox txtBox_passwordConfirm;
    }
}