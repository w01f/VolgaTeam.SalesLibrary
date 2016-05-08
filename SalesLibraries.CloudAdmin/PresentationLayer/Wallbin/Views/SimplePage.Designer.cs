namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	partial class SimplePage
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
			this.pnContainer = new DevExpress.XtraEditors.PanelControl();
			((System.ComponentModel.ISupportInitialize)(this.pnContainer)).BeginInit();
			this.SuspendLayout();
			// 
			// pnEmpty
			// 
			this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnEmpty.Location = new System.Drawing.Point(0, 0);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(775, 496);
			this.pnEmpty.TabIndex = 2;
			// 
			// pnContainer
			// 
			this.pnContainer.Appearance.BackColor = System.Drawing.Color.White;
			this.pnContainer.Appearance.BackColor2 = System.Drawing.Color.White;
			this.pnContainer.Appearance.Options.UseBackColor = true;
			this.pnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContainer.Location = new System.Drawing.Point(0, 0);
			this.pnContainer.Name = "pnContainer";
			this.pnContainer.Size = new System.Drawing.Size(775, 496);
			this.pnContainer.TabIndex = 4;
			// 
			// SimplePage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnContainer);
			this.Controls.Add(this.pnEmpty);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SimplePage";
			this.Size = new System.Drawing.Size(775, 496);
			((System.ComponentModel.ISupportInitialize)(this.pnContainer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.Panel pnEmpty;
		protected DevExpress.XtraEditors.PanelControl pnContainer;
	}
}
