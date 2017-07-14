using System;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	class VideoPreviewGenerationLogger : PreviewGenerationLogger
	{
		public VideoPreviewGenerationLogger(BasePreviewContainer previewContainer) : base(previewContainer) { }

		public void LogInfoStage()
		{
			_log.AppendLine(String.Format(@"{0} - {1:hh\:mm\:ss tt}", PreviewFormats.VideoInfo.ToUpper(), DateTime.Now));

			var formatContentPath = Path.Combine(_previewContainer.ContainerPath, PreviewFormats.VideoInfo);
			var files = Directory.GetFiles(formatContentPath);
			if (files.Any())
			{
				var videoPreviewContainer = (VideoPreviewContainer)_previewContainer;
				var videoData = videoPreviewContainer.GetVideoData();
				if (videoData != null)
				{
					_log.AppendLine(String.Format(@"Length - {0:hh\:mm\:ss}", TimeSpan.FromSeconds(videoData.Duration)));
					SaveToFile();
				}
			}
			else
			{
				SaveToFile();
				throw PreviewGenerationException.Create(_logPath);
			}
		}
	}
}
