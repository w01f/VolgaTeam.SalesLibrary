namespace SalesDepot.PresentationClasses.Gallery
{
	partial class FavoriteImagesControl
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
			this.gridControlLogoGallery = new DevExpress.XtraGrid.GridControl();
			this.layoutViewLogoGallery = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.gridColumnLogoGallery = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.layoutViewField_gridColumnLogoGallery_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.layoutViewField_gridColumnName_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.circularProgressWebpage = new DevComponents.DotNetBar.Controls.CircularProgress();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnLogoGallery_1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnName_1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControlLogoGallery
			// 
			this.gridControlLogoGallery.AllowDrop = true;
			this.gridControlLogoGallery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlLogoGallery.Location = new System.Drawing.Point(0, 0);
			this.gridControlLogoGallery.MainView = this.layoutViewLogoGallery;
			this.gridControlLogoGallery.Name = "gridControlLogoGallery";
			this.gridControlLogoGallery.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlLogoGallery.Size = new System.Drawing.Size(622, 464);
			this.gridControlLogoGallery.TabIndex = 38;
			this.gridControlLogoGallery.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewLogoGallery});
			// 
			// layoutViewLogoGallery
			// 
			this.layoutViewLogoGallery.Appearance.SelectionFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
			this.layoutViewLogoGallery.Appearance.SelectionFrame.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.layoutViewLogoGallery.Appearance.SelectionFrame.Options.UseBackColor = true;
			this.layoutViewLogoGallery.CardMinSize = new System.Drawing.Size(110, 125);
			this.layoutViewLogoGallery.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.gridColumnLogoGallery,
            this.gridColumnName});
			this.layoutViewLogoGallery.GridControl = this.gridControlLogoGallery;
			this.layoutViewLogoGallery.Name = "layoutViewLogoGallery";
			this.layoutViewLogoGallery.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewLogoGallery.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewLogoGallery.OptionsBehavior.AllowExpandCollapse = false;
			this.layoutViewLogoGallery.OptionsBehavior.AllowRuntimeCustomization = false;
			this.layoutViewLogoGallery.OptionsBehavior.AutoSelectAllInEditor = false;
			this.layoutViewLogoGallery.OptionsBehavior.Editable = false;
			this.layoutViewLogoGallery.OptionsBehavior.ReadOnly = true;
			this.layoutViewLogoGallery.OptionsCustomization.AllowFilter = false;
			this.layoutViewLogoGallery.OptionsCustomization.AllowSort = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupCardCaptions = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupCardIndents = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupCards = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupFields = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupHiddenItems = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupLayout = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupLayoutTreeView = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupView = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowResetShrinkButtons = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowSaveLoadLayoutButtons = false;
			this.layoutViewLogoGallery.OptionsItemText.TextToControlDistance = 0;
			this.layoutViewLogoGallery.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewLogoGallery.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewLogoGallery.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
			this.layoutViewLogoGallery.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutViewLogoGallery.OptionsView.FocusRectStyle = DevExpress.XtraGrid.Views.Layout.FocusRectStyle.None;
			this.layoutViewLogoGallery.OptionsView.ShowCardCaption = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardExpandButton = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardLines = false;
			this.layoutViewLogoGallery.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewLogoGallery.OptionsView.ShowHeaderPanel = false;
			this.layoutViewLogoGallery.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewLogoGallery.TemplateCard = this.layoutViewCard1;
			this.layoutViewLogoGallery.MouseDown += new System.Windows.Forms.MouseEventHandler(this.layoutViewLogoGallery_MouseDown);
			// 
			// gridColumnLogoGallery
			// 
			this.gridColumnLogoGallery.Caption = "TinyImage";
			this.gridColumnLogoGallery.ColumnEdit = this.repositoryItemPictureEdit;
			this.gridColumnLogoGallery.CustomizationCaption = "BigImage";
			this.gridColumnLogoGallery.FieldName = "BigImage";
			this.gridColumnLogoGallery.LayoutViewField = this.layoutViewField_gridColumnLogoGallery_1;
			this.gridColumnLogoGallery.Name = "gridColumnLogoGallery";
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// layoutViewField_gridColumnLogoGallery_1
			// 
			this.layoutViewField_gridColumnLogoGallery_1.EditorPreferredWidth = 107;
			this.layoutViewField_gridColumnLogoGallery_1.Location = new System.Drawing.Point(0, 0);
			this.layoutViewField_gridColumnLogoGallery_1.Name = "layoutViewField_gridColumnLogoGallery_1";
			this.layoutViewField_gridColumnLogoGallery_1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewField_gridColumnLogoGallery_1.Size = new System.Drawing.Size(127, 104);
			this.layoutViewField_gridColumnLogoGallery_1.TextLocation = DevExpress.Utils.Locations.Default;
			this.layoutViewField_gridColumnLogoGallery_1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewField_gridColumnLogoGallery_1.TextToControlDistance = 0;
			this.layoutViewField_gridColumnLogoGallery_1.TextVisible = false;
			// 
			// gridColumnName
			// 
			this.gridColumnName.AppearanceCell.BackColor = System.Drawing.Color.AliceBlue;
			this.gridColumnName.AppearanceCell.Options.UseBackColor = true;
			this.gridColumnName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnName.Caption = "Name";
			this.gridColumnName.FieldName = "Name";
			this.gridColumnName.LayoutViewField = this.layoutViewField_gridColumnName_1;
			this.gridColumnName.Name = "gridColumnName";
			// 
			// layoutViewField_gridColumnName_1
			// 
			this.layoutViewField_gridColumnName_1.AllowHotTrack = false;
			this.layoutViewField_gridColumnName_1.EditorPreferredWidth = 107;
			this.layoutViewField_gridColumnName_1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutViewField_gridColumnName_1.Location = new System.Drawing.Point(0, 104);
			this.layoutViewField_gridColumnName_1.Name = "layoutViewField_gridColumnName_1";
			this.layoutViewField_gridColumnName_1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewField_gridColumnName_1.Size = new System.Drawing.Size(127, 18);
			this.layoutViewField_gridColumnName_1.TextLocation = DevExpress.Utils.Locations.Default;
			this.layoutViewField_gridColumnName_1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewField_gridColumnName_1.TextToControlDistance = 0;
			this.layoutViewField_gridColumnName_1.TextVisible = false;
			// 
			// layoutViewCard1
			// 
			this.layoutViewCard1.CustomizationFormText = "TemplateCard";
			this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutViewCard1.GroupBordersVisible = false;
			this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_gridColumnLogoGallery_1,
            this.layoutViewField_gridColumnName_1});
			this.layoutViewCard1.Name = "layoutViewCard1";
			this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
			this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewCard1.Text = "TemplateCard";
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemRename,
            this.toolStripSeparator1,
            this.toolStripMenuItemDelete});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.ShowImageMargin = false;
			this.contextMenuStrip.Size = new System.Drawing.Size(102, 98);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(127, 22);
			this.toolStripMenuItemCopy.Text = "Copy";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
			// 
			// toolStripMenuItemRename
			// 
			this.toolStripMenuItemRename.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemRename.Name = "toolStripMenuItemRename";
			this.toolStripMenuItemRename.Size = new System.Drawing.Size(127, 22);
			this.toolStripMenuItemRename.Text = "Rename...";
			this.toolStripMenuItemRename.Click += new System.EventHandler(this.toolStripMenuItemRename_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(127, 22);
			this.toolStripMenuItemDelete.Text = "Delete";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// toolStripMenuItemEdit
			// 
			this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
			this.toolStripMenuItemEdit.Size = new System.Drawing.Size(127, 22);
			this.toolStripMenuItemEdit.Text = "Edit";
			this.toolStripMenuItemEdit.Click += new System.EventHandler(this.toolStripMenuItemEdit_Click);
			// 
			// circularProgressWebpage
			// 
			this.circularProgressWebpage.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgressWebpage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressWebpage.Location = new System.Drawing.Point(280, 203);
			this.circularProgressWebpage.Name = "circularProgressWebpage";
			this.circularProgressWebpage.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressWebpage.Size = new System.Drawing.Size(62, 59);
			this.circularProgressWebpage.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressWebpage.TabIndex = 39;
			this.circularProgressWebpage.TabStop = false;
			// 
			// FavoriteImagesControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.circularProgressWebpage);
			this.Controls.Add(this.gridControlLogoGallery);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "FavoriteImagesControl";
			this.Size = new System.Drawing.Size(622, 464);
			this.Resize += new System.EventHandler(this.Control_Resize);
			((System.ComponentModel.ISupportInitialize)(this.gridControlLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnLogoGallery_1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnName_1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlLogoGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewLogoGallery;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn gridColumnLogoGallery;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn gridColumnName;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_gridColumnLogoGallery_1;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_gridColumnName_1;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRename;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressWebpage;
	}
}
