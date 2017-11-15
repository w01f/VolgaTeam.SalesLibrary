using System;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class PowerPointStartupSettingsControl : BaseSettingsControl
	{
		private bool _allowToSave;

		public PowerPointStartupSettingsControl()
		{
			InitializeComponent();
			layoutControlItemEnabled.MaxSize = RectangleHelper.ScaleSize(layoutControlItemEnabled.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemEnabled.MinSize = RectangleHelper.ScaleSize(layoutControlItemEnabled.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisabled.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDisabled.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisabled.MinSize = RectangleHelper.ScaleSize(layoutControlItemDisabled.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public override void LoadData()
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
