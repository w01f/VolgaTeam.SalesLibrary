using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.InactiveUsersService;

namespace SalesLibraries.ServiceConnector.Services
{
	public partial class ServiceConnection
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

		public UserModel[] GetInactiveUsers(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var userRecords = new List<UserModel>();
			var client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						userRecords.AddRange(client.getInactiveUsers(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new UserModel[] { });
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

		public void ResetUsers(string[] userIds, bool onlyEmail, string sender, string subject, string body, out string message)
		{
			message = string.Empty;
			var client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.resetUsers(sessionKey, userIds, onlyEmail, sender, subject, body);
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

		public void DeleteUsers(string[] userIds, bool onlyEmail, string sender, string subject, string body, out string message)
		{
			message = string.Empty;
			var client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteUsers(sessionKey, userIds, onlyEmail, sender, subject, body);
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