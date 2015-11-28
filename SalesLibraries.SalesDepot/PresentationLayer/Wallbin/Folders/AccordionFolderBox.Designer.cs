namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders
{
	partial class AccordionFolderBox
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
		protected override void InitializeComponent()
		{
			base.InitializeComponent();

			this.buttonXHeader = new DevComponents.DotNetBar.ButtonX();
			this.pnHeader.SuspendLayout();
			this.pnHeaderBorder.SuspendLayout();
			this.pnBorders.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.buttonXHeader);
			this.pnHeader.Size = new System.Drawing.Size(269, 50);
			// 
			// pnHeaderBorder
			// 
			this.pnHeaderBorder.Size = new System.Drawing.Size(269, 51);
			// 
			// pnBorders
			// 
			this.pnBorders.Location = new System.Drawing.Point(20, 20);
			this.pnBorders.Size = new System.Drawing.Size(271, 268);
			// 
			// buttonXHeader
			// 
			this.buttonXHeader.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHeader.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonXHeader.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXHeader.Location = new System.Drawing.Point(0, 0);
			this.buttonXHeader.Name = "buttonXHeader";
			this.buttonXHeader.Size = new System.Drawing.Size(269, 49);
			this.buttonXHeader.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXHeader.TabIndex = 8;
			this.buttonXHeader.Text = "buttonX1 <font color=\"#595959\">(test)</font>";
			this.buttonXHeader.TextColor = System.Drawing.Color.Black;
			this.buttonXHeader.UseMnemonic = false;
			this.buttonXHeader.Click += new System.EventHandler(this.buttonXHeader_Click);
			// 
			// AccordionFolderBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Name = "AccordionFolderBox";
			this.Padding = new System.Windows.Forms.Padding(20);
			this.pnHeader.ResumeLayout(false);
			this.pnHeaderBorder.ResumeLayout(false);
			this.pnBorders.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXHeader;
	}
}
