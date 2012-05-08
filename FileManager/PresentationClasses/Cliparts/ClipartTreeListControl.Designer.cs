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
            this.pnTreeViewProgress = new System.Windows.Forms.Panel();
            this.gbTreeViewProgress = new System.Windows.Forms.GroupBox();
            this.pgTreeViewProgress = new System.Windows.Forms.ProgressBar();
            this.laTreeViewProgressLable = new System.Windows.Forms.Label();
            this.pnTreeViewOptions = new System.Windows.Forms.Panel();
            this.gbTreeViewOptions = new System.Windows.Forms.GroupBox();
            this.btRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).BeginInit();
            this.pnTreeViewProgress.SuspendLayout();
            this.gbTreeViewProgress.SuspendLayout();
            this.pnTreeViewOptions.SuspendLayout();
            this.gbTreeViewOptions.SuspendLayout();
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
            this.treeListAllFiles.Location = new System.Drawing.Point(0, 59);
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
            this.treeListAllFiles.Size = new System.Drawing.Size(270, 369);
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
            // pnTreeViewProgress
            // 
            this.pnTreeViewProgress.Controls.Add(this.gbTreeViewProgress);
            this.pnTreeViewProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnTreeViewProgress.Location = new System.Drawing.Point(0, 428);
            this.pnTreeViewProgress.Name = "pnTreeViewProgress";
            this.pnTreeViewProgress.Size = new System.Drawing.Size(270, 72);
            this.pnTreeViewProgress.TabIndex = 5;
            // 
            // gbTreeViewProgress
            // 
            this.gbTreeViewProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTreeViewProgress.Controls.Add(this.pgTreeViewProgress);
            this.gbTreeViewProgress.Controls.Add(this.laTreeViewProgressLable);
            this.gbTreeViewProgress.Location = new System.Drawing.Point(6, 3);
            this.gbTreeViewProgress.Name = "gbTreeViewProgress";
            this.gbTreeViewProgress.Size = new System.Drawing.Size(258, 61);
            this.gbTreeViewProgress.TabIndex = 0;
            this.gbTreeViewProgress.TabStop = false;
            // 
            // pgTreeViewProgress
            // 
            this.pgTreeViewProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgTreeViewProgress.Location = new System.Drawing.Point(8, 34);
            this.pgTreeViewProgress.Name = "pgTreeViewProgress";
            this.pgTreeViewProgress.Size = new System.Drawing.Size(242, 19);
            this.pgTreeViewProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgTreeViewProgress.TabIndex = 1;
            // 
            // laTreeViewProgressLable
            // 
            this.laTreeViewProgressLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.laTreeViewProgressLable.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTreeViewProgressLable.Location = new System.Drawing.Point(8, 12);
            this.laTreeViewProgressLable.Name = "laTreeViewProgressLable";
            this.laTreeViewProgressLable.Size = new System.Drawing.Size(242, 19);
            this.laTreeViewProgressLable.TabIndex = 0;
            this.laTreeViewProgressLable.Text = "label1";
            // 
            // pnTreeViewOptions
            // 
            this.pnTreeViewOptions.Controls.Add(this.gbTreeViewOptions);
            this.pnTreeViewOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTreeViewOptions.Location = new System.Drawing.Point(0, 0);
            this.pnTreeViewOptions.Name = "pnTreeViewOptions";
            this.pnTreeViewOptions.Size = new System.Drawing.Size(270, 59);
            this.pnTreeViewOptions.TabIndex = 6;
            // 
            // gbTreeViewOptions
            // 
            this.gbTreeViewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTreeViewOptions.Controls.Add(this.btRefresh);
            this.gbTreeViewOptions.Location = new System.Drawing.Point(5, 2);
            this.gbTreeViewOptions.Name = "gbTreeViewOptions";
            this.gbTreeViewOptions.Size = new System.Drawing.Size(260, 48);
            this.gbTreeViewOptions.TabIndex = 0;
            this.gbTreeViewOptions.TabStop = false;
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btRefresh.Location = new System.Drawing.Point(6, 11);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(245, 31);
            this.btRefresh.TabIndex = 2;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // ClipartTreeListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.treeListAllFiles);
            this.Controls.Add(this.pnTreeViewOptions);
            this.Controls.Add(this.pnTreeViewProgress);
            this.Name = "ClipartTreeListControl";
            this.Size = new System.Drawing.Size(270, 500);
            ((System.ComponentModel.ISupportInitialize)(this.treeListAllFiles)).EndInit();
            this.pnTreeViewProgress.ResumeLayout(false);
            this.gbTreeViewProgress.ResumeLayout(false);
            this.pnTreeViewOptions.ResumeLayout(false);
            this.gbTreeViewOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTreeViewProgress;
        private System.Windows.Forms.GroupBox gbTreeViewProgress;
        private System.Windows.Forms.ProgressBar pgTreeViewProgress;
        private System.Windows.Forms.Label laTreeViewProgressLable;
        private System.Windows.Forms.Panel pnTreeViewOptions;
        private System.Windows.Forms.GroupBox gbTreeViewOptions;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.ImageList imageListFiles;
        private DevExpress.XtraTreeList.TreeList treeListAllFiles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPath;
    }
}
