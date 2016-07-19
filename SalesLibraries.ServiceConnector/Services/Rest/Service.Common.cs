using RestSharp;

namespace SalesLibraries.ServiceConnector.Services.Rest
{
	public partial class RestServiceConnection
	{
		private RestClient GetClient()
		{
			return new RestClient(Website);
		}
	}
}
