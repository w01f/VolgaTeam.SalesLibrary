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
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.checkEditConvertZip = new DevExpress.XtraEditors.CheckEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.checkEditConvertPdf = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXEmptyEmailBin = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCreateEmail = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditConvertZip.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditConvertPdf.Properties)).BeginInit();
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
			this.gridControlFiles.Size = new System.Drawing.Size(436, 340);
			this.gridControlFiles.TabIndex = 5;
			this.gridControlFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFiles});
			this.gridControlFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControlFiles_DragDrop);
			this.gridControlFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridControlFiles_DragEnter);
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
			this.pnBottom.Controls.Add(this.checkEditConvertZip);
			this.pnBottom.Controls.Add(this.checkEditConvertPdf);
			this.pnBottom.Controls.Add(this.buttonXEmptyEmailBin);
			this.pnBottom.Controls.Add(this.buttonXCreateEmail);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.ForeColor = System.Drawing.Color.Black;
			this.pnBottom.Location = new System.Drawing.Point(0, 340);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnBottom.Size = new System.Drawing.Size(436, 165);
			this.pnBottom.TabIndex = 4;
			// 
			// checkEditConvertZip
			// 
			this.checkEditConvertZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditConvertZip.Location = new System.Drawing.Point(12, 54);
			this.checkEditConvertZip.Name = "checkEditConvertZip";
			this.checkEditConvertZip.Properties.AutoWidth = true;
			this.checkEditConvertZip.Properties.Caption = "Compress All files into a zip";
			this.checkEditConvertZip.Size = new System.Drawing.Size(184, 20);
			this.checkEditConvertZip.StyleController = this.styleController;
			this.checkEditConvertZip.TabIndex = 7;
			this.checkEditConvertZip.CheckedChanged += new System.EventHandler(this.OnZipToggleCheckedChanged);
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
			// checkEditConvertPdf
			// 
			this.checkEditConvertPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditConvertPdf.Location = new System.Drawing.Point(12, 16);
			this.checkEditConvertPdf.Name = "checkEditConvertPdf";
			this.checkEditConvertPdf.Properties.AutoWidth = true;
			this.checkEditConvertPdf.Properties.Caption = "Convert All PPT files to PDF";
			this.checkEditConvertPdf.Size = new System.Drawing.Size(185, 20);
			this.checkEditConvertPdf.StyleController = this.styleController;
			this.checkEditConvertPdf.TabIndex = 6;
			this.checkEditConvertPdf.CheckedChanged += new System.EventHandler(this.OnPDFToggleCheckedChanged);
			// 
			// buttonXEmptyEmailBin
			// 
			this.buttonXEmptyEmailBin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmptyEmailBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXEmptyEmailBin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmptyEmailBin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXEmptyEmailBin.ImageFixedSize = new System.Drawing.Size(40, 40);
			this.buttonXEmptyEmailBin.Location = new System.Drawing.Point(13, 112);
			this.buttonXEmptyEmailBin.Name = "buttonXEmptyEmailBin";
			this.buttonXEmptyEmailBin.Size = new System.Drawing.Size(123, 37);
			this.buttonXEmptyEmailBin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmptyEmailBin.TabIndex = 3;
			this.buttonXEmptyEmailBin.Text = "Empty Email Bin";
			this.buttonXEmptyEmailBin.TextColor = System.Drawing.Color.Black;
			this.buttonXEmptyEmailBin.Click += new System.EventHandler(this.buttonXEmptyEmailBin_Click);
			// 
			// buttonXCreateEmail
			// 
			this.buttonXCreateEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCreateEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCreateEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCreateEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXCreateEmail.ImageFixedSize = new System.Drawing.Size(42, 40);
			this.buttonXCreateEmail.Location = new System.Drawing.Point(298, 112);
			this.buttonXCreateEmail.Name = "buttonXCreateEmail";
			this.buttonXCreateEmail.Size = new System.Drawing.Size(125, 37);
			this.buttonXCreateEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCreateEmail.TabIndex = 0;
			this.buttonXCreateEmail.Text = "Create Email";
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
			this.pnBottom.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditConvertZip.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditConvertPdf.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlFiles;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
		private System.Windows.Forms.Panel pnBottom;
		private DevComponents.DotNetBar.ButtonX buttonXEmptyEmailBin;
		private DevComponents.DotNetBar.ButtonX buttonXCreateEmail;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
		private DevExpress.XtraEditors.CheckEdit checkEditConvertZip;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditConvertPdf;
	}
}
