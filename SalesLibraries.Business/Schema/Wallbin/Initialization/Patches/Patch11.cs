using System.Linq;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Schema.Wallbin.Initialization.Patches
{
	class Patch11 : IPatch
	{
		public int Version => 11;

		public void Apply<TLibraryContext>(TLibraryContext context) where TLibraryContext : LibraryContext
		{
			context.Library.Path = context.DataSourceFolderPath;

			var fileLinks = context.Library.Pages
				.SelectMany(p => p.AllGroupLinks)
				.OfType<LibraryFileLink>()
				.Where(f => !f.IsDead && !f.IsFolder)
				.ToList();

			foreach (var fileLink in fileLinks)
			{
				var oneDriveLinkSettings = SettingsContainer.CreateInstance<OneDriveLinkSettings>(fileLink, fileLink.OneDriveEncoded);

				if (!oneDriveLinkSettings.Enable) continue;

				var previewContainer = context.Library.GetPreviewContainerBySourcePath(fileLink.FullPath);

				if (previewContainer == null) continue;

				var filePreviewSettings = (FilePreviewContainerSettings)previewContainer.Settings;
				filePreviewSettings.OneDriveSettings.ItemId = oneDriveLinkSettings.ItemId;
				filePreviewSettings.OneDriveSettings.AppId = oneDriveLinkSettings.AppId;
				filePreviewSettings.OneDriveSettings.AppRoot = oneDriveLinkSettings.AppRoot;
				filePreviewSettings.OneDriveSettings.Url = oneDriveLinkSettings.Url;
				filePreviewSettings.OneDriveSettings.UrlGeneratingDate = oneDriveLinkSettings.UrlGeneratingDate;
				filePreviewSettings.OneDriveSettings.UrlId = oneDriveLinkSettings.UrlId;
				previewContainer.AllowToHandleChanges = true;
				previewContainer.MarkAsModified();
			}

			context.Library.Save(context, context.Library);
		}
	}
}
