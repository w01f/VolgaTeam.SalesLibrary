using System;
using System.Linq;
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

		public void SendUserChangeNotificationEmail(string userName, string userLogin, string userEmail, string userPassword, bool newUser)
		{
			string subject;
			var bodyLines = new StringBuilder();

			var selectedSite = WebSiteManager.Instance.SelectedSite.Website;
			var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == selectedSite) ??
				new UsersEmailSettings();

			if (newUser)
			{
				subject = emailSettings.NewAccountSubject;

				bodyLines.AppendLine(String.Format("Hello {0}:", userName));
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.NewAccountBodyPlaceholder1);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}",
					emailSettings.NewAccountBodyPlaceholder2, userLogin));
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}",
					emailSettings.NewAccountBodyPlaceholder3, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.NewAccountBodyPlaceholder4);
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.NewAccountBodyPlaceholder5);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0}/auth/changePassword?login={1}&password={2}&rememberMe=", selectedSite.TrimEnd('/'), userLogin, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.NewAccountBodyPlaceholder6);
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.NewAccountBodyPlaceholder7);
				bodyLines.AppendLine();
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.NewAccountBodyPlaceholder8);
			}
			else
			{
				subject = emailSettings.ResetAccountSubject;

				bodyLines.AppendLine(String.Format("Hello {0}:", userName));
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.ResetAccountBodyPlaceholder1);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}", emailSettings.ResetAccountBodyPlaceholder2, userLogin));
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0} {1}", emailSettings.ResetAccountBodyPlaceholder3, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.ResetAccountBodyPlaceholder4);
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.ResetAccountBodyPlaceholder5);
				bodyLines.AppendLine();
				bodyLines.AppendLine(String.Format("{0}/auth/changePassword?login={1}&password={2}&rememberMe=", selectedSite.TrimEnd('/'), userLogin, userPassword));
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.ResetAccountBodyPlaceholder6);
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.ResetAccountBodyPlaceholder7);
				bodyLines.AppendLine();
				bodyLines.AppendLine();
				bodyLines.AppendLine(emailSettings.ResetAccountBodyPlaceholder8);
			}

			if (OutlookHelper.Instance.Connect())
			{
				OutlookHelper.Instance.SendMessage(emailSettings.LocalEmailAccountName, new[] { userEmail }, emailSettings.LocalEmailCopyAddresses, subject, bodyLines.ToString());
				OutlookHelper.Instance.Disconnect();
			}
		}

		public void SendUserDeleteNotificationEmail(string userName, string userLogin)
		{
			var bodyLines = new StringBuilder();

			var selectedSite = WebSiteManager.Instance.SelectedSite.Website;
			var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == selectedSite) ??
				new UsersEmailSettings();

			if (String.IsNullOrWhiteSpace(emailSettings.DeleteAccountRecipients))
				return;

			var subject = emailSettings.DeleteAccountSubject;

			bodyLines.AppendLine(String.Format("{0} {1:MM-dd-yy h:mm tt}", emailSettings.DeleteAccountBodyPlaceholder1, DateTime.Now));
			bodyLines.AppendLine();
			bodyLines.AppendLine(String.Format("{0} {1}", emailSettings.DeleteAccountBodyPlaceholder2, userName));
			bodyLines.AppendLine();
			bodyLines.AppendLine(String.Format("{0} {1}", emailSettings.DeleteAccountBodyPlaceholder3, userLogin));

			if (OutlookHelper.Instance.Connect())
			{
				OutlookHelper.Instance.SendMessage(
					emailSettings.LocalEmailAccountName,
					emailSettings.DeleteAccountRecipients
						.Split(';', ',')
						.Select(item => item.Trim())
						.Where(item => !String.IsNullOrWhiteSpace(item)),
					emailSettings.LocalEmailCopyAddresses,
					subject,
					bodyLines.ToString());
				OutlookHelper.Instance.Disconnect();
			}
		}
	}
}
