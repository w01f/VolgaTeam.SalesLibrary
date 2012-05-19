namespace SalesDepot.PresentationClasses.WallBin
{
    partial class ClassicViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassicViewControl));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
            this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnTop = new System.Windows.Forms.Panel();
            this.laTitle = new System.Windows.Forms.Label();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.buttonXPDF = new DevComponents.DotNetBar.ButtonX();
            this.buttonXZip = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEmptyEmailBin = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCreateEmail = new DevComponents.DotNetBar.ButtonX();
            this.pnSalesDepotContainer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
            this.pnTop.SuspendLayout();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Appearance.ForeColor = System.Drawing.Color.Black;
            this.splitContainerControl.Appearance.Options.UseForeColor = true;
            this.splitContainerControl.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.splitContainerControl.AppearanceCaption.Options.UseForeColor = true;
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.gridControlFiles);
            this.splitContainerControl.Panel1.Controls.Add(this.pnTop);
            this.splitContainerControl.Panel1.Controls.Add(this.pnBottom);
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.pnSalesDepotContainer);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            this.splitContainerControl.Size = new System.Drawing.Size(678, 434);
            this.splitContainerControl.SplitterPosition = 257;
            this.splitContainerControl.TabIndex = 4;
            this.splitContainerControl.Text = "splitContainerControl1";
            // 
            // gridControlFiles
            // 
            this.gridControlFiles.AllowDrop = true;
            this.gridControlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFiles.Location = new System.Drawing.Point(0, 50);
            this.gridControlFiles.MainView = this.gridViewFiles;
            this.gridControlFiles.Name = "gridControlFiles";
            this.gridControlFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit});
            this.gridControlFiles.Size = new System.Drawing.Size(0, 0);
            this.gridControlFiles.TabIndex = 3;
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
            this.gridColumnName.FieldName = "PropertiesName";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            // 
            // repositoryItemButtonEdit
            // 
            this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
            // 
            // pnTop
            // 
            this.pnTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnTop.Controls.Add(this.laTitle);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnTop.Size = new System.Drawing.Size(0, 50);
            this.pnTop.TabIndex = 2;
            // 
            // laTitle
            // 
            this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laTitle.Location = new System.Drawing.Point(10, 0);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(0, 50);
            this.laTitle.TabIndex = 4;
            this.laTitle.Text = "Right-Click on the file and drag and drop it here:";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnBottom
            // 
            this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnBottom.Controls.Add(this.buttonXPDF);
            this.pnBottom.Controls.Add(this.buttonXZip);
            this.pnBottom.Controls.Add(this.buttonXEmptyEmailBin);
            this.pnBottom.Controls.Add(this.buttonXCreateEmail);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.ForeColor = System.Drawing.Color.Black;
            this.pnBottom.Location = new System.Drawing.Point(0, -228);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.pnBottom.Size = new System.Drawing.Size(0, 228);
            this.pnBottom.TabIndex = 1;
            // 
            // buttonXPDF
            // 
            this.buttonXPDF.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPDF.AutoCheckOnClick = true;
            this.buttonXPDF.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPDF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXPDF.Image = global::SalesDepot.Properties.Resources.Acrobat;
            this.buttonXPDF.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.buttonXPDF.Location = new System.Drawing.Point(10, 9);
            this.buttonXPDF.Name = "buttonXPDF";
            this.buttonXPDF.Size = new System.Drawing.Size(233, 47);
            this.buttonXPDF.TabIndex = 5;
            this.buttonXPDF.Text = "   PowerPoint to PDF";
            this.buttonXPDF.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXPDF.TextColor = System.Drawing.Color.Black;
            this.buttonXPDF.CheckedChanged += new System.EventHandler(this.ckConvertPDF_CheckedChanged);
            // 
            // buttonXZip
            // 
            this.buttonXZip.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXZip.AutoCheckOnClick = true;
            this.buttonXZip.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXZip.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXZip.Image = global::SalesDepot.Properties.Resources.zip;
            this.buttonXZip.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.buttonXZip.Location = new System.Drawing.Point(10, 62);
            this.buttonXZip.Name = "buttonXZip";
            this.buttonXZip.Size = new System.Drawing.Size(233, 47);
            this.buttonXZip.TabIndex = 4;
            this.buttonXZip.Text = "   Zip Attachment";
            this.buttonXZip.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXZip.TextColor = System.Drawing.Color.Black;
            this.buttonXZip.CheckedChanged += new System.EventHandler(this.ckZip_CheckedChanged);
            // 
            // buttonXEmptyEmailBin
            // 
            this.buttonXEmptyEmailBin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEmptyEmailBin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEmptyEmailBin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXEmptyEmailBin.Image = global::SalesDepot.Properties.Resources.ClearSolutionView;
            this.buttonXEmptyEmailBin.ImageFixedSize = new System.Drawing.Size(40, 40);
            this.buttonXEmptyEmailBin.Location = new System.Drawing.Point(10, 115);
            this.buttonXEmptyEmailBin.Name = "buttonXEmptyEmailBin";
            this.buttonXEmptyEmailBin.Size = new System.Drawing.Size(233, 47);
            this.buttonXEmptyEmailBin.TabIndex = 3;
            this.buttonXEmptyEmailBin.Text = "   Empty Email Bin";
            this.buttonXEmptyEmailBin.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXEmptyEmailBin.TextColor = System.Drawing.Color.Black;
            this.buttonXEmptyEmailBin.Click += new System.EventHandler(this.buttonXEmptyEmailBin_Click);
            // 
            // buttonXCreateEmail
            // 
            this.buttonXCreateEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCreateEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCreateEmail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXCreateEmail.Image = global::SalesDepot.Properties.Resources.Send;
            this.buttonXCreateEmail.ImageFixedSize = new System.Drawing.Size(42, 40);
            this.buttonXCreateEmail.Location = new System.Drawing.Point(10, 168);
            this.buttonXCreateEmail.Name = "buttonXCreateEmail";
            this.buttonXCreateEmail.Size = new System.Drawing.Size(233, 47);
            this.buttonXCreateEmail.TabIndex = 0;
            this.buttonXCreateEmail.Text = "  Create Email";
            this.buttonXCreateEmail.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXCreateEmail.TextColor = System.Drawing.Color.Black;
            this.buttonXCreateEmail.Click += new System.EventHandler(this.buttonXCreateEmail_Click);
            // 
            // pnSalesDepotContainer
            // 
            this.pnSalesDepotContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSalesDepotContainer.Location = new System.Drawing.Point(0, 0);
            this.pnSalesDepotContainer.Name = "pnSalesDepotContainer";
            this.pnSalesDepotContainer.Size = new System.Drawing.Size(678, 434);
            this.pnSalesDepotContainer.TabIndex = 3;
            // 
            // ClassicViewControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.splitContainerControl);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ClassicViewControl";
            this.Size = new System.Drawing.Size(678, 434);
            this.Load += new System.EventHandler(this.ClassicViewControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
            this.pnTop.ResumeLayout(false);
            this.pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnSalesDepotContainer;
        public DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnBottom;
        private DevComponents.DotNetBar.ButtonX buttonXCreateEmail;
        private System.Windows.Forms.Label laTitle;
        private DevExpress.XtraGrid.GridControl gridControlFiles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevComponents.DotNetBar.ButtonX buttonXEmptyEmailBin;
        private DevComponents.DotNetBar.ButtonX buttonXPDF;
        private DevComponents.DotNetBar.ButtonX buttonXZip;

    }
}
