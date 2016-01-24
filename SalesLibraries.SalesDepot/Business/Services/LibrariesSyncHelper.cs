using System.Collections.Generic;
using System.IO;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Synchronization;

namespace SalesLibraries.SalesDepot.Business.Services
{
	static class LibrariesSyncHelper
	{
		public static void Sync(IEnumerable<string> sourcePaths, string destinationPath)
		{
			foreach (var sourcePath in sourcePaths)
			{
				if (!Directory.Exists(sourcePath)) continue;
				if (!Directory.Exists(destinationPath)) continue;

				foreach (var sourceLibraryPath in Directory.GetDirectories(sourcePath))
				{
					var legacyLibraryPath = Path.Combine(sourceLibraryPath, Constants.OldPrimaryFileStorageName);
					var sourceLibraryCachePath = Directory.Exists(legacyLibraryPath) ? legacyLibraryPath : sourceLibraryPath;

					var libraryFolderName = Path.GetFileName(sourceLibraryPath);

					var sourceLibraryCacheFile = File.Exists(Path.Combine(sourceLibraryCachePath, Constants.StorageFileName)) ?
						new FileInfo(Path.Combine(sourceLibraryCachePath, Constants.StorageFileName)) :
						new FileInfo(Path.Combine(sourceLibraryCachePath, Constants.LegacyStorageFileName));

					if (!sourceLibraryCacheFile.Exists) continue;

					var destinationLibraryPath = Path.Combine(destinationPath, libraryFolderName);
					if (!Directory.Exists(destinationLibraryPath))
						Directory.CreateDirectory(destinationLibraryPath);

					var destinationLibraryCacheFile = new FileInfo(Path.Combine(destinationLibraryPath, sourceLibraryCacheFile.Name));
					if (!destinationLibraryCacheFile.Exists ||
						destinationLibraryCacheFile.LastWriteTime < sourceLibraryCacheFile.LastWriteTime ||
						destinationLibraryCacheFile.Length != sourceLibraryCacheFile.Length
						)
					{
						var syncHelper = new SynchronizationHelper();
						var syncOptions = new SynchronizationOptions(
							new DirectoryInfo(sourceLibraryCachePath),
							new DirectoryInfo(destinationLibraryPath),
							true
							);
						syncHelper.SynchronizeFolder(syncOptions);
					}
				}
				break;
			}
		}
	}
}
