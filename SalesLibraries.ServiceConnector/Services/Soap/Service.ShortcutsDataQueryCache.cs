using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.ShortcutsDataQueryCacheService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private ShortcutsDataQueryCacheControllerService GetShortcutsDataQueryCacheClient()
		{
			try
			{
				var client = new ShortcutsDataQueryCacheControllerService();
				client.Timeout = 6000000;
				client.Url = String.Format("{0}/ShortcutsDataQueryCache/quote?ws=1", Website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		public SoapShortcutModel[] GetLandingPages(out string message)
		{
			message = string.Empty;
			var landingPages = new List<SoapShortcutModel>();
			var client = GetShortcutsDataQueryCacheClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						landingPages.AddRange(client.getLandingPages(sessionKey) ?? new SoapShortcutModel[] { });
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
			return landingPages.ToArray();
		}

		public void ResetDataQueryCache(string landingPageId, out string message)
		{
			message = string.Empty;
			var client = GetShortcutsDataQueryCacheClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.resetDataQueryCache(sessionKey, landingPageId);
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

		public ShortcutDataQueryCacheServiceProfile[] GetShortcutDataQueryCacheProfiles(out string message)
		{
			message = string.Empty;
			var landingPages = new List<ShortcutDataQueryCacheServiceProfile>();
			var client = GetShortcutsDataQueryCacheClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						landingPages.AddRange(client.getProfiles(sessionKey) ?? new ShortcutDataQueryCacheServiceProfile[] { });
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
			return landingPages.ToArray();
		}

		public void SaveShortcutDataQueryCacheProfile(ShortcutDataQueryCacheServiceProfile profile, out String message)
		{
			message = string.Empty;
			var client = GetShortcutsDataQueryCacheClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.saveProfile(sessionKey, profile);
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

		public void DeleteShortcutDataQueryCacheProfile(ShortcutDataQueryCacheServiceProfile profile, out String message)
		{
			message = string.Empty;
			var client = GetShortcutsDataQueryCacheClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteProfile(sessionKey, profile);
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
