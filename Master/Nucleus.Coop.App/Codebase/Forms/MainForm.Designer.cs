namespace Nucleus.Coop.App.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.titleBarControl = new Nucleus.Gaming.Platform.Windows.Controls.TitleBarControl();
            this.panel_formContent = new System.Windows.Forms.Panel();
            this.panel_pageTitle = new System.Windows.Forms.Panel();
            this.panel_bottomLine = new System.Windows.Forms.Panel();
            this.gamePageBrowserControl = new Nucleus.Coop.App.Controls.GamePageBrowserControl();
            this.gameNameControl = new Nucleus.Coop.App.Controls.GameNameControl();
            this.list_games = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.panel_allPages = new System.Windows.Forms.Panel();
            this.selectGameFolderPageControl = new Nucleus.Coop.App.Controls.Pages.SelectGameFolderPageControl();
            this.gamePageControl = new Nucleus.Coop.App.Controls.GamePageControl();
            this.noGamesInstalledPageControl = new Nucleus.Coop.App.Controls.Pages.NoGamesInstalledPageControl();
            this.handlerManagerControl = new Nucleus.Coop.App.Controls.HandlerManagerControl();
            this.panel_formContent.SuspendLayout();
            this.panel_pageTitle.SuspendLayout();
            this.panel_allPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleBarControl
            // 
            this.titleBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBarControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.titleBarControl.EnableMaximize = true;
            this.titleBarControl.Icon = global::Nucleus.Coop.App.Properties.Resources.splitscreenme;
            this.titleBarControl.Location = new System.Drawing.Point(0, 0);
            this.titleBarControl.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarControl.Name = "titleBarControl";
            this.titleBarControl.ShowIcon = false;
            this.titleBarControl.Size = new System.Drawing.Size(1007, 21);
            this.titleBarControl.StripWidth = 30;
            this.titleBarControl.TabIndex = 24;
            // 
            // panel_formContent
            // 
            this.panel_formContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_formContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.panel_formContent.Controls.Add(this.titleBarControl);
            this.panel_formContent.Controls.Add(this.panel_pageTitle);
            this.panel_formContent.Controls.Add(this.list_games);
            this.panel_formContent.Controls.Add(this.panel_allPages);
            this.panel_formContent.Location = new System.Drawing.Point(0, 0);
            this.panel_formContent.Name = "panel_formContent";
            this.panel_formContent.Size = new System.Drawing.Size(1007, 661);
            this.panel_formContent.TabIndex = 34;
            // 
            // panel_pageTitle
            // 
            this.panel_pageTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_pageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.panel_pageTitle.Controls.Add(this.panel_bottomLine);
            this.panel_pageTitle.Controls.Add(this.gamePageBrowserControl);
            this.panel_pageTitle.Controls.Add(this.gameNameControl);
            this.panel_pageTitle.Location = new System.Drawing.Point(272, 21);
            this.panel_pageTitle.Margin = new System.Windows.Forms.Padding(0);
            this.panel_pageTitle.Name = "panel_pageTitle";
            this.panel_pageTitle.Size = new System.Drawing.Size(735, 52);
            this.panel_pageTitle.TabIndex = 33;
            // 
            // panel_bottomLine
            // 
            this.panel_bottomLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_bottomLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.panel_bottomLine.Location = new System.Drawing.Point(0, 49);
            this.panel_bottomLine.Name = "panel_bottomLine";
            this.panel_bottomLine.Size = new System.Drawing.Size(735, 1);
            this.panel_bottomLine.TabIndex = 15;
            // 
            // gamePageBrowserControl
            // 
            this.gamePageBrowserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePageBrowserControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.gamePageBrowserControl.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gamePageBrowserControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gamePageBrowserControl.Location = new System.Drawing.Point(557, 5);
            this.gamePageBrowserControl.Margin = new System.Windows.Forms.Padding(8);
            this.gamePageBrowserControl.Name = "gamePageBrowserControl";
            this.gamePageBrowserControl.Size = new System.Drawing.Size(156, 41);
            this.gamePageBrowserControl.TabIndex = 14;
            // 
            // gameNameControl
            // 
            this.gameNameControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.gameNameControl.GameInfo = null;
            this.gameNameControl.Location = new System.Drawing.Point(3, 2);
            this.gameNameControl.Name = "gameNameControl";
            this.gameNameControl.Size = new System.Drawing.Size(722, 46);
            this.gameNameControl.TabIndex = 13;
            // 
            // list_games
            // 
            this.list_games.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_games.AutoScroll = true;
            this.list_games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.list_games.Border = 1;
            this.list_games.CanSelectControls = true;
            this.list_games.Location = new System.Drawing.Point(0, 21);
            this.list_games.Margin = new System.Windows.Forms.Padding(0);
            this.list_games.Name = "list_games";
            this.list_games.Offset = new System.Drawing.Size(0, 2);
            this.list_games.Size = new System.Drawing.Size(272, 640);
            this.list_games.TabIndex = 3;
            this.list_games.VerticalScrollEnabled = true;
            // 
            // panel_allPages
            // 
            this.panel_allPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_allPages.Controls.Add(this.selectGameFolderPageControl);
            this.panel_allPages.Controls.Add(this.gamePageControl);
            this.panel_allPages.Controls.Add(this.noGamesInstalledPageControl);
            this.panel_allPages.Controls.Add(this.handlerManagerControl);
            this.panel_allPages.Location = new System.Drawing.Point(272, 76);
            this.panel_allPages.Name = "panel_allPages";
            this.panel_allPages.Size = new System.Drawing.Size(735, 585);
            this.panel_allPages.TabIndex = 33;
            // 
            // selectGameFolderPageControl
            // 
            this.selectGameFolderPageControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectGameFolderPageControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.selectGameFolderPageControl.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.selectGameFolderPageControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.selectGameFolderPageControl.Location = new System.Drawing.Point(0, 0);
            this.selectGameFolderPageControl.Margin = new System.Windows.Forms.Padding(8);
            this.selectGameFolderPageControl.Name = "selectGameFolderPageControl";
            this.selectGameFolderPageControl.RequiredTitleBarWidth = 0;
            this.selectGameFolderPageControl.Size = new System.Drawing.Size(735, 585);
            this.selectGameFolderPageControl.TabIndex = 4;
            // 
            // gamePageControl
            // 
            this.gamePageControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePageControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.gamePageControl.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gamePageControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gamePageControl.Location = new System.Drawing.Point(0, 0);
            this.gamePageControl.Margin = new System.Windows.Forms.Padding(8);
            this.gamePageControl.Name = "gamePageControl";
            this.gamePageControl.RequiredTitleBarWidth = 0;
            this.gamePageControl.Size = new System.Drawing.Size(735, 585);
            this.gamePageControl.TabIndex = 2;
            // 
            // noGamesInstalledPageControl
            // 
            this.noGamesInstalledPageControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noGamesInstalledPageControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.noGamesInstalledPageControl.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.noGamesInstalledPageControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.noGamesInstalledPageControl.Location = new System.Drawing.Point(0, 0);
            this.noGamesInstalledPageControl.Margin = new System.Windows.Forms.Padding(8);
            this.noGamesInstalledPageControl.Name = "noGamesInstalledPageControl";
            this.noGamesInstalledPageControl.RequiredTitleBarWidth = 0;
            this.noGamesInstalledPageControl.Size = new System.Drawing.Size(735, 585);
            this.noGamesInstalledPageControl.TabIndex = 1;
            // 
            // handlerManagerControl
            // 
            this.handlerManagerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.handlerManagerControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.handlerManagerControl.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.handlerManagerControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.handlerManagerControl.Location = new System.Drawing.Point(0, 0);
            this.handlerManagerControl.Margin = new System.Windows.Forms.Padding(4);
            this.handlerManagerControl.Name = "handlerManagerControl";
            this.handlerManagerControl.RequiredTitleBarWidth = 273;
            this.handlerManagerControl.Size = new System.Drawing.Size(735, 585);
            this.handlerManagerControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1007, 661);
            this.Controls.Add(this.panel_formContent);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainForm";
            this.Text = "Nucleus Coop";
            this.panel_formContent.ResumeLayout(false);
            this.panel_pageTitle.ResumeLayout(false);
            this.panel_allPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Gaming.Platform.Windows.Controls.ControlListBox list_games;
        private Gaming.Platform.Windows.Controls.TitleBarControl titleBarControl;
        private Controls.GameNameControl gameNameControl;
        private System.Windows.Forms.Panel panel_pageTitle;
        private Controls.HandlerManagerControl handlerManagerControl;
        private System.Windows.Forms.Panel panel_allPages;
        private Controls.Pages.NoGamesInstalledPageControl noGamesInstalledPageControl;
        private Controls.GamePageControl gamePageControl;
        private Controls.GamePageBrowserControl gamePageBrowserControl;
        private System.Windows.Forms.Panel panel_formContent;
        private System.Windows.Forms.Panel panel_bottomLine;
        private Controls.Pages.SelectGameFolderPageControl selectGameFolderPageControl;
    }
}