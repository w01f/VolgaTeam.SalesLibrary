using System;
using SalesDepot.Services.UtilityService;

namespace SalesDepot.Services
{
	public partial class SiteClient
	{
		private UtilityControllerService GetUtilityClient()
		{
			try
			{
				var client = new UtilityControllerService();
				client.Url = String.Format("{0}/utility/quote?ws=1", _website);
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
					var sessionKey = client.getSessionKey(_login, _password);
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
					var sessionKey = client.getSessionKey(_login, _password);
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

		public void NotifyDeadLinks(out string message)
		{
			var client = GetUtilityClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					message = !string.IsNullOrEmpty(sessionKey) ? client.notifyDeadLinks(sessionKey) : "Couldn't complete operation.\nLogin or password are not correct.";
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
					var sessionKey = client.getSessionKey(_login, _password);
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
	}
}
