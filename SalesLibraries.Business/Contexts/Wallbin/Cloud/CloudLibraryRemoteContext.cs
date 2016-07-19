using System.Data.Entity;
using SalesLibraries.Business.Schema.Wallbin.Initialization;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Contexts.Wallbin.Cloud
{
	class CloudLibraryRemoteContext : CloudLibraryContext
	{
		public CloudLibraryRemoteContext(string libraryName, string libraryPath, CloudWallbinManager wallbinManager)
			: base(libraryName, libraryPath, Constants.RemoteStorageFileName, wallbinManager)
		{
			Database.SetInitializer(new CloudlWallbinInitializer<CloudLibraryRemoteContext>());
			Database.Initialize(true);
			ObjectContext.ObjectMaterialized += OnObjectMaterialized;
		}
	}
}
