using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Common.Objects.Video;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class VideoPreviewContainer : FilePreviewContainer
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
				var mp4ContainerPath = Path.Combine(ContainerPath, PreviewFormats.VideoMp4);
				return IsMp4Converted || (Directory.Exists(mp4ContainerPath) && Directory.GetFiles(mp4ContainerPath).Any());
			}
		}

		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => VideoSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => new[]
		{
			PreviewFormats.VideoMp4,
			PreviewFormats.VideoInfo,
			PreviewFormats.VideoThumbnail,
			PreviewFormats.ThumbnailsForDatatable
		};

		[NotMapped, JsonIgnore]
		public bool IsMp4Converted
		{
			get
			{
				var videoData = GetVideoData();
				return FileFormatHelper.IsMp4File(SourcePath) && videoData != null && videoData.IsH264Encoded && videoData.IsBitrateNormal;
			}
		}

		private IEnumerable<string> NecessaryPreviewFormats => new[]
		{
			PreviewFormats.VideoInfo,
			PreviewFormats.VideoThumbnail,
		};
		#endregion

		protected override void UpdateState(IEnumerable<IPreviewableLink> associatedLinks)
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
