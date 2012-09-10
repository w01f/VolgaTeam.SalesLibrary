﻿namespace FileManager.ToolForms.IPad
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
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.laPassword = new System.Windows.Forms.Label();
            this.checkEditPassword = new DevExpress.XtraEditors.CheckEdit();
            this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
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
            this.laLogin.Location = new System.Drawing.Point(12, 9);
            this.laLogin.Name = "laLogin";
            this.laLogin.Size = new System.Drawing.Size(43, 16);
            this.laLogin.TabIndex = 0;
            this.laLogin.Text = "Login:";
            // 
            // textEditLogin
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditLogin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxValidationProvider.SetIconAlignment(this.textEditLogin, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditLogin.Location = new System.Drawing.Point(103, 6);
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
            this.laFirstName.Location = new System.Drawing.Point(12, 46);
            this.laFirstName.Name = "laFirstName";
            this.laFirstName.Size = new System.Drawing.Size(76, 16);
            this.laFirstName.TabIndex = 2;
            this.laFirstName.Text = "First Name:";
            // 
            // textEditFirstName
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditFirstName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxValidationProvider.SetIconAlignment(this.textEditFirstName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditFirstName.Location = new System.Drawing.Point(103, 43);
            this.textEditFirstName.Name = "textEditFirstName";
            this.textEditFirstName.Properties.NullText = "Type...";
            this.textEditFirstName.Size = new System.Drawing.Size(187, 22);
            this.textEditFirstName.StyleController = this.styleController;
            this.textEditFirstName.TabIndex = 3;
            this.textEditFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
            // 
            // textEditLastName
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditLastName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxValidationProvider.SetIconAlignment(this.textEditLastName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditLastName.Location = new System.Drawing.Point(103, 83);
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
            this.laLastName.Location = new System.Drawing.Point(12, 86);
            this.laLastName.Name = "laLastName";
            this.laLastName.Size = new System.Drawing.Size(75, 16);
            this.laLastName.TabIndex = 4;
            this.laLastName.Text = "Last Name:";
            // 
            // textEditEmail
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxValidationProvider.SetIconAlignment(this.textEditEmail, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditEmail.Location = new System.Drawing.Point(103, 124);
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
            this.laEmail.Location = new System.Drawing.Point(12, 127);
            this.laEmail.Name = "laEmail";
            this.laEmail.Size = new System.Drawing.Size(45, 16);
            this.laEmail.TabIndex = 6;
            this.laEmail.Text = "Email:";
            // 
            // textEditPassword
            // 
            this.dxErrorProvider.SetIconAlignment(this.textEditPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxValidationProvider.SetIconAlignment(this.textEditPassword, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.textEditPassword.Location = new System.Drawing.Point(103, 164);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.textEditPassword.Properties.NullText = "Type...";
            this.textEditPassword.Size = new System.Drawing.Size(187, 22);
            this.textEditPassword.StyleController = this.styleController;
            this.textEditPassword.TabIndex = 9;
            this.textEditPassword.Validating += new System.ComponentModel.CancelEventHandler(this.textEdit_Validating);
            // 
            // laPassword
            // 
            this.laPassword.AutoSize = true;
            this.laPassword.Location = new System.Drawing.Point(12, 167);
            this.laPassword.Name = "laPassword";
            this.laPassword.Size = new System.Drawing.Size(69, 16);
            this.laPassword.TabIndex = 8;
            this.laPassword.Text = "Password:";
            // 
            // checkEditPassword
            // 
            this.checkEditPassword.EditValue = true;
            this.checkEditPassword.Location = new System.Drawing.Point(12, 155);
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
            this.buttonXSave.Location = new System.Drawing.Point(63, 207);
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
            this.buttonXCancel.Location = new System.Drawing.Point(165, 207);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(75, 33);
            this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXCancel.TabIndex = 12;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // FormEditUser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(303, 247);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXSave);
            this.Controls.Add(this.textEditPassword);
            this.Controls.Add(this.laPassword);
            this.Controls.Add(this.textEditEmail);
            this.Controls.Add(this.laEmail);
            this.Controls.Add(this.textEditLastName);
            this.Controls.Add(this.laLastName);
            this.Controls.Add(this.textEditFirstName);
            this.Controls.Add(this.laFirstName);
            this.Controls.Add(this.textEditLogin);
            this.Controls.Add(this.laLogin);
            this.Controls.Add(this.checkEditPassword);
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
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        public DevExpress.XtraEditors.TextEdit textEditPassword;
        public DevExpress.XtraEditors.CheckEdit checkEditPassword;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
    }
}