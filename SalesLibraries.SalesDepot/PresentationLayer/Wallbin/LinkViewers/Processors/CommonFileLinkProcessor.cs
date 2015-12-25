using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(CommonFileLink))]
	class CommonFileLinkProcessor : ILinkViewProcessor
	{
		private readonly LibraryFileLink _link;

		public CommonFileLinkProcessor(LibraryFileLink link)
		{
			_link = link;
		}

		public void Open()
		{
			LinkManager.OpenCopyOfFile(_link);
		}
	}
}
