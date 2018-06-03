using System;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.PreviewGenerators;

namespace SalesLibraries.FileManager.Business.Models.VideoInfo
{
	class VideoInfo
	{
		public const string NoCrfValue = "none";

		private VideoPreviewContainer _previewContainer;
		public string SourceFileInfo { get; set; }
		public string SourceFolderPath { get; set; }
		public string PreviewContainerPath { get; set; }
		public string Mp4FilePath { get; set; }
		public string Mp4FileInfo { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public string Length { get; set; }
		public int Index { get; set; }
		public bool Converted { get; set; }
		public bool Selected { get; set; }

		public VideoPreviewContainerSettings PreviewContainerSettings
			=> (VideoPreviewContainerSettings)_previewContainer.Settings;

		public string Crf
		{
			get
			{
				if (PreviewContainerSettings.VideoConvertSettings.Crf.HasValue)
					return PreviewContainerSettings.VideoConvertSettings.Crf.Value.ToString();
				return NoCrfValue;
			}
			set
			{
				if (!String.Equals(value, NoCrfValue, StringComparison.OrdinalIgnoreCase))
					PreviewContainerSettings.VideoConvertSettings.Crf = Int32.Parse(value);
				else
					PreviewContainerSettings.VideoConvertSettings.Crf = null;
			}
		}

		private VideoInfo() { }

		public static VideoInfo Create(VideoPreviewContainer previewContainer)
		{
			var videoInfo = new VideoInfo
			{
				_previewContainer = previewContainer,
				SourceFolderPath = Path.GetDirectoryName(previewContainer.SourcePath),
				SourceFileInfo = String.Format("{0} <color=lightgray>{1}</color>",
					Path.GetFileName(previewContainer.SourcePath),
					Utils.FormatFileSize(new FileInfo(previewContainer.SourcePath).Length)),
				PreviewContainerPath = previewContainer.ContainerPath,
				Converted = previewContainer.IsConverted
			};

			var videoData = previewContainer.GetOriginalVideoData();
			if (videoData != null)
			{
				videoInfo.Width = videoData.Width;
				videoInfo.Height = videoData.Height;

				var durationSpan = TimeSpan.FromSeconds(videoData.Duration);
				videoInfo.Length = String.Format("{0}{1:# ##0}:{2:00}",
					durationSpan.Hours > 0 ? String.Format("{0:#0}:", durationSpan.Hours) : String.Empty, durationSpan.Minutes, durationSpan.Seconds);
			}

			if (previewContainer.IsMp4Converted)
			{
				videoInfo.Mp4FileInfo = Utils.FormatFileSize(new FileInfo(previewContainer.SourcePath).Length);
				videoInfo.Mp4FilePath = previewContainer.SourcePath;
			}
			else
			{
				var mp4FilePath = Path.Combine(
					previewContainer.ContainerPath,
					PreviewFormats.VideoMp4,
					Path.GetFileName(Path.ChangeExtension(previewContainer.SourcePath, PreviewFormats.VideoMp4)));
				if (File.Exists(mp4FilePath))
				{
					videoInfo.Mp4FilePath = mp4FilePath;
					videoInfo.Mp4FileInfo = Utils.FormatFileSize(new FileInfo(videoInfo.Mp4FilePath).Length);
				}
				else
				{
					videoInfo.Mp4FilePath = null;
					videoInfo.Mp4FileInfo = videoData != null && videoData.IsCorrupted ? "CORRUPTED!" : "MISSING!";
				}
			}

			return videoInfo;
		}

		public void UpdateContent(CancellationToken cancellationToken)
		{
			var previewGenerator = _previewContainer.GetPreviewContentGenerator();
			try
			{
				_previewContainer.UpdatePreviewContent(previewGenerator, cancellationToken);
			}
			catch { }
		}

		public void ClearContent()
		{
			_previewContainer.ClearContent();
		}

		public void DeleteWithLinks()
		{
			var topLevelLinks = _previewContainer.Library.GetPreviewableLinksBySourcePath(_previewContainer.SourcePath, true).OfType<BaseLibraryLink>().ToList();
			var allLinks = _previewContainer.Library.GetPreviewableLinksBySourcePath(_previewContainer.SourcePath).ToList();
			if (topLevelLinks.Any())
			{
				DataStateObserver.Instance.RaiseLinksDeleted(topLevelLinks.Select(l => l.ExtId));
				foreach (var previewableLink in topLevelLinks)
					previewableLink.DeleteLink();
			}
			if (topLevelLinks.Count == allLinks.Count)
				_previewContainer.DeleteContainer();
			else
				_previewContainer.ClearContent();
		}
	}
}
