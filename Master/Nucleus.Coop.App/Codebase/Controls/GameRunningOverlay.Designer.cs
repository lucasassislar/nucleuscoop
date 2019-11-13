namespace Nucleus.Coop.App.Controls {
    partial class GameRunningOverlay
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
            this.btn_Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Stop
            // 
            this.btn_Stop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_Stop.Location = new System.Drawing.Point(219, 223);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(368, 133);
            this.btn_Stop.TabIndex = 0;
            this.btn_Stop.Text = "Ｓ Ｔ Ｏ Ｐ";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // GameRunningOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.Controls.Add(this.btn_Stop);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "GameRunningOverlay";
            this.Size = new System.Drawing.Size(810, 601);
            this.SizeChanged += new System.EventHandler(this.GameRunningOverlay_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Stop;
    }
}
