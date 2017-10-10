using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	interface IResetLibraryLinkSettingsControl:IResetLibraryContentControl
	{
		IList<LinkSettingsGroupType> GetSelectedSettingsGroups();
	}
}
