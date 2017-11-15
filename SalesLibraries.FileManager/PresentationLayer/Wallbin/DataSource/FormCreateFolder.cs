using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public partial class FormCreateFolder : MetroForm
	{
		public string FolderName => textEditName.EditValue as String;

		public FormCreateFolder()
		{
			InitializeComponent();

			layoutControlItemCreate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCreate.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCreate.MinSize = RectangleHelper.ScaleSize(layoutControlItemCreate.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void FormCreateFolder_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(FolderName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set folder name");
				e.Cancel = true;
			}
		}
	}
}
