using System.Collections.Generic;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Objects.Graphics
{
	public interface IImageSourceList
	{
		StorageDirectory MainFolder { get; }
		StorageDirectory AdditionalFolder { get; }
		StorageDirectory FavsFolder { get; }
		List<ImageSourceGroup> Items { get; }
		SearchResultsImageGroup SearchResults { get; }
	}
}
