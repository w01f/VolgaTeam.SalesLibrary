using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
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

			if (!cancellationToken.IsCancellationRequested)
			{
				var infoDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoInfo);
				var updateInfo = !(Directory.Exists(infoDestination) && Directory.GetFiles(infoDestination).Any());
				if (!Directory.Exists(infoDestination))
					Directory.CreateDirectory(infoDestination);
				if (updateInfo)
				{
					VideoHelper.ExtractVideoInfo(previewContainer.SourcePath, infoDestination, cancellationToken);
					logger.LogInfoStage();
				}
				videoData = ((VideoPreviewContainer)previewContainer).GetVideoData();
				updated |= updateInfo;
			}

			if (!cancellationToken.IsCancellationRequested && videoData != null && !((VideoPreviewContainer)previewContainer).IsMp4Converted)
			{
				var mp4Destination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoMp4);
				var updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination).Any());
				if (!Directory.Exists(mp4Destination))
					Directory.CreateDirectory(mp4Destination);
				if (updateMp4)
				{
					VideoHelper.ExportMp4(previewContainer.SourcePath, mp4Destination, videoData, cancellationToken);
					logger.LogStage(PreviewFormats.VideoMp4);
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
					JpegGenerator.GenerateDatatableJpegs(thumbDestination, thumbDatatableDestination);
					PngHelper.ConvertFiles(thumbDestination);
					logger.LogStage(PreviewFormats.VideoThumbnail);
					logger.LogStage(PreviewFormats.ThumbnailsForDatatable);
				}
				updated |= updateThumbs || updateThumbsDatatable;
			}

			if (updated)
				previewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
