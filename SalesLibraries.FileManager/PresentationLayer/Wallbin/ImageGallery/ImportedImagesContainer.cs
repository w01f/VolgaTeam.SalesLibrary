using System;
using System.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery
{
	class ImportedImagesContainer : RegularImagesContainer
	{
		public ImportedImagesContainer(ImageSourceGroup parent) : base(parent) {}
		protected override void InitPopupMenu()
		{
			base.InitPopupMenu();

			var menuItemRemoveFromImported = new ToolStripMenuItem();
			menuItemRemoveFromImported.Text = "Remove from Imported";
			menuItemRemoveFromImported.Image = Properties.Resources.ButtonDelete;
			menuItemRemoveFromImported.Click += OnRemoveFromFavorites;
			contextMenuStrip.Items.Add(menuItemRemoveFromImported);
		}

		private void OnRemoveFromFavorites(object sender, EventArgs e)
		{
			if (_menuHitInfo == null) return;
			var imageItem = imageListView.Items[_menuHitInfo.ItemIndex];
			var imageSource = imageItem.Tag as BaseImageSource;
			imageSource?.DeleteFromImported();
		}
	}
}
