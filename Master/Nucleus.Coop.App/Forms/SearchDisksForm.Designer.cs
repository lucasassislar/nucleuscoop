namespace Nucleus.Coop.App.Forms
{
    partial class SearchDisksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDisksForm));
            this.listBox_drives = new System.Windows.Forms.CheckedListBox();
            this.lbl_selectDrives = new System.Windows.Forms.Label();
            this.lbl_foundGames = new System.Windows.Forms.Label();
            this.list_games = new System.Windows.Forms.ListBox();
            this.progress_search = new System.Windows.Forms.ProgressBar();
            this.lbl_progress = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_drives
            // 
            this.listBox_drives.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_drives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.listBox_drives.CheckOnClick = true;
            this.listBox_drives.ForeColor = System.Drawing.Color.White;
            this.listBox_drives.FormattingEnabled = true;
            this.listBox_drives.IntegralHeight = false;
            this.listBox_drives.Location = new System.Drawing.Point(12, 33);
            this.listBox_drives.Name = "listBox_drives";
            this.listBox_drives.Size = new System.Drawing.Size(336, 335);
            this.listBox_drives.TabIndex = 0;
            // 
            // lbl_selectDrives
            // 
            this.lbl_selectDrives.AutoSize = true;
            this.lbl_selectDrives.Location = new System.Drawing.Point(12, 9);
            this.lbl_selectDrives.Name = "lbl_selectDrives";
            this.lbl_selectDrives.Size = new System.Drawing.Size(99, 21);
            this.lbl_selectDrives.TabIndex = 1;
            this.lbl_selectDrives.Text = "Select Drives";
            // 
            // lbl_foundGames
            // 
            this.lbl_foundGames.AutoSize = true;
            this.lbl_foundGames.Location = new System.Drawing.Point(354, 9);
            this.lbl_foundGames.Name = "lbl_foundGames";
            this.lbl_foundGames.Size = new System.Drawing.Size(106, 21);
            this.lbl_foundGames.TabIndex = 2;
            this.lbl_foundGames.Text = "Found Games";
            // 
            // list_games
            // 
            this.list_games.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.list_games.ForeColor = System.Drawing.Color.White;
            this.list_games.FormattingEnabled = true;
            this.list_games.IntegralHeight = false;
            this.list_games.ItemHeight = 21;
            this.list_games.Location = new System.Drawing.Point(357, 33);
            this.list_games.Name = "list_games";
            this.list_games.Size = new System.Drawing.Size(337, 412);
            this.list_games.TabIndex = 3;
            // 
            // progress_search
            // 
            this.progress_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progress_search.Location = new System.Drawing.Point(12, 422);
            this.progress_search.Name = "progress_search";
            this.progress_search.Size = new System.Drawing.Size(336, 23);
            this.progress_search.TabIndex = 6;
            // 
            // lbl_progress
            // 
            this.lbl_progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_progress.AutoSize = true;
            this.lbl_progress.Location = new System.Drawing.Point(8, 394);
            this.lbl_progress.Name = "lbl_progress";
            this.lbl_progress.Size = new System.Drawing.Size(71, 21);
            this.lbl_progress.TabIndex = 7;
            this.lbl_progress.Text = "Progress";
            this.lbl_progress.Visible = false;
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Location = new System.Drawing.Point(245, 372);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(103, 43);
            this.btn_search.TabIndex = 8;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // SearchDisksForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(703, 461);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.lbl_progress);
            this.Controls.Add(this.progress_search);
            this.Controls.Add(this.list_games);
            this.Controls.Add(this.lbl_foundGames);
            this.Controls.Add(this.lbl_selectDrives);
            this.Controls.Add(this.listBox_drives);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchDisksForm";
            this.Text = "Search Drives";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listBox_drives;
        private System.Windows.Forms.Label lbl_selectDrives;
        private System.Windows.Forms.Label lbl_foundGames;
        private System.Windows.Forms.ListBox list_games;
        private System.Windows.Forms.ProgressBar progress_search;
        private System.Windows.Forms.Label lbl_progress;
        private System.Windows.Forms.Button btn_search;
    }
}