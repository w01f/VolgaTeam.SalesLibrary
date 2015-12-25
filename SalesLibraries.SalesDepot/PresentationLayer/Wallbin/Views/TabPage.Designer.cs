namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	sealed partial class TabPage
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
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.pnContainer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnEmpty
			// 
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(3, 441);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(766, 45);
			this.pnEmpty.TabIndex = 0;
			// 
			// pnContainer
			// 
			this.pnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContainer.Location = new System.Drawing.Point(393, 3);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(316, 496);
			this.pnContainer.TabIndex = 3;
			// 
			// TabPage
			// 
			this.Appearance.PageClient.BackColor = System.Drawing.Color.White;
			this.Appearance.PageClient.Options.UseBackColor = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SimplePage";
			this.Size = new System.Drawing.Size(442, 454);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnEmpty;
		protected System.Windows.Forms.Panel pnContainer;
	}
}
