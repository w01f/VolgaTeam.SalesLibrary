using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
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

			var selectedLinks = new List<LibraryFileLink>();
			var fileLinks = library.Pages
				.SelectMany(p => p.AllGroupLinks)
				.OfType<LibraryFileLink>()
				.Where(f => !f.IsDead && !f.IsFolder)
				.ToList();
			if (checkEditAllFiles.Checked || _formatToggles.All(item => item.Checked))
				selectedLinks.AddRange(fileLinks);
			else
			{
				if (checkEditPowerPoint.Checked)
					selectedLinks.AddRange(fileLinks.OfType<PowerPointLink>());
				if (checkEditWord.Checked)
					selectedLinks.AddRange(fileLinks.OfType<WordLink>());
				if (checkEditPdf.Checked)
					selectedLinks.AddRange(fileLinks.OfType<PdfLink>());
				if (checkEditExcel.Checked)
					selectedLinks.AddRange(fileLinks.OfType<ExcelLink>());
				if (checkEditMp4.Checked)
					selectedLinks.AddRange(fileLinks.OfType<VideoLink>().Where(fileLink => FileFormatHelper.IsMp4File(fileLink.NameWithExtension)));
				if (checkEditMov.Checked)
					selectedLinks.AddRange(fileLinks.OfType<VideoLink>().Where(fileLink => FileFormatHelper.IsMovFile(fileLink.NameWithExtension)));
				if (checkEditWmv.Checked)
					selectedLinks.AddRange(fileLinks.OfType<VideoLink>().Where(fileLink => FileFormatHelper.IsWmvFile(fileLink.NameWithExtension)));
				if (checkEditM4v.Checked)
					selectedLinks.AddRange(fileLinks.OfType<VideoLink>().Where(fileLink => FileFormatHelper.IsM4vFile(fileLink.NameWithExtension)));
				if (checkEditImages.Checked)
					selectedLinks.AddRange(fileLinks.OfType<ImageLink>());
			}

			MainController.Instance.ProcessManager.Run("Resetting OneDrive URLs...", (cancelationToken, formProgess) =>
			{
				var oneDriveConnector = new OneDriveConnector();
				AsyncHelper.RunSync(async () =>
				{
					await oneDriveConnector.ProcessLinksResetUrl(selectedLinks, cancelationToken);
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
