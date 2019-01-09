namespace Nucleus.Coop.App.Controls {
    partial class GamePageBrowserControl {
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
            this.btn_previous = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_previous
            // 
            this.btn_previous.Enabled = false;
            this.btn_previous.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btn_previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_previous.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_previous.Location = new System.Drawing.Point(3, 3);
            this.btn_previous.Name = "btn_previous";
            this.btn_previous.Size = new System.Drawing.Size(33, 34);
            this.btn_previous.TabIndex = 29;
            this.btn_previous.Text = "<";
            this.btn_previous.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_previous.UseVisualStyleBackColor = true;
            this.btn_previous.Click += new System.EventHandler(this.btn_previous_Click);
            // 
            // btn_next
            // 
            this.btn_next.Enabled = false;
            this.btn_next.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_next.Location = new System.Drawing.Point(42, 3);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(33, 34);
            this.btn_next.TabIndex = 30;
            this.btn_next.Text = ">";
            this.btn_next.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_play
            // 
            this.btn_play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_play.Enabled = false;
            this.btn_play.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btn_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_play.Location = new System.Drawing.Point(80, 3);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(76, 34);
            this.btn_play.TabIndex = 28;
            this.btn_play.Text = "P L A Y";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // GamePageBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_previous);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.btn_play);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.Name = "GamePageBrowserControl";
            this.Size = new System.Drawing.Size(156, 41);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_previous;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_play;
    }
}
