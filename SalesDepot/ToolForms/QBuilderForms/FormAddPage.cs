using System;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormAddPage : Form
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
			simpleButtonAddPage.Text = isClone ? "Clone quickSITE" : "Add quickSITE";
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
