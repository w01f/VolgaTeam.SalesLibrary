using System.ComponentModel;
using DevComponents.DotNetBar;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.PresentationClasses.Settings
{
	public partial class QuickViewSettingsControl : BaseSettingsControl
	{
		protected bool _allowToSave;

		public QuickViewSettingsControl()
		{
			InitializeComponent();
		}

		public void LoadData()
		{
			_allowToSave = false;
			buttonXImages.Checked = !SettingsManager.Instance.OldStyleQuickView;
			buttonXSlides.Checked = SettingsManager.Instance.OldStyleQuickView;
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
			SettingsManager.Instance.OldStyleQuickView = buttonXSlides.Checked;
			SettingsManager.Instance.SaveSettings();
		}
	}
}
