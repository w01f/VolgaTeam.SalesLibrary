using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders
{
	public partial class FormAddNetworkLink : MetroForm
	{
		public FormAddNetworkLink()
		{
			InitializeComponent();
		}

		public string LinkName
		{
			get { return edLinkName.Text; }
		}

		public string LinkPath
		{
			get { return buttonEditFolderPath.EditValue as String; }
		}

		private void AddLinkForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(LinkName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link name before saving");
				e.Cancel = true;
				return;
			}
			if (String.IsNullOrEmpty(LinkPath))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the link path before saving");
				e.Cancel = true;
			}
			else if (!(Directory.Exists(LinkPath) || File.Exists(LinkPath)))
			{
				MainController.Instance.PopupMessages.ShowWarning("Link path is not correct");
				e.Cancel = true;
			}
		}

		private void buttonEditFolderPath_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				buttonEditFolderPath.EditValue = folderBrowserDialog.SelectedPath;
		}
	}
}