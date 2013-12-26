namespace SalesDepot.SiteManager.PresentationClasses.Utilities
{
	sealed partial class UtilitiesManagerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnUtilityOperations = new System.Windows.Forms.Panel();
			this.simpleButtonProcessDeadLinks = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCleanExpiredEmails = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonUpdateShorcuts = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonUpdateContent = new DevExpress.XtraEditors.SimpleButton();
			this.memoEditResult = new DevExpress.XtraEditors.MemoEdit();
			this.simpleButtonUpdateQuizzes = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnUtilityOperations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditResult.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.pnUtilityOperations);
			this.splitContainerControl.Panel1.MinSize = 230;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.memoEditResult);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(911, 483);
			this.splitContainerControl.SplitterPosition = 230;
			this.splitContainerControl.TabIndex = 0;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// pnUtilityOperations
			// 
			this.pnUtilityOperations.Controls.Add(this.simpleButtonUpdateQuizzes);
			this.pnUtilityOperations.Controls.Add(this.simpleButtonProcessDeadLinks);
			this.pnUtilityOperations.Controls.Add(this.simpleButtonCleanExpiredEmails);
			this.pnUtilityOperations.Controls.Add(this.simpleButtonUpdateShorcuts);
			this.pnUtilityOperations.Controls.Add(this.simpleButtonUpdateContent);
			this.pnUtilityOperations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnUtilityOperations.Location = new System.Drawing.Point(0, 0);
			this.pnUtilityOperations.Name = "pnUtilityOperations";
			this.pnUtilityOperations.Size = new System.Drawing.Size(230, 483);
			this.pnUtilityOperations.TabIndex = 21;
			// 
			// simpleButtonProcessDeadLinks
			// 
			this.simpleButtonProcessDeadLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonProcessDeadLinks.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonProcessDeadLinks.Appearance.Options.UseFont = true;
			this.simpleButtonProcessDeadLinks.Location = new System.Drawing.Point(12, 238);
			this.simpleButtonProcessDeadLinks.Name = "simpleButtonProcessDeadLinks";
			this.simpleButtonProcessDeadLinks.Size = new System.Drawing.Size(205, 41);
			this.simpleButtonProcessDeadLinks.TabIndex = 3;
			this.simpleButtonProcessDeadLinks.Text = "Dead Links Notifiers";
			this.simpleButtonProcessDeadLinks.Click += new System.EventHandler(this.simpleButtonProcessDeadLinks_Click);
			// 
			// simpleButtonCleanExpiredEmails
			// 
			this.simpleButtonCleanExpiredEmails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonCleanExpiredEmails.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonCleanExpiredEmails.Appearance.Options.UseFont = true;
			this.simpleButtonCleanExpiredEmails.Location = new System.Drawing.Point(12, 163);
			this.simpleButtonCleanExpiredEmails.Name = "simpleButtonCleanExpiredEmails";
			this.simpleButtonCleanExpiredEmails.Size = new System.Drawing.Size(205, 41);
			this.simpleButtonCleanExpiredEmails.TabIndex = 2;
			this.simpleButtonCleanExpiredEmails.Text = "Clean Expired Emails";
			this.simpleButtonCleanExpiredEmails.Click += new System.EventHandler(this.simpleButtonCleanExpiredEmails_Click);
			// 
			// simpleButtonUpdateShorcuts
			// 
			this.simpleButtonUpdateShorcuts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonUpdateShorcuts.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonUpdateShorcuts.Appearance.Options.UseFont = true;
			this.simpleButtonUpdateShorcuts.Location = new System.Drawing.Point(12, 88);
			this.simpleButtonUpdateShorcuts.Name = "simpleButtonUpdateShorcuts";
			this.simpleButtonUpdateShorcuts.Size = new System.Drawing.Size(205, 41);
			this.simpleButtonUpdateShorcuts.TabIndex = 1;
			this.simpleButtonUpdateShorcuts.Text = "Update Shortcuts";
			this.simpleButtonUpdateShorcuts.Click += new System.EventHandler(this.simpleButtonUpdateShorcuts_Click);
			// 
			// simpleButtonUpdateContent
			// 
			this.simpleButtonUpdateContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonUpdateContent.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonUpdateContent.Appearance.Options.UseFont = true;
			this.simpleButtonUpdateContent.Location = new System.Drawing.Point(12, 13);
			this.simpleButtonUpdateContent.Name = "simpleButtonUpdateContent";
			this.simpleButtonUpdateContent.Size = new System.Drawing.Size(205, 41);
			this.simpleButtonUpdateContent.TabIndex = 0;
			this.simpleButtonUpdateContent.Text = "Update Data";
			this.simpleButtonUpdateContent.Click += new System.EventHandler(this.simpleButtonUpdateContent_Click);
			// 
			// memoEditResult
			// 
			this.memoEditResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.memoEditResult.Location = new System.Drawing.Point(0, 0);
			this.memoEditResult.Name = "memoEditResult";
			this.memoEditResult.Properties.AllowFocused = false;
			this.memoEditResult.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditResult.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.memoEditResult.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditResult.Properties.Appearance.Options.UseFont = true;
			this.memoEditResult.Properties.ReadOnly = true;
			this.memoEditResult.Size = new System.Drawing.Size(675, 483);
			this.memoEditResult.TabIndex = 0;
			// 
			// simpleButtonUpdateQuizzes
			// 
			this.simpleButtonUpdateQuizzes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonUpdateQuizzes.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonUpdateQuizzes.Appearance.Options.UseFont = true;
			this.simpleButtonUpdateQuizzes.Location = new System.Drawing.Point(12, 313);
			this.simpleButtonUpdateQuizzes.Name = "simpleButtonUpdateQuizzes";
			this.simpleButtonUpdateQuizzes.Size = new System.Drawing.Size(205, 41);
			this.simpleButtonUpdateQuizzes.TabIndex = 4;
			this.simpleButtonUpdateQuizzes.Text = "Update Quizzes";
			this.simpleButtonUpdateQuizzes.Click += new System.EventHandler(this.simpleButtonUpdateQuizzes_Click);
			// 
			// UtilitiesManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "UtilitiesManagerControl";
			this.Size = new System.Drawing.Size(911, 483);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.pnUtilityOperations.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditResult.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private System.Windows.Forms.Panel pnUtilityOperations;
		private DevExpress.XtraEditors.MemoEdit memoEditResult;
		private DevExpress.XtraEditors.SimpleButton simpleButtonUpdateContent;
		private DevExpress.XtraEditors.SimpleButton simpleButtonProcessDeadLinks;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCleanExpiredEmails;
		private DevExpress.XtraEditors.SimpleButton simpleButtonUpdateShorcuts;
		private DevExpress.XtraEditors.SimpleButton simpleButtonUpdateQuizzes;
    }
}
