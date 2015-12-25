using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormWallbinSettings : MetroForm
	{
		public FormWallbinSettings()
		{
			InitializeComponent();
		}

		private void FormFileSettings_Load(object sender, EventArgs e)
		{
			buttonXTabPages.Checked = MainController.Instance.Settings.MultitabView;
			buttonXComboboxes.Checked = !MainController.Instance.Settings.MultitabView;
		}

		private void FormFileSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(DialogResult!=DialogResult.OK) return;
			MainController.Instance.Settings.MultitabView = buttonXTabPages.Checked;
			MainController.Instance.Settings.Save();
		}

		private void OnPageSelectorTypeButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXTabPages.Checked = false;
			buttonXComboboxes.Checked = false;
			button.Checked = true;
		}
	}
}
