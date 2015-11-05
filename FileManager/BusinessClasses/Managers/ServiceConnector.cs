using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.Services;
using SalesDepot.Services.IPadAdminService;

namespace FileManager.BusinessClasses
{
	class ServiceConnector
	{
		private static readonly ServiceConnector _instance = new ServiceConnector();

		private const string Login = "fm_admin";
		private const string Password = "M>f0tcGwK>c4'[V";

		private SiteClient _siteClient;
		private string _resourcesPath;

		public string WebServiceSite { get; private set; }

		public bool Connected
		{
			get { return !String.IsNullOrEmpty(WebServiceSite); }
		}

		public string ResorcesUrl
		{
			get { return String.Format("{0}{1}", WebServiceSite, _resourcesPath); }
		}

		public static ServiceConnector Instance
		{
			get { return _instance; }
		}

		private ServiceConnector() { }

		public void Init()
		{
			var serviceConnectionSettingsFilePath = Path.Combine(Path.GetDirectoryName(typeof(ServiceConnector).Assembly.Location), "credentials.xml");

			if (!File.Exists(serviceConnectionSettingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(serviceConnectionSettingsFilePath);
			var node = document.SelectSingleNode(@"/ipadsite/site");
			if (node != null)
				WebServiceSite = node.InnerText;
			node = document.SelectSingleNode(@"/ipadsite/fmresources");
			if (node != null)
				_resourcesPath = node.InnerText;
			if (Connected)
				_siteClient = new SiteClient(WebServiceSite, Login, Password);
		}

		#region Permissions Manager

		#region Users
		public bool IsUserPasswordComplex(out string message)
		{
			return _siteClient.IsUserPasswordComplex(out message);
		}

		public UserModel[] GetUsers(out string message)
		{
			return _siteClient.GetUsers(out message);
		}

		public void SetUser(string login, string password, string firstName, string lastName, string email, string phone, int role, GroupModel[] groups, SalesDepot.Services.IPadAdminService.LibraryPage[] pages, out string message)
		{
			_siteClient.SetUser(login, password, firstName, lastName, email, phone, role, groups, pages, out message);
		}

		public void DeleteUser(string login, out string message)
		{
			_siteClient.DeleteUser(login, out message);
		}

		public IEnumerable<string> LoadUserLoginsFromFile(string filePath)
		{
			var document = new XmlDocument();
			document.Load(filePath);
			return document.SelectNodes(@"/Users/User").OfType<XmlNode>().Select(node => node.InnerText.ToLower());
		}
		#endregion

		#region Groups
		public GroupModel[] GetGroups(out string message)
		{
			return _siteClient.GetGroups(out message);
		}

		public void SetGroup(string id, string name, UserModel[] users, LibraryPage[] pages, out string message)
		{
			_siteClient.SetGroup(id, name, users, pages, out message);
		}

		public void DeleteGroup(string id, out string message)
		{
			_siteClient.DeleteGroup(id, out message);
		}

		public string[] GetGroupTemplates(out string message)
		{
			return _siteClient.GetGroupTemplates(out message);
		}
		#endregion

		#region Libraraies
		public Library[] GetLibraries(out string message)
		{
			return _siteClient.GetLibraries(out message);
		}

		public void SetPage(string id, UserModel[] users, GroupModel[] groups, out string message)
		{
			_siteClient.SetPage(id, users, groups, out message);
		}
		#endregion

		#endregion

		#region Cloud Data Management
		public string GetMetaData(string dataTag, string propertyName, out string message)
		{
			return _siteClient.GetMetaData(dataTag, propertyName, out message);
		}

		public IEnumerable<SalesDepot.Services.FileManagerDataService.GroupModel> GetSecurityGroups(out string message)
		{
			return _siteClient.GetSecurityGroups(out message).ToList();
		}

		public IEnumerable<SalesDepot.Services.FileManagerDataService.Category> GetCategories(out string message)
		{
			return _siteClient.GetCategories(out message).ToList();
		}

		public IEnumerable<SalesDepot.Services.FileManagerDataService.SuperFilter> GetSuperFilters(out string message)
		{
			return _siteClient.GetSuperFilters(out message).ToList();
		}
		#endregion
	}
}
