namespace Nucleus.Coop
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
            this.StepPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_GameTitle = new System.Windows.Forms.Label();
            this.pic_Game = new System.Windows.Forms.PictureBox();
            this.list_Games = new Nucleus.Gaming.ControlListBox();
            this.label_StepTitle = new System.Windows.Forms.Label();
            this.btn_Play = new System.Windows.Forms.Button();
            this.arrow_Next = new Nucleus.Gaming.ImageButton();
            this.arrow_Back = new Nucleus.Gaming.ImageButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Game)).BeginInit();
            this.SuspendLayout();
            // 
            // StepPanel
            // 
            this.StepPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StepPanel.Location = new System.Drawing.Point(242, 101);
            this.StepPanel.Name = "StepPanel";
            this.StepPanel.Size = new System.Drawing.Size(800, 589);
            this.StepPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Controls.Add(this.label_GameTitle);
            this.panel1.Controls.Add(this.pic_Game);
            this.panel1.Location = new System.Drawing.Point(242, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 46);
            this.panel1.TabIndex = 1;
            // 
            // label_GameTitle
            // 
            this.label_GameTitle.AutoSize = true;
            this.label_GameTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label_GameTitle.Location = new System.Drawing.Point(49, 6);
            this.label_GameTitle.Name = "label_GameTitle";
            this.label_GameTitle.Size = new System.Drawing.Size(142, 32);
            this.label_GameTitle.TabIndex = 1;
            this.label_GameTitle.Text = "Borderlands";
            // 
            // pic_Game
            // 
            this.pic_Game.Location = new System.Drawing.Point(3, 3);
            this.pic_Game.Name = "pic_Game";
            this.pic_Game.Size = new System.Drawing.Size(40, 40);
            this.pic_Game.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Game.TabIndex = 0;
            this.pic_Game.TabStop = false;
            // 
            // list_Games
            // 
            this.list_Games.AutoScroll = true;
            this.list_Games.Location = new System.Drawing.Point(12, 12);
            this.list_Games.Name = "list_Games";
            this.list_Games.Offset = new System.Drawing.Size(0, 2);
            this.list_Games.Size = new System.Drawing.Size(224, 678);
            this.list_Games.TabIndex = 2;
            this.list_Games.SelectedChanged += new System.Action<object, System.Windows.Forms.Control>(this.list_Games_SelectedChanged);
            // 
            // label_StepTitle
            // 
            this.label_StepTitle.AutoSize = true;
            this.label_StepTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label_StepTitle.Location = new System.Drawing.Point(345, 65);
            this.label_StepTitle.Name = "label_StepTitle";
            this.label_StepTitle.Size = new System.Drawing.Size(61, 32);
            this.label_StepTitle.TabIndex = 3;
            this.label_StepTitle.Text = "Title";
            // 
            // btn_Play
            // 
            this.btn_Play.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Play.Location = new System.Drawing.Point(242, 64);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(97, 31);
            this.btn_Play.TabIndex = 4;
            this.btn_Play.Text = "P L A Y";
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // arrow_Next
            // 
            this.arrow_Next.Image = global::Nucleus.Coop.Properties.Resources.right_arrow;
            this.arrow_Next.ImageDisabled = global::Nucleus.Coop.Properties.Resources.right_arrow_disabled;
            this.arrow_Next.ImageHover = global::Nucleus.Coop.Properties.Resources.right_arrow_hover;
            this.arrow_Next.ImagePressed = global::Nucleus.Coop.Properties.Resources.right_arrow_press;
            this.arrow_Next.Location = new System.Drawing.Point(1007, 62);
            this.arrow_Next.Name = "arrow_Next";
            this.arrow_Next.Size = new System.Drawing.Size(35, 33);
            this.arrow_Next.TabIndex = 5;
            // 
            // arrow_Back
            // 
            this.arrow_Back.Image = global::Nucleus.Coop.Properties.Resources.left_arrow;
            this.arrow_Back.ImageDisabled = global::Nucleus.Coop.Properties.Resources.left_arrow_disabled;
            this.arrow_Back.ImageHover = global::Nucleus.Coop.Properties.Resources.left_arrow_hover;
            this.arrow_Back.ImagePressed = global::Nucleus.Coop.Properties.Resources.left_arrow_press;
            this.arrow_Back.Location = new System.Drawing.Point(966, 62);
            this.arrow_Back.Name = "arrow_Back";
            this.arrow_Back.Size = new System.Drawing.Size(35, 33);
            this.arrow_Back.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 702);
            this.Controls.Add(this.arrow_Back);
            this.Controls.Add(this.arrow_Next);
            this.Controls.Add(this.btn_Play);
            this.Controls.Add(this.label_StepTitle);
            this.Controls.Add(this.list_Games);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.StepPanel);
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainForm";
            this.Text = "Nucleus Coop";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Game)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel StepPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pic_Game;
        private System.Windows.Forms.Label label_GameTitle;
        private Gaming.ControlListBox list_Games;
        private System.Windows.Forms.Label label_StepTitle;
        private System.Windows.Forms.Button btn_Play;
        private Gaming.ImageButton arrow_Next;
        private Gaming.ImageButton arrow_Back;
    }
}