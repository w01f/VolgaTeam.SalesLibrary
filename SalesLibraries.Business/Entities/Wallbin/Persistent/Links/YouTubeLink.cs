using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class YouTubeLink : PreviewableHyperLink
	{
		#region Nonpersistent Properties
		private YouTubeSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<YouTubeSettings>(this, SettingsEncoded));
			set => _settings = value as YouTubeSettings;
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.YouTube;

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => "youtube";
		#endregion

		public YouTubeLink()
		{
			Type = LinkType.YouTube;
		}

		public static YouTubeLink Create(YouTubeLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((YouTubeSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
			if (linkInfo.FormatAsBluelink)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Underline;
				link.Settings.ForeColor = Color.Blue;
			}
			if (linkInfo.FormatBold)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Bold;
			}
			link.AfterCreate();
			return link;
		}

		public static YouTubeLink Create(string path)
		{
			return CreateEntity<YouTubeLink>(youTubeLink =>
			{
				youTubeLink.RelativePath = path;
			});
		}
	}
}
