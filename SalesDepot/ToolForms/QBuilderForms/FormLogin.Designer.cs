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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.labelControlHost = new DevExpress.XtraEditors.LabelControl();
			this.labelControlUser = new DevExpress.XtraEditors.LabelControl();
			this.labelControlPassword = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxEditHost = new DevExpress.XtraEditors.ComboBoxEdit();
			this.textEditUser = new DevExpress.XtraEditors.TextEdit();
			this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
			this.checkEditSave = new DevExpress.XtraEditors.CheckEdit();
			this.simpleButtonLogin = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.labelControlError = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHost.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSave.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// labelControlHost
			// 
			this.labelControlHost.Location = new System.Drawing.Point(12, 12);
			this.labelControlHost.Name = "labelControlHost";
			this.labelControlHost.Size = new System.Drawing.Size(27, 16);
			this.labelControlHost.StyleController = this.styleController;
			this.labelControlHost.TabIndex = 0;
			this.labelControlHost.Text = "Site:";
			// 
			// labelControlUser
			// 
			this.labelControlUser.Location = new System.Drawing.Point(12, 45);
			this.labelControlUser.Name = "labelControlUser";
			this.labelControlUser.Size = new System.Drawing.Size(31, 16);
			this.labelControlUser.StyleController = this.styleController;
			this.labelControlUser.TabIndex = 1;
			this.labelControlUser.Text = "User:";
			// 
			// labelControlPassword
			// 
			this.labelControlPassword.Location = new System.Drawing.Point(12, 77);
			this.labelControlPassword.Name = "labelControlPassword";
			this.labelControlPassword.Size = new System.Drawing.Size(61, 16);
			this.labelControlPassword.StyleController = this.styleController;
			this.labelControlPassword.TabIndex = 2;
			this.labelControlPassword.Text = "Password:";
			// 
			// comboBoxEditHost
			// 
			this.comboBoxEditHost.Location = new System.Drawing.Point(84, 9);
			this.comboBoxEditHost.Name = "comboBoxEditHost";
			this.comboBoxEditHost.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditHost.Size = new System.Drawing.Size(232, 22);
			this.comboBoxEditHost.StyleController = this.styleController;
			this.comboBoxEditHost.TabIndex = 3;
			// 
			// textEditUser
			// 
			this.textEditUser.Location = new System.Drawing.Point(84, 42);
			this.textEditUser.Name = "textEditUser";
			this.textEditUser.Size = new System.Drawing.Size(232, 22);
			this.textEditUser.StyleController = this.styleController;
			this.textEditUser.TabIndex = 4;
			// 
			// textEditPassword
			// 
			this.textEditPassword.Location = new System.Drawing.Point(84, 74);
			this.textEditPassword.Name = "textEditPassword";
			this.textEditPassword.Properties.PasswordChar = '*';
			this.textEditPassword.Size = new System.Drawing.Size(232, 22);
			this.textEditPassword.StyleController = this.styleController;
			this.textEditPassword.TabIndex = 5;
			// 
			// checkEditSave
			// 
			this.checkEditSave.Location = new System.Drawing.Point(10, 112);
			this.checkEditSave.Name = "checkEditSave";
			this.checkEditSave.Properties.Caption = "Save Password";
			this.checkEditSave.Size = new System.Drawing.Size(306, 21);
			this.checkEditSave.StyleController = this.styleController;
			this.checkEditSave.TabIndex = 6;
			// 
			// simpleButtonLogin
			// 
			this.simpleButtonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.simpleButtonLogin.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonLogin.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonLogin.Appearance.Options.UseFont = true;
			this.simpleButtonLogin.Appearance.Options.UseForeColor = true;
			this.simpleButtonLogin.Location = new System.Drawing.Point(55, 169);
			this.simpleButtonLogin.Name = "simpleButtonLogin";
			this.simpleButtonLogin.Size = new System.Drawing.Size(93, 34);
			this.simpleButtonLogin.StyleController = this.styleController;
			this.simpleButtonLogin.TabIndex = 7;
			this.simpleButtonLogin.Text = "Login";
			this.simpleButtonLogin.Click += new System.EventHandler(this.simpleButtonLogin_Click);
			// 
			// simpleButtonCancel
			// 
			this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonCancel.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonCancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonCancel.Appearance.Options.UseFont = true;
			this.simpleButtonCancel.Appearance.Options.UseForeColor = true;
			this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButtonCancel.Location = new System.Drawing.Point(180, 169);
			this.simpleButtonCancel.Name = "simpleButtonCancel";
			this.simpleButtonCancel.Size = new System.Drawing.Size(93, 34);
			this.simpleButtonCancel.StyleController = this.styleController;
			this.simpleButtonCancel.TabIndex = 8;
			this.simpleButtonCancel.Text = "Cancel";
			// 
			// labelControlError
			// 
			this.labelControlError.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControlError.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.labelControlError.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlError.Location = new System.Drawing.Point(12, 139);
			this.labelControlError.Name = "labelControlError";
			this.labelControlError.Size = new System.Drawing.Size(304, 13);
			this.labelControlError.TabIndex = 9;
			this.labelControlError.Text = "User or Password not correct for selected Site";
			this.labelControlError.Visible = false;
			// 
			// FormLogin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(328, 217);
			this.Controls.Add(this.labelControlError);
			this.Controls.Add(this.simpleButtonCancel);
			this.Controls.Add(this.simpleButtonLogin);
			this.Controls.Add(this.checkEditSave);
			this.Controls.Add(this.textEditPassword);
			this.Controls.Add(this.textEditUser);
			this.Controls.Add(this.comboBoxEditHost);
			this.Controls.Add(this.labelControlPassword);
			this.Controls.Add(this.labelControlUser);
			this.Controls.Add(this.labelControlHost);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.LabelControl labelControlHost;
		private DevExpress.XtraEditors.LabelControl labelControlUser;
		private DevExpress.XtraEditors.LabelControl labelControlPassword;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditHost;
		private DevExpress.XtraEditors.TextEdit textEditUser;
		private DevExpress.XtraEditors.TextEdit textEditPassword;
		private DevExpress.XtraEditors.CheckEdit checkEditSave;
		private DevExpress.XtraEditors.SimpleButton simpleButtonLogin;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
		private DevExpress.XtraEditors.LabelControl labelControlError;
	}
}