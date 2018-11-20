namespace Nucleus.Coop.App.Controls {
    partial class GamePageControl {
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
            this.panel_steps = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_steps
            // 
            this.panel_steps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_steps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_steps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.panel_steps.Location = new System.Drawing.Point(1, 0);
            this.panel_steps.Name = "panel_steps";
            this.panel_steps.Size = new System.Drawing.Size(781, 628);
            this.panel_steps.TabIndex = 32;
            // 
            // GamePageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_steps);
            this.Name = "GamePageControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_steps;
    }
}
