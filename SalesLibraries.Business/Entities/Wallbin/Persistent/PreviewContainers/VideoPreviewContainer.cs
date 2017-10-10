using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Objects.Video;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class VideoPreviewContainer : FilePreviewContainer
	{
		#region Nonpersistent Properties
		private VideoPreviewContainerSettings _settings;
		[NotMapped, JsonIgnore]
		public override BasePreviewContainerSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<VideoPreviewContainerSettings>(this, SettingsEncoded)); }
			set { _settings = value as VideoPreviewContainerSettings; }
		}

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
				var videoData = GetOriginalVideoData();
				return FileFormatHelper.IsMp4File(SourcePath) &&
					videoData != null &&
					videoData.IsH264Encoded &&
					videoData.IsBitrateNormal &&
					!((VideoPreviewContainerSettings)Settings).VideoConvertSettings.Crf.HasValue;
			}
		}

		[NotMapped, JsonIgnore]
		private IEnumerable<string> NecessaryPreviewFormats => new[]
		{
			PreviewFormats.VideoInfo,
			PreviewFormats.VideoThumbnail,
		};
		#endregion

		protected override void UpdateState(IList<IPreviewableLink> associatedLinks)
		{
			base.UpdateState(associatedLinks);
			if (!IsUpToDate)
				return;
			IsUpToDate = IsConverted;
		}

		public override void InitDefaultSettings()
		{
			base.InitDefaultSettings();

			if (Library.Settings.ApplyConvertSettingsForAllVideo)
			{
				var videoPreviewContainerSettings = (VideoPreviewContainerSettings)Settings;
				videoPreviewContainerSettings.VideoConvertSettings.Crf = Library.Settings.VideoConvertSettings.Crf;
			}
		}

		public FFMpegData GetOriginalVideoData()
		{
			var infoPath = GetOriginalInfoPath();
			return !String.IsNullOrEmpty(infoPath) ? FFMpegData.LoadFromFile(infoPath) : null;
		}

		private string GetOriginalInfoPath()
		{
			var infoFileDestination = Path.Combine(ContainerPath, PreviewFormats.VideoInfo, String.Format(Constants.OriginalVideoInfoFileNameTemplate, Path.GetFileNameWithoutExtension(SourcePath))) + ".txt";
			var oldFileDestination = Path.Combine(ContainerPath, PreviewFormats.VideoInfo, Path.GetFileNameWithoutExtension(SourcePath)) + ".txt";
			return File.Exists(infoFileDestination) ?
				infoFileDestination :
				(File.Exists(oldFileDestination) ? oldFileDestination : null);
		}

		public FFMpegData GetOutputVideoData()
		{
			var infoPath = GetOutputInfoPath();
			return !String.IsNullOrEmpty(infoPath) ? FFMpegData.LoadFromFile(infoPath) : null;
		}

		public string GetOutputInfoPath()
		{
			var infoFileDestination = Path.Combine(ContainerPath, PreviewFormats.VideoInfo, String.Format(Constants.OutputVideoInfoFileNameTemplate, Path.GetFileNameWithoutExtension(SourcePath))) + ".txt";
			return File.Exists(infoFileDestination) ?
				infoFileDestination :
				null;
		}
	}
}
