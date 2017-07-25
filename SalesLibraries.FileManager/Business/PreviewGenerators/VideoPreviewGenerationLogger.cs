using System;
using System.IO;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.FileManager.Business.Models.VideoInfo;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	class VideoPreviewGenerationLogger : PreviewGenerationLogger
	{
		public VideoPreviewGenerationLogger(BasePreviewContainer previewContainer) : base(previewContainer) { }

		public void LogOriginalInfoStage()
		{
			_log.AppendLine(String.Format(@"{0} - {1:hh\:mm\:ss tt}", String.Format(Constants.OriginalVideoInfoFileNameTemplate, PreviewFormats.VideoInfo).ToUpper(), DateTime.Now));

			var videoPreviewContainer = (VideoPreviewContainer)_previewContainer;
			var videoData = videoPreviewContainer.GetOriginalVideoData();
			if (videoData != null)
			{
				_log.AppendLine(String.Format(@"Length - {0:hh\:mm\:ss}", TimeSpan.FromSeconds(videoData.Duration)));
				SaveToFile();
			}
			else
			{
				SaveToFile();
				throw PreviewGenerationException.Create(_logPath);
			}
		}

		public void LogOutputInfoStage()
		{
			var videoPreviewContainer = (VideoPreviewContainer)_previewContainer;

			_log.AppendLine(String.Format(@"CRF - {0}", ((VideoPreviewContainerSettings)videoPreviewContainer.Settings).VideoConvertSettings.Crf.HasValue ? ((VideoPreviewContainerSettings)videoPreviewContainer.Settings).VideoConvertSettings.Crf.Value.ToString() : VideoInfo.NoCrfValue));
			_log.AppendLine(String.Format(@"{0} - {1:hh\:mm\:ss tt}", String.Format(Constants.OutputVideoInfoFileNameTemplate, PreviewFormats.VideoInfo).ToUpper(), DateTime.Now));
			SaveToFile();
			if (!File.Exists(videoPreviewContainer.GetOutputInfoPath()))
				throw PreviewGenerationException.Create(_logPath);
		}
	}
}
