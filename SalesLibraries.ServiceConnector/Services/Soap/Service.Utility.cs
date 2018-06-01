using System;
using SalesLibraries.ServiceConnector.UtilityService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private UtilityControllerService GetUtilityClient()
		{
			try
			{
				var client = new UtilityControllerService();
				client.Timeout = 6000000;
				client.Url = String.Format("{0}/utility/quote?ws=1", Website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		public void UpdateContent(out string message)
		{
			var client = GetUtilityClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					message = !string.IsNullOrEmpty(sessionKey) ? client.updateWallbin(sessionKey) : "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void UpdateShortcuts(out string message)
		{
			var client = GetUtilityClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					message = !string.IsNullOrEmpty(sessionKey) ? client.updateShortcuts(sessionKey) : "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void UpdateQuizzes(out string message)
		{
			var client = GetUtilityClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					message = !string.IsNullOrEmpty(sessionKey) ? client.updateQuizzes(sessionKey) : "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void ResetOpCache(out string message)
		{
			var client = GetUtilityClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					message = !string.IsNullOrEmpty(sessionKey) ? client.resetOpCache(sessionKey) : "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}
	}
}
