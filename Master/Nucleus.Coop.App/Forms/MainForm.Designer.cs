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
            this.panel_handlerManager = new System.Windows.Forms.Panel();
            this.panel_pageTitle = new System.Windows.Forms.Panel();
            this.panel_gameBtns = new System.Windows.Forms.Panel();
            this.btn_previous = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.titleBarControl1 = new Nucleus.Gaming.Platform.Windows.Controls.TitleBarControl();
            this.list_games = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.panel_game = new System.Windows.Forms.Panel();
            this.imgBtn_handlers = new Nucleus.Gaming.Platform.Windows.Controls.ImageButton();
            this.btn_autoSearchGame = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.lbl_handler = new System.Windows.Forms.Label();
            this.combo_handlers = new System.Windows.Forms.ComboBox();
            this.lbl_stepTitle = new System.Windows.Forms.Label();
            this.panel_steps = new System.Windows.Forms.Panel();
            this.panel_noGames = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.handlerManagerControl1 = new Nucleus.Coop.App.Controls.HandlerManagerControl();
            this.gameNameControl = new Nucleus.Coop.Controls.GameNameControl();
            this.panel_handlerManager.SuspendLayout();
            this.panel_pageTitle.SuspendLayout();
            this.panel_gameBtns.SuspendLayout();
            this.panel_game.SuspendLayout();
            this.panel_noGames.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_handlerManager
            // 
            this.panel_handlerManager.Controls.Add(this.handlerManagerControl1);
            this.panel_handlerManager.Location = new System.Drawing.Point(272, 73);
            this.panel_handlerManager.Name = "panel_handlerManager";
            this.panel_handlerManager.Size = new System.Drawing.Size(782, 628);
            this.panel_handlerManager.TabIndex = 33;
            // 
            // panel_pageTitle
            // 
            this.panel_pageTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_pageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.panel_pageTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_pageTitle.Controls.Add(this.panel_gameBtns);
            this.panel_pageTitle.Controls.Add(this.gameNameControl);
            this.panel_pageTitle.Location = new System.Drawing.Point(272, 21);
            this.panel_pageTitle.Margin = new System.Windows.Forms.Padding(0);
            this.panel_pageTitle.Name = "panel_pageTitle";
            this.panel_pageTitle.Size = new System.Drawing.Size(782, 52);
            this.panel_pageTitle.TabIndex = 33;
            // 
            // panel_gameBtns
            // 
            this.panel_gameBtns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_gameBtns.Controls.Add(this.btn_previous);
            this.panel_gameBtns.Controls.Add(this.btn_next);
            this.panel_gameBtns.Controls.Add(this.btn_play);
            this.panel_gameBtns.Location = new System.Drawing.Point(617, 5);
            this.panel_gameBtns.Name = "panel_gameBtns";
            this.panel_gameBtns.Size = new System.Drawing.Size(158, 40);
            this.panel_gameBtns.TabIndex = 28;
            // 
            // btn_previous
            // 
            this.btn_previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_previous.Enabled = false;
            this.btn_previous.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_previous.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_previous.Location = new System.Drawing.Point(5, 3);
            this.btn_previous.Name = "btn_previous";
            this.btn_previous.Size = new System.Drawing.Size(33, 34);
            this.btn_previous.TabIndex = 25;
            this.btn_previous.Text = "<";
            this.btn_previous.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_previous.UseVisualStyleBackColor = true;
            // 
            // btn_next
            // 
            this.btn_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_next.Enabled = false;
            this.btn_next.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_next.Location = new System.Drawing.Point(44, 3);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(33, 34);
            this.btn_next.TabIndex = 27;
            this.btn_next.Text = ">";
            this.btn_next.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_next.UseVisualStyleBackColor = true;
            // 
            // btn_play
            // 
            this.btn_play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_play.Enabled = false;
            this.btn_play.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_play.Location = new System.Drawing.Point(80, 3);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(76, 34);
            this.btn_play.TabIndex = 24;
            this.btn_play.Text = "P L A Y";
            this.btn_play.UseVisualStyleBackColor = true;
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
            this.titleBarControl1.Size = new System.Drawing.Size(1054, 21);
            this.titleBarControl1.TabIndex = 24;
            // 
            // list_games
            // 
            this.list_games.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_games.AutoScroll = true;
            this.list_games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.list_games.Border = 1;
            this.list_games.Location = new System.Drawing.Point(0, 21);
            this.list_games.Margin = new System.Windows.Forms.Padding(0);
            this.list_games.Name = "list_games";
            this.list_games.Offset = new System.Drawing.Size(0, 2);
            this.list_games.Size = new System.Drawing.Size(272, 680);
            this.list_games.TabIndex = 3;
            // 
            // panel_game
            // 
            this.panel_game.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_game.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_game.Controls.Add(this.imgBtn_handlers);
            this.panel_game.Controls.Add(this.btn_autoSearchGame);
            this.panel_game.Controls.Add(this.btn_browse);
            this.panel_game.Controls.Add(this.lbl_handler);
            this.panel_game.Controls.Add(this.combo_handlers);
            this.panel_game.Controls.Add(this.lbl_stepTitle);
            this.panel_game.Controls.Add(this.panel_steps);
            this.panel_game.Location = new System.Drawing.Point(272, 72);
            this.panel_game.Name = "panel_game";
            this.panel_game.Size = new System.Drawing.Size(782, 629);
            this.panel_game.TabIndex = 26;
            // 
            // imgBtn_handlers
            // 
            this.imgBtn_handlers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgBtn_handlers.Image = global::Nucleus.Coop.App.Properties.Resources.icon;
            this.imgBtn_handlers.Location = new System.Drawing.Point(679, -1);
            this.imgBtn_handlers.Name = "imgBtn_handlers";
            this.imgBtn_handlers.Size = new System.Drawing.Size(34, 34);
            this.imgBtn_handlers.TabIndex = 31;
            this.imgBtn_handlers.Click += new System.EventHandler(this.imgBtn_handlers_Click);
            // 
            // btn_autoSearchGame
            // 
            this.btn_autoSearchGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_autoSearchGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_autoSearchGame.Location = new System.Drawing.Point(751, 3);
            this.btn_autoSearchGame.Name = "btn_autoSearchGame";
            this.btn_autoSearchGame.Size = new System.Drawing.Size(26, 29);
            this.btn_autoSearchGame.TabIndex = 26;
            this.btn_autoSearchGame.Text = "Auto Search";
            this.btn_autoSearchGame.UseVisualStyleBackColor = true;
            this.btn_autoSearchGame.Visible = false;
            // 
            // btn_browse
            // 
            this.btn_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_browse.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_browse.Location = new System.Drawing.Point(719, 3);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(26, 29);
            this.btn_browse.TabIndex = 28;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Visible = false;
            // 
            // lbl_handler
            // 
            this.lbl_handler.AutoSize = true;
            this.lbl_handler.Location = new System.Drawing.Point(218, 7);
            this.lbl_handler.Name = "lbl_handler";
            this.lbl_handler.Size = new System.Drawing.Size(65, 21);
            this.lbl_handler.TabIndex = 30;
            this.lbl_handler.Text = "Handler";
            // 
            // combo_handlers
            // 
            this.combo_handlers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_handlers.FormattingEnabled = true;
            this.combo_handlers.ItemHeight = 21;
            this.combo_handlers.Location = new System.Drawing.Point(307, 4);
            this.combo_handlers.Name = "combo_handlers";
            this.combo_handlers.Size = new System.Drawing.Size(105, 29);
            this.combo_handlers.TabIndex = 29;
            // 
            // lbl_stepTitle
            // 
            this.lbl_stepTitle.AutoSize = true;
            this.lbl_stepTitle.Location = new System.Drawing.Point(3, 7);
            this.lbl_stepTitle.Name = "lbl_stepTitle";
            this.lbl_stepTitle.Size = new System.Drawing.Size(127, 21);
            this.lbl_stepTitle.TabIndex = 23;
            this.lbl_stepTitle.Text = "Nothing selected";
            // 
            // panel_steps
            // 
            this.panel_steps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_steps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_steps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.panel_steps.Location = new System.Drawing.Point(5, 38);
            this.panel_steps.Name = "panel_steps";
            this.panel_steps.Size = new System.Drawing.Size(775, 589);
            this.panel_steps.TabIndex = 22;
            // 
            // panel_noGames
            // 
            this.panel_noGames.Controls.Add(this.label1);
            this.panel_noGames.Location = new System.Drawing.Point(272, 72);
            this.panel_noGames.Name = "panel_noGames";
            this.panel_noGames.Size = new System.Drawing.Size(782, 629);
            this.panel_noGames.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "No games installed";
            // 
            // handlerManagerControl1
            // 
            this.handlerManagerControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.handlerManagerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.handlerManagerControl1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.handlerManagerControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.handlerManagerControl1.Location = new System.Drawing.Point(0, 0);
            this.handlerManagerControl1.Margin = new System.Windows.Forms.Padding(4);
            this.handlerManagerControl1.Name = "handlerManagerControl1";
            this.handlerManagerControl1.Size = new System.Drawing.Size(782, 628);
            this.handlerManagerControl1.TabIndex = 0;
            // 
            // gameNameControl
            // 
            this.gameNameControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.gameNameControl.GameInfo = null;
            this.gameNameControl.Location = new System.Drawing.Point(3, 2);
            this.gameNameControl.Name = "gameNameControl";
            this.gameNameControl.Size = new System.Drawing.Size(232, 46);
            this.gameNameControl.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1054, 701);
            this.Controls.Add(this.panel_handlerManager);
            this.Controls.Add(this.panel_pageTitle);
            this.Controls.Add(this.titleBarControl1);
            this.Controls.Add(this.list_games);
            this.Controls.Add(this.panel_game);
            this.Controls.Add(this.panel_noGames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainForm";
            this.Text = "Nucleus Coop";
            this.panel_handlerManager.ResumeLayout(false);
            this.panel_pageTitle.ResumeLayout(false);
            this.panel_gameBtns.ResumeLayout(false);
            this.panel_game.ResumeLayout(false);
            this.panel_game.PerformLayout();
            this.panel_noGames.ResumeLayout(false);
            this.panel_noGames.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Gaming.Platform.Windows.Controls.ControlListBox list_games;
        private Gaming.Platform.Windows.Controls.TitleBarControl titleBarControl1;
        private System.Windows.Forms.Panel panel_game;
        private Gaming.Platform.Windows.Controls.ImageButton imgBtn_handlers;
        private System.Windows.Forms.Button btn_autoSearchGame;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Label lbl_handler;
        private System.Windows.Forms.ComboBox combo_handlers;
        private System.Windows.Forms.Label lbl_stepTitle;
        private System.Windows.Forms.Panel panel_steps;
        private Coop.Controls.GameNameControl gameNameControl;
        private System.Windows.Forms.Panel panel_gameBtns;
        private System.Windows.Forms.Button btn_previous;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Panel panel_pageTitle;
        private System.Windows.Forms.Panel panel_noGames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_handlerManager;
        private Controls.HandlerManagerControl handlerManagerControl1;
    }
}