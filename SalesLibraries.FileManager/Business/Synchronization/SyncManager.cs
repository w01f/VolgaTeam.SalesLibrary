using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
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

namespace SalesLibraries.FileManager.Business.Synchronization
{
	static class SyncManager
	{
		public static void SyncRegular(bool isAutoSync, CancellationToken cancellationToken)
		{
			var targetContext = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage;
			var targetLibrary = targetContext.Library;
			var tempLogFiles = new List<string>();
			var tempPath = Path.GetTempPath();

			ProcessResetLinkSettingsShedulers(targetLibrary, cancellationToken);

			UpdateFolderContent(targetLibrary, cancellationToken);

			ApplyOriginalFileStateChangesOnAssociatedLink(targetLibrary, cancellationToken);

			UpdatePowerPointInfo(targetLibrary, cancellationToken);

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				var oneDriveConnector = new OneDriveConnector();
				AsyncHelper.RunSync(async () =>
				{
					await oneDriveConnector.ProcessLinks(targetLibrary.Pages
						.SelectMany(p => p.AllGroupLinks)
						.OfType<LibraryFileLink>()
						.Where(f => !f.IsDead)
						.ToList()
						, cancellationToken);
				});
				tempLogFiles.Add(oneDriveConnector.SaveLog(tempPath));
			}

			UpdatePreviewContent(targetLibrary, cancellationToken);

			DeleteDeadLinks(targetLibrary, cancellationToken);

			targetLibrary.SyncDate = DateTime.Now;

			var linkActionLogFilePath = GenerateLinkActionLog(targetLibrary, cancellationToken);
			tempLogFiles.Add(linkActionLogFilePath);

			targetContext.SaveChanges();

			if (cancellationToken.IsCancellationRequested) return;
			WebContentManager.GenerateWebContent(targetLibrary);

			if (cancellationToken.IsCancellationRequested) return;
			var syncLogs = SyncLibrary(targetLibrary, isAutoSync, cancellationToken);
			tempLogFiles.AddRange(syncLogs.Select(log => log.Save(tempPath)));

			GenerateSyncLogArchive(tempLogFiles, targetLibrary, cancellationToken);
		}

		public static void SyncSilent()
		{
			foreach (var targetContext in MainController.Instance.Wallbin.Libraries)
			{
				var targetLibrary = targetContext.Library;
				var cancellationToken = new CancellationToken();
				var tempLogFiles = new List<string>();
				var tempPath = Path.GetTempPath();

				ProcessResetLinkSettingsShedulers(targetLibrary, cancellationToken);

				UpdateFolderContent(targetLibrary, cancellationToken);

				ApplyOriginalFileStateChangesOnAssociatedLink(targetLibrary, cancellationToken);

				UpdatePowerPointInfo(targetLibrary, cancellationToken);

				if (MainController.Instance.Settings.OneDriveSettings.Enabled)
				{
					var oneDriveConnector = new OneDriveConnector();
					AsyncHelper.RunSync(async () =>
					{
						await oneDriveConnector.ProcessLinks(targetLibrary.Pages
								.SelectMany(p => p.AllGroupLinks)
								.OfType<LibraryFileLink>()
								.Where(f => !f.IsDead)
								.ToList()
							, cancellationToken);
					});
					tempLogFiles.Add(oneDriveConnector.SaveLog(tempPath));
				}

				UpdatePreviewContent(targetLibrary, cancellationToken);

				DeleteDeadLinks(targetLibrary, cancellationToken);

				targetLibrary.SyncDate = DateTime.Now;

				var linkActionLogFilePath = GenerateLinkActionLog(targetLibrary, cancellationToken);
				tempLogFiles.Add(linkActionLogFilePath);

				targetContext.SaveChanges();

				if (cancellationToken.IsCancellationRequested) return;
				WebContentManager.GenerateWebContent(targetLibrary);

				if (cancellationToken.IsCancellationRequested) return;
				var syncLogs = SyncLibrary(targetLibrary, false, cancellationToken);
				tempLogFiles.AddRange(syncLogs.Select(log => log.Save(tempPath)));

				GenerateSyncLogArchive(tempLogFiles, targetLibrary, cancellationToken);
			}
		}

