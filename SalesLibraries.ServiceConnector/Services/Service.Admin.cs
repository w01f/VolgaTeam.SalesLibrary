using SalesLibraries.ServiceConnector.AdminService;

namespace SalesLibraries.ServiceConnector.Services
{
	public partial class ServiceConnection
	{
		private AdminControllerService GetAdminClient()
		{
			if (!IsConnected) return null;
			try
			{
				var client = new AdminControllerService
				{
					Url = string.Format("{0}/admin/quote?ws=1", Website)
				};
				return client;
			}
			catch
			{
				return null;
			}
		}
	}
}
