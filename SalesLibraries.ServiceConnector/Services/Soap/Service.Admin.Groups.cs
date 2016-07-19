using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.AdminService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		public IEnumerable<GroupModel> GetGroups(out string message)
		{
			message = string.Empty;
			var groups = new List<GroupModel>();
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						groups.AddRange(client.getGroups(sessionKey) ?? new GroupModel[] { });
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
			return groups;
		}

		public void SetGroup(string id, string name, UserModel[] users, SoapLibraryPage[] pages, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setGroup(sessionKey, id, name, users, pages);
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

		public void DeleteGroup(string id, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteGroup(sessionKey, id);
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

		public IEnumerable<string> GetGroupTemplates(out string message)
		{
			message = string.Empty;
			var groupTemplates = new List<string>();
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						groupTemplates.AddRange(client.getGroupTemplates(sessionKey) ?? new string[] { });
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
			return groupTemplates;
		}
	}
}
