namespace FileManager.PresentationClasses.IPad
{
    partial class IPadUsersControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IPadUsersControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.gridControlUsers = new DevExpress.XtraGrid.GridControl();
			this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnUsersFullName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersEmail = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnUsersActions = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditUsersActions = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditUsersActions)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlUsers
			// 
			this.gridControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlUsers.Location = new System.Drawing.Point(0, 0);
			this.gridControlUsers.MainView = this.gridViewUsers;
			this.gridControlUsers.Name = "gridControlUsers";
			this.gridControlUsers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditUsersActions});
			this.gridControlUsers.Size = new System.Drawing.Size(898, 483);
			this.gridControlUsers.TabIndex = 2;
			this.gridControlUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers});
			// 
			// gridViewUsers
			// 
			this.gridViewUsers.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewUsers.Appearance.EvenRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewUsers.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewUsers.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewUsers.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.OddRow.Options.UseFont = true;
			this.gridViewUsers.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.Preview.Options.UseFont = true;
			this.gridViewUsers.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.Row.Options.UseFont = true;
			this.gridViewUsers.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewUsers.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnUsersFullName,
            this.gridColumnUsersEmail,
            this.gridColumnUsersLogin,
            this.gridColumnUsersActions});
			this.gridViewUsers.GridControl = this.gridControlUsers;
			this.gridViewUsers.Name = "gridViewUsers";
			this.gridViewUsers.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewUsers.OptionsCustomization.AllowFilter = false;
			this.gridViewUsers.OptionsCustomization.AllowGroup = false;
			this.gridViewUsers.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewUsers.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewUsers.OptionsView.EnableAppearanceEvenRow = true;
			this.gridViewUsers.OptionsView.EnableAppearanceOddRow = true;
			this.gridViewUsers.OptionsView.ShowDetailButtons = false;
			this.gridViewUsers.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewUsers.OptionsView.ShowGroupPanel = false;
			this.gridViewUsers.OptionsView.ShowIndicator = false;
			this.gridViewUsers.OptionsView.ShowPreview = true;
			this.gridViewUsers.PreviewFieldName = "AssignedLibraries";
			this.gridViewUsers.PreviewIndent = 5;
			this.gridViewUsers.RowHeight = 35;
			this.gridViewUsers.RowSeparatorHeight = 5;
			this.gridViewUsers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewUsers_FocusedRowChanged);
			// 
			// gridColumnUsersFullName
			// 
			this.gridColumnUsersFullName.Caption = "User";
			this.gridColumnUsersFullName.FieldName = "FullName";
			this.gridColumnUsersFullName.Name = "gridColumnUsersFullName";
			this.gridColumnUsersFullName.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersFullName.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersFullName.Visible = true;
			this.gridColumnUsersFullName.VisibleIndex = 0;
			this.gridColumnUsersFullName.Width = 271;
			// 
			// gridColumnUsersEmail
			// 
			this.gridColumnUsersEmail.Caption = "Email";
			this.gridColumnUsersEmail.FieldName = "email";
			this.gridColumnUsersEmail.Name = "gridColumnUsersEmail";
			this.gridColumnUsersEmail.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersEmail.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersEmail.Visible = true;
			this.gridColumnUsersEmail.VisibleIndex = 1;
			this.gridColumnUsersEmail.Width = 271;
			// 
			// gridColumnUsersLogin
			// 
			this.gridColumnUsersLogin.Caption = "Login";
			this.gridColumnUsersLogin.FieldName = "login";
			this.gridColumnUsersLogin.Name = "gridColumnUsersLogin";
			this.gridColumnUsersLogin.OptionsColumn.AllowEdit = false;
			this.gridColumnUsersLogin.OptionsColumn.ReadOnly = true;
			this.gridColumnUsersLogin.Visible = true;
			this.gridColumnUsersLogin.VisibleIndex = 2;
			this.gridColumnUsersLogin.Width = 280;
			// 
			// gridColumnUsersActions
			// 
			this.gridColumnUsersActions.ColumnEdit = this.repositoryItemButtonEditUsersActions;
			this.gridColumnUsersActions.Name = "gridColumnUsersActions";
			this.gridColumnUsersActions.OptionsColumn.FixedWidth = true;
			this.gridColumnUsersActions.OptionsColumn.ShowCaption = false;
			this.gridColumnUsersActions.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnUsersActions.Visible = true;
			this.gridColumnUsersActions.VisibleIndex = 3;
			this.gridColumnUsersActions.Width = 80;
			// 
			// repositoryItemButtonEditUsersActions
			// 
			this.repositoryItemButtonEditUsersActions.AutoHeight = false;
			this.repositoryItemButtonEditUsersActions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditUsersActions.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditUsersActions.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEditUsersActions.Name = "repositoryItemButtonEditUsersActions";
			this.repositoryItemButtonEditUsersActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditUsersActions.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditUsersActions_ButtonClick);
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// IPadUsersControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.gridControlUsers);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "IPadUsersControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditUsersActions)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraGrid.GridControl gridControlUsers;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersFullName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersEmail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersLogin;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUsersActions;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditUsersActions;
    }
}
