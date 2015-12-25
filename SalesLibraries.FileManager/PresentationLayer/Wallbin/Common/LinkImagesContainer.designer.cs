namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	sealed partial class LinkImagesContainer
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
			this.gridControlGallery = new DevExpress.XtraGrid.GridControl();
			this.layoutViewGallery = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.gridColumnImage = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.layoutViewFieldGallery = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCardGallery = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.gridControlGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCardGallery)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControlGallery
			// 
			this.gridControlGallery.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlGallery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlGallery.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlGallery.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlGallery.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlGallery.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlGallery.Location = new System.Drawing.Point(0, 0);
			this.gridControlGallery.MainView = this.layoutViewGallery;
			this.gridControlGallery.Name = "gridControlGallery";
			this.gridControlGallery.Size = new System.Drawing.Size(667, 503);
			this.gridControlGallery.TabIndex = 37;
			this.gridControlGallery.ToolTipController = this.toolTipController;
			this.gridControlGallery.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewGallery});
			// 
			// layoutViewGallery
			// 
			this.layoutViewGallery.CardMinSize = new System.Drawing.Size(72, 72);
			this.layoutViewGallery.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.gridColumnImage});
			this.layoutViewGallery.GridControl = this.gridControlGallery;
			this.layoutViewGallery.Name = "layoutViewGallery";
			this.layoutViewGallery.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewGallery.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewGallery.OptionsBehavior.AllowExpandCollapse = false;
			this.layoutViewGallery.OptionsBehavior.AllowRuntimeCustomization = false;
			this.layoutViewGallery.OptionsBehavior.AutoSelectAllInEditor = false;
			this.layoutViewGallery.OptionsBehavior.Editable = false;
			this.layoutViewGallery.OptionsBehavior.ReadOnly = true;
			this.layoutViewGallery.OptionsCustomization.AllowFilter = false;
			this.layoutViewGallery.OptionsCustomization.AllowSort = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupCardCaptions = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupCardIndents = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupCards = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupFields = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupHiddenItems = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupLayout = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupLayoutTreeView = false;
			this.layoutViewGallery.OptionsCustomization.ShowGroupView = false;
			this.layoutViewGallery.OptionsCustomization.ShowResetShrinkButtons = false;
			this.layoutViewGallery.OptionsCustomization.ShowSaveLoadLayoutButtons = false;
			this.layoutViewGallery.OptionsFind.AllowFindPanel = false;
			this.layoutViewGallery.OptionsFind.ClearFindOnClose = false;
			this.layoutViewGallery.OptionsFind.ShowCloseButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableCarouselModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableColumnModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableCustomizeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableMultiColumnModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableMultiRowModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnablePanButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableRowModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.EnableSingleModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowCarouselModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowColumnModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowCustomizeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowMultiColumnModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowMultiRowModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowPanButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowRowModeButton = false;
			this.layoutViewGallery.OptionsHeaderPanel.ShowSingleModeButton = false;
			this.layoutViewGallery.OptionsItemText.TextToControlDistance = 2;
			this.layoutViewGallery.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewGallery.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutViewGallery.OptionsView.ShowCardCaption = false;
			this.layoutViewGallery.OptionsView.ShowCardExpandButton = false;
			this.layoutViewGallery.OptionsView.ShowCardLines = false;
			this.layoutViewGallery.OptionsView.ShowFieldHints = false;
			this.layoutViewGallery.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewGallery.OptionsView.ShowHeaderPanel = false;
			this.layoutViewGallery.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewGallery.TemplateCard = this.layoutViewCardGallery;
			// 
			// gridColumnImage
			// 
			this.gridColumnImage.Caption = "Image";
			this.gridColumnImage.FieldName = "Image";
			this.gridColumnImage.LayoutViewField = this.layoutViewFieldGallery;
			this.gridColumnImage.Name = "gridColumnImage";
			// 
			// layoutViewFieldGallery
			// 
			this.layoutViewFieldGallery.EditorPreferredWidth = 63;
			this.layoutViewFieldGallery.Location = new System.Drawing.Point(0, 0);
			this.layoutViewFieldGallery.Name = "layoutViewFieldGallery";
			this.layoutViewFieldGallery.Size = new System.Drawing.Size(69, 22);
			this.layoutViewFieldGallery.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewFieldGallery.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewFieldGallery.TextToControlDistance = 0;
			this.layoutViewFieldGallery.TextVisible = false;
			// 
			// layoutViewCardGallery
			// 
			this.layoutViewCardGallery.CustomizationFormText = "TemplateCard";
			this.layoutViewCardGallery.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutViewCardGallery.GroupBordersVisible = false;
			this.layoutViewCardGallery.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewFieldGallery});
			this.layoutViewCardGallery.Name = "layoutViewCard1";
			this.layoutViewCardGallery.OptionsItemText.TextToControlDistance = 2;
			this.layoutViewCardGallery.Text = "TemplateCard";
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ShowShadow = false;
			this.toolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController_GetActiveObjectInfo);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFavoritesToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(174, 36);
			// 
			// addToFavoritesToolStripMenuItem
			// 
			this.addToFavoritesToolStripMenuItem.Image = global::SalesLibraries.FileManager.Properties.Resources.Favorites;
			this.addToFavoritesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
			this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(173, 32);
			this.addToFavoritesToolStripMenuItem.Text = "Add To Favorites";
			this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
			// 
			// LinkImagesContainer
			// 
			this.Controls.Add(this.gridControlGallery);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "LinkImagesContainer";
			this.Size = new System.Drawing.Size(667, 503);
			((System.ComponentModel.ISupportInitialize)(this.gridControlGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCardGallery)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControlGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewGallery;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn gridColumnImage;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewFieldGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCardGallery;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
		private DevExpress.Utils.ToolTipController toolTipController;
	}
}
