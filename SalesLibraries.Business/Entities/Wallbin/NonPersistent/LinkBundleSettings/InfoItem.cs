using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class InfoItem : BaseBundleItem, ILinkBundleInfoItem
	{
		public const string ItemName = "Info";

		private string _header;
		public string Header
		{
			get { return _header; }
			set
			{
				if (_header != value)
					OnSettingsChanged();
				_header = value;
			}
		}

		private string _body;
		public string Body
		{
			get { return _body; }
			set
			{
				if (_body != value)
					OnSettingsChanged();
				_body = value;
			}
		}

		[JsonIgnore]
		public override string Name
		{
			get { return ItemName; }
			set { throw new NotImplementedException(); }
		}

		public InfoItem()
		{
			ItemType = LinkBundleItemType.Info;
		}
	}
}
