using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class LinkBundleLink : LibraryObjectLink
	{
		#region Nonpersistent Properties

		private LinkBundleLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LinkBundleLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as LinkBundleLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string FullPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string LinkInfoDisplayName => Settings.TextWordWrap ? "Link Bundle" : Name;

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.LinkBundle;
		#endregion

		public LinkBundleLink()
		{
			Type = FileTypes.LinkBundle;
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
	}
}
