using System;
using SalesDepot.ToolForms.Settings;

namespace SalesDepot.PresentationClasses.Settings
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
				form.ShowDialog();
			}
		}
	}
}
