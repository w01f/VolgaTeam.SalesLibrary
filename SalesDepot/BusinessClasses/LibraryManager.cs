using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SalesDepot.BusinessClasses
{
    class LibraryManager
    {
        private static LibraryManager _instance = new LibraryManager();
        public List<LibraryPackage> LibraryPackageCollection { get; set; }
        public bool OldFormatDetected { get; set; }

        private LibraryManager()
        {
            this.LibraryPackageCollection = new List<LibraryPackage>();
            this.OldFormatDetected = true;
        }

        public static LibraryManager Instance
        {
            get
            {
                return _instance;
            }
        }
        public void LoadLibraryPackages(DirectoryInfo rootFolder)
        {
            this.LibraryPackageCollection.Clear();
            if (rootFolder.Exists)
            {
                foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
                {
                    if (!subFolder.Name.StartsWith("!") && !subFolder.Name.ToLower().Equals("_gsdata_"))
                    {
                        LibraryPackage package = new LibraryPackage(subFolder.Name, subFolder);
                        if (package.LibraryCollection.Count > 0)
                            this.LibraryPackageCollection.Add(package);
                    }
                }
            }
        }
    }
}