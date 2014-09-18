using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormPowerPointWarning : MetroForm
	{
		public FormPowerPointWarning()
		{
			InitializeComponent();
		}

		private void FormPowerPointWarning_Load(object sender, System.EventArgs e)
		{
			checkEditDoNotShow.Checked = SettingsManager.Instance.RunPowerPointWhenNeeded.HasValue;
		}

		private void FormPowerPointWarning_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (!checkEditDoNotShow.Checked) return;
			SettingsManager.Instance.RunPowerPointWhenNeeded = DialogResult == DialogResult.OK;
			SettingsManager.Instance.SaveSettings();
		}
	}
}
