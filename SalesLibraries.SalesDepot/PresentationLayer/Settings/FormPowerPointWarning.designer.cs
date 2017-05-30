namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	partial class FormPowerPointWarning
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
			this.labelControlDescription = new DevExpress.XtraEditors.LabelControl();
			this.buttonXLaunch = new DevComponents.DotNetBar.ButtonX();
			this.buttonXIgnore = new DevComponents.DotNetBar.ButtonX();
			this.checkEditDoNotShow = new DevExpress.XtraEditors.CheckEdit();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDoNotShow.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControlDescription
			// 
			this.labelControlDescription.AllowHtmlString = true;
			this.labelControlDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlDescription.Appearance.BackColor = System.Drawing.Color.White;
			this.labelControlDescription.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlDescription.Appearance.ForeColor = System.Drawing.Color.Black;
			this.labelControlDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControlDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlDescription.Location = new System.Drawing.Point(125, 12);
			this.labelControlDescription.Name = "labelControlDescription";
			this.labelControlDescription.Size = new System.Drawing.Size(351, 162);
			this.labelControlDescription.TabIndex = 0;
			this.labelControlDescription.Text = "Do you want to go ahead and START a New PowerPoint Presentation?\r\n\r\nClick <b>LAUN" +
    "CH</b> to go ahead and open PowerPoint.\r\n\r\nClick <b>IGNORE</b> if you don’t real" +
    "ly give a rip either way…";
			// 
			// buttonXLaunch
			// 
			this.buttonXLaunch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLaunch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLaunch.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXLaunch.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXLaunch.Location = new System.Drawing.Point(199, 209);
			this.buttonXLaunch.Name = "buttonXLaunch";
			this.buttonXLaunch.Size = new System.Drawing.Size(119, 46);
			this.buttonXLaunch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLaunch.TabIndex = 1;
			this.buttonXLaunch.Text = "Launch";
			// 
			// buttonXIgnore
			// 
			this.buttonXIgnore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXIgnore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXIgnore.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.buttonXIgnore.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXIgnore.Location = new System.Drawing.Point(357, 209);
			this.buttonXIgnore.Name = "buttonXIgnore";
			this.buttonXIgnore.Size = new System.Drawing.Size(119, 46);
			this.buttonXIgnore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXIgnore.TabIndex = 2;
			this.buttonXIgnore.Text = "Ignore";
			// 
			// checkEditDoNotShow
			// 
			this.checkEditDoNotShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditDoNotShow.Location = new System.Drawing.Point(12, 180);
			this.checkEditDoNotShow.Name = "checkEditDoNotShow";
			this.checkEditDoNotShow.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditDoNotShow.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditDoNotShow.Properties.Appearance.Options.UseFont = true;
			this.checkEditDoNotShow.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditDoNotShow.Properties.AutoHeight = false;
			this.checkEditDoNotShow.Properties.Caption = "Don’t show this reminder again";
			this.checkEditDoNotShow.Size = new System.Drawing.Size(464, 23);
			this.checkEditDoNotShow.TabIndex = 4;
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::SalesLibraries.SalesDepot.Properties.Resources.PowerPointWarning;
			this.pbLogo.Location = new System.Drawing.Point(12, 12);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(96, 96);
			this.pbLogo.TabIndex = 3;
			this.pbLogo.TabStop = false;
			// 
			// FormPowerPointWarning
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(488, 267);
			this.Controls.Add(this.checkEditDoNotShow);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.buttonXIgnore);
			this.Controls.Add(this.buttonXLaunch);
			this.Controls.Add(this.labelControlDescription);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPowerPointWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PowerPoint Warning";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPowerPointWarning_FormClosed);
			this.Load += new System.EventHandler(this.FormPowerPointWarning_Load);
			((System.ComponentModel.ISupportInitialize)(this.checkEditDoNotShow.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlDescription;
		private DevComponents.DotNetBar.ButtonX buttonXLaunch;
		private DevComponents.DotNetBar.ButtonX buttonXIgnore;
		private System.Windows.Forms.PictureBox pbLogo;
		private DevExpress.XtraEditors.CheckEdit checkEditDoNotShow;
	}
}