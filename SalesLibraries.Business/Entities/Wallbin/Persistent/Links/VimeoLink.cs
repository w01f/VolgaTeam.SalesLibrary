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
	public class VimeoLink : PreviewableHyperLink
	{
		#region Nonpersistent Properties
		private VimeoSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<VimeoSettings>(this, SettingsEncoded)); }
			set { _settings = value as VimeoSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.Vimeo;

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => "vimeo";
		#endregion

		public VimeoLink()
		{
			Type = LinkType.Vimeo;
		}

		public static VimeoLink Create(VimeoLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((VimeoSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
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

		public static VimeoLink Create(string path)
		{
			return CreateEntity<VimeoLink>(vimeoLink =>
			{
				vimeoLink.RelativePath = path;
			});
		}
	}
}
