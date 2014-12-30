using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SalesDepot.Services.InactiveUsersService;
using SalesDepot.Services.IPadAdminService;
using SalesDepot.Services.StatisticService;
using SalesDepot.Services.TickerService;
using GroupModel = SalesDepot.Services.IPadAdminService.GroupModel;
using Library = SalesDepot.Services.IPadAdminService.Library;
using LibraryPage = SalesDepot.Services.IPadAdminService.LibraryPage;
using UserModel = SalesDepot.Services.IPadAdminService.UserModel;

namespace SalesDepot.Services
{
	public partial class SiteClient
	{
		protected string _login;
		protected string _password;

		public string _website;

		public SiteClient(XmlNode node)
		{
			Deserialize(node);
		}

		public SiteClient(string website, string login, string password)
		{
			_website = website;
			_login = login;
			_password = password;
		}

		public string Website
		{
			get { return _website; }
		}

		public string User
		{
			get { return _login; }
		}

		public string Password
		{
			get { return _password; }
		}

		public override string ToString()
		{
			return _website;
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Url":
						_website = childNode.InnerText;
						break;
					case "User":
						_login = childNode.InnerText;
						break;
					case "Password":
						_password = childNode.InnerText;
						break;
				}
			}
		}

		private AdminControllerService GetAdminClient()
		{
			try
			{
				var client = new AdminControllerService();
				client.Url = string.Format("{0}/admin/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		private StatisticControllerService GetStatisticClient()
		{
			try
			{
				var client = new StatisticControllerService();
				client.Timeout = 600000;
				client.Url = string.Format("{0}/statistic/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		private TickerControllerService GetTickerClient()
		{
			try
			{
				var client = new TickerControllerService();
				client.Timeout = 600000;
				client.Url = string.Format("{0}/ticker/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		private InactiveusersControllerService GetInactiveUsersClient()
		{
			try
			{
				var client = new InactiveusersControllerService();
				client.Timeout = 600000;
				client.Url = string.Format("{0}/inactiveusers/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		#region Users
		public bool IsUserPasswordComplex(out string message)
		{
			message = string.Empty;
			bool result = true;
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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

		public UserModel[] GetUsers(out string message)
		{
			message = string.Empty;
			var users = new List<UserModel>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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
			return users.ToArray();
		}

		public void SetUser(string login, string password, string firstName, string lastName, string email, string phone, int role, GroupModel[] groups, LibraryPage[] pages, out string message)
		{
			message = string.Empty;
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						IEnumerable<GroupModel> uniqueGroups = users.SelectMany(user => user.Groups).Where(group => group.IsNew).Distinct();
						foreach (GroupModel group in uniqueGroups)
							client.setGroup(sessionKey, group.id, group.name, new UserModel[] { }, new LibraryPage[] { });
						foreach (UserInfo user in users)
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
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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
		#endregion

		#region Groups
		public GroupModel[] GetGroups(out string message)
		{
			message = string.Empty;
			var groups = new List<GroupModel>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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
			return groups.ToArray();
		}

		public GroupModel[] GetGroupsByLibrary(string libraryId, out string message)
		{
			message = string.Empty;
			var groups = new List<GroupModel>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						groups.AddRange(client.getGroupsByLibrary(sessionKey, libraryId) ?? new GroupModel[] { });
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

		public void SetGroup(string id, string name, UserModel[] users, LibraryPage[] pages, out string message)
		{
			message = string.Empty;
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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

		public string[] GetGroupTemplates(out string message)
		{
			message = string.Empty;
			var groupTemplates = new List<string>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
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
			return groupTemplates.ToArray();
		}
		#endregion

		#region Libraraies
		public Library[] GetLibraries(out string message)
		{
			message = string.Empty;
			var libraries = new List<Library>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						libraries.AddRange(client.getLibraries(sessionKey) ?? new Library[] { });
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
			return libraries.ToArray();
		}

		public void SetPage(string id, UserModel[] users, GroupModel[] groups, out string message)
		{
			message = string.Empty;
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setPage(sessionKey, id, users, groups);
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
		#endregion

		#region Activities
		public UserActivity[] GetActivities(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<UserActivity>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						while (startDate < endDate)
						{
							DateTime nextDate = startDate.AddDays(10);
							if (nextDate > endDate)
								nextDate = endDate;
							activities.AddRange(client.getActivities(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), nextDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new UserActivity[] { });
							startDate = nextDate;
						}
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
			return activities.ToArray();
		}

		public MainUserReportModel[] GetMainUserReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<MainUserReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getMainUserReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new MainUserReportModel[] { });
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
			return activities.ToArray();
		}

		public MainGroupReportModel[] GetMainGroupReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<MainGroupReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getMainGroupReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new MainGroupReportModel[] { });
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
			return activities.ToArray();
		}

		public NavigationUserReportModel[] GetNavigationUserReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<NavigationUserReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getNavigationUserReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new NavigationUserReportModel[] { });
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
			return activities.ToArray();
		}

		public NavigationGroupReportModel[] GetNavigationGroupReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<NavigationGroupReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getNavigationGroupReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new NavigationGroupReportModel[] { });
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
			return activities.ToArray();
		}

		public AccessReportModel[] GetAccessReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<AccessReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getAccessReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new AccessReportModel[] { });
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
			return activities.ToArray();
		}

		public QuizPassUserReportModel[] GetQuizPassUserReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<QuizPassUserReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getQuizPassUserReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new QuizPassUserReportModel[] { });
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
			return activities.ToArray();
		}

		public QuizPassGroupReportModel[] GetQuizPassGroupReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<QuizPassGroupReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getQuizPassGroupReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new QuizPassGroupReportModel[] { });
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
			return activities.ToArray();
		}

		public FileActivityReportModel[] GetFileActivityReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<FileActivityReportModel>();
			StatisticControllerService client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getFileActivityReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new FileActivityReportModel[] { });
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
			return activities.ToArray();
		}

		public VideoLinkInfo[] GetVideoLinkInfo(out string message)
		{
			message = string.Empty;
			var activities = new List<VideoLinkInfo>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getVideoLinkInfo(sessionKey) ?? new VideoLinkInfo[] { });
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
			return activities.ToArray();
		}
		#endregion

		#region Ticker
		public TickerLink[] GetTickerLinks(out string message)
		{
			message = string.Empty;
			var records = new List<TickerLink>();
			TickerControllerService client = GetTickerClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						records.AddRange(client.getTickerLinks(sessionKey) ?? new TickerLink[] { });
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
			return records.ToArray();
		}

		public void SetTickerLinks(TickerLink[] tickerLinks, out string message)
		{
			message = string.Empty;
			TickerControllerService client = GetTickerClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setTickerLinks(sessionKey, tickerLinks);
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
		#endregion

		#region Inactive Users
		public InactiveUsersService.UserModel[] GetInactiveUsers(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var userRecords = new List<InactiveUsersService.UserModel>();
			InactiveusersControllerService client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						userRecords.AddRange(client.getInactiveUsers(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new InactiveUsersService.UserModel[] { });
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
			return userRecords.ToArray();
		}

		public void ResetUsers(string[] userIds, bool onlyEmail, string sender, string subject, string body, out string message)
		{
			message = string.Empty;
			InactiveusersControllerService client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.resetUsers(sessionKey, userIds, onlyEmail, sender, subject, body);
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

		public void DeleteUsers(string[] userIds, bool onlyEmail, string sender, string subject, string body, out string message)
		{
			message = string.Empty;
			InactiveusersControllerService client = GetInactiveUsersClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteUsers(sessionKey, userIds, onlyEmail, sender, subject, body);
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
		#endregion
	}
}