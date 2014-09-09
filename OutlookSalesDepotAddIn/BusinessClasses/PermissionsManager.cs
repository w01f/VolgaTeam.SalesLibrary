using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class PermissionsManager
	{
		private static readonly PermissionsManager _instance = new PermissionsManager();
		private readonly string _currentUser = Environment.UserName;
		private readonly List<UserGroup> _userGroups = new List<UserGroup>();

		private PermissionsManager()
		{
			Load();
		}

		public bool Configured { get; set; }

		public static PermissionsManager Instance
		{
			get { return _instance; }
		}

		private void Load()
		{
			if (!File.Exists(SettingsManager.Instance.PermissionsFilePath)) return;
			var document = new XmlDocument();
			document.Load(SettingsManager.Instance.PermissionsFilePath);

			var node = document.SelectSingleNode(@"/ArrayOfUserGroup");
			if (node != null)
				foreach (XmlNode childNode in node.ChildNodes)
					if (childNode.Name.Equals("UserGroup"))
					{
						var userGroup = new UserGroup();
						userGroup.Deserialize(childNode);
						_userGroups.Add(userGroup);
					}
			Configured = true;
		}

		public bool IsUserAutorized()
		{
			return _userGroups.Any(x => x.HasUser(_currentUser));
		}

		public IEnumerable<string> GetAvailableLibraries()
		{
			var libraries = new List<string>();
			foreach (var userGroup in _userGroups)
				libraries.AddRange(userGroup.GetAvailableLibraries(_currentUser));
			return libraries;
		}

		public IEnumerable<string> GetAvailablePages(string libraryName)
		{
			var pages = new List<string>();
			foreach (var userGroup in _userGroups)
				pages.AddRange(userGroup.GetAvailablePages(libraryName, _currentUser));
			return pages;
		}
	}

	public class UserGroup
	{
		private readonly List<LibraryPermissions> _libraryPermissions = new List<LibraryPermissions>();
		private readonly List<User> _users = new List<User>();

		public UserGroup()
		{
			Name = string.Empty;
		}

		public string Name { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Users":
						foreach (XmlNode userNode in childNode.ChildNodes)
						{
							var user = new User();
							user.Deserialize(userNode);
							_users.Add(user);
						}
						break;
					case "Permissions":
						foreach (XmlNode permissionNode in childNode.ChildNodes)
						{
							var permission = new LibraryPermissions();
							permission.Deserialize(permissionNode);
							_libraryPermissions.Add(permission);
						}
						break;
				}
			}
		}

		public bool HasUser(string login)
		{
			return _users.Any(x => x.Login.ToLower().Equals(login.ToLower()));
		}

		public string[] GetAvailableLibraries(string login)
		{
			if (_users.Any(x => x.Login.ToLower().Equals(login.ToLower())))
				return _libraryPermissions.Select(x => x.LibraryName.ToLower()).ToArray();
			return new string[] { };
		}

		public string[] GetAvailablePages(string libraryName, string login)
		{
			if (_users.Any(x => x.Login.ToLower().Equals(login.ToLower())))
				return _libraryPermissions.Where(x => x.LibraryName.ToLower().Equals(libraryName.ToLower())).SelectMany(x => x.AvailablePages.Select(y => y.Trim().ToLower())).ToArray();
			return new string[] { };
		}
	}

	public class User
	{
		public User()
		{
			FirstName = string.Empty;
			LastName = string.Empty;
			Phone = string.Empty;
			Email = string.Empty;
			Login = string.Empty;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "FirstName":
						FirstName = childNode.InnerText;
						break;
					case "LastName":
						LastName = childNode.InnerText;
						break;
					case "Phone":
						Phone = childNode.InnerText;
						break;
					case "EmailAddress":
						Email = childNode.InnerText;
						break;
					case "WindowsActiveDirectoryUserName":
						Login = childNode.InnerText;
						break;
				}
			}
		}
	}

	public class LibraryPermissions
	{
		public LibraryPermissions()
		{
			AvailablePages = new List<string>();
		}

		public string LibraryName { get; set; }
		public List<string> AvailablePages { get; private set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "key":
						XmlNode libraryNameNode = childNode.SelectSingleNode("string");
						if (libraryNameNode != null)
							LibraryName = libraryNameNode.InnerText;
						break;
					case "value":
						XmlNode pageNodes = childNode.SelectSingleNode("ArrayOfPermission");
						if (pageNodes != null)
							foreach (XmlNode pageChildNode in pageNodes.ChildNodes)
							{
								XmlNode pageNameNode = pageChildNode.SelectSingleNode("PageName");
								XmlNode pageAvailableNode = pageChildNode.SelectSingleNode("Value");
								if (pageNameNode != null && pageAvailableNode != null)
								{
									bool pageAvailable;
									if (bool.TryParse(pageAvailableNode.InnerText, out pageAvailable))
										if (pageAvailable)
											AvailablePages.Add(pageNameNode.InnerText);
								}
							}
						break;
				}
			}
		}
	}
}