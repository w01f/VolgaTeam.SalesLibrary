using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.SiteManager.ConfigurationClasses;

namespace SalesLibraries.SiteManager.ToolForms
{
	public partial class FormUsersSettings : MetroForm
	{
		public FormUsersSettings()
		{
			InitializeComponent();

			memoEditEmailCopyAddresses.EnableSelectAll();

			textEditNewAccountBodyPlaceholder1.EnableSelectAll();
			textEditNewAccountBodyPlaceholder2.EnableSelectAll();
			textEditNewAccountBodyPlaceholder3.EnableSelectAll();
			textEditNewAccountBodyPlaceholder4.EnableSelectAll();
			textEditNewAccountBodyPlaceholder5.EnableSelectAll();
			memoEditNewAccountBodyPlaceholder6.EnableSelectAll();
			memoEditNewAccountBodyPlaceholder7.EnableSelectAll();
			textEditNewAccountBodyPlaceholder8.EnableSelectAll();

			textEditResetAccountBodyPlaceholder1.EnableSelectAll();
			textEditResetAccountBodyPlaceholder2.EnableSelectAll();
			textEditResetAccountBodyPlaceholder3.EnableSelectAll();
			textEditResetAccountBodyPlaceholder4.EnableSelectAll();
			textEditResetAccountBodyPlaceholder5.EnableSelectAll();
			memoEditResetAccountBodyPlaceholder6.EnableSelectAll();
			memoEditResetAccountBodyPlaceholder7.EnableSelectAll();
			textEditResetAccountBodyPlaceholder8.EnableSelectAll();

			var scaleFactor = Utils.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSave.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSave.MaxSize, scaleFactor);
			layoutControlItemSave.MinSize = RectangleHelper.ScaleSize(layoutControlItemSave.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			checkEditServerEmail.Checked = !SettingsManager.Instance.UsersEmailSettings.SendLocalEmail;
			checkEditLocalEmail.Checked = SettingsManager.Instance.UsersEmailSettings.SendLocalEmail;

			memoEditEmailCopyAddresses.EditValue = SettingsManager.Instance.UsersEmailSettings.LocalEmailCopyAddresses;

			if (OutlookHelper.Instance.Connect())
			{

				var accountNames = OutlookHelper.Instance.GetEmailAccounts();
				comboBoxEditLocalEmailAccount.Properties.Items.AddRange(accountNames.ToArray());
				comboBoxEditLocalEmailAccount.EditValue = accountNames.FirstOrDefault(accountName => String.Equals(accountName,
																SettingsManager.Instance.UsersEmailSettings.LocalEmailAccountName,
																StringComparison.OrdinalIgnoreCase)) ??
															  accountNames.FirstOrDefault();
				comboBoxEditLocalEmailAccount.Enabled = accountNames.Count > 1;
				OutlookHelper.Instance.Disconnect();
			}

			textEditNewAccountSubject.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountSubject;
			textEditNewAccountBodyPlaceholder1.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder1;
			textEditNewAccountBodyPlaceholder2.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder2;
			textEditNewAccountBodyPlaceholder3.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder3;
			textEditNewAccountBodyPlaceholder4.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder4;
			textEditNewAccountBodyPlaceholder5.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder5;
			memoEditNewAccountBodyPlaceholder6.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder6;
			memoEditNewAccountBodyPlaceholder7.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder7;
			textEditNewAccountBodyPlaceholder8.EditValue = SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder8;

			textEditResetAccountSubject.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountSubject;
			textEditResetAccountBodyPlaceholder1.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder1;
			textEditResetAccountBodyPlaceholder2.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder2;
			textEditResetAccountBodyPlaceholder3.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder3;
			textEditResetAccountBodyPlaceholder4.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder4;
			textEditResetAccountBodyPlaceholder5.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder5;
			memoEditResetAccountBodyPlaceholder6.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder6;
			memoEditResetAccountBodyPlaceholder7.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder7;
			textEditResetAccountBodyPlaceholder8.EditValue = SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder8;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;

			SettingsManager.Instance.UsersEmailSettings.SendLocalEmail = checkEditLocalEmail.Checked;
			SettingsManager.Instance.UsersEmailSettings.LocalEmailAccountName = comboBoxEditLocalEmailAccount.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.LocalEmailCopyAddresses = memoEditEmailCopyAddresses.EditValue as String;

			SettingsManager.Instance.UsersEmailSettings.NewAccountSubject = textEditNewAccountSubject.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder1 = textEditNewAccountBodyPlaceholder1.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder2 = textEditNewAccountBodyPlaceholder2.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder3 = textEditNewAccountBodyPlaceholder3.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder4 = textEditNewAccountBodyPlaceholder4.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder5 = textEditNewAccountBodyPlaceholder5.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder6 = memoEditNewAccountBodyPlaceholder6.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder7 = memoEditNewAccountBodyPlaceholder7.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.NewAccountBodyPlaceholder8 = textEditNewAccountBodyPlaceholder8.EditValue as String;

			SettingsManager.Instance.UsersEmailSettings.ResetAccountSubject = textEditResetAccountSubject.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder1 = textEditResetAccountBodyPlaceholder1.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder2 = textEditResetAccountBodyPlaceholder2.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder3 = textEditResetAccountBodyPlaceholder3.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder4 = textEditResetAccountBodyPlaceholder4.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder5 = textEditResetAccountBodyPlaceholder5.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder6 = memoEditResetAccountBodyPlaceholder6.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder7 = memoEditResetAccountBodyPlaceholder7.EditValue as String;
			SettingsManager.Instance.UsersEmailSettings.ResetAccountBodyPlaceholder8 = textEditResetAccountBodyPlaceholder8.EditValue as String;

			SettingsManager.Instance.UsersEmailSettings.Save();
		}

		private void OnEmailSendModeCheckedChanged(object sender, System.EventArgs e)
		{
			layoutControlGroupLocalEmailSettings.Enabled =
				layoutControlGroupNewAccount.PageEnabled =
				layoutControlGroupResetAccount.PageEnabled =
				checkEditLocalEmail.Checked;
		}
	}
}
