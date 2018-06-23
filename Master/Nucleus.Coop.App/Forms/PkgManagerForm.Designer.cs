namespace Nucleus.Coop.App.Forms
{
    partial class PkgManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PkgManagerForm));
            this.list_left = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.txt_gameName = new Nucleus.Coop.App.Controls.NCTextBox();
            this.panel_browse = new System.Windows.Forms.Panel();
            this.list_handlers = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.radio_browse = new System.Windows.Forms.RadioButton();
            this.radio_installed = new System.Windows.Forms.RadioButton();
            this.panel_installed = new System.Windows.Forms.Panel();
            this.btn_search = new System.Windows.Forms.Button();
            this.panel_browse.SuspendLayout();
            this.SuspendLayout();
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
            // txt_gameName
            // 
            this.txt_gameName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txt_gameName.BorderColor = System.Drawing.Color.Red;
            this.txt_gameName.BorderSize = 1;
            this.txt_gameName.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.txt_gameName.Location = new System.Drawing.Point(12, 51);
            this.txt_gameName.Name = "txt_gameName";
            this.txt_gameName.Padding = new System.Windows.Forms.Padding(1);
            this.txt_gameName.Size = new System.Drawing.Size(850, 33);
            this.txt_gameName.TabIndex = 2;
            this.txt_gameName.TextBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txt_gameName.TextBoxForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.txt_gameName.UsePasswordChar = false;
            this.txt_gameName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.txt_gameName.WaterMarkText = "Game Name";
            this.txt_gameName.TextChanged += new System.EventHandler(this.txt_gameName_OnTextChanged);
            this.txt_gameName.TextBoxKeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_gameName_TextBoxKeyDown);
            // 
            // panel_browse
            // 
            this.panel_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_browse.Controls.Add(this.list_handlers);
            this.panel_browse.Location = new System.Drawing.Point(388, 90);
            this.panel_browse.Name = "panel_browse";
            this.panel_browse.Size = new System.Drawing.Size(578, 648);
            this.panel_browse.TabIndex = 12;
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
            // radio_browse
            // 
            this.radio_browse.Appearance = System.Windows.Forms.Appearance.Button;
            this.radio_browse.AutoSize = true;
            this.radio_browse.Checked = true;
            this.radio_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radio_browse.Location = new System.Drawing.Point(12, 12);
            this.radio_browse.Name = "radio_browse";
            this.radio_browse.Size = new System.Drawing.Size(73, 33);
            this.radio_browse.TabIndex = 0;
            this.radio_browse.TabStop = true;
            this.radio_browse.Text = "Browse";
            this.radio_browse.UseVisualStyleBackColor = true;
            this.radio_browse.CheckedChanged += new System.EventHandler(this.radio_browse_CheckedChanged);
            // 
            // radio_installed
            // 
            this.radio_installed.Appearance = System.Windows.Forms.Appearance.Button;
            this.radio_installed.AutoSize = true;
            this.radio_installed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radio_installed.Location = new System.Drawing.Point(91, 12);
            this.radio_installed.Name = "radio_installed";
            this.radio_installed.Size = new System.Drawing.Size(80, 33);
            this.radio_installed.TabIndex = 1;
            this.radio_installed.Text = "Installed";
            this.radio_installed.UseVisualStyleBackColor = true;
            this.radio_installed.CheckedChanged += new System.EventHandler(this.radio_browse_CheckedChanged);
            // 
            // panel_installed
            // 
            this.panel_installed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_installed.Location = new System.Drawing.Point(388, 90);
            this.panel_installed.Name = "panel_installed";
            this.panel_installed.Size = new System.Drawing.Size(578, 648);
            this.panel_installed.TabIndex = 18;
            this.panel_installed.Visible = false;
            // 
            // btn_search
            // 
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Location = new System.Drawing.Point(868, 51);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(98, 33);
            this.btn_search.TabIndex = 3;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // PkgManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 750);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.panel_installed);
            this.Controls.Add(this.radio_installed);
            this.Controls.Add(this.radio_browse);
            this.Controls.Add(this.panel_browse);
            this.Controls.Add(this.txt_gameName);
            this.Controls.Add(this.list_left);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PkgManagerForm";
            this.Text = "Handler Manager";
            this.panel_browse.ResumeLayout(false);
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
        private System.Windows.Forms.Button btn_search;
    }
}