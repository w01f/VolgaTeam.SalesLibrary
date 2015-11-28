using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormEmailList : MetroForm
	{
		public Library Library { get; set; }
		public FormEmailList()
		{
			InitializeComponent();
			if (!((CreateGraphics()).DpiX > 96)) return;
			laDescription.Font = new Font(laDescription.Font.FontFamily, laDescription.Font.Size - 2, laDescription.Font.Style);
			laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			radioButtonCreateEmail.Font = new Font(radioButtonCreateEmail.Font.FontFamily, radioButtonCreateEmail.Font.Size - 2, radioButtonCreateEmail.Font.Style);
			radioButtonSendEmail.Font = new Font(radioButtonSendEmail.Font.FontFamily, radioButtonSendEmail.Font.Size - 2, radioButtonSendEmail.Font.Style);
		}

		private void Form_Load(object sender, EventArgs e)
		{
			memoEditEmails.EditValue = String.Join(";", Library.InactiveLinksSettings.EmailList);
			radioButtonCreateEmail.Checked = !Library.InactiveLinksSettings.SendEmail;
			radioButtonSendEmail.Checked = Library.InactiveLinksSettings.SendEmail;
		}

		private void FormEmailList_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (memoEditEmails.EditValue == null) return;
			Library.InactiveLinksSettings.EmailList.Clear();
			foreach (var email in memoEditEmails.EditValue.ToString().Split(new[] { ';' }).Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)))
				Library.InactiveLinksSettings.EmailList.Add(email.Trim());
			Library.InactiveLinksSettings.SendEmail = radioButtonSendEmail.Checked;
		}
	}
}