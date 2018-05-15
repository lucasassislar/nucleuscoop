namespace Nucleus.Coop.Api
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRegSend = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxRegUsername = new System.Windows.Forms.TextBox();
            this.groupBoxReg = new System.Windows.Forms.GroupBox();
            this.textBoxRegPassword = new System.Windows.Forms.TextBox();
            this.textBoxRegEmail = new System.Windows.Forms.TextBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.textBoxLogEmail = new System.Windows.Forms.TextBox();
            this.textBoxLogPassword = new System.Windows.Forms.TextBox();
            this.buttonLogSend = new System.Windows.Forms.Button();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.textBoxSearchGameName = new System.Windows.Forms.TextBox();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.buttonSearchSend = new System.Windows.Forms.Button();
            this.buttonSetToken = new System.Windows.Forms.Button();
            this.groupBoxReg.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRegSend
            // 
            this.buttonRegSend.Location = new System.Drawing.Point(24, 97);
            this.buttonRegSend.Name = "buttonRegSend";
            this.buttonRegSend.Size = new System.Drawing.Size(75, 23);
            this.buttonRegSend.TabIndex = 0;
            this.buttonRegSend.Text = "Register";
            this.buttonRegSend.UseVisualStyleBackColor = true;
            this.buttonRegSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 226);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(806, 220);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // textBoxRegUsername
            // 
            this.textBoxRegUsername.Location = new System.Drawing.Point(6, 19);
            this.textBoxRegUsername.Name = "textBoxRegUsername";
            this.textBoxRegUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegUsername.TabIndex = 2;
            this.textBoxRegUsername.Text = "username";
            // 
            // groupBoxReg
            // 
            this.groupBoxReg.Controls.Add(this.textBoxRegEmail);
            this.groupBoxReg.Controls.Add(this.textBoxRegPassword);
            this.groupBoxReg.Controls.Add(this.textBoxRegUsername);
            this.groupBoxReg.Controls.Add(this.buttonRegSend);
            this.groupBoxReg.Location = new System.Drawing.Point(12, 12);
            this.groupBoxReg.Name = "groupBoxReg";
            this.groupBoxReg.Size = new System.Drawing.Size(122, 131);
            this.groupBoxReg.TabIndex = 3;
            this.groupBoxReg.TabStop = false;
            this.groupBoxReg.Text = "Registering";
            // 
            // textBoxRegPassword
            // 
            this.textBoxRegPassword.Location = new System.Drawing.Point(6, 45);
            this.textBoxRegPassword.Name = "textBoxRegPassword";
            this.textBoxRegPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegPassword.TabIndex = 3;
            this.textBoxRegPassword.Text = "password";
            // 
            // textBoxRegEmail
            // 
            this.textBoxRegEmail.Location = new System.Drawing.Point(6, 71);
            this.textBoxRegEmail.Name = "textBoxRegEmail";
            this.textBoxRegEmail.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegEmail.TabIndex = 4;
            this.textBoxRegEmail.Text = "email";
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBoxLogEmail);
            this.groupBoxLog.Controls.Add(this.textBoxLogPassword);
            this.groupBoxLog.Controls.Add(this.buttonLogSend);
            this.groupBoxLog.Location = new System.Drawing.Point(140, 12);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(122, 131);
            this.groupBoxLog.TabIndex = 5;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Login";
            // 
            // textBoxLogEmail
            // 
            this.textBoxLogEmail.Location = new System.Drawing.Point(6, 55);
            this.textBoxLogEmail.Name = "textBoxLogEmail";
            this.textBoxLogEmail.Size = new System.Drawing.Size(100, 20);
            this.textBoxLogEmail.TabIndex = 4;
            this.textBoxLogEmail.Text = "email";
            // 
            // textBoxLogPassword
            // 
            this.textBoxLogPassword.Location = new System.Drawing.Point(6, 29);
            this.textBoxLogPassword.Name = "textBoxLogPassword";
            this.textBoxLogPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxLogPassword.TabIndex = 3;
            this.textBoxLogPassword.Text = "password";
            // 
            // buttonLogSend
            // 
            this.buttonLogSend.Location = new System.Drawing.Point(24, 97);
            this.buttonLogSend.Name = "buttonLogSend";
            this.buttonLogSend.Size = new System.Drawing.Size(75, 23);
            this.buttonLogSend.TabIndex = 0;
            this.buttonLogSend.Text = "Login";
            this.buttonLogSend.UseVisualStyleBackColor = true;
            this.buttonLogSend.Click += new System.EventHandler(this.buttonLogSend_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.textBoxSearchGameName);
            this.groupBoxSearch.Controls.Add(this.buttonSearchSend);
            this.groupBoxSearch.Location = new System.Drawing.Point(278, 57);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(195, 62);
            this.groupBoxSearch.TabIndex = 6;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Search IGDB game (Req. Auth)";
            // 
            // textBoxSearchGameName
            // 
            this.textBoxSearchGameName.Location = new System.Drawing.Point(6, 26);
            this.textBoxSearchGameName.Name = "textBoxSearchGameName";
            this.textBoxSearchGameName.Size = new System.Drawing.Size(100, 20);
            this.textBoxSearchGameName.TabIndex = 4;
            this.textBoxSearchGameName.Text = "zelda";
            // 
            // textBoxToken
            // 
            this.textBoxToken.Location = new System.Drawing.Point(278, 12);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.Size = new System.Drawing.Size(457, 20);
            this.textBoxToken.TabIndex = 3;
            this.textBoxToken.Text = "token";
            // 
            // buttonSearchSend
            // 
            this.buttonSearchSend.Location = new System.Drawing.Point(112, 26);
            this.buttonSearchSend.Name = "buttonSearchSend";
            this.buttonSearchSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSearchSend.TabIndex = 0;
            this.buttonSearchSend.Text = "Search";
            this.buttonSearchSend.UseVisualStyleBackColor = true;
            this.buttonSearchSend.Click += new System.EventHandler(this.buttonSearchSend_Click);
            // 
            // buttonSetToken
            // 
            this.buttonSetToken.Location = new System.Drawing.Point(741, 12);
            this.buttonSetToken.Name = "buttonSetToken";
            this.buttonSetToken.Size = new System.Drawing.Size(75, 23);
            this.buttonSetToken.TabIndex = 7;
            this.buttonSetToken.Text = "Set Token";
            this.buttonSetToken.UseVisualStyleBackColor = true;
            this.buttonSetToken.Click += new System.EventHandler(this.buttonSetToken_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 458);
            this.Controls.Add(this.buttonSetToken);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBoxReg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBoxReg.ResumeLayout(false);
            this.groupBoxReg.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRegSend;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBoxRegUsername;
        private System.Windows.Forms.GroupBox groupBoxReg;
        private System.Windows.Forms.TextBox textBoxRegEmail;
        private System.Windows.Forms.TextBox textBoxRegPassword;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.TextBox textBoxLogEmail;
        private System.Windows.Forms.TextBox textBoxLogPassword;
        private System.Windows.Forms.Button buttonLogSend;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox textBoxSearchGameName;
        private System.Windows.Forms.Button buttonSearchSend;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Button buttonSetToken;
    }
}

