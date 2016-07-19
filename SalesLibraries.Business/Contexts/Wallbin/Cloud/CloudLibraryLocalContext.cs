using System.Data.Entity;
using SalesLibraries.Business.Schema.Wallbin.Initialization;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Contexts.Wallbin.Cloud
{
	class CloudLibraryLocalContext : CloudLibraryContext
	{
		public CloudLibraryLocalContext(string libraryName, string libraryPath, CloudWallbinManager wallbinManager)
			: base(libraryName, libraryPath, Constants.LocalStorageFileName, wallbinManager)
		{
			Database.SetInitializer( new CloudlWallbinInitializer<CloudLibraryLocalContext>());
			Database.Initialize(true);
			ObjectContext.ObjectMaterialized += OnObjectMaterialized;
		}
	}
}
