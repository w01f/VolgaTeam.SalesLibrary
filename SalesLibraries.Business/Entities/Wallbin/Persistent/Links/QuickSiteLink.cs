using System;
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
	public class QuickSiteLink : PreviewableHyperLink
	{
		#region Nonpersistent Properties
		private QuickSiteSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<QuickSiteSettings>(this, SettingsEncoded)); }
			set { _settings = value as QuickSiteSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.QuickSite;

		[NotMapped, JsonIgnore]
		public override string PreviewSourcePath => String.Format("{0}&useForThumbnail=true", FullPath);

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => "quicksite";
		#endregion

		public QuickSiteLink()
		{
			Type = LinkType.QPageLink;
		}

		public static QuickSiteLink Create(QuickSiteLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((QuickSiteSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
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

		public static QuickSiteLink Create(string path)
		{
			return CreateEntity<QuickSiteLink>(quickSiteLink =>
			{
				quickSiteLink.RelativePath = path;
			});
		}
	}
}
