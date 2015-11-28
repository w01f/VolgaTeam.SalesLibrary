namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	partial class FormResetCache
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
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXResetQV = new DevComponents.DotNetBar.ButtonX();
			this.buttonXResetWV = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControlTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlTitle.Location = new System.Drawing.Point(0, 0);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Padding = new System.Windows.Forms.Padding(10);
			this.labelControlTitle.Size = new System.Drawing.Size(273, 52);
			this.labelControlTitle.TabIndex = 0;
			this.labelControlTitle.Text = "Reset your !WV and !QV Folders for ALL LINKS in your Library...";
			// 
			// buttonXResetQV
			// 
			this.buttonXResetQV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXResetQV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXResetQV.Location = new System.Drawing.Point(9, 67);
			this.buttonXResetQV.Name = "buttonXResetQV";
			this.buttonXResetQV.Size = new System.Drawing.Size(252, 44);
			this.buttonXResetQV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXResetQV.TabIndex = 1;
			this.buttonXResetQV.Text = "Reset my !QV Folder";
			this.buttonXResetQV.Click += new System.EventHandler(this.buttonXResetQV_Click);
			// 
			// buttonXResetWV
			// 
			this.buttonXResetWV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXResetWV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXResetWV.Location = new System.Drawing.Point(9, 147);
			this.buttonXResetWV.Name = "buttonXResetWV";
			this.buttonXResetWV.Size = new System.Drawing.Size(252, 44);
			this.buttonXResetWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXResetWV.TabIndex = 2;
			this.buttonXResetWV.Text = "Reset my !WV Folder";
			this.buttonXResetWV.Click += new System.EventHandler(this.buttonXResetWV_Click);
			// 
			// FormResetCache
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(273, 223);
			this.Controls.Add(this.buttonXResetWV);
			this.Controls.Add(this.buttonXResetQV);
			this.Controls.Add(this.labelControlTitle);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormResetCache";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Advanced Stuff";
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevComponents.DotNetBar.ButtonX buttonXResetQV;
		private DevComponents.DotNetBar.ButtonX buttonXResetWV;
	}
}