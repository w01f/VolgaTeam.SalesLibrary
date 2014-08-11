using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.BusinessClasses;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormEmailLink : MetroForm
	{
		public FormEmailLink()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				ckChangeEmailName.Font = new Font(ckChangeEmailName.Font.FontFamily, ckChangeEmailName.Font.Size - 2, ckChangeEmailName.Font.Style);
				buttonXEmail.Font = new Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 2, buttonXEmail.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		public LibraryLink link { get; set; }

		private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
		{
			textEditEmailName.Enabled = ckChangeEmailName.Checked;
		}

		private void FormEmailPresentation_Load(object sender, EventArgs e)
		{
			Text = string.Format(Text, link.NameWithExtension);
		}

		private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Email Link", link.Name, link.Type.ToString(), link.OriginalPath, link.Parent.Parent.Parent.Name, link.Parent.Parent.Name);
			string selectedName = ckChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : link.NameWithExtension;
			string destinationFilePath = Path.Combine(Path.GetTempPath(), (selectedName + link.Extension));
			File.Copy(link.LocalPath, destinationFilePath, true);
			if (File.Exists(destinationFilePath))
				LinkManager.Instance.EmailFile(destinationFilePath);
		}
	}
}