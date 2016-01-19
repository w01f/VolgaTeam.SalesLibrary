namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	sealed partial class LinkImagesContainer
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
			this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.imageListView = new Manina.Windows.Forms.ImageListView();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFavoritesToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(174, 36);
			// 
			// addToFavoritesToolStripMenuItem
			// 
			this.addToFavoritesToolStripMenuItem.Image = global::SalesLibraries.FileManager.Properties.Resources.Favorites;
			this.addToFavoritesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
			this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(173, 32);
			this.addToFavoritesToolStripMenuItem.Text = "Add To Favorites";
			this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
			// 
			// toolTip
			// 
			this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.toolTip.UseAnimation = false;
			this.toolTip.UseFading = false;
			// 
			// imageListView
			// 
			this.imageListView.AllowDrag = true;
			this.imageListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.imageListView.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.imageListView.ContextMenuStrip = this.contextMenuStrip;
			this.imageListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageListView.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.imageListView.Location = new System.Drawing.Point(0, 0);
			this.imageListView.MultiSelect = false;
			this.imageListView.Name = "imageListView";
			this.imageListView.PersistentCacheDirectory = "";
			this.imageListView.PersistentCacheSize = ((long)(100));
			this.imageListView.Size = new System.Drawing.Size(667, 503);
			this.imageListView.TabIndex = 40;
			this.imageListView.ThumbnailSize = new System.Drawing.Size(64, 64);
			// 
			// LinkImagesContainer
			// 
			this.Controls.Add(this.imageListView);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(667, 503);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip;
		private Manina.Windows.Forms.ImageListView imageListView;
	}
}
