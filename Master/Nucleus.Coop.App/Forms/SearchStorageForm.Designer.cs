namespace Nucleus.Coop.App.Forms
{
    partial class SearchStorageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchStorageForm));
            this.list_storage = new System.Windows.Forms.CheckedListBox();
            this.lbl_selectDrives = new System.Windows.Forms.Label();
            this.lbl_foundGames = new System.Windows.Forms.Label();
            this.list_games = new System.Windows.Forms.ListBox();
            this.progress_search = new System.Windows.Forms.ProgressBar();
            this.lbl_progress = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.panel_formContent = new System.Windows.Forms.Panel();
            this.titleBarControl1 = new Nucleus.Gaming.Platform.Windows.Controls.TitleBarControl();
            this.panel_formContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_storage
            // 
            this.list_storage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_storage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.list_storage.CheckOnClick = true;
            this.list_storage.ForeColor = System.Drawing.Color.White;
            this.list_storage.FormattingEnabled = true;
            this.list_storage.IntegralHeight = false;
            this.list_storage.Location = new System.Drawing.Point(9, 53);
            this.list_storage.Name = "list_storage";
            this.list_storage.Size = new System.Drawing.Size(336, 319);
            this.list_storage.TabIndex = 0;
            // 
            // lbl_selectDrives
            // 
            this.lbl_selectDrives.AutoSize = true;
            this.lbl_selectDrives.Location = new System.Drawing.Point(5, 29);
            this.lbl_selectDrives.Name = "lbl_selectDrives";
            this.lbl_selectDrives.Size = new System.Drawing.Size(108, 21);
            this.lbl_selectDrives.TabIndex = 1;
            this.lbl_selectDrives.Text = "Select Devices";
            // 
            // lbl_foundGames
            // 
            this.lbl_foundGames.AutoSize = true;
            this.lbl_foundGames.Location = new System.Drawing.Point(351, 29);
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
            this.list_games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.list_games.ForeColor = System.Drawing.Color.White;
            this.list_games.FormattingEnabled = true;
            this.list_games.IntegralHeight = false;
            this.list_games.ItemHeight = 21;
            this.list_games.Location = new System.Drawing.Point(354, 53);
            this.list_games.Name = "list_games";
            this.list_games.Size = new System.Drawing.Size(337, 396);
            this.list_games.TabIndex = 3;
            // 
            // progress_search
            // 
            this.progress_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progress_search.Location = new System.Drawing.Point(9, 426);
            this.progress_search.Name = "progress_search";
            this.progress_search.Size = new System.Drawing.Size(336, 23);
            this.progress_search.TabIndex = 6;
            // 
            // lbl_progress
            // 
            this.lbl_progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_progress.AutoSize = true;
            this.lbl_progress.Location = new System.Drawing.Point(5, 398);
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
            this.btn_search.Location = new System.Drawing.Point(242, 376);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(103, 43);
            this.btn_search.TabIndex = 8;
            this.btn_search.Text = "Search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // panel_formContent
            // 
            this.panel_formContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.panel_formContent.Controls.Add(this.titleBarControl1);
            this.panel_formContent.Controls.Add(this.btn_search);
            this.panel_formContent.Controls.Add(this.list_storage);
            this.panel_formContent.Controls.Add(this.lbl_progress);
            this.panel_formContent.Controls.Add(this.list_games);
            this.panel_formContent.Controls.Add(this.progress_search);
            this.panel_formContent.Controls.Add(this.lbl_selectDrives);
            this.panel_formContent.Controls.Add(this.lbl_foundGames);
            this.panel_formContent.Location = new System.Drawing.Point(0, 0);
            this.panel_formContent.Name = "panel_formContent";
            this.panel_formContent.Size = new System.Drawing.Size(703, 461);
            this.panel_formContent.TabIndex = 9;
            // 
            // titleBarControl1
            // 
            this.titleBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBarControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.titleBarControl1.EnableMaximize = true;
            this.titleBarControl1.Location = new System.Drawing.Point(0, 0);
            this.titleBarControl1.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarControl1.Name = "titleBarControl1";
            this.titleBarControl1.Size = new System.Drawing.Size(703, 21);
            this.titleBarControl1.TabIndex = 9;
            // 
            // SearchDisksForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(703, 461);
            this.Controls.Add(this.panel_formContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchDisksForm";
            this.Text = "Search Drives";
            this.panel_formContent.ResumeLayout(false);
            this.panel_formContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox list_storage;
        private System.Windows.Forms.Label lbl_selectDrives;
        private System.Windows.Forms.Label lbl_foundGames;
        private System.Windows.Forms.ListBox list_games;
        private System.Windows.Forms.ProgressBar progress_search;
        private System.Windows.Forms.Label lbl_progress;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Panel panel_formContent;
        private Gaming.Platform.Windows.Controls.TitleBarControl titleBarControl1;
    }
}