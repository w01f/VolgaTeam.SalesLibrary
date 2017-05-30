namespace SalesLibraries.CloudAdmin
{
	partial class FormStart
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.pbCancelRegular = new System.Windows.Forms.PictureBox();
			this.pnNormal = new System.Windows.Forms.Panel();
			this.labelControlDownloadInfo = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.circularProgressRegular = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.pnProgressStages = new System.Windows.Forms.Panel();
			this.pnProgressStageFiles = new System.Windows.Forms.Panel();
			this.pbProgressStageFiles = new System.Windows.Forms.PictureBox();
			this.pnProgressStageSecurity = new System.Windows.Forms.Panel();
			this.pbProgressStageSecurity = new System.Windows.Forms.PictureBox();
			this.pnProgressStageWebConnection = new System.Windows.Forms.Panel();
			this.pbProgressStageWebSite = new System.Windows.Forms.PictureBox();
			this.pbHeaderRegular = new System.Windows.Forms.PictureBox();
			this.pbBrand = new System.Windows.Forms.PictureBox();
			this.pnMinimized = new System.Windows.Forms.Panel();
			this.pnSeparator = new System.Windows.Forms.Panel();
			this.pbHeaderMinimized = new System.Windows.Forms.PictureBox();
			this.circularProgressMinimized = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
			this.toolStripMenuItemShowProgress = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemKillApp = new System.Windows.Forms.ToolStripMenuItem();
			this.panelEx.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCancelRegular)).BeginInit();
			this.pnNormal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnProgressStages.SuspendLayout();
			this.pnProgressStageFiles.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageFiles)).BeginInit();
			this.pnProgressStageSecurity.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageSecurity)).BeginInit();
			this.pnProgressStageWebConnection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageWebSite)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbHeaderRegular)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbBrand)).BeginInit();
			this.pnMinimized.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbHeaderMinimized)).BeginInit();
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
			this.panelEx.Size = new System.Drawing.Size(733, 552);
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
			this.pbCancelRegular.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartCancel;
			this.pbCancelRegular.Location = new System.Drawing.Point(696, 2);
			this.pbCancelRegular.Name = "pbCancelRegular";
			this.pbCancelRegular.Size = new System.Drawing.Size(32, 32);
			this.pbCancelRegular.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCancelRegular.TabIndex = 0;
			this.pbCancelRegular.TabStop = false;
			this.pbCancelRegular.Click += new System.EventHandler(this.pbCancel_Click);
			// 
			// pnNormal
			// 
			this.pnNormal.Controls.Add(this.labelControlDownloadInfo);
			this.pnNormal.Controls.Add(this.circularProgressRegular);
			this.pnNormal.Controls.Add(this.pnProgressStages);
			this.pnNormal.Controls.Add(this.pbHeaderRegular);
			this.pnNormal.Controls.Add(this.pbBrand);
			this.pnNormal.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnNormal.Location = new System.Drawing.Point(0, 55);
			this.pnNormal.Name = "pnNormal";
			this.pnNormal.Padding = new System.Windows.Forms.Padding(10);
			this.pnNormal.Size = new System.Drawing.Size(733, 370);
			this.pnNormal.TabIndex = 26;
			// 
			// labelControlDownloadInfo
			// 
			this.labelControlDownloadInfo.AllowHtmlString = true;
			this.labelControlDownloadInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlDownloadInfo.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlDownloadInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlDownloadInfo.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AppearanceHovered.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
			this.labelControlDownloadInfo.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AppearancePressed.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.labelControlDownloadInfo.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
			this.labelControlDownloadInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDownloadInfo.Location = new System.Drawing.Point(63, 321);
			this.labelControlDownloadInfo.Name = "labelControlDownloadInfo";
			this.labelControlDownloadInfo.Size = new System.Drawing.Size(351, 49);
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
			// circularProgressRegular
			// 
			this.circularProgressRegular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.circularProgressRegular.AnimationSpeed = 50;
			this.circularProgressRegular.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgressRegular.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgressRegular.Enabled = false;
			this.circularProgressRegular.FocusCuesEnabled = false;
			this.circularProgressRegular.Location = new System.Drawing.Point(676, 325);
			this.circularProgressRegular.Name = "circularProgressRegular";
			this.circularProgressRegular.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgressRegular.ProgressColor = System.Drawing.Color.White;
			this.circularProgressRegular.ProgressTextFormat = "";
			this.circularProgressRegular.Size = new System.Drawing.Size(47, 35);
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
			this.pnProgressStages.Size = new System.Drawing.Size(713, 217);
			this.pnProgressStages.TabIndex = 24;
			// 
			// pnProgressStageFiles
			// 
			this.pnProgressStageFiles.Controls.Add(this.pbProgressStageFiles);
			this.pnProgressStageFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnProgressStageFiles.Location = new System.Drawing.Point(50, 140);
			this.pnProgressStageFiles.Name = "pnProgressStageFiles";
			this.pnProgressStageFiles.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
			this.pnProgressStageFiles.Size = new System.Drawing.Size(663, 70);
			this.pnProgressStageFiles.TabIndex = 21;
			this.pnProgressStageFiles.Visible = false;
			// 
			// pbProgressStageFiles
			// 
			this.pbProgressStageFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbProgressStageFiles.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartRegularFiles;
			this.pbProgressStageFiles.Location = new System.Drawing.Point(0, 0);
			this.pbProgressStageFiles.Name = "pbProgressStageFiles";
			this.pbProgressStageFiles.Size = new System.Drawing.Size(663, 50);
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
			this.pnProgressStageSecurity.Size = new System.Drawing.Size(663, 70);
			this.pnProgressStageSecurity.TabIndex = 20;
			this.pnProgressStageSecurity.Visible = false;
			// 
			// pbProgressStageSecurity
			// 
			this.pbProgressStageSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbProgressStageSecurity.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartRegularSecurity;
			this.pbProgressStageSecurity.Location = new System.Drawing.Point(0, 0);
			this.pbProgressStageSecurity.Name = "pbProgressStageSecurity";
			this.pbProgressStageSecurity.Size = new System.Drawing.Size(663, 50);
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
			this.pnProgressStageWebConnection.Size = new System.Drawing.Size(663, 70);
			this.pnProgressStageWebConnection.TabIndex = 19;
			this.pnProgressStageWebConnection.Visible = false;
			// 
			// pbProgressStageWebSite
			// 
			this.pbProgressStageWebSite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbProgressStageWebSite.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartRegularWebConnection;
			this.pbProgressStageWebSite.Location = new System.Drawing.Point(0, 0);
			this.pbProgressStageWebSite.Name = "pbProgressStageWebSite";
			this.pbProgressStageWebSite.Size = new System.Drawing.Size(663, 50);
			this.pbProgressStageWebSite.TabIndex = 24;
			this.pbProgressStageWebSite.TabStop = false;
			// 
			// pbHeaderRegular
			// 
			this.pbHeaderRegular.Dock = System.Windows.Forms.DockStyle.Top;
			this.pbHeaderRegular.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartRegularHeader;
			this.pbHeaderRegular.Location = new System.Drawing.Point(10, 10);
			this.pbHeaderRegular.Name = "pbHeaderRegular";
			this.pbHeaderRegular.Size = new System.Drawing.Size(713, 95);
			this.pbHeaderRegular.TabIndex = 22;
			this.pbHeaderRegular.TabStop = false;
			// 
			// pbBrand
			// 
			this.pbBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbBrand.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartRegularBrand;
			this.pbBrand.Location = new System.Drawing.Point(436, 321);
			this.pbBrand.Name = "pbBrand";
			this.pbBrand.Size = new System.Drawing.Size(230, 49);
			this.pbBrand.TabIndex = 23;
			this.pbBrand.TabStop = false;
			// 
			// pnMinimized
			// 
			this.pnMinimized.Controls.Add(this.pnSeparator);
			this.pnMinimized.Controls.Add(this.pbHeaderMinimized);
			this.pnMinimized.Controls.Add(this.circularProgressMinimized);
			this.pnMinimized.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnMinimized.Location = new System.Drawing.Point(0, 0);
			this.pnMinimized.Name = "pnMinimized";
			this.pnMinimized.Padding = new System.Windows.Forms.Padding(5);
			this.pnMinimized.Size = new System.Drawing.Size(733, 55);
			this.pnMinimized.TabIndex = 22;
			// 
			// pnSeparator
			// 
			this.pnSeparator.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnSeparator.Location = new System.Drawing.Point(35, 5);
			this.pnSeparator.Name = "pnSeparator";
			this.pnSeparator.Size = new System.Drawing.Size(5, 45);
			this.pnSeparator.TabIndex = 27;
			// 
			// pbHeaderMinimized
			// 
			this.pbHeaderMinimized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbHeaderMinimized.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressStartMinimizedRegularStart;
			this.pbHeaderMinimized.Location = new System.Drawing.Point(35, 5);
			this.pbHeaderMinimized.Name = "pbHeaderMinimized";
			this.pbHeaderMinimized.Size = new System.Drawing.Size(693, 45);
			this.pbHeaderMinimized.TabIndex = 25;
			this.pbHeaderMinimized.TabStop = false;
			// 
			// circularProgressMinimized
			// 
			this.circularProgressMinimized.AnimationSpeed = 50;
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
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(733, 552);
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
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnProgressStages.ResumeLayout(false);
			this.pnProgressStageFiles.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageFiles)).EndInit();
			this.pnProgressStageSecurity.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageSecurity)).EndInit();
			this.pnProgressStageWebConnection.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbProgressStageWebSite)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbHeaderRegular)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbBrand)).EndInit();
			this.pnMinimized.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbHeaderMinimized)).EndInit();
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
		private System.Windows.Forms.PictureBox pbBrand;
		private System.Windows.Forms.Panel pnProgressStages;
		private System.Windows.Forms.Panel pnProgressStageWebConnection;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnProgressStageFiles;
		private System.Windows.Forms.Panel pnProgressStageSecurity;
		private System.Windows.Forms.PictureBox pbProgressStageWebSite;
		private System.Windows.Forms.PictureBox pbProgressStageFiles;
		private System.Windows.Forms.PictureBox pbProgressStageSecurity;
		private System.Windows.Forms.PictureBox pbHeaderMinimized;
		private System.Windows.Forms.Panel pnMinimized;
		private System.Windows.Forms.Panel pnNormal;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgressMinimized;
		private System.Windows.Forms.Panel pnSeparator;
		public System.Windows.Forms.PictureBox pbHeaderRegular;
		private DevExpress.XtraEditors.LabelControl labelControlDownloadInfo;
	}
}