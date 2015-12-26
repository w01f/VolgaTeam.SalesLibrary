namespace SalesLibraries.SiteManager.PresentationClasses.InactiveUsers
{
	sealed partial class InactiveUsersManagerControl
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
			this.gridControlRecords = new DevExpress.XtraGrid.GridControl();
			this.gridViewRecords = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnUsersSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEditUsers = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumnUsersName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersGroup = new DevExpress.XtraGrid.Columns.GridColumn();
			this.splitContainerControlMain = new DevExpress.XtraEditors.SplitContainerControl();
			this.splitContainerControlData = new DevExpress.XtraEditors.SplitContainerControl();
			this.pnCustomFilter = new System.Windows.Forms.Panel();
			this.pnFilterButtons = new System.Windows.Forms.Panel();
			this.buttonXLoadData = new DevComponents.DotNetBar.ButtonX();
			this.gbDate = new System.Windows.Forms.GroupBox();
			this.labelControlDateEnd = new DevExpress.XtraEditors.LabelControl();
			this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
			this.labelControlDateStart = new DevExpress.XtraEditors.LabelControl();
			this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
			this.xtraTabControlEmail = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageEmailReset = new DevExpress.XtraTab.XtraTabPage();
			this.panelEmailReset = new System.Windows.Forms.Panel();
			this.buttonXEmailResetSend = new DevComponents.DotNetBar.ButtonX();
			this.labelControlEmailResetUserCount = new DevExpress.XtraEditors.LabelControl();
			this.memoEditEmailResetBody = new DevExpress.XtraEditors.MemoEdit();
			this.labelControlEmailResetBody = new DevExpress.XtraEditors.LabelControl();
			this.textEditEmailResetSubject = new DevExpress.XtraEditors.TextEdit();
			this.labelControlEmailResetSubject = new DevExpress.XtraEditors.LabelControl();
			this.textEditEmailResetSender = new DevExpress.XtraEditors.TextEdit();
			this.labelControlEmailResetSender = new DevExpress.XtraEditors.LabelControl();
			this.xtraTabPageEmailDelete = new DevExpress.XtraTab.XtraTabPage();
			this.panelEmailDelete = new System.Windows.Forms.Panel();
			this.buttonXEmailDeleteSend = new DevComponents.DotNetBar.ButtonX();
			this.labelControlEmailDeleteUserCount = new DevExpress.XtraEditors.LabelControl();
			this.memoEditEmailDeleteBody = new DevExpress.XtraEditors.MemoEdit();
			this.labelControlEmailDeleteBody = new DevExpress.XtraEditors.LabelControl();
			this.textEditEmailDeleteSubject = new DevExpress.XtraEditors.TextEdit();
			this.labelControlEmailDeleteSubject = new DevExpress.XtraEditors.LabelControl();
			this.textEditEmailDeleteSender = new DevExpress.XtraEditors.TextEdit();
			this.labelControlEmailDeleteSender = new DevExpress.XtraEditors.LabelControl();
			this.printingSystem = new DevExpress.XtraPrinting.PrintingSystem(this.components);
			this.printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlMain)).BeginInit();
			this.splitContainerControlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).BeginInit();
			this.splitContainerControlData.SuspendLayout();
			this.pnFilterButtons.SuspendLayout();
			this.gbDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlEmail)).BeginInit();
			this.xtraTabControlEmail.SuspendLayout();
			this.xtraTabPageEmailReset.SuspendLayout();
			this.panelEmailReset.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditEmailResetBody.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailResetSubject.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailResetSender.Properties)).BeginInit();
			this.xtraTabPageEmailDelete.SuspendLayout();
			this.panelEmailDelete.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditEmailDeleteBody.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailDeleteSubject.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailDeleteSender.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
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
			// gridControlRecords
			// 
			this.gridControlRecords.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlRecords.Location = new System.Drawing.Point(0, 0);
			this.gridControlRecords.MainView = this.gridViewRecords;
			this.gridControlRecords.Name = "gridControlRecords";
			this.gridControlRecords.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditUsers});
			this.gridControlRecords.Size = new System.Drawing.Size(257, 483);
			this.gridControlRecords.TabIndex = 3;
			this.gridControlRecords.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRecords});
			// 
			// gridViewRecords
			// 
			this.gridViewRecords.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewRecords.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewRecords.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewRecords.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.OddRow.Options.UseFont = true;
			this.gridViewRecords.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.Preview.Options.UseFont = true;
			this.gridViewRecords.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.Row.Options.UseFont = true;
			this.gridViewRecords.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewRecords.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewRecords.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnUsersSelected,
            this.gridColumnUsersName,
            this.gridColumnUsersGroup});
			this.gridViewRecords.GridControl = this.gridControlRecords;
			this.gridViewRecords.Name = "gridViewRecords";
			this.gridViewRecords.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewRecords.OptionsCustomization.AllowFilter = false;
			this.gridViewRecords.OptionsCustomization.AllowGroup = false;
			this.gridViewRecords.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewRecords.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewRecords.OptionsView.AutoCalcPreviewLineCount = true;
			this.gridViewRecords.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewRecords.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewRecords.OptionsView.ShowDetailButtons = false;
			this.gridViewRecords.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewRecords.OptionsView.ShowGroupPanel = false;
			this.gridViewRecords.OptionsView.ShowIndicator = false;
			this.gridViewRecords.PreviewFieldName = "DetailString";
			this.gridViewRecords.PreviewIndent = 5;
			this.gridViewRecords.RowHeight = 35;
			this.gridViewRecords.RowSeparatorHeight = 5;
			this.gridViewRecords.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewRecords_CellValueChanged);
			// 
			// gridColumnUsersSelected
			// 
			this.gridColumnUsersSelected.Caption = "Selected";
			this.gridColumnUsersSelected.ColumnEdit = this.repositoryItemCheckEditUsers;
			this.gridColumnUsersSelected.FieldName = "Selected";
			this.gridColumnUsersSelected.Name = "gridColumnUsersSelected";
			this.gridColumnUsersSelected.OptionsColumn.FixedWidth = true;
			this.gridColumnUsersSelected.OptionsColumn.ShowCaption = false;
			this.gridColumnUsersSelected.Visible = true;
			this.gridColumnUsersSelected.VisibleIndex = 0;
			this.gridColumnUsersSelected.Width = 42;
			// 
			// repositoryItemCheckEditUsers
			// 
			this.repositoryItemCheckEditUsers.AutoHeight = false;
			this.repositoryItemCheckEditUsers.Caption = "Check";
			this.repositoryItemCheckEditUsers.Name = "repositoryItemCheckEditUsers";
			this.repositoryItemCheckEditUsers.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEditUsers_CheckedChanged);
			// 
			// gridColumnUsersName
			// 
			this.gridColumnUsersName.Caption = "Name";
			this.gridColumnUsersName.FieldName = "FullName";
			this.gridColumnUsersName.Name = "gridColumnUsersName";
			this.gridColumnUsersName.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersName.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersName.Visible = true;
			this.gridColumnUsersName.VisibleIndex = 1;
			this.gridColumnUsersName.Width = 176;
			// 
			// gridColumnUsersGroup
			// 
			this.gridColumnUsersGroup.Caption = "Group";
			this.gridColumnUsersGroup.FieldName = "groupNames";
			this.gridColumnUsersGroup.Name = "gridColumnUsersGroup";
			this.gridColumnUsersGroup.Visible = true;
			this.gridColumnUsersGroup.VisibleIndex = 2;
			this.gridColumnUsersGroup.Width = 160;
			// 
			// splitContainerControlMain
			// 
			this.splitContainerControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControlMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlMain.Name = "splitContainerControlMain";
			this.splitContainerControlMain.Panel1.Controls.Add(this.splitContainerControlData);
			this.splitContainerControlMain.Panel1.Text = "Panel1";
			this.splitContainerControlMain.Panel2.Controls.Add(this.xtraTabControlEmail);
			this.splitContainerControlMain.Panel2.MinSize = 380;
			this.splitContainerControlMain.Panel2.Text = "Panel2";
			this.splitContainerControlMain.Size = new System.Drawing.Size(911, 483);
			this.splitContainerControlMain.TabIndex = 4;
			this.splitContainerControlMain.Text = "splitContainerControl1";
			// 
			// splitContainerControlData
			// 
			this.splitContainerControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControlData.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControlData.Name = "splitContainerControlData";
			this.splitContainerControlData.Panel1.Controls.Add(this.pnCustomFilter);
			this.splitContainerControlData.Panel1.Controls.Add(this.pnFilterButtons);
			this.splitContainerControlData.Panel1.Controls.Add(this.gbDate);
			this.splitContainerControlData.Panel1.MinSize = 250;
			this.splitContainerControlData.Panel1.Text = "Panel1";
			this.splitContainerControlData.Panel2.Controls.Add(this.gridControlRecords);
			this.splitContainerControlData.Panel2.Text = "Panel2";
			this.splitContainerControlData.Size = new System.Drawing.Size(519, 483);
			this.splitContainerControlData.TabIndex = 0;
			this.splitContainerControlData.Text = "splitContainerControl1";
			// 
			// pnCustomFilter
			// 
			this.pnCustomFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnCustomFilter.Location = new System.Drawing.Point(0, 137);
			this.pnCustomFilter.Name = "pnCustomFilter";
			this.pnCustomFilter.Size = new System.Drawing.Size(250, 346);
			this.pnCustomFilter.TabIndex = 21;
			// 
			// pnFilterButtons
			// 
			this.pnFilterButtons.Controls.Add(this.buttonXLoadData);
			this.pnFilterButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnFilterButtons.Location = new System.Drawing.Point(0, 98);
			this.pnFilterButtons.Name = "pnFilterButtons";
			this.pnFilterButtons.Size = new System.Drawing.Size(250, 39);
			this.pnFilterButtons.TabIndex = 22;
			// 
			// buttonXLoadData
			// 
			this.buttonXLoadData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLoadData.CausesValidation = false;
			this.buttonXLoadData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLoadData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLoadData.Location = new System.Drawing.Point(7, 5);
			this.buttonXLoadData.Name = "buttonXLoadData";
			this.buttonXLoadData.Size = new System.Drawing.Size(236, 27);
			this.buttonXLoadData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLoadData.TabIndex = 16;
			this.buttonXLoadData.Text = "Load Data";
			this.buttonXLoadData.TextColor = System.Drawing.Color.Black;
			this.buttonXLoadData.Click += new System.EventHandler(this.buttonXLoadData_Click);
			// 
			// gbDate
			// 
			this.gbDate.Controls.Add(this.labelControlDateEnd);
			this.gbDate.Controls.Add(this.dateEditEnd);
			this.gbDate.Controls.Add(this.labelControlDateStart);
			this.gbDate.Controls.Add(this.dateEditStart);
			this.gbDate.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbDate.Location = new System.Drawing.Point(0, 0);
			this.gbDate.Name = "gbDate";
			this.gbDate.Size = new System.Drawing.Size(250, 98);
			this.gbDate.TabIndex = 20;
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
			// dateEditEnd
			// 
			this.dateEditEnd.EditValue = null;
			this.dateEditEnd.Location = new System.Drawing.Point(100, 60);
			this.dateEditEnd.Name = "dateEditEnd";
			this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditEnd.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
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
			this.dateEditStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditStart.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Size = new System.Drawing.Size(132, 22);
			this.dateEditStart.StyleController = this.styleController;
			this.dateEditStart.TabIndex = 0;
			// 
			// xtraTabControlEmail
			// 
			this.xtraTabControlEmail.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlEmail.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlEmail.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlEmail.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlEmail.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEmail.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlEmail.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEmail.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlEmail.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEmail.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlEmail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlEmail.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlEmail.Name = "xtraTabControlEmail";
			this.xtraTabControlEmail.SelectedTabPage = this.xtraTabPageEmailReset;
			this.xtraTabControlEmail.Size = new System.Drawing.Size(380, 483);
			this.xtraTabControlEmail.TabIndex = 0;
			this.xtraTabControlEmail.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageEmailReset,
            this.xtraTabPageEmailDelete});
			// 
			// xtraTabPageEmailReset
			// 
			this.xtraTabPageEmailReset.Controls.Add(this.panelEmailReset);
			this.xtraTabPageEmailReset.Name = "xtraTabPageEmailReset";
			this.xtraTabPageEmailReset.Size = new System.Drawing.Size(378, 455);
			this.xtraTabPageEmailReset.Text = "Password Reset Email";
			// 
			// panelEmailReset
			// 
			this.panelEmailReset.Controls.Add(this.buttonXEmailResetSend);
			this.panelEmailReset.Controls.Add(this.labelControlEmailResetUserCount);
			this.panelEmailReset.Controls.Add(this.memoEditEmailResetBody);
			this.panelEmailReset.Controls.Add(this.labelControlEmailResetBody);
			this.panelEmailReset.Controls.Add(this.textEditEmailResetSubject);
			this.panelEmailReset.Controls.Add(this.labelControlEmailResetSubject);
			this.panelEmailReset.Controls.Add(this.textEditEmailResetSender);
			this.panelEmailReset.Controls.Add(this.labelControlEmailResetSender);
			this.panelEmailReset.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEmailReset.Location = new System.Drawing.Point(0, 0);
			this.panelEmailReset.Name = "panelEmailReset";
			this.panelEmailReset.Size = new System.Drawing.Size(378, 455);
			this.panelEmailReset.TabIndex = 0;
			// 
			// buttonXEmailResetSend
			// 
			this.buttonXEmailResetSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmailResetSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXEmailResetSend.CausesValidation = false;
			this.buttonXEmailResetSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmailResetSend.Location = new System.Drawing.Point(8, 405);
			this.buttonXEmailResetSend.Name = "buttonXEmailResetSend";
			this.buttonXEmailResetSend.Size = new System.Drawing.Size(364, 38);
			this.buttonXEmailResetSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmailResetSend.TabIndex = 17;
			this.buttonXEmailResetSend.Text = "Send Email";
			this.buttonXEmailResetSend.TextColor = System.Drawing.Color.Black;
			this.buttonXEmailResetSend.Click += new System.EventHandler(this.buttonXEmailResetSend_Click);
			// 
			// labelControlEmailResetUserCount
			// 
			this.labelControlEmailResetUserCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelControlEmailResetUserCount.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlEmailResetUserCount.Location = new System.Drawing.Point(8, 380);
			this.labelControlEmailResetUserCount.Name = "labelControlEmailResetUserCount";
			this.labelControlEmailResetUserCount.Size = new System.Drawing.Size(321, 16);
			this.labelControlEmailResetUserCount.StyleController = this.styleController;
			this.labelControlEmailResetUserCount.TabIndex = 8;
			this.labelControlEmailResetUserCount.Text = "Email will not be sent. There are no selected users";
			// 
			// memoEditEmailResetBody
			// 
			this.memoEditEmailResetBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditEmailResetBody.Location = new System.Drawing.Point(8, 166);
			this.memoEditEmailResetBody.Name = "memoEditEmailResetBody";
			this.memoEditEmailResetBody.Size = new System.Drawing.Size(364, 197);
			this.memoEditEmailResetBody.StyleController = this.styleController;
			this.memoEditEmailResetBody.TabIndex = 7;
			// 
			// labelControlEmailResetBody
			// 
			this.labelControlEmailResetBody.Location = new System.Drawing.Point(8, 144);
			this.labelControlEmailResetBody.Name = "labelControlEmailResetBody";
			this.labelControlEmailResetBody.Size = new System.Drawing.Size(34, 16);
			this.labelControlEmailResetBody.StyleController = this.styleController;
			this.labelControlEmailResetBody.TabIndex = 6;
			this.labelControlEmailResetBody.Text = "Body:";
			// 
			// textEditEmailResetSubject
			// 
			this.textEditEmailResetSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditEmailResetSubject.Location = new System.Drawing.Point(8, 94);
			this.textEditEmailResetSubject.Name = "textEditEmailResetSubject";
			this.textEditEmailResetSubject.Size = new System.Drawing.Size(364, 22);
			this.textEditEmailResetSubject.StyleController = this.styleController;
			this.textEditEmailResetSubject.TabIndex = 5;
			// 
			// labelControlEmailResetSubject
			// 
			this.labelControlEmailResetSubject.Location = new System.Drawing.Point(8, 72);
			this.labelControlEmailResetSubject.Name = "labelControlEmailResetSubject";
			this.labelControlEmailResetSubject.Size = new System.Drawing.Size(93, 16);
			this.labelControlEmailResetSubject.StyleController = this.styleController;
			this.labelControlEmailResetSubject.TabIndex = 4;
			this.labelControlEmailResetSubject.Text = "Subject Header:";
			// 
			// textEditEmailResetSender
			// 
			this.textEditEmailResetSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditEmailResetSender.Location = new System.Drawing.Point(8, 28);
			this.textEditEmailResetSender.Name = "textEditEmailResetSender";
			this.textEditEmailResetSender.Size = new System.Drawing.Size(364, 22);
			this.textEditEmailResetSender.StyleController = this.styleController;
			this.textEditEmailResetSender.TabIndex = 3;
			// 
			// labelControlEmailResetSender
			// 
			this.labelControlEmailResetSender.Location = new System.Drawing.Point(8, 6);
			this.labelControlEmailResetSender.Name = "labelControlEmailResetSender";
			this.labelControlEmailResetSender.Size = new System.Drawing.Size(102, 16);
			this.labelControlEmailResetSender.StyleController = this.styleController;
			this.labelControlEmailResetSender.TabIndex = 2;
			this.labelControlEmailResetSender.Text = "Email Sent From:";
			// 
			// xtraTabPageEmailDelete
			// 
			this.xtraTabPageEmailDelete.Controls.Add(this.panelEmailDelete);
			this.xtraTabPageEmailDelete.Name = "xtraTabPageEmailDelete";
			this.xtraTabPageEmailDelete.PageEnabled = false;
			this.xtraTabPageEmailDelete.Size = new System.Drawing.Size(378, 455);
			this.xtraTabPageEmailDelete.Text = "Account Termination Email";
			// 
			// panelEmailDelete
			// 
			this.panelEmailDelete.Controls.Add(this.buttonXEmailDeleteSend);
			this.panelEmailDelete.Controls.Add(this.labelControlEmailDeleteUserCount);
			this.panelEmailDelete.Controls.Add(this.memoEditEmailDeleteBody);
			this.panelEmailDelete.Controls.Add(this.labelControlEmailDeleteBody);
			this.panelEmailDelete.Controls.Add(this.textEditEmailDeleteSubject);
			this.panelEmailDelete.Controls.Add(this.labelControlEmailDeleteSubject);
			this.panelEmailDelete.Controls.Add(this.textEditEmailDeleteSender);
			this.panelEmailDelete.Controls.Add(this.labelControlEmailDeleteSender);
			this.panelEmailDelete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEmailDelete.Location = new System.Drawing.Point(0, 0);
			this.panelEmailDelete.Name = "panelEmailDelete";
			this.panelEmailDelete.Size = new System.Drawing.Size(378, 455);
			this.panelEmailDelete.TabIndex = 1;
			// 
			// buttonXEmailDeleteSend
			// 
			this.buttonXEmailDeleteSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmailDeleteSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXEmailDeleteSend.CausesValidation = false;
			this.buttonXEmailDeleteSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmailDeleteSend.Location = new System.Drawing.Point(8, 405);
			this.buttonXEmailDeleteSend.Name = "buttonXEmailDeleteSend";
			this.buttonXEmailDeleteSend.Size = new System.Drawing.Size(364, 38);
			this.buttonXEmailDeleteSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmailDeleteSend.TabIndex = 17;
			this.buttonXEmailDeleteSend.Text = "Send Email";
			this.buttonXEmailDeleteSend.TextColor = System.Drawing.Color.Black;
			this.buttonXEmailDeleteSend.Click += new System.EventHandler(this.buttonXEmailDeleteSend_Click);
			// 
			// labelControlEmailDeleteUserCount
			// 
			this.labelControlEmailDeleteUserCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelControlEmailDeleteUserCount.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlEmailDeleteUserCount.Location = new System.Drawing.Point(8, 380);
			this.labelControlEmailDeleteUserCount.Name = "labelControlEmailDeleteUserCount";
			this.labelControlEmailDeleteUserCount.Size = new System.Drawing.Size(321, 16);
			this.labelControlEmailDeleteUserCount.StyleController = this.styleController;
			this.labelControlEmailDeleteUserCount.TabIndex = 8;
			this.labelControlEmailDeleteUserCount.Text = "Email will not be sent. There are no selected users";
			// 
			// memoEditEmailDeleteBody
			// 
			this.memoEditEmailDeleteBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditEmailDeleteBody.Location = new System.Drawing.Point(8, 166);
			this.memoEditEmailDeleteBody.Name = "memoEditEmailDeleteBody";
			this.memoEditEmailDeleteBody.Size = new System.Drawing.Size(364, 197);
			this.memoEditEmailDeleteBody.StyleController = this.styleController;
			this.memoEditEmailDeleteBody.TabIndex = 7;
			// 
			// labelControlEmailDeleteBody
			// 
			this.labelControlEmailDeleteBody.Location = new System.Drawing.Point(8, 144);
			this.labelControlEmailDeleteBody.Name = "labelControlEmailDeleteBody";
			this.labelControlEmailDeleteBody.Size = new System.Drawing.Size(34, 16);
			this.labelControlEmailDeleteBody.StyleController = this.styleController;
			this.labelControlEmailDeleteBody.TabIndex = 6;
			this.labelControlEmailDeleteBody.Text = "Body:";
			// 
			// textEditEmailDeleteSubject
			// 
			this.textEditEmailDeleteSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditEmailDeleteSubject.Location = new System.Drawing.Point(8, 94);
			this.textEditEmailDeleteSubject.Name = "textEditEmailDeleteSubject";
			this.textEditEmailDeleteSubject.Size = new System.Drawing.Size(364, 22);
			this.textEditEmailDeleteSubject.StyleController = this.styleController;
			this.textEditEmailDeleteSubject.TabIndex = 5;
			// 
			// labelControlEmailDeleteSubject
			// 
			this.labelControlEmailDeleteSubject.Location = new System.Drawing.Point(8, 72);
			this.labelControlEmailDeleteSubject.Name = "labelControlEmailDeleteSubject";
			this.labelControlEmailDeleteSubject.Size = new System.Drawing.Size(93, 16);
			this.labelControlEmailDeleteSubject.StyleController = this.styleController;
			this.labelControlEmailDeleteSubject.TabIndex = 4;
			this.labelControlEmailDeleteSubject.Text = "Subject Header:";
			// 
			// textEditEmailDeleteSender
			// 
			this.textEditEmailDeleteSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textEditEmailDeleteSender.Location = new System.Drawing.Point(8, 28);
			this.textEditEmailDeleteSender.Name = "textEditEmailDeleteSender";
			this.textEditEmailDeleteSender.Size = new System.Drawing.Size(364, 22);
			this.textEditEmailDeleteSender.StyleController = this.styleController;
			this.textEditEmailDeleteSender.TabIndex = 3;
			// 
			// labelControlEmailDeleteSender
			// 
			this.labelControlEmailDeleteSender.Location = new System.Drawing.Point(8, 6);
			this.labelControlEmailDeleteSender.Name = "labelControlEmailDeleteSender";
			this.labelControlEmailDeleteSender.Size = new System.Drawing.Size(102, 16);
			this.labelControlEmailDeleteSender.StyleController = this.styleController;
			this.labelControlEmailDeleteSender.TabIndex = 2;
			this.labelControlEmailDeleteSender.Text = "Email Sent From:";
			// 
			// printingSystem
			// 
			this.printingSystem.Links.AddRange(new object[] {
            this.printableComponentLink});
			// 
			// printableComponentLink
			// 
			this.printableComponentLink.Component = this.gridControlRecords;
			this.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
			this.printableComponentLink.PrintingSystemBase = this.printingSystem;
			this.printableComponentLink.CreateReportHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.printableComponentLink_CreateReportHeaderArea);
			// 
			// InactiveUsersManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.splitContainerControlMain);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "InactiveUsersManagerControl";
			this.Size = new System.Drawing.Size(911, 483);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlMain)).EndInit();
			this.splitContainerControlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).EndInit();
			this.splitContainerControlData.ResumeLayout(false);
			this.pnFilterButtons.ResumeLayout(false);
			this.gbDate.ResumeLayout(false);
			this.gbDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlEmail)).EndInit();
			this.xtraTabControlEmail.ResumeLayout(false);
			this.xtraTabPageEmailReset.ResumeLayout(false);
			this.panelEmailReset.ResumeLayout(false);
			this.panelEmailReset.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditEmailResetBody.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailResetSubject.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailResetSender.Properties)).EndInit();
			this.xtraTabPageEmailDelete.ResumeLayout(false);
			this.panelEmailDelete.ResumeLayout(false);
			this.panelEmailDelete.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditEmailDeleteBody.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailDeleteSubject.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditEmailDeleteSender.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraGrid.GridControl gridControlRecords;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewRecords;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersSelected;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersName;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlMain;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControlData;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditUsers;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersGroup;
		private System.Windows.Forms.Panel pnCustomFilter;
		private System.Windows.Forms.Panel pnFilterButtons;
		private DevComponents.DotNetBar.ButtonX buttonXLoadData;
		private System.Windows.Forms.GroupBox gbDate;
		private DevExpress.XtraEditors.LabelControl labelControlDateEnd;
		private DevExpress.XtraEditors.DateEdit dateEditEnd;
		private DevExpress.XtraEditors.LabelControl labelControlDateStart;
		private DevExpress.XtraEditors.DateEdit dateEditStart;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlEmail;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageEmailReset;
		private System.Windows.Forms.Panel panelEmailReset;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageEmailDelete;
		private DevExpress.XtraEditors.TextEdit textEditEmailResetSubject;
		private DevExpress.XtraEditors.LabelControl labelControlEmailResetSubject;
		private DevExpress.XtraEditors.TextEdit textEditEmailResetSender;
		private DevExpress.XtraEditors.LabelControl labelControlEmailResetSender;
		private DevComponents.DotNetBar.ButtonX buttonXEmailResetSend;
		private DevExpress.XtraEditors.LabelControl labelControlEmailResetUserCount;
		private DevExpress.XtraEditors.MemoEdit memoEditEmailResetBody;
		private DevExpress.XtraEditors.LabelControl labelControlEmailResetBody;
		private System.Windows.Forms.Panel panelEmailDelete;
		private DevComponents.DotNetBar.ButtonX buttonXEmailDeleteSend;
		private DevExpress.XtraEditors.LabelControl labelControlEmailDeleteUserCount;
		private DevExpress.XtraEditors.MemoEdit memoEditEmailDeleteBody;
		private DevExpress.XtraEditors.LabelControl labelControlEmailDeleteBody;
		private DevExpress.XtraEditors.TextEdit textEditEmailDeleteSubject;
		private DevExpress.XtraEditors.LabelControl labelControlEmailDeleteSubject;
		private DevExpress.XtraEditors.TextEdit textEditEmailDeleteSender;
		private DevExpress.XtraEditors.LabelControl labelControlEmailDeleteSender;
		private DevExpress.XtraPrinting.PrintingSystem printingSystem;
		private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink;
    }
}
