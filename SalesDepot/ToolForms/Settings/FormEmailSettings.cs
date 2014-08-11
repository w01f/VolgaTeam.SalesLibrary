using System;
using DevComponents.DotNetBar.Metro;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.ToolForms.Settings
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
				SettingsManager.Instance.EmailButtons = EmailButtonsDisplayOptions.DisplayEmailBin | EmailButtonsDisplayOptions.DisplayQuickView | EmailButtonsDisplayOptions.DisplayViewOptions;
			else if (rbEmailButtonsEnaiblePartial.Checked)
			{
				SettingsManager.Instance.EmailButtons = EmailButtonsDisplayOptions.DisplayNone;
				if (ckEnableEmailBin.Checked)
					SettingsManager.Instance.EmailButtons = SettingsManager.Instance.EmailButtons | EmailButtonsDisplayOptions.DisplayEmailBin;
				if (ckEnableQuickView.Checked)
					SettingsManager.Instance.EmailButtons = SettingsManager.Instance.EmailButtons | EmailButtonsDisplayOptions.DisplayQuickView;
				if (ckEnableViewOptions.Checked)
					SettingsManager.Instance.EmailButtons = SettingsManager.Instance.EmailButtons | EmailButtonsDisplayOptions.DisplayViewOptions;
			}
			else if (rbEmailButtonsDisableAll.Checked)
				SettingsManager.Instance.EmailButtons = EmailButtonsDisplayOptions.DisplayNone;

			SettingsManager.Instance.SaveSettings();
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			if (SettingsManager.Instance.EmailButtons == (EmailButtonsDisplayOptions.DisplayEmailBin | EmailButtonsDisplayOptions.DisplayQuickView | EmailButtonsDisplayOptions.DisplayViewOptions))
				rbEmailButtonsEnableAll.Checked = true;
			else if (SettingsManager.Instance.EmailButtons == EmailButtonsDisplayOptions.DisplayNone)
				rbEmailButtonsDisableAll.Checked = true;
			else
			{
				rbEmailButtonsEnaiblePartial.Checked = true;
				if ((SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayEmailBin) == EmailButtonsDisplayOptions.DisplayEmailBin)
					ckEnableEmailBin.Checked = true;
				if ((SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayQuickView) == EmailButtonsDisplayOptions.DisplayQuickView)
					ckEnableQuickView.Checked = true;
				if ((SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayViewOptions) == EmailButtonsDisplayOptions.DisplayViewOptions)
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