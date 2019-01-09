namespace Nucleus.Coop.App.Controls.Pages {
    partial class GameManagerPageControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.list_left = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.label_exePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Location = new System.Drawing.Point(279, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 52);
            this.panel1.TabIndex = 9;
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
            this.list_left.Size = new System.Drawing.Size(272, 628);
            this.list_left.TabIndex = 8;
            // 
            // btn_remove
            // 
            this.btn_remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_remove.Enabled = false;
            this.btn_remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_remove.Location = new System.Drawing.Point(595, 571);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(184, 54);
            this.btn_remove.TabIndex = 13;
            this.btn_remove.Text = "Remove From List";
            this.btn_remove.UseVisualStyleBackColor = true;
            // 
            // label_exePath
            // 
            this.label_exePath.AutoSize = true;
            this.label_exePath.Location = new System.Drawing.Point(279, 55);
            this.label_exePath.Name = "label_exePath";
            this.label_exePath.Size = new System.Drawing.Size(23, 21);
            this.label_exePath.TabIndex = 14;
            this.label_exePath.Text = "C:";
            // 
            // GameManagerPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_exePath);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.list_left);
            this.Name = "GameManagerPageControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Gaming.Platform.Windows.Controls.ControlListBox list_left;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label label_exePath;
    }
}
