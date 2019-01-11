namespace Nucleus.Coop.App.Controls.Pages {
    partial class SelectGameFolderPageControl {
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
            this.list_gameFolders = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.SuspendLayout();
            // 
            // list_gameFolders
            // 
            this.list_gameFolders.Border = 1;
            this.list_gameFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_gameFolders.Location = new System.Drawing.Point(0, 0);
            this.list_gameFolders.Name = "list_gameFolders";
            this.list_gameFolders.Offset = new System.Drawing.Size(0, 0);
            this.list_gameFolders.Size = new System.Drawing.Size(782, 628);
            this.list_gameFolders.TabIndex = 0;
            // 
            // SelectGameFolderPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.list_gameFolders);
            this.Name = "SelectGameFolderPageControl";
            this.ResumeLayout(false);

        }

        #endregion

        private Gaming.Platform.Windows.Controls.ControlListBox list_gameFolders;
    }
}
