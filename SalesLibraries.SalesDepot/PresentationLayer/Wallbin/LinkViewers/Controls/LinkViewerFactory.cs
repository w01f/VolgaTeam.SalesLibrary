using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	static class LinkViewerFactory
	{
		public static ILinkViewer Create(LibraryObjectLink sourceLink)
		{
			return ObjectIntendHelper.GetObjectInstances(
					typeof(ILinkViewer),
					EntitySettingsResolver.ExtractObjectTypeFromProxy(sourceLink.GetType()),
					sourceLink)
				.OfType<ILinkViewer>()
				.FirstOrDefault() ?? new CommonFileViewer(sourceLink);
		}
	}
}
