namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	partial class LinkEmbeddedOptions
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnAdminTools = new System.Windows.Forms.Panel();
			this.laAdminTools = new System.Windows.Forms.Label();
			this.buttonXRefreshPreview = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenQV = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenWV = new DevComponents.DotNetBar.ButtonX();
			this.pnAdminTools.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnAdminTools
			// 
			this.pnAdminTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnAdminTools.Controls.Add(this.laAdminTools);
			this.pnAdminTools.Controls.Add(this.buttonXRefreshPreview);
			this.pnAdminTools.Controls.Add(this.buttonXOpenQV);
			this.pnAdminTools.Controls.Add(this.buttonXOpenWV);
			this.pnAdminTools.ForeColor = System.Drawing.Color.Black;
			this.pnAdminTools.Location = new System.Drawing.Point(3, 486);
			this.pnAdminTools.Name = "pnAdminTools";
			this.pnAdminTools.Size = new System.Drawing.Size(525, 52);
			this.pnAdminTools.TabIndex = 24;
			// 
			// laAdminTools
			// 
			this.laAdminTools.AutoSize = true;
			this.laAdminTools.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdminTools.ForeColor = System.Drawing.Color.Black;
			this.laAdminTools.Location = new System.Drawing.Point(-1, 0);
			this.laAdminTools.Name = "laAdminTools";
			this.laAdminTools.Size = new System.Drawing.Size(83, 16);
			this.laAdminTools.TabIndex = 10;
			this.laAdminTools.Text = "Admin Tools:";
			// 
			// buttonXRefreshPreview
			// 
			this.buttonXRefreshPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshPreview.Location = new System.Drawing.Point(286, 19);
			this.buttonXRefreshPreview.Name = "buttonXRefreshPreview";
			this.buttonXRefreshPreview.Size = new System.Drawing.Size(118, 26);
			this.buttonXRefreshPreview.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshPreview.TabIndex = 13;
			this.buttonXRefreshPreview.Text = "Refresh QV && WV";
			this.buttonXRefreshPreview.TextColor = System.Drawing.Color.Black;
			this.buttonXRefreshPreview.Click += new System.EventHandler(this.buttonXRefreshPreview_Click);
			// 
			// buttonXOpenQV
			// 
			this.buttonXOpenQV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenQV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenQV.Location = new System.Drawing.Point(0, 19);
			this.buttonXOpenQV.Name = "buttonXOpenQV";
			this.buttonXOpenQV.Size = new System.Drawing.Size(118, 26);
			this.buttonXOpenQV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenQV.TabIndex = 11;
			this.buttonXOpenQV.Text = "!QV Folder";
			this.buttonXOpenQV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenQV.Click += new System.EventHandler(this.buttonXOpenQV_Click);
			// 
			// buttonXOpenWV
			// 
			this.buttonXOpenWV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenWV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenWV.Location = new System.Drawing.Point(143, 19);
			this.buttonXOpenWV.Name = "buttonXOpenWV";
			this.buttonXOpenWV.Size = new System.Drawing.Size(118, 26);
			this.buttonXOpenWV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenWV.TabIndex = 12;
			this.buttonXOpenWV.Text = "!WV Folder";
			this.buttonXOpenWV.TextColor = System.Drawing.Color.Black;
			this.buttonXOpenWV.Click += new System.EventHandler(this.buttonXOpenWV_Click);
			// 
			// LinkEmbeddedOptions
			// 
			this.Controls.Add(this.pnAdminTools);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Size = new System.Drawing.Size(531, 541);
			this.Text = "Sync Settings";
			this.pnAdminTools.ResumeLayout(false);
			this.pnAdminTools.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Panel pnAdminTools;
		public System.Windows.Forms.Label laAdminTools;
		public DevComponents.DotNetBar.ButtonX buttonXRefreshPreview;
		public DevComponents.DotNetBar.ButtonX buttonXOpenQV;
		public DevComponents.DotNetBar.ButtonX buttonXOpenWV;
	}
}
