using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.Business.Services
{
	class TaggedLinksManager
	{
		public int TotalLibraryLinks { get; private set; }
		public int TaggedLibraryLinks { get; private set; }
		public int TotalPageLinks { get; private set; }
		public int TaggedPageLinks { get; private set; }

		public static TaggedLinksManager Instance { get; } = new TaggedLinksManager();

		public void Load(Library library)
		{
			var links = library.Pages.SelectMany(p => p.TopLevelLinks).OfType<LibraryObjectLink>().ToList();
			TotalLibraryLinks = links.Count;
			TaggedLibraryLinks = links.Count(l => l.Tags.HasCategories);
		}

		public void Load(LibraryPage libraryPage)
		{
			Load(libraryPage.Library);

			var links = libraryPage.TopLevelLinks.OfType<LibraryObjectLink>().ToList();
			TotalPageLinks = links.Count;
			TaggedPageLinks = links.Count(l => l.Tags.HasCategories);
		}
	}
}
