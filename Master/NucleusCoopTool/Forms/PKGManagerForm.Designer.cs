namespace Nucleus.Coop.Forms
{
    partial class PKGManagerForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel_Browse = new System.Windows.Forms.Panel();
            this.panel_Installed = new System.Windows.Forms.Panel();
            this.panel_Updates = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 577);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.panel_Browse);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browse";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel_Installed);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Installed";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel_Updates);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(752, 543);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Updates";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel_Browse
            // 
            this.panel_Browse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Browse.Location = new System.Drawing.Point(3, 3);
            this.panel_Browse.Name = "panel_Browse";
            this.panel_Browse.Size = new System.Drawing.Size(746, 537);
            this.panel_Browse.TabIndex = 0;
            // 
            // panel_Installed
            // 
            this.panel_Installed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Installed.Location = new System.Drawing.Point(3, 3);
            this.panel_Installed.Name = "panel_Installed";
            this.panel_Installed.Size = new System.Drawing.Size(746, 537);
            this.panel_Installed.TabIndex = 0;
            // 
            // panel_Updates
            // 
            this.panel_Updates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Updates.Location = new System.Drawing.Point(0, 0);
            this.panel_Updates.Name = "panel_Updates";
            this.panel_Updates.Size = new System.Drawing.Size(752, 543);
            this.panel_Updates.TabIndex = 0;
            // 
            // StoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 601);
            this.Controls.Add(this.tabControl1);
            this.Name = "StoreForm";
            this.Text = "Store";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel_Browse;
        private System.Windows.Forms.Panel panel_Installed;
        private System.Windows.Forms.Panel panel_Updates;
    }
}