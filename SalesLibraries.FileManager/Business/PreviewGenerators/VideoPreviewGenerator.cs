using System;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Video;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(VideoPreviewContainer))]
	class VideoPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var updated = false;
			FFMpegData videoData = null;

			var logger = new VideoPreviewGenerationLogger(previewContainer);
			logger.StartLogging();

			var infoDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoInfo);
			if (!Directory.Exists(infoDestination))
				Directory.CreateDirectory(infoDestination);

			var oneDriveUrl = ((VideoPreviewContainer)previewContainer).OneDriveUrl;
			var oneDriveUrlDestination = Path.Combine(((VideoPreviewContainer)previewContainer).ContainerPath, PreviewFormats.OneDrive);
			var updateOneDrive = !Directory.Exists(oneDriveUrlDestination) && !String.IsNullOrEmpty(oneDriveUrl);
			if (!Directory.Exists(oneDriveUrlDestination) && updateOneDrive)
				Directory.CreateDirectory(oneDriveUrlDestination);

			if (!cancellationToken.IsCancellationRequested)
			{
				var infoFilePath = Path.Combine(infoDestination, String.Format(Constants.OriginalVideoInfoFileNameTemplate, Path.GetFileNameWithoutExtension(previewContainer.SourcePath)) + ".txt");
				var updateOriginalInfo = !File.Exists(infoFilePath);
				if (updateOriginalInfo)
				{
					VideoHelper.ExtractVideoInfo(previewContainer.SourcePath, infoFilePath, cancellationToken);
					logger.LogOriginalInfoStage();
				}
				videoData = ((VideoPreviewContainer)previewContainer).GetOriginalVideoData();
				updated |= updateOriginalInfo;
			}

			if (!cancellationToken.IsCancellationRequested && videoData != null && !((VideoPreviewContainer)previewContainer).IsMp4Converted)
			{
				var mp4Destination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoMp4);
				var updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination).Any());
				if (!Directory.Exists(mp4Destination))
					Directory.CreateDirectory(mp4Destination);
				if (updateMp4)
				{
					VideoHelper.ExportMp4(previewContainer.SourcePath, mp4Destination, videoData, ((VideoPreviewContainerSettings)previewContainer.Settings).VideoConvertSettings.Crf, cancellationToken);
					logger.LogStage(PreviewFormats.VideoMp4);

					var infoFilePath = Path.Combine(infoDestination, String.Format(Constants.OutputVideoInfoFileNameTemplate, Path.GetFileNameWithoutExtension(previewContainer.SourcePath)) + ".txt");
					var updateOutputInfo = !File.Exists(infoFilePath);
					if (updateOutputInfo)
					{
						VideoHelper.ExtractVideoInfo(Path.Combine(mp4Destination, Path.ChangeExtension(Path.GetFileName(previewContainer.SourcePath), ".mp4")), infoFilePath, cancellationToken);
						logger.LogOutputInfoStage();
					}
					updated |= updateOutputInfo;
				}
				updated |= updateMp4;
			}

			if (!cancellationToken.IsCancellationRequested && videoData != null)
			{
				var sourceFile = !((VideoPreviewContainer)previewContainer).IsMp4Converted ?
					Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoMp4, Path.ChangeExtension(Path.GetFileName(previewContainer.SourcePath), ".mp4")) :
					previewContainer.SourcePath;

				var thumbDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoThumbnail);
				var updateThumbs = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination).Any());
				if (!Directory.Exists(thumbDestination))
					Directory.CreateDirectory(thumbDestination);

				var thumbDatatableDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
				var updateThumbsDatatable = !(Directory.Exists(thumbDatatableDestination) && Directory.GetFiles(thumbDatatableDestination).Any());
				if (!Directory.Exists(thumbDatatableDestination))
					Directory.CreateDirectory(thumbDatatableDestination);

				if (updateThumbs || updateThumbsDatatable)
				{
					VideoHelper.GenerateThumbnails(sourceFile, thumbDestination, videoData, cancellationToken);
					JpegHelper.ConvertFiles(thumbDestination, thumbDatatableDestination);
					PngHelper.ConvertFiles(thumbDestination);
					logger.LogStage(PreviewFormats.VideoThumbnail);
					logger.LogStage(PreviewFormats.ThumbnailsForDatatable);
				}
				updated |= updateThumbs || updateThumbsDatatable;
			}

			if (updateOneDrive)
			{
				OneDrivePreviewHelper.GenerateShortcutFiles(
					oneDriveUrl,
					Path.GetFileName(previewContainer.SourcePath),
					oneDriveUrlDestination);
				logger.LogStage(PreviewFormats.OneDrive);
			}

			if (updated)
				previewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
