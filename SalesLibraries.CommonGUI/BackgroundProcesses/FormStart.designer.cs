namespace SalesLibraries.CommonGUI.BackgroundProcesses
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
			this.laTitle = new System.Windows.Forms.Label();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.labelXDescription = new DevComponents.DotNetBar.LabelX();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.labelXDetails = new DevComponents.DotNetBar.LabelX();
			this.panelEx.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(96, 23);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(397, 35);
			this.laTitle.TabIndex = 2;
			this.laTitle.Text = "Loading data...";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laTitle.UseMnemonic = false;
			this.laTitle.UseWaitCursor = true;
			// 
			// circularProgress
			// 
			this.circularProgress.AnimationSpeed = 50;
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Enabled = false;
			this.circularProgress.Location = new System.Drawing.Point(22, 12);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.circularProgress.ProgressTextFormat = "";
			this.circularProgress.Size = new System.Drawing.Size(55, 67);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 3;
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.labelXDescription);
			this.panelEx.Controls.Add(this.pictureBoxLogo);
			this.panelEx.Controls.Add(this.labelXDetails);
			this.panelEx.Controls.Add(this.laTitle);
			this.panelEx.Controls.Add(this.circularProgress);
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(2, 2);
			this.panelEx.Name = "panelEx";
			this.panelEx.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.panelEx.Size = new System.Drawing.Size(496, 136);
			this.panelEx.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.White;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
			// 
			// labelXDescription
			// 
			this.labelXDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelXDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelXDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelXDescription.ForeColor = System.Drawing.Color.Black;
			this.labelXDescription.Location = new System.Drawing.Point(22, 100);
			this.labelXDescription.Name = "labelXDescription";
			this.labelXDescription.Size = new System.Drawing.Size(333, 23);
			this.labelXDescription.TabIndex = 6;
			this.labelXDescription.Text = "<font color=\"#8C8C8C\">Details</font>";
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxLogo.ForeColor = System.Drawing.Color.Black;
			this.pictureBoxLogo.Image = global::SalesLibraries.CommonGUI.Properties.Resources.StartLogo;
			this.pictureBoxLogo.Location = new System.Drawing.Point(361, 85);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(132, 48);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxLogo.TabIndex = 5;
			this.pictureBoxLogo.TabStop = false;
			// 
			// labelXDetails
			// 
			this.labelXDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelXDetails.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelXDetails.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelXDetails.ForeColor = System.Drawing.Color.Black;
			this.labelXDetails.Location = new System.Drawing.Point(100, 61);
			this.labelXDetails.Name = "labelXDetails";
			this.labelXDetails.Size = new System.Drawing.Size(393, 18);
			this.labelXDetails.TabIndex = 4;
			// 
			// FormStart
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(500, 140);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormStart";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ProgressForm";
			this.Shown += new System.EventHandler(this.FormProgress_Shown);
			this.panelEx.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
		private DevComponents.DotNetBar.PanelEx panelEx;
		private DevComponents.DotNetBar.LabelX labelXDetails;
		private DevComponents.DotNetBar.LabelX labelXDescription;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}