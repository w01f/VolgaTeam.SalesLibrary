namespace FileManager.ToolForms.IPad
{
    partial class FormVideoWarning
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
			this.laWarningText = new System.Windows.Forms.Label();
			this.panelEx = new DevComponents.DotNetBar.PanelEx();
			this.panelExCancel = new DevComponents.DotNetBar.PanelEx();
			this.pbCancel = new System.Windows.Forms.PictureBox();
			this.panelEx.SuspendLayout();
			this.panelExCancel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
			this.SuspendLayout();
			// 
			// laWarningText
			// 
			this.laWarningText.BackColor = System.Drawing.Color.Transparent;
			this.laWarningText.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.laWarningText.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laWarningText.ForeColor = System.Drawing.Color.White;
			this.laWarningText.Location = new System.Drawing.Point(15, 4);
			this.laWarningText.Name = "laWarningText";
			this.laWarningText.Size = new System.Drawing.Size(181, 76);
			this.laWarningText.TabIndex = 2;
			this.laWarningText.Text = "You Need to CONVERT some VIDEO…";
			this.laWarningText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laWarningText.UseMnemonic = false;
			this.laWarningText.Click += new System.EventHandler(this.laWarningText_Click);
			// 
			// panelEx
			// 
			this.panelEx.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx.Controls.Add(this.panelExCancel);
			this.panelEx.Controls.Add(this.laWarningText);
			this.panelEx.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panelEx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx.Location = new System.Drawing.Point(2, 2);
			this.panelEx.Name = "panelEx";
			this.panelEx.Size = new System.Drawing.Size(276, 84);
			this.panelEx.Style.BackColor1.Color = System.Drawing.Color.Black;
			this.panelEx.Style.BackColor2.Color = System.Drawing.Color.Black;
			this.panelEx.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx.Style.BorderColor.Color = System.Drawing.Color.White;
			this.panelEx.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx.Style.GradientAngle = 90;
			this.panelEx.TabIndex = 4;
			// 
			// panelExCancel
			// 
			this.panelExCancel.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExCancel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExCancel.Controls.Add(this.pbCancel);
			this.panelExCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelExCancel.Location = new System.Drawing.Point(202, 0);
			this.panelExCancel.Name = "panelExCancel";
			this.panelExCancel.Padding = new System.Windows.Forms.Padding(1);
			this.panelExCancel.Size = new System.Drawing.Size(74, 84);
			this.panelExCancel.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExCancel.Style.BackColor1.Color = System.Drawing.Color.Black;
			this.panelExCancel.Style.BackColor2.Color = System.Drawing.Color.Black;
			this.panelExCancel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelExCancel.Style.BorderColor.Color = System.Drawing.Color.White;
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
			this.pbCancel.Image = global::FileManager.Properties.Resources.ToolboxClose;
			this.pbCancel.Location = new System.Drawing.Point(8, 10);
			this.pbCancel.Name = "pbCancel";
			this.pbCancel.Size = new System.Drawing.Size(58, 65);
			this.pbCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCancel.TabIndex = 0;
			this.pbCancel.TabStop = false;
			this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
			this.pbCancel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pbCancel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// FormVideoWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(280, 88);
			this.ControlBox = false;
			this.Controls.Add(this.panelEx);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormVideoWarning";
			this.Opacity = 0.85D;
			this.Padding = new System.Windows.Forms.Padding(2);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProgressForm";
			this.TopMost = true;
			this.panelEx.ResumeLayout(false);
			this.panelExCancel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Label laWarningText;
        private System.Windows.Forms.PictureBox pbCancel;
        protected DevComponents.DotNetBar.PanelEx panelEx;
		protected DevComponents.DotNetBar.PanelEx panelExCancel;
    }
}