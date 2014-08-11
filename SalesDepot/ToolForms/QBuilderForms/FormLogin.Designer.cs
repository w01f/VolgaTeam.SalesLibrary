namespace SalesDepot.ToolForms.QBuilderForms
{
	partial class FormLogin
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
			this.labelControlHost = new DevExpress.XtraEditors.LabelControl();
			this.labelControlUser = new DevExpress.XtraEditors.LabelControl();
			this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxEditHost = new DevExpress.XtraEditors.ComboBoxEdit();
			this.textEditUser = new DevExpress.XtraEditors.TextEdit();
			this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
			this.checkEditSave = new DevExpress.XtraEditors.CheckEdit();
			this.labelControlError = new DevExpress.XtraEditors.LabelControl();
			this.labelControlDislaimer = new DevExpress.XtraEditors.LabelControl();
			this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLogin = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHost.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSave.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
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
			// labelControlHost
			// 
			this.labelControlHost.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlHost.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlHost.Location = new System.Drawing.Point(12, 68);
			this.labelControlHost.Name = "labelControlHost";
			this.labelControlHost.Size = new System.Drawing.Size(27, 16);
			this.labelControlHost.StyleController = this.styleController;
			this.labelControlHost.TabIndex = 0;
			this.labelControlHost.Text = "Site:";
			// 
			// labelControlUser
			// 
			this.labelControlUser.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlUser.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlUser.Location = new System.Drawing.Point(12, 101);
			this.labelControlUser.Name = "labelControlUser";
			this.labelControlUser.Size = new System.Drawing.Size(31, 16);
			this.labelControlUser.StyleController = this.styleController;
			this.labelControlUser.TabIndex = 1;
			this.labelControlUser.Text = "User:";
			// 
			// labelControlPassword
			// 
			this.labelControlPassword.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlPassword.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlPassword.Location = new System.Drawing.Point(12, 133);
			this.labelControlPassword.Name = "labelControlPassword";
			this.labelControlPassword.Size = new System.Drawing.Size(61, 16);
			this.labelControlPassword.StyleController = this.styleController;
			this.labelControlPassword.TabIndex = 2;
			this.labelControlPassword.Text = "Password:";
			// 
			// comboBoxEditHost
			// 
			this.comboBoxEditHost.Location = new System.Drawing.Point(84, 65);
			this.comboBoxEditHost.Name = "comboBoxEditHost";
			this.comboBoxEditHost.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.comboBoxEditHost.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.comboBoxEditHost.Properties.Appearance.Options.UseBackColor = true;
			this.comboBoxEditHost.Properties.Appearance.Options.UseForeColor = true;
			this.comboBoxEditHost.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditHost.Size = new System.Drawing.Size(265, 22);
			this.comboBoxEditHost.StyleController = this.styleController;
			this.comboBoxEditHost.TabIndex = 3;
			// 
			// textEditUser
			// 
			this.textEditUser.Location = new System.Drawing.Point(84, 98);
			this.textEditUser.Name = "textEditUser";
			this.textEditUser.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditUser.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditUser.Properties.Appearance.Options.UseBackColor = true;
			this.textEditUser.Properties.Appearance.Options.UseForeColor = true;
			this.textEditUser.Size = new System.Drawing.Size(265, 22);
			this.textEditUser.StyleController = this.styleController;
			this.textEditUser.TabIndex = 4;
			// 
			// textEditPassword
			// 
			this.textEditPassword.Location = new System.Drawing.Point(84, 130);
			this.textEditPassword.Name = "textEditPassword";
			this.textEditPassword.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditPassword.Properties.Appearance.Options.UseBackColor = true;
			this.textEditPassword.Properties.Appearance.Options.UseForeColor = true;
			this.textEditPassword.Properties.PasswordChar = '*';
			this.textEditPassword.Size = new System.Drawing.Size(265, 22);
			this.textEditPassword.StyleController = this.styleController;
			this.textEditPassword.TabIndex = 5;
			// 
			// checkEditSave
			// 
			this.checkEditSave.Location = new System.Drawing.Point(10, 168);
			this.checkEditSave.Name = "checkEditSave";
			this.checkEditSave.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditSave.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditSave.Properties.Caption = "Save Password";
			this.checkEditSave.Size = new System.Drawing.Size(306, 20);
			this.checkEditSave.StyleController = this.styleController;
			this.checkEditSave.TabIndex = 6;
			// 
			// labelControlError
			// 
			this.labelControlError.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlError.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlError.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.labelControlError.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlError.Location = new System.Drawing.Point(24, 195);
			this.labelControlError.Name = "labelControlError";
			this.labelControlError.Size = new System.Drawing.Size(304, 13);
			this.labelControlError.TabIndex = 9;
			this.labelControlError.Text = "User or Password not correct for selected Site";
			this.labelControlError.Visible = false;
			// 
			// labelControlDislaimer
			// 
			this.labelControlDislaimer.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlDislaimer.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlDislaimer.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlDislaimer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.labelControlDislaimer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlDislaimer.Location = new System.Drawing.Point(55, 0);
			this.labelControlDislaimer.Name = "labelControlDislaimer";
			this.labelControlDislaimer.Padding = new System.Windows.Forms.Padding(3, 5, 3, 0);
			this.labelControlDislaimer.Size = new System.Drawing.Size(288, 59);
			this.labelControlDislaimer.StyleController = this.styleController;
			this.labelControlDislaimer.TabIndex = 10;
			this.labelControlDislaimer.Text = "You must Log into your Cloud Sales Library to create quickSITES on the internet…";
			// 
			// pictureBoxHelp
			// 
			this.pictureBoxHelp.BackColor = System.Drawing.Color.White;
			this.pictureBoxHelp.ForeColor = System.Drawing.Color.Black;
			this.pictureBoxHelp.Image = global::SalesDepot.Properties.Resources.SearchBarHelp;
			this.pictureBoxHelp.Location = new System.Drawing.Point(3, 5);
			this.pictureBoxHelp.Name = "pictureBoxHelp";
			this.pictureBoxHelp.Size = new System.Drawing.Size(48, 48);
			this.pictureBoxHelp.TabIndex = 11;
			this.pictureBoxHelp.TabStop = false;
			this.pictureBoxHelp.Click += new System.EventHandler(this.pictureBoxHelp_Click);
			this.pictureBoxHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pictureBoxHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(184, 220);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(107, 37);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 16;
			this.buttonXCancel.Text = "Cancel";
			// 
			// buttonXLogin
			// 
			this.buttonXLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLogin.Location = new System.Drawing.Point(61, 220);
			this.buttonXLogin.Name = "buttonXLogin";
			this.buttonXLogin.Size = new System.Drawing.Size(107, 37);
			this.buttonXLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLogin.TabIndex = 17;
			this.buttonXLogin.Text = "Login";
			this.buttonXLogin.Click += new System.EventHandler(this.simpleButtonLogin_Click);
			// 
			// FormLogin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(352, 265);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXLogin);
			this.Controls.Add(this.pictureBoxHelp);
			this.Controls.Add(this.labelControlDislaimer);
			this.Controls.Add(this.labelControlError);
			this.Controls.Add(this.checkEditSave);
			this.Controls.Add(this.textEditPassword);
			this.Controls.Add(this.textEditUser);
			this.Controls.Add(this.comboBoxEditHost);
			this.Controls.Add(this.labelControlPassword);
			this.Controls.Add(this.labelControlUser);
			this.Controls.Add(this.labelControlHost);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLogin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHost.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSave.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlHost;
		private DevExpress.XtraEditors.LabelControl labelControlUser;
		private DevExpress.XtraEditors.LabelControl labelControlPassword;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditHost;
		private DevExpress.XtraEditors.TextEdit textEditUser;
		private DevExpress.XtraEditors.TextEdit textEditPassword;
		private DevExpress.XtraEditors.CheckEdit checkEditSave;
		private DevExpress.XtraEditors.LabelControl labelControlError;
		private DevExpress.XtraEditors.LabelControl labelControlDislaimer;
		private System.Windows.Forms.PictureBox pictureBoxHelp;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXLogin;
	}
}