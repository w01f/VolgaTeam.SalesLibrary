using System;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class SaveSettingsControl : BaseSettingsControl
	{
		public SaveSettingsControl()
		{
			InitializeComponent();
			layoutControlItemOpen.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpen.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpen.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpen.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
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
