using System;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.Business.PreviewGenerators;

namespace SalesLibraries.FileManager.Business.Models.VideoInfo
{
	class VideoInfo
	{
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

		private VideoInfo() { }

		public static VideoInfo Create(VideoPreviewContainer previewContainer)
		{
			var videoInfo = new VideoInfo
			{
				_previewContainer = previewContainer,
				SourceFolderPath = Path.GetDirectoryName(previewContainer.SourcePath),
				SourceFileInfo = String.Format("{0}     <b>({1})</b>",
					Path.GetFileName(previewContainer.SourcePath),
					FormatVideoSize(new FileInfo(previewContainer.SourcePath).Length)),
				PreviewContainerPath = previewContainer.ContainerPath,
				Converted = previewContainer.IsConverted
			};

			var videoData = previewContainer.GetVideoData();
			if (videoData != null)
			{
				videoInfo.Width = videoData.Width;
				videoInfo.Height = videoData.Height;

				var durationSpan = TimeSpan.FromSeconds(videoData.Duration);
				videoInfo.Length = String.Format("{0}{1}:{2}",
					durationSpan.Hours > 0 ? String.Format("{0}:", durationSpan.Hours.ToString("#0")) : String.Empty,
					durationSpan.Minutes.ToString("# ##0"),
					durationSpan.Seconds.ToString("00"));
			}

			if (previewContainer.IsMp4Converted)
			{
				videoInfo.Mp4FileInfo = videoInfo.SourceFileInfo;
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
					videoInfo.Mp4FileInfo = String.Format("{0}{1}",
						Path.GetFileName(mp4FilePath),
						String.Format("     <b>({0})</b>", FormatVideoSize(new FileInfo(videoInfo.Mp4FilePath).Length)));
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
			var previewGenerator = _previewContainer.GetPreviewGenerator();
			_previewContainer.UpdateContent(previewGenerator, cancellationToken);
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

		private static string FormatVideoSize(long size)
		{
			if (size >= 524288000)
				return String.Format("{0} gb", (size * 0.0009765625 * 0.0009765625 * 0.0009765625).ToString("# ##0"));
			if (size < 524288000 && size >= 512000)
				return String.Format("{0} mb", (size * 0.0009765625 * 0.0009765625).ToString("# ##0"));
			if (size < 512000)
				return String.Format("{0} kb", (size * 0.0009765625).ToString("# ##0"));
			return String.Format("{0} b", size.ToString("# ##0"));
		}
	}
}
