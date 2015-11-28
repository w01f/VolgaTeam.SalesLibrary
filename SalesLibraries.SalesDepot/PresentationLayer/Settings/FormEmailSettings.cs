using System;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class FormEmailSettings : MetroForm
	{
		public FormEmailSettings()
		{
			InitializeComponent();
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			if (rbEmailButtonsEnableAll.Checked)
				MainController.Instance.Settings.EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayEmailBin | EmailButtonsDisplayOptionsEnum.DisplayQuickView | EmailButtonsDisplayOptionsEnum.DisplayViewOptions;
			else if (rbEmailButtonsEnaiblePartial.Checked)
			{
				MainController.Instance.Settings.EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayNone;
				if (ckEnableEmailBin.Checked)
					MainController.Instance.Settings.EmailButtons = MainController.Instance.Settings.EmailButtons | EmailButtonsDisplayOptionsEnum.DisplayEmailBin;
				if (ckEnableQuickView.Checked)
					MainController.Instance.Settings.EmailButtons = MainController.Instance.Settings.EmailButtons | EmailButtonsDisplayOptionsEnum.DisplayQuickView;
				if (ckEnableViewOptions.Checked)
					MainController.Instance.Settings.EmailButtons = MainController.Instance.Settings.EmailButtons | EmailButtonsDisplayOptionsEnum.DisplayViewOptions;
			}
			else if (rbEmailButtonsDisableAll.Checked)
				MainController.Instance.Settings.EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayNone;

			MainController.Instance.Settings.SaveSettings();
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			if (MainController.Instance.Settings.EmailButtons == (EmailButtonsDisplayOptionsEnum.DisplayEmailBin | EmailButtonsDisplayOptionsEnum.DisplayQuickView | EmailButtonsDisplayOptionsEnum.DisplayViewOptions))
				rbEmailButtonsEnableAll.Checked = true;
			else if (MainController.Instance.Settings.EmailButtons == EmailButtonsDisplayOptionsEnum.DisplayNone)
				rbEmailButtonsDisableAll.Checked = true;
			else
			{
				rbEmailButtonsEnaiblePartial.Checked = true;
				if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayEmailBin) == EmailButtonsDisplayOptionsEnum.DisplayEmailBin)
					ckEnableEmailBin.Checked = true;
				if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayQuickView) == EmailButtonsDisplayOptionsEnum.DisplayQuickView)
					ckEnableQuickView.Checked = true;
				if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayViewOptions) == EmailButtonsDisplayOptionsEnum.DisplayViewOptions)
					ckEnableViewOptions.Checked = true;
			}
			rbEmailButtons_CheckedChanged(null, null);
		}

		private void rbEmailButtons_CheckedChanged(object sender, EventArgs e)
		{
			ckEnableEmailBin.Enabled = rbEmailButtonsEnaiblePartial.Checked;
			ckEnableQuickView.Enabled = rbEmailButtonsEnaiblePartial.Checked;
			ckEnableViewOptions.Enabled = rbEmailButtonsEnaiblePartial.Checked;
		}
	}
}