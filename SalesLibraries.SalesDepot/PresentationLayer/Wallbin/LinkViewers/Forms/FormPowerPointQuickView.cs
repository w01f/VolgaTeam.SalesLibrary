using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Business.Services;
using SalesLibraries.SalesDepot.Business.Themes;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Settings;
using RemoteResourceManager = SalesLibraries.Common.Helpers.RemoteResourceManager;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms
{
	public partial class FormPowerPointQuickView : MetroForm
	{
		private FileInfo _tempFileCopy;
		private PresentationPreviewContainer _previewData;
		public PowerPointLink PowerPointLink { get; set; }
		public Action AfterClose { get; private set; }

		private PresentationSlideThumbnail SelectedThumbnail => (PresentationSlideThumbnail)comboBoxEditSlides.EditValue;

		public FormPowerPointQuickView()
		{
			InitializeComponent();
			if (!((CreateGraphics()).DpiX > 96)) return;
			checkEditChangeSlideTemplate.Font = new Font(checkEditChangeSlideTemplate.Font.FontFamily, checkEditChangeSlideTemplate.Font.Size - 1, checkEditChangeSlideTemplate.Font.Style);
			checkEditKeepSlideTemplate.Font = new Font(checkEditKeepSlideTemplate.Font.FontFamily, checkEditKeepSlideTemplate.Font.Size - 1, checkEditKeepSlideTemplate.Font.Style);
			labelControlSlideTemplate.Font = new Font(labelControlSlideTemplate.Font.FontFamily, labelControlSlideTemplate.Font.Size - 2, labelControlSlideTemplate.Font.Style);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Environment.OSVersion.Version.Major < 6) return;
			int attrValue = 1;
			int res = WinAPIHelper.DwmSetWindowAttribute(Handle, WinAPIHelper.DWMWA_TRANSITIONS_FORCEDISABLED, ref attrValue, sizeof(int));
			if (res < 0)
				throw new Exception("Can't disable aero animation");
		}

		#region Form GUI Event Habdlers
		private void FormQuickView_Shown(object sender, EventArgs e)
		{
			AfterClose = null;
			if (PowerPointLink != null)
			{
				if (File.Exists(PowerPointLink.FullPath))
				{
					var tempPath = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, DateTime.Now.ToString("yyyyMMdd-hmmsstt") + Path.GetExtension(PowerPointLink.FullPath));
					File.Copy(PowerPointLink.FullPath, tempPath, true);
					_tempFileCopy = new FileInfo(tempPath);
				}


				Text = "QuickView - " + PowerPointLink.NameWithExtension;
				laFileInfo.Text = "File Added: " + PowerPointLink.AddDate.ToString("MM/dd/yy");

				var themeManager = new ThemeManager();
				MainController.Instance.ProcessManager.Run("Loading Themes...", (cancelletionToken, formProgress) => themeManager.Load());
				var availableThemes = themeManager.GetThemes(SlideType.SalesDepot).ToList();
				if (availableThemes.Any())
				{
					pnSlideTemplate.Visible = true;
					comboBoxEditSlideTemplate.Properties.Items.Clear();
					comboBoxEditSlideTemplate.Properties.Items.AddRange(availableThemes);
					comboBoxEditSlideTemplate.SelectedIndex = 0;
				}
				else
					pnSlideTemplate.Visible = false;

				_previewData = new PresentationPreviewContainer(PowerPointLink);
				MainController.Instance.ProcessManager.Run("Loading Presentation...", (cancelletionToken, formProgress) => _previewData.GetPreviewImages());

				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				comboBoxEditSlides.Properties.Items.AddRange(_previewData.Thumbnails);
				if (comboBoxEditSlides.Properties.Items.Count > 0)
					comboBoxEditSlides.SelectedIndex = 0;
				comboBoxEditSlides_SelectedIndexChanged(null, null);
				comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;

				barLargeButtonItemEmail.Enabled = (MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayQuickView) == EmailButtonsDisplayOptionsEnum.DisplayQuickView;
			}
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = false;
		}

		private void FormPowerPointQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			pictureBoxPreview.Image = null;
			_previewData.ReleaseThumbnails();
		}

		private void FormQuickView_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			FloaterManager.Instance.ShowFloater(
				MainController.Instance.MainForm,
				MainController.Instance.Settings.SalesDepotName,
				MainController.Instance.MainForm.FloaterLogo,
				() => LinkManager.OpenCopyOfFile(PowerPointLink));
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			LinkManager.SaveLink("Save copy of the presentation as", PowerPointLink);
		}

		private void barButtonItemSaveAsPDF_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!CheckPowerPointRunning()) return;
			using (var form = new FormSaveAsPDF())
			{
				var result = form.ShowDialog(this);
				var wholeFile = form.WholeFile;

				if (result == DialogResult.Cancel) return;
				var destinationFileName = Path.Combine(Path.GetTempPath(), PowerPointLink.NameWithoutExtension + ".pdf");

				MainController.Instance.ProcessManager.Run(
					"Saving as PDF...",
					(cancelletionToken, formProgress) =>
					{
						PowerPointSingleton.Instance.OpenSlideSourcePresentation(_tempFileCopy);
						PowerPointSingleton.Instance.ExportSlideAsPdf(wholeFile ? -1 : SelectedThumbnail.Index, destinationFileName);

					});

				LinkManager.SaveFile("Save PDF as", new FileInfo(destinationFileName));
			}
		}

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!CheckPowerPointRunning()) return;
			PowerPointSingleton.Instance.OpenSlideSourcePresentation(_tempFileCopy);
			using (var form = new FormEmailPresentation())
			{
				form.PowerPointLink = PowerPointLink;
				form.ActiveSlide = SelectedThumbnail.Index;
				form.ShowDialog(this);
			}
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			using (var powerPointProcessor = new PowerPointHidden())
			{
				if (!powerPointProcessor.Connect(true)) return;
				powerPointProcessor.PrintPresentation(
					_tempFileCopy.FullName,
					SelectedThumbnail.Index,
					printAction => MainController.Instance.ProcessManager.Run(
						"Printing...",
						(cancelletionToken, formProgress) => printAction()));
			}
		}

		private void barLargeButtonItemAddAllSlides_ItemClick(object sender, ItemClickEventArgs e)
		{
			InsertSlide(true);
		}

		private void barLargeButtonItemAddSlide_ItemClick(object sender, ItemClickEventArgs e)
		{
			InsertSlide();
		}

		private void simpleButtonSettings_Click(object sender, EventArgs e)
		{
			using (var form = new FormFileSettings())
			{
				form.ShowDialog(this);
			}
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("qv");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		#region Other Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxPreview.Image = SelectedThumbnail.SlideImage;
			pictureBoxPreview.BackColor = Color.WhiteSmoke;
			laSlideNumber.Text = string.Format("Slide {0} of {1}", SelectedThumbnail.Index, _previewData.Thumbnails.Count);
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 1:
					if ((comboBoxEditSlides.SelectedIndex + 1) >= comboBoxEditSlides.Properties.Items.Count)
						comboBoxEditSlides.SelectedIndex = 0;
					else
						comboBoxEditSlides.SelectedIndex++;
					break;
				case 2:
					if (comboBoxEditSlides.SelectedIndex == 0)
						comboBoxEditSlides.SelectedIndex = comboBoxEditSlides.Properties.Items.Count - 1;
					else
						comboBoxEditSlides.SelectedIndex--;
					break;
			}
		}

		private void checkEditSlideTemplate_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditSlideTemplate.Enabled = checkEditChangeSlideTemplate.Checked;
		}
		#endregion

		#region Common Methods

		private bool CheckPowerPointRunning()
		{
			if (PowerPointSingleton.Instance.Connect()) return true;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) != DialogResult.Yes) return false;
			AfterClose = () => MainController.Instance.CheckPowerPointRunning();
			Close();
			return false;
		}

		private void InsertSlide(bool allSlides = false)
		{
			if (!CheckPowerPointRunning()) return;
			MainController.Instance.ActivateApplication();
			var activeSlideSettings = PowerPointSingleton.Instance.GetSlideSettings();
			if (activeSlideSettings.SlideSize.Orientation.ToString() != _previewData.Settings.Orientation)
				if (MainController.Instance.PopupMessages.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
					return;

			var selectedTheme = comboBoxEditSlideTemplate.EditValue as Theme;
			var templatePath = checkEditChangeSlideTemplate.Checked && selectedTheme != null ? selectedTheme.GetThemePath() : String.Empty;
			FloaterManager.Instance.ShowFloater(
				MainController.Instance.MainForm,
				MainController.Instance.Settings.SalesDepotName,
				MainController.Instance.MainForm.FloaterLogo,
				() => MainController.Instance.ProcessManager.Run(
					"Inserting selected slide...",
					(cancelletionToken, formProgress) =>
					{
						PowerPointManager.Instance.ActivatePowerPoint();
						PowerPointSingleton.Instance.OpenSlideSourcePresentation(_tempFileCopy);
						PowerPointSingleton.Instance.AppendSlide(
						allSlides ? -1 : SelectedThumbnail.Index,
						templatePath);

					})
				);
		}
		#endregion
	}
}