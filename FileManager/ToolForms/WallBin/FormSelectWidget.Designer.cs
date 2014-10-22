namespace FileManager.ToolForms.WallBin
{
	partial class FormSelectWidget
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
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.pbSelectedWidget = new System.Windows.Forms.PictureBox();
			this.laAvailableWidgets = new System.Windows.Forms.Label();
			this.laSelectedWidget = new System.Windows.Forms.Label();
			this.xtraTabControlWidgets = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedWidget)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(165, 463);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 8;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(302, 463);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 9;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// pbSelectedWidget
			// 
			this.pbSelectedWidget.BackColor = System.Drawing.Color.White;
			this.pbSelectedWidget.ForeColor = System.Drawing.Color.Black;
			this.pbSelectedWidget.Location = new System.Drawing.Point(135, 12);
			this.pbSelectedWidget.Name = "pbSelectedWidget";
			this.pbSelectedWidget.Size = new System.Drawing.Size(36, 36);
			this.pbSelectedWidget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbSelectedWidget.TabIndex = 3;
			this.pbSelectedWidget.TabStop = false;
			// 
			// laAvailableWidgets
			// 
			this.laAvailableWidgets.AutoSize = true;
			this.laAvailableWidgets.BackColor = System.Drawing.Color.White;
			this.laAvailableWidgets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvailableWidgets.ForeColor = System.Drawing.Color.Black;
			this.laAvailableWidgets.Location = new System.Drawing.Point(12, 59);
			this.laAvailableWidgets.Name = "laAvailableWidgets";
			this.laAvailableWidgets.Size = new System.Drawing.Size(254, 16);
			this.laAvailableWidgets.TabIndex = 2;
			this.laAvailableWidgets.Text = "Click on image below to select widget:";
			// 
			// laSelectedWidget
			// 
			this.laSelectedWidget.AutoSize = true;
			this.laSelectedWidget.BackColor = System.Drawing.Color.White;
			this.laSelectedWidget.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSelectedWidget.ForeColor = System.Drawing.Color.Black;
			this.laSelectedWidget.Location = new System.Drawing.Point(12, 22);
			this.laSelectedWidget.Name = "laSelectedWidget";
			this.laSelectedWidget.Size = new System.Drawing.Size(117, 16);
			this.laSelectedWidget.TabIndex = 0;
			this.laSelectedWidget.Text = "Selected Widget:";
			// 
			// xtraTabControlWidgets
			// 
			this.xtraTabControlWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControlWidgets.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraTabControlWidgets.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControlWidgets.Appearance.Options.UseBackColor = true;
			this.xtraTabControlWidgets.Appearance.Options.UseForeColor = true;
			this.xtraTabControlWidgets.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlWidgets.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlWidgets.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlWidgets.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlWidgets.Location = new System.Drawing.Point(15, 78);
			this.xtraTabControlWidgets.Name = "xtraTabControlWidgets";
			this.xtraTabControlWidgets.Size = new System.Drawing.Size(533, 379);
			this.xtraTabControlWidgets.TabIndex = 7;
			// 
			// FormSelectWidget
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(560, 504);
			this.Controls.Add(this.pbSelectedWidget);
			this.Controls.Add(this.xtraTabControlWidgets);
			this.Controls.Add(this.laAvailableWidgets);
			this.Controls.Add(this.laSelectedWidget);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectWidget";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Widget";
			((System.ComponentModel.ISupportInitialize)(this.pbSelectedWidget)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlWidgets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		public System.Windows.Forms.PictureBox pbSelectedWidget;
		private System.Windows.Forms.Label laAvailableWidgets;
		private System.Windows.Forms.Label laSelectedWidget;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlWidgets;
	}
}