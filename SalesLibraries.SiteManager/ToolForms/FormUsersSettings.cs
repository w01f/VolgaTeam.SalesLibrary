using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ConfigurationClasses;

namespace SalesLibraries.SiteManager.ToolForms
{
	public partial class FormUsersSettings : MetroForm
	{
		public FormUsersSettings()
		{
			InitializeComponent();

			memoEditEmailCopyAddresses.EnableSelectAll();

			textEditNewAccountSubject.EnableSelectAll();
			textEditNewAccountBodyPlaceholder1.EnableSelectAll();
			textEditNewAccountBodyPlaceholder2.EnableSelectAll();
			textEditNewAccountBodyPlaceholder3.EnableSelectAll();
			textEditNewAccountBodyPlaceholder4.EnableSelectAll();
			textEditNewAccountBodyPlaceholder5.EnableSelectAll();
			memoEditNewAccountBodyPlaceholder6.EnableSelectAll();
			memoEditNewAccountBodyPlaceholder7.EnableSelectAll();
			textEditNewAccountBodyPlaceholder8.EnableSelectAll();

			textEditResetAccountSubject.EnableSelectAll();
			textEditResetAccountBodyPlaceholder1.EnableSelectAll();
			textEditResetAccountBodyPlaceholder2.EnableSelectAll();
			textEditResetAccountBodyPlaceholder3.EnableSelectAll();
			textEditResetAccountBodyPlaceholder4.EnableSelectAll();
			textEditResetAccountBodyPlaceholder5.EnableSelectAll();
			memoEditResetAccountBodyPlaceholder6.EnableSelectAll();
			memoEditResetAccountBodyPlaceholder7.EnableSelectAll();
			textEditResetAccountBodyPlaceholder8.EnableSelectAll();

			textEditDeleteAccountSubject.EnableSelectAll();
			textEditDeleteAccountBodyPlaceholder1.EnableSelectAll();
			textEditDeleteAccountBodyPlaceholder2.EnableSelectAll();
			textEditDeleteAccountBodyPlaceholder3.EnableSelectAll();
			memoEditDeleteAccountRecipients.EnableSelectAll();

			textEditInactiveUsersResetSubject.EnableSelectAll();
			textEditInactiveUsersResetBodyPlaceholder1.EnableSelectAll();
			memoEditInactiveUsersResetBodyPlaceholder2.EnableSelectAll();
			textEditInactiveUsersResetBodyPlaceholder3.EnableSelectAll();

			textEditInactiveUsersDeleteSubject.EnableSelectAll();
			memoEditInactiveUsersDeleteBodyPlaceholder1.EnableSelectAll();
			textEditInactiveUsersDeleteBodyPlaceholder2.EnableSelectAll();

			var scaleFactor = Utils.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSave.MaxSize, scaleFactor);
			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
								new UsersEmailSettings();

			checkEditServerEmail.Checked = !emailSettings.SendLocalEmail;
			checkEditLocalEmail.Checked = emailSettings.SendLocalEmail;

			memoEditEmailCopyAddresses.EditValue = emailSettings.LocalEmailCopyAddresses;

			if (OutlookHelper.Instance.Connect())
			{

				var accountNames = OutlookHelper.Instance.GetEmailAccounts();
				comboBoxEditLocalEmailAccount.Properties.Items.AddRange(accountNames.ToArray());
				comboBoxEditLocalEmailAccount.EditValue = accountNames.FirstOrDefault(accountName => String.Equals(accountName,
																emailSettings.LocalEmailAccountName,
																StringComparison.OrdinalIgnoreCase)) ??
															  accountNames.FirstOrDefault();
				comboBoxEditLocalEmailAccount.Enabled = accountNames.Count > 1;
				OutlookHelper.Instance.Disconnect();
			}

			textEditNewAccountSubject.EditValue = emailSettings.NewAccountSubject;
			textEditNewAccountBodyPlaceholder1.EditValue = emailSettings.NewAccountBodyPlaceholder1;
			textEditNewAccountBodyPlaceholder2.EditValue = emailSettings.NewAccountBodyPlaceholder2;
			textEditNewAccountBodyPlaceholder3.EditValue = emailSettings.NewAccountBodyPlaceholder3;
			textEditNewAccountBodyPlaceholder4.EditValue = emailSettings.NewAccountBodyPlaceholder4;
			textEditNewAccountBodyPlaceholder5.EditValue = emailSettings.NewAccountBodyPlaceholder5;
			memoEditNewAccountBodyPlaceholder6.EditValue = emailSettings.NewAccountBodyPlaceholder6;
			memoEditNewAccountBodyPlaceholder7.EditValue = emailSettings.NewAccountBodyPlaceholder7;
			textEditNewAccountBodyPlaceholder8.EditValue = emailSettings.NewAccountBodyPlaceholder8;

			textEditResetAccountSubject.EditValue = emailSettings.ResetAccountSubject;
			textEditResetAccountBodyPlaceholder1.EditValue = emailSettings.ResetAccountBodyPlaceholder1;
			textEditResetAccountBodyPlaceholder2.EditValue = emailSettings.ResetAccountBodyPlaceholder2;
			textEditResetAccountBodyPlaceholder3.EditValue = emailSettings.ResetAccountBodyPlaceholder3;
			textEditResetAccountBodyPlaceholder4.EditValue = emailSettings.ResetAccountBodyPlaceholder4;
			textEditResetAccountBodyPlaceholder5.EditValue = emailSettings.ResetAccountBodyPlaceholder5;
			memoEditResetAccountBodyPlaceholder6.EditValue = emailSettings.ResetAccountBodyPlaceholder6;
			memoEditResetAccountBodyPlaceholder7.EditValue = emailSettings.ResetAccountBodyPlaceholder7;
			textEditResetAccountBodyPlaceholder8.EditValue = emailSettings.ResetAccountBodyPlaceholder8;

			textEditDeleteAccountSubject.EditValue = emailSettings.DeleteAccountSubject;
			textEditDeleteAccountBodyPlaceholder1.EditValue = emailSettings.DeleteAccountBodyPlaceholder1;
			textEditDeleteAccountBodyPlaceholder2.EditValue = emailSettings.DeleteAccountBodyPlaceholder2;
			textEditDeleteAccountBodyPlaceholder3.EditValue = emailSettings.DeleteAccountBodyPlaceholder3;
			memoEditDeleteAccountRecipients.EditValue = emailSettings.DeleteAccountRecipients;

			var inactiveUserSettings = SettingsManager.Instance.InactiveUsersSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
								new InactiveUsersSettings();

			textEditInactiveUsersResetSubject.EditValue = inactiveUserSettings.ResetEmailSubject;
			textEditInactiveUsersResetBodyPlaceholder1.EditValue = inactiveUserSettings.ResetEmailBodyPlaceholder1;
			memoEditInactiveUsersResetBodyPlaceholder2.EditValue = inactiveUserSettings.ResetEmailBodyPlaceholder2;
			textEditInactiveUsersResetBodyPlaceholder3.EditValue = inactiveUserSettings.ResetEmailBodyPlaceholder3;

			textEditInactiveUsersDeleteSubject.EditValue = inactiveUserSettings.DeleteEmailSubject;
			memoEditInactiveUsersDeleteBodyPlaceholder1.EditValue = inactiveUserSettings.DeleteEmailBodyPlaceholder1;
			textEditInactiveUsersDeleteBodyPlaceholder2.EditValue = inactiveUserSettings.DeleteEmailBodyPlaceholder2;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;

			var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website);
			if (emailSettings == null)
			{
				emailSettings = new UsersEmailSettings();
				emailSettings.SiteUrl = WebSiteManager.Instance.SelectedSite.Website;
				SettingsManager.Instance.UsersEmailSettingItems.Add(emailSettings);
			}

			emailSettings.SendLocalEmail = checkEditLocalEmail.Checked;
			emailSettings.LocalEmailAccountName = comboBoxEditLocalEmailAccount.EditValue as String;
			emailSettings.LocalEmailCopyAddresses = memoEditEmailCopyAddresses.EditValue as String;

