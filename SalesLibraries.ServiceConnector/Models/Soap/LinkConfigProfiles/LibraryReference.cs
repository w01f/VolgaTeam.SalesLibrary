namespace SalesLibraries.ServiceConnector.LinkConfigProfileService
{
	public partial class LibraryReference
	{
		public bool Selected { get; set; }

		public LibraryReference Clone()
		{
			return new LibraryReference
			{
				id = id,
				name = name,
			};
		}
	}
}
