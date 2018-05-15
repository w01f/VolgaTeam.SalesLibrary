using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class CoverItem : BaseBundleItem, ILinkBundleInfoItem
	{
		public const string ItemName = "Cover Art";

		protected Image _logo;
		public Image Logo
		{
			get => _logo;
			set
			{
				if (_logo != value)
				{
					OnSettingsChanged();
				}
				_logo = value;
			}
		}

		[JsonIgnore]
		public override string Name
		{
			get => ItemName;
			set => throw new NotImplementedException();
		}

		public CoverItem()
		{
			ItemType = LinkBundleItemType.Cover;
		}
	}
}
