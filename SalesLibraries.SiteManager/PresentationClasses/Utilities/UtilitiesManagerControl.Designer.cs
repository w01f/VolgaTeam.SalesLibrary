namespace SalesLibraries.SiteManager.PresentationClasses.Utilities
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnUtilityOperations = new System.Windows.Forms.Panel();
			this.buttonXUpdateContent = new DevComponents.DotNetBar.ButtonX();
			this.buttonXUpdateShorcuts = new DevComponents.DotNetBar.ButtonX();
			this.buttonXUpdateQuizzes = new DevComponents.DotNetBar.ButtonX();
			this.memoEditResult = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.pnUtilityOperations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditResult.Properties)).BeginInit();
			this.SuspendLayout();
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
			this.pnUtilityOperations.Controls.Add(this.buttonXUpdateContent);
			this.pnUtilityOperations.Controls.Add(this.buttonXUpdateQuizzes);
			this.pnUtilityOperations.Controls.Add(this.buttonXUpdateShorcuts);
			this.pnUtilityOperations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnUtilityOperations.Location = new System.Drawing.Point(0, 0);
			this.pnUtilityOperations.Name = "pnUtilityOperations";
			this.pnUtilityOperations.Size = new System.Drawing.Size(230, 483);
			this.pnUtilityOperations.TabIndex = 21;
			// 
			// buttonXUpdateContent
			// 
			this.buttonXUpdateContent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUpdateContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUpdateContent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUpdateContent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUpdateContent.Location = new System.Drawing.Point(12, 13);
			this.buttonXUpdateContent.Name = "buttonXUpdateContent";
			this.buttonXUpdateContent.Size = new System.Drawing.Size(205, 41);
			this.buttonXUpdateContent.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUpdateContent.TabIndex = 5;
			this.buttonXUpdateContent.Text = "Update Data";
			this.buttonXUpdateContent.Click += new System.EventHandler(this.simpleButtonUpdateContent_Click);
			// 
			// buttonXUpdateShorcuts
			// 
			this.buttonXUpdateShorcuts.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUpdateShorcuts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUpdateShorcuts.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUpdateShorcuts.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUpdateShorcuts.Location = new System.Drawing.Point(12, 86);
			this.buttonXUpdateShorcuts.Name = "buttonXUpdateShorcuts";
			this.buttonXUpdateShorcuts.Size = new System.Drawing.Size(205, 41);
			this.buttonXUpdateShorcuts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUpdateShorcuts.TabIndex = 6;
			this.buttonXUpdateShorcuts.Text = "Update Shortcuts";
			this.buttonXUpdateShorcuts.Click += new System.EventHandler(this.simpleButtonUpdateShorcuts_Click);
			// 
			// buttonXUpdateQuizzes
			// 
			this.buttonXUpdateQuizzes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXUpdateQuizzes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXUpdateQuizzes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXUpdateQuizzes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXUpdateQuizzes.Location = new System.Drawing.Point(12, 159);
			this.buttonXUpdateQuizzes.Name = "buttonXUpdateQuizzes";
			this.buttonXUpdateQuizzes.Size = new System.Drawing.Size(205, 41);
			this.buttonXUpdateQuizzes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXUpdateQuizzes.TabIndex = 9;
			this.buttonXUpdateQuizzes.Text = "Update Quizzes";
			this.buttonXUpdateQuizzes.Click += new System.EventHandler(this.simpleButtonUpdateQuizzes_Click);
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
			this.memoEditResult.Size = new System.Drawing.Size(669, 483);
			this.memoEditResult.TabIndex = 0;
			// 
			// UtilitiesManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
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

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private System.Windows.Forms.Panel pnUtilityOperations;
		private DevExpress.XtraEditors.MemoEdit memoEditResult;
		private DevComponents.DotNetBar.ButtonX buttonXUpdateContent;
		private DevComponents.DotNetBar.ButtonX buttonXUpdateShorcuts;
		private DevComponents.DotNetBar.ButtonX buttonXUpdateQuizzes;
    }
}
