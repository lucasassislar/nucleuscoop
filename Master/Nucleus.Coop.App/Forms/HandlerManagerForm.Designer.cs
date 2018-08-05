namespace Nucleus.Coop.App.Forms
{
    partial class HandlerManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandlerManagerForm));
            this.radio_installed = new System.Windows.Forms.RadioButton();
            this.radio_browse = new System.Windows.Forms.RadioButton();
            this.panel_browse = new System.Windows.Forms.Panel();
            this.panel_handlerInfo = new System.Windows.Forms.Panel();
            this.btn_gameHandlerInstall = new System.Windows.Forms.Button();
            this.combo_gameHandlerVersions = new System.Windows.Forms.ComboBox();
            this.label_gameHandlerDescription = new System.Windows.Forms.Label();
            this.label_gameHandlerName = new System.Windows.Forms.Label();
            this.list_handlers = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.list_left = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.panel_installed = new System.Windows.Forms.Panel();
            this.btn_uninstall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_installedGameName = new System.Windows.Forms.Label();
            this.txt_gameName = new Nucleus.Coop.App.Controls.NCTextBox();
            this.btn_installPkg = new System.Windows.Forms.Button();
            this.panel_browse.SuspendLayout();
            this.panel_handlerInfo.SuspendLayout();
            this.panel_installed.SuspendLayout();
            this.SuspendLayout();
            // 
            // radio_installed
            // 
            this.radio_installed.Appearance = System.Windows.Forms.Appearance.Button;
            this.radio_installed.AutoSize = true;
            this.radio_installed.Checked = true;
            this.radio_installed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radio_installed.Location = new System.Drawing.Point(91, 12);
            this.radio_installed.Name = "radio_installed";
            this.radio_installed.Size = new System.Drawing.Size(80, 33);
            this.radio_installed.TabIndex = 1;
            this.radio_installed.TabStop = true;
            this.radio_installed.Text = "Installed";
            this.radio_installed.UseVisualStyleBackColor = true;
            this.radio_installed.CheckedChanged += new System.EventHandler(this.radio_browse_CheckedChanged);
            // 
            // radio_browse
            // 
            this.radio_browse.Appearance = System.Windows.Forms.Appearance.Button;
            this.radio_browse.AutoSize = true;
            this.radio_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radio_browse.Location = new System.Drawing.Point(12, 12);
            this.radio_browse.Name = "radio_browse";
            this.radio_browse.Size = new System.Drawing.Size(73, 33);
            this.radio_browse.TabIndex = 0;
            this.radio_browse.Text = "Browse";
            this.radio_browse.UseVisualStyleBackColor = true;
            this.radio_browse.CheckedChanged += new System.EventHandler(this.radio_browse_CheckedChanged);
            // 
            // panel_browse
            // 
            this.panel_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_browse.Controls.Add(this.panel_handlerInfo);
            this.panel_browse.Controls.Add(this.list_handlers);
            this.panel_browse.Location = new System.Drawing.Point(388, 90);
            this.panel_browse.Name = "panel_browse";
            this.panel_browse.Size = new System.Drawing.Size(910, 648);
            this.panel_browse.TabIndex = 12;
            // 
            // panel_handlerInfo
            // 
            this.panel_handlerInfo.Controls.Add(this.btn_gameHandlerInstall);
            this.panel_handlerInfo.Controls.Add(this.combo_gameHandlerVersions);
            this.panel_handlerInfo.Controls.Add(this.label_gameHandlerDescription);
            this.panel_handlerInfo.Controls.Add(this.label_gameHandlerName);
            this.panel_handlerInfo.Location = new System.Drawing.Point(374, 3);
            this.panel_handlerInfo.Name = "panel_handlerInfo";
            this.panel_handlerInfo.Size = new System.Drawing.Size(533, 642);
            this.panel_handlerInfo.TabIndex = 18;
            // 
            // btn_gameHandlerInstall
            // 
            this.btn_gameHandlerInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_gameHandlerInstall.Enabled = false;
            this.btn_gameHandlerInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gameHandlerInstall.Location = new System.Drawing.Point(438, 35);
            this.btn_gameHandlerInstall.Name = "btn_gameHandlerInstall";
            this.btn_gameHandlerInstall.Size = new System.Drawing.Size(92, 33);
            this.btn_gameHandlerInstall.TabIndex = 19;
            this.btn_gameHandlerInstall.Text = "Install";
            this.btn_gameHandlerInstall.UseVisualStyleBackColor = true;
            this.btn_gameHandlerInstall.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // combo_gameHandlerVersions
            // 
            this.combo_gameHandlerVersions.FormattingEnabled = true;
            this.combo_gameHandlerVersions.Location = new System.Drawing.Point(5, 38);
            this.combo_gameHandlerVersions.Name = "combo_gameHandlerVersions";
            this.combo_gameHandlerVersions.Size = new System.Drawing.Size(427, 29);
            this.combo_gameHandlerVersions.TabIndex = 2;
            // 
            // label_gameHandlerDescription
            // 
            this.label_gameHandlerDescription.Location = new System.Drawing.Point(14, 70);
            this.label_gameHandlerDescription.Name = "label_gameHandlerDescription";
            this.label_gameHandlerDescription.Size = new System.Drawing.Size(516, 186);
            this.label_gameHandlerDescription.TabIndex = 1;
            this.label_gameHandlerDescription.Text = resources.GetString("label_gameHandlerDescription.Text");
            // 
            // label_gameHandlerName
            // 
            this.label_gameHandlerName.AutoSize = true;
            this.label_gameHandlerName.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.label_gameHandlerName.Location = new System.Drawing.Point(0, 0);
            this.label_gameHandlerName.Name = "label_gameHandlerName";
            this.label_gameHandlerName.Size = new System.Drawing.Size(217, 30);
            this.label_gameHandlerName.TabIndex = 0;
            this.label_gameHandlerName.Text = "Game Handler Name";
            // 
            // list_handlers
            // 
            this.list_handlers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_handlers.AutoScroll = true;
            this.list_handlers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.list_handlers.Border = 2;
            this.list_handlers.Location = new System.Drawing.Point(0, 0);
            this.list_handlers.Name = "list_handlers";
            this.list_handlers.Offset = new System.Drawing.Size(0, 0);
            this.list_handlers.Size = new System.Drawing.Size(368, 648);
            this.list_handlers.TabIndex = 17;
            // 
            // list_left
            // 
            this.list_left.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_left.AutoScroll = true;
            this.list_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.list_left.Border = 2;
            this.list_left.Location = new System.Drawing.Point(12, 90);
            this.list_left.Name = "list_left";
            this.list_left.Offset = new System.Drawing.Size(0, 0);
            this.list_left.Size = new System.Drawing.Size(368, 648);
            this.list_left.TabIndex = 1;
            // 
            // panel_installed
            // 
            this.panel_installed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_installed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_installed.Controls.Add(this.btn_uninstall);
            this.panel_installed.Controls.Add(this.label1);
            this.panel_installed.Controls.Add(this.label_installedGameName);
            this.panel_installed.Location = new System.Drawing.Point(388, 90);
            this.panel_installed.Name = "panel_installed";
            this.panel_installed.Size = new System.Drawing.Size(910, 648);
            this.panel_installed.TabIndex = 18;
            this.panel_installed.Visible = false;
            // 
            // btn_uninstall
            // 
            this.btn_uninstall.Enabled = false;
            this.btn_uninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_uninstall.Location = new System.Drawing.Point(721, 6);
            this.btn_uninstall.Name = "btn_uninstall";
            this.btn_uninstall.Size = new System.Drawing.Size(183, 49);
            this.btn_uninstall.TabIndex = 1;
            this.btn_uninstall.Text = "Uninstall";
            this.btn_uninstall.UseVisualStyleBackColor = true;
            this.btn_uninstall.Click += new System.EventHandler(this.btn_uninstall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(856, 623);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "Ｗ.Ｉ.Ｐ.";
            // 
            // label_installedGameName
            // 
            this.label_installedGameName.AutoSize = true;
            this.label_installedGameName.Location = new System.Drawing.Point(12, 12);
            this.label_installedGameName.Name = "label_installedGameName";
            this.label_installedGameName.Size = new System.Drawing.Size(97, 21);
            this.label_installedGameName.TabIndex = 0;
            this.label_installedGameName.Text = "Game Name";
            // 
            // txt_gameName
            // 
            this.txt_gameName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_gameName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txt_gameName.BorderColor = System.Drawing.Color.Red;
            this.txt_gameName.BorderSize = 1;
            this.txt_gameName.Enabled = false;
            this.txt_gameName.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txt_gameName.Location = new System.Drawing.Point(12, 51);
            this.txt_gameName.Name = "txt_gameName";
            this.txt_gameName.Padding = new System.Windows.Forms.Padding(1);
            this.txt_gameName.Size = new System.Drawing.Size(368, 33);
            this.txt_gameName.TabIndex = 2;
            this.txt_gameName.TextBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txt_gameName.TextBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.txt_gameName.UsePasswordChar = false;
            this.txt_gameName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txt_gameName.WaterMarkText = "Search (Disabled)";
            this.txt_gameName.TextChanged += new System.EventHandler(this.txt_gameName_OnTextChanged);
            this.txt_gameName.TextBoxKeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_gameName_TextBoxKeyDown);
            // 
            // btn_installPkg
            // 
            this.btn_installPkg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_installPkg.Location = new System.Drawing.Point(1115, 12);
            this.btn_installPkg.Name = "btn_installPkg";
            this.btn_installPkg.Size = new System.Drawing.Size(183, 33);
            this.btn_installPkg.TabIndex = 2;
            this.btn_installPkg.Text = "Install Package...";
            this.btn_installPkg.UseVisualStyleBackColor = true;
            this.btn_installPkg.Click += new System.EventHandler(this.btn_installPkg_Click);
            // 
            // HandlerManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 750);
            this.Controls.Add(this.btn_installPkg);
            this.Controls.Add(this.radio_installed);
            this.Controls.Add(this.radio_browse);
            this.Controls.Add(this.txt_gameName);
            this.Controls.Add(this.list_left);
            this.Controls.Add(this.panel_installed);
            this.Controls.Add(this.panel_browse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HandlerManagerForm";
            this.Text = "Handler Manager";
            this.panel_browse.ResumeLayout(false);
            this.panel_handlerInfo.ResumeLayout(false);
            this.panel_handlerInfo.PerformLayout();
            this.panel_installed.ResumeLayout(false);
            this.panel_installed.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gaming.Platform.Windows.Controls.ControlListBox list_left;
        private Controls.NCTextBox txt_gameName;
        private System.Windows.Forms.Panel panel_browse;
        private System.Windows.Forms.RadioButton radio_browse;
        private System.Windows.Forms.RadioButton radio_installed;
        private Gaming.Platform.Windows.Controls.ControlListBox list_handlers;
        private System.Windows.Forms.Panel panel_installed;
        private System.Windows.Forms.Panel panel_handlerInfo;
        private System.Windows.Forms.Label label_gameHandlerName;
        private System.Windows.Forms.Label label_gameHandlerDescription;
        private System.Windows.Forms.ComboBox combo_gameHandlerVersions;
        private System.Windows.Forms.Button btn_gameHandlerInstall;
        private System.Windows.Forms.Label label_installedGameName;
        private System.Windows.Forms.Button btn_uninstall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_installPkg;
    }
}