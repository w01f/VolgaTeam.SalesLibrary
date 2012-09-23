using System;
using System.Collections.Generic;
using System.IO;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.BusinessClasses
{
    public class LibraryPackage
    {
        private string _name;
        public DirectoryInfo Folder { get; set; }
        private List<Library> _libraryCollection = new List<Library>();

        public List<Library> LibraryCollection
        {
            get
            {
                return _libraryCollection;
            }
        }

        public string Name
        {
            get
            {
                if (_name.Equals(CoreObjects.BusinessClasses.Constants.WholeDriveFilesStorage))
                {
                    if (_libraryCollection.Count > 0 && !string.IsNullOrEmpty(_libraryCollection[0].BrandingText))
                        return _libraryCollection[0].BrandingText;
                    else
                        return ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
                }
                else
                    return _name;
            }
        }

        public LibraryPackage(string name, DirectoryInfo folder)
        {
            this.Folder = folder;
            _name = name;
            LoadSalesDepots();
        }

        private void LoadSalesDepots()
        {
            Library library = null;
            if (this.Folder.GetFiles("*.xml").Length > 0 && !this.Folder.Name.ToLower().Equals("_gsdata_"))
            {
                library = new Library(this, this.Folder.Name.Equals(CoreObjects.BusinessClasses.Constants.WholeDriveFilesStorage) ? this.Folder.Parent.Name : this.Folder.Name, this.Folder);
                if (library != null && library.IsConfigured && (ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Count == 0 || ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Contains(library.Name.ToLower())))
                    _libraryCollection.Add(library);
            }
            else
                foreach (DirectoryInfo subFolder in this.Folder.GetDirectories())
                    if (!subFolder.Name.StartsWith("!") && !subFolder.Name.ToLower().Equals("_gsdata_"))
                    {
                        DirectoryInfo primaryRootFolder = new DirectoryInfo(Path.Combine(subFolder.FullName, CoreObjects.BusinessClasses.Constants.WholeDriveFilesStorage));
                        if (primaryRootFolder.Exists)
                            library = new Library(this, primaryRootFolder.Parent.Name, primaryRootFolder);
                        else
                            library = new Library(this, subFolder.Name, subFolder);
                        if (library != null && library.IsConfigured && (ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Count == 0 || ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Contains(library.Name.ToLower())))
                            _libraryCollection.Add(library);
                    }
        }

        public ILibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (Library library in _libraryCollection)
                searchFiles.AddRange(library.SearchByTags(searchCriteria));
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (Library library in _libraryCollection)
                searchFiles.AddRange(library.SearchByName(template, fullMatchOnly, type));
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (Library library in _libraryCollection)
                searchFiles.AddRange(library.SearchByDate(startDate, endDate));
            return searchFiles.ToArray();
        }
    }
}
