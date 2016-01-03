using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.LinkConfigProfileService;

namespace SalesLibraries.ServiceConnector.Services
{
	public partial class ServiceConnection
	{
		private LinkConfigProfileControllerService GetLinkConfigProfilesClient()
		{
			if (!IsConnected) return null;
			try
			{
				var client = new LinkConfigProfileControllerService
				{
					Url = string.Format("{0}/LinkConfigProfile/quote?ws=1", Website)
				};
				return client;
			}
			catch
			{
				return null;
			}
		}

		public LibraryReference[] GetLibraryReferences()
		{
			var libraryReferences = new List<LibraryReference>();
			var client = GetLinkConfigProfilesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						libraryReferences.AddRange(client.getLibraries(sessionKey) ?? new LibraryReference[] { });
				}
				catch (Exception ex)
				{
				}
			}
			return libraryReferences.ToArray();
		}

		public SecurityGroupReference[] GetSecurityGroups()
		{
			var libraryReferences = new List<SecurityGroupReference>();
			var client = GetLinkConfigProfilesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						libraryReferences.AddRange(client.getSecurityGroups(sessionKey) ?? new SecurityGroupReference[] { });
				}
				catch (Exception ex)
				{
				}
			}
			return libraryReferences.ToArray();
		}

		public LinkConfigProfileModel[] GetLinkConfigProfiles()
		{
			var linkConfigProfileModels = new List<LinkConfigProfileModel>();
			var client = GetLinkConfigProfilesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						linkConfigProfileModels.AddRange(client.getProfiles(sessionKey) ?? new LinkConfigProfileModel[] { });
				}
				catch (Exception ex)
				{
				}
			}
			return linkConfigProfileModels.ToArray();
		}

		public void SaveLinkConfigProfile(LinkConfigProfileModel profile)
		{
			var client = GetLinkConfigProfilesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.saveProfile(sessionKey,profile);
				}
				catch (Exception ex)
				{
				}
			}
		}

		public void DeleteLinkConfigProfile(LinkConfigProfileModel profile)
		{
			var client = GetLinkConfigProfilesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteProfile(sessionKey, profile);
				}
				catch (Exception ex)
				{
				}
			}
		}

		public LibraryLinkReference[] GetLinkConfigProfileAffectedLinks(LinkConfigProfileModel profile)
		{
			var affectedLinks = new List<LibraryLinkReference>();
			var client = GetLinkConfigProfilesClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
						affectedLinks.AddRange(client.getAffectedLinks(sessionKey, profile) ?? new LibraryLinkReference[] { });
				}
				catch (Exception ex)
				{
				}
			}
			return affectedLinks.ToArray();
		}
	}
}