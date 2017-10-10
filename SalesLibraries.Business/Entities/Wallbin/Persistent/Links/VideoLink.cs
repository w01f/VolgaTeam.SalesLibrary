using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class VideoLink : PreviewableFileLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties
		private VideoLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<VideoLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as VideoLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.Video;

		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => Folder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => true;
		#endregion

		public VideoLink()
		{
			Type = LinkType.Video;
		}

		public IList<string> GetThumbnailSourceFiles()
		{
			var previewFiles = new List<string>();
			var sourceFilesPath = Path.Combine(PreviewContainerPath, PreviewFormats.VideoThumbnail);
			if (Directory.Exists(sourceFilesPath))
				previewFiles.AddRange(Directory.GetFiles(sourceFilesPath));
			previewFiles.Sort(WinAPIHelper.StrCmpLogicalW);
			return previewFiles;
		}
	}
}
