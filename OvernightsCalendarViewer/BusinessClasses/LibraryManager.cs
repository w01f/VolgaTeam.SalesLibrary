using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OvernightsCalendarViewer.BusinessClasses
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
	}
}