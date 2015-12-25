using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormEmailLink : MetroForm
	{
		public FormEmailLink()
		{
			InitializeComponent();
			if (!((base.CreateGraphics()).DpiX > 96)) return;
			ckChangeEmailName.Font = new Font(ckChangeEmailName.Font.FontFamily, ckChangeEmailName.Font.Size - 2, ckChangeEmailName.Font.Style);
			buttonXEmail.Font = new Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 2, buttonXEmail.Font.Style);
			buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
		}

		public LibraryFileLink FileLink { get; set; }

		private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
		{
			textEditEmailName.Enabled = ckChangeEmailName.Checked;
		}

		private void FormEmailPresentation_Load(object sender, EventArgs e)
		{
			Text = string.Format(Text, FileLink.NameWithExtension);
		}

		private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			MainController.Instance.ActivityManager.AddLinkAccessActivity("Email Link", FileLink);
			var selectedName = ckChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : FileLink.NameWithExtension;
			var destinationFilePath = Path.Combine(Path.GetTempPath(), (selectedName + FileLink.Extension));
			File.Copy(FileLink.FullPath, destinationFilePath, true);
			if (File.Exists(destinationFilePath))
				LinkManager.EmailFile(destinationFilePath);
		}
	}
}