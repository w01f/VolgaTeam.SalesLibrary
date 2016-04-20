using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource
{
	public partial class FormCreateFolder : MetroForm
	{
		public string FolderName => textEditName.EditValue as String;

		public FormCreateFolder()
		{
			InitializeComponent();
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
