using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	internal class LibraryManager
	{
		private static readonly LibraryManager _instance = new LibraryManager();

		private LibraryManager()
		{
			LibraryPackageCollection = new List<LibraryPackage>();
			OldFormatDetected = true;
		}

		public List<LibraryPackage> LibraryPackageCollection { get; set; }
		public bool OldFormatDetected { get; set; }

		public static LibraryManager Instance
		{
			get { return _instance; }
		}

		public void LoadLibraryPackages(DirectoryInfo rootFolder)
		{
			LibraryPackageCollection.Clear();
			if (!rootFolder.Exists) return;
			foreach (var subFolder in rootFolder.GetDirectories())
			{
				if (subFolder.Name.StartsWith("!") || subFolder.Name.ToLower().Equals("_gsdata_")) continue;
				var package = new LibraryPackage(subFolder.Name, subFolder);
				if (package.LibraryCollection.Count > 0)
					LibraryPackageCollection.Add(package);
			}
		}

		public LibraryLink GetLibraryLink(string libraryId, string linkId)
		{
			var library = LibraryPackageCollection.SelectMany(p => p.LibraryCollection).FirstOrDefault(l => l.Identifier.ToString().ToLower().Equals(libraryId.ToLower()));
			return library == null ? null : library.Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files.OfType<LibraryLink>())).FirstOrDefault(l => l.Identifier.ToString().ToLower().Equals(linkId.ToLower()));
		}
	}
}