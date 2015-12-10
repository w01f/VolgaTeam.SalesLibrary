using System;
using DevComponents.DotNetBar;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class PowerPointStartupSettingsControl : BaseSettingsControl
	{
		private bool _allowToSave;

		public PowerPointStartupSettingsControl()
		{
			InitializeComponent();
		}

		public void LoadData()
		{
			_allowToSave = false;
			buttonXEnabled.Checked = !MainController.Instance.Settings.RunPowerPointWhenNeeded.HasValue;
			buttonXDisabled.Checked = MainController.Instance.Settings.RunPowerPointWhenNeeded.HasValue;
			_allowToSave = true;
		}

		private void OnToggleButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXEnabled.Checked = false;
			buttonXDisabled.Checked = false;
			button.Checked = true;
		}

		private void OnToggleButtonCheckedChanged(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			MainController.Instance.Settings.RunPowerPointWhenNeeded = buttonXDisabled.Checked ?
				true :
				(bool?)null;
			MainController.Instance.Settings.SaveSettings();
		}
	}
}
