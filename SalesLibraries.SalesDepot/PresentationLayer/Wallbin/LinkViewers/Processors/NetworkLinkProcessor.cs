using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(NetworkLink))]
	[IntendForClass(typeof(WebLink))]
	[IntendForClass(typeof(YouTubeLink))]
	class NetworkLinkProcessor : ILinkViewProcessor
	{
		private readonly LibraryObjectLink _link;

		public NetworkLinkProcessor(LibraryObjectLink link)
		{
			_link = link;
		}

		public void Open()
		{
			Utils.OpenFile(_link.FullPath);
		}
	}
}
