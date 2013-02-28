namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	sealed partial class ActivitiesManagerControl
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
			this.gridControlActivities = new DevExpress.XtraGrid.GridControl();
			this.gridViewActivities = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnActivitiesDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemDateEditActivitiesDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.gridColumnActivitiesLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnActivitiesType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnActivitiesSubType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.buttonXApplyFilter = new DevComponents.DotNetBar.ButtonX();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.gbDate = new System.Windows.Forms.GroupBox();
			this.labelControlDateEnd = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
			this.labelControlDateStart = new DevExpress.XtraEditors.LabelControl();
			this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlActivities)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewActivities)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditActivitiesDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditActivitiesDate.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.gbDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlActivities
			// 
			this.gridControlActivities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlActivities.Location = new System.Drawing.Point(0, 0);
			this.gridControlActivities.MainView = this.gridViewActivities;
			this.gridControlActivities.Name = "gridControlActivities";
			this.gridControlActivities.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditActivitiesDate});
			this.gridControlActivities.Size = new System.Drawing.Size(640, 483);
			this.gridControlActivities.TabIndex = 2;
			this.gridControlActivities.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewActivities});
			// 
			// gridViewActivities
			// 
			this.gridViewActivities.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewActivities.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewActivities.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewActivities.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewActivities.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewActivities.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.OddRow.Options.UseFont = true;
			this.gridViewActivities.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.Preview.Options.UseFont = true;
			this.gridViewActivities.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.Row.Options.UseFont = true;
			this.gridViewActivities.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewActivities.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewActivities.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnActivitiesDate,
            this.gridColumnActivitiesLogin,
            this.gridColumnActivitiesType,
            this.gridColumnActivitiesSubType});
			this.gridViewActivities.GridControl = this.gridControlActivities;
			this.gridViewActivities.Name = "gridViewActivities";
			this.gridViewActivities.OptionsBehavior.Editable = false;
			this.gridViewActivities.OptionsBehavior.ReadOnly = true;
			this.gridViewActivities.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewActivities.OptionsCustomization.AllowFilter = false;
			this.gridViewActivities.OptionsCustomization.AllowGroup = false;
			this.gridViewActivities.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewActivities.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewActivities.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewActivities.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewActivities.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewActivities.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewActivities.OptionsView.ShowDetailButtons = false;
			this.gridViewActivities.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewActivities.OptionsView.ShowGroupPanel = false;
			this.gridViewActivities.OptionsView.ShowIndicator = false;
			this.gridViewActivities.OptionsView.ShowPreview = true;
			this.gridViewActivities.PreviewFieldName = "Details";
			this.gridViewActivities.PreviewIndent = 5;
			this.gridViewActivities.RowHeight = 35;
			this.gridViewActivities.RowSeparatorHeight = 5;
			// 
			// gridColumnActivitiesDate
			// 
			this.gridColumnActivitiesDate.Caption = "Date\\Time";
			this.gridColumnActivitiesDate.ColumnEdit = this.repositoryItemDateEditActivitiesDate;
			this.gridColumnActivitiesDate.FieldName = "ActivityDate";
			this.gridColumnActivitiesDate.Name = "gridColumnActivitiesDate";
			this.gridColumnActivitiesDate.Visible = true;
			this.gridColumnActivitiesDate.VisibleIndex = 0;
			this.gridColumnActivitiesDate.Width = 99;
			// 
			// repositoryItemDateEditActivitiesDate
			// 
			this.repositoryItemDateEditActivitiesDate.AutoHeight = false;
			this.repositoryItemDateEditActivitiesDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemDateEditActivitiesDate.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditActivitiesDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditActivitiesDate.EditFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditActivitiesDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditActivitiesDate.Name = "repositoryItemDateEditActivitiesDate";
			this.repositoryItemDateEditActivitiesDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			// 
			// gridColumnActivitiesLogin
			// 
			this.gridColumnActivitiesLogin.Caption = "Login";
			this.gridColumnActivitiesLogin.FieldName = "login";
			this.gridColumnActivitiesLogin.Name = "gridColumnActivitiesLogin";
			this.gridColumnActivitiesLogin.Visible = true;
			this.gridColumnActivitiesLogin.VisibleIndex = 1;
			this.gridColumnActivitiesLogin.Width = 94;
			// 
			// gridColumnActivitiesType
			// 
			this.gridColumnActivitiesType.Caption = "Action Group";
			this.gridColumnActivitiesType.FieldName = "type";
			this.gridColumnActivitiesType.Name = "gridColumnActivitiesType";
			this.gridColumnActivitiesType.Visible = true;
			this.gridColumnActivitiesType.VisibleIndex = 2;
			this.gridColumnActivitiesType.Width = 146;
			// 
			// gridColumnActivitiesSubType
			// 
			this.gridColumnActivitiesSubType.Caption = "Action";
			this.gridColumnActivitiesSubType.FieldName = "subType";
			this.gridColumnActivitiesSubType.Name = "gridColumnActivitiesSubType";
			this.gridColumnActivitiesSubType.Visible = true;
			this.gridColumnActivitiesSubType.VisibleIndex = 3;
			this.gridColumnActivitiesSubType.Width = 297;
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
			// 
			// buttonXApplyFilter
			// 
			this.buttonXApplyFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXApplyFilter.CausesValidation = false;
			this.buttonXApplyFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXApplyFilter.Location = new System.Drawing.Point(14, 438);
			this.buttonXApplyFilter.Name = "buttonXApplyFilter";
			this.buttonXApplyFilter.Size = new System.Drawing.Size(222, 33);
			this.buttonXApplyFilter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXApplyFilter.TabIndex = 16;
			this.buttonXApplyFilter.Text = "Apply Filter";
			this.buttonXApplyFilter.TextColor = System.Drawing.Color.Black;
			this.buttonXApplyFilter.Click += new System.EventHandler(this.buttonXApplyFilter_Click);
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.gbDate);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXApplyFilter);
			this.splitContainerControl.Panel1.MinSize = 250;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.gridControlActivities);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(898, 483);
			this.splitContainerControl.SplitterPosition = 250;
			this.splitContainerControl.TabIndex = 17;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// gbDate
			// 
			this.gbDate.Controls.Add(this.labelControlDateEnd);
			this.gbDate.Controls.Add(this.dateEditEnd);
			this.gbDate.Controls.Add(this.labelControlDateStart);
			this.gbDate.Controls.Add(this.dateEditStart);
			this.gbDate.Location = new System.Drawing.Point(4, 8);
			this.gbDate.Name = "gbDate";
			this.gbDate.Size = new System.Drawing.Size(242, 98);
			this.gbDate.TabIndex = 17;
			this.gbDate.TabStop = false;
			this.gbDate.Text = "Date range";
			// 
			// labelControlDateEnd
			// 
			this.labelControlDateEnd.Location = new System.Drawing.Point(10, 63);
			this.labelControlDateEnd.Name = "labelControlDateEnd";
			this.labelControlDateEnd.Size = new System.Drawing.Size(58, 16);
			this.labelControlDateEnd.StyleController = this.styleController;
			this.labelControlDateEnd.TabIndex = 3;
			this.labelControlDateEnd.Text = "End Date:";
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
			// dateEditEnd
			// 
			this.dateEditEnd.EditValue = null;
			this.dateEditEnd.Location = new System.Drawing.Point(100, 60);
			this.dateEditEnd.Name = "dateEditEnd";
			this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditEnd.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditEnd.Size = new System.Drawing.Size(132, 22);
			this.dateEditEnd.StyleController = this.styleController;
			this.dateEditEnd.TabIndex = 2;
			// 
			// labelControlDateStart
			// 
			this.labelControlDateStart.Location = new System.Drawing.Point(10, 24);
			this.labelControlDateStart.Name = "labelControlDateStart";
			this.labelControlDateStart.Size = new System.Drawing.Size(63, 16);
			this.labelControlDateStart.StyleController = this.styleController;
			this.labelControlDateStart.TabIndex = 1;
			this.labelControlDateStart.Text = "Start Date:";
			// 
			// dateEditStart
			// 
			this.dateEditStart.EditValue = null;
			this.dateEditStart.Location = new System.Drawing.Point(100, 21);
			this.dateEditStart.Name = "dateEditStart";
			this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditStart.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditStart.Size = new System.Drawing.Size(132, 22);
			this.dateEditStart.StyleController = this.styleController;
			this.dateEditStart.TabIndex = 0;
			// 
			// ActivitiesManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ActivitiesManagerControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlActivities)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewActivities)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditActivitiesDate.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditActivitiesDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.gbDate.ResumeLayout(false);
			this.gbDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraGrid.GridControl gridControlActivities;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewActivities;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnActivitiesDate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnActivitiesLogin;
		private DevComponents.DotNetBar.ButtonX buttonXApplyFilter;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private System.Windows.Forms.GroupBox gbDate;
		private DevExpress.XtraEditors.DateEdit dateEditStart;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlDateEnd;
		private DevExpress.XtraEditors.DateEdit dateEditEnd;
		private DevExpress.XtraEditors.LabelControl labelControlDateStart;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditActivitiesDate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnActivitiesType;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnActivitiesSubType;
    }
}
