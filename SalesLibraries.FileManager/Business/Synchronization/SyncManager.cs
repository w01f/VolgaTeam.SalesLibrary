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
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
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
		public static void SyncRegular()
		{
			MainController.Instance.ProcessChanges();
			if (MainController.Instance.WallbinViews.ActiveWallbin == null) return;
			var targetContext = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage;
			var targetLibrary = targetContext.Library;

			var isSyncLock = false;
			var uncompletedTags = 0;
			var unconvertedVideos = 0;
			var inactiveLinks = 0;
			MainController.Instance.ProcessManager.Run("Checking library...", cancellationToken =>
			{
				TaggedLinksManager.Instance.Load(targetLibrary);
				InactiveLinkManager.Instance.Load(new[] { targetLibrary });
				isSyncLock = targetLibrary.IsLockedForSync(out uncompletedTags, out unconvertedVideos, out inactiveLinks);
			});
			if (isSyncLock)
			{
				using (var form = new FormSyncLock(uncompletedTags, inactiveLinks, unconvertedVideos))
				{
					form.ShowDialog();
				}
				return;
			}

			InactiveLinkManager.Instance.NotifyAboutExpiredLinks(targetLibrary.InactiveLinksSettings);

			MainController.Instance.MainForm.ribbonControl.Enabled = false;

			var savedState = MainController.Instance.MainForm.WindowState;
			var mainAction = new Action<CancellationToken>(cancellationToken =>
			{
				MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					if (targetLibrary.SyncSettings.MinimizeOnSync)
						MainController.Instance.MainForm.WindowState = FormWindowState.Minimized;
				}));
				UpdateFolderContent(targetLibrary, cancellationToken);
				UpdateFileDates(targetLibrary, cancellationToken);
				UpdatePreviewContent(targetLibrary, cancellationToken);
				targetLibrary.SyncDate = DateTime.Now;
				targetContext.SaveChanges();
				if (cancellationToken.IsCancellationRequested) return;
				WebContentManager.GenerateWebContent(targetLibrary);
				if (cancellationToken.IsCancellationRequested) return;
				SyncLibrary(targetLibrary, cancellationToken);
			});

			if (targetLibrary.SyncSettings.ShowProgress)
			{
				var formProgressSync = new FormProgressSync();
				MainController.Instance.ProcessManager.RunWithProgress(formProgressSync, true,
					mainAction,
					cancellationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
					{
						if (!cancellationToken.IsCancellationRequested && targetLibrary.SyncSettings.CloseAfterSync)
						{
							Application.Exit();
							return;
						}
					})));
			}
			else
			{
				mainAction(new CancellationTokenSource().Token);
				if (targetLibrary.SyncSettings.CloseAfterSync)
				{
					Application.Exit();
					return;
				}
			}
			MainController.Instance.MainForm.WindowState = savedState;
			MainController.Instance.MainForm.ribbonControl.Enabled = true;
		}

		public static void SyncSilent()
		{
			foreach (var targetContext in MainController.Instance.Wallbin.Libraries)
			{
				var targetLibrary = targetContext.Library;

				int uncompletedTags;
				int unconvertedVideos;
				int inactiveLinks;
				TaggedLinksManager.Instance.Load(targetLibrary);
				InactiveLinkManager.Instance.Load(new[] { targetLibrary });
				var isSyncLock = targetLibrary.IsLockedForSync(out uncompletedTags, out unconvertedVideos, out inactiveLinks);
				if (isSyncLock)
					return;

				var cancellationToken = new CancellationToken();
				UpdateFolderContent(targetLibrary, cancellationToken);
				UpdateFileDates(targetLibrary, cancellationToken);
				UpdatePreviewContent(targetLibrary, cancellationToken);
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

			var webSyncLog = new SyncLog("iPad Sync Manual");
			LibraryFilesSyncHelper.SyncLibraryWebFiles(library, webSyncLog, cancellationToken);
			if (cancellationToken.IsCancellationRequested) return;
			syncLogs.Add(webSyncLog);

			var resultFiles = new List<string>();
			var tempPath = Path.GetTempPath();
			resultFiles.AddRange(syncLogs.Select(log => log.Save(tempPath)));
			resultFiles.Add(Path.Combine(library.Path, Constants.LocalStorageFileName));
			resultFiles.Add(Path.Combine(library.Path, Constants.LibrariesJsonFileName));
			resultFiles.Add(Path.Combine(library.Path, Constants.ShortLibraryInfoFileName));
			resultFiles.Add(RemoteResourceManager.Instance.AppSettingsFile.LocalPath);
			ArchiveSyncResulst(resultFiles);
		}

		private static void UpdateFileDates(Library library, CancellationToken cancellationToken)
		{
			var fileLinks = library.Pages.SelectMany(p => p.AllLinks).OfType<LibraryFileLink>().Where(f=>!f.IsFolder).ToList();
			if (!fileLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;
			fileLinks.ForEach(f => f.UpdateFileDate());
		}

		private static void UpdateFolderContent(Library library, CancellationToken cancellationToken)
		{
			var folderLinks = library.Pages.SelectMany(p => p.AllLinks).OfType<LibraryFolderLink>().ToList();
			if (!folderLinks.Any()) return;
			if (cancellationToken.IsCancellationRequested) return;
			folderLinks.ForEach(f => f.UpdateContent());
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

		private static void ArchiveSyncResulst(IEnumerable<string> resultFiles)
		{
			var archiveDateTime = DateTime.Now;
			var archiveName = String.Format("{0}-{1}", archiveDateTime.ToString("MMddyy"), archiveDateTime.ToString("hhmmsstt"));
			var archiveFolder = new ArchiveDirectory(Configuration.RemoteResourceManager.Instance.ArchiveFolder.RelativePathParts.Merge(archiveName));
			AsyncHelper.RunSync(() => archiveFolder.Allocate(false));
			archiveFolder.AddFiles(resultFiles);
			AsyncHelper.RunSync(archiveFolder.Upload);
		}
	}
}
