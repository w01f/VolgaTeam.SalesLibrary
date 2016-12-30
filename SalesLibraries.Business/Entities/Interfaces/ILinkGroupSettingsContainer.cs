using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkGroupSettings;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface ILinkGroupSettingsContainer
	{
		ILinkGroupSettingsContainer ParentLinkSettingsContainer { get; }
		List<LinkGroupSettingsTemplate> LinkSettingsTemplates { get; }
		void OnSettingsChanged();
	}
}
