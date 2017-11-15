using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class FormEmailSettings : MetroForm
	{
		public FormEmailSettings()
		{
			InitializeComponent();
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			if (MainController.Instance.Settings.EmailButtons == (EmailButtonsDisplayOptionsEnum.DisplayEmailBin | EmailButtonsDisplayOptionsEnum.DisplayQuickView | EmailButtonsDisplayOptionsEnum.DisplayViewOptions))
				checkEditEmailButtonsEnableAll.Checked = true;
			else if (MainController.Instance.Settings.EmailButtons == EmailButtonsDisplayOptionsEnum.DisplayNone)
				checkEditEmailButtonsDisableAll.Checked = true;
			else
			{
				checkEditEmailButtonsEnaiblePartial.Checked = true;
				if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayEmailBin) == EmailButtonsDisplayOptionsEnum.DisplayEmailBin)
					checkEditEnableEmailBin.Checked = true;
				if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayQuickView) == EmailButtonsDisplayOptionsEnum.DisplayQuickView)
					checkEditEnableQuickView.Checked = true;
				if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayViewOptions) == EmailButtonsDisplayOptionsEnum.DisplayViewOptions)
					checkEditEnableViewOptions.Checked = true;
			}
			rbEmailButtons_CheckedChanged(null, null);
		}

		private void FormEmailSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			if(DialogResult!=DialogResult.OK) return;
			if (checkEditEmailButtonsEnableAll.Checked)
				MainController.Instance.Settings.EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayEmailBin | EmailButtonsDisplayOptionsEnum.DisplayQuickView | EmailButtonsDisplayOptionsEnum.DisplayViewOptions;
			else if (checkEditEmailButtonsEnaiblePartial.Checked)
			{
				MainController.Instance.Settings.EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayNone;
				if (checkEditEnableEmailBin.Checked)
					MainController.Instance.Settings.EmailButtons = MainController.Instance.Settings.EmailButtons | EmailButtonsDisplayOptionsEnum.DisplayEmailBin;
				if (checkEditEnableQuickView.Checked)
					MainController.Instance.Settings.EmailButtons = MainController.Instance.Settings.EmailButtons | EmailButtonsDisplayOptionsEnum.DisplayQuickView;
				if (checkEditEnableViewOptions.Checked)
					MainController.Instance.Settings.EmailButtons = MainController.Instance.Settings.EmailButtons | EmailButtonsDisplayOptionsEnum.DisplayViewOptions;
			}
			else if (checkEditEmailButtonsDisableAll.Checked)
				MainController.Instance.Settings.EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayNone;

			MainController.Instance.Settings.SaveSettings();
		}

		private void rbEmailButtons_CheckedChanged(object sender, EventArgs e)
		{
			checkEditEnableEmailBin.Enabled = checkEditEmailButtonsEnaiblePartial.Checked;
			checkEditEnableQuickView.Enabled = checkEditEmailButtonsEnaiblePartial.Checked;
			checkEditEnableViewOptions.Enabled = checkEditEmailButtonsEnaiblePartial.Checked;
		}
	}
}