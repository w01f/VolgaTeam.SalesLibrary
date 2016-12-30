using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface ILinksGroup
	{
		ILinkGroupSettingsContainer LinkGroupSettingsContainer { get; }
		IEnumerable<BaseLibraryLink> AllLinks { get; }
	}
}
