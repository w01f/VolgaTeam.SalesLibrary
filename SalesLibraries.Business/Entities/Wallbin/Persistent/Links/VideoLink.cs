using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class VideoLink : PreviewableLink
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
		public override string WebFormat
		{
			get
			{
				return WebFormats.Video;
			}
		}
		#endregion

		public VideoLink()
		{
			Type = FileTypes.Video;
		}
	}
}