		public static void ProcessSyncException(Exception ex)
		{
			if (ex is PreviewGenerationException)
			{
				var previewGenerationexception = (PreviewGenerationException)ex;
				if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("{0}.{1}Do you want to open log file?", previewGenerationexception.Message, Environment.NewLine)) == DialogResult.Yes)
					Utils.OpenFile(previewGenerationexception.LogPath);
			}
			else
				MainController.Instance.PopupMessages.ShowWarning(ex.Message);
		}

		private static IList<SyncLog> SyncLibrary(Library library, bool isAutoSync, CancellationToken cancellationToken)
		{
			var syncLogs = new List<SyncLog>();

			if (MainController.Instance.Settings.EnableLocalSync)
			{
				var localSyncLog = isAutoSync && MainController.Instance.Settings.IdleSettings.Enabled && MainController.Instance.Settings.IdleSettings.SyncOnClose ?
					new SyncLog("Library Sync", true, MainController.Instance.Settings.IdleSettings.InactivityMinutesTimeout) :
					new SyncLog("Library Sync");
				LibraryFilesSyncHelper.SyncLibraryLocalFiles(library, localSyncLog, cancellationToken);
				if (cancellationToken.IsCancellationRequested) return syncLogs;
				syncLogs.Add(localSyncLog);
			}

			if (MainController.Instance.Settings.EnableWebSync)
			{
				var webSyncLog = isAutoSync && MainController.Instance.Settings.IdleSettings.Enabled && MainController.Instance.Settings.IdleSettings.SyncOnClose ?
					new SyncLog("iPad Sync", true, MainController.Instance.Settings.IdleSettings.InactivityMinutesTimeout) :
					new SyncLog("iPad Sync");
				LibraryFilesSyncHelper.SyncLibraryWebFiles(library, webSyncLog, cancellationToken);
				if (cancellationToken.IsCancellationRequested) return syncLogs;
				syncLogs.Add(webSyncLog);
			}

			return syncLogs;
		}

		private static void ProcessResetLinkSettingsShedulers(Library library, CancellationToken cancellationToken)
		{
			var currentDateTime = DateTime.Now;
			var libraryLinks = library.Pages
				.SelectMany(p => p.AllGroupLinks)
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
			var folderLinks = library.Pages.SelectMany(p => p.AllGroupLinks).OfType<LibraryFolderLink>().ToList();
			if (!folderLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;
			folderLinks.ForEach(f => f.UpdateContent());
		}

		private static void ApplyOriginalFileStateChangesOnAssociatedLink(Library library, CancellationToken cancellationToken)
		{
			var fileLinks = library.Pages.SelectMany(p => p.AllGroupLinks).OfType<LibraryFileLink>().Where(f => !f.IsFolder).ToList();
			if (!fileLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;
			fileLinks.ForEach(f =>
			{
				f.ApplyOriginalFileStateChangesOnAssociatedLink();
			});
		}

		private static void UpdatePowerPointInfo(Library library, CancellationToken cancellationToken)
		{
			var powerPointFiles = library.Pages.SelectMany(p => p.AllGroupLinks).OfType<PowerPointLink>().ToList();
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

			var powerPointFiles = library.Pages.SelectMany(p => p.AllGroupLinks).OfType<PowerPointLink>().ToList();
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
				.SelectMany(p => p.AllGroupLinks)
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

		private static string GenerateLinkActionLog(Library library, CancellationToken cancellationToken)
		{
			var addActionRecords = new List<LinkActionLog>();
			var deleteActionRecords = new List<LinkActionLog>();
			var changeActionRecords = new List<LinkActionLog>();

			foreach (var linkActionRecord in library.LinkActions.OrderBy(record => record.ActionType).ToList())
			{
				switch (linkActionRecord.ActionType)
				{
					case LinkActionType.Add:
						if (addActionRecords.All(record => record.Settings.ExtId != linkActionRecord.Settings.ExtId))
							addActionRecords.Add(linkActionRecord);
						break;
					case LinkActionType.Delete:
						if (deleteActionRecords.All(record => record.Settings.ExtId != linkActionRecord.Settings.ExtId))
							deleteActionRecords.Add(linkActionRecord);
						break;
					default:
						if (addActionRecords.All(record => record.Settings.ExtId != linkActionRecord.Settings.ExtId) &&
							deleteActionRecords.All(record => record.Settings.ExtId != linkActionRecord.Settings.ExtId) &&
							changeActionRecords.All(record => record.Settings.ExtId != linkActionRecord.Settings.ExtId))
							changeActionRecords.Add(linkActionRecord);
						break;
				}
			}

			var logText = new StringBuilder();

			logText.AppendLine(String.Format("Link Summary: {0}", library.SyncDate?.ToString("MM-dd-yy h:mm tt")));
			logText.AppendLine(String.Empty);

			logText.AppendLine("*Links Added:");
			foreach (var actionRecord in addActionRecords)
				logText.AppendLine(String.Format("{0} ({1})", actionRecord.Settings.Name, actionRecord.Settings.Path));
			logText.AppendLine(String.Empty);
			logText.AppendLine(String.Empty);
			logText.AppendLine(String.Empty);

			logText.AppendLine("*Links Removed:");
			foreach (var actionRecord in deleteActionRecords)
				logText.AppendLine(String.Format("{0} ({1})", actionRecord.Settings.Name, actionRecord.Settings.Path));
			logText.AppendLine(String.Empty);
			logText.AppendLine(String.Empty);
			logText.AppendLine(String.Empty);

			logText.AppendLine("*Links Changed:");
			foreach (var actionRecord in changeActionRecords)
				logText.AppendLine(String.Format("{0} ({1})", actionRecord.Settings.Name, actionRecord.Settings.Path));

			var logFilePath = Path.Combine(Path.GetTempPath(), "link_summary.txt");
			File.WriteAllText(logFilePath, logText.ToString());

			library.ClearLinkActionLog();

			return logFilePath;
		}

		private static void GenerateSyncLogArchive(IList<string> tempFiles, Library library, CancellationToken cancellationToken)
		{
			var resultFiles = new List<string>();

			resultFiles.AddRange(tempFiles);

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
	}
}
