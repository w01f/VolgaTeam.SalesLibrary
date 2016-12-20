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
			if (ParentImageGroup.Images.Any())
			{
				PageVisible = ParentImageGroup.Images.Any();
				TabControl.SelectedTabPage = this;
			}
		}
	}
}
