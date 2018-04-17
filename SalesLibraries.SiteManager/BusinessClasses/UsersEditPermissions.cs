using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SalesLibraries.SiteManager.BusinessClasses
{
	class UsersEditPermissions
	{
		public string UserName { get; }

		public Tab1Permissions Tab1 { get; }
		public Tab2Permissions Tab2 { get; }
		public Tab3Permissions Tab3 { get; }

		public UsersEditPermissions(string userName)
		{
			UserName = userName;

			Tab1 = new Tab1Permissions();
			Tab2 = new Tab2Permissions();
			Tab3 = new Tab3Permissions();
		}

		public void LoadConfiguration(XmlNode configNode)
		{
			if (configNode == null) return;

			Tab1.LoadConfiguration(configNode.SelectSingleNode("./UsersSubtab"));
			Tab2.LoadConfiguration(configNode.SelectSingleNode("./GroupsSubtab"));
			Tab3.LoadConfiguration(configNode.SelectSingleNode("./LibrariesSubtab"));
		}

		internal class Tab1Permissions
		{
			public bool Visible { get; private set; }
			public bool Enabled { get; private set; }

			public List<string> AllowedGroups { get; }

			public Tab1ActionPermissions AddActionPermissions { get; }
			public Tab1ActionPermissions EditActionPermissions { get; }
			public Tab1ActionPermissions DeleteActionPermissions { get; }

			public Tab1Permissions()
			{
				Visible = true;
				Enabled = true;

				AllowedGroups = new List<String>();

				AddActionPermissions = new Tab1ActionPermissions();
				EditActionPermissions = new Tab1ActionPermissions();
				DeleteActionPermissions = new Tab1ActionPermissions();
			}

			public void LoadConfiguration(XmlNode configNode)
			{
				if (configNode == null) return;

				if (Boolean.TryParse(configNode.SelectSingleNode("./Visible")?.InnerText ?? "true", out var temp))
					Visible = temp;
				if (Boolean.TryParse(configNode.SelectSingleNode("./Enabled")?.InnerText ?? "true", out temp))
					Enabled = temp;

				foreach (var xmlNode in (configNode.SelectNodes("./GroupsVisible/Group")?.OfType<XmlNode>() ?? new XmlNode[] { }).ToList())
					AllowedGroups.Add(xmlNode.InnerText);

				AddActionPermissions.LoadConfiguration(configNode.SelectSingleNode("./AllowedActions/AddUser"));
				EditActionPermissions.LoadConfiguration(configNode.SelectSingleNode("./AllowedActions/EditUser"));
				DeleteActionPermissions.LoadConfiguration(configNode.SelectSingleNode("./AllowedActions/DeleteUser"));
			}

			internal class Tab1ActionPermissions
			{
				public bool Enabled { get; set; }
				public List<string> AllowedGroups { get; }

				public Tab1ActionPermissions()
				{
					Enabled = true;
					AllowedGroups = new List<String>();
				}

				public void LoadConfiguration(XmlNode configNode)
				{
					if (configNode == null) return;

					if (Boolean.TryParse(configNode.SelectSingleNode("./Enabled")?.InnerText ?? "true", out var temp))
						Enabled = temp;

					foreach (var xmlNode in (configNode.SelectNodes("./GroupsAllowed/Group")?.OfType<XmlNode>() ?? new XmlNode[] { }).ToList())
						AllowedGroups.Add(xmlNode.InnerText);
				}
			}
		}

		internal class Tab2Permissions
		{
			public bool Visible { get; private set; }
			public bool Enabled { get; private set; }

			public List<string> AllowedGroups { get; }

			public Tab2Permissions()
			{
				Visible = true;
				Enabled = true;

				AllowedGroups = new List<String>();
			}

			public void LoadConfiguration(XmlNode configNode)
			{
				if (configNode == null) return;

				if (Boolean.TryParse(configNode.SelectSingleNode("./Visible")?.InnerText ?? "true", out var temp))
					Visible = temp;
				if (Boolean.TryParse(configNode.SelectSingleNode("./Enabled")?.InnerText ?? "true", out temp))
					Enabled = temp;

				foreach (var xmlNode in (configNode.SelectNodes("./GroupsVisible/Group")?.OfType<XmlNode>() ?? new XmlNode[] { }).ToList())
					AllowedGroups.Add(xmlNode.InnerText);
			}
		}

		internal class Tab3Permissions
		{
			public bool Visible { get; private set; }
			public bool Enabled { get; private set; }

			public List<string> AllowedLibraries { get; }

			public Tab3Permissions()
			{
				Visible = true;
				Enabled = true;

				AllowedLibraries = new List<String>();
			}

			public void LoadConfiguration(XmlNode configNode)
			{
				if (configNode == null) return;

				if (Boolean.TryParse(configNode.SelectSingleNode("./Visible")?.InnerText ?? "true", out var temp))
					Visible = temp;
				if (Boolean.TryParse(configNode.SelectSingleNode("./Enabled")?.InnerText ?? "true", out temp))
					Enabled = temp;

				foreach (var xmlNode in (configNode.SelectNodes("./LibrariesVisible/Library")?.OfType<XmlNode>() ?? new XmlNode[] { }).ToList())
					AllowedLibraries.Add(xmlNode.InnerText);
			}
		}
	}
}
