using System;
using System.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.ImageGallery
{
	class RegularImagesContainer : BaseLinkImagesContainer
	{
		public RegularImagesContainer(ImageSourceGroup parent) : base(parent) { }
		
		protected override void InitPopupMenu()
		{
			var menuItemAddToFavorites = new ToolStripMenuItem();
			menuItemAddToFavorites.Text = "Add To Favorites";
			menuItemAddToFavorites.Image = Properties.Resources.Favorites;
			menuItemAddToFavorites.Click += OnAddToFavorites;
			contextMenuStrip.Items.Add(menuItemAddToFavorites);
		}

		private void OnAddToFavorites(object sender, EventArgs e)
		{
			if (_menuHitInfo == null) return;
			var imageSource = imageListView.Items[_menuHitInfo.ItemIndex].Tag as BaseImageSource;
			imageSource?.CopyToFavs();
		}
	}
}
