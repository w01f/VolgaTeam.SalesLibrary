namespace SalesLibraries.BatchTagger.PresentationLayer
{
	partial class FormLibraryCredentials
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.labelControlUser = new DevExpress.XtraEditors.LabelControl();
			this.textEditUser = new DevExpress.XtraEditors.TextEdit();
			this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
			this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.ForeColor = System.Drawing.Color.Black;
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.Appearance.Options.UseForeColor = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseForeColor = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(383, 91);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(79, 34);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 8;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(273, 91);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(79, 34);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 7;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// labelControlUser
			// 
			this.labelControlUser.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlUser.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlUser.Location = new System.Drawing.Point(12, 12);
			this.labelControlUser.Name = "labelControlUser";
			this.labelControlUser.Size = new System.Drawing.Size(93, 16);
			this.labelControlUser.StyleController = this.styleController;
			this.labelControlUser.TabIndex = 9;
			this.labelControlUser.Text = "User:";
			// 
			// textEditUser
			// 
			this.textEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditUser.Location = new System.Drawing.Point(111, 9);
			this.textEditUser.Name = "textEditUser";
			this.textEditUser.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditUser.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditUser.Properties.Appearance.Options.UseBackColor = true;
			this.textEditUser.Properties.Appearance.Options.UseForeColor = true;
			this.textEditUser.Size = new System.Drawing.Size(351, 22);
			this.textEditUser.StyleController = this.styleController;
			this.textEditUser.TabIndex = 10;
			// 
			// textEditPassword
			// 
			this.textEditPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditPassword.Location = new System.Drawing.Point(111, 52);
			this.textEditPassword.Name = "textEditPassword";
			this.textEditPassword.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditPassword.Properties.Appearance.Options.UseBackColor = true;
			this.textEditPassword.Properties.Appearance.Options.UseForeColor = true;
			this.textEditPassword.Size = new System.Drawing.Size(351, 22);
			this.textEditPassword.StyleController = this.styleController;
			this.textEditPassword.TabIndex = 12;
			// 
			// labelControlPassword
			// 
			this.labelControlPassword.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlPassword.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlPassword.Location = new System.Drawing.Point(12, 55);
			this.labelControlPassword.Name = "labelControlPassword";
			this.labelControlPassword.Size = new System.Drawing.Size(93, 16);
			this.labelControlPassword.StyleController = this.styleController;
			this.labelControlPassword.TabIndex = 11;
			this.labelControlPassword.Text = "Password:";
			// 
			// FormLibraryCredentials
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(474, 137);
			this.Controls.Add(this.textEditPassword);
			this.Controls.Add(this.labelControlPassword);
			this.Controls.Add(this.textEditUser);
			this.Controls.Add(this.labelControlUser);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLibraryCredentials";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Credentials";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraEditors.LabelControl labelControlUser;
		private DevExpress.XtraEditors.TextEdit textEditUser;
		private DevExpress.XtraEditors.TextEdit textEditPassword;
		private DevExpress.XtraEditors.LabelControl labelControlPassword;
	}
}