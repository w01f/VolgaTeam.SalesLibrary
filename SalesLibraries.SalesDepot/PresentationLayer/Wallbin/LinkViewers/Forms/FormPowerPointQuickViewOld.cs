using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Core;
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
	public partial class FormPowerPointQuickViewOld : MetroForm
	{
		private FileInfo _originalFile;
		private double _scaleK;
		private FileInfo _viewedFile;
		private PresentationPreviewContainer _previewData;

		public FormPowerPointQuickViewOld()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				checkEditChangeSlideTemplate.Font = new Font(checkEditChangeSlideTemplate.Font.FontFamily, checkEditChangeSlideTemplate.Font.Size - 1, checkEditChangeSlideTemplate.Font.Style);
				checkEditKeepSlideTemplate.Font = new Font(checkEditKeepSlideTemplate.Font.FontFamily, checkEditKeepSlideTemplate.Font.Size - 1, checkEditKeepSlideTemplate.Font.Style);
				labelControlSlideTemplate.Font = new Font(labelControlSlideTemplate.Font.FontFamily, labelControlSlideTemplate.Font.Size - 2, labelControlSlideTemplate.Font.Style);
			}
		}

		public PowerPointLink PowerPointLink { get; set; }

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
			if (PowerPointLink != null)
			{
				_originalFile = new FileInfo(PowerPointLink.FullPath);
				_viewedFile = _originalFile.CopyTo(Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, DateTime.Now.ToString("yyyyMMdd-hmmsstt.PPT")), true);
				Text = "QuickView - " + PowerPointLink.NameWithExtension;
				laFileInfo.Text = "File Added: " + PowerPointLink.AddDate.ToString("MM/dd/yy");
				MainController.Instance.MainForm.TopMost = true;
				PowerPointSingleton.Instance.SetVisibility(true);
				_scaleK = IsLargeFont() ? 1.67 : 1.35;

				_previewData = new PresentationPreviewContainer(PowerPointLink);

				var themeManager = new ThemeManager();
				MainController.Instance.ProcessManager.Run("Loading Themes...", cancellationToken => themeManager.Load());
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

				var containerHandle = pnPreview.Handle;
				var containerHeight = pnPreview.Height;
				var containerWidth = pnPreview.Width;

				MainController.Instance.ProcessManager.Run("Loading the presentation...", cancellationToken =>
				{
					PowerPointSingleton.Instance.OpenSlideSourcePresentation(_viewedFile);
					PowerPointSingleton.Instance.ViewSlideShow();
					PowerPointSingleton.Instance.ResizeSlideShow(
						containerHandle,
						(int)(containerHeight / _scaleK),
						(int)(containerWidth / _scaleK));
				});

				if (PowerPointSingleton.Instance.SlideSourcePresentation != null)
				{
					comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
					comboBoxEditSlides.Properties.Items.Clear();
					for (var i = 1; i <= PowerPointSingleton.Instance.SlideSourcePresentation.Slides.Count; i++)
						comboBoxEditSlides.Properties.Items.Add(i.ToString());
					if (comboBoxEditSlides.Properties.Items.Count > 0)
						comboBoxEditSlides.SelectedIndex = 0;
					barLargeButtonItemAddAllSlides.Enabled = comboBoxEditSlides.Properties.Items.Count > 1;
					comboBoxEditSlides_SelectedIndexChanged(null, null);
					comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
					barLargeButtonItemEmail.Enabled = (MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayQuickView) == EmailButtonsDisplayOptionsEnum.DisplayQuickView;
				}
				MainController.Instance.MainForm.TopMost = false;
				TopMost = true;
				TopMost = false;
			}
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = false;
		}

		private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainController.Instance.ProcessManager.Run("Closing the presentation...", cancellationToken => PowerPointSingleton.Instance.ExitSlideShow());
			try
			{
				_viewedFile.Delete();
			}
			catch { }
		}

		private void FormQuickView_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
			MainController.Instance.MainForm.TopMost = true;

			var containerHandle = pnPreview.Handle;
			var containerHeight = pnPreview.Height;
			var containerWidth = pnPreview.Width;

			MainController.Instance.ProcessManager.Run("Resizing the presentation...",
				cancellationToken =>
					PowerPointSingleton.Instance.ResizeSlideShow(
						containerHandle,
						(int)(containerHeight / _scaleK),
						(int)(containerWidth / _scaleK)));

			MainController.Instance.MainForm.TopMost = false;
			TopMost = true;
			TopMost = false;
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
			using (var form = new FormSaveAsPDF())
			{
				var result = form.ShowDialog(this);
				var wholeFile = form.WholeFile;

				if (result == DialogResult.Cancel) return;
				var destinationFileName = Path.Combine(Path.GetTempPath(), PowerPointLink.NameWithoutExtension + ".pdf");

				MainController.Instance.ProcessManager.Run(
					"Saving as PDF...",
					cancellationToken =>
						PowerPointSingleton.Instance.ExportSlideAsPdf(
						wholeFile ? -1 : (comboBoxEditSlides.SelectedIndex + 1),
						destinationFileName));

				LinkManager.SaveFile("Save PDF as", new FileInfo(destinationFileName));
			}
		}

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (PowerPointLink == null) return;
			using (var form = new FormEmailPresentation())
			{
				form.PowerPointLink = PowerPointLink;
				form.ActiveSlide = comboBoxEditSlides.SelectedIndex + 1;
				form.ShowDialog(this);
			}
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			using (var powerPointProcessor = new PowerPointHidden())
			{
				if (!powerPointProcessor.Connect(true)) return;
				powerPointProcessor.PrintPresentation(
					_viewedFile.FullName,
					comboBoxEditSlides.SelectedIndex + 1,
					printAction => MainController.Instance.ProcessManager.Run(
						"Printing...",
						cancellationToken => printAction()));
				
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
			PowerPointSingleton.Instance.SlideShowWindow.View.GotoSlide(comboBoxEditSlides.SelectedIndex + 1, MsoTriState.msoFalse);
			laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (comboBoxEditSlides.SelectedIndex + 1).ToString(), comboBoxEditSlides.Properties.Items.Count.ToString() });
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (comboBoxEditSlides.Properties.Items.Count <= 0) return;
			switch (e.Button.Index)
			{
				case 1:
					if (comboBoxEditSlides.SelectedIndex < comboBoxEditSlides.Properties.Items.Count - 1)
						comboBoxEditSlides.SelectedIndex++;
					else
						comboBoxEditSlides.SelectedIndex = 0;
					break;
				case 2:
					if (comboBoxEditSlides.SelectedIndex > 0)
						comboBoxEditSlides.SelectedIndex--;
					else
						comboBoxEditSlides.SelectedIndex = comboBoxEditSlides.Properties.Items.Count - 1;
					break;
			}
		}

		private void checkEditSlideTemplate_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditSlideTemplate.Enabled = checkEditChangeSlideTemplate.Checked;
		}
		#endregion

		#region Common Methods
		public void InsertSlide(bool allSlides = false)
		{
			if (PowerPointLink == null) return;
			if (PowerPointSingleton.Instance.GetActiveSlideIndex() != -1)
			{
				PowerPointManager.Instance.ActivatePowerPoint();
				MainController.Instance.ActivateApplication();
				var activeSlideSettings = PowerPointSingleton.Instance.GetSlideSettings();
				if (activeSlideSettings.Orientation.ToString() != _previewData.Settings.Orientation)
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
						return;

				var selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
				var selectedTheme = comboBoxEditSlideTemplate.EditValue as Theme;
				var templatePath = checkEditChangeSlideTemplate.Checked && selectedTheme != null ? selectedTheme.GetThemePath() : String.Empty;
				FloaterManager.Instance.ShowFloater(
					MainController.Instance.MainForm,
					MainController.Instance.Settings.SalesDepotName,
					MainController.Instance.MainForm.FloaterLogo,
					() => MainController.Instance.ProcessManager.Run(
						"Inserting selected slide...",
						cancellationToken =>
						{
							PowerPointManager.Instance.ActivatePowerPoint();
							PowerPointSingleton.Instance.AppendSlide(
								allSlides ? -1 : selectedIndex,
								templatePath);
						})
					);
			}
			else
			{
				using (var warningForm = new FormSelectSlideWarning())
					warningForm.ShowDialog(this);
			}
		}

		private bool IsLargeFont()
		{
			Graphics g = CreateGraphics();
			return g.DpiX > 96;
		}
		#endregion
	}
}