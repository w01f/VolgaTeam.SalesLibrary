namespace SalesDepot.SiteManager.PresentationClasses.Ticker.LinkEditors
{
	partial class LibraryLinkEditor
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
			this.labelControlLibraryTitle = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.textEditLibraryValue = new DevExpress.XtraEditors.TextEdit();
			this.textEditPageValue = new DevExpress.XtraEditors.TextEdit();
			this.labelControlPageTitle = new DevExpress.XtraEditors.LabelControl();
			this.textEditLinkValue = new DevExpress.XtraEditors.TextEdit();
			this.labelControlLinkTitle = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLibraryValue.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPageValue.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkValue.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControlLibraryTitle
			// 
			this.labelControlLibraryTitle.Location = new System.Drawing.Point(2, 0);
			this.labelControlLibraryTitle.Name = "labelControlLibraryTitle";
			this.labelControlLibraryTitle.Size = new System.Drawing.Size(43, 16);
			this.labelControlLibraryTitle.StyleController = this.styleController;
			this.labelControlLibraryTitle.TabIndex = 0;
			this.labelControlLibraryTitle.Text = "Library:";
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
			// textEditLibraryValue
			// 
			this.textEditLibraryValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditLibraryValue.Location = new System.Drawing.Point(0, 22);
			this.textEditLibraryValue.Name = "textEditLibraryValue";
			this.textEditLibraryValue.Properties.NullText = "Type Library Name...";
			this.textEditLibraryValue.Size = new System.Drawing.Size(280, 22);
			this.textEditLibraryValue.StyleController = this.styleController;
			this.textEditLibraryValue.TabIndex = 1;
			// 
			// textEditPageValue
			// 
			this.textEditPageValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditPageValue.Location = new System.Drawing.Point(0, 73);
			this.textEditPageValue.Name = "textEditPageValue";
			this.textEditPageValue.Properties.NullText = "Type Page Name...";
			this.textEditPageValue.Size = new System.Drawing.Size(280, 22);
			this.textEditPageValue.StyleController = this.styleController;
			this.textEditPageValue.TabIndex = 3;
			// 
			// labelControlPageTitle
			// 
			this.labelControlPageTitle.Location = new System.Drawing.Point(2, 51);
			this.labelControlPageTitle.Name = "labelControlPageTitle";
			this.labelControlPageTitle.Size = new System.Drawing.Size(34, 16);
			this.labelControlPageTitle.StyleController = this.styleController;
			this.labelControlPageTitle.TabIndex = 2;
			this.labelControlPageTitle.Text = "Page:";
			// 
			// textEditLinkValue
			// 
			this.textEditLinkValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditLinkValue.Location = new System.Drawing.Point(0, 123);
			this.textEditLinkValue.Name = "textEditLinkValue";
			this.textEditLinkValue.Properties.NullText = "Type Link Name...";
			this.textEditLinkValue.Size = new System.Drawing.Size(280, 22);
			this.textEditLinkValue.StyleController = this.styleController;
			this.textEditLinkValue.TabIndex = 5;
			// 
			// labelControlLinkTitle
			// 
			this.labelControlLinkTitle.Location = new System.Drawing.Point(2, 101);
			this.labelControlLinkTitle.Name = "labelControlLinkTitle";
			this.labelControlLinkTitle.Size = new System.Drawing.Size(28, 16);
			this.labelControlLinkTitle.StyleController = this.styleController;
			this.labelControlLinkTitle.TabIndex = 4;
			this.labelControlLinkTitle.Text = "Link:";
			// 
			// LibraryLinkEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.textEditLinkValue);
			this.Controls.Add(this.labelControlLinkTitle);
			this.Controls.Add(this.textEditPageValue);
			this.Controls.Add(this.labelControlPageTitle);
			this.Controls.Add(this.textEditLibraryValue);
			this.Controls.Add(this.labelControlLibraryTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "LibraryLinkEditor";
			this.Size = new System.Drawing.Size(280, 160);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLibraryValue.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditPageValue.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditLinkValue.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlLibraryTitle;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.TextEdit textEditLibraryValue;
		private DevExpress.XtraEditors.TextEdit textEditPageValue;
		private DevExpress.XtraEditors.LabelControl labelControlPageTitle;
		private DevExpress.XtraEditors.TextEdit textEditLinkValue;
		private DevExpress.XtraEditors.LabelControl labelControlLinkTitle;
	}
}
