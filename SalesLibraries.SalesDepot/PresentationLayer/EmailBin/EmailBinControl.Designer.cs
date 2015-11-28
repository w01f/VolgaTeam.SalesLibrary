namespace SalesLibraries.SalesDepot.PresentationLayer.EmailBin
{
	partial class EmailBinControl
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.laEmailBinDescription = new System.Windows.Forms.Label();
			this.buttonXPDF = new DevComponents.DotNetBar.ButtonX();
			this.buttonXZip = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEmptyEmailBin = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCreateEmail = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			this.pnBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControlFiles
			// 
			this.gridControlFiles.AllowDrop = true;
			this.gridControlFiles.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlFiles.Location = new System.Drawing.Point(0, 0);
			this.gridControlFiles.MainView = this.gridViewFiles;
			this.gridControlFiles.Name = "gridControlFiles";
			this.gridControlFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit});
			this.gridControlFiles.Size = new System.Drawing.Size(436, 179);
			this.gridControlFiles.TabIndex = 5;
			this.gridControlFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFiles});
			// 
			// gridViewFiles
			// 
			this.gridViewFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewFiles.Appearance.Row.Options.UseFont = true;
			this.gridViewFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName});
			this.gridViewFiles.GridControl = this.gridControlFiles;
			this.gridViewFiles.Name = "gridViewFiles";
			this.gridViewFiles.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewFiles.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewFiles.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewFiles.OptionsCustomization.AllowFilter = false;
			this.gridViewFiles.OptionsCustomization.AllowGroup = false;
			this.gridViewFiles.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewFiles.OptionsCustomization.AllowSort = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewFiles.OptionsView.RowAutoHeight = true;
			this.gridViewFiles.OptionsView.ShowColumnHeaders = false;
			this.gridViewFiles.OptionsView.ShowDetailButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupPanel = false;
			this.gridViewFiles.OptionsView.ShowIndicator = false;
			// 
			// gridColumnName
			// 
			this.gridColumnName.Caption = "gridColumnName";
			this.gridColumnName.ColumnEdit = this.repositoryItemButtonEdit;
			this.gridColumnName.FieldName = "NameWithExtension";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 0;
			// 
			// repositoryItemButtonEdit
			// 
			this.repositoryItemButtonEdit.AutoHeight = false;
			this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::SalesLibraries.SalesDepot.Properties.Resources.ButtonDelete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
			this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.laEmailBinDescription);
			this.pnBottom.Controls.Add(this.buttonXPDF);
			this.pnBottom.Controls.Add(this.buttonXZip);
			this.pnBottom.Controls.Add(this.buttonXEmptyEmailBin);
			this.pnBottom.Controls.Add(this.buttonXCreateEmail);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.ForeColor = System.Drawing.Color.Black;
			this.pnBottom.Location = new System.Drawing.Point(0, 179);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnBottom.Size = new System.Drawing.Size(436, 326);
			this.pnBottom.TabIndex = 4;
			// 
			// laEmailBinDescription
			// 
			this.laEmailBinDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.laEmailBinDescription.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laEmailBinDescription.ForeColor = System.Drawing.Color.DimGray;
			this.laEmailBinDescription.Location = new System.Drawing.Point(10, 0);
			this.laEmailBinDescription.Name = "laEmailBinDescription";
			this.laEmailBinDescription.Size = new System.Drawing.Size(416, 99);
			this.laEmailBinDescription.TabIndex = 6;
			this.laEmailBinDescription.Text = "You can Email some files to clients  with Outlook.\r\n\r\nRight Click on the file  if" +
    " you want to ATTACH IT to an  email.";
			this.laEmailBinDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonXPDF
			// 
			this.buttonXPDF.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXPDF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXPDF.AutoCheckOnClick = true;
			this.buttonXPDF.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXPDF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXPDF.Image = global::SalesLibraries.SalesDepot.Properties.Resources.EmailBinPdf;
			this.buttonXPDF.ImageFixedSize = new System.Drawing.Size(40, 40);
			this.buttonXPDF.Location = new System.Drawing.Point(12, 104);
			this.buttonXPDF.Name = "buttonXPDF";
			this.buttonXPDF.Size = new System.Drawing.Size(413, 47);
			this.buttonXPDF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXPDF.TabIndex = 5;
			this.buttonXPDF.Text = "   PowerPoint to PDF";
			this.buttonXPDF.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXPDF.TextColor = System.Drawing.Color.Black;
			this.buttonXPDF.CheckedChanged += new System.EventHandler(this.buttonXPDF_CheckedChanged);
			// 
			// buttonXZip
			// 
			this.buttonXZip.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXZip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXZip.AutoCheckOnClick = true;
			this.buttonXZip.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXZip.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXZip.Image = global::SalesLibraries.SalesDepot.Properties.Resources.EmailBinZip;
			this.buttonXZip.ImageFixedSize = new System.Drawing.Size(40, 40);
			this.buttonXZip.Location = new System.Drawing.Point(12, 157);
			this.buttonXZip.Name = "buttonXZip";
			this.buttonXZip.Size = new System.Drawing.Size(413, 47);
			this.buttonXZip.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXZip.TabIndex = 4;
			this.buttonXZip.Text = "   Zip Attachment";
			this.buttonXZip.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXZip.TextColor = System.Drawing.Color.Black;
			this.buttonXZip.CheckedChanged += new System.EventHandler(this.buttonXZip_CheckedChanged);
			// 
			// buttonXEmptyEmailBin
			// 
			this.buttonXEmptyEmailBin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmptyEmailBin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXEmptyEmailBin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmptyEmailBin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXEmptyEmailBin.Image = global::SalesLibraries.SalesDepot.Properties.Resources.EmailBinEmpty;
			this.buttonXEmptyEmailBin.ImageFixedSize = new System.Drawing.Size(40, 40);
			this.buttonXEmptyEmailBin.Location = new System.Drawing.Point(12, 210);
			this.buttonXEmptyEmailBin.Name = "buttonXEmptyEmailBin";
			this.buttonXEmptyEmailBin.Size = new System.Drawing.Size(413, 47);
			this.buttonXEmptyEmailBin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmptyEmailBin.TabIndex = 3;
			this.buttonXEmptyEmailBin.Text = "   Empty Email Bin";
			this.buttonXEmptyEmailBin.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXEmptyEmailBin.TextColor = System.Drawing.Color.Black;
			this.buttonXEmptyEmailBin.Click += new System.EventHandler(this.buttonXEmptyEmailBin_Click);
			// 
			// buttonXCreateEmail
			// 
			this.buttonXCreateEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCreateEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCreateEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCreateEmail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXCreateEmail.Image = global::SalesLibraries.SalesDepot.Properties.Resources.EmailBinSend;
			this.buttonXCreateEmail.ImageFixedSize = new System.Drawing.Size(42, 40);
			this.buttonXCreateEmail.Location = new System.Drawing.Point(12, 263);
			this.buttonXCreateEmail.Name = "buttonXCreateEmail";
			this.buttonXCreateEmail.Size = new System.Drawing.Size(413, 47);
			this.buttonXCreateEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCreateEmail.TabIndex = 0;
			this.buttonXCreateEmail.Text = "  Create Email";
			this.buttonXCreateEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonXCreateEmail.TextColor = System.Drawing.Color.Black;
			this.buttonXCreateEmail.Click += new System.EventHandler(this.buttonXCreateEmail_Click);
			// 
			// EmailBinControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.gridControlFiles);
			this.Controls.Add(this.pnBottom);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "EmailBinControl";
			this.Size = new System.Drawing.Size(436, 505);
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			this.pnBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlFiles;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
		private System.Windows.Forms.Panel pnBottom;
		private System.Windows.Forms.Label laEmailBinDescription;
		private DevComponents.DotNetBar.ButtonX buttonXPDF;
		private DevComponents.DotNetBar.ButtonX buttonXZip;
		private DevComponents.DotNetBar.ButtonX buttonXEmptyEmailBin;
		private DevComponents.DotNetBar.ButtonX buttonXCreateEmail;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
	}
}
