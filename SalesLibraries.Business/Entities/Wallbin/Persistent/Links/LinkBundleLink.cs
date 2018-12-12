using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class LinkBundleLink : LibraryObjectLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties

		private LinkBundleLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<LinkBundleLinkSettings>(this, SettingsEncoded));
			set => _settings = value as LinkBundleLinkSettings;
		}

		[NotMapped, JsonIgnore]
		public override string FullPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string LinkInfoDisplayName => Settings.TextWordWrap ? "Link Bundle" : Name;

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.LinkBundle;

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => "link bundle";

		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => ParentFolder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => true;
		#endregion

		public LinkBundleLink()
		{
			Type = LinkType.LinkBundle;
		}

		public override string ToString()
		{
			return "Link Bundle";
		}

		public static LinkBundleLink Create(LibraryFolder parentFolder, LinkBundle bundle)
		{
			return CreateEntity<LinkBundleLink>(linkBundleLink =>
			{
				linkBundleLink.Name = bundle.Name;
				linkBundleLink.Folder = parentFolder;
				((LinkBundleLinkSettings)linkBundleLink.Settings).BundleId = bundle.ExtId;
			});
		}

		public IList<string> GetThumbnailSourceFiles(string sessionKey)
		{
			var previewFiles = new List<string>();
			foreach (var previewableLink in ((LinkBundleLinkSettings)Settings).Bundle.Settings.Items
				.OfType<LibraryLinkItem>()
				.Select(item => item.TargetLink)
				.OfType<IThumbnailSettingsHolder>())
			{
				var tempList = previewableLink.GetThumbnailSourceFiles(sessionKey).ToList();
				tempList.Sort(WinAPIHelper.StrCmpLogicalW);
				previewFiles.AddRange(tempList);
			}
			return previewFiles;
		}
	}
}
