namespace SalesDepot.ToolForms.Settings
{
	partial class FormFileSettings
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
			this.buttonXResetToDefault = new DevComponents.DotNetBar.ButtonX();
			this.labelControlFormTitle = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.labelControlOpenFileTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonEditOpenFile = new DevExpress.XtraEditors.ButtonEdit();
			this.buttonEditSaveFile = new DevExpress.XtraEditors.ButtonEdit();
			this.labelControlSaveFileTitle = new DevExpress.XtraEditors.LabelControl();
			this.pbSaveFile = new System.Windows.Forms.PictureBox();
			this.pbOpenFile = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditOpenFile.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditSaveFile.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSaveFile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbOpenFile)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(322, 215);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(119, 37);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 10;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXEmail
			// 
			this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmail.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXEmail.Location = new System.Drawing.Point(185, 215);
			this.buttonXEmail.Name = "buttonXEmail";
			this.buttonXEmail.Size = new System.Drawing.Size(119, 37);
			this.buttonXEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmail.TabIndex = 9;
			this.buttonXEmail.Text = "OK";
			this.buttonXEmail.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXResetToDefault
			// 
			this.buttonXResetToDefault.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXResetToDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXResetToDefault.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXResetToDefault.Location = new System.Drawing.Point(12, 215);
			this.buttonXResetToDefault.Name = "buttonXResetToDefault";
			this.buttonXResetToDefault.Size = new System.Drawing.Size(119, 37);
			this.buttonXResetToDefault.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXResetToDefault.TabIndex = 11;
			this.buttonXResetToDefault.Text = "Reset to Default";
			this.buttonXResetToDefault.TextColor = System.Drawing.Color.Black;
			this.buttonXResetToDefault.Click += new System.EventHandler(this.buttonXResetToDefault_Click);
			// 
			// labelControlFormTitle
			// 
			this.labelControlFormTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlFormTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlFormTitle.Location = new System.Drawing.Point(12, 12);
			this.labelControlFormTitle.Name = "labelControlFormTitle";
			this.labelControlFormTitle.Size = new System.Drawing.Size(198, 16);
			this.labelControlFormTitle.StyleController = this.styleController;
			this.labelControlFormTitle.TabIndex = 12;
			this.labelControlFormTitle.Text = "File Save and File Open Locations";
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
			// labelControlOpenFileTitle
			// 
			this.labelControlOpenFileTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlOpenFileTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlOpenFileTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlOpenFileTitle.Location = new System.Drawing.Point(77, 47);
			this.labelControlOpenFileTitle.Name = "labelControlOpenFileTitle";
			this.labelControlOpenFileTitle.Size = new System.Drawing.Size(79, 18);
			this.labelControlOpenFileTitle.StyleController = this.styleController;
			this.labelControlOpenFileTitle.TabIndex = 14;
			this.labelControlOpenFileTitle.Text = "OPEN FILE";
			// 
			// buttonEditOpenFile
			// 
			this.buttonEditOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditOpenFile.Location = new System.Drawing.Point(77, 75);
			this.buttonEditOpenFile.Name = "buttonEditOpenFile";
			this.buttonEditOpenFile.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditOpenFile.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditOpenFile.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditOpenFile.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditOpenFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditOpenFile.Size = new System.Drawing.Size(364, 22);
			this.buttonEditOpenFile.StyleController = this.styleController;
			this.buttonEditOpenFile.TabIndex = 15;
			this.buttonEditOpenFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditOpenFile_ButtonClick);
			// 
			// buttonEditSaveFile
			// 
			this.buttonEditSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditSaveFile.Location = new System.Drawing.Point(77, 153);
			this.buttonEditSaveFile.Name = "buttonEditSaveFile";
			this.buttonEditSaveFile.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.buttonEditSaveFile.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.buttonEditSaveFile.Properties.Appearance.Options.UseBackColor = true;
			this.buttonEditSaveFile.Properties.Appearance.Options.UseForeColor = true;
			this.buttonEditSaveFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditSaveFile.Size = new System.Drawing.Size(364, 22);
			this.buttonEditSaveFile.StyleController = this.styleController;
			this.buttonEditSaveFile.TabIndex = 18;
			this.buttonEditSaveFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditSaveFile_ButtonClick);
			// 
			// labelControlSaveFileTitle
			// 
			this.labelControlSaveFileTitle.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlSaveFileTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlSaveFileTitle.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlSaveFileTitle.Location = new System.Drawing.Point(77, 125);
			this.labelControlSaveFileTitle.Name = "labelControlSaveFileTitle";
			this.labelControlSaveFileTitle.Size = new System.Drawing.Size(74, 18);
			this.labelControlSaveFileTitle.StyleController = this.styleController;
			this.labelControlSaveFileTitle.TabIndex = 17;
			this.labelControlSaveFileTitle.Text = "SAVE FILE";
			// 
			// pbSaveFile
			// 
			this.pbSaveFile.BackColor = System.Drawing.Color.White;
			this.pbSaveFile.ForeColor = System.Drawing.Color.Black;
			this.pbSaveFile.Image = global::SalesDepot.Properties.Resources.SearchBarSave;
			this.pbSaveFile.Location = new System.Drawing.Point(12, 125);
			this.pbSaveFile.Name = "pbSaveFile";
			this.pbSaveFile.Size = new System.Drawing.Size(59, 50);
			this.pbSaveFile.TabIndex = 16;
			this.pbSaveFile.TabStop = false;
			// 
			// pbOpenFile
			// 
			this.pbOpenFile.BackColor = System.Drawing.Color.White;
			this.pbOpenFile.ForeColor = System.Drawing.Color.Black;
			this.pbOpenFile.Image = global::SalesDepot.Properties.Resources.SearchBarOpen;
			this.pbOpenFile.Location = new System.Drawing.Point(12, 47);
			this.pbOpenFile.Name = "pbOpenFile";
			this.pbOpenFile.Size = new System.Drawing.Size(59, 50);
			this.pbOpenFile.TabIndex = 13;
			this.pbOpenFile.TabStop = false;
			// 
			// FormFileSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(453, 264);
			this.Controls.Add(this.buttonEditSaveFile);
			this.Controls.Add(this.labelControlSaveFileTitle);
			this.Controls.Add(this.pbSaveFile);
			this.Controls.Add(this.buttonEditOpenFile);
			this.Controls.Add(this.labelControlOpenFileTitle);
			this.Controls.Add(this.pbOpenFile);
			this.Controls.Add(this.labelControlFormTitle);
			this.Controls.Add(this.buttonXResetToDefault);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXEmail);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFileSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Saving Preferences";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFileSettings_FormClosing);
			this.Load += new System.EventHandler(this.FormFileSettings_Load);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditOpenFile.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditSaveFile.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSaveFile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbOpenFile)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXEmail;
		private DevComponents.DotNetBar.ButtonX buttonXResetToDefault;
		private DevExpress.XtraEditors.LabelControl labelControlFormTitle;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.PictureBox pbOpenFile;
		private DevExpress.XtraEditors.LabelControl labelControlOpenFileTitle;
		private DevExpress.XtraEditors.ButtonEdit buttonEditOpenFile;
		private DevExpress.XtraEditors.ButtonEdit buttonEditSaveFile;
		private DevExpress.XtraEditors.LabelControl labelControlSaveFileTitle;
		private System.Windows.Forms.PictureBox pbSaveFile;
	}
}