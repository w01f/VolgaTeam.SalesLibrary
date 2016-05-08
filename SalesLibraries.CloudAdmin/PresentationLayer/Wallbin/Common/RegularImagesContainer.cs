using System;
using System.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Common
{
	class RegularImagesContainer : BaseLinkImagesContainer
	{
		public RegularImagesContainer(LinkImageGroup parent) : base(parent) { }
		
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
			var imageSource = imageListView.Items[_menuHitInfo.ItemIndex].Tag as LinkImageSource;
			if (imageSource == null) return;
			imageSource.CopyToFavs();
		}
	}
}
