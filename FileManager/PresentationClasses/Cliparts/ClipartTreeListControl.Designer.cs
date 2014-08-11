namespace FileManager.PresentationClasses.Cliparts
{
    partial class ClipartTreeListControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipartTreeListControl));
			this.treeListAllFiles = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumnPath = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.imageListFiles = new System.Windows.Forms.ImageList(this.components);
			this.pnTreeViewOptions = new System.Windows.Forms.Panel();
			this.buttonXRefresh = new DevComponents.DotNetBar.ButtonX();
			this.pnTreeViewProgress = new System.Windows.Forms.Panel();
			this.laTreeViewProgressLabel = new System.Windows.Forms.Label();
			this.circularProgressTreeView = new DevComponents.DotNetBar.Controls.CircularProgress();
			((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).BeginInit();
			this.pnTreeViewOptions.SuspendLayout();
			this.pnTreeViewProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeListAllFiles
			// 
			this.treeListAllFiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListAllFiles.Appearance.FocusedCell.Options.UseFont = true;
			this.treeListAllFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListAllFiles.Appearance.FocusedRow.Options.UseFont = true;
			this.treeListAllFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeListAllFiles.Appearance.Row.Options.UseFont = true;
			this.treeListAllFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.treeListAllFiles.Appearance.SelectedRow.Options.UseFont = true;
			this.treeListAllFiles.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.treeListColumnPath});
			this.treeListAllFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListAllFiles.Location = new System.Drawing.Point(0, 57);
			this.treeListAllFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.treeListAllFiles.Name = "treeListAllFiles";
			this.treeListAllFiles.OptionsBehavior.AutoChangeParent = false;
			this.treeListAllFiles.OptionsBehavior.Editable = false;
			this.treeListAllFiles.OptionsBehavior.ResizeNodes = false;
			this.treeListAllFiles.OptionsLayout.AddNewColumns = false;
			this.treeListAllFiles.OptionsMenu.EnableColumnMenu = false;
			this.treeListAllFiles.OptionsMenu.EnableFooterMenu = false;
			this.treeListAllFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListAllFiles.OptionsSelection.MultiSelect = true;
			this.treeListAllFiles.OptionsView.ShowColumns = false;
			this.treeListAllFiles.OptionsView.ShowFocusedFrame = false;
			this.treeListAllFiles.OptionsView.ShowHorzLines = false;
			this.treeListAllFiles.OptionsView.ShowIndicator = false;
			this.treeListAllFiles.OptionsView.ShowVertLines = false;
			this.treeListAllFiles.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
			this.treeListAllFiles.Size = new System.Drawing.Size(264, 518);
			this.treeListAllFiles.StateImageList = this.imageListFiles;
			this.treeListAllFiles.TabIndex = 1;
			this.treeListAllFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListAllFiles_MouseDoubleClick);
			this.treeListAllFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDown);
			this.treeListAllFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseMove);
			// 
			// treeListColumnName
			// 
			this.treeListColumnName.Caption = "treeListColumn1";
			this.treeListColumnName.FieldName = "treeListColumn1";
			this.treeListColumnName.MinWidth = 33;
			this.treeListColumnName.Name = "treeListColumnName";
			this.treeListColumnName.Visible = true;
			this.treeListColumnName.VisibleIndex = 0;
			// 
			// treeListColumnPath
			// 
			this.treeListColumnPath.Caption = "treeListColumn1";
			this.treeListColumnPath.FieldName = "treeListColumn1";
			this.treeListColumnPath.Name = "treeListColumnPath";
			// 
			// imageListFiles
			// 
			this.imageListFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFiles.ImageStream")));
			this.imageListFiles.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListFiles.Images.SetKeyName(0, "");
			this.imageListFiles.Images.SetKeyName(1, "");
			this.imageListFiles.Images.SetKeyName(2, "Image Files.png");
			// 
			// pnTreeViewOptions
			// 
			this.pnTreeViewOptions.Controls.Add(this.buttonXRefresh);
			this.pnTreeViewOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTreeViewOptions.Location = new System.Drawing.Point(0, 0);
			this.pnTreeViewOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pnTreeViewOptions.Name = "pnTreeViewOptions";
			this.pnTreeViewOptions.Size = new System.Drawing.Size(264, 57);
			this.pnTreeViewOptions.TabIndex = 6;
			// 
			// buttonXRefresh
			// 
			this.buttonXRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefresh.Location = new System.Drawing.Point(10, 13);
			this.buttonXRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonXRefresh.Name = "buttonXRefresh";
			this.buttonXRefresh.Size = new System.Drawing.Size(244, 30);
			this.buttonXRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefresh.TabIndex = 0;
			this.buttonXRefresh.Text = "Refresh";
			this.buttonXRefresh.Click += new System.EventHandler(this.Refresh_Click);
			// 
			// pnTreeViewProgress
			// 
			this.pnTreeViewProgress.Controls.Add(this.laTreeViewProgressLabel);
			this.pnTreeViewProgress.Controls.Add(this.circularProgressTreeView);
			this.pnTreeViewProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnTreeViewProgress.Location = new System.Drawing.Point(0, 575);
			this.pnTreeViewProgress.Name = "pnTreeViewProgress";
			this.pnTreeViewProgress.Padding = new System.Windows.Forms.Padding(5);
			this.pnTreeViewProgress.Size = new System.Drawing.Size(264, 40);
			this.pnTreeViewProgress.TabIndex = 13;
			// 
			// laTreeViewProgressLabel
			// 
			this.laTreeViewProgressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTreeViewProgressLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTreeViewProgressLabel.Location = new System.Drawing.Point(68, 5);
			this.laTreeViewProgressLabel.Name = "laTreeViewProgressLabel";
			this.laTreeViewProgressLabel.Size = new System.Drawing.Size(191, 30);
			this.laTreeViewProgressLabel.TabIndex = 0;
			this.laTreeViewProgressLabel.Text = "label1";
			this.laTreeViewProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// circularProgressTreeView
			// 
			this.circularProgressTreeView.AnimationSpeed = 50;
			// 
			// 
			// 
			this.circularProgressTreeView.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgressTreeView.FocusCuesEnabled = false;
			this.circularProgressTreeView.Location = new System.Drawing.Point(5, 5);
			this.circularProgressTreeView.Name = "circularProgressTreeView";
			this.circularProgressTreeView.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressTreeView.ProgressTextFormat = "";
			this.circularProgressTreeView.Size = new System.Drawing.Size(63, 30);
			this.circularProgressTreeView.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressTreeView.TabIndex = 1;
			// 
			// ClipartTreeListControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.treeListAllFiles);
			this.Controls.Add(this.pnTreeViewProgress);
			this.Controls.Add(this.pnTreeViewOptions);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ClipartTreeListControl";
			this.Size = new System.Drawing.Size(264, 615);
			((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).EndInit();
			this.pnTreeViewOptions.ResumeLayout(false);
			this.pnTreeViewProgress.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnTreeViewOptions;
        private System.Windows.Forms.ImageList imageListFiles;
        private DevExpress.XtraTreeList.TreeList treeListAllFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPath;
		private DevComponents.DotNetBar.ButtonX buttonXRefresh;
		private System.Windows.Forms.Panel pnTreeViewProgress;
		private System.Windows.Forms.Label laTreeViewProgressLabel;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressTreeView;
    }
}
