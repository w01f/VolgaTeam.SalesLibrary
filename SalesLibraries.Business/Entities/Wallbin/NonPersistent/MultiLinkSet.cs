using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class MultiLinkSet : ILinksGroup
	{
		public ILinkGroupSettingsContainer LinkGroupSettingsContainer { get; } = null;
		public IEnumerable<BaseLibraryLink> AllLinks { get; }

		public MultiLinkSet(IEnumerable<BaseLibraryLink> links)
		{
			AllLinks = links.ToList();
		}
	}
}
