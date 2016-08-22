using System;
using System.Web;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class QuickSiteSettings : HyperLinkSettings
	{
		[JsonIgnore]
		public string QuickSiteId
		{
			get
			{
				var uri = new Uri(((QuickSiteLink)ParentLink).WebPath);
				return HttpUtility.ParseQueryString(uri.Query).Get("id");
			}
		}
	}
}
