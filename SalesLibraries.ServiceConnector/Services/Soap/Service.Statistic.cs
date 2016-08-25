using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.StatisticService;

namespace SalesLibraries.ServiceConnector.Services.Soap
{
	public partial class SoapServiceConnection
	{
		private StatisticControllerService GetStatisticClient()
		{
			if (!IsConnected) return null;
			try
			{
				var client = new StatisticControllerService
				{
					Url = string.Format("{0}/statistic/quote?ws=1", Website)
				};
				return client;
			}
			catch
			{
				return null;
			}
		}

		public UserActivity[] GetActivities(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<UserActivity>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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

		public AccessReportModel[] GetAccessReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<AccessReportModel>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
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

		public FileActivityReportModel[] GetFileActivityReportLegacy(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<FileActivityReportModel>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getFileActivityReportLegacy(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new FileActivityReportModel[] { });
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
					var sessionKey = client.getSessionKey(Login, Password);
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

		public LibraryFilesModel[] GetLibraryFiles(out string message)
		{
			message = string.Empty;
			var activities = new List<LibraryFilesModel>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(Login, Password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getLibraryFiles(sessionKey) ?? new LibraryFilesModel[] { });
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
	}
}