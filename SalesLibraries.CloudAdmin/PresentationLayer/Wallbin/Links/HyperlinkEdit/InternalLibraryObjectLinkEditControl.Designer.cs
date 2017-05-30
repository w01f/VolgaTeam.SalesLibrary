namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	partial class InternalLibraryObjectLinkEditControl
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
			this.laWindowName = new System.Windows.Forms.Label();
			this.laLibraryLinkName = new System.Windows.Forms.Label();
			this.comboBoxEditLibraryName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditPageName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditWindowName = new DevExpress.XtraEditors.ComboBoxEdit();
			this.comboBoxEditLibraryLinkName = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditWindowName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryLinkName.Properties)).BeginInit();
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
			this.laPageName.Location = new System.Drawing.Point(3, 86);
			this.laPageName.Name = "laPageName";
			this.laPageName.Size = new System.Drawing.Size(77, 16);
			this.laPageName.TabIndex = 46;
			this.laPageName.Text = "Target Page";
			// 
			// laWindowName
			// 
			this.laWindowName.AutoSize = true;
			this.laWindowName.BackColor = System.Drawing.Color.White;
			this.laWindowName.ForeColor = System.Drawing.Color.Black;
			this.laWindowName.Location = new System.Drawing.Point(3, 166);
			this.laWindowName.Name = "laWindowName";
			this.laWindowName.Size = new System.Drawing.Size(93, 16);
			this.laWindowName.TabIndex = 50;
			this.laWindowName.Text = "Target Window";
			// 
			// laLibraryLinkName
			// 
			this.laLibraryLinkName.AutoSize = true;
			this.laLibraryLinkName.BackColor = System.Drawing.Color.White;
			this.laLibraryLinkName.ForeColor = System.Drawing.Color.Black;
			this.laLibraryLinkName.Location = new System.Drawing.Point(3, 246);
			this.laLibraryLinkName.Name = "laLibraryLinkName";
			this.laLibraryLinkName.Size = new System.Drawing.Size(71, 16);
			this.laLibraryLinkName.TabIndex = 51;
			this.laLibraryLinkName.Text = "Target Link";
			// 
			// comboBoxEditLibraryName
			// 
			this.comboBoxEditLibraryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditLibraryName.Location = new System.Drawing.Point(109, 3);
			this.comboBoxEditLibraryName.Name = "comboBoxEditLibraryName";
			this.comboBoxEditLibraryName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditLibraryName.Size = new System.Drawing.Size(413, 22);
			this.comboBoxEditLibraryName.StyleController = this.styleController;
			this.comboBoxEditLibraryName.TabIndex = 52;
			this.comboBoxEditLibraryName.EditValueChanged += new System.EventHandler(this.OnLibraryChanged);
			// 
			// comboBoxEditPageName
			// 
			this.comboBoxEditPageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditPageName.Location = new System.Drawing.Point(109, 83);
			this.comboBoxEditPageName.Name = "comboBoxEditPageName";
			this.comboBoxEditPageName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditPageName.Size = new System.Drawing.Size(413, 22);
			this.comboBoxEditPageName.StyleController = this.styleController;
			this.comboBoxEditPageName.TabIndex = 53;
			this.comboBoxEditPageName.EditValueChanged += new System.EventHandler(this.OnLibraryPageChanged);
			// 
			// comboBoxEditWindowName
			// 
			this.comboBoxEditWindowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditWindowName.Location = new System.Drawing.Point(109, 163);
			this.comboBoxEditWindowName.Name = "comboBoxEditWindowName";
			this.comboBoxEditWindowName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditWindowName.Size = new System.Drawing.Size(413, 22);
			this.comboBoxEditWindowName.StyleController = this.styleController;
			this.comboBoxEditWindowName.TabIndex = 54;
			this.comboBoxEditWindowName.EditValueChanged += new System.EventHandler(this.OnLibraryFolderChanged);
			// 
			// comboBoxEditLibraryLinkName
			// 
			this.comboBoxEditLibraryLinkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEditLibraryLinkName.Location = new System.Drawing.Point(109, 243);
			this.comboBoxEditLibraryLinkName.Name = "comboBoxEditLibraryLinkName";
			this.comboBoxEditLibraryLinkName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditLibraryLinkName.Size = new System.Drawing.Size(413, 22);
			this.comboBoxEditLibraryLinkName.StyleController = this.styleController;
			this.comboBoxEditLibraryLinkName.TabIndex = 55;
			// 
			// InternalLibraryObjectLinkEditControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.laLibraryLinkName);
			this.Controls.Add(this.laWindowName);
			this.Controls.Add(this.laPageName);
			this.Controls.Add(this.laLibraryName);
			this.Controls.Add(this.comboBoxEditLibraryName);
			this.Controls.Add(this.comboBoxEditLibraryLinkName);
			this.Controls.Add(this.comboBoxEditWindowName);
			this.Controls.Add(this.comboBoxEditPageName);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "InternalLibraryObjectLinkEditControl";
			this.Size = new System.Drawing.Size(525, 280);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditWindowName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLibraryLinkName.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Label laLibraryName;
		private System.Windows.Forms.Label laPageName;
		private System.Windows.Forms.Label laWindowName;
		private System.Windows.Forms.Label laLibraryLinkName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLibraryName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPageName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditWindowName;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLibraryLinkName;
	}
}
