using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;

namespace SalesLibraries.FileManager.Business.Services
{
	static class CorruptedLinksHelper
	{
		public static void DeleteCorruptedLinks(Library targetLibrary)
		{
			var corruptedLinks = targetLibrary.Pages
				.SelectMany(page => page.AllGroupLinks)
				.OfType<LibraryObjectLink>()
				.Where(link => link.IsCorrupted)
				.ToList();
			if (!corruptedLinks.Any()) return;
			DataStateObserver.Instance.RaiseLinksDeleted(corruptedLinks.Select(link => link.ExtId));
			foreach (var libraryObjectLink in corruptedLinks)
				libraryObjectLink.DeleteLink();
		}
	}
}
