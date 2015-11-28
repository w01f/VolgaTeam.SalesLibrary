using System;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class SaveSettingsControl : BaseSettingsControl
	{
		public SaveSettingsControl()
		{
			InitializeComponent();
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			using (var form = new FormFileSettings())
			{
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}
	}
}
