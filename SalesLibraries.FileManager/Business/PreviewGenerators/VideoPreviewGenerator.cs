﻿using System.IO;
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

			if (!cancellationToken.IsCancellationRequested)
			{
				var infoDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoInfo);
				var updateInfo = !(Directory.Exists(infoDestination) && Directory.GetFiles(infoDestination).Any());
				if (!Directory.Exists(infoDestination))
					Directory.CreateDirectory(infoDestination);
				if (updateInfo)
					VideoHelper.ExtractVideoInfo(previewContainer.SourcePath, infoDestination, cancellationToken);
				videoData = ((VideoPreviewContainer)previewContainer).GetVideoData();
				updated |= updateInfo;
			}

			if (!cancellationToken.IsCancellationRequested && videoData != null && !videoData.IsH264Encoded)
			{
				var mp4Destination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoMp4);
				var updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination).Any());
				if (!Directory.Exists(mp4Destination))
					Directory.CreateDirectory(mp4Destination);
				if (updateMp4)
					VideoHelper.ExportMp4(previewContainer.SourcePath, mp4Destination, videoData, cancellationToken);
				updated |= updateMp4;
			}

			if (!cancellationToken.IsCancellationRequested && videoData != null)
			{
				var sourceFile = !videoData.IsH264Encoded ?
					Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoMp4, Path.ChangeExtension(Path.GetFileName(previewContainer.SourcePath), ".mp4")) :
					previewContainer.SourcePath;
				var thumbDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoThumbnail);
				var updateThumbs = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination).Any());
				if (!Directory.Exists(thumbDestination))
					Directory.CreateDirectory(thumbDestination);
				if (updateThumbs)
				{
					VideoHelper.GenerateThumbnails(sourceFile, thumbDestination, videoData, cancellationToken);
					PngHelper.ConvertFiles(thumbDestination);
				}
				updated |= updateThumbs;
			}

			if (updated)
				previewContainer.MarkAsModified();
		}
	}
}
