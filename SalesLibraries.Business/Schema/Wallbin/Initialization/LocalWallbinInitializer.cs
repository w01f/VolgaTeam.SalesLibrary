using System.Linq;
using SalesLibraries.Business.Contexts.Wallbin.Local;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.Business.Schema.Wallbin.Initialization
{
	class LocalWallbinInitializer : WallbinInitializer<LocalLibraryContext>
	{
		protected override void Seed()
		{
			var library = WallbinEntity.CreateEntity<Library>(lib =>
			{
				lib.Context = _context;
				lib.Name = _context.LibraryName;
				lib.Path = _context.DataSourceFolderPath;
			});
			library.ImportLegacyData(_context.DataSourceFolderPath);
			library.BeforeSave();
			if (!library.Pages.Any())
				library.AddPage();
			_context.Libraries.Add(library);
			_context.SaveChanges();
		}
	}
}
