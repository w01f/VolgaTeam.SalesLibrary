using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.Common.Synchronization;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Sync;

namespace SalesLibraries.FileManager.Business.Synchronization
{
	static class SyncManager
	{
		public static void SyncRegular(CancellationToken cancellationToken)
		{
			var targetContext = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage;
			var targetLibrary = targetContext.Library;

			ProcessResetLinkSettingsShedulers(targetLibrary, cancellationToken);

			UpdateFolderContent(targetLibrary, cancellationToken);

			ApplyOriginalFileStateChangesOnAssociatedLink(targetLibrary, cancellationToken);

			UpdatePowerPointInfo(targetLibrary, cancellationToken);

			UpdatePreviewContent(targetLibrary, cancellationToken);

			DeleteDeadLinks(targetLibrary, cancellationToken);

			targetLibrary.SyncDate = DateTime.Now;
			targetContext.SaveChanges();
			if (cancellationToken.IsCancellationRequested) return;
			WebContentManager.GenerateWebContent(targetLibrary);
			if (cancellationToken.IsCancellationRequested) return;
			SyncLibrary(targetLibrary, cancellationToken);
		}

		public static void SyncSilent()
		{
			foreach (var targetContext in MainController.Instance.Wallbin.Libraries)
			{
				var targetLibrary = targetContext.Library;
				var cancellationToken = new CancellationToken();

				ProcessResetLinkSettingsShedulers(targetLibrary, cancellationToken);

				UpdateFolderContent(targetLibrary, cancellationToken);

				ApplyOriginalFileStateChangesOnAssociatedLink(targetLibrary, cancellationToken);

				UpdatePowerPointInfo(targetLibrary, cancellationToken);

				UpdatePreviewContent(targetLibrary, cancellationToken);

				DeleteDeadLinks(targetLibrary, cancellationToken);

				targetLibrary.SyncDate = DateTime.Now;
				targetContext.SaveChanges();
				if (cancellationToken.IsCancellationRequested) return;
				WebContentManager.GenerateWebContent(targetLibrary);
				if (cancellationToken.IsCancellationRequested) return;
				SyncLibrary(targetLibrary, cancellationToken);
			}
		}

		private static void SyncLibrary(Library library, CancellationToken cancellationToken)
		{
			var syncLogs = new List<SyncLog>();

			if (MainController.Instance.Settings.EnableLocalSync)
			{
				var localSyncLog = new SyncLog("Library Sync Manual");
				LibraryFilesSyncHelper.SyncLibraryLocalFiles(library, localSyncLog, cancellationToken);
				if (cancellationToken.IsCancellationRequested) return;
				syncLogs.Add(localSyncLog);
			}

			if (MainController.Instance.Settings.EnableWebSync)
			{
				var webSyncLog = new SyncLog("iPad Sync Manual");
				LibraryFilesSyncHelper.SyncLibraryWebFiles(library, webSyncLog, cancellationToken);
				if (cancellationToken.IsCancellationRequested) return;
				syncLogs.Add(webSyncLog);
			}

			var resultFiles = new List<string>();
			var tempPath = Path.GetTempPath();
			resultFiles.AddRange(syncLogs.Select(log => log.Save(tempPath)));
			resultFiles.Add(Path.Combine(library.Path, Constants.LocalStorageFileName));
			resultFiles.Add(Path.Combine(library.Path, Constants.LibrariesJsonFileName));
			resultFiles.Add(Path.Combine(library.Path, Constants.ShortLibraryInfoFileName));

			var deadLinksFile = Path.Combine(library.Path, Constants.DeadLinkInfoFileName);
			if (File.Exists(deadLinksFile))
				resultFiles.Add(deadLinksFile);

			resultFiles.Add(RemoteResourceManager.Instance.AppSettingsFile.LocalPath);
			var archiveFolderPath = Path.Combine(library.Path, Constants.LogArchiveFolderName);
			if (!Directory.Exists(archiveFolderPath))
				Directory.CreateDirectory(archiveFolderPath);
			var archiveDateTime = DateTime.Now;
			var archiveFilePath = Path.Combine(
				archiveFolderPath,
				String.Format("{0}-{1:MMddyy}-{2:hhmmsstt}.zip", Environment.UserName, archiveDateTime, archiveDateTime));
			Utils.CompressFiles(resultFiles, archiveFilePath);
		}

