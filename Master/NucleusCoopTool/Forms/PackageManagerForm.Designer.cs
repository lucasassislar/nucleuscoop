namespace Nucleus.Coop.Forms
{
    partial class PackageManagerForm
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
            this.btnInstall = new System.Windows.Forms.Button();
            this.lbl_NukeVersion = new System.Windows.Forms.Label();
            this.lbl_GameTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboVersions = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_gameDeveloper = new System.Windows.Forms.Label();
            this.lbl_gameName = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel_Installed = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel_Updates = new System.Windows.Forms.Panel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gameListBrowser = new Nucleus.Coop.Controls.Repo.RepoGameListControl();
            this.gameListInstalled = new Nucleus.Coop.Controls.Repo.RepoGameListControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel_Installed.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Enabled = false;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Location = new System.Drawing.Point(851, 180);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(139, 33);
            this.btnInstall.TabIndex = 11;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // lbl_NukeVersion
            // 
            this.lbl_NukeVersion.AutoSize = true;
            this.lbl_NukeVersion.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lbl_NukeVersion.Location = new System.Drawing.Point(723, 158);
            this.lbl_NukeVersion.Name = "lbl_NukeVersion";
            this.lbl_NukeVersion.Size = new System.Drawing.Size(88, 21);
            this.lbl_NukeVersion.TabIndex = 10;
            this.lbl_NukeVersion.Text = "NC Version";
            // 
            // lbl_GameTitle
            // 
            this.lbl_GameTitle.AutoSize = true;
            this.lbl_GameTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lbl_GameTitle.Location = new System.Drawing.Point(723, 127);
            this.lbl_GameTitle.Name = "lbl_GameTitle";
            this.lbl_GameTitle.Size = new System.Drawing.Size(220, 21);
            this.lbl_GameTitle.TabIndex = 9;
            this.lbl_GameTitle.Text = "Game Title Read From Version";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.Location = new System.Drawing.Point(492, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Platform Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(492, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Title";
            // 
            // comboVersions
            // 
            this.comboVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVersions.FormattingEnabled = true;
            this.comboVersions.Location = new System.Drawing.Point(727, 93);
            this.comboVersions.Name = "comboVersions";
            this.comboVersions.Size = new System.Drawing.Size(263, 29);
            this.comboVersions.TabIndex = 6;
            this.comboVersions.SelectedIndexChanged += new System.EventHandler(this.comboVersions_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(492, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Version";
            // 
            // lbl_gameDeveloper
            // 
            this.lbl_gameDeveloper.AutoSize = true;
            this.lbl_gameDeveloper.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lbl_gameDeveloper.Location = new System.Drawing.Point(491, 57);
            this.lbl_gameDeveloper.Name = "lbl_gameDeveloper";
            this.lbl_gameDeveloper.Size = new System.Drawing.Size(98, 25);
            this.lbl_gameDeveloper.TabIndex = 2;
            this.lbl_gameDeveloper.Text = "Developer";
            // 
            // lbl_gameName
            // 
            this.lbl_gameName.AutoSize = true;
            this.lbl_gameName.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.lbl_gameName.Location = new System.Drawing.Point(488, 12);
            this.lbl_gameName.Name = "lbl_gameName";
            this.lbl_gameName.Size = new System.Drawing.Size(197, 45);
            this.lbl_gameName.TabIndex = 1;
            this.lbl_gameName.Text = "Game Name";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 577);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.gameListBrowser);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(462, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browse";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel_Installed);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(462, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Installed";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel_Installed
            // 
            this.panel_Installed.Controls.Add(this.gameListInstalled);
            this.panel_Installed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Installed.Location = new System.Drawing.Point(3, 3);
            this.panel_Installed.Name = "panel_Installed";
            this.panel_Installed.Size = new System.Drawing.Size(456, 545);
            this.panel_Installed.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel_Updates);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(462, 543);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Updates";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel_Updates
            // 
            this.panel_Updates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Updates.Location = new System.Drawing.Point(0, 0);
            this.panel_Updates.Name = "panel_Updates";
            this.panel_Updates.Size = new System.Drawing.Size(462, 551);
            this.panel_Updates.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(462, 543);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Repositories";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gameListBrowser
            // 
            this.gameListBrowser.Border = 1;
            this.gameListBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameListBrowser.Location = new System.Drawing.Point(3, 3);
            this.gameListBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gameListBrowser.Name = "gameListBrowser";
            this.gameListBrowser.Offset = new System.Drawing.Size(0, 0);
            this.gameListBrowser.Size = new System.Drawing.Size(456, 537);
            this.gameListBrowser.TabIndex = 0;
            // 
            // gameListInstalled
            // 
            this.gameListInstalled.Border = 1;
            this.gameListInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameListInstalled.Location = new System.Drawing.Point(0, 0);
            this.gameListInstalled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gameListInstalled.Name = "gameListInstalled";
            this.gameListInstalled.Offset = new System.Drawing.Size(0, 0);
            this.gameListInstalled.Size = new System.Drawing.Size(456, 545);
            this.gameListInstalled.TabIndex = 0;
            // 
            // PackageManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 601);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.lbl_NukeVersion);
            this.Controls.Add(this.lbl_GameTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboVersions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_gameDeveloper);
            this.Controls.Add(this.lbl_gameName);
            this.Controls.Add(this.tabControl1);
            this.Name = "PackageManagerForm";
            this.Text = "Package Manager";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel_Installed.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel_Installed;
        private System.Windows.Forms.Panel panel_Updates;
        private System.Windows.Forms.TabPage tabPage4;
        private Controls.Repo.RepoGameListControl gameListBrowser;
        private Controls.Repo.RepoGameListControl gameListInstalled;
        private System.Windows.Forms.Label lbl_gameName;
        private System.Windows.Forms.Label lbl_gameDeveloper;
        private System.Windows.Forms.ComboBox comboVersions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_GameTitle;
        private System.Windows.Forms.Label lbl_NukeVersion;
        private System.Windows.Forms.Button btnInstall;
    }
}