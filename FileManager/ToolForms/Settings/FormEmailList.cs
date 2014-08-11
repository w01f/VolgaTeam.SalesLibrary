using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using FileManager.Controllers;

namespace FileManager.ToolForms.Settings
{
	public partial class FormEmailList : MetroForm
	{
		public FormEmailList()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laDescription.Font = new Font(laDescription.Font.FontFamily, laDescription.Font.Size - 2, laDescription.Font.Style);
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				radioButtonCreateEmail.Font = new Font(radioButtonCreateEmail.Font.FontFamily, radioButtonCreateEmail.Font.Size - 2, radioButtonCreateEmail.Font.Style);
				radioButtonSendEmail.Font = new Font(radioButtonSendEmail.Font.FontFamily, radioButtonSendEmail.Font.Size - 2, radioButtonSendEmail.Font.Style);
			}
		}

		private void Form_Load(object sender, EventArgs e)
		{
			memoEditEmails.EditValue = string.Join("; ", MainController.Instance.ActiveDecorator.Library.EmailList.ToArray());
			radioButtonCreateEmail.Checked = !MainController.Instance.ActiveDecorator.Library.SendEmail;
			radioButtonSendEmail.Checked = MainController.Instance.ActiveDecorator.Library.SendEmail;
		}

		private void FormEmailList_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				if (memoEditEmails.EditValue != null)
				{
					MainController.Instance.ActiveDecorator.Library.EmailList.Clear();
					foreach (string email in memoEditEmails.EditValue.ToString().Split(new[] { ';' }))
						if (!string.IsNullOrEmpty(email.Trim()))
							MainController.Instance.ActiveDecorator.Library.EmailList.Add(email.Trim());
					MainController.Instance.ActiveDecorator.Library.SendEmail = radioButtonSendEmail.Checked;
					MainController.Instance.ActiveDecorator.Library.Save();
				}
		}
	}
}