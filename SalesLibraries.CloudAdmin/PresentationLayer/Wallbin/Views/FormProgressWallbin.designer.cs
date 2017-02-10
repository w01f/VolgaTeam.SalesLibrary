namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
    partial class FormProgressWallbin
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
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.panelEx.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.Color.Empty;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.pictureBoxLogo);
			this.panelEx.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(2, 2);
			this.panelEx.Name = "panelEx";
			this.panelEx.Padding = new System.Windows.Forms.Padding(10);
			this.panelEx.Size = new System.Drawing.Size(334, 98);
			this.panelEx.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.White;
			this.panelEx.Style.BackColor2.Color = System.Drawing.Color.White;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.Color = System.Drawing.Color.LightGray;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 5;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxLogo.Image = global::SalesLibraries.CloudAdmin.Properties.Resources.ProgressWallbinLogo;
			this.pictureBoxLogo.Location = new System.Drawing.Point(10, 10);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(314, 78);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxLogo.TabIndex = 0;
			this.pictureBoxLogo.TabStop = false;
			// 
			// FormProgressWallbin
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(338, 102);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormProgressWallbin";
			this.Opacity = 0.85D;
			this.Padding = new System.Windows.Forms.Padding(2);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ProgressForm";
			this.panelEx.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
		private DevComponents.DotNetBar.PanelEx panelEx;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
	}
}