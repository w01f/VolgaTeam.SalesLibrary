using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesLibraries.SiteManager.BusinessClasses
{
	class UsersEditPermissionsManager
	{
		public string CurrentUserName { get; set; }
		public UsersEditPermissions CurrentUserPermissions { get; private set; }
		public List<UsersEditPermissions> UserPermissionsList { get; }
		public static UsersEditPermissionsManager Instance { get; } = new UsersEditPermissionsManager();

		public UsersEditPermissionsManager()
		{
			CurrentUserPermissions = new UsersEditPermissions(Environment.UserName);
			UserPermissionsList = new List<UsersEditPermissions>();
		}

		public void Load(string configurationFilePath)
		{
			if (!File.Exists(configurationFilePath)) return;
			var document = new XmlDocument();
			document.Load(configurationFilePath);

			var accountNodes = (document.SelectNodes("//UserAccountControls/Account")?.OfType<XmlNode>() ?? new XmlNode[] { }).ToList();
			foreach (var accountNode in accountNodes)
			{
				var userName = accountNode.SelectSingleNode("./UserAccount")?.InnerText;
				var userPermissions = new UsersEditPermissions(userName);
				userPermissions.LoadConfiguration(accountNode);
				UserPermissionsList.Add(userPermissions);
			}
		}

		public void LoadCurrentUserConfiguration(string userName)
		{
			CurrentUserPermissions =
				UserPermissionsList.First(item => String.Equals(item.UserName, userName, StringComparison.OrdinalIgnoreCase));
		}
	}
}
