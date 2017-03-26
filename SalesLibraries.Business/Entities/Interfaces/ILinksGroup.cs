using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface ILinksGroup
	{
		string LinkGroupName { get; }
		ILinkGroupSettingsContainer LinkGroupSettingsContainer { get; }
		IEnumerable<BaseLibraryLink> AllGroupLinks { get; }
	}
}
