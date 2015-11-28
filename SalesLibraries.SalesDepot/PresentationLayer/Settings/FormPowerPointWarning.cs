using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class FormPowerPointWarning : MetroForm
	{
		public FormPowerPointWarning()
		{
			InitializeComponent();
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
