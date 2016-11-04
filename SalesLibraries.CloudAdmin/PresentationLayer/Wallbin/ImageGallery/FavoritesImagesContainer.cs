using System;
using System.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.ImageGallery
{
	class FavoritesImagesContainer : BaseLinkImagesContainer
	{
		public FavoritesImagesContainer(ImageSourceGroup parent) : base(parent) {}
		protected override void InitPopupMenu()
		{
			var menuItemRemoveFromFavorites = new ToolStripMenuItem();
			menuItemRemoveFromFavorites.Text = "Remove from Favorites";
			menuItemRemoveFromFavorites.Image = Properties.Resources.ButtonDelete;
			menuItemRemoveFromFavorites.Click += OnRemoveFromFavorites;
			contextMenuStrip.Items.Add(menuItemRemoveFromFavorites);
		}

		private void OnRemoveFromFavorites(object sender, EventArgs e)
		{
			if (_menuHitInfo == null) return;
			var imageItem = imageListView.Items[_menuHitInfo.ItemIndex];
			var imageSource = imageItem.Tag as BaseImageSource;
			imageSource?.DeleteFromFavs();
		}
	}
}
