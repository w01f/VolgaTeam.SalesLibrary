namespace FileManager.ToolForms
{
    partial class FormProgressSync
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
			this.laProgress = new System.Windows.Forms.Label();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.ckCloseAfterSync = new System.Windows.Forms.CheckBox();
			this.panelExCancel = new DevComponents.DotNetBar.PanelEx();
			this.pbCancel = new System.Windows.Forms.PictureBox();
			this.laTime = new System.Windows.Forms.Label();
			this.panelEx.SuspendLayout();
			this.panelExCancel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
			this.SuspendLayout();
			// 
			// laProgress
			// 
			this.laProgress.BackColor = System.Drawing.Color.Transparent;
			this.laProgress.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laProgress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laProgress.ForeColor = System.Drawing.Color.White;
			this.laProgress.Location = new System.Drawing.Point(62, 4);
			this.laProgress.Name = "laProgress";
			this.laProgress.Size = new System.Drawing.Size(155, 36);
			this.laProgress.TabIndex = 2;
			this.laProgress.Text = "Loading data...";
			this.laProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.laProgress.UseMnemonic = false;
			// 
			// circularProgress
			// 
			this.circularProgress.AnimationSpeed = 50;
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.circularProgress.Enabled = false;
			this.circularProgress.FocusCuesEnabled = false;
			this.circularProgress.Location = new System.Drawing.Point(8, 3);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.PieBorderDark = System.Drawing.Color.White;
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.White;
			this.circularProgress.ProgressTextFormat = "";
			this.circularProgress.Size = new System.Drawing.Size(48, 78);
			this.circularProgress.SpokeBorderDark = System.Drawing.Color.White;
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 3;
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.ckCloseAfterSync);
			this.panelEx.Controls.Add(this.panelExCancel);
			this.panelEx.Controls.Add(this.laTime);
			this.panelEx.Controls.Add(this.laProgress);
			this.panelEx.Controls.Add(this.circularProgress);
			this.panelEx.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(2, 2);
			this.panelEx.Name = "panelEx";
			this.panelEx.Size = new System.Drawing.Size(276, 84);
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.Blue;
			this.panelEx.Style.BackColor2.Color = System.Drawing.Color.Blue;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
			// 
			// ckCloseAfterSync
			// 
			this.ckCloseAfterSync.AutoSize = true;
			this.ckCloseAfterSync.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.ckCloseAfterSync.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckCloseAfterSync.ForeColor = System.Drawing.Color.White;
			this.ckCloseAfterSync.Location = new System.Drawing.Point(71, 60);
			this.ckCloseAfterSync.Name = "ckCloseAfterSync";
			this.ckCloseAfterSync.Size = new System.Drawing.Size(135, 20);
			this.ckCloseAfterSync.TabIndex = 20;
			this.ckCloseAfterSync.Text = "Close on complete";
			this.ckCloseAfterSync.UseVisualStyleBackColor = true;
			// 
			// panelExCancel
			// 
			this.panelExCancel.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExCancel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExCancel.Controls.Add(this.pbCancel);
			this.panelExCancel.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelExCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelExCancel.Location = new System.Drawing.Point(222, 0);
			this.panelExCancel.Name = "panelExCancel";
			this.panelExCancel.Padding = new System.Windows.Forms.Padding(1);
			this.panelExCancel.Size = new System.Drawing.Size(54, 84);
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
			this.pbCancel.Image = global::FileManager.Properties.Resources.CancelSync;
			this.pbCancel.Location = new System.Drawing.Point(4, 4);
			this.pbCancel.Name = "pbCancel";
			this.pbCancel.Size = new System.Drawing.Size(46, 76);
			this.pbCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbCancel.TabIndex = 0;
			this.pbCancel.TabStop = false;
			this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
			this.pbCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// laTime
			// 
			this.laTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.laTime.BackColor = System.Drawing.Color.Transparent;
			this.laTime.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laTime.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTime.ForeColor = System.Drawing.Color.White;
			this.laTime.Location = new System.Drawing.Point(62, 40);
			this.laTime.Name = "laTime";
			this.laTime.Size = new System.Drawing.Size(155, 17);
			this.laTime.TabIndex = 4;
			this.laTime.Text = "0:00:00";
			this.laTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.laTime.UseMnemonic = false;
			// 
			// FormProgressSync
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(280, 88);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormProgressSync";
			this.Opacity = 0.85D;
			this.Padding = new System.Windows.Forms.Padding(2);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProgressForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.FormProgress_Shown);
			this.panelEx.ResumeLayout(false);
			this.panelEx.PerformLayout();
			this.panelExCancel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label laProgress;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
        public System.Windows.Forms.Label laTime;
        private System.Windows.Forms.PictureBox pbCancel;
        protected DevComponents.DotNetBar.PanelEx panelEx;
        protected DevComponents.DotNetBar.PanelEx panelExCancel;
        protected System.Windows.Forms.CheckBox ckCloseAfterSync;
    }
}