namespace Borderlands2PCCoop
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_Games = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Play = new System.Windows.Forms.Button();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.check_WindowedFullscreen = new System.Windows.Forms.CheckBox();
            this.txt_File = new System.Windows.Forms.TextBox();
            this.check_Keyboard = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.delayUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.num_Players = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Players)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.combo_Games);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Game:";
            // 
            // combo_Games
            // 
            this.combo_Games.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combo_Games.FormattingEnabled = true;
            this.combo_Games.Location = new System.Drawing.Point(132, 19);
            this.combo_Games.Name = "combo_Games";
            this.combo_Games.Size = new System.Drawing.Size(422, 21);
            this.combo_Games.TabIndex = 6;
            this.combo_Games.SelectedIndexChanged += new System.EventHandler(this.gameBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btn_Play);
            this.groupBox2.Controls.Add(this.btn_Browse);
            this.groupBox2.Controls.Add(this.check_WindowedFullscreen);
            this.groupBox2.Controls.Add(this.txt_File);
            this.groupBox2.Controls.Add(this.check_Keyboard);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.delayUpDown);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.num_Players);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(563, 352);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2.";
            // 
            // btn_Play
            // 
            this.btn_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Play.Location = new System.Drawing.Point(482, 291);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(75, 23);
            this.btn_Play.TabIndex = 2;
            this.btn_Play.Text = "PLAY!";
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Browse.Location = new System.Drawing.Point(479, 16);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(75, 23);
            this.btn_Browse.TabIndex = 5;
            this.btn_Browse.Text = "Browse...";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // check_WindowedFullscreen
            // 
            this.check_WindowedFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.check_WindowedFullscreen.AutoSize = true;
            this.check_WindowedFullscreen.Location = new System.Drawing.Point(9, 329);
            this.check_WindowedFullscreen.Name = "check_WindowedFullscreen";
            this.check_WindowedFullscreen.Size = new System.Drawing.Size(253, 17);
            this.check_WindowedFullscreen.TabIndex = 12;
            this.check_WindowedFullscreen.Text = "Use Windowed Fullscreen on Fullscreen Players";
            this.check_WindowedFullscreen.UseVisualStyleBackColor = true;
            // 
            // txt_File
            // 
            this.txt_File.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_File.Location = new System.Drawing.Point(132, 19);
            this.txt_File.Name = "txt_File";
            this.txt_File.ReadOnly = true;
            this.txt_File.Size = new System.Drawing.Size(341, 20);
            this.txt_File.TabIndex = 4;
            // 
            // check_Keyboard
            // 
            this.check_Keyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.check_Keyboard.AutoSize = true;
            this.check_Keyboard.Location = new System.Drawing.Point(9, 306);
            this.check_Keyboard.Name = "check_Keyboard";
            this.check_Keyboard.Size = new System.Drawing.Size(125, 17);
            this.check_Keyboard.TabIndex = 11;
            this.check_Keyboard.Text = "First Player Keyboard";
            this.check_Keyboard.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game File:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Delay";
            // 
            // delayUpDown
            // 
            this.delayUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delayUpDown.Location = new System.Drawing.Point(439, 47);
            this.delayUpDown.Name = "delayUpDown";
            this.delayUpDown.Size = new System.Drawing.Size(115, 20);
            this.delayUpDown.TabIndex = 3;
            this.delayUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(9, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 214);
            this.panel1.TabIndex = 2;
            // 
            // num_Players
            // 
            this.num_Players.Location = new System.Drawing.Point(57, 45);
            this.num_Players.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.num_Players.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_Players.Name = "num_Players";
            this.num_Players.Size = new System.Drawing.Size(120, 20);
            this.num_Players.TabIndex = 1;
            this.num_Players.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_Players.ValueChanged += new System.EventHandler(this.num_Players_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Players:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(588, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 451);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PC Offline Coop Tool - v0.17";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Players)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown num_Players;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown delayUpDown;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_Games;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.TextBox txt_File;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox check_WindowedFullscreen;
        private System.Windows.Forms.CheckBox check_Keyboard;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    }
}

