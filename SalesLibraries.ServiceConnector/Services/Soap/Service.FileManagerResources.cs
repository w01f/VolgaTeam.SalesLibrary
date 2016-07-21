using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.FileManagerResourcesService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private FileManagerDataControllerService GetFileManagerResourcesClient()
		{
			if (!IsConnected) return null;
			try
			{
				var client = new FileManagerDataControllerService
				{
					Url = string.Format("{0}/FileManagerData/quote?ws=1", Website)
				};
				return client;
			}
			catch
			{
				return null;
			}
		}

		public string GetMetaData(string dataTag, string propertyName, out string message)
		{
			message = string.Empty;
			var client = GetFileManagerResourcesClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						return client.getMetaData(sessionKey, dataTag, propertyName);
					message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return null;
		}

		public IEnumerable<GroupModel> GetSecurityGroups(out string message)
		{
			message = string.Empty;
			var groups = new List<GroupModel>();
			var client = GetFileManagerResourcesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						groups.AddRange(client.getSecurityGroups(sessionKey) ?? new GroupModel[] { });
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
			return groups.ToArray();
		}

		public IEnumerable<SoapCategory> GetCategories(out string message)
		{
			message = string.Empty;
			var categories = new List<SoapCategory>();
			var client = GetFileManagerResourcesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						categories.AddRange(client.getCategories(sessionKey) ?? new SoapCategory[] { });
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
			return categories.ToArray();
		}

		public IEnumerable<SoapSuperFilter> GetSuperFilters(out string message)
		{
			message = string.Empty;
			var superFilters = new List<SoapSuperFilter>();
			var client = GetFileManagerResourcesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						superFilters.AddRange(client.getSuperFilters(sessionKey) ?? new SoapSuperFilter[] { });
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
			return superFilters.ToArray();
		}
	}
}
