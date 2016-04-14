using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(NetworkLink))]
	[IntendForClass(typeof(WebLink))]
	[IntendForClass(typeof(YouTubeLink))]
	[IntendForClass(typeof(QuickSiteLink))]
	[IntendForClass(typeof(AppLink))]
	class HyperLinkProcessor : ILinkViewProcessor
	{
		private readonly LibraryObjectLink _link;

		public HyperLinkProcessor(LibraryObjectLink link)
		{
			_link = link;
		}

		public void Open()
		{
			Utils.OpenFile(_link.OpenPaths);
		}
	}
}
