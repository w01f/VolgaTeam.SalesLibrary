using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders
{
	public partial class FormAddUrl : MetroForm
	{
		public FormAddUrl()
		{
			InitializeComponent();
		}

		public string LinkName
		{
			get { return edLinkName.Text; }
		}

		public string LinkPath
		{
			get { return textEditWebAddress.EditValue as String; }
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
		}
	}
}