using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Synchronization;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.Synchronization
{
	static class LibraryFilesSyncHelper
	{
		public static void SyncLibraryLocalFiles(Library library, SyncLog syncLog, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested) return;

			syncLog.StartLogging();
			syncLog.AddRecord(string.Format("Sync {0}", library.Name));

			var result = SyncPrimaryRoot(
				library,
				false,
				syncLog,
				cancellationToken);

			if (result != SynchronizationResult.Completed || cancellationToken.IsCancellationRequested) return;

			result = SyncSpecialFolder(
				Path.Combine(library.Path, Constants.RegularPreviewContainersRootFolderName),
				Path.Combine(GetLibrarySyncDestinationPath(library, false), Constants.RegularPreviewContainersRootFolderName),
				syncLog,
				cancellationToken);
			if (result != SynchronizationResult.Completed || cancellationToken.IsCancellationRequested) return;

			if (library.Calendar.Enabled)
			{
				result = SyncSpecialFolder(
					library.Calendar.Path,
					Path.Combine(GetLibrarySyncDestinationPath(library, false), Constants.OvernightsCalendarRootFolderName),
					syncLog,
					cancellationToken);
				if (result != SynchronizationResult.Completed || cancellationToken.IsCancellationRequested) return;
			}

			if (library.ProgramData.Enable)
			{
				result = SyncSpecialFolder(
					library.ProgramData.Path,
					Path.Combine(GetLibrarySyncDestinationPath(library, false), Constants.ProgramManagerRootFolderName),
					syncLog,
					cancellationToken);
				if (result != SynchronizationResult.Completed || cancellationToken.IsCancellationRequested) return;
			}

			syncLog.FinishLoging();
		}

		public static void SyncLibraryWebFiles(Library library, SyncLog syncLog, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested) return;

			syncLog.StartLogging();
			syncLog.AddRecord(string.Format("Sync {0}", library.Name));

			var result = SyncPrimaryRoot(
				library,
				true,
				syncLog,
				cancellationToken);

			if (result != SynchronizationResult.Completed || cancellationToken.IsCancellationRequested) return;

			result = SyncSpecialFolder(
				Path.Combine(library.Path, Constants.WebPreviewContainersRootFolderName),
				Path.Combine(GetLibrarySyncDestinationPath(library, true), Constants.WebPreviewContainersRootFolderName),
				syncLog,
				cancellationToken);
			if (result != SynchronizationResult.Completed || cancellationToken.IsCancellationRequested) return;

			syncLog.FinishLoging();
		}

		private static SynchronizationResult SyncPrimaryRoot(
			Library library,
			bool isWebSync,
			SyncLog syncLog,
			CancellationToken cancellationToken)
		{
			var synchronizer = new SynchronizationHelper();
			synchronizer.FileSynchronized += syncLog.OnFileSynchronized;
			synchronizer.FolderSynchronized += syncLog.OnFolderSynchronized;

			var whiteListFolderNames = GetSyncedSpecialFolders(library, isWebSync);
			synchronizer.FolderSynchronizing += (o, e) =>
			{
				e.Cancel = whiteListFolderNames.Contains(Path.GetFileName(e.DestinationFilePath));
			};
			synchronizer.FileSynchronizing += (o, e) =>
			{
				if (cancellationToken.IsCancellationRequested)
					synchronizer.Abort(SynchronizationResult.AbortedDueToShutDown);
			};
			synchronizer.SynchronizationCompleting += (o, e) =>
			{
				if (e.Result != SynchronizationResult.Completed)
					syncLog.AbortLoging();
			};

			var filesWhiteListItems = library.Pages
				.SelectMany(p => p.AllLinks)
				.OfType<LibraryFileLink>()
				.Where(link => link.DataSourceId == library.DataSourceId)
				.Select(link => link.FullPath)
				.ToList();
			if (isWebSync)
			{
				filesWhiteListItems.Add(Path.Combine(library.Path, Constants.LibrariesJsonFileName));
				filesWhiteListItems.Add(Path.Combine(library.Path, Constants.ShortLibraryInfoFileName));
			}
			else
				filesWhiteListItems.Add(Path.Combine(library.Path, Constants.LocalStorageFileName));

			var destinationPath = GetLibrarySyncDestinationPath(library, isWebSync);

			var syncOptions = new SynchronizationOptions(
				new DirectoryInfo(library.Path),
				new DirectoryInfo(destinationPath),
				true);
			syncOptions.FilterList = SyncFilterList.Create(filesWhiteListItems, SyncFilterType.ByWhiteList);

			if (!Directory.Exists(destinationPath))
				synchronizer.CreateFolder(destinationPath);

			return synchronizer.SynchronizeFolder(syncOptions);
		}

		private static SynchronizationResult SyncSpecialFolder(
			string specialFolderPath,
			string destinationFolderPath,
			SyncLog syncLog,
			CancellationToken cancellationToken)
		{
			var synchronizer = new SynchronizationHelper();
			synchronizer.FileSynchronized += syncLog.OnFileSynchronized;
			synchronizer.FolderSynchronized += syncLog.OnFolderSynchronized;
			synchronizer.FileSynchronizing += (o, e) =>
			{
				if (cancellationToken.IsCancellationRequested)
					synchronizer.Abort(SynchronizationResult.AbortedDueToShutDown);
			};
			synchronizer.SynchronizationCompleting += (o, e) =>
			{
				if (e.Result != SynchronizationResult.Completed)
					syncLog.AbortLoging();
			};

			if (!Directory.Exists(destinationFolderPath))
				synchronizer.CreateFolder(destinationFolderPath);

			var syncOptions = new SynchronizationOptions(
				new DirectoryInfo(specialFolderPath),
				new DirectoryInfo(destinationFolderPath),
				true);

			return synchronizer.SynchronizeFolder(syncOptions);
		}

		private static HashSet<string> GetSyncedSpecialFolders(Library library, bool isWebSync)
		{
			var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			result.Add(isWebSync ?
				Constants.WebPreviewContainersRootFolderName :
				Constants.RegularPreviewContainersRootFolderName);

			if (library.Calendar.Enabled)
				result.Add(Constants.OvernightsCalendarRootFolderName);

			if (library.ProgramData.Enable)
				result.Add(Constants.ProgramManagerRootFolderName);

			result.Add(Constants.GoodSyncServiceFolderName);

			return result;
		}

		private static string GetLibrarySyncDestinationPath(IDataSource library, bool isWebSync)
		{
			var destinationRoot = isWebSync ?
				MainController.Instance.Settings.WebPath :
				MainController.Instance.Settings.NetworkPath;
			if (library.Name == Constants.PrimaryFileStorageName)
				return destinationRoot;
			return Path.Combine(destinationRoot, library.Name);
		}
	}
}
