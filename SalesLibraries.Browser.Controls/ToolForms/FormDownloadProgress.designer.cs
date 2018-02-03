namespace SalesLibraries.Browser.Controls.ToolForms
{
	partial class FormDownloadProgress
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDownloadProgress));
			this.laTitle = new System.Windows.Forms.Label();
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.laDetails = new System.Windows.Forms.Label();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemShowProgress = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemKillApp = new System.Windows.Forms.ToolStripMenuItem();
			this.panelEx.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTitle.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.White;
			this.laTitle.Location = new System.Drawing.Point(26, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.laTitle.Size = new System.Drawing.Size(282, 32);
			this.laTitle.TabIndex = 2;
			this.laTitle.Text = "Test...";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laTitle.UseMnemonic = false;
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.laTitle);
			this.panelEx.Controls.Add(this.laDetails);
			this.panelEx.Controls.Add(this.circularProgress);
			this.panelEx.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(1, 1);
			this.panelEx.Name = "panelEx";
			this.panelEx.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.panelEx.Size = new System.Drawing.Size(308, 32);
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.ForestGreen;
			this.panelEx.Style.BackColor2.Color = System.Drawing.Color.ForestGreen;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
			// 
			// laDetails
			// 
			this.laDetails.BackColor = System.Drawing.Color.Transparent;
			this.laDetails.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laDetails.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDetails.ForeColor = System.Drawing.Color.White;
			this.laDetails.Location = new System.Drawing.Point(26, 0);
			this.laDetails.Name = "laDetails";
			this.laDetails.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.laDetails.Size = new System.Drawing.Size(282, 32);
			this.laDetails.TabIndex = 13;
			this.laDetails.Text = "Test...";
			this.laDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laDetails.UseMnemonic = false;
			// 
			// circularProgress
			// 
			this.circularProgress.AnimationSpeed = 50;
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Dock = System.Windows.Forms.DockStyle.Left;
			this.circularProgress.Enabled = false;
			this.circularProgress.Location = new System.Drawing.Point(5, 0);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.White;
			this.circularProgress.ProgressTextFormat = "";
			this.circularProgress.Size = new System.Drawing.Size(21, 32);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 17;
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
			// 
			// FormDownloadProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(310, 34);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormDownloadProgress";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProgressForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.OnFormShown);
			this.panelEx.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Label laTitle;
        protected DevComponents.DotNetBar.PanelEx panelEx;
		public System.Windows.Forms.Label laDetails;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowProgress;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKillApp;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
    }
}