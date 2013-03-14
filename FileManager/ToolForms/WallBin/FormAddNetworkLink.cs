using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace FileManager.ToolForms.WallBin
{
	public partial class FormAddNetworkLink : Form
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
			get
			{
				if (buttonEditFolderPath.EditValue != null)
					return buttonEditFolderPath.EditValue.ToString();
				return string.Empty;
			}
		}

		private void AddLinkForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (string.IsNullOrEmpty(edLinkName.Text))
			{
				AppManager.Instance.ShowWarning("You should set the link name before saving");
				e.Cancel = true;
				return;
			}
			if (buttonEditFolderPath.EditValue == null || string.IsNullOrEmpty(buttonEditFolderPath.EditValue.ToString()))
			{
				AppManager.Instance.ShowWarning("You should set the link path before saving");
				e.Cancel = true;
			}
			else if (!(Directory.Exists(buttonEditFolderPath.EditValue.ToString()) || File.Exists(buttonEditFolderPath.EditValue.ToString())))
			{
				AppManager.Instance.ShowWarning("Link path is not correct");
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