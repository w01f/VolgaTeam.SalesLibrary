using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesDepot.ConfigurationClasses
{
	public class PermissionsManager
	{
		private static PermissionsManager _instance = new PermissionsManager();
		private string _currentUser = Environment.UserName;
		private readonly List<UserGroup> _userGroups = new List<UserGroup>();

		public bool Configured { get; set; }

		public static PermissionsManager Instance
		{
			get { return _instance; }
		}

		private PermissionsManager()
		{
			Load();
		}

		private void Load()
		{
			if (File.Exists(SettingsManager.Instance.PermissionsFilePath))
			{
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
				this.Configured = true;
			}
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
		private readonly List<User> _users = new List<User>();
		private readonly List<LibraryPermissions> _libraryPermissions = new List<LibraryPermissions>();

		public string Name { get; set; }

		public UserGroup()
		{
			this.Name = string.Empty;
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Name":
						this.Name = childNode.InnerText;
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
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }

		public User()
		{
			this.FirstName = string.Empty;
			this.LastName = string.Empty;
			this.Phone = string.Empty;
			this.Email = string.Empty;
			this.Login = string.Empty;
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "FirstName":
						this.FirstName = childNode.InnerText;
						break;
					case "LastName":
						this.LastName = childNode.InnerText;
						break;
					case "Phone":
						this.Phone = childNode.InnerText;
						break;
					case "EmailAddress":
						this.Email = childNode.InnerText;
						break;
					case "WindowsActiveDirectoryUserName":
						this.Login = childNode.InnerText;
						break;
				}
			}
		}
	}

	public class LibraryPermissions
	{
		public string LibraryName { get; set; }
		public List<string> AvailablePages { get; private set; }

		public LibraryPermissions()
		{
			this.AvailablePages = new List<string>();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "key":
						var libraryNameNode = childNode.SelectSingleNode("string");
						if (libraryNameNode != null)
							this.LibraryName = libraryNameNode.InnerText;
						break;
					case "value":
						var pageNodes = childNode.SelectSingleNode("ArrayOfPermission");
						if (pageNodes != null)
							foreach (XmlNode pageChildNode in pageNodes.ChildNodes)
							{
								var pageNameNode = pageChildNode.SelectSingleNode("PageName");
								var pageAvailableNode = pageChildNode.SelectSingleNode("Value");
								if (pageNameNode != null && pageAvailableNode != null)
								{
									bool pageAvailable;
									if (bool.TryParse(pageAvailableNode.InnerText, out pageAvailable))
										if (pageAvailable)
											this.AvailablePages.Add(pageNameNode.InnerText);
								}
							}
						break;
				}
			}
		}
	}
}
