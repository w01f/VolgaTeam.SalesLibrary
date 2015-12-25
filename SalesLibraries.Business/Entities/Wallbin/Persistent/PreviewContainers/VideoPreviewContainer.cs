using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Objects.Video;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class VideoPreviewContainer : BasePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public bool IsConverted
		{
			get
			{
				var necessaryFormatsConverted = NecessaryPreviewFormats.All(previewFormat =>
				{
					var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
					return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
				});
				if (!necessaryFormatsConverted)
					return false;
				var videoData = GetVideoData();
				var mp4ContainerPath = Path.Combine(ContainerPath, PreviewFormats.VideoMp4);
				return videoData != null && (videoData.IsH264Encoded || (Directory.Exists(mp4ContainerPath) && Directory.GetFiles(mp4ContainerPath).Any()));
			}
		}

		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder
		{
			get { return VideoSubFolderName; }
		}

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats
		{
			get { return new[] { PreviewFormats.VideoMp4, PreviewFormats.VideoInfo, PreviewFormats.VideoThumbnail }; }
		}

		private IEnumerable<string> NecessaryPreviewFormats
		{
			get { return new[] { PreviewFormats.VideoInfo, PreviewFormats.VideoThumbnail }; }
		}
		#endregion

		protected override void UpdateState(IEnumerable<PreviewableLink> associatedLinks)
		{
			var associatedLinksList = associatedLinks.ToList();
			base.UpdateState(associatedLinksList);
			if (!IsUpToDate)
				return;
			IsUpToDate = IsConverted;
		}

		public FFMpegData GetVideoData()
		{
			var infoPath = GetInfoPath();
			return !String.IsNullOrEmpty(infoPath) ? FFMpegData.LoadFromFile(infoPath) : null;
		}

		private string GetInfoPath()
		{
			var infoDestination = Path.Combine(ContainerPath, PreviewFormats.VideoInfo);
			return Directory.Exists(infoDestination) ?
				Directory.GetFiles(infoDestination, "*.txt").FirstOrDefault() :
				null;
		}
	}
}
