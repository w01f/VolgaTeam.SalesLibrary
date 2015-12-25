using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	static class LinkProcessorFactory
	{
		public static ILinkViewProcessor Create(LibraryObjectLink link)
		{
			return ObjectIntendHelper.GetObjectInstances(
					typeof(ILinkViewProcessor),
					EFProxyContractResolver.ExtractObjectTypeFromProxy(link.GetType()),
					link)
				.OfType<ILinkViewProcessor>()
				.FirstOrDefault() ?? new CommonFileLinkProcessor((LibraryFileLink)link);
		}
	}
}
