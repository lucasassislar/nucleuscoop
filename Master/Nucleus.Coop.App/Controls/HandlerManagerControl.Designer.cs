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
            this.radio_installed = new System.Windows.Forms.RadioButton();
            this.radio_browse = new System.Windows.Forms.RadioButton();
            this.list_left = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.SuspendLayout();
            // 
            // radio_installed
            // 
            this.radio_installed.Appearance = System.Windows.Forms.Appearance.Button;
            this.radio_installed.AutoSize = true;
            this.radio_installed.Checked = true;
            this.radio_installed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radio_installed.Location = new System.Drawing.Point(87, 8);
            this.radio_installed.Name = "radio_installed";
            this.radio_installed.Size = new System.Drawing.Size(80, 33);
            this.radio_installed.TabIndex = 3;
            this.radio_installed.TabStop = true;
            this.radio_installed.Text = "Installed";
            this.radio_installed.UseVisualStyleBackColor = true;
            // 
            // radio_browse
            // 
            this.radio_browse.Appearance = System.Windows.Forms.Appearance.Button;
            this.radio_browse.AutoSize = true;
            this.radio_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radio_browse.Location = new System.Drawing.Point(8, 8);
            this.radio_browse.Name = "radio_browse";
            this.radio_browse.Size = new System.Drawing.Size(73, 33);
            this.radio_browse.TabIndex = 2;
            this.radio_browse.Text = "Browse";
            this.radio_browse.UseVisualStyleBackColor = true;
            // 
            // list_left
            // 
            this.list_left.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_left.AutoScroll = true;
            this.list_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.list_left.Border = 2;
            this.list_left.Location = new System.Drawing.Point(8, 47);
            this.list_left.Name = "list_left";
            this.list_left.Offset = new System.Drawing.Size(0, 0);
            this.list_left.Size = new System.Drawing.Size(368, 576);
            this.list_left.TabIndex = 4;
            // 
            // HandlerManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.list_left);
            this.Controls.Add(this.radio_installed);
            this.Controls.Add(this.radio_browse);
            this.Name = "HandlerManagerControl";
            this.Size = new System.Drawing.Size(782, 629);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radio_installed;
        private System.Windows.Forms.RadioButton radio_browse;
        private Gaming.Platform.Windows.Controls.ControlListBox list_left;
    }
}
