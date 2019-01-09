namespace Nucleus.Coop.App.Controls
{
    partial class HandlerInfoControl
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_handlerName = new System.Windows.Forms.Label();
            this.lbl_description = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.lbl_devName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pictureBox1.Location = new System.Drawing.Point(4, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 97);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_handlerName
            // 
            this.lbl_handlerName.AutoSize = true;
            this.lbl_handlerName.Location = new System.Drawing.Point(102, 5);
            this.lbl_handlerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_handlerName.Name = "lbl_handlerName";
            this.lbl_handlerName.Size = new System.Drawing.Size(111, 21);
            this.lbl_handlerName.TabIndex = 1;
            this.lbl_handlerName.Text = "Handler Name";
            // 
            // lbl_description
            // 
            this.lbl_description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_description.Location = new System.Drawing.Point(102, 26);
            this.lbl_description.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Size = new System.Drawing.Size(333, 55);
            this.lbl_description.TabIndex = 2;
            this.lbl_description.Text = "Handler Description";
            // 
            // lbl_version
            // 
            this.lbl_version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_version.AutoSize = true;
            this.lbl_version.Location = new System.Drawing.Point(384, 5);
            this.lbl_version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(51, 21);
            this.lbl_version.TabIndex = 3;
            this.lbl_version.Text = "v1.0.0";
            // 
            // lbl_devName
            // 
            this.lbl_devName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_devName.AutoSize = true;
            this.lbl_devName.Location = new System.Drawing.Point(308, 81);
            this.lbl_devName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_devName.Name = "lbl_devName";
            this.lbl_devName.Size = new System.Drawing.Size(127, 21);
            this.lbl_devName.TabIndex = 4;
            this.lbl_devName.Text = "Developer Name";
            // 
            // HandlerInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_devName);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.lbl_description);
            this.Controls.Add(this.lbl_handlerName);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HandlerInfoControl";
            this.Size = new System.Drawing.Size(439, 106);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_handlerName;
        private System.Windows.Forms.Label lbl_description;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_devName;
    }
}
