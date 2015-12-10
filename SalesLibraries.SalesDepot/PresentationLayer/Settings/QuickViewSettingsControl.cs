using DevComponents.DotNetBar;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class QuickViewSettingsControl : BaseSettingsControl
	{
		private bool _allowToSave;

		public QuickViewSettingsControl()
		{
			InitializeComponent();
		}

		public override void LoadData()
		{
			_allowToSave = false;
			buttonXImages.Checked = !MainController.Instance.Settings.LinkLaunchSettings.OldStyleQuickView;
			buttonXSlides.Checked = MainController.Instance.Settings.LinkLaunchSettings.OldStyleQuickView;
			_allowToSave = true;
		}

		private void Button_Click(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked) return;
			_allowToSave = false;
			buttonXImages.Checked = false;
			buttonXSlides.Checked = false;
			_allowToSave = true;
			button.Checked = true;
		}

		private void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			MainController.Instance.Settings.LinkLaunchSettings.OldStyleQuickView = buttonXSlides.Checked;
			MainController.Instance.Settings.SaveSettings();
		}
	}
}
