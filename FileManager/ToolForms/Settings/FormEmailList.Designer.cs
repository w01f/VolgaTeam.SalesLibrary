namespace FileManager.ToolForms.Settings
{
    public partial class FormEmailList
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
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laDescription = new System.Windows.Forms.Label();
			this.memoEditEmails = new DevExpress.XtraEditors.MemoEdit();
			this.radioButtonCreateEmail = new System.Windows.Forms.RadioButton();
			this.radioButtonSendEmail = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditEmails.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(12, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(463, 43);
			this.laTitle.TabIndex = 0;
			this.laTitle.Text = "The list below will receive Sales File Expiration Notifications:";
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(124, 313);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 8;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(261, 313);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 9;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
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
			// laDescription
			// 
			this.laDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDescription.Location = new System.Drawing.Point(12, 52);
			this.laDescription.Name = "laDescription";
			this.laDescription.Size = new System.Drawing.Size(398, 21);
			this.laDescription.TabIndex = 10;
			this.laDescription.Text = "The list below will receive Sales File Expiration Notifications:";
			// 
			// memoEditEmails
			// 
			this.memoEditEmails.Location = new System.Drawing.Point(13, 76);
			this.memoEditEmails.Name = "memoEditEmails";
			this.memoEditEmails.Size = new System.Drawing.Size(452, 185);
			this.memoEditEmails.StyleController = this.styleController;
			this.memoEditEmails.TabIndex = 11;
			this.memoEditEmails.UseOptimizedRendering = true;
			// 
			// radioButtonCreateEmail
			// 
			this.radioButtonCreateEmail.AutoSize = true;
			this.radioButtonCreateEmail.Location = new System.Drawing.Point(15, 278);
			this.radioButtonCreateEmail.Name = "radioButtonCreateEmail";
			this.radioButtonCreateEmail.Size = new System.Drawing.Size(139, 20);
			this.radioButtonCreateEmail.TabIndex = 12;
			this.radioButtonCreateEmail.TabStop = true;
			this.radioButtonCreateEmail.Text = "Only Create Emails";
			this.radioButtonCreateEmail.UseVisualStyleBackColor = true;
			// 
			// radioButtonSendEmail
			// 
			this.radioButtonSendEmail.AutoSize = true;
			this.radioButtonSendEmail.Location = new System.Drawing.Point(310, 278);
			this.radioButtonSendEmail.Name = "radioButtonSendEmail";
			this.radioButtonSendEmail.Size = new System.Drawing.Size(155, 20);
			this.radioButtonSendEmail.TabIndex = 13;
			this.radioButtonSendEmail.TabStop = true;
			this.radioButtonSendEmail.Text = "Create & Send Emails";
			this.radioButtonSendEmail.UseMnemonic = false;
			this.radioButtonSendEmail.UseVisualStyleBackColor = true;
			// 
			// FormEmailList
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(479, 349);
			this.Controls.Add(this.radioButtonSendEmail);
			this.Controls.Add(this.radioButtonCreateEmail);
			this.Controls.Add(this.memoEditEmails);
			this.Controls.Add(this.laDescription);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.laTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEmailList";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email List";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEmailList_FormClosed);
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditEmails.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Label laDescription;
        private DevExpress.XtraEditors.MemoEdit memoEditEmails;
        private System.Windows.Forms.RadioButton radioButtonCreateEmail;
        private System.Windows.Forms.RadioButton radioButtonSendEmail;
    }
}