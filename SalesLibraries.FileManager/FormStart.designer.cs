namespace SalesLibraries.FileManager
{
	partial class FormStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used./// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.pbCancelRegular = new System.Windows.Forms.PictureBox();
			this.pnNormal = new System.Windows.Forms.Panel();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.labelControlDownloadInfo = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pictureEditBrand = new DevExpress.XtraEditors.PictureEdit();
			this.pnSeparator2 = new System.Windows.Forms.Panel();
			this.circularProgressRegular = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pnProgressStages = new System.Windows.Forms.Panel();
			this.pnProgressStageFiles = new System.Windows.Forms.Panel();
			this.pbProgressStageFiles = new System.Windows.Forms.PictureBox();
			this.pnProgressStageSecurity = new System.Windows.Forms.Panel();
			this.pbProgressStageSecurity = new System.Windows.Forms.PictureBox();
			this.pnProgressStageWebConnection = new System.Windows.Forms.Panel();
			this.pbProgressStageWebSite = new System.Windows.Forms.PictureBox();
			this.pictureEditHeaderRegular = new DevExpress.XtraEditors.PictureEdit();
			this.pnMinimized = new System.Windows.Forms.Panel();
			this.pictureEditHeaderMinimized = new DevExpress.XtraEditors.PictureEdit();
			this.pnSeparator1 = new System.Windows.Forms.Panel();
			this.circularProgressMinimized = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemShowProgress = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemKillApp = new System.Windows.Forms.ToolStripMenuItem();
			this.panelEx.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCancelRegular)).BeginInit();
			this.pnNormal.SuspendLayout();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditBrand.Properties)).BeginInit();
			this.pnProgressStages.SuspendLayout();
			this.pnProgressStageFiles.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageFiles)).BeginInit();
			this.pnProgressStageSecurity.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageSecurity)).BeginInit();
			this.pnProgressStageWebConnection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageWebSite)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditHeaderRegular.Properties)).BeginInit();
			this.pnMinimized.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditHeaderMinimized.Properties)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.pbCancelRegular);
			this.panelEx.Controls.Add(this.pnNormal);
			this.panelEx.Controls.Add(this.pnMinimized);
			this.panelEx.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(0, 0);
			this.panelEx.Name = "panelEx";
			this.panelEx.Size = new System.Drawing.Size(700, 552);
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.DeepSkyBlue;
			this.panelEx.Style.BackColor2.Color = System.Drawing.Color.DeepSkyBlue;
			this.panelEx.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
			// 
			// pbCancelRegular
			// 
			this.pbCancelRegular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbCancelRegular.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbCancelRegular.Image = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartCancel;
			this.pbCancelRegular.Location = new System.Drawing.Point(663, 2);
			this.pbCancelRegular.Name = "pbCancelRegular";
			this.pbCancelRegular.Size = new System.Drawing.Size(32, 32);
			this.pbCancelRegular.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCancelRegular.TabIndex = 0;
			this.pbCancelRegular.TabStop = false;
			this.pbCancelRegular.Click += new System.EventHandler(this.pbCancel_Click);
			// 
			// pnNormal
			// 
			this.pnNormal.Controls.Add(this.pnBottom);
			this.pnNormal.Controls.Add(this.pnProgressStages);
			this.pnNormal.Controls.Add(this.pictureEditHeaderRegular);
			this.pnNormal.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnNormal.Location = new System.Drawing.Point(0, 55);
			this.pnNormal.Name = "pnNormal";
			this.pnNormal.Padding = new System.Windows.Forms.Padding(10);
			this.pnNormal.Size = new System.Drawing.Size(700, 370);
			this.pnNormal.TabIndex = 26;
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.labelControlDownloadInfo);
			this.pnBottom.Controls.Add(this.pictureEditBrand);
			this.pnBottom.Controls.Add(this.pnSeparator2);
			this.pnBottom.Controls.Add(this.circularProgressRegular);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(10, 310);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
			this.pnBottom.Size = new System.Drawing.Size(680, 50);
			this.pnBottom.TabIndex = 27;
			// 
			// labelControlDownloadInfo
			// 
			this.labelControlDownloadInfo.AllowHtmlString = true;
			this.labelControlDownloadInfo.Appearance.Options.UseTextOptions = true;
			this.labelControlDownloadInfo.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlDownloadInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AppearanceDisabled.Options.UseTextOptions = true;
			this.labelControlDownloadInfo.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlDownloadInfo.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AppearanceHovered.Options.UseTextOptions = true;
			this.labelControlDownloadInfo.AppearanceHovered.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
			this.labelControlDownloadInfo.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AppearancePressed.Options.UseTextOptions = true;
			this.labelControlDownloadInfo.AppearancePressed.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlDownloadInfo.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDownloadInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlDownloadInfo.Location = new System.Drawing.Point(50, 0);
			this.labelControlDownloadInfo.Name = "labelControlDownloadInfo";
			this.labelControlDownloadInfo.Size = new System.Drawing.Size(308, 50);
			this.labelControlDownloadInfo.StyleController = this.styleController;
			this.labelControlDownloadInfo.TabIndex = 25;
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
			// pictureEditBrand
			// 
			this.pictureEditBrand.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.pictureEditBrand.Dock = System.Windows.Forms.DockStyle.Right;
			this.pictureEditBrand.EditValue = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartRegularBrand;
			this.pictureEditBrand.Location = new System.Drawing.Point(358, 0);
			this.pictureEditBrand.Name = "pictureEditBrand";
			this.pictureEditBrand.Properties.AllowFocused = false;
			this.pictureEditBrand.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEditBrand.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditBrand.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditBrand.Properties.PictureAlignment = System.Drawing.ContentAlignment.MiddleRight;
			this.pictureEditBrand.Properties.ReadOnly = true;
			this.pictureEditBrand.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditBrand.Properties.ShowMenu = false;
			this.pictureEditBrand.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			this.pictureEditBrand.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditBrand.Size = new System.Drawing.Size(246, 50);
			this.pictureEditBrand.TabIndex = 27;
			// 
			// pnSeparator2
			// 
			this.pnSeparator2.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnSeparator2.Location = new System.Drawing.Point(604, 0);
			this.pnSeparator2.Name = "pnSeparator2";
			this.pnSeparator2.Size = new System.Drawing.Size(20, 50);
			this.pnSeparator2.TabIndex = 28;
			// 
			// circularProgressRegular
			// 
			this.circularProgressRegular.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgressRegular.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressRegular.Dock = System.Windows.Forms.DockStyle.Right;
			this.circularProgressRegular.Enabled = false;
			this.circularProgressRegular.FocusCuesEnabled = false;
			this.circularProgressRegular.Location = new System.Drawing.Point(624, 0);
			this.circularProgressRegular.Name = "circularProgressRegular";
			this.circularProgressRegular.Padding = new System.Windows.Forms.Padding(10);
			this.circularProgressRegular.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressRegular.ProgressColor = System.Drawing.Color.White;
			this.circularProgressRegular.ProgressTextFormat = "";
			this.circularProgressRegular.Size = new System.Drawing.Size(56, 50);
			this.circularProgressRegular.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressRegular.TabIndex = 18;
			// 
			// pnProgressStages
			// 
			this.pnProgressStages.Controls.Add(this.pnProgressStageFiles);
			this.pnProgressStages.Controls.Add(this.pnProgressStageSecurity);
			this.pnProgressStages.Controls.Add(this.pnProgressStageWebConnection);
			this.pnProgressStages.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnProgressStages.Location = new System.Drawing.Point(10, 105);
			this.pnProgressStages.Name = "pnProgressStages";
			this.pnProgressStages.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
			this.pnProgressStages.Size = new System.Drawing.Size(680, 217);
			this.pnProgressStages.TabIndex = 24;
			// 
			// pnProgressStageFiles
			// 
			this.pnProgressStageFiles.Controls.Add(this.pbProgressStageFiles);
			this.pnProgressStageFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnProgressStageFiles.Location = new System.Drawing.Point(50, 140);
			this.pnProgressStageFiles.Name = "pnProgressStageFiles";
			this.pnProgressStageFiles.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
			this.pnProgressStageFiles.Size = new System.Drawing.Size(630, 70);
			this.pnProgressStageFiles.TabIndex = 21;
			this.pnProgressStageFiles.Visible = false;
			// 
			// pbProgressStageFiles
			// 
			this.pbProgressStageFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbProgressStageFiles.Image = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartRegularFiles;
			this.pbProgressStageFiles.Location = new System.Drawing.Point(0, 0);
			this.pbProgressStageFiles.Name = "pbProgressStageFiles";
			this.pbProgressStageFiles.Size = new System.Drawing.Size(630, 50);
			this.pbProgressStageFiles.TabIndex = 26;
			this.pbProgressStageFiles.TabStop = false;
			// 
			// pnProgressStageSecurity
			// 
			this.pnProgressStageSecurity.Controls.Add(this.pbProgressStageSecurity);
			this.pnProgressStageSecurity.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnProgressStageSecurity.Location = new System.Drawing.Point(50, 70);
			this.pnProgressStageSecurity.Name = "pnProgressStageSecurity";
			this.pnProgressStageSecurity.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
			this.pnProgressStageSecurity.Size = new System.Drawing.Size(630, 70);
			this.pnProgressStageSecurity.TabIndex = 20;
			this.pnProgressStageSecurity.Visible = false;
			// 
			// pbProgressStageSecurity
			// 
			this.pbProgressStageSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbProgressStageSecurity.Image = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartRegularSecurity;
			this.pbProgressStageSecurity.Location = new System.Drawing.Point(0, 0);
			this.pbProgressStageSecurity.Name = "pbProgressStageSecurity";
			this.pbProgressStageSecurity.Size = new System.Drawing.Size(630, 50);
			this.pbProgressStageSecurity.TabIndex = 25;
			this.pbProgressStageSecurity.TabStop = false;
			// 
			// pnProgressStageWebConnection
			// 
			this.pnProgressStageWebConnection.Controls.Add(this.pbProgressStageWebSite);
			this.pnProgressStageWebConnection.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnProgressStageWebConnection.Location = new System.Drawing.Point(50, 0);
			this.pnProgressStageWebConnection.Name = "pnProgressStageWebConnection";
			this.pnProgressStageWebConnection.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
			this.pnProgressStageWebConnection.Size = new System.Drawing.Size(630, 70);
			this.pnProgressStageWebConnection.TabIndex = 19;
			this.pnProgressStageWebConnection.Visible = false;
			// 
			// pbProgressStageWebSite
			// 
			this.pbProgressStageWebSite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbProgressStageWebSite.Image = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartRegularWebConnection;
			this.pbProgressStageWebSite.Location = new System.Drawing.Point(0, 0);
			this.pbProgressStageWebSite.Name = "pbProgressStageWebSite";
			this.pbProgressStageWebSite.Size = new System.Drawing.Size(630, 50);
			this.pbProgressStageWebSite.TabIndex = 24;
			this.pbProgressStageWebSite.TabStop = false;
			// 
			// pictureEditHeaderRegular
			// 
			this.pictureEditHeaderRegular.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.pictureEditHeaderRegular.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureEditHeaderRegular.EditValue = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartRegularHeader;
			this.pictureEditHeaderRegular.Location = new System.Drawing.Point(10, 10);
			this.pictureEditHeaderRegular.Name = "pictureEditHeaderRegular";
			this.pictureEditHeaderRegular.Properties.AllowFocused = false;
			this.pictureEditHeaderRegular.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEditHeaderRegular.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditHeaderRegular.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditHeaderRegular.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.pictureEditHeaderRegular.Properties.ReadOnly = true;
			this.pictureEditHeaderRegular.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditHeaderRegular.Properties.ShowMenu = false;
			this.pictureEditHeaderRegular.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditHeaderRegular.Size = new System.Drawing.Size(680, 95);
			this.pictureEditHeaderRegular.TabIndex = 26;
			// 
			// pnMinimized
			// 
			this.pnMinimized.Controls.Add(this.pictureEditHeaderMinimized);
			this.pnMinimized.Controls.Add(this.pnSeparator1);
			this.pnMinimized.Controls.Add(this.circularProgressMinimized);
			this.pnMinimized.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnMinimized.Location = new System.Drawing.Point(0, 0);
			this.pnMinimized.Name = "pnMinimized";
			this.pnMinimized.Padding = new System.Windows.Forms.Padding(5);
			this.pnMinimized.Size = new System.Drawing.Size(700, 55);
			this.pnMinimized.TabIndex = 22;
			// 
			// pictureEditHeaderMinimized
			// 
			this.pictureEditHeaderMinimized.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.pictureEditHeaderMinimized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureEditHeaderMinimized.EditValue = global::SalesLibraries.FileManager.Properties.Resources.ProgressStartMinimizedRegularStart;
			this.pictureEditHeaderMinimized.Location = new System.Drawing.Point(40, 5);
			this.pictureEditHeaderMinimized.Name = "pictureEditHeaderMinimized";
			this.pictureEditHeaderMinimized.Properties.AllowFocused = false;
			this.pictureEditHeaderMinimized.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEditHeaderMinimized.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditHeaderMinimized.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditHeaderMinimized.Properties.PictureAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.pictureEditHeaderMinimized.Properties.ReadOnly = true;
			this.pictureEditHeaderMinimized.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditHeaderMinimized.Properties.ShowMenu = false;
			this.pictureEditHeaderMinimized.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditHeaderMinimized.Size = new System.Drawing.Size(655, 45);
			this.pictureEditHeaderMinimized.TabIndex = 28;
			// 
			// pnSeparator1
			// 
			this.pnSeparator1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnSeparator1.Location = new System.Drawing.Point(35, 5);
			this.pnSeparator1.Name = "pnSeparator1";
			this.pnSeparator1.Size = new System.Drawing.Size(5, 45);
			this.pnSeparator1.TabIndex = 27;
			// 
			// circularProgressMinimized
			// 
			this.circularProgressMinimized.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgressMinimized.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressMinimized.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgressMinimized.Enabled = false;
			this.circularProgressMinimized.FocusCuesEnabled = false;
			this.circularProgressMinimized.Location = new System.Drawing.Point(5, 5);
			this.circularProgressMinimized.Name = "circularProgressMinimized";
			this.circularProgressMinimized.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressMinimized.ProgressColor = System.Drawing.Color.White;
			this.circularProgressMinimized.ProgressTextFormat = "";
			this.circularProgressMinimized.Size = new System.Drawing.Size(30, 45);
			this.circularProgressMinimized.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgressMinimized.TabIndex = 26;
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShowProgress,
            this.toolStripSeparator1,
            this.toolStripMenuItemKillApp});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(170, 54);
			// 
			// toolStripMenuItemShowProgress
			// 
			this.toolStripMenuItemShowProgress.Name = "toolStripMenuItemShowProgress";
			this.toolStripMenuItemShowProgress.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItemShowProgress.Text = "Show Sync Details";
			this.toolStripMenuItemShowProgress.Click += new System.EventHandler(this.toolStripMenuItemShowProgress_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
			// 
			// toolStripMenuItemKillApp
			// 
			this.toolStripMenuItemKillApp.Name = "toolStripMenuItemKillApp";
			this.toolStripMenuItemKillApp.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItemKillApp.Text = "Kill {0}";
			this.toolStripMenuItemKillApp.Click += new System.EventHandler(this.toolStripMenuItemKillApp_Click);
			// 
			// FormStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(700, 552);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormStart";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProgressForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.FormProgress_Shown);
			this.panelEx.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbCancelRegular)).EndInit();
			this.pnNormal.ResumeLayout(false);
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditBrand.Properties)).EndInit();
			this.pnProgressStages.ResumeLayout(false);
			this.pnProgressStageFiles.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageFiles)).EndInit();
			this.pnProgressStageSecurity.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageSecurity)).EndInit();
			this.pnProgressStageWebConnection.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageWebSite)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditHeaderRegular.Properties)).EndInit();
			this.pnMinimized.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureEditHeaderMinimized.Properties)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbCancelRegular;
        protected DevComponents.DotNetBar.PanelEx panelEx;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowProgress;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKillApp;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressRegular;
		private System.Windows.Forms.Panel pnProgressStages;
		private System.Windows.Forms.Panel pnProgressStageWebConnection;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnProgressStageFiles;
		private System.Windows.Forms.Panel pnProgressStageSecurity;
		private System.Windows.Forms.PictureBox pbProgressStageWebSite;
		private System.Windows.Forms.PictureBox pbProgressStageFiles;
		private System.Windows.Forms.PictureBox pbProgressStageSecurity;
		private System.Windows.Forms.Panel pnMinimized;
		private System.Windows.Forms.Panel pnNormal;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressMinimized;
		private System.Windows.Forms.Panel pnSeparator1;
		private DevExpress.XtraEditors.LabelControl labelControlDownloadInfo;
	    public DevExpress.XtraEditors.PictureEdit pictureEditHeaderRegular;
		private System.Windows.Forms.Panel pnBottom;
		private DevExpress.XtraEditors.PictureEdit pictureEditBrand;
		private System.Windows.Forms.Panel pnSeparator2;
		private DevExpress.XtraEditors.PictureEdit pictureEditHeaderMinimized;
	}
}