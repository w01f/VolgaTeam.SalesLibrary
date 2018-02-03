using System;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Objects
{
	public class SiteSettings
	{
		public Guid Id { get; }
		public SiteType SiteType { get; set; }
		public string BaseUrl { get; set; }
		public string Title { get; set; }
		public bool EnableMenu { get; set; }
		public bool EnableScroll { get; set; }

		public SiteSettings()
		{
			Id = Guid.NewGuid();
		}

		public override String ToString()
		{
			return Title ?? BaseUrl;
		}
	}
}
