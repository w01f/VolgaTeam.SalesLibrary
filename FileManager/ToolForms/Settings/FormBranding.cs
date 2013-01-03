using System;
using System.Windows.Forms;
using FileManager.Controllers;

namespace FileManager.ToolForms.Settings
{
	public partial class FormBranding : Form
	{
		public FormBranding()
		{
			InitializeComponent();
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult == DialogResult.OK)
			{
				e.Cancel = true;
				if (textEditBrandingName.EditValue == null)
				{
					AppManager.Instance.ShowWarning("Branding Name is not set");
					return;
				}
				else if (string.IsNullOrEmpty(textEditBrandingName.EditValue.ToString()))
				{
					AppManager.Instance.ShowWarning("Branding Name is not set");
					return;
				}
				e.Cancel = false;
				MainController.Instance.ActiveDecorator.Library.BrandingText = textEditBrandingName.EditValue.ToString();
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			textEditBrandingName.EditValue = MainController.Instance.ActiveDecorator.Library.BrandingText;
		}
	}
}