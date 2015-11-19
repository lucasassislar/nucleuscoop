namespace SplitTool
{
    partial class FindGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindGameForm));
            this.btn_Manually = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.list_Games = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // btn_Manually
            // 
            this.btn_Manually.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Manually.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Manually.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Manually.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.btn_Manually.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Manually.Location = new System.Drawing.Point(210, 239);
            this.btn_Manually.Name = "btn_Manually";
            this.btn_Manually.Size = new System.Drawing.Size(147, 31);
            this.btn_Manually.TabIndex = 1;
            this.btn_Manually.Text = "Browse...";
            this.btn_Manually.UseVisualStyleBackColor = false;
            this.btn_Manually.Click += new System.EventHandler(this.btn_Manually_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.btn_Add.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Add.Location = new System.Drawing.Point(363, 240);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(147, 31);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "Add Selected";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // list_Games
            // 
            this.list_Games.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Games.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.list_Games.CheckBoxes = true;
            this.list_Games.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.list_Games.ForeColor = System.Drawing.Color.DimGray;
            this.list_Games.LabelWrap = false;
            this.list_Games.Location = new System.Drawing.Point(12, 12);
            this.list_Games.Name = "list_Games";
            this.list_Games.Size = new System.Drawing.Size(501, 222);
            this.list_Games.SmallImageList = this.imageList1;
            this.list_Games.TabIndex = 3;
            this.list_Games.UseCompatibleStateImageBehavior = false;
            this.list_Games.View = System.Windows.Forms.View.List;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FindGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(522, 282);
            this.Controls.Add(this.list_Games);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Manually);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindGameForm";
            this.Text = "Find Game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Manually;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.ListView list_Games;
        private System.Windows.Forms.ImageList imageList1;
    }
}