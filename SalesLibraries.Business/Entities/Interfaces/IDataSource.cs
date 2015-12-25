using System;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IDataSource
	{
		Guid DataSourceId { get; }
		string Path { get; }
		string Name { get; }
		int Order { get; }
		string GetFilePath();
	}
}
