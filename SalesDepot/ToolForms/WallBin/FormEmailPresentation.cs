using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.InteropClasses;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormEmailPresentation : MetroForm
	{
		public FormEmailPresentation()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				ckConvertToPDF.Font = new Font(ckConvertToPDF.Font.FontFamily, ckConvertToPDF.Font.Size - 2, ckConvertToPDF.Font.Style);
				ckChangeEmailName.Font = new Font(ckChangeEmailName.Font.FontFamily, ckChangeEmailName.Font.Size - 2, ckChangeEmailName.Font.Style);
				rbActiveSlide.Font = new Font(rbActiveSlide.Font.FontFamily, rbActiveSlide.Font.Size - 2, rbActiveSlide.Font.Style);
				rbAllSlides.Font = new Font(rbAllSlides.Font.FontFamily, rbAllSlides.Font.Size - 2, rbAllSlides.Font.Style);
				buttonXEmail.Font = new Font(buttonXEmail.Font.FontFamily, buttonXEmail.Font.Size - 2, buttonXEmail.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		public int ActiveSlide { get; set; }
		public LibraryLink SelectedFile { get; set; }

		private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
		{
			textEditEmailName.Enabled = ckChangeEmailName.Checked;
		}

		private void FormEmailPresentation_Load(object sender, EventArgs e)
		{
			Text = string.Format(Text, SelectedFile.NameWithExtension);
			ckConvertToPDF.Enabled = SettingsManager.Instance.EnablePdfConverting;
			ckConvertToPDF.Checked = SettingsManager.Instance.EnablePdfConverting & SettingsManager.Instance.EmailBinSendAsPdf;
		}

		private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				string selectedName = ckChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : SelectedFile.NameWithoutExtesion;
				string destinationFilePath = Path.Combine(Path.GetTempPath(), ckConvertToPDF.Checked ? ((string.IsNullOrEmpty(selectedName) ? SelectedFile.NameWithoutExtesion : selectedName) + ".pdf") : (string.IsNullOrEmpty(selectedName) ? SelectedFile.NameWithExtension : (selectedName + SelectedFile.Extension)));
				if (ckConvertToPDF.Checked)
				{
					PowerPointHelper.Instance.ExportPresentationAsPDF(rbActiveSlide.Checked ? ActiveSlide : -1, destinationFilePath);
				}
				else if (rbActiveSlide.Checked)
					PowerPointHelper.Instance.SaveSingleSlide(ActiveSlide, destinationFilePath);
				else
					File.Copy(SelectedFile.LocalPath, destinationFilePath, true);
				if (File.Exists(destinationFilePath))
					LinkManager.Instance.EmailFile(destinationFilePath);
			}
		}
	}
}