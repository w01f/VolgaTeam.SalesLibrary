﻿using System.Collections.Generic;
using System.IO;

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
        public void LoadSalesDepotsPackages(DirectoryInfo rootFolder)
        {
            this.LibraryPackageCollection.Clear();
            if (rootFolder.Exists)
            {
                foreach (DirectoryInfo subFolder in rootFolder.GetDirectories())
                {
                    if (!subFolder.Name.StartsWith("!") && !subFolder.Name.ToLower().Equals("_gsdata_"))
                    {
                        LibraryPackage package = new LibraryPackage(subFolder.Name, subFolder);
                        this.LibraryPackageCollection.Add(package);
                    }
                }
            }
        }
    }
}