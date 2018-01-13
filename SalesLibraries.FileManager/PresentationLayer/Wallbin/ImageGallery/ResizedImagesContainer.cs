using System;
using System.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery
{
	class ResizedImagesContainer : RegularImagesContainer
	{
		public ResizedImagesContainer(ImageSourceGroup parent) : base(parent) {}
		protected override void InitPopupMenu()
		{
			base.InitPopupMenu();

			var menuItemRemoveFromResized = new ToolStripMenuItem();
			menuItemRemoveFromResized.Text = "Remove from Resized";
			menuItemRemoveFromResized.Image = Properties.Resources.ButtonDelete;
			menuItemRemoveFromResized.Click += OnRemoveFromResized;
			contextMenuStrip.Items.Add(menuItemRemoveFromResized);
		}

		private void OnRemoveFromResized(object sender, EventArgs e)
		{
			if (_menuHitInfo == null) return;
			var imageItem = imageListView.Items[_menuHitInfo.ItemIndex];
			var imageSource = imageItem.Tag as BaseImageSource;
			imageSource?.DeleteFromResized();
		}
	}
}
