using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	//public partial class ResetPreviewContentControl : UserControl, IResetLibraryContentControl
	public partial class ResetPreviewContentControl : XtraTabPage, IResetLibraryContentControl
	{
		private readonly List<CheckEdit> _wvFormatsToggles = new List<CheckEdit>();

		public bool SelectionMade =>
				(buttonXRefreshWV.Checked || buttonXDeleteWV.Checked) && (checkEditAllFiles.Checked || _wvFormatsToggles.Any(item => item.Checked)) ||
				buttonXRefreshQV.Checked || buttonXDeleteQV.Checked;

		public event EventHandler<EventArgs> SelectionChanged;

		public ResetPreviewContentControl()
		{
			InitializeComponent();

			Text = MainController.Instance.Settings.EnableLocalSync ? "QV-WV" : "WV";

			_wvFormatsToggles.AddRange(new[]
			{
				checkEditPowerPoint,
				checkEditWord,
				checkEditExcel,
				checkEditPdf,
				checkEditUrl,
				checkEditHtml5,
				checkEditQuicksite,
				checkEditMp4,
				checkEditMov,
				checkEditWmv,
				checkEditM4v,
				checkEditYoutube,
				checkEditVimeo,
				checkEditImages
			});

			layoutControlItemRefreshQV.Visibility =
				layoutControlItemDeleteQV.Visibility =
					MainController.Instance.Settings.EnableLocalSync ? LayoutVisibility.Always : LayoutVisibility.Never;

			layoutControlItemRefreshWV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshWV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshWV.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshWV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteWV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDeleteWV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteWV.MinSize = RectangleHelper.ScaleSize(layoutControlItemDeleteWV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshQV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshQV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshQV.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshQV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteQV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDeleteQV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDeleteQV.MinSize = RectangleHelper.ScaleSize(layoutControlItemDeleteQV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			layoutControlGroupWVSettings.Enabled = false;
		}

		public void ResetContent(Library library)
		{
			var actionNames = new List<string>();
			if (buttonXRefreshWV.Checked)
				actionNames.Add("Refresh WV");
			if (buttonXDeleteWV.Checked)
				actionNames.Add("Delete WV");
			if (buttonXRefreshQV.Checked)
				actionNames.Add("Refresh QV");
			if (buttonXDeleteQV.Checked)
				actionNames.Add("Delete QV");

			using (var confirmationForm = new FormResetPreviewContentConfirmation(actionNames))
			{
				if (confirmationForm.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
			}

			if (buttonXRefreshWV.Checked)
			{
				var previewContainers = new List<BasePreviewContainer>();
				if (checkEditAllFiles.Checked || _wvFormatsToggles.All(item => item.Checked))
					previewContainers.AddRange(library.PreviewContainers);
				else
				{
					if (checkEditPowerPoint.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<PowerPointPreviewContainer>());
					if (checkEditWord.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<WordPreviewContainer>());
					if (checkEditPdf.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<PdfPreviewContainer>());
					if (checkEditExcel.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<ExcelPreviewContainer>());
					if (checkEditUrl.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<WebLinkPreviewContainer>().Where(container => !container.IsQuickSite));
					if (checkEditHtml5.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<Html5PreviewContainer>());
					if (checkEditQuicksite.Checked)
					{
						previewContainers.AddRange(
							library.PreviewContainers.OfType<WebLinkPreviewContainer>().Where(container => container.IsQuickSite));
						previewContainers.AddRange(library.PreviewContainers.OfType<QuickSitePreviewContainer>());
					}
					if (checkEditMp4.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Mp4));
					if (checkEditMov.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Mov));
					if (checkEditWmv.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Wmv));
					if (checkEditM4v.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.M4v));
					if (checkEditYoutube.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<YoutubePreviewContainer>());
					if (checkEditVimeo.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VimeoPreviewContainer>());
					if (checkEditImages.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<ImagePreviewContainer>());
				}
				MainController.Instance.ProcessManager.Run("Updating WV files...", (cancelationToken, formProgess) =>
				{
					foreach (var previewContainer in previewContainers)
					{
						if (cancelationToken.IsCancellationRequested) break;
						previewContainer.ClearContent();
						var previewGenerator = previewContainer.GetPreviewGenerator();
						previewContainer.UpdateContent(previewGenerator, cancelationToken);
					}
				});
			}
			else if (buttonXDeleteWV.Checked)
			{
				if (checkEditAllFiles.Checked || _wvFormatsToggles.All(item => item.Checked))
				{
					var wvFolderPath = Path.Combine(library.Path, Constants.WebPreviewContainersRootFolderName);
					if (!Directory.Exists(wvFolderPath)) return;
					MainController.Instance.ProcessManager.Run("Deleting WV Files...", (cancelationToken, formProgess) =>
					{
						Utils.DeleteFolder(wvFolderPath);
						try
						{
							Directory.Delete(wvFolderPath);
						}
						catch { }
					});
				}
				else
				{
					var previewContainers = new List<BasePreviewContainer>();
					if (checkEditPowerPoint.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<PowerPointPreviewContainer>());
					if (checkEditWord.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<WordPreviewContainer>());
					if (checkEditPdf.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<PdfPreviewContainer>());
					if (checkEditExcel.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<ExcelPreviewContainer>());
					if (checkEditExcel.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<ExcelPreviewContainer>());
					if (checkEditUrl.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<WebLinkPreviewContainer>().Where(container => !container.IsQuickSite));
					if (checkEditHtml5.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<Html5PreviewContainer>());
					if (checkEditQuicksite.Checked)
					{
						previewContainers.AddRange(
							library.PreviewContainers.OfType<WebLinkPreviewContainer>().Where(container => container.IsQuickSite));
						previewContainers.AddRange(library.PreviewContainers.OfType<QuickSitePreviewContainer>());
					}
					if (checkEditMp4.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Mp4));
					if (checkEditMov.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Mov));
					if (checkEditWmv.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.Wmv));
					if (checkEditM4v.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VideoPreviewContainer>().Where(container => container.SourceSubType == FileTypes.M4v));
					if (checkEditYoutube.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<YoutubePreviewContainer>());
					if (checkEditVimeo.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<VimeoPreviewContainer>());
					if (checkEditImages.Checked)
						previewContainers.AddRange(library.PreviewContainers.OfType<ImagePreviewContainer>());
					MainController.Instance.ProcessManager.Run("Deleting WV files...", (cancelationToken, formProgess) =>
					{
						foreach (var previewContainer in previewContainers)
						{
							if (cancelationToken.IsCancellationRequested) break;
							previewContainer.ClearContent();
						}
					});
				}
			}
			if (buttonXRefreshQV.Checked)
			{
				MainController.Instance.ProcessManager.Run("Updating QV files...", (cancelationToken, formProgess) =>
				{
					var powerPointFiles = library.Pages.SelectMany(p => p.AllGroupLinks).OfType<PowerPointLink>().ToList();
					if (!powerPointFiles.Any()) return;
					using (var powerPointProcessor = new PowerPointHidden())
					{
						if (!powerPointProcessor.Connect(true)) return;
						foreach (var powerPointLink in powerPointFiles)
						{
							((PowerPointLinkSettings)powerPointLink.Settings).UpdateQuickViewContent(powerPointProcessor);
						}
					}
				});
			}
			else if (buttonXDeleteQV.Checked)
			{
				var qvFolderPath = Path.Combine(library.Path, Constants.RegularPreviewContainersRootFolderName);
				if (!Directory.Exists(qvFolderPath)) return;
				MainController.Instance.ProcessManager.Run("Deleting QV Files...", (cancelationToken, formProgess) =>
				{
					Utils.DeleteFolder(qvFolderPath);
					try
					{
						Directory.Delete(qvFolderPath);
					}
					catch { }
				});
			}
		}

		private void OnWVActionClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			var isButtonChecked = button.Checked;
			buttonXRefreshWV.Checked = false;
			buttonXDeleteWV.Checked = false;
			if (!isButtonChecked)
				button.Checked = true;
		}

		private void OnWVActionChanged(object sender, EventArgs e)
		{
			layoutControlGroupWVSettings.Enabled = buttonXRefreshWV.Checked || buttonXDeleteWV.Checked;
			SelectionChanged?.Invoke(sender, e);
		}
		private void OnQVActionClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			var isButtonChecked = button.Checked;
			buttonXRefreshQV.Checked = false;
			buttonXDeleteQV.Checked = false;
			if (!isButtonChecked)
				button.Checked = true;
		}

		private void OnWVAllFilesCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupWVFileFormats.Enabled = !checkEditAllFiles.Checked;
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
