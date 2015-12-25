using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.Business.Services
{
	class TaggedLinksManager
	{
		private static readonly TaggedLinksManager _instance = new TaggedLinksManager();

		public int TotalLinks { get; private set; }
		public int TaggedLinks { get; private set; }

		public static TaggedLinksManager Instance
		{
			get { return _instance; }
		}

		public void Load(Library library)
		{
			var links = library.Pages.SelectMany(p => p.TopLevelLinks).OfType<LibraryObjectLink>().ToList();
			TotalLinks = links.Count;
			TaggedLinks = links.Count(l => l.Tags.HasCategories);
		}
	}
}
