namespace Nucleus.Coop.App.Controls
{
    partial class HandlerManagerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.list_left = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.label_developer = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_nukeVer = new System.Windows.Forms.Label();
            this.btn_uninstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_left
            // 
            this.list_left.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_left.AutoScroll = true;
            this.list_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.list_left.Border = 2;
            this.list_left.Location = new System.Drawing.Point(1, 0);
            this.list_left.Name = "list_left";
            this.list_left.Offset = new System.Drawing.Size(0, 0);
            this.list_left.Size = new System.Drawing.Size(272, 629);
            this.list_left.TabIndex = 4;
            // 
            // label_developer
            // 
            this.label_developer.AutoSize = true;
            this.label_developer.Location = new System.Drawing.Point(279, 55);
            this.label_developer.Name = "label_developer";
            this.label_developer.Size = new System.Drawing.Size(127, 21);
            this.label_developer.TabIndex = 5;
            this.label_developer.Text = "Developer Name";
            // 
            // label_version
            // 
            this.label_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_version.AutoSize = true;
            this.label_version.Location = new System.Drawing.Point(748, 55);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(31, 21);
            this.label_version.TabIndex = 6;
            this.label_version.Text = "0.0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Location = new System.Drawing.Point(279, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 52);
            this.panel1.TabIndex = 7;
            // 
            // label_nukeVer
            // 
            this.label_nukeVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_nukeVer.AutoSize = true;
            this.label_nukeVer.Location = new System.Drawing.Point(700, 76);
            this.label_nukeVer.Name = "label_nukeVer";
            this.label_nukeVer.Size = new System.Drawing.Size(79, 21);
            this.label_nukeVer.TabIndex = 8;
            this.label_nukeVer.Text = "Nucleus 9";
            // 
            // btn_uninstall
            // 
            this.btn_uninstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_uninstall.Enabled = false;
            this.btn_uninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_uninstall.Location = new System.Drawing.Point(595, 572);
            this.btn_uninstall.Name = "btn_uninstall";
            this.btn_uninstall.Size = new System.Drawing.Size(184, 54);
            this.btn_uninstall.TabIndex = 9;
            this.btn_uninstall.Text = "Uninstall";
            this.btn_uninstall.UseVisualStyleBackColor = true;
            this.btn_uninstall.Click += new System.EventHandler(this.btn_uninstall_Click);
            // 
            // HandlerManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_uninstall);
            this.Controls.Add(this.label_nukeVer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.label_developer);
            this.Controls.Add(this.list_left);
            this.Name = "HandlerManagerControl";
            this.Size = new System.Drawing.Size(782, 629);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Gaming.Platform.Windows.Controls.ControlListBox list_left;
        private System.Windows.Forms.Label label_developer;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_nukeVer;
        private System.Windows.Forms.Button btn_uninstall;
    }
}
