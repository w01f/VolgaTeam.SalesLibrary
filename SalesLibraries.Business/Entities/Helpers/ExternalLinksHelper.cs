using System;
using System.Collections.Generic;
using System.IO;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Synchronization;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class ExternalLinksHelper
	{
		public static bool IsLinkExternal(this LibraryFileLink targetLink)
		{
			return targetLink.FullPath.ToUpper().Contains(Constants.ExternalFilesRootFolderName.ToUpper());
		}

		public static string CopyExternalFile(string sourcePath, string dataSourcePath, Guid destinationLinkId)
		{
			var localStoragePath = GenerateLocalStoragePath(dataSourcePath, destinationLinkId);
			var destinationPath = Path.Combine(localStoragePath, Path.GetFileName(sourcePath));
			try
			{
				File.Copy(sourcePath, destinationPath, true);
			}
			catch { }
			return destinationPath;
		}

		public static string CopyExternalFolder(string sourcePath, string dataSourcePath, Guid destinationLinkId, IList<string> contentWhiteList = null)
		{
			var localStoragePath = GenerateLocalStoragePath(dataSourcePath, destinationLinkId);
			var destinationPath = Path.Combine(localStoragePath, Path.GetFileName(sourcePath));
			var synchronizer = new SynchronizationHelper();
			if (!Directory.Exists(destinationPath))
				synchronizer.CreateFolder(destinationPath);
			var syncOptions = new SynchronizationOptions(
				new DirectoryInfo(sourcePath),
				new DirectoryInfo(destinationPath),
				true);
			if (contentWhiteList != null)
				syncOptions.FilterList = SyncFilterList.Create(contentWhiteList, SyncFilterType.ByWhiteList);
			synchronizer.SynchronizeFolder(syncOptions);
			return destinationPath;
		}

		public static string CopyExternalFiles(string sourcePath, string dataSourcePath, Guid destinationLinkId)
		{
			String destinationPath;
			if (Directory.Exists(sourcePath))
			{
				destinationPath = CopyExternalFolder(sourcePath, dataSourcePath, destinationLinkId);
			}
			else if (File.Exists(sourcePath))
			{
				destinationPath = CopyExternalFile(sourcePath, dataSourcePath, destinationLinkId);
			}
			else
			{
				throw new ArgumentOutOfRangeException("Undefined file link");
			}
			return destinationPath;
		}

		public static void DeleteExternalLink(this LibraryFileLink target)
		{
			if (target.FolderLink != null) return;
			var trashFolderPath = GenerateTrashPath(target.ParentLibrary.Path, target.ExtId);
			var trashPath = Path.Combine(trashFolderPath, target.NameWithExtension);
			if (target.IsFolder)
			{
				var synchronizer = new SynchronizationHelper();
				if (!Directory.Exists(trashPath))
					synchronizer.CreateFolder(trashPath);
				var syncOptions = new SynchronizationOptions(
					new DirectoryInfo(target.FullPath),
					new DirectoryInfo(trashPath),
					true);
				synchronizer.SynchronizeFolder(syncOptions);
				Utils.DeleteFolder(target.FullPath);
			}
			else
			{
				try
				{
					File.Copy(target.FullPath, trashPath, true);
					File.Delete(target.FullPath);
				}
				catch { }
			}

			var localStoragePath = GenerateLocalStoragePath(target.ParentLibrary.Path, target.ExtId);
			if (Directory.Exists(localStoragePath))
				Utils.DeleteFolder(localStoragePath);
		}

		private static string GenerateLocalStoragePath(string dataSourcePath, Guid destinationLinkId)
		{
			var localStoragePath = Path.Combine(
				dataSourcePath,
				Constants.ExternalFilesRootFolderName,
				Constants.ExternalFilesStorageFolderName,
				destinationLinkId.ToString());
			if (!Directory.Exists(localStoragePath))
				Directory.CreateDirectory(localStoragePath);
			return localStoragePath;
		}

		private static string GenerateTrashPath(string dataSourcePath, Guid destinationLinkId)
		{
			var trashFolderPath = Path.Combine(
				dataSourcePath,
				Constants.ExternalFilesRootFolderName,
				Constants.ExternalFilesTrashFolderName,
				DateTime.Now.ToString("mmddyy"),
				destinationLinkId.ToString());
			if (!Directory.Exists(trashFolderPath))
				Directory.CreateDirectory(trashFolderPath);
			return trashFolderPath;
		}
	}
}
