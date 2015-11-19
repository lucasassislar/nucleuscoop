namespace SplitTool
{
    partial class GamesViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GamesViewer));
            this.label_Library = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Search = new System.Windows.Forms.Label();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_AddGame = new Nucleus.Gaming.Controls.NButton();
            this.borderPanel1 = new Nucleus.Gaming.Controls.BorderPanel();
            this.btn_Presets = new Nucleus.Gaming.Controls.NButton();
            this.pic_Keyboard = new System.Windows.Forms.PictureBox();
            this.label_title = new System.Windows.Forms.Label();
            this.playerCount1 = new SplitTool.Controls.PlayerCount();
            this.monitorControl1 = new SplitTool.Controls.MonitorControl();
            this.label_maxPlayas = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_gamePad = new System.Windows.Forms.PictureBox();
            this.btn_Previous = new Nucleus.Gaming.Controls.NButton();
            this.btn_Next = new Nucleus.Gaming.Controls.NButton();
            this.button_Play = new Nucleus.Gaming.Controls.NButton();
            this.label_GameName = new System.Windows.Forms.Label();
            this.game_Box = new System.Windows.Forms.PictureBox();
            this.list_Games = new System.Windows.Forms.ListBox();
            this.playerOptions1 = new SplitTool.Controls.PlayerOptions();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.borderPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Keyboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_gamePad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_Box)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Library
            // 
            this.label_Library.AutoSize = true;
            this.label_Library.Font = new System.Drawing.Font("Segoe UI Symbol", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Library.ForeColor = System.Drawing.Color.White;
            this.label_Library.Location = new System.Drawing.Point(5, 7);
            this.label_Library.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Library.Name = "label_Library";
            this.label_Library.Size = new System.Drawing.Size(116, 37);
            this.label_Library.TabIndex = 0;
            this.label_Library.Text = "LIBRARY";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel1.Controls.Add(this.label_Search);
            this.panel1.Controls.Add(this.txt_Search);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(8, 48);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 24);
            this.panel1.TabIndex = 2;
            // 
            // label_Search
            // 
            this.label_Search.AutoSize = true;
            this.label_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.label_Search.Location = new System.Drawing.Point(24, 7);
            this.label_Search.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Search.Name = "label_Search";
            this.label_Search.Size = new System.Drawing.Size(41, 13);
            this.label_Search.TabIndex = 0;
            this.label_Search.Text = "Search";
            // 
            // txt_Search
            // 
            this.txt_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Search.Location = new System.Drawing.Point(23, 6);
            this.txt_Search.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(120, 15);
            this.txt_Search.TabIndex = 1;
            this.txt_Search.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_Search_MouseClick);
            this.txt_Search.MouseEnter += new System.EventHandler(this.txt_Search_MouseEnter);
            this.txt_Search.MouseLeave += new System.EventHandler(this.txt_Search_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Nucleus.Coop.Properties.Resources.lupa;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 17);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_AddGame
            // 
            this.btn_AddGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AddGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_AddGame.DefaultForeColor = System.Drawing.Color.DimGray;
            this.btn_AddGame.DisabledForeColor = System.Drawing.Color.DimGray;
            this.btn_AddGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddGame.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddGame.ForeColor = System.Drawing.Color.DimGray;
            this.btn_AddGame.Image = global::Nucleus.Coop.Properties.Resources.plus;
            this.btn_AddGame.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_AddGame.Location = new System.Drawing.Point(8, 674);
            this.btn_AddGame.Name = "btn_AddGame";
            this.btn_AddGame.Size = new System.Drawing.Size(143, 31);
            this.btn_AddGame.TabIndex = 4;
            this.btn_AddGame.Text = "ADD A GAME...";
            this.btn_AddGame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AddGame.UseVisualStyleBackColor = false;
            this.btn_AddGame.Click += new System.EventHandler(this.btn_AddGame_Click);
            // 
            // borderPanel1
            // 
            this.borderPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.borderPanel1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.borderPanel1.BorderImage = global::Nucleus.Coop.Properties.Resources.border;
            this.borderPanel1.Controls.Add(this.btn_Presets);
            this.borderPanel1.Controls.Add(this.pic_Keyboard);
            this.borderPanel1.Controls.Add(this.label_title);
            this.borderPanel1.Controls.Add(this.playerCount1);
            this.borderPanel1.Controls.Add(this.monitorControl1);
            this.borderPanel1.Controls.Add(this.label_maxPlayas);
            this.borderPanel1.Controls.Add(this.label1);
            this.borderPanel1.Controls.Add(this.pic_gamePad);
            this.borderPanel1.Controls.Add(this.btn_Previous);
            this.borderPanel1.Controls.Add(this.btn_Next);
            this.borderPanel1.Controls.Add(this.button_Play);
            this.borderPanel1.Controls.Add(this.label_GameName);
            this.borderPanel1.Controls.Add(this.game_Box);
            this.borderPanel1.Controls.Add(this.list_Games);
            this.borderPanel1.EdgeImage = global::Nucleus.Coop.Properties.Resources.edge;
            this.borderPanel1.ForeColor = System.Drawing.Color.White;
            this.borderPanel1.Location = new System.Drawing.Point(8, 79);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(995, 588);
            this.borderPanel1.TabIndex = 3;
            // 
            // btn_Presets
            // 
            this.btn_Presets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_Presets.DefaultForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btn_Presets.DisabledForeColor = System.Drawing.Color.Black;
            this.btn_Presets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Presets.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Presets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btn_Presets.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Presets.Location = new System.Drawing.Point(270, 96);
            this.btn_Presets.Name = "btn_Presets";
            this.btn_Presets.Size = new System.Drawing.Size(75, 27);
            this.btn_Presets.TabIndex = 23;
            this.btn_Presets.Text = "PRESETS";
            this.btn_Presets.UseVisualStyleBackColor = false;
            this.btn_Presets.Visible = false;
            this.btn_Presets.Click += new System.EventHandler(this.btn_Presets_Click);
            // 
            // pic_Keyboard
            // 
            this.pic_Keyboard.BackColor = System.Drawing.Color.Transparent;
            this.pic_Keyboard.Image = global::Nucleus.Coop.Properties.Resources.keyboard;
            this.pic_Keyboard.Location = new System.Drawing.Point(351, 58);
            this.pic_Keyboard.Name = "pic_Keyboard";
            this.pic_Keyboard.Size = new System.Drawing.Size(35, 31);
            this.pic_Keyboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Keyboard.TabIndex = 22;
            this.pic_Keyboard.TabStop = false;
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.label_title.Location = new System.Drawing.Point(272, 102);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(52, 21);
            this.label_title.TabIndex = 21;
            this.label_title.Text = "label3";
            // 
            // playerCount1
            // 
            this.playerCount1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.playerCount1.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.playerCount1.ForeColor = System.Drawing.Color.White;
            this.playerCount1.Location = new System.Drawing.Point(197, 134);
            this.playerCount1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playerCount1.Name = "playerCount1";
            this.playerCount1.Size = new System.Drawing.Size(795, 451);
            this.playerCount1.TabIndex = 20;
            // 
            // monitorControl1
            // 
            this.monitorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monitorControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.monitorControl1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.monitorControl1.Location = new System.Drawing.Point(197, 134);
            this.monitorControl1.Name = "monitorControl1";
            this.monitorControl1.Size = new System.Drawing.Size(795, 451);
            this.monitorControl1.TabIndex = 15;
            // 
            // label_maxPlayas
            // 
            this.label_maxPlayas.AutoSize = true;
            this.label_maxPlayas.BackColor = System.Drawing.Color.Transparent;
            this.label_maxPlayas.Location = new System.Drawing.Point(433, 68);
            this.label_maxPlayas.Name = "label_maxPlayas";
            this.label_maxPlayas.Size = new System.Drawing.Size(13, 13);
            this.label_maxPlayas.TabIndex = 19;
            this.label_maxPlayas.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(424, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "x";
            // 
            // pic_gamePad
            // 
            this.pic_gamePad.BackColor = System.Drawing.Color.Transparent;
            this.pic_gamePad.Image = global::Nucleus.Coop.Properties.Resources.gamepad;
            this.pic_gamePad.Location = new System.Drawing.Point(392, 58);
            this.pic_gamePad.Name = "pic_gamePad";
            this.pic_gamePad.Size = new System.Drawing.Size(35, 31);
            this.pic_gamePad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_gamePad.TabIndex = 17;
            this.pic_gamePad.TabStop = false;
            // 
            // btn_Previous
            // 
            this.btn_Previous.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Previous.DefaultForeColor = System.Drawing.Color.White;
            this.btn_Previous.DisabledForeColor = System.Drawing.Color.DimGray;
            this.btn_Previous.FlatAppearance.BorderSize = 0;
            this.btn_Previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Previous.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Previous.ForeColor = System.Drawing.Color.White;
            this.btn_Previous.Image = global::Nucleus.Coop.Properties.Resources.left_arrow;
            this.btn_Previous.Location = new System.Drawing.Point(196, 96);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(32, 32);
            this.btn_Previous.TabIndex = 12;
            this.btn_Previous.UseVisualStyleBackColor = false;
            this.btn_Previous.Visible = false;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Next.DefaultForeColor = System.Drawing.Color.White;
            this.btn_Next.DisabledForeColor = System.Drawing.Color.DimGray;
            this.btn_Next.Enabled = false;
            this.btn_Next.FlatAppearance.BorderSize = 0;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Next.ForeColor = System.Drawing.Color.White;
            this.btn_Next.Image = global::Nucleus.Coop.Properties.Resources.right_arrow;
            this.btn_Next.Location = new System.Drawing.Point(234, 96);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(32, 32);
            this.btn_Next.TabIndex = 6;
            this.btn_Next.UseVisualStyleBackColor = false;
            this.btn_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // button_Play
            // 
            this.button_Play.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button_Play.DefaultForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button_Play.DisabledForeColor = System.Drawing.Color.Black;
            this.button_Play.Enabled = false;
            this.button_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Play.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Play.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button_Play.Image = global::Nucleus.Coop.Properties.Resources.play;
            this.button_Play.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Play.Location = new System.Drawing.Point(270, 58);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(75, 31);
            this.button_Play.TabIndex = 3;
            this.button_Play.Text = "    PLAY";
            this.button_Play.UseVisualStyleBackColor = false;
            this.button_Play.Click += new System.EventHandler(this.button_Play_Click);
            // 
            // label_GameName
            // 
            this.label_GameName.AutoSize = true;
            this.label_GameName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.label_GameName.Font = new System.Drawing.Font("Segoe WP Semibold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_GameName.ForeColor = System.Drawing.Color.White;
            this.label_GameName.Location = new System.Drawing.Point(265, 25);
            this.label_GameName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_GameName.Name = "label_GameName";
            this.label_GameName.Size = new System.Drawing.Size(135, 30);
            this.label_GameName.TabIndex = 2;
            this.label_GameName.Text = "Game Name";
            // 
            // game_Box
            // 
            this.game_Box.Image = global::Nucleus.Coop.Properties.Resources.not_found;
            this.game_Box.Location = new System.Drawing.Point(197, 25);
            this.game_Box.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.game_Box.Name = "game_Box";
            this.game_Box.Size = new System.Drawing.Size(64, 64);
            this.game_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.game_Box.TabIndex = 1;
            this.game_Box.TabStop = false;
            // 
            // list_Games
            // 
            this.list_Games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.list_Games.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_Games.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_Games.ForeColor = System.Drawing.Color.White;
            this.list_Games.FormattingEnabled = true;
            this.list_Games.IntegralHeight = false;
            this.list_Games.ItemHeight = 21;
            this.list_Games.Location = new System.Drawing.Point(11, 4);
            this.list_Games.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.list_Games.Name = "list_Games";
            this.list_Games.Size = new System.Drawing.Size(180, 580);
            this.list_Games.TabIndex = 0;
            this.list_Games.SelectedIndexChanged += new System.EventHandler(this.list_Games_SelectedIndexChanged);
            // 
            // playerOptions1
            // 
            this.playerOptions1.AutoScroll = true;
            this.playerOptions1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.playerOptions1.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F);
            this.playerOptions1.ForeColor = System.Drawing.Color.White;
            this.playerOptions1.Location = new System.Drawing.Point(206, 214);
            this.playerOptions1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playerOptions1.Name = "playerOptions1";
            this.playerOptions1.Size = new System.Drawing.Size(795, 451);
            this.playerOptions1.TabIndex = 16;
            // 
            // GamesViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1015, 715);
            this.Controls.Add(this.playerOptions1);
            this.Controls.Add(this.btn_AddGame);
            this.Controls.Add(this.borderPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_Library);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "GamesViewer";
            this.Text = "Split Play PC - Games";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GamesViewer_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.borderPanel1.ResumeLayout(false);
            this.borderPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Keyboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_gamePad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_Box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Library;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Nucleus.Gaming.Controls.BorderPanel borderPanel1;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Label label_Search;
        private System.Windows.Forms.ListBox list_Games;
        private System.Windows.Forms.PictureBox game_Box;
        private System.Windows.Forms.Label label_GameName;
        private Nucleus.Gaming.Controls.NButton button_Play;
        private Nucleus.Gaming.Controls.NButton btn_AddGame;
        private Nucleus.Gaming.Controls.NButton btn_Next;
        private Nucleus.Gaming.Controls.NButton btn_Previous;
        private Controls.MonitorControl monitorControl1;
        private Controls.PlayerOptions playerOptions1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pic_gamePad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_maxPlayas;
        private Controls.PlayerCount playerCount1;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pic_Keyboard;
        private System.Windows.Forms.ToolTip toolTip1;
        private Nucleus.Gaming.Controls.NButton btn_Presets;

    }
}