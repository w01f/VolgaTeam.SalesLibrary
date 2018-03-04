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
					Url = string.Format((String)"{0}/admin/quote?ws=1", (Object)Website)
				};
				return client;
			}
			catch
			{
				return null;
			}
		}

		public bool IsAuthenticated(string login, string password)
		{
			var client = GetAdminClient();
			try
			{
				var sessionKey = client.getSessionKey(login, password);
				return !String.IsNullOrEmpty(sessionKey);
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
