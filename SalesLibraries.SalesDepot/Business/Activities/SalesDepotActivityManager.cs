using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Activity;

namespace SalesLibraries.SalesDepot.Business.Activities
{
	class SalesDepotActivityManager : ActivityManager
	{
		public void AddLinkAccessActivity(string activityType, LibraryObjectLink link)
		{
			AddActivity(new LinkAccessActivity(activityType, link.Name, link.Type.ToString(), link.FullPath, link.ParentLibrary.Name, link.ParentPage.Name));
		}
	}
}
