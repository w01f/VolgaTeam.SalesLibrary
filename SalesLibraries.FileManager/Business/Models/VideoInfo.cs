using System;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.Business.PreviewGenerators;

namespace SalesLibraries.FileManager.Business.Models
{
	class VideoInfo
	{
		private VideoPreviewContainer _previewContainer;
		public string SourceFileName { get; set; }
		public string SourceFolderPath { get; set; }
		public string PreviewContainerPath { get; set; }
		public string Mp4FilePath { get; set; }
		public string Mp4FileName { get; set; }
		public int Index { get; set; }
		public bool Converted { get; set; }
		public bool Selected { get; set; }

		private VideoInfo() { }

		public static VideoInfo Create(VideoPreviewContainer previewContainer)
		{
			var videoInfo = new VideoInfo
			{
				_previewContainer = previewContainer,
				SourceFileName = Path.GetFileName(previewContainer.SourcePath),
				SourceFolderPath = Path.GetDirectoryName(previewContainer.SourcePath),
				PreviewContainerPath = previewContainer.ContainerPath,
				Converted = previewContainer.IsConverted
			};

			if (previewContainer.IsMp4Converted)
			{
				videoInfo.Mp4FileName = videoInfo.SourceFileName;
				videoInfo.Mp4FilePath = previewContainer.SourcePath;
			}
			else
			{
				var mp4Folder = Path.Combine(previewContainer.ContainerPath, PreviewFormats.VideoMp4);
				if (Directory.Exists(mp4Folder))
				{
					videoInfo.Mp4FileName = String.Format("{0}.{1}",
						Path.GetFileNameWithoutExtension(previewContainer.SourcePath),
						PreviewFormats.VideoMp4);
					videoInfo.Mp4FilePath = Path.Combine(mp4Folder, videoInfo.Mp4FileName);
				}
			}

			return videoInfo;
		}

		public void UpdateContent(CancellationToken cancellationToken)
		{
			var previewGenerator = _previewContainer.GetPreviewGenerator();
			_previewContainer.UpdateContent(previewGenerator, cancellationToken);
		}

		public void ClearContent()
		{
			_previewContainer.ClearContent();
		}

		public void DeleteWithLinks()
		{
			var topLevelLinks = _previewContainer.Library.GetPreviewableLinksBySourcePath(_previewContainer.SourcePath, true).ToList();
			var allLinks = _previewContainer.Library.GetPreviewableLinksBySourcePath(_previewContainer.SourcePath).ToList();
			if (topLevelLinks.Any())
			{
				DataStateObserver.Instance.RaiseLinksDeleted(topLevelLinks.Select(l => l.ExtId));
				foreach (var previewableLink in topLevelLinks)
					previewableLink.DeleteLink(true);
			}
			if (topLevelLinks.Count == allLinks.Count)
				_previewContainer.DeleteContainer();
			else
				_previewContainer.ClearContent();
		}
	}
}
