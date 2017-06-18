using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class LibraryLinkItem : BaseBundleItem
	{
		public Guid LinkId { get; set; }

		[JsonIgnore]
		public LibraryObjectLink TargetLink => ParentBundle?.Library?.GetLinkById<LibraryObjectLink>(LinkId, true);

		[JsonIgnore]
		public override string Name
		{
			get { return TargetLink is LibraryFileLink && TargetLink.Settings.TextWordWrap ? ((LibraryFileLink)TargetLink).NameWithExtension : TargetLink?.Name ?? "Link is Missing"; }
			set { throw new NotImplementedException(); }
		}

		[JsonIgnore]
		public bool IsDead => TargetLink == null;

		[JsonIgnore]
		public bool ThumbnailAvailable => (TargetLink is IPreviewableLink && !(TargetLink is ExcelLink)) || TargetLink is ImageLink || TargetLink is InternalLibraryObjectLink;

		public LibraryLinkItem()
		{
			ItemType = LinkBundleItemType.LibraryLink;
		}

		protected override void AfterConstraction(params object[] parameters)
		{
			LinkId = (Guid)parameters[0];
			base.AfterConstraction(parameters);
		}
	}
}
