using System;
using SalesLibraries.ServiceConnector.FileManagerResourcesService;

namespace SalesLibraries.ServiceConnector.Services
{
	public partial class ServiceConnection
	{
		public static bool IsAuthorized(string url, string login, string password)
		{
			try
			{
				var client = new FileManagerDataControllerService
				{
					Url = String.Format("{0}/FileManagerData/quote?ws=1", url)
				};
				var sessionKey = client.getSessionKey(login, password);
				return !String.IsNullOrEmpty(sessionKey);
			}
			catch
			{
				return false;
			}
		}
	}
}
