using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormAddPage : MetroForm
	{
		public string PageTitle
		{
			get { return textEditName.EditValue != null ? textEditName.EditValue.ToString() : null; }
		}

		public FormAddPage()
		{
			InitializeComponent();
		}

		public void Init(bool isClone)
		{
			textEditName.EditValue = null;
			Text = isClone ? "Clone quickSITE" : "Add quickSITE";
			buttonXAddPage.Text = isClone ? "Clone quickSITE" : "Add quickSITE";
		}

		private void FormAddPage_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK && String.IsNullOrEmpty(PageTitle))
			{
				AppManager.Instance.ShowWarning("You need to set Name for new quickSITE.");
				e.Cancel = true;
			}
		}
	}
}
