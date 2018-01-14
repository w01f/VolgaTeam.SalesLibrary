namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemAddFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddFolder});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(179, 26);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// toolStripMenuItemAddFolder
			// 
			this.toolStripMenuItemAddFolder.Name = "toolStripMenuItemAddFolder";
			this.toolStripMenuItemAddFolder.Size = new System.Drawing.Size(178, 22);
			this.toolStripMenuItemAddFolder.Text = "Add a Window here";
			this.toolStripMenuItemAddFolder.Click += new System.EventHandler(this.toolStripMenuItemAddFolder_Click);
			// 
			// PageContent
			// 
			this.AllowDrop = true;
			this.AlwaysScrollActiveControlIntoView = false;
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Size = new System.Drawing.Size(632, 535);
			this.Click += new System.EventHandler(this.OnPageContentClick);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProcessDragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.ProcessDragOver);
			this.DragLeave += new System.EventHandler(this.ProcessDragLeave);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.OnLayout);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddFolder;
	}
}