			emailSettings.NewAccountSubject = textEditNewAccountSubject.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder1 = textEditNewAccountBodyPlaceholder1.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder2 = textEditNewAccountBodyPlaceholder2.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder3 = textEditNewAccountBodyPlaceholder3.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder4 = textEditNewAccountBodyPlaceholder4.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder5 = textEditNewAccountBodyPlaceholder5.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder6 = memoEditNewAccountBodyPlaceholder6.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder7 = memoEditNewAccountBodyPlaceholder7.EditValue as String;
			emailSettings.NewAccountBodyPlaceholder8 = textEditNewAccountBodyPlaceholder8.EditValue as String;

			emailSettings.ResetAccountSubject = textEditResetAccountSubject.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder1 = textEditResetAccountBodyPlaceholder1.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder2 = textEditResetAccountBodyPlaceholder2.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder3 = textEditResetAccountBodyPlaceholder3.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder4 = textEditResetAccountBodyPlaceholder4.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder5 = textEditResetAccountBodyPlaceholder5.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder6 = memoEditResetAccountBodyPlaceholder6.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder7 = memoEditResetAccountBodyPlaceholder7.EditValue as String;
			emailSettings.ResetAccountBodyPlaceholder8 = textEditResetAccountBodyPlaceholder8.EditValue as String;

			emailSettings.DeleteAccountSubject = textEditDeleteAccountSubject.EditValue as String;
			emailSettings.DeleteAccountBodyPlaceholder1 = textEditDeleteAccountBodyPlaceholder1.EditValue as String;
			emailSettings.DeleteAccountBodyPlaceholder2 = textEditDeleteAccountBodyPlaceholder2.EditValue as String;
			emailSettings.DeleteAccountBodyPlaceholder3 = textEditDeleteAccountBodyPlaceholder3.EditValue as String;
			emailSettings.DeleteAccountRecipients = memoEditDeleteAccountRecipients.EditValue as String;

			UsersEmailSettings.SaveToFile(SettingsManager.Instance.UsersEmailSettingItems);

			var inactiveUserSettings = SettingsManager.Instance.InactiveUsersSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website);
			if (inactiveUserSettings == null)
			{
				inactiveUserSettings = new InactiveUsersSettings();
				inactiveUserSettings.SiteUrl = WebSiteManager.Instance.SelectedSite.Website;
				SettingsManager.Instance.InactiveUsersSettingItems.Add(inactiveUserSettings);
			}

			inactiveUserSettings.ResetEmailSubject = textEditInactiveUsersResetSubject.EditValue as String;
			inactiveUserSettings.ResetEmailBodyPlaceholder1 = textEditInactiveUsersResetBodyPlaceholder1.EditValue as String;
			inactiveUserSettings.ResetEmailBodyPlaceholder2 = memoEditInactiveUsersResetBodyPlaceholder2.EditValue as String;
			inactiveUserSettings.ResetEmailBodyPlaceholder3 = textEditInactiveUsersResetBodyPlaceholder3.EditValue as String;

			inactiveUserSettings.DeleteEmailSubject = textEditInactiveUsersDeleteSubject.EditValue as String;
			inactiveUserSettings.DeleteEmailBodyPlaceholder1 = memoEditInactiveUsersDeleteBodyPlaceholder1.EditValue as String;
			inactiveUserSettings.DeleteEmailBodyPlaceholder2 = textEditInactiveUsersDeleteBodyPlaceholder2.EditValue as String;

			InactiveUsersSettings.SaveToFile(SettingsManager.Instance.InactiveUsersSettingItems);
		}

		private void OnEmailSendModeCheckedChanged(object sender, System.EventArgs e)
		{
			layoutControlGroupLocalEmailSettings.Enabled =
				layoutControlGroupNewAccount.PageEnabled =
				layoutControlGroupResetAccount.PageEnabled =
				layoutControlGroupDeleteAccount.PageEnabled =
				layoutControlGroupInactiveUsers.PageEnabled =
				checkEditLocalEmail.Checked;
		}
	}
}
