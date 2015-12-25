using System;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class EmailSettingsControl : BaseSettingsControl
	{
		public EmailSettingsControl()
		{
			InitializeComponent();
		}

		private void buttonXOpen_Click(object sender, EventArgs e)
		{
			using (var form = new FormEmailSettings())
			{
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}
	}
}
