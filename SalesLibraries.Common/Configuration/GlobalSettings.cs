﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SalesLibraries.Common.Configuration
{
	public static class GlobalSettings
	{
		public static string ApplicationRootPath { get; private set; }
		public static List<string> HiddenObjects { get; private set; }

		static GlobalSettings()
		{
			ApplicationRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			HiddenObjects = new List<string> {
				Constants.WindowsThumbnailFile, 
				Constants.GoodSyncServiceFolderName, 
				Constants.RegularPreviewContainersRootFolderName, 
				Constants.WebPreviewContainersRootFolderName, 
				Constants.OvernightsCalendarRootFolderName, 
				Constants.ProgramManagerRootFolderName, 
				Constants.StorageFileName, 
				Constants.LegacyStorageFileName, 
				Constants.LibrariesJsonFileName, 
				Constants.ShortLibraryInfoFileName, 
			};
		}
	}
}