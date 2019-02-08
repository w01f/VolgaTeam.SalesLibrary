using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.InactiveUsersService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private InactiveUsersControllerService GetInactiveUsersClient()
		{
			if (!IsConnected) return null;
			try
			{
				var client = new InactiveUsersControllerService
				{
					Url = string.Format("{0}/InactiveUsers/quote?ws=1", Website)
				};
				return client;
			}
			catch
			{
				return null;
			}
		}

		public UserViewModel[] GetInactiveUsers(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var userRecords = new List<UserViewModel>();
			var client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						userRecords.AddRange(client.getInactiveUsers(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new UserViewModel[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return userRecords.ToArray();
		}

		public void ResetInactiveUser(string login, string password, out string message)
		{
			message = string.Empty;
			var client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.resetUser(sessionKey, login, password);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void DeleteInactiveUser(string login, out string message)
		{
			message = string.Empty;
			var client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteUser(sessionKey, login);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
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