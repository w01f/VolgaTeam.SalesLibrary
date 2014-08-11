using System;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.PresentationClasses.Settings
{
	public partial class SaveSettingsControl : BaseSettingsControl
	{
		public SaveSettingsControl()
		{
			InitializeComponent();
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.FileLocationSettings();
		}
	}
}
