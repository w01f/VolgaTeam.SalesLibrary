using System;
using System.IO;
using SalesLibraries.Business.Contexts.Wallbin.Local;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Cloner
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
			var wallbinManager = new LocalWallbinManager();
			wallbinManager.LoadLibrary(rootPath);
			foreach (var libraryContext in wallbinManager.Libraries)
			{
				libraryContext.Library.ExtId = Guid.NewGuid();
				foreach (var libraryPage in libraryContext.Library.Pages)
				{
					libraryPage.ExtId = Guid.NewGuid();
					foreach (var libraryFolder in libraryPage.Folders)
						libraryFolder.ExtId = Guid.NewGuid();
					foreach (var columnTitle in libraryPage.ColumnTitles)
						columnTitle.ExtId = Guid.NewGuid();
					foreach (var libraryLink in libraryPage.AllGroupLinks)
					{
						libraryLink.ExtId = Guid.NewGuid();
						if (libraryLink is LibraryFileLink)
							((LibraryFileLink) libraryLink).DataSourceId = libraryContext.Library.ExtId;
					}
				}
				foreach (var previewContainer in libraryContext.Library.PreviewContainers)
				{
					previewContainer.ExtId = Guid.NewGuid();
				}
				libraryContext.SaveChanges();
			}
		}
	}
}
