using System;
using SalesLibraries.ServiceConnector.AdminService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private AdminControllerService GetAdminClient()
		{
			if (!IsConnected) return null;
			try
			{
				var client = new AdminControllerService
				{
					Url = string.Format((String) "{0}/admin/quote?ws=1", (Object) Website)
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
