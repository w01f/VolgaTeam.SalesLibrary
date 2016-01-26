namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	partial class FormProgressWithAbort
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
			this.laTitle = new System.Windows.Forms.Label();
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.panelExCancel = new DevComponents.DotNetBar.PanelEx();
			this.pbCancel = new System.Windows.Forms.PictureBox();
			this.laTime = new System.Windows.Forms.Label();
			this.panelEx.SuspendLayout();
			this.panelExCancel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laTitle.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.White;
			this.laTitle.Location = new System.Drawing.Point(26, 0);
			this.laTitle.Name = "laTitle";
			this.laTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.laTitle.Size = new System.Drawing.Size(160, 32);
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
			this.panelEx.Controls.Add(this.laTime);
			this.panelEx.Controls.Add(this.circularProgress);
			this.panelEx.Controls.Add(this.panelExCancel);
			this.panelEx.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(1, 1);
			this.panelEx.Name = "panelEx";
			this.panelEx.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.panelEx.Size = new System.Drawing.Size(281, 32);
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.ForestGreen;
			this.panelEx.Style.BackColor2.Color = System.Drawing.Color.ForestGreen;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
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
			this.circularProgress.TabIndex = 18;
			// 
			// panelExCancel
			// 
			this.panelExCancel.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExCancel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExCancel.Controls.Add(this.pbCancel);
			this.panelExCancel.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelExCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelExCancel.Location = new System.Drawing.Point(247, 0);
			this.panelExCancel.Name = "panelExCancel";
			this.panelExCancel.Padding = new System.Windows.Forms.Padding(1);
			this.panelExCancel.Size = new System.Drawing.Size(34, 32);
			this.panelExCancel.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExCancel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelExCancel.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelExCancel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExCancel.Style.GradientAngle = 90;
			this.panelExCancel.TabIndex = 5;
			// 
			// pbCancel
			// 
			this.pbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbCancel.Image = global::SalesLibraries.CommonGUI.Properties.Resources.ProgressCancel;
			this.pbCancel.Location = new System.Drawing.Point(4, 4);
			this.pbCancel.Name = "pbCancel";
			this.pbCancel.Size = new System.Drawing.Size(26, 24);
			this.pbCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCancel.TabIndex = 0;
			this.pbCancel.TabStop = false;
			this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
			// 
			// laTime
			// 
			this.laTime.BackColor = System.Drawing.Color.Transparent;
			this.laTime.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laTime.Dock = System.Windows.Forms.DockStyle.Right;
			this.laTime.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTime.ForeColor = System.Drawing.Color.White;
			this.laTime.Location = new System.Drawing.Point(186, 0);
			this.laTime.Name = "laTime";
			this.laTime.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.laTime.Size = new System.Drawing.Size(61, 32);
			this.laTime.TabIndex = 22;
			this.laTime.Text = "0:00:00";
			this.laTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.laTime.UseMnemonic = false;
			// 
			// FormProgressWithAbort
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(283, 34);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormProgressWithAbort";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProgressForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.FormProgress_Shown);
			this.panelEx.ResumeLayout(false);
			this.panelExCancel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Label laTitle;
        private System.Windows.Forms.PictureBox pbCancel;
        protected DevComponents.DotNetBar.PanelEx panelEx;
		protected DevComponents.DotNetBar.PanelEx panelExCancel;
		private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
		public System.Windows.Forms.Label laTime;
    }
}