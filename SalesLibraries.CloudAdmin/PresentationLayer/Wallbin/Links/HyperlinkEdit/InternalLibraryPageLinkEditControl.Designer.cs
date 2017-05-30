namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	partial class InternalLibraryPageLinkEditControl
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
			this.laLibraryName = new System.Windows.Forms.Label();
			this.laPageName = new System.Windows.Forms.Label();
			this.checkEditShowHeaderText = new DevExpress.XtraEditors.CheckEdit();
			this.comboBoxEditLibraryName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditPageName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.checkEditOpenOnSamePage = new DevExpress.XtraEditors.CheckEdit();
			this.labelControlStyle = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxEditStyle = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowHeaderText.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditOpenOnSamePage.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStyle.Properties)).BeginInit();
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
			// laLibraryName
			// 
			this.laLibraryName.AutoSize = true;
			this.laLibraryName.BackColor = System.Drawing.Color.White;
			this.laLibraryName.ForeColor = System.Drawing.Color.Black;
			this.laLibraryName.Location = new System.Drawing.Point(3, 6);
			this.laLibraryName.Name = "laLibraryName";
			this.laLibraryName.Size = new System.Drawing.Size(86, 16);
			this.laLibraryName.TabIndex = 33;
			this.laLibraryName.Text = "Target Library";
			// 
			// laPageName
			// 
			this.laPageName.AutoSize = true;
			this.laPageName.BackColor = System.Drawing.Color.White;
			this.laPageName.ForeColor = System.Drawing.Color.Black;
			this.laPageName.Location = new System.Drawing.Point(3, 74);
			this.laPageName.Name = "laPageName";
			this.laPageName.Size = new System.Drawing.Size(77, 16);
			this.laPageName.TabIndex = 46;
			this.laPageName.Text = "Target Page";
			// 
			// checkEditShowHeaderText
			// 
			this.checkEditShowHeaderText.EditValue = true;
			this.checkEditShowHeaderText.Location = new System.Drawing.Point(6, 140);
			this.checkEditShowHeaderText.Name = "checkEditShowHeaderText";
			this.checkEditShowHeaderText.Properties.Caption = "Show Link Name in Header";
			this.checkEditShowHeaderText.Size = new System.Drawing.Size(249, 20);
			this.checkEditShowHeaderText.StyleController = this.styleController;
			this.checkEditShowHeaderText.TabIndex = 62;
			// 
			// comboBoxEditLibraryName
			// 
			this.comboBoxEditLibraryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditLibraryName.Location = new System.Drawing.Point(109, 3);
			this.comboBoxEditLibraryName.Name = "comboBoxEditLibraryName";
			this.comboBoxEditLibraryName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditLibraryName.Size = new System.Drawing.Size(448, 22);
			this.comboBoxEditLibraryName.StyleController = this.styleController;
			this.comboBoxEditLibraryName.TabIndex = 67;
			this.comboBoxEditLibraryName.EditValueChanged += new System.EventHandler(this.OnLibraryChanged);
			// 
			// comboBoxEditPageName
			// 
			this.comboBoxEditPageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditPageName.Location = new System.Drawing.Point(109, 71);
			this.comboBoxEditPageName.Name = "comboBoxEditPageName";
			this.comboBoxEditPageName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditPageName.Size = new System.Drawing.Size(448, 22);
			this.comboBoxEditPageName.StyleController = this.styleController;
			this.comboBoxEditPageName.TabIndex = 68;
			// 
			// checkEditOpenOnSamePage
			// 
			this.checkEditOpenOnSamePage.Location = new System.Drawing.Point(6, 257);
			this.checkEditOpenOnSamePage.Name = "checkEditOpenOnSamePage";
			this.checkEditOpenOnSamePage.Properties.Caption = "Launch this link in new Browser Tab";
			this.checkEditOpenOnSamePage.Size = new System.Drawing.Size(505, 20);
			this.checkEditOpenOnSamePage.StyleController = this.styleController;
			this.checkEditOpenOnSamePage.TabIndex = 69;
			// 
			// labelControlStyle
			// 
			this.labelControlStyle.Location = new System.Drawing.Point(6, 210);
			this.labelControlStyle.Name = "labelControlStyle";
			this.labelControlStyle.Size = new System.Drawing.Size(30, 16);
			this.labelControlStyle.StyleController = this.styleController;
			this.labelControlStyle.TabIndex = 78;
			this.labelControlStyle.Text = "Style";
			// 
			// comboBoxEditStyle
			// 
			this.comboBoxEditStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditStyle.Location = new System.Drawing.Point(109, 207);
			this.comboBoxEditStyle.Name = "comboBoxEditStyle";
			this.comboBoxEditStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditStyle.Properties.NullText = "Select Style...";
			this.comboBoxEditStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditStyle.Size = new System.Drawing.Size(448, 22);
			this.comboBoxEditStyle.StyleController = this.styleController;
			this.comboBoxEditStyle.TabIndex = 77;
			// 
			// InternalLibraryPageLinkEditControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.labelControlStyle);
			this.Controls.Add(this.comboBoxEditStyle);
			this.Controls.Add(this.checkEditOpenOnSamePage);
			this.Controls.Add(this.comboBoxEditLibraryName);
			this.Controls.Add(this.laPageName);
			this.Controls.Add(this.laLibraryName);
			this.Controls.Add(this.comboBoxEditPageName);
			this.Controls.Add(this.checkEditShowHeaderText);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "InternalLibraryPageLinkEditControl";
			this.Size = new System.Drawing.Size(560, 280);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowHeaderText.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditOpenOnSamePage.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStyle.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Label laLibraryName;
		private System.Windows.Forms.Label laPageName;
		private DevExpress.XtraEditors.CheckEdit checkEditShowHeaderText;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLibraryName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPageName;
		private DevExpress.XtraEditors.CheckEdit checkEditOpenOnSamePage;
		private DevExpress.XtraEditors.LabelControl labelControlStyle;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStyle;
	}
}
