using System.Linq;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery
{
	class SearchResultsImagesContainer : RegularImagesContainer
	{
		public SearchResultsImagesContainer(ImageSourceGroup parent) : base(parent)
		{
			PageVisible = false;
		}

		protected override void LoadImages()
		{
			base.LoadImages();
			if (_parent.Images.Any())
			{
				PageVisible = _parent.Images.Any();
				TabControl.SelectedTabPage = this;
			}
		}
	}
}
