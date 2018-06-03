using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Synchronization;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	//public partial class ResetOneDriveLinksControl : UserControl, IResetLibraryContentControl
	public partial class ResetOneDriveLinksControl : XtraTabPage, IResetLibraryContentControl
	{
		private readonly List<CheckEdit> _formatToggles = new List<CheckEdit>();

		public bool SelectionMade => buttonXReset.Checked;

		public event EventHandler<EventArgs> SelectionChanged;

		public ResetOneDriveLinksControl()
		{
			InitializeComponent();

			Text = "OneDrive";

			_formatToggles.AddRange(new[]
			{
				checkEditPowerPoint,
				checkEditWord,
				checkEditExcel,
				checkEditPdf,
				checkEditMp4,
				checkEditMov,
				checkEditWmv,
				checkEditM4v,
				checkEditImages
			});


			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			layoutControlGroupSettings.Enabled = false;
		}

		public void ResetContent(Library library)
		{
			if (!buttonXReset.Checked) return;

			using (var confirmationForm = new FormResetOneDriveLinksConfirmation())
			{
				if (confirmationForm.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK)
					return;
			}

			var allPreviewContainers = library.PreviewContainers
				.OfType<FilePreviewContainer>()
				.ToList();
			var selectedPreviewContainers = new List<FilePreviewContainer>();
			if (checkEditAllFiles.Checked || _formatToggles.All(item => item.Checked))
				selectedPreviewContainers.AddRange(allPreviewContainers);
			else
			{
				if (checkEditPowerPoint.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<PowerPointPreviewContainer>());
				if (checkEditWord.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<WordPreviewContainer>());
				if (checkEditPdf.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<PdfPreviewContainer>());
				if (checkEditExcel.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<ExcelPreviewContainer>());
				if (checkEditMp4.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Mp4));
				if (checkEditMov.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Mov));
				if (checkEditWmv.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Wmv));
				if (checkEditM4v.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.M4v));
				if (checkEditImages.Checked)
					selectedPreviewContainers.AddRange(allPreviewContainers.OfType<ImagePreviewContainer>());
			}

			MainController.Instance.ProcessManager.Run("Resetting OneDrive URLs...", (cancelationToken, formProgess) =>
			{
				var oneDriveConnector = new OneDriveConnector();
				AsyncHelper.RunSync(async () =>
				{
					await oneDriveConnector.ProcessLinksResetUrl(selectedPreviewContainers, cancelationToken);
				});
			});
		}

		private void OnResetStateChanged(object sender, EventArgs e)
		{
			layoutControlGroupSettings.Enabled = buttonXReset.Checked;
			SelectionChanged?.Invoke(sender, e);
		}

		private void OnAllFilesCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupFileFormats.Enabled = !checkEditAllFiles.Checked;
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
