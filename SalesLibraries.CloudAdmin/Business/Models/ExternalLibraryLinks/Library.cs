namespace SalesLibraries.CloudAdmin.Business.Models.ExternalLibraryLinks
{
	class Library
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public LibraryPage[] Pages { get; set; }
	}
}
