using System;
using System.Text;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.SiteManager.ConfigurationClasses;

namespace SalesLibraries.SiteManager.BusinessClasses
{
	class LocalUsersEmailManager
	{
		public static LocalUsersEmailManager Instance { get; } = new LocalUsersEmailManager();

		private LocalUsersEmailManager() { }

		public bool IsAvailable()
		{
			var connected = OutlookHelper.Instance.Connect();
			OutlookHelper.Instance.Disconnect();
			return connected;
		}

		public void SendEmailToUser(string userName, string userLogin, string userEmail, string userPassword, bool newUser)
		{
			string subject;
			var bodyLines = new StringBuilder();
			if (newUser)
			{
				subject = SettingsManager.Instance.UsersEmailSettings.NewAccountSubject;

				bodyLines.AppendLine(String.Format("Hello {0}:", userName));
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder1);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}",
					SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder2, userLogin));
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}",
					SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder3, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder4);
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder5);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0}/auth/changePassword?login={1}&password={2}&rememberMe=",
					WebSiteManager.Instance.SelectedSite.Website, userLogin, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder6);
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder7);
				bodyLines.AppendLine();
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder8);
			}
			else
			{
				subject = SettingsManager.Instance.UsersEmailSettings.ResetAccountSubject;

				bodyLines.AppendLine(String.Format("Hello {0}:", userName));
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder1);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}", SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder2, userLogin));
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}", SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder3, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder4);
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder5);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0}/auth/changePassword?login={1}&password={2}&rememberMe=", WebSiteManager.Instance.SelectedSite.Website, userLogin, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder6);
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder7);
				bodyLines.AppendLine();
				bodyLines.AppendLine();
				bodyLines.AppendLine(SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder8);
			}

			if (OutlookHelper.Instance.Connect())
			{
				OutlookHelper.Instance.SendMessage(SettingsManager.Instance.UsersEmailSettings.LocalEmailAccountName,new[]{userEmail}, SettingsManager.Instance.UsersEmailSettings.LocalEmailCopyAddresses,subject,bodyLines.ToString());
				OutlookHelper.Instance.Disconnect();
			}
		}
	}
}
