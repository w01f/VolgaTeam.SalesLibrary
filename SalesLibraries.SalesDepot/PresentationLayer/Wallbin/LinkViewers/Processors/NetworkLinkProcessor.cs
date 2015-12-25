using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(NetworkLink))]
	[IntendForClass(typeof(WebLink))]
	class NetworkLinkProcessor : ILinkViewProcessor
	{
		private readonly LibraryObjectLink _link;

		public NetworkLinkProcessor(LibraryObjectLink link)
		{
			_link = link;
		}

		public void Open()
		{
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Open Link", _link);
			Utils.OpenFile(_link.FullPath);
		}
	}
}
