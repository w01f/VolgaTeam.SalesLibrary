using System.Linq;

namespace SalesLibraries.Business.Contexts.Wallbin.Cloud
{
	public abstract class CloudLibraryContext : LibraryContext
	{
		public CloudWallbinManager WallbinManager { get; private set; }
		public bool HasData => Libraries.Any();

		protected CloudLibraryContext(string libraryName, string libraryPath, string libraryFileName, CloudWallbinManager wallbinManager)
			: base(libraryName, libraryPath, libraryFileName)
		{
			WallbinManager = wallbinManager;
		}
	}
}
