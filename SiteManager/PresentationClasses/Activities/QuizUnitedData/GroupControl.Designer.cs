namespace SalesDepot.SiteManager.PresentationClasses.Activities.QuizUnitedData
{
	public partial class GroupControl
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
			this.gridControlData = new DevExpress.XtraGrid.GridControl();
			this.gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnUser = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnQuiz = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTaken = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemSpinEditNumeric = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumnPassed = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemDateEditDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.repositoryItemSpinEditPercent = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlData
			// 
			this.gridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlData.Location = new System.Drawing.Point(0, 0);
			this.gridControlData.MainView = this.gridViewData;
			this.gridControlData.Name = "gridControlData";
			this.gridControlData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEditDate,
            this.repositoryItemSpinEditNumeric,
            this.repositoryItemSpinEditPercent});
			this.gridControlData.Size = new System.Drawing.Size(898, 483);
			this.gridControlData.TabIndex = 2;
			this.gridControlData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewData});
			// 
			// gridViewData
			// 
			this.gridViewData.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewData.Appearance.EvenRow.Options.UseTextOptions = true;
			this.gridViewData.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewData.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewData.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewData.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewData.Appearance.FooterPanel.Options.UseFont = true;
			this.gridViewData.Appearance.FooterPanel.Options.UseTextOptions = true;
			this.gridViewData.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewData.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.GroupButton.Options.UseFont = true;
			this.gridViewData.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewData.Appearance.GroupFooter.Options.UseFont = true;
			this.gridViewData.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.GroupPanel.Options.UseFont = true;
			this.gridViewData.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewData.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
			this.gridViewData.Appearance.GroupRow.Options.UseFont = true;
			this.gridViewData.Appearance.GroupRow.Options.UseForeColor = true;
			this.gridViewData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewData.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewData.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewData.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.OddRow.Options.UseFont = true;
			this.gridViewData.Appearance.OddRow.Options.UseTextOptions = true;
			this.gridViewData.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewData.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.Preview.Options.UseFont = true;
			this.gridViewData.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.Row.Options.UseFont = true;
			this.gridViewData.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewData.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewData.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.gridViewData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnUser,
            this.gridColumnQuiz,
            this.gridColumnTaken,
            this.gridColumnPassed});
			this.gridViewData.GridControl = this.gridControlData;
			this.gridViewData.GroupCount = 1;
			this.gridViewData.GroupFormat = "[#image]{1} {2}";
			this.gridViewData.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "quizTryCount", null, "{0:n0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "QuizPassDate", null, "{0:n0}")});
			this.gridViewData.Name = "gridViewData";
			this.gridViewData.OptionsBehavior.Editable = false;
			this.gridViewData.OptionsBehavior.ReadOnly = true;
			this.gridViewData.OptionsCustomization.AllowFilter = false;
			this.gridViewData.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewData.OptionsMenu.EnableColumnMenu = false;
			this.gridViewData.OptionsMenu.EnableFooterMenu = false;
			this.gridViewData.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewData.OptionsMenu.ShowAutoFilterRowItem = false;
			this.gridViewData.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewData.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewData.OptionsPrint.PrintPreview = true;
			this.gridViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewData.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewData.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewData.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewData.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewData.OptionsView.ShowDetailButtons = false;
			this.gridViewData.OptionsView.ShowFooter = true;
			this.gridViewData.OptionsView.ShowGroupPanel = false;
			this.gridViewData.OptionsView.ShowIndicator = false;
			this.gridViewData.PreviewIndent = 20;
			this.gridViewData.RowSeparatorHeight = 10;
			this.gridViewData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnQuiz, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnUser, DevExpress.Data.ColumnSortOrder.Ascending)});
			this.gridViewData.CustomDrawFooterCell += new DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventHandler(this.gridViewData_CustomDrawFooterCell);
			this.gridViewData.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridViewData_CustomDrawGroupRow);
			this.gridViewData.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gridViewData_CustomSummaryCalculate);
			this.gridViewData.EndGrouping += new System.EventHandler(this.advBandedGridViewData_EndGrouping);
			this.gridViewData.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridViewData_CustomColumnSort);
			// 
			// gridColumnUser
			// 
			this.gridColumnUser.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnUser.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnUser.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumnUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnUser.Caption = "User:";
			this.gridColumnUser.FieldName = "FullName";
			this.gridColumnUser.Name = "gridColumnUser";
			this.gridColumnUser.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
			this.gridColumnUser.SummaryItem.DisplayFormat = "Total Active Quizzes in the system: {0:n0}";
			this.gridColumnUser.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
			this.gridColumnUser.Visible = true;
			this.gridColumnUser.VisibleIndex = 0;
			this.gridColumnUser.Width = 639;
			// 
			// gridColumnQuiz
			// 
			this.gridColumnQuiz.Caption = "Quiz";
			this.gridColumnQuiz.FieldName = "quizName";
			this.gridColumnQuiz.FieldNameSortGroup = "Date";
			this.gridColumnQuiz.Name = "gridColumnQuiz";
			this.gridColumnQuiz.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
			this.gridColumnQuiz.Width = 167;
			// 
			// gridColumnTaken
			// 
			this.gridColumnTaken.Caption = "Taken:";
			this.gridColumnTaken.ColumnEdit = this.repositoryItemSpinEditNumeric;
			this.gridColumnTaken.FieldName = "quizTryCount";
			this.gridColumnTaken.Name = "gridColumnTaken";
			this.gridColumnTaken.OptionsColumn.FixedWidth = true;
			this.gridColumnTaken.SummaryItem.DisplayFormat = "Taken: {0:n0}";
			this.gridColumnTaken.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			this.gridColumnTaken.Visible = true;
			this.gridColumnTaken.VisibleIndex = 1;
			this.gridColumnTaken.Width = 110;
			// 
			// repositoryItemSpinEditNumeric
			// 
			this.repositoryItemSpinEditNumeric.AutoHeight = false;
			this.repositoryItemSpinEditNumeric.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditNumeric.DisplayFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditNumeric.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditNumeric.EditFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditNumeric.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditNumeric.Name = "repositoryItemSpinEditNumeric";
			this.repositoryItemSpinEditNumeric.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// gridColumnPassed
			// 
			this.gridColumnPassed.Caption = "Passed:";
			this.gridColumnPassed.ColumnEdit = this.repositoryItemDateEditDate;
			this.gridColumnPassed.FieldName = "QuizPassDate";
			this.gridColumnPassed.Name = "gridColumnPassed";
			this.gridColumnPassed.OptionsColumn.FixedWidth = true;
			this.gridColumnPassed.SummaryItem.DisplayFormat = "Passed: {0:n0}";
			this.gridColumnPassed.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
			this.gridColumnPassed.Visible = true;
			this.gridColumnPassed.VisibleIndex = 2;
			this.gridColumnPassed.Width = 150;
			// 
			// repositoryItemDateEditDate
			// 
			this.repositoryItemDateEditDate.AutoHeight = false;
			this.repositoryItemDateEditDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemDateEditDate.DisplayFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.EditFormat.FormatString = "MM/dd/yyyy hh:mm tt";
			this.repositoryItemDateEditDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemDateEditDate.Name = "repositoryItemDateEditDate";
			this.repositoryItemDateEditDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			// 
			// repositoryItemSpinEditPercent
			// 
			this.repositoryItemSpinEditPercent.AutoHeight = false;
			this.repositoryItemSpinEditPercent.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditPercent.DisplayFormat.FormatString = "#0.##%";
			this.repositoryItemSpinEditPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditPercent.EditFormat.FormatString = "#0.##%";
			this.repositoryItemSpinEditPercent.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditPercent.Name = "repositoryItemSpinEditPercent";
			this.repositoryItemSpinEditPercent.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// GroupControl
			// 
			this.Controls.Add(this.gridControlData);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEditDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPercent)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlData;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEditDate;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumeric;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPercent;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewData;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUser;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnQuiz;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTaken;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnPassed;
    }
}
