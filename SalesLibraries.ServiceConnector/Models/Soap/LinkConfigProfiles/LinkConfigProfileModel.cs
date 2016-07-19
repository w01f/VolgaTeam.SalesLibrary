using System;

namespace SalesLibraries.ServiceConnector.LinkConfigProfileService
{
	public partial class LinkConfigProfileModel
	{
		public LinkConfigProfileModel()
		{
			InitDefaults();
		}

		private void InitDefaults()
		{
			id = Guid.NewGuid().ToString();

			config = new LinkConfig();
		
			config.disableDownload = false;
			config.disablePdf = false;
			config.disableFavorites = false;
			config.disableQuickSite = false;
			
			config.disablePreview = false;
			config.disableSave = false;
			config.disableEmail = false;

			config.libraryLinkTags = new string[] { };
			config.libraryReferences = new LibraryReference[] { };
			config.securityGroupReferences = new SecurityGroupReference[] { };
			config.ignoredLinkReferences = new LibraryLinkReference[] { };
		}
	}
}
