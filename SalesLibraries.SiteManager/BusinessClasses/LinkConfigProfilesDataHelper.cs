using System.Collections.Generic;
using SalesLibraries.ServiceConnector.LinkConfigProfileService;
using SalesLibraries.ServiceConnector.Services;

namespace SalesLibraries.SiteManager.BusinessClasses
{
	class LinkConfigProfilesDataHelper
	{
		public static List<LibraryReference> Libraries { get; private set; }
		public static List<SecurityGroupReference> SecurityGroups { get; private set; }

		static LinkConfigProfilesDataHelper()
		{
			Libraries = new List<LibraryReference>();
			SecurityGroups = new List<SecurityGroupReference>();
		}

		public static void LoadReferences(ServiceConnection site)
		{
			Libraries.Clear();
			Libraries.AddRange(site.GetLibraryReferences());

			SecurityGroups.Clear();
			SecurityGroups.AddRange(site.GetSecurityGroups());
		}
	}
}
