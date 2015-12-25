using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class FormWallbinSettings : MetroForm
	{
		public FormWallbinSettings()
		{
			InitializeComponent();
		}

		private void FormFileSettings_Load(object sender, EventArgs e)
		{
			buttonXTabPages.Checked = MainController.Instance.Settings.WallbinViewSettings.MultitabView;
			buttonXComboboxes.Checked = !MainController.Instance.Settings.WallbinViewSettings.MultitabView;
			buttonXColumns.Checked = MainController.Instance.Settings.WallbinViewSettings.ClassicView;
			buttonXList.Checked = MainController.Instance.Settings.WallbinViewSettings.ListView;
			buttonXAccordion.Checked = MainController.Instance.Settings.WallbinViewSettings.AccordionView;
		}

		private void FormFileSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(DialogResult!=DialogResult.OK) return;
			MainController.Instance.Settings.WallbinViewSettings.MultitabView = buttonXTabPages.Checked;
			MainController.Instance.Settings.WallbinViewSettings.ClassicView = buttonXColumns.Checked;
			MainController.Instance.Settings.WallbinViewSettings.ListView = buttonXList.Checked;
			MainController.Instance.Settings.WallbinViewSettings.AccordionView = buttonXAccordion.Checked;
			MainController.Instance.Settings.SaveSettings();
		}

		private void OnPageSelectorTypeButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXTabPages.Checked = false;
			buttonXComboboxes.Checked = false;
			button.Checked = true;
		}

		private void OnWallbinViewTypeSelectorClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXColumns.Checked = false;
			buttonXAccordion.Checked = false;
			buttonXList.Checked = false;
			button.Checked = true;
		}
	}
}
