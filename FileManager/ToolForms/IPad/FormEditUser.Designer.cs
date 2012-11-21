namespace FileManager.ToolForms.IPad
{
    partial class FormEditUser
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laLogin = new System.Windows.Forms.Label();
			this.textEditLogin = new DevExpress.XtraEditors.TextEdit();
			this.laFirstName = new System.Windows.Forms.Label();
			this.textEditFirstName = new DevExpress.XtraEditors.TextEdit();
			this.textEditLastName = new DevExpress.XtraEditors.TextEdit();
			this.laLastName = new System.Windows.Forms.Label();
			this.textEditEmail = new DevExpress.XtraEditors.TextEdit();
			this.laEmail = new System.Windows.Forms.Label();
			this.laPassword = new System.Windows.Forms.Label();
			this.checkEditPassword = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.buttonEditPassword = new DevExpress.XtraEditors.ButtonEdit();
			this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
			this.xtraTabControl = new FileManager.ToolForms.IPad.ValidatableTabControl();
			this.xtraTabPageUser = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageLibraries = new DevExpress.XtraTab.XtraTabPage();
			this.checkedListBoxLibraries = new DevExpress.XtraEditors.CheckedListBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditFirstName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLastName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmail.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageUser.SuspendLayout();
			this.xtraTabPageLibraries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxLibraries)).BeginInit();
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
			// laLogin
			// 
			this.laLogin.AutoSize = true;
			this.laLogin.Location = new System.Drawing.Point(3, 11);
			this.laLogin.Name = "laLogin";
			this.laLogin.Size = new System.Drawing.Size(43, 16);
			this.laLogin.TabIndex = 0;
			this.laLogin.Text = "Login:";
			// 
			// textEditLogin
			// 
			this.dxValidationProvider.SetIconAlignment(this.textEditLogin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditLogin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditLogin.Location = new System.Drawing.Point(94, 8);
			this.textEditLogin.Name = "textEditLogin";
			this.textEditLogin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.textEditLogin.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.textEditLogin.Properties.NullText = "Type...";
			this.textEditLogin.Size = new System.Drawing.Size(187, 22);
			this.textEditLogin.StyleController = this.styleController;
			this.textEditLogin.TabIndex = 1;
			this.textEditLogin.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// laFirstName
			// 
			this.laFirstName.AutoSize = true;
			this.laFirstName.Location = new System.Drawing.Point(3, 48);
			this.laFirstName.Name = "laFirstName";
			this.laFirstName.Size = new System.Drawing.Size(76, 16);
			this.laFirstName.TabIndex = 2;
			this.laFirstName.Text = "First Name:";
			// 
			// textEditFirstName
			// 
			this.dxValidationProvider.SetIconAlignment(this.textEditFirstName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditFirstName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditFirstName.Location = new System.Drawing.Point(94, 45);
			this.textEditFirstName.Name = "textEditFirstName";
			this.textEditFirstName.Properties.NullText = "Type...";
			this.textEditFirstName.Size = new System.Drawing.Size(187, 22);
			this.textEditFirstName.StyleController = this.styleController;
			this.textEditFirstName.TabIndex = 3;
			this.textEditFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// textEditLastName
			// 
			this.dxValidationProvider.SetIconAlignment(this.textEditLastName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditLastName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditLastName.Location = new System.Drawing.Point(94, 85);
			this.textEditLastName.Name = "textEditLastName";
			this.textEditLastName.Properties.NullText = "Type...";
			this.textEditLastName.Size = new System.Drawing.Size(187, 22);
			this.textEditLastName.StyleController = this.styleController;
			this.textEditLastName.TabIndex = 5;
			this.textEditLastName.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// laLastName
			// 
			this.laLastName.AutoSize = true;
			this.laLastName.Location = new System.Drawing.Point(3, 88);
			this.laLastName.Name = "laLastName";
			this.laLastName.Size = new System.Drawing.Size(75, 16);
			this.laLastName.TabIndex = 4;
			this.laLastName.Text = "Last Name:";
			// 
			// textEditEmail
			// 
			this.dxValidationProvider.SetIconAlignment(this.textEditEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.textEditEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.textEditEmail.Location = new System.Drawing.Point(94, 126);
			this.textEditEmail.Name = "textEditEmail";
			this.textEditEmail.Properties.Mask.EditMask = "(\\w|[\\.\\-])+@(\\w|[\\-]+\\.)*(\\w|[\\-]){2,63}\\.[a-zA-Z]{2,4}";
			this.textEditEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			this.textEditEmail.Properties.NullText = "Type...";
			this.textEditEmail.Size = new System.Drawing.Size(187, 22);
			this.textEditEmail.StyleController = this.styleController;
			this.textEditEmail.TabIndex = 7;
			this.textEditEmail.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
			// 
			// laEmail
			// 
			this.laEmail.AutoSize = true;
			this.laEmail.Location = new System.Drawing.Point(3, 129);
			this.laEmail.Name = "laEmail";
			this.laEmail.Size = new System.Drawing.Size(45, 16);
			this.laEmail.TabIndex = 6;
			this.laEmail.Text = "Email:";
			// 
			// laPassword
			// 
			this.laPassword.AutoSize = true;
			this.laPassword.Location = new System.Drawing.Point(3, 169);
			this.laPassword.Name = "laPassword";
			this.laPassword.Size = new System.Drawing.Size(69, 16);
			this.laPassword.TabIndex = 8;
			this.laPassword.Text = "Password:";
			// 
			// checkEditPassword
			// 
			this.checkEditPassword.EditValue = true;
			this.checkEditPassword.Location = new System.Drawing.Point(3, 157);
			this.checkEditPassword.Name = "checkEditPassword";
			this.checkEditPassword.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditPassword.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditPassword.Properties.AutoHeight = false;
			this.checkEditPassword.Properties.Caption = "Reset password";
			this.checkEditPassword.Size = new System.Drawing.Size(85, 43);
			this.checkEditPassword.StyleController = this.styleController;
			this.checkEditPassword.TabIndex = 10;
			this.checkEditPassword.Visible = false;
			this.checkEditPassword.CheckedChanged += new System.EventHandler(this.checkEditPassword_CheckedChanged);
			// 
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(63, 244);
			this.buttonXSave.Name = "buttonXSave";
			this.buttonXSave.Size = new System.Drawing.Size(75, 33);
			this.buttonXSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSave.TabIndex = 11;
			this.buttonXSave.Text = "Save";
			this.buttonXSave.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.CausesValidation = false;
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(165, 244);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 12;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonEditPassword
			// 
			this.dxValidationProvider.SetIconAlignment(this.buttonEditPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.dxErrorProvider.SetIconAlignment(this.buttonEditPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
			this.buttonEditPassword.Location = new System.Drawing.Point(94, 166);
			this.buttonEditPassword.Name = "buttonEditPassword";
			this.buttonEditPassword.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.buttonEditPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Generate new password", null, null, true)});
			this.buttonEditPassword.Properties.NullText = "Type...";
			this.buttonEditPassword.Size = new System.Drawing.Size(187, 22);
			this.buttonEditPassword.StyleController = this.styleController;
			this.buttonEditPassword.TabIndex = 13;
			this.buttonEditPassword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditPassword_ButtonClick);
			// 
			// dxErrorProvider
			// 
			this.dxErrorProvider.ContainerControl = this;
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Location = new System.Drawing.Point(2, 2);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageUser;
			this.xtraTabControl.Size = new System.Drawing.Size(294, 236);
			this.xtraTabControl.TabIndex = 14;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUser,
            this.xtraTabPageLibraries});
			// 
			// xtraTabPageUser
			// 
			this.xtraTabPageUser.Controls.Add(this.laLogin);
			this.xtraTabPageUser.Controls.Add(this.buttonEditPassword);
			this.xtraTabPageUser.Controls.Add(this.checkEditPassword);
			this.xtraTabPageUser.Controls.Add(this.textEditLogin);
			this.xtraTabPageUser.Controls.Add(this.laFirstName);
			this.xtraTabPageUser.Controls.Add(this.laPassword);
			this.xtraTabPageUser.Controls.Add(this.textEditFirstName);
			this.xtraTabPageUser.Controls.Add(this.textEditEmail);
			this.xtraTabPageUser.Controls.Add(this.laLastName);
			this.xtraTabPageUser.Controls.Add(this.laEmail);
			this.xtraTabPageUser.Controls.Add(this.textEditLastName);
			this.xtraTabPageUser.Name = "xtraTabPageUser";
			this.xtraTabPageUser.Size = new System.Drawing.Size(292, 210);
			this.xtraTabPageUser.Text = "User";
			// 
			// xtraTabPageLibraries
			// 
			this.xtraTabPageLibraries.Controls.Add(this.checkedListBoxLibraries);
			this.xtraTabPageLibraries.Name = "xtraTabPageLibraries";
			this.xtraTabPageLibraries.Size = new System.Drawing.Size(292, 210);
			this.xtraTabPageLibraries.Text = "Assigned Libraries";
			// 
			// checkedListBoxLibraries
			// 
			this.checkedListBoxLibraries.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkedListBoxLibraries.Appearance.Options.UseFont = true;
			this.checkedListBoxLibraries.CheckOnClick = true;
			this.checkedListBoxLibraries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBoxLibraries.ItemHeight = 20;
			this.checkedListBoxLibraries.Location = new System.Drawing.Point(0, 0);
			this.checkedListBoxLibraries.Name = "checkedListBoxLibraries";
			this.checkedListBoxLibraries.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxLibraries.Size = new System.Drawing.Size(292, 210);
			this.checkedListBoxLibraries.TabIndex = 1;
			// 
			// FormEditUser
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(298, 284);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditUser";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit User";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditUser_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditFirstName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLastName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmail.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageUser.ResumeLayout(false);
			this.xtraTabPageUser.PerformLayout();
			this.xtraTabPageLibraries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxLibraries)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Label laLogin;
        private System.Windows.Forms.Label laFirstName;
        private System.Windows.Forms.Label laLastName;
        private System.Windows.Forms.Label laEmail;
        private System.Windows.Forms.Label laPassword;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        public DevExpress.XtraEditors.TextEdit textEditLogin;
        public DevExpress.XtraEditors.TextEdit textEditFirstName;
        public DevExpress.XtraEditors.TextEdit textEditLastName;
		public DevExpress.XtraEditors.TextEdit textEditEmail;
        public DevExpress.XtraEditors.CheckEdit checkEditPassword;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
		private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
		public DevExpress.XtraEditors.ButtonEdit buttonEditPassword;
		private ValidatableTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageUser;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageLibraries;
		public DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxLibraries;
    }
}