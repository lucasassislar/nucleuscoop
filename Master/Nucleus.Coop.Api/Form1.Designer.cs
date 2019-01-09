namespace Nucleus.Coop.Api
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRegSend = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxRegUsername = new System.Windows.Forms.TextBox();
            this.groupBoxReg = new System.Windows.Forms.GroupBox();
            this.textBoxRegEmail = new System.Windows.Forms.TextBox();
            this.textBoxRegPassword = new System.Windows.Forms.TextBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.textBoxLogEmail = new System.Windows.Forms.TextBox();
            this.textBoxLogPassword = new System.Windows.Forms.TextBox();
            this.buttonLogSend = new System.Windows.Forms.Button();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.textBoxSearchGameName = new System.Windows.Forms.TextBox();
            this.buttonSearchSend = new System.Windows.Forms.Button();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.buttonSetToken = new System.Windows.Forms.Button();
            this.groupBoxImport = new System.Windows.Forms.GroupBox();
            this.textBoxImportId = new System.Windows.Forms.TextBox();
            this.buttonImportSend = new System.Windows.Forms.Button();
            this.groupBoxListGames = new System.Windows.Forms.GroupBox();
            this.dataGridViewListGames = new System.Windows.Forms.DataGridView();
            this.buttonListGameSend = new System.Windows.Forms.Button();
            this.groupBoxSpecificGame = new System.Windows.Forms.GroupBox();
            this.textBoxSpecificGameId = new System.Windows.Forms.TextBox();
            this.dataGridViewListHandlers = new System.Windows.Forms.DataGridView();
            this.buttonSpecificGameSend = new System.Windows.Forms.Button();
            this.groupBoxAddHandler = new System.Windows.Forms.GroupBox();
            this.textBoxAddHandlerDetails = new System.Windows.Forms.TextBox();
            this.textBoxAddHandlerName = new System.Windows.Forms.TextBox();
            this.textBoxAddHandlerGameId = new System.Windows.Forms.TextBox();
            this.buttonAddHandlerSend = new System.Windows.Forms.Button();
            this.groupBoxAddPackage = new System.Windows.Forms.GroupBox();
            this.textBoxAddPackageFileFullPath = new System.Windows.Forms.TextBox();
            this.textBoxAddPackageInfos = new System.Windows.Forms.TextBox();
            this.textBoxAddPackageHandlerId = new System.Windows.Forms.TextBox();
            this.buttonAddPackageSend = new System.Windows.Forms.Button();
            this.openFileDialogAddPackageFileFullPath = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxGetPackage = new System.Windows.Forms.GroupBox();
            this.textBoxGetPackageHandlerId = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxGetPackageSpecificVersion = new System.Windows.Forms.TextBox();
            this.buttonGetPackageSend = new System.Windows.Forms.Button();
            this.groupBoxReg.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.groupBoxImport.SuspendLayout();
            this.groupBoxListGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListGames)).BeginInit();
            this.groupBoxSpecificGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListHandlers)).BeginInit();
            this.groupBoxAddHandler.SuspendLayout();
            this.groupBoxAddPackage.SuspendLayout();
            this.groupBoxGetPackage.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRegSend
            // 
            this.buttonRegSend.Location = new System.Drawing.Point(24, 97);
            this.buttonRegSend.Name = "buttonRegSend";
            this.buttonRegSend.Size = new System.Drawing.Size(75, 23);
            this.buttonRegSend.TabIndex = 0;
            this.buttonRegSend.Text = "Register";
            this.buttonRegSend.UseVisualStyleBackColor = true;
            this.buttonRegSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 149);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(250, 297);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Raw JSON output";
            // 
            // textBoxRegUsername
            // 
            this.textBoxRegUsername.Location = new System.Drawing.Point(6, 19);
            this.textBoxRegUsername.Name = "textBoxRegUsername";
            this.textBoxRegUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegUsername.TabIndex = 2;
            this.textBoxRegUsername.Text = "username";
            // 
            // groupBoxReg
            // 
            this.groupBoxReg.Controls.Add(this.textBoxRegEmail);
            this.groupBoxReg.Controls.Add(this.textBoxRegPassword);
            this.groupBoxReg.Controls.Add(this.textBoxRegUsername);
            this.groupBoxReg.Controls.Add(this.buttonRegSend);
            this.groupBoxReg.Location = new System.Drawing.Point(12, 12);
            this.groupBoxReg.Name = "groupBoxReg";
            this.groupBoxReg.Size = new System.Drawing.Size(122, 131);
            this.groupBoxReg.TabIndex = 3;
            this.groupBoxReg.TabStop = false;
            this.groupBoxReg.Text = "Registering";
            // 
            // textBoxRegEmail
            // 
            this.textBoxRegEmail.Location = new System.Drawing.Point(6, 71);
            this.textBoxRegEmail.Name = "textBoxRegEmail";
            this.textBoxRegEmail.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegEmail.TabIndex = 4;
            this.textBoxRegEmail.Text = "email";
            // 
            // textBoxRegPassword
            // 
            this.textBoxRegPassword.Location = new System.Drawing.Point(6, 45);
            this.textBoxRegPassword.Name = "textBoxRegPassword";
            this.textBoxRegPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegPassword.TabIndex = 3;
            this.textBoxRegPassword.Text = "password";
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBoxLogEmail);
            this.groupBoxLog.Controls.Add(this.textBoxLogPassword);
            this.groupBoxLog.Controls.Add(this.buttonLogSend);
            this.groupBoxLog.Location = new System.Drawing.Point(140, 12);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(122, 131);
            this.groupBoxLog.TabIndex = 5;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Login";
            // 
            // textBoxLogEmail
            // 
            this.textBoxLogEmail.Location = new System.Drawing.Point(6, 55);
            this.textBoxLogEmail.Name = "textBoxLogEmail";
            this.textBoxLogEmail.Size = new System.Drawing.Size(100, 20);
            this.textBoxLogEmail.TabIndex = 4;
            this.textBoxLogEmail.Text = "email";
            // 
            // textBoxLogPassword
            // 
            this.textBoxLogPassword.Location = new System.Drawing.Point(6, 29);
            this.textBoxLogPassword.Name = "textBoxLogPassword";
            this.textBoxLogPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxLogPassword.TabIndex = 3;
            this.textBoxLogPassword.Text = "password";
            // 
            // buttonLogSend
            // 
            this.buttonLogSend.Location = new System.Drawing.Point(24, 97);
            this.buttonLogSend.Name = "buttonLogSend";
            this.buttonLogSend.Size = new System.Drawing.Size(75, 23);
            this.buttonLogSend.TabIndex = 0;
            this.buttonLogSend.Text = "Login";
            this.buttonLogSend.UseVisualStyleBackColor = true;
            this.buttonLogSend.Click += new System.EventHandler(this.buttonLogSend_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.textBoxSearchGameName);
            this.groupBoxSearch.Controls.Add(this.buttonSearchSend);
            this.groupBoxSearch.Location = new System.Drawing.Point(278, 57);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(195, 62);
            this.groupBoxSearch.TabIndex = 6;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "1. Search IGDB game (Req. Auth)";
            // 
            // textBoxSearchGameName
            // 
            this.textBoxSearchGameName.Location = new System.Drawing.Point(6, 26);
            this.textBoxSearchGameName.Name = "textBoxSearchGameName";
            this.textBoxSearchGameName.Size = new System.Drawing.Size(100, 20);
            this.textBoxSearchGameName.TabIndex = 4;
            this.textBoxSearchGameName.Text = "zelda";
            // 
            // buttonSearchSend
            // 
            this.buttonSearchSend.Location = new System.Drawing.Point(112, 26);
            this.buttonSearchSend.Name = "buttonSearchSend";
            this.buttonSearchSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSearchSend.TabIndex = 0;
            this.buttonSearchSend.Text = "Search";
            this.buttonSearchSend.UseVisualStyleBackColor = true;
            this.buttonSearchSend.Click += new System.EventHandler(this.buttonSearchSend_Click);
            // 
            // textBoxToken
            // 
            this.textBoxToken.Location = new System.Drawing.Point(278, 12);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.Size = new System.Drawing.Size(457, 20);
            this.textBoxToken.TabIndex = 3;
            this.textBoxToken.Text = "token";
            // 
            // buttonSetToken
            // 
            this.buttonSetToken.Location = new System.Drawing.Point(741, 12);
            this.buttonSetToken.Name = "buttonSetToken";
            this.buttonSetToken.Size = new System.Drawing.Size(75, 23);
            this.buttonSetToken.TabIndex = 7;
            this.buttonSetToken.Text = "Set Token";
            this.buttonSetToken.UseVisualStyleBackColor = true;
            this.buttonSetToken.Click += new System.EventHandler(this.buttonSetToken_Click);
            // 
            // groupBoxImport
            // 
            this.groupBoxImport.Controls.Add(this.textBoxImportId);
            this.groupBoxImport.Controls.Add(this.buttonImportSend);
            this.groupBoxImport.Location = new System.Drawing.Point(479, 57);
            this.groupBoxImport.Name = "groupBoxImport";
            this.groupBoxImport.Size = new System.Drawing.Size(238, 62);
            this.groupBoxImport.TabIndex = 8;
            this.groupBoxImport.TabStop = false;
            this.groupBoxImport.Text = "2. Import game to internal DB (Req. Auth)";
            // 
            // textBoxImportId
            // 
            this.textBoxImportId.Location = new System.Drawing.Point(6, 26);
            this.textBoxImportId.Name = "textBoxImportId";
            this.textBoxImportId.Size = new System.Drawing.Size(100, 20);
            this.textBoxImportId.TabIndex = 4;
            this.textBoxImportId.Text = "igdb_id";
            // 
            // buttonImportSend
            // 
            this.buttonImportSend.Location = new System.Drawing.Point(112, 26);
            this.buttonImportSend.Name = "buttonImportSend";
            this.buttonImportSend.Size = new System.Drawing.Size(75, 23);
            this.buttonImportSend.TabIndex = 0;
            this.buttonImportSend.Text = "Import";
            this.buttonImportSend.UseVisualStyleBackColor = true;
            this.buttonImportSend.Click += new System.EventHandler(this.buttonImportSend_Click);
            // 
            // groupBoxListGames
            // 
            this.groupBoxListGames.Controls.Add(this.dataGridViewListGames);
            this.groupBoxListGames.Controls.Add(this.buttonListGameSend);
            this.groupBoxListGames.Location = new System.Drawing.Point(278, 125);
            this.groupBoxListGames.Name = "groupBoxListGames";
            this.groupBoxListGames.Size = new System.Drawing.Size(439, 141);
            this.groupBoxListGames.TabIndex = 7;
            this.groupBoxListGames.TabStop = false;
            this.groupBoxListGames.Text = "List internal games";
            // 
            // dataGridViewListGames
            // 
            this.dataGridViewListGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewListGames.Location = new System.Drawing.Point(112, 19);
            this.dataGridViewListGames.Name = "dataGridViewListGames";
            this.dataGridViewListGames.Size = new System.Drawing.Size(321, 116);
            this.dataGridViewListGames.TabIndex = 1;
            // 
            // buttonListGameSend
            // 
            this.buttonListGameSend.Location = new System.Drawing.Point(6, 19);
            this.buttonListGameSend.Name = "buttonListGameSend";
            this.buttonListGameSend.Size = new System.Drawing.Size(75, 23);
            this.buttonListGameSend.TabIndex = 0;
            this.buttonListGameSend.Text = "Retrieve";
            this.buttonListGameSend.UseVisualStyleBackColor = true;
            this.buttonListGameSend.Click += new System.EventHandler(this.buttonListGameSend_Click);
            // 
            // groupBoxSpecificGame
            // 
            this.groupBoxSpecificGame.Controls.Add(this.textBoxSpecificGameId);
            this.groupBoxSpecificGame.Controls.Add(this.dataGridViewListHandlers);
            this.groupBoxSpecificGame.Controls.Add(this.buttonSpecificGameSend);
            this.groupBoxSpecificGame.Location = new System.Drawing.Point(278, 272);
            this.groupBoxSpecificGame.Name = "groupBoxSpecificGame";
            this.groupBoxSpecificGame.Size = new System.Drawing.Size(439, 141);
            this.groupBoxSpecificGame.TabIndex = 9;
            this.groupBoxSpecificGame.TabStop = false;
            this.groupBoxSpecificGame.Text = "Retrieve specific games (and display handlers)";
            // 
            // textBoxSpecificGameId
            // 
            this.textBoxSpecificGameId.Location = new System.Drawing.Point(6, 19);
            this.textBoxSpecificGameId.Name = "textBoxSpecificGameId";
            this.textBoxSpecificGameId.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpecificGameId.TabIndex = 5;
            this.textBoxSpecificGameId.Text = "internal_id";
            // 
            // dataGridViewListHandlers
            // 
            this.dataGridViewListHandlers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewListHandlers.Location = new System.Drawing.Point(112, 19);
            this.dataGridViewListHandlers.Name = "dataGridViewListHandlers";
            this.dataGridViewListHandlers.Size = new System.Drawing.Size(321, 116);
            this.dataGridViewListHandlers.TabIndex = 1;
            // 
            // buttonSpecificGameSend
            // 
            this.buttonSpecificGameSend.Location = new System.Drawing.Point(6, 45);
            this.buttonSpecificGameSend.Name = "buttonSpecificGameSend";
            this.buttonSpecificGameSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSpecificGameSend.TabIndex = 0;
            this.buttonSpecificGameSend.Text = "Retrieve";
            this.buttonSpecificGameSend.UseVisualStyleBackColor = true;
            this.buttonSpecificGameSend.Click += new System.EventHandler(this.buttonSpecificGameSend_Click);
            // 
            // groupBoxAddHandler
            // 
            this.groupBoxAddHandler.Controls.Add(this.textBoxAddHandlerDetails);
            this.groupBoxAddHandler.Controls.Add(this.textBoxAddHandlerName);
            this.groupBoxAddHandler.Controls.Add(this.textBoxAddHandlerGameId);
            this.groupBoxAddHandler.Controls.Add(this.buttonAddHandlerSend);
            this.groupBoxAddHandler.Location = new System.Drawing.Point(278, 419);
            this.groupBoxAddHandler.Name = "groupBoxAddHandler";
            this.groupBoxAddHandler.Size = new System.Drawing.Size(439, 59);
            this.groupBoxAddHandler.TabIndex = 10;
            this.groupBoxAddHandler.TabStop = false;
            this.groupBoxAddHandler.Text = "Create Handler (Req. Auth)";
            // 
            // textBoxAddHandlerDetails
            // 
            this.textBoxAddHandlerDetails.Location = new System.Drawing.Point(218, 19);
            this.textBoxAddHandlerDetails.Name = "textBoxAddHandlerDetails";
            this.textBoxAddHandlerDetails.Size = new System.Drawing.Size(100, 20);
            this.textBoxAddHandlerDetails.TabIndex = 7;
            this.textBoxAddHandlerDetails.Text = "handler_details";
            // 
            // textBoxAddHandlerName
            // 
            this.textBoxAddHandlerName.Location = new System.Drawing.Point(112, 19);
            this.textBoxAddHandlerName.Name = "textBoxAddHandlerName";
            this.textBoxAddHandlerName.Size = new System.Drawing.Size(100, 20);
            this.textBoxAddHandlerName.TabIndex = 6;
            this.textBoxAddHandlerName.Text = "handler_name";
            // 
            // textBoxAddHandlerGameId
            // 
            this.textBoxAddHandlerGameId.Location = new System.Drawing.Point(6, 19);
            this.textBoxAddHandlerGameId.Name = "textBoxAddHandlerGameId";
            this.textBoxAddHandlerGameId.Size = new System.Drawing.Size(100, 20);
            this.textBoxAddHandlerGameId.TabIndex = 5;
            this.textBoxAddHandlerGameId.Text = "game_id";
            // 
            // buttonAddHandlerSend
            // 
            this.buttonAddHandlerSend.Location = new System.Drawing.Point(324, 19);
            this.buttonAddHandlerSend.Name = "buttonAddHandlerSend";
            this.buttonAddHandlerSend.Size = new System.Drawing.Size(75, 23);
            this.buttonAddHandlerSend.TabIndex = 0;
            this.buttonAddHandlerSend.Text = "Create";
            this.buttonAddHandlerSend.UseVisualStyleBackColor = true;
            this.buttonAddHandlerSend.Click += new System.EventHandler(this.buttonAddHandlerSend_Click);
            // 
            // groupBoxAddPackage
            // 
            this.groupBoxAddPackage.Controls.Add(this.textBoxAddPackageFileFullPath);
            this.groupBoxAddPackage.Controls.Add(this.textBoxAddPackageInfos);
            this.groupBoxAddPackage.Controls.Add(this.textBoxAddPackageHandlerId);
            this.groupBoxAddPackage.Controls.Add(this.buttonAddPackageSend);
            this.groupBoxAddPackage.Location = new System.Drawing.Point(278, 484);
            this.groupBoxAddPackage.Name = "groupBoxAddPackage";
            this.groupBoxAddPackage.Size = new System.Drawing.Size(439, 95);
            this.groupBoxAddPackage.TabIndex = 11;
            this.groupBoxAddPackage.TabStop = false;
            this.groupBoxAddPackage.Text = "Add package to handler (Req. Auth + Handler owner)";
            // 
            // textBoxAddPackageFileFullPath
            // 
            this.textBoxAddPackageFileFullPath.Location = new System.Drawing.Point(112, 19);
            this.textBoxAddPackageFileFullPath.Name = "textBoxAddPackageFileFullPath";
            this.textBoxAddPackageFileFullPath.Size = new System.Drawing.Size(321, 20);
            this.textBoxAddPackageFileFullPath.TabIndex = 8;
            this.textBoxAddPackageFileFullPath.Text = "package_file";
            this.textBoxAddPackageFileFullPath.Click += new System.EventHandler(this.textBoxAddPackageFileFullPath_Click);
            // 
            // textBoxAddPackageInfos
            // 
            this.textBoxAddPackageInfos.Location = new System.Drawing.Point(6, 45);
            this.textBoxAddPackageInfos.Name = "textBoxAddPackageInfos";
            this.textBoxAddPackageInfos.Size = new System.Drawing.Size(427, 20);
            this.textBoxAddPackageInfos.TabIndex = 7;
            this.textBoxAddPackageInfos.Text = "package_infos";
            // 
            // textBoxAddPackageHandlerId
            // 
            this.textBoxAddPackageHandlerId.Location = new System.Drawing.Point(6, 19);
            this.textBoxAddPackageHandlerId.Name = "textBoxAddPackageHandlerId";
            this.textBoxAddPackageHandlerId.Size = new System.Drawing.Size(100, 20);
            this.textBoxAddPackageHandlerId.TabIndex = 5;
            this.textBoxAddPackageHandlerId.Text = "handler_id";
            // 
            // buttonAddPackageSend
            // 
            this.buttonAddPackageSend.Location = new System.Drawing.Point(358, 66);
            this.buttonAddPackageSend.Name = "buttonAddPackageSend";
            this.buttonAddPackageSend.Size = new System.Drawing.Size(75, 23);
            this.buttonAddPackageSend.TabIndex = 0;
            this.buttonAddPackageSend.Text = "Upload";
            this.buttonAddPackageSend.UseVisualStyleBackColor = true;
            this.buttonAddPackageSend.Click += new System.EventHandler(this.buttonAddPackageSend_Click);
            // 
            // openFileDialogAddPackageFileFullPath
            // 
            this.openFileDialogAddPackageFileFullPath.FileName = "Nucleus Handler Package";
            this.openFileDialogAddPackageFileFullPath.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogAddPackageFileFullPath_FileOk);
            // 
            // groupBoxGetPackage
            // 
            this.groupBoxGetPackage.Controls.Add(this.buttonGetPackageSend);
            this.groupBoxGetPackage.Controls.Add(this.textBoxGetPackageSpecificVersion);
            this.groupBoxGetPackage.Controls.Add(this.textBoxGetPackageHandlerId);
            this.groupBoxGetPackage.Controls.Add(this.button1);
            this.groupBoxGetPackage.Location = new System.Drawing.Point(12, 484);
            this.groupBoxGetPackage.Name = "groupBoxGetPackage";
            this.groupBoxGetPackage.Size = new System.Drawing.Size(250, 95);
            this.groupBoxGetPackage.TabIndex = 12;
            this.groupBoxGetPackage.TabStop = false;
            this.groupBoxGetPackage.Text = "Get package";
            // 
            // textBoxGetPackageHandlerId
            // 
            this.textBoxGetPackageHandlerId.Location = new System.Drawing.Point(6, 19);
            this.textBoxGetPackageHandlerId.Name = "textBoxGetPackageHandlerId";
            this.textBoxGetPackageHandlerId.Size = new System.Drawing.Size(100, 20);
            this.textBoxGetPackageHandlerId.TabIndex = 5;
            this.textBoxGetPackageHandlerId.Text = "handler_id";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(358, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxGetPackageSpecificVersion
            // 
            this.textBoxGetPackageSpecificVersion.Location = new System.Drawing.Point(6, 45);
            this.textBoxGetPackageSpecificVersion.Name = "textBoxGetPackageSpecificVersion";
            this.textBoxGetPackageSpecificVersion.Size = new System.Drawing.Size(161, 20);
            this.textBoxGetPackageSpecificVersion.TabIndex = 6;
            this.textBoxGetPackageSpecificVersion.Text = "specific_version (Latest if unset)";
            // 
            // buttonGetPackageSend
            // 
            this.buttonGetPackageSend.Location = new System.Drawing.Point(169, 66);
            this.buttonGetPackageSend.Name = "buttonGetPackageSend";
            this.buttonGetPackageSend.Size = new System.Drawing.Size(75, 23);
            this.buttonGetPackageSend.TabIndex = 7;
            this.buttonGetPackageSend.Text = "Download";
            this.buttonGetPackageSend.UseVisualStyleBackColor = true;
            this.buttonGetPackageSend.Click += new System.EventHandler(this.buttonGetPackageSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 660);
            this.Controls.Add(this.groupBoxGetPackage);
            this.Controls.Add(this.groupBoxAddPackage);
            this.Controls.Add(this.groupBoxAddHandler);
            this.Controls.Add(this.groupBoxSpecificGame);
            this.Controls.Add(this.groupBoxListGames);
            this.Controls.Add(this.groupBoxImport);
            this.Controls.Add(this.buttonSetToken);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBoxReg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBoxReg.ResumeLayout(false);
            this.groupBoxReg.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.groupBoxImport.ResumeLayout(false);
            this.groupBoxImport.PerformLayout();
            this.groupBoxListGames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListGames)).EndInit();
            this.groupBoxSpecificGame.ResumeLayout(false);
            this.groupBoxSpecificGame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListHandlers)).EndInit();
            this.groupBoxAddHandler.ResumeLayout(false);
            this.groupBoxAddHandler.PerformLayout();
            this.groupBoxAddPackage.ResumeLayout(false);
            this.groupBoxAddPackage.PerformLayout();
            this.groupBoxGetPackage.ResumeLayout(false);
            this.groupBoxGetPackage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRegSend;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBoxRegUsername;
        private System.Windows.Forms.GroupBox groupBoxReg;
        private System.Windows.Forms.TextBox textBoxRegEmail;
        private System.Windows.Forms.TextBox textBoxRegPassword;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.TextBox textBoxLogEmail;
        private System.Windows.Forms.TextBox textBoxLogPassword;
        private System.Windows.Forms.Button buttonLogSend;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox textBoxSearchGameName;
        private System.Windows.Forms.Button buttonSearchSend;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Button buttonSetToken;
        private System.Windows.Forms.GroupBox groupBoxImport;
        private System.Windows.Forms.TextBox textBoxImportId;
        private System.Windows.Forms.Button buttonImportSend;
        private System.Windows.Forms.GroupBox groupBoxListGames;
        private System.Windows.Forms.Button buttonListGameSend;
        private System.Windows.Forms.DataGridView dataGridViewListGames;
        private System.Windows.Forms.GroupBox groupBoxSpecificGame;
        private System.Windows.Forms.TextBox textBoxSpecificGameId;
        private System.Windows.Forms.DataGridView dataGridViewListHandlers;
        private System.Windows.Forms.Button buttonSpecificGameSend;
        private System.Windows.Forms.GroupBox groupBoxAddHandler;
        private System.Windows.Forms.TextBox textBoxAddHandlerDetails;
        private System.Windows.Forms.TextBox textBoxAddHandlerName;
        private System.Windows.Forms.TextBox textBoxAddHandlerGameId;
        private System.Windows.Forms.Button buttonAddHandlerSend;
        private System.Windows.Forms.GroupBox groupBoxAddPackage;
        private System.Windows.Forms.TextBox textBoxAddPackageFileFullPath;
        private System.Windows.Forms.TextBox textBoxAddPackageInfos;
        private System.Windows.Forms.TextBox textBoxAddPackageHandlerId;
        private System.Windows.Forms.Button buttonAddPackageSend;
        private System.Windows.Forms.OpenFileDialog openFileDialogAddPackageFileFullPath;
        private System.Windows.Forms.GroupBox groupBoxGetPackage;
        private System.Windows.Forms.Button buttonGetPackageSend;
        private System.Windows.Forms.TextBox textBoxGetPackageSpecificVersion;
        private System.Windows.Forms.TextBox textBoxGetPackageHandlerId;
        private System.Windows.Forms.Button button1;
    }
}

