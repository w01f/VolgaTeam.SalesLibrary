namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	sealed partial class PageContent
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
			this.SuspendLayout();
			// 
			// PageContent
			// 
			this.AllowDrop = true;
			this.AlwaysScrollActiveControlIntoView = false;
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Size = new System.Drawing.Size(632, 535);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProcessDragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.ProcessDragOver);
			this.DragLeave += new System.EventHandler(this.ProcessDragLeave);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.OnLayout);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.ResumeLayout(false);

		}

		#endregion

	}
}
