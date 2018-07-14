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
            this.label1 = new System.Windows.Forms.Label();
            this.combo_Handlers = new System.Windows.Forms.ComboBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.gameNameControl = new Nucleus.Coop.Controls.GameNameControl();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_AutoSearchGame = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.btn_Play = new System.Windows.Forms.Button();
            this.lbl_StepTitle = new System.Windows.Forms.Label();
            this.panel_Steps = new System.Windows.Forms.Panel();
            this.list_Games = new Nucleus.Gaming.Platform.Windows.Controls.ControlListBox();
            this.btn_Handlers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 21);
            this.label1.TabIndex = 17;
            this.label1.Text = "Handler";
            // 
            // combo_Handlers
            // 
            this.combo_Handlers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Handlers.FormattingEnabled = true;
            this.combo_Handlers.ItemHeight = 21;
            this.combo_Handlers.Location = new System.Drawing.Point(391, 69);
            this.combo_Handlers.Name = "combo_Handlers";
            this.combo_Handlers.Size = new System.Drawing.Size(471, 29);
            this.combo_Handlers.TabIndex = 16;
            this.combo_Handlers.SelectedIndexChanged += new System.EventHandler(this.combo_Handlers_SelectedIndexChanged);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Browse.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Browse.Location = new System.Drawing.Point(9, 654);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(125, 35);
            this.btn_Browse.TabIndex = 15;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // gameNameControl
            // 
            this.gameNameControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.gameNameControl.GameInfo = null;
            this.gameNameControl.Location = new System.Drawing.Point(280, 12);
            this.gameNameControl.Name = "gameNameControl";
            this.gameNameControl.Size = new System.Drawing.Size(98, 46);
            this.gameNameControl.TabIndex = 13;
            // 
            // btn_Next
            // 
            this.btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Next.Enabled = false;
            this.btn_Next.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_Next.Location = new System.Drawing.Point(906, 69);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(33, 29);
            this.btn_Next.TabIndex = 11;
            this.btn_Next.Text = ">";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btn_AutoSearchGame
            // 
            this.btn_AutoSearchGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AutoSearchGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AutoSearchGame.Location = new System.Drawing.Point(140, 654);
            this.btn_AutoSearchGame.Name = "btn_AutoSearchGame";
            this.btn_AutoSearchGame.Size = new System.Drawing.Size(134, 35);
            this.btn_AutoSearchGame.TabIndex = 10;
            this.btn_AutoSearchGame.Text = "Auto Search";
            this.btn_AutoSearchGame.UseVisualStyleBackColor = true;
            this.btn_AutoSearchGame.Click += new System.EventHandler(this.btnAutoSearch_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Previous.Enabled = false;
            this.btn_Previous.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Previous.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_Previous.Location = new System.Drawing.Point(867, 69);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(33, 29);
            this.btn_Previous.TabIndex = 9;
            this.btn_Previous.Text = "<";
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.arrow_Back_Click);
            // 
            // btn_Play
            // 
            this.btn_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Play.Enabled = false;
            this.btn_Play.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Play.Location = new System.Drawing.Point(945, 69);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(97, 29);
            this.btn_Play.TabIndex = 4;
            this.btn_Play.Text = "P L A Y";
            this.btn_Play.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // lbl_StepTitle
            // 
            this.lbl_StepTitle.AutoSize = true;
            this.lbl_StepTitle.Location = new System.Drawing.Point(276, 113);
            this.lbl_StepTitle.Name = "lbl_StepTitle";
            this.lbl_StepTitle.Size = new System.Drawing.Size(127, 21);
            this.lbl_StepTitle.TabIndex = 3;
            this.lbl_StepTitle.Text = "Nothing selected";
            // 
            // panel_Steps
            // 
            this.panel_Steps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Steps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_Steps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel_Steps.Location = new System.Drawing.Point(280, 137);
            this.panel_Steps.Name = "panel_Steps";
            this.panel_Steps.Size = new System.Drawing.Size(762, 552);
            this.panel_Steps.TabIndex = 0;
            // 
            // list_Games
            // 
            this.list_Games.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_Games.AutoScroll = true;
            this.list_Games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.list_Games.Border = 1;
            this.list_Games.Location = new System.Drawing.Point(9, 9);
            this.list_Games.Margin = new System.Windows.Forms.Padding(0);
            this.list_Games.Name = "list_Games";
            this.list_Games.Offset = new System.Drawing.Size(0, 2);
            this.list_Games.Size = new System.Drawing.Size(263, 642);
            this.list_Games.TabIndex = 3;
            // 
            // btn_Handlers
            // 
            this.btn_Handlers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Handlers.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Handlers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Handlers.Location = new System.Drawing.Point(945, 12);
            this.btn_Handlers.Name = "btn_Handlers";
            this.btn_Handlers.Size = new System.Drawing.Size(97, 29);
            this.btn_Handlers.TabIndex = 20;
            this.btn_Handlers.Text = "Handlers";
            this.btn_Handlers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Handlers.UseVisualStyleBackColor = true;
            this.btn_Handlers.Click += new System.EventHandler(this.btn_Handlers_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1054, 701);
            this.Controls.Add(this.list_Games);
            this.Controls.Add(this.btn_AutoSearchGame);
            this.Controls.Add(this.btn_Handlers);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_Handlers);
            this.Controls.Add(this.gameNameControl);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_Previous);
            this.Controls.Add(this.btn_Play);
            this.Controls.Add(this.lbl_StepTitle);
            this.Controls.Add(this.panel_Steps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainForm";
            this.Text = "Nucleus Coop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Steps;
        private System.Windows.Forms.Label lbl_StepTitle;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button btn_AutoSearchGame;
        private System.Windows.Forms.Button btn_Next;
        private Coop.Controls.GameNameControl gameNameControl;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.ComboBox combo_Handlers;
        private System.Windows.Forms.Label label1;
        private Gaming.Platform.Windows.Controls.ControlListBox list_Games;
        private System.Windows.Forms.Button btn_Handlers;
    }
}