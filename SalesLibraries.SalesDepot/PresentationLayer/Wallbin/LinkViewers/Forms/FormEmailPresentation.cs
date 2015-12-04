using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormEmailPresentation : MetroForm
	{
		public FormEmailPresentation()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
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
		public PowerPointLink PowerPointLink { get; set; }

		private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
		{
			textEditEmailName.Enabled = ckChangeEmailName.Checked;
		}

		private void FormEmailPresentation_Load(object sender, EventArgs e)
		{
			Text = string.Format(Text, PowerPointLink.NameWithExtension);
			ckConvertToPDF.Checked = MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsPdf;
		}

		private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				var selectedName = ckChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : PowerPointLink.NameWithoutExtension;
				var destinationFilePath = Path.Combine(Path.GetTempPath(), ckConvertToPDF.Checked ? ((string.IsNullOrEmpty(selectedName) ? PowerPointLink.NameWithoutExtension : selectedName) + ".pdf") : (string.IsNullOrEmpty(selectedName) ? PowerPointLink.NameWithExtension : (selectedName + PowerPointLink.Extension)));
				if (ckConvertToPDF.Checked)
				{
					PowerPointSingleton.Instance.ExportSlideAsPdf(rbActiveSlide.Checked ? ActiveSlide : -1, destinationFilePath);
				}
				else if (rbActiveSlide.Checked)
					PowerPointSingleton.Instance.SaveSingleSlide(ActiveSlide, destinationFilePath);
				else
					File.Copy(PowerPointLink.FullPath, destinationFilePath, true);
				if (File.Exists(destinationFilePath))
					LinkManager.EmailFile(destinationFilePath);
			}
		}
	}
}