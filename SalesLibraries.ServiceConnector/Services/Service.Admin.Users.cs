using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.ServiceConnector.AdminService;

namespace SalesLibraries.ServiceConnector.Services
{
	public partial class ServiceConnection
	{
		public bool IsUserPasswordComplex(out string message)
		{
			message = string.Empty;
			var result = true;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						result = client.isUserPasswordComplex(sessionKey);
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
			return result;
		}

		public IEnumerable<UserModel> GetUsers(out string message)
		{
			message = string.Empty;
			var users = new List<UserModel>();
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						users.AddRange(client.getUsers(sessionKey) ?? new UserModel[] { });
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
			return users;
		}

		public void SetUser(string login,
			string password,
			string firstName,
			string lastName,
			string email,
			string phone,
			int role,
			GroupModel[] groups,
			LibraryPage[] pages,
			out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setUser(sessionKey, login, password, firstName, lastName, email, phone, groups, pages, role);
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

		public void SetUsers(UserInfo[] users, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						var uniqueGroups = users.SelectMany(user => user.Groups).Where(group => group.IsNew).Distinct();
						foreach (var group in uniqueGroups)
							client.setGroup(sessionKey, group.id, group.name, new UserModel[] { }, new LibraryPage[] { });
						foreach (var user in users)
							client.setUser(sessionKey, user.Login, user.Password, user.FirstName, user.LastName, user.Email, user.Phone, user.Groups.ToArray(), user.Pages.ToArray(), 0);
					}
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

		public void DeleteUser(string login, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
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
