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
	public class Html5Link : PreviewableHyperLink
	{
		#region Nonpersistent Properties
		private Html5LinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<Html5LinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as Html5LinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.Html5;

		#endregion

		public Html5Link()
		{
			Type = FileTypes.Html5;
		}

		public static Html5Link Create(Html5LinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((Html5LinkSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
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

		public static Html5Link Create(string path)
		{
			return CreateEntity<Html5Link>(html5Link =>
			{
				html5Link.RelativePath = path;
			});
		}
	}
}
