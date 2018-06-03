using System;
using System.IO;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	abstract class FilePreviewGenerator : IPreviewContentGenerator, IOneDriveContentGenerator
	{
		public abstract void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken);

		public void GenerateOneDriveContent(FilePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			if (!MainController.Instance.Settings.OneDriveSettings.Enabled)
				return;

			var oneDriveUrl = previewContainer.OneDriveSettings.Url;

			if (String.IsNullOrEmpty(oneDriveUrl)) return;

			var oneDriveUrlDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.OneDrive);

			var needToUpdate = true;

			if (Directory.Exists(oneDriveUrlDestination))
			{
				var containerActualDate = Directory.GetLastWriteTime(oneDriveUrlDestination);
				needToUpdate = previewContainer.OneDriveSettings.UrlGeneratingDate.HasValue &&
					previewContainer.OneDriveSettings.UrlGeneratingDate.Value.Subtract(containerActualDate).Minutes > 0;
			}
			else
				Directory.CreateDirectory(oneDriveUrlDestination);

			if (needToUpdate)
				OneDrivePreviewHelper.GenerateShortcutFiles(
					oneDriveUrl,
					Path.GetFileName(previewContainer.SourcePath),
					oneDriveUrlDestination);
		}
	}
}
