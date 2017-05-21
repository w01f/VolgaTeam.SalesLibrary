using System.Data.Entity;
using System.IO;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Schema.Wallbin.Initialization;

namespace SalesLibraries.BatchTagger.BusinessClasses
{
	class EditTagsLibraryContext : LibraryContext
	{
		public EditTagsLibraryContext(string libraryName, string libraryFilePath)
			: base(libraryName, Path.GetDirectoryName(libraryFilePath), libraryFilePath)
		{
			Database.SetInitializer(new LocalWallbinInitializer());
			Database.Initialize(true);
			ObjectContext.ObjectMaterialized += OnObjectMaterialized;
		}
	}
}
