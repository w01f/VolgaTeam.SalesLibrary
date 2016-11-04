using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class UrlItem : BaseBundleItem
	{
		private string _url;
		public string Url
		{
			get { return _url; }
			set
			{
				if (_url != value)
					OnSettingsChanged();
				_url = value;
			}
		}

		[JsonIgnore]
		public override string Name
		{
			get { return Url; }
			set { Url = value; }
		}

		public UrlItem()
		{
			ItemType = LinkBundleItemType.Url;
		}

		protected override void AfterConstraction(params object[] parameters)
		{
			base.AfterConstraction(parameters);
			Url = parameters[0] as String;
		}
	}
}
