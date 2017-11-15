using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
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
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public int ActiveSlide { get; set; }
		public PowerPointLink PowerPointLink { get; set; }

		private void ckChangeEmailName_CheckedChanged(object sender, EventArgs e)
		{
			textEditEmailName.Enabled = checkEditChangeEmailName.Checked;
		}

		private void FormEmailPresentation_Load(object sender, EventArgs e)
		{
			Text = string.Format(Text, PowerPointLink.NameWithExtension);
			checkEditConvertToPDF.Checked = MainController.Instance.Settings.EmailBinSettings.EmailBinSendAsPdf;
		}

		private void FormEmailPresentation_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				var selectedName = checkEditChangeEmailName.Checked && textEditEmailName.EditValue != null ? textEditEmailName.EditValue.ToString() : PowerPointLink.NameWithoutExtension;
				var destinationFilePath = Path.Combine(Path.GetTempPath(), checkEditConvertToPDF.Checked ? ((string.IsNullOrEmpty(selectedName) ? PowerPointLink.NameWithoutExtension : selectedName) + ".pdf") : (string.IsNullOrEmpty(selectedName) ? PowerPointLink.NameWithExtension : (selectedName + PowerPointLink.Extension)));
				if (checkEditConvertToPDF.Checked)
				{
					PowerPointSingleton.Instance.ExportSlideAsPdf(checkEditActiveSlide.Checked ? ActiveSlide : -1, destinationFilePath);
				}
				else if (checkEditActiveSlide.Checked)
					PowerPointSingleton.Instance.SaveSingleSlide(ActiveSlide, destinationFilePath);
				else
					File.Copy(PowerPointLink.FullPath, destinationFilePath, true);
				if (File.Exists(destinationFilePath))
					LinkManager.EmailFile(destinationFilePath);
			}
		}
	}
}