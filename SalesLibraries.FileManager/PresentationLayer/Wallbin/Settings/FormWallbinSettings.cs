using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormWallbinSettings : MetroForm
	{
		public FormWallbinSettings()
		{
			InitializeComponent();

			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
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
