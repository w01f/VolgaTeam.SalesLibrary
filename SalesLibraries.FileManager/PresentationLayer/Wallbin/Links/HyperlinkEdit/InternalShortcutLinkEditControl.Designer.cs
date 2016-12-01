namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	partial class InternalShortcutLinkEditControl
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
			this.laShortcutId = new System.Windows.Forms.Label();
			this.textEditShortcutId = new DevExpress.XtraEditors.TextEdit();
			this.checkEditOpenOnSamePage = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditShortcutId.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditOpenOnSamePage.Properties)).BeginInit();
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
			// laShortcutId
			// 
			this.laShortcutId.AutoSize = true;
			this.laShortcutId.BackColor = System.Drawing.Color.White;
			this.laShortcutId.ForeColor = System.Drawing.Color.Black;
			this.laShortcutId.Location = new System.Drawing.Point(14, 9);
			this.laShortcutId.Name = "laShortcutId";
			this.laShortcutId.Size = new System.Drawing.Size(58, 16);
			this.laShortcutId.TabIndex = 33;
			this.laShortcutId.Text = "Static ID";
			// 
			// textEditShortcutId
			// 
			this.textEditShortcutId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditShortcutId.Location = new System.Drawing.Point(130, 6);
			this.textEditShortcutId.Name = "textEditShortcutId";
			this.textEditShortcutId.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditShortcutId.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.textEditShortcutId.Properties.Appearance.Options.UseBackColor = true;
			this.textEditShortcutId.Properties.Appearance.Options.UseForeColor = true;
			this.textEditShortcutId.Size = new System.Drawing.Size(392, 22);
			this.textEditShortcutId.StyleController = this.styleController;
			this.textEditShortcutId.TabIndex = 1;
			// 
			// checkEditShowLogo
			// 
			this.checkEditOpenOnSamePage.EditValue = true;
			this.checkEditOpenOnSamePage.Location = new System.Drawing.Point(17, 47);
			this.checkEditOpenOnSamePage.Name = "checkEditOpenOnSamePage";
			this.checkEditOpenOnSamePage.Properties.Caption = "Launch this shortcut in new Browser Tab";
			this.checkEditOpenOnSamePage.Size = new System.Drawing.Size(505, 20);
			this.checkEditOpenOnSamePage.StyleController = this.styleController;
			this.checkEditOpenOnSamePage.TabIndex = 57;
			// 
			// InternalShortcutLinkEditControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkEditOpenOnSamePage);
			this.Controls.Add(this.laShortcutId);
			this.Controls.Add(this.textEditShortcutId);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "InternalShortcutLinkEditControl";
			this.Size = new System.Drawing.Size(525, 190);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditShortcutId.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditOpenOnSamePage.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Label laShortcutId;
		private DevExpress.XtraEditors.TextEdit textEditShortcutId;
		private DevExpress.XtraEditors.CheckEdit checkEditOpenOnSamePage;
	}
}
