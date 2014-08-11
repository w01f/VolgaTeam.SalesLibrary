namespace FileManager.ToolForms.WallBin
{
	partial class FormSelectWidget
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
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.groupBoxWidgets = new DevExpress.XtraEditors.GroupControl();
			this.gridControlWidgets = new DevExpress.XtraGrid.GridControl();
			this.layoutViewWidgets = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.gridColumnImage = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.layoutViewField_gridColumnImage = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
			this.pbSelectedWidget = new System.Windows.Forms.PictureBox();
			this.laAvailableWidgets = new System.Windows.Forms.Label();
			this.laSelectedWidget = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxWidgets)).BeginInit();
			this.groupBoxWidgets.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlWidgets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewWidgets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedWidget)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(67, 292);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 8;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(204, 292);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 9;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
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
			// groupBoxWidgets
			// 
			this.groupBoxWidgets.Appearance.BackColor = System.Drawing.Color.White;
			this.groupBoxWidgets.Appearance.ForeColor = System.Drawing.Color.Black;
			this.groupBoxWidgets.Appearance.Options.UseBackColor = true;
			this.groupBoxWidgets.Appearance.Options.UseForeColor = true;
			this.groupBoxWidgets.Controls.Add(this.gridControlWidgets);
			this.groupBoxWidgets.Controls.Add(this.pbSelectedWidget);
			this.groupBoxWidgets.Controls.Add(this.laAvailableWidgets);
			this.groupBoxWidgets.Controls.Add(this.laSelectedWidget);
			this.groupBoxWidgets.Location = new System.Drawing.Point(12, 7);
			this.groupBoxWidgets.Name = "groupBoxWidgets";
			this.groupBoxWidgets.ShowCaption = false;
			this.groupBoxWidgets.Size = new System.Drawing.Size(341, 271);
			this.groupBoxWidgets.TabIndex = 10;
			// 
			// gridControlWidgets
			// 
			this.gridControlWidgets.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlWidgets.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlWidgets.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlWidgets.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlWidgets.Location = new System.Drawing.Point(9, 79);
			this.gridControlWidgets.MainView = this.layoutViewWidgets;
			this.gridControlWidgets.Name = "gridControlWidgets";
			this.gridControlWidgets.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlWidgets.Size = new System.Drawing.Size(323, 186);
			this.gridControlWidgets.TabIndex = 4;
			this.gridControlWidgets.ToolTipController = this.toolTipController;
			this.gridControlWidgets.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewWidgets});
			// 
			// layoutViewWidgets
			// 
			this.layoutViewWidgets.CardMinSize = new System.Drawing.Size(37, 38);
			this.layoutViewWidgets.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.gridColumnImage});
			this.layoutViewWidgets.GridControl = this.gridControlWidgets;
			this.layoutViewWidgets.Name = "layoutViewWidgets";
			this.layoutViewWidgets.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewWidgets.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewWidgets.OptionsBehavior.AllowExpandCollapse = false;
			this.layoutViewWidgets.OptionsBehavior.AllowRuntimeCustomization = false;
			this.layoutViewWidgets.OptionsBehavior.AutoSelectAllInEditor = false;
			this.layoutViewWidgets.OptionsBehavior.Editable = false;
			this.layoutViewWidgets.OptionsBehavior.ReadOnly = true;
			this.layoutViewWidgets.OptionsCustomization.AllowFilter = false;
			this.layoutViewWidgets.OptionsCustomization.AllowSort = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupCardCaptions = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupCardIndents = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupCards = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupFields = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupHiddenItems = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupLayout = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupLayoutTreeView = false;
			this.layoutViewWidgets.OptionsCustomization.ShowGroupView = false;
			this.layoutViewWidgets.OptionsCustomization.ShowResetShrinkButtons = false;
			this.layoutViewWidgets.OptionsCustomization.ShowSaveLoadLayoutButtons = false;
			this.layoutViewWidgets.OptionsFind.AllowFindPanel = false;
			this.layoutViewWidgets.OptionsFind.ClearFindOnClose = false;
			this.layoutViewWidgets.OptionsFind.ShowCloseButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableCarouselModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableColumnModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableCustomizeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableMultiColumnModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableMultiRowModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnablePanButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableRowModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.EnableSingleModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowCarouselModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowColumnModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowCustomizeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowMultiColumnModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowMultiRowModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowPanButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowRowModeButton = false;
			this.layoutViewWidgets.OptionsHeaderPanel.ShowSingleModeButton = false;
			this.layoutViewWidgets.OptionsItemText.TextToControlDistance = 2;
			this.layoutViewWidgets.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewWidgets.OptionsView.ShowCardCaption = false;
			this.layoutViewWidgets.OptionsView.ShowCardExpandButton = false;
			this.layoutViewWidgets.OptionsView.ShowCardLines = false;
			this.layoutViewWidgets.OptionsView.ShowFieldHints = false;
			this.layoutViewWidgets.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewWidgets.OptionsView.ShowHeaderPanel = false;
			this.layoutViewWidgets.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewWidgets.TemplateCard = this.layoutViewCard1;
			this.layoutViewWidgets.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.layoutViewWidgets_FocusedRowChanged);
			this.layoutViewWidgets.Click += new System.EventHandler(this.layoutViewWidgets_Click);
			// 
			// gridColumnImage
			// 
			this.gridColumnImage.Caption = "Image";
			this.gridColumnImage.ColumnEdit = this.repositoryItemPictureEdit;
			this.gridColumnImage.FieldName = "Image";
			this.gridColumnImage.LayoutViewField = this.layoutViewField_gridColumnImage;
			this.gridColumnImage.Name = "gridColumnImage";
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// layoutViewField_gridColumnImage
			// 
			this.layoutViewField_gridColumnImage.EditorPreferredWidth = 51;
			this.layoutViewField_gridColumnImage.Location = new System.Drawing.Point(0, 0);
			this.layoutViewField_gridColumnImage.Name = "layoutViewField_gridColumnImage";
			this.layoutViewField_gridColumnImage.Size = new System.Drawing.Size(57, 42);
			this.layoutViewField_gridColumnImage.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewField_gridColumnImage.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewField_gridColumnImage.TextToControlDistance = 0;
			this.layoutViewField_gridColumnImage.TextVisible = false;
			// 
			// layoutViewCard1
			// 
			this.layoutViewCard1.CustomizationFormText = "TemplateCard";
			this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutViewCard1.GroupBordersVisible = false;
			this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_gridColumnImage});
			this.layoutViewCard1.Name = "layoutViewCard1";
			this.layoutViewCard1.OptionsItemText.TextToControlDistance = 2;
			this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.layoutViewCard1.Text = "TemplateCard";
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ShowShadow = false;
			this.toolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController_GetActiveObjectInfo);
			// 
			// pbSelectedWidget
			// 
			this.pbSelectedWidget.BackColor = System.Drawing.Color.White;
			this.pbSelectedWidget.ForeColor = System.Drawing.Color.Black;
			this.pbSelectedWidget.Location = new System.Drawing.Point(129, 13);
			this.pbSelectedWidget.Name = "pbSelectedWidget";
			this.pbSelectedWidget.Size = new System.Drawing.Size(36, 36);
			this.pbSelectedWidget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbSelectedWidget.TabIndex = 3;
			this.pbSelectedWidget.TabStop = false;
			// 
			// laAvailableWidgets
			// 
			this.laAvailableWidgets.AutoSize = true;
			this.laAvailableWidgets.BackColor = System.Drawing.Color.White;
			this.laAvailableWidgets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvailableWidgets.ForeColor = System.Drawing.Color.Black;
			this.laAvailableWidgets.Location = new System.Drawing.Point(6, 60);
			this.laAvailableWidgets.Name = "laAvailableWidgets";
			this.laAvailableWidgets.Size = new System.Drawing.Size(254, 16);
			this.laAvailableWidgets.TabIndex = 2;
			this.laAvailableWidgets.Text = "Click on image below to select widget:";
			// 
			// laSelectedWidget
			// 
			this.laSelectedWidget.AutoSize = true;
			this.laSelectedWidget.BackColor = System.Drawing.Color.White;
			this.laSelectedWidget.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSelectedWidget.ForeColor = System.Drawing.Color.Black;
			this.laSelectedWidget.Location = new System.Drawing.Point(6, 23);
			this.laSelectedWidget.Name = "laSelectedWidget";
			this.laSelectedWidget.Size = new System.Drawing.Size(117, 16);
			this.laSelectedWidget.TabIndex = 0;
			this.laSelectedWidget.Text = "Selected Widget:";
			// 
			// FormSelectWidget
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(365, 333);
			this.Controls.Add(this.groupBoxWidgets);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectWidget";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Widget";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBoxWidgets)).EndInit();
			this.groupBoxWidgets.ResumeLayout(false);
			this.groupBoxWidgets.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlWidgets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewWidgets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedWidget)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.GroupControl groupBoxWidgets;
		private DevExpress.XtraGrid.GridControl gridControlWidgets;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewWidgets;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn gridColumnImage;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_gridColumnImage;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
		public System.Windows.Forms.PictureBox pbSelectedWidget;
		private System.Windows.Forms.Label laAvailableWidgets;
		private System.Windows.Forms.Label laSelectedWidget;
		private DevExpress.Utils.ToolTipController toolTipController;
	}
}