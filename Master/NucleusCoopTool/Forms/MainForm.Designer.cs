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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel_Steps = new System.Windows.Forms.Panel();
            this.list_Games = new Nucleus.Gaming.ControlListBox();
            this.lbl_StepTitle = new System.Windows.Forms.Label();
            this.btn_Play = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.btn_AutoSearch = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.gameNameControl = new Nucleus.Coop.Controls.GameNameControl();
            this.btn_Packages = new System.Windows.Forms.Button();
            this.btn_Install = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel_Steps
            // 
            this.panel_Steps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Steps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_Steps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel_Steps.Location = new System.Drawing.Point(280, 101);
            this.panel_Steps.Name = "panel_Steps";
            this.panel_Steps.Size = new System.Drawing.Size(762, 588);
            this.panel_Steps.TabIndex = 0;
            // 
            // list_Games
            // 
            this.list_Games.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_Games.AutoScroll = true;
            this.list_Games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.list_Games.Border = 1;
            this.list_Games.Location = new System.Drawing.Point(12, 12);
            this.list_Games.Name = "list_Games";
            this.list_Games.Offset = new System.Drawing.Size(0, 2);
            this.list_Games.Size = new System.Drawing.Size(262, 636);
            this.list_Games.TabIndex = 2;
            // 
            // lbl_StepTitle
            // 
            this.lbl_StepTitle.AutoSize = true;
            this.lbl_StepTitle.Location = new System.Drawing.Point(276, 70);
            this.lbl_StepTitle.Name = "lbl_StepTitle";
            this.lbl_StepTitle.Size = new System.Drawing.Size(127, 21);
            this.lbl_StepTitle.TabIndex = 3;
            this.lbl_StepTitle.Text = "Nothing selected";
            // 
            // btn_Play
            // 
            this.btn_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Play.Enabled = false;
            this.btn_Play.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Play.Location = new System.Drawing.Point(945, 63);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(97, 35);
            this.btn_Play.TabIndex = 4;
            this.btn_Play.Text = "P L A Y";
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Location = new System.Drawing.Point(12, 654);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(128, 35);
            this.btn_Search.TabIndex = 7;
            this.btn_Search.Text = "Search Game";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Previous.Enabled = false;
            this.btn_Previous.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Previous.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_Previous.Location = new System.Drawing.Point(868, 63);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(33, 35);
            this.btn_Previous.TabIndex = 9;
            this.btn_Previous.Text = "<";
            this.btn_Previous.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.arrow_Back_Click);
            // 
            // btn_AutoSearch
            // 
            this.btn_AutoSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AutoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AutoSearch.Location = new System.Drawing.Point(146, 654);
            this.btn_AutoSearch.Name = "btn_AutoSearch";
            this.btn_AutoSearch.Size = new System.Drawing.Size(128, 35);
            this.btn_AutoSearch.TabIndex = 10;
            this.btn_AutoSearch.Text = "Auto Search";
            this.btn_AutoSearch.UseVisualStyleBackColor = true;
            this.btn_AutoSearch.Click += new System.EventHandler(this.btnAutoSearch_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Next.Enabled = false;
            this.btn_Next.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_Next.Location = new System.Drawing.Point(907, 63);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(33, 35);
            this.btn_Next.TabIndex = 11;
            this.btn_Next.Text = ">";
            this.btn_Next.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lbl_Version
            // 
            this.lbl_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.Location = new System.Drawing.Point(968, 666);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(71, 21);
            this.lbl_Version.TabIndex = 12;
            this.lbl_Version.Text = "ALPHA 8";
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
            // btn_Packages
            // 
            this.btn_Packages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Packages.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Packages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Packages.Location = new System.Drawing.Point(868, 23);
            this.btn_Packages.Name = "btn_Packages";
            this.btn_Packages.Size = new System.Drawing.Size(174, 35);
            this.btn_Packages.TabIndex = 14;
            this.btn_Packages.Text = "Packages";
            this.btn_Packages.UseVisualStyleBackColor = true;
            this.btn_Packages.Visible = false;
            this.btn_Packages.Click += new System.EventHandler(this.btn_Packages_Click);
            // 
            // btn_Install
            // 
            this.btn_Install.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Install.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Install.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Install.Location = new System.Drawing.Point(868, 23);
            this.btn_Install.Name = "btn_Install";
            this.btn_Install.Size = new System.Drawing.Size(174, 35);
            this.btn_Install.TabIndex = 15;
            this.btn_Install.Text = "Install Package";
            this.btn_Install.UseVisualStyleBackColor = true;
            this.btn_Install.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1054, 701);
            this.Controls.Add(this.btn_Install);
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.btn_Packages);
            this.Controls.Add(this.gameNameControl);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_AutoSearch);
            this.Controls.Add(this.btn_Previous);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_Play);
            this.Controls.Add(this.lbl_StepTitle);
            this.Controls.Add(this.list_Games);
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
        private Gaming.ControlListBox list_Games;
        private System.Windows.Forms.Label lbl_StepTitle;
        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button btn_AutoSearch;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Label lbl_Version;
        private Controls.GameNameControl gameNameControl;
        private System.Windows.Forms.Button btn_Packages;
        private System.Windows.Forms.Button btn_Install;
    }
}