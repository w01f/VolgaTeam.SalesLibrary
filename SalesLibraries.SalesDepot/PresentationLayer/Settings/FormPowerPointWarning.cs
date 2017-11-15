using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class FormPowerPointWarning : MetroForm
	{
		public FormPowerPointWarning()
		{
			InitializeComponent();
			layoutControlItemLaunch.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLaunch.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLaunch.MinSize = RectangleHelper.ScaleSize(layoutControlItemLaunch.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemIgnore.MaxSize = RectangleHelper.ScaleSize(layoutControlItemIgnore.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemIgnore.MinSize = RectangleHelper.ScaleSize(layoutControlItemIgnore.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void FormPowerPointWarning_Load(object sender, System.EventArgs e)
		{
			checkEditDoNotShow.Checked = MainController.Instance.Settings.RunPowerPointWhenNeeded.HasValue;
		}

		private void FormPowerPointWarning_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (!checkEditDoNotShow.Checked) return;
			MainController.Instance.Settings.RunPowerPointWhenNeeded = DialogResult == DialogResult.OK;
			MainController.Instance.Settings.SaveSettings();
		}
	}
}
