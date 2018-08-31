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
            this.btn_autoSearchGame = new System.Windows.Forms.Button();
            this.btn_handlers = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.lbl_handler = new System.Windows.Forms.Label();
            this.combo_handlers = new System.Windows.Forms.ComboBox();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_previous = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.lbl_stepTitle = new System.Windows.Forms.Label();
            this.panel_steps = new System.Windows.Forms.Panel();
            this.gameNameControl = new Nucleus.Coop.Controls.GameNameControl();
            this.imgBtn_handlers = new Nucleus.Gaming.Platform.Windows.Controls.ImageButton();
            this.list_games = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.SuspendLayout();
            // 
            // btn_autoSearchGame
            // 
            this.btn_autoSearchGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_autoSearchGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_autoSearchGame.Location = new System.Drawing.Point(627, 12);
            this.btn_autoSearchGame.Name = "btn_autoSearchGame";
            this.btn_autoSearchGame.Size = new System.Drawing.Size(46, 46);
            this.btn_autoSearchGame.TabIndex = 10;
            this.btn_autoSearchGame.Text = "Auto Search";
            this.btn_autoSearchGame.UseVisualStyleBackColor = true;
            this.btn_autoSearchGame.Click += new System.EventHandler(this.btnAutoSearch_Click);
            // 
            // btn_handlers
            // 
            this.btn_handlers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_handlers.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_handlers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_handlers.Image = global::Nucleus.Coop.App.Properties.Resources.icon;
            this.btn_handlers.Location = new System.Drawing.Point(679, 12);
            this.btn_handlers.Name = "btn_handlers";
            this.btn_handlers.Size = new System.Drawing.Size(46, 46);
            this.btn_handlers.TabIndex = 20;
            this.btn_handlers.Text = "Handlers";
            this.btn_handlers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_handlers.UseVisualStyleBackColor = true;
            // 
            // btn_browse
            // 
            this.btn_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_browse.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_browse.Location = new System.Drawing.Point(575, 12);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(46, 46);
            this.btn_browse.TabIndex = 15;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // lbl_handler
            // 
            this.lbl_handler.AutoSize = true;
            this.lbl_handler.Location = new System.Drawing.Point(276, 77);
            this.lbl_handler.Name = "lbl_handler";
            this.lbl_handler.Size = new System.Drawing.Size(65, 21);
            this.lbl_handler.TabIndex = 17;
            this.lbl_handler.Text = "Handler";
            // 
            // combo_handlers
            // 
            this.combo_handlers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_handlers.FormattingEnabled = true;
            this.combo_handlers.ItemHeight = 21;
            this.combo_handlers.Location = new System.Drawing.Point(391, 69);
            this.combo_handlers.Name = "combo_handlers";
            this.combo_handlers.Size = new System.Drawing.Size(471, 29);
            this.combo_handlers.TabIndex = 16;
            this.combo_handlers.SelectedIndexChanged += new System.EventHandler(this.combo_Handlers_SelectedIndexChanged);
            // 
            // btn_next
            // 
            this.btn_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_next.Enabled = false;
            this.btn_next.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_next.Location = new System.Drawing.Point(906, 69);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(33, 29);
            this.btn_next.TabIndex = 11;
            this.btn_next.Text = ">";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btn_previous
            // 
            this.btn_previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_previous.Enabled = false;
            this.btn_previous.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_previous.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_previous.Location = new System.Drawing.Point(867, 69);
            this.btn_previous.Name = "btn_previous";
            this.btn_previous.Size = new System.Drawing.Size(33, 29);
            this.btn_previous.TabIndex = 9;
            this.btn_previous.Text = "<";
            this.btn_previous.UseVisualStyleBackColor = true;
            this.btn_previous.Click += new System.EventHandler(this.arrow_Back_Click);
            // 
            // btn_play
            // 
            this.btn_play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_play.Enabled = false;
            this.btn_play.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_play.Location = new System.Drawing.Point(945, 69);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(97, 29);
            this.btn_play.TabIndex = 4;
            this.btn_play.Text = "P L A Y";
            this.btn_play.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // lbl_stepTitle
            // 
            this.lbl_stepTitle.AutoSize = true;
            this.lbl_stepTitle.Location = new System.Drawing.Point(276, 113);
            this.lbl_stepTitle.Name = "lbl_stepTitle";
            this.lbl_stepTitle.Size = new System.Drawing.Size(127, 21);
            this.lbl_stepTitle.TabIndex = 3;
            this.lbl_stepTitle.Text = "Nothing selected";
            // 
            // panel_steps
            // 
            this.panel_steps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_steps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_steps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel_steps.Location = new System.Drawing.Point(280, 137);
            this.panel_steps.Name = "panel_steps";
            this.panel_steps.Size = new System.Drawing.Size(762, 552);
            this.panel_steps.TabIndex = 0;
            // 
            // gameNameControl
            // 
            this.gameNameControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.gameNameControl.GameInfo = null;
            this.gameNameControl.Location = new System.Drawing.Point(280, 12);
            this.gameNameControl.Name = "gameNameControl";
            this.gameNameControl.Size = new System.Drawing.Size(232, 46);
            this.gameNameControl.TabIndex = 13;
            // 
            // imgBtn_handlers
            // 
            this.imgBtn_handlers.Image = global::Nucleus.Coop.App.Properties.Resources.icon;
            this.imgBtn_handlers.Location = new System.Drawing.Point(996, 9);
            this.imgBtn_handlers.Name = "imgBtn_handlers";
            this.imgBtn_handlers.Size = new System.Drawing.Size(46, 46);
            this.imgBtn_handlers.TabIndex = 21;
            this.imgBtn_handlers.Click += new System.EventHandler(this.imgBtn_handlers_Click);
            // 
            // list_games
            // 
            this.list_games.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_games.AutoScroll = true;
            this.list_games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.list_games.Border = 1;
            this.list_games.Location = new System.Drawing.Point(9, 9);
            this.list_games.Margin = new System.Windows.Forms.Padding(0);
            this.list_games.Name = "list_games";
            this.list_games.Offset = new System.Drawing.Size(0, 2);
            this.list_games.Size = new System.Drawing.Size(263, 683);
            this.list_games.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1054, 701);
            this.Controls.Add(this.imgBtn_handlers);
            this.Controls.Add(this.list_games);
            this.Controls.Add(this.btn_autoSearchGame);
            this.Controls.Add(this.btn_handlers);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.lbl_handler);
            this.Controls.Add(this.combo_handlers);
            this.Controls.Add(this.gameNameControl);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.btn_previous);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.lbl_stepTitle);
            this.Controls.Add(this.panel_steps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainForm";
            this.Text = "Nucleus Coop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_steps;
        private System.Windows.Forms.Label lbl_stepTitle;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Button btn_previous;
        private System.Windows.Forms.Button btn_autoSearchGame;
        private System.Windows.Forms.Button btn_next;
        private Coop.Controls.GameNameControl gameNameControl;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.ComboBox combo_handlers;
        private System.Windows.Forms.Label lbl_handler;
        private Gaming.Platform.Windows.Controls.ControlListBox list_games;
        private System.Windows.Forms.Button btn_handlers;
        private Gaming.Platform.Windows.Controls.ImageButton imgBtn_handlers;
    }
}