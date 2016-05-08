using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.CloudAdmin.Business.Services
{
	class TaggedLinksManager
	{
		public int TotalLinks { get; private set; }
		public int TaggedLinks { get; private set; }

		public static TaggedLinksManager Instance { get; } = new TaggedLinksManager();

		public void Load(Library library)
		{
			var links = library.Pages.SelectMany(p => p.TopLevelLinks).OfType<LibraryObjectLink>().ToList();
			TotalLinks = links.Count;
			TaggedLinks = links.Count(l => l.Tags.HasCategories);
		}
	}
}
