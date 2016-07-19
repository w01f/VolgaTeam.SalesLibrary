using System.Data.Entity;
using SalesLibraries.Business.Schema.Wallbin.Initialization;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Contexts.Wallbin.Local
{
	public class LocalLibraryContext : LibraryContext
	{
		public LocalLibraryContext(string libraryName, string libraryPath)
			: base(libraryName, libraryPath, Constants.LocalStorageFileName)
		{
			Database.SetInitializer(new LocalWallbinInitializer());
			Database.Initialize(true);
			ObjectContext.ObjectMaterialized += OnObjectMaterialized;
		}
	}
}
