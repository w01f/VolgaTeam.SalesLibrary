using RestSharp;

namespace SalesLibraries.ServiceConnector.Models.Rest.Common
{
	public interface IRequestData
	{
		string ModelName { get; }
		Method Method { get; }
	}
}
