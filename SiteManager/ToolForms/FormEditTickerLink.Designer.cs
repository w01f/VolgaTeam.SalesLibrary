namespace SalesDepot.SiteManager.ToolForms
{
    partial class FormEditTickerLink
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
			this.buttonXSave = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.labelControlTextTitle = new DevExpress.XtraEditors.LabelControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.memoEditText = new DevExpress.XtraEditors.MemoEdit();
			this.comboBoxEditType = new DevExpress.XtraEditors.ComboBoxEdit();
			this.pnDetails = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditText.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditType.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
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
			// buttonXSave
			// 
			this.buttonXSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXSave.Location = new System.Drawing.Point(67, 325);
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
			this.buttonXCancel.Location = new System.Drawing.Point(169, 325);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 12;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// labelControlTextTitle
			// 
			this.labelControlTextTitle.Location = new System.Drawing.Point(12, 66);
			this.labelControlTextTitle.Name = "labelControlTextTitle";
			this.labelControlTextTitle.Size = new System.Drawing.Size(29, 16);
			this.labelControlTextTitle.StyleController = this.styleController;
			this.labelControlTextTitle.TabIndex = 13;
			this.labelControlTextTitle.Text = "Text:";
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(10, 12);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(71, 16);
			this.labelControl1.StyleController = this.styleController;
			this.labelControl1.TabIndex = 15;
			this.labelControl1.Text = "Ticker Type:";
			// 
			// memoEditText
			// 
			this.memoEditText.Location = new System.Drawing.Point(8, 88);
			this.memoEditText.Name = "memoEditText";
			this.memoEditText.Properties.NullText = "Type Link Text...";
			this.memoEditText.Size = new System.Drawing.Size(290, 71);
			this.memoEditText.StyleController = this.styleController;
			this.memoEditText.TabIndex = 17;
			// 
			// comboBoxEditType
			// 
			this.comboBoxEditType.Location = new System.Drawing.Point(8, 34);
			this.comboBoxEditType.Name = "comboBoxEditType";
			this.comboBoxEditType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditType.Properties.NullText = "Select Ticker Type...";
			this.comboBoxEditType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditType.Size = new System.Drawing.Size(290, 22);
			this.comboBoxEditType.StyleController = this.styleController;
			this.comboBoxEditType.TabIndex = 18;
			// 
			// pnDetails
			// 
			this.pnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnDetails.Location = new System.Drawing.Point(8, 165);
			this.pnDetails.Name = "pnDetails";
			this.pnDetails.Size = new System.Drawing.Size(290, 154);
			this.pnDetails.TabIndex = 19;
			// 
			// FormEditTickerLink
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.ClientSize = new System.Drawing.Size(310, 365);
			this.Controls.Add(this.pnDetails);
			this.Controls.Add(this.comboBoxEditType);
			this.Controls.Add(this.memoEditText);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.labelControlTextTitle);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXSave);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditTickerLink";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Ticker Link";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditTickerLink_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditText.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditType.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
        private DevComponents.DotNetBar.ButtonX buttonXSave;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.LabelControl labelControlTextTitle;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.MemoEdit memoEditText;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditType;
		private System.Windows.Forms.Panel pnDetails;
    }
}