namespace Nucleus.Coop.Controls.Repo
{
    partial class RepoGameControl
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
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.label_GameName = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // picHeader
            // 
            this.picHeader.Location = new System.Drawing.Point(6, 6);
            this.picHeader.Margin = new System.Windows.Forms.Padding(6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(90, 90);
            this.picHeader.TabIndex = 0;
            this.picHeader.TabStop = false;
            // 
            // label_GameName
            // 
            this.label_GameName.AutoSize = true;
            this.label_GameName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label_GameName.ForeColor = System.Drawing.Color.White;
            this.label_GameName.Location = new System.Drawing.Point(103, 6);
            this.label_GameName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_GameName.Name = "label_GameName";
            this.label_GameName.Size = new System.Drawing.Size(118, 31);
            this.label_GameName.TabIndex = 1;
            this.label_GameName.Text = "Game Name";
            this.label_GameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_GameName.UseCompatibleTextRendering = true;
            // 
            // lbl_Description
            // 
            this.lbl_Description.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lbl_Description.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Description.Location = new System.Drawing.Point(105, 37);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(352, 59);
            this.lbl_Description.TabIndex = 2;
            this.lbl_Description.Text = "Description";
            // 
            // RepoGameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.label_GameName);
            this.Controls.Add(this.picHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "RepoGameControl";
            this.Size = new System.Drawing.Size(460, 102);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label label_GameName;
        private System.Windows.Forms.Label lbl_Description;
    }
}
