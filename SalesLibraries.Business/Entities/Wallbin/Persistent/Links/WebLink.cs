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
	public class WebLink : HyperLink
	{
		#region Nonpersistent Properties
		private WebLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<WebLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as WebLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get { return ((WebLinkSettings)Settings).IsUrl365 ? WebFormats.Url365 : WebFormats.Url; }
		}
		#endregion

		public WebLink()
		{
			Type = FileTypes.Url;
		}

		public static WebLink Create(UrlLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((WebLinkSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
			if (linkInfo.FormatAsBluelink)
			{
				((WebLinkSettings)link.Settings).RegularFontStyle = FontStyle.Underline | FontStyle.Bold;
				link.Settings.ForeColor = Color.Blue;
			}
			return link;
		}

		public static WebLink Create(string path)
		{
			return new WebLink { RelativePath = path };
		}
	}
}
