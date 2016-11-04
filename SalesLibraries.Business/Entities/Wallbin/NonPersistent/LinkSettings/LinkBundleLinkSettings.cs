using System;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LinkBundleLinkSettings : LibraryObjectLinkSettings
	{
		public Guid BundleId { get; set; }

		private string _customWebFormat;
		public string CustomWebFormat
		{
			get { return _customWebFormat; }
			set
			{
				if (_customWebFormat != value)
					OnSettingsChanged();
				_customWebFormat = value;
			}
		}

		[JsonIgnore]
		public LinkBundle Bundle => ParentLink.ParentLibrary.LinkBundles.FirstOrDefault(lb => lb.ExtId == BundleId);

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			CustomWebFormat = WebFormats.PowerPoint;
		}
	}
}
