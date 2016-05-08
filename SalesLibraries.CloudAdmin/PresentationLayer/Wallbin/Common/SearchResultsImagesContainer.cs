using System.Linq;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Common
{
	class SearchResultsImagesContainer : RegularImagesContainer
	{
		public SearchResultsImagesContainer(LinkImageGroup parent) : base(parent)
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
