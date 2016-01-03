namespace SalesLibraries.SiteManager.PresentationClasses.LinkConfigProfiles
{
	partial class FormEditProfile
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.textEditProfileName = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditProfileName.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.CausesValidation = false;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(177, 52);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(75, 52);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(75, 33);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 1;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControl1.Location = new System.Drawing.Point(12, 12);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(78, 16);
			this.labelControl1.StyleController = this.styleController;
			this.labelControl1.TabIndex = 15;
			this.labelControl1.Text = "Profile Name:";
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
			// textEditProfileName
			// 
			this.textEditProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditProfileName.Location = new System.Drawing.Point(96, 9);
			this.textEditProfileName.Name = "textEditProfileName";
			this.textEditProfileName.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditProfileName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditProfileName.Properties.Appearance.Options.UseBackColor = true;
			this.textEditProfileName.Properties.Appearance.Options.UseForeColor = true;
			this.textEditProfileName.Size = new System.Drawing.Size(223, 22);
			this.textEditProfileName.StyleController = this.styleController;
			this.textEditProfileName.TabIndex = 0;
			// 
			// FormEditProfile
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(331, 97);
			this.Controls.Add(this.textEditProfileName);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditProfile";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Profile";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditProfileName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.TextEdit textEditProfileName;
	}
}