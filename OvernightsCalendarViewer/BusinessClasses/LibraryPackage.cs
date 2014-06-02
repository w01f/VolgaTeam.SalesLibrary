using System.Collections.Generic;
using System.IO;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OvernightsCalendarViewer.BusinessClasses
{
	public class LibraryPackage
	{
		private readonly List<Library> _libraryCollection = new List<Library>();
		private readonly string _name;

		public LibraryPackage(string name, DirectoryInfo folder)
		{
			Folder = folder;
			_name = name;
			LoadSalesDepots();
		}

		public DirectoryInfo Folder { get; set; }

		public List<Library> LibraryCollection
		{
			get { return _libraryCollection; }
		}

		public string Name
		{
			get
			{
				if (_name.Equals(Constants.WholeDriveFilesStorage))
				{
					if (_libraryCollection.Count > 0 && !string.IsNullOrEmpty(_libraryCollection[0].BrandingText))
						return _libraryCollection[0].BrandingText;
					return "Overnights";
				}
				return _name;
			}
		}

		private void LoadSalesDepots()
		{
			Library library;
			if (Folder.GetFiles("*.xml").Length > 0 && !Folder.Name.ToLower().Equals("_gsdata_"))
			{
				library = new Library(this, Folder.Name.Equals(Constants.WholeDriveFilesStorage) ? Folder.Parent.Name : Folder.Name, Folder);
				if (library != null && library.IsConfigured )
					_libraryCollection.Add(library);
			}
			else
				foreach (var subFolder in Folder.GetDirectories())
					if (!subFolder.Name.StartsWith("!") && !subFolder.Name.ToLower().Equals("_gsdata_"))
					{
						var primaryRootFolder = new DirectoryInfo(Path.Combine(subFolder.FullName, Constants.WholeDriveFilesStorage));
						if (primaryRootFolder.Exists)
							library = new Library(this, primaryRootFolder.Parent.Name, primaryRootFolder);
						else
							library = new Library(this, subFolder.Name, subFolder);
						if (library != null && library.IsConfigured)
							_libraryCollection.Add(library);
					}
		}
	}
}