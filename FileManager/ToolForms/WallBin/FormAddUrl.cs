using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace FileManager.ToolForms.WallBin
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
			get
			{
				if (textEditWebAddress.EditValue != null)
					return textEditWebAddress.EditValue.ToString();
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
			if (textEditWebAddress.EditValue == null || string.IsNullOrEmpty(textEditWebAddress.EditValue.ToString()))
			{
				AppManager.Instance.ShowWarning("You should set the link path before saving");
				e.Cancel = true;
			}
		}
	}
}