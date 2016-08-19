namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	sealed partial class PowerPointPreviewOptions
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
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.laAdminTools = new System.Windows.Forms.Label();
			this.buttonXOpenQV = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRefreshPreview = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenWV = new DevComponents.DotNetBar.ButtonX();
			this.checkEditFakeDate = new DevExpress.XtraEditors.CheckEdit();
			this.dateEditFakeDate = new DevExpress.XtraEditors.DateEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFakeDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditFakeDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditFakeDate.Properties)).BeginInit();
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
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
			this.labelControlTitle.Location = new System.Drawing.Point(8, 12);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(440, 34);
			this.labelControlTitle.TabIndex = 24;
			this.labelControlTitle.Text = "This is some serious Advanced Admin Ninja Stuff…\r\nDon’t mess with these, unless y" +
    "ou really know what you are doing…";
			// 
			// laAdminTools
			// 
			this.laAdminTools.AutoSize = true;
			this.laAdminTools.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdminTools.ForeColor = System.Drawing.Color.Black;
			this.laAdminTools.Location = new System.Drawing.Point(5, 73);
			this.laAdminTools.Name = "laAdminTools";
			this.laAdminTools.Size = new System.Drawing.Size(83, 16);
			this.laAdminTools.TabIndex = 10;
			this.laAdminTools.Text = "Admin Tools:";
			// 
			// buttonXOpenQV
			// 
			this.buttonXOpenQV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenQV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOpenQV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenQV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpenQV.Location = new System.Drawing.Point(78, 114);
			this.buttonXOpenQV.Name = "buttonXOpenQV";
			this.buttonXOpenQV.Size = new System.Drawing.Size(375, 30);
			this.buttonXOpenQV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenQV.TabIndex = 11;
			this.buttonXOpenQV.Text = "!QV Folder";
			this.buttonXOpenQV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenQV.Click += new System.EventHandler(this.buttonXOpenQV_Click);
			// 
			// buttonXRefreshPreview
			// 
			this.buttonXRefreshPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshPreview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXRefreshPreview.Location = new System.Drawing.Point(78, 217);
			this.buttonXRefreshPreview.Name = "buttonXRefreshPreview";
			this.buttonXRefreshPreview.Size = new System.Drawing.Size(375, 30);
			this.buttonXRefreshPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshPreview.TabIndex = 26;
			this.buttonXRefreshPreview.Text = "Refresh QV & WV";
			this.buttonXRefreshPreview.TextColor = System.Drawing.Color.Black;
			this.buttonXRefreshPreview.UseMnemonic = false;
			this.buttonXRefreshPreview.Click += new System.EventHandler(this.buttonXRefreshPreview_Click);
			// 
			// buttonXOpenWV
			// 
			this.buttonXOpenWV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenWV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOpenWV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenWV.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXOpenWV.Location = new System.Drawing.Point(78, 165);
			this.buttonXOpenWV.Name = "buttonXOpenWV";
			this.buttonXOpenWV.Size = new System.Drawing.Size(375, 30);
			this.buttonXOpenWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenWV.TabIndex = 25;
			this.buttonXOpenWV.Text = "!WV Folder";
			this.buttonXOpenWV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenWV.Click += new System.EventHandler(this.buttonXOpenWV_Click);
			// 
			// checkEditFakeDate
			// 
			this.checkEditFakeDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditFakeDate.Location = new System.Drawing.Point(8, 508);
			this.checkEditFakeDate.Name = "checkEditFakeDate";
			this.checkEditFakeDate.Properties.AutoWidth = true;
			this.checkEditFakeDate.Properties.Caption = "";
			this.checkEditFakeDate.Size = new System.Drawing.Size(19, 19);
			this.checkEditFakeDate.StyleController = this.styleController;
			this.checkEditFakeDate.TabIndex = 27;
			this.checkEditFakeDate.CheckedChanged += new System.EventHandler(this.checkEditFakeDate_CheckedChanged);
			// 
			// dateEditFakeDate
			// 
			this.dateEditFakeDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateEditFakeDate.EditValue = null;
			this.dateEditFakeDate.Location = new System.Drawing.Point(33, 507);
			this.dateEditFakeDate.Name = "dateEditFakeDate";
			this.dateEditFakeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditFakeDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditFakeDate.Size = new System.Drawing.Size(129, 22);
			this.dateEditFakeDate.StyleController = this.styleController;
			this.dateEditFakeDate.TabIndex = 28;
			this.dateEditFakeDate.Visible = false;
			// 
			// PowerPointPreviewOptions
			// 
			this.Controls.Add(this.dateEditFakeDate);
			this.Controls.Add(this.checkEditFakeDate);
			this.Controls.Add(this.buttonXRefreshPreview);
			this.Controls.Add(this.buttonXOpenWV);
			this.Controls.Add(this.laAdminTools);
			this.Controls.Add(this.buttonXOpenQV);
			this.Controls.Add(this.labelControlTitle);
			this.Name = "PowerPointPreviewOptions";
			this.Size = new System.Drawing.Size(531, 541);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFakeDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditFakeDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditFakeDate.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		public System.Windows.Forms.Label laAdminTools;
		public DevComponents.DotNetBar.ButtonX buttonXOpenQV;
		public DevComponents.DotNetBar.ButtonX buttonXRefreshPreview;
		public DevComponents.DotNetBar.ButtonX buttonXOpenWV;
		private DevExpress.XtraEditors.CheckEdit checkEditFakeDate;
		private DevExpress.XtraEditors.DateEdit dateEditFakeDate;
	}
}