		private static void ProcessResetLinkSettingsShedulers(Library library, CancellationToken cancellationToken)
		{
			var currentDateTime = DateTime.Now;
			var libraryLinks = library.Pages
				.SelectMany(p => p.AllLinks)
				.Where(l => l.ResetSettingsScheduler.Enabled && l.ResetSettingsScheduler.ResetDate <= currentDateTime)
				.ToList();
			if (!libraryLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;

			libraryLinks.ForEach(libraryLink =>
			{
				libraryLink.ResetToDefault(libraryLink.ResetSettingsScheduler.SettingsGroups);
			});
		}

		private static void UpdateFolderContent(Library library, CancellationToken cancellationToken)
		{
			var folderLinks = library.Pages.SelectMany(p => p.AllLinks).OfType<LibraryFolderLink>().ToList();
			if (!folderLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;
			folderLinks.ForEach(f => f.UpdateContent());
		}


		private static void ApplyOriginalFileStateChangesOnAssociatedLink(Library library, CancellationToken cancellationToken)
		{
			var fileLinks = library.Pages.SelectMany(p => p.AllLinks).OfType<LibraryFileLink>().Where(f => !f.IsFolder).ToList();
			if (!fileLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;
			fileLinks.ForEach(f =>
			{
				f.ApplyOriginalFileStateChangesOnAssociatedLink();
			});
		}

		private static void UpdatePowerPointInfo(Library library, CancellationToken cancellationToken)
		{
			var powerPointFiles = library.Pages.SelectMany(p => p.AllLinks).OfType<PowerPointLink>().ToList();
			if (!powerPointFiles.Any()) return;
			using (var powerPointProcessor = new PowerPointHidden())
			{
				if (!powerPointProcessor.Connect(true)) return;
				foreach (var powerPointLink in powerPointFiles)
				{
					if (cancellationToken.IsCancellationRequested) break;
					((PowerPointLinkSettings)powerPointLink.Settings).UpdatePresentationInfo(powerPointProcessor);
				}
			}
		}

		private static void UpdatePreviewContent(Library library, CancellationToken cancellationToken)
		{
			UpdateQuickViewContent(library, cancellationToken);
			if (cancellationToken.IsCancellationRequested) return;
			UpdateWebPreviewContent(library, cancellationToken);
		}

		private static void UpdateQuickViewContent(Library library, CancellationToken cancellationToken)
		{
			if (!MainController.Instance.Settings.EnableLocalSync)
			{
				try
				{
					Utils.DeleteFolder(Path.Combine(library.Path, Constants.RegularPreviewContainersRootFolderName));
				}
				catch { }
				return;
			}

			var powerPointFiles = library.Pages.SelectMany(p => p.AllLinks).OfType<PowerPointLink>().ToList();
			if (!powerPointFiles.Any()) return;
			using (var powerPointProcessor = new PowerPointHidden())
			{
				if (!powerPointProcessor.Connect(true)) return;
				foreach (var powerPointLink in powerPointFiles)
				{
					if (cancellationToken.IsCancellationRequested) break;
					((PowerPointLinkSettings)powerPointLink.Settings).UpdateQuickViewContent(powerPointProcessor);
				}
			}
		}

		private static void UpdateWebPreviewContent(Library library, CancellationToken cancellationToken)
		{
			var previewContainers = library.PreviewContainers.ToList();
			foreach (var previewContainer in previewContainers)
			{
				if (cancellationToken.IsCancellationRequested) break;
				var previewGenerator = previewContainer.GetPreviewGenerator();
				previewContainer.UpdateContent(previewGenerator, cancellationToken);
			}
		}


		private static void DeleteDeadLinks(Library library, CancellationToken cancellationToken)
		{
			var deadLinksList = library.Pages
				.SelectMany(p => p.AllLinks)
				.OfType<LibraryFileLink>()
				.Where(f => f.IsDead)
				.ToList();

			var deadLinksFile = Path.Combine(library.Path, Constants.DeadLinkInfoFileName);
			if (deadLinksList.Any())
				File.WriteAllLines(deadLinksFile, deadLinksList.Select(f => f.FullPath));
			else if (File.Exists(deadLinksFile))
				try
				{
					File.Delete(deadLinksFile);
				}
				catch
				{
				}

			deadLinksList.ForEach(link => link.DeleteLink());
		}
	}
}
