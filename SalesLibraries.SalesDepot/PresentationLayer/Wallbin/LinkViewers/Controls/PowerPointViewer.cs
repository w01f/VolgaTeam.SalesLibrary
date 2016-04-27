using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(PowerPointLink))]
	[ToolboxItem(false)]
	public partial class PowerPointViewer : UserControl, ILinkViewer
	{
		private readonly FileInfo _tempCopy;
		private readonly PresentationPreviewContainer _previewData;

		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName
		{
			get { return Link.Name; }
		}

		public PowerPointLink PowerPointLink { get { return (PowerPointLink)Link; } }

		private PresentationSlideThumbnail SelectedThumbnail
		{
			get { return (PresentationSlideThumbnail)comboBoxEditSlides.EditValue; }
		}
		#endregion

		public PowerPointViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;
			if (System.IO.File.Exists(Link.FullPath))
			{
				string tempPath = Path.Combine(RemoteResourceManager.Instance.TempFolder.LocalPath, DateTime.Now.ToString("yyyyMMdd-hmmsstt") + Path.GetExtension(Link.FullPath));
				System.IO.File.Copy(Link.FullPath, tempPath, true);
				_tempCopy = new FileInfo(tempPath);
			}

			pictureBoxPreview.Image = null;
			laSlideNumber.Text = string.Empty;

			_previewData = new PresentationPreviewContainer(PowerPointLink);
			_previewData.GetPreviewImages();

			laFileInfo.Text = "File Added: " + Link.AddDate.ToString("MM/dd/yy");
			comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
			comboBoxEditSlides.Properties.Items.Clear();
			comboBoxEditSlides.Properties.Items.AddRange(_previewData.Thumbnails);
			if (comboBoxEditSlides.Properties.Items.Count > 0)
				comboBoxEditSlides.SelectedIndex = 0;
			comboBoxEditSlides_SelectedIndexChanged(null, null);
			comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			pictureBoxPreview.Image = null;
			_previewData.ReleaseThumbnails();
		}

		public void Open()
		{
			FloaterManager.Instance.ShowFloater(
				MainController.Instance.MainForm,
				MainController.Instance.Settings.SalesDepotName,
				MainController.Instance.MainForm.FloaterLogo,
				() => LinkManager.OpenCopyOfFile(PowerPointLink));
		}

		public void Save()
		{
			LinkManager.SaveLink("Save copy of the presentation as", PowerPointLink);
		}

		public void Email()
		{
			if (!MainController.Instance.CheckPowerPointRunning(
				() => MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				) return;
			PowerPointSingleton.Instance.OpenSlideSourcePresentation(_tempCopy);
			using (var form = new FormEmailPresentation())
			{
				form.PowerPointLink = PowerPointLink;
				form.ActiveSlide = SelectedThumbnail.Index;
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}

		public void Print()
		{
			using (var powerPointProcessor = new PowerPointHidden())
			{
				if (!powerPointProcessor.Connect(true)) return;
				powerPointProcessor.PrintPresentation(
					_tempCopy.FullName,
					SelectedThumbnail.Index,
					printAction => MainController.Instance.ProcessManager.Run(
						"Printing...",
						cancellationToken => printAction()));
			}
		}
		#endregion

		#region PowerPoint Methods
		public void InsertSlide()
		{
			if (!MainController.Instance.CheckPowerPointRunning(
				() => MainController.Instance.PopupMessages.ShowWarningQuestion("PowerPoint is not Running. Do you want to open it now?") == DialogResult.Yes)
				) return;
			if (PowerPointSingleton.Instance.GetActiveSlideIndex() != -1)
			{
				MainController.Instance.ActivateApplication();
				var activeSlideSettings = PowerPointSingleton.Instance.GetSlideSettings();
				if (activeSlideSettings.SlideSize.Orientation.ToString() != _previewData.Settings.Orientation)
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
						return;
				FloaterManager.Instance.ShowFloater(
					MainController.Instance.MainForm,
					MainController.Instance.Settings.SalesDepotName,
					MainController.Instance.MainForm.FloaterLogo,
					() => MainController.Instance.ProcessManager.Run(
						"Inserting selected slide...",
						cancellationToken =>
						{
							PowerPointManager.Instance.ActivatePowerPoint();
							PowerPointSingleton.Instance.OpenSlideSourcePresentation(_tempCopy);
							PowerPointSingleton.Instance.AppendSlide(SelectedThumbnail.Index);

						})
					);
			}
			else
			{
				using (var warningForm = new FormSelectSlideWarning())
					warningForm.ShowDialog(MainController.Instance.MainForm);
			}
		}

		public void SaveAsPDF()
		{
			if (!MainController.Instance.CheckPowerPointRunning(
				() => MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				) return;
			using (var form = new FormSaveAsPDF())
			{
				var result = form.ShowDialog(MainController.Instance.MainForm);
				var wholeFile = form.WholeFile;

				if (result == DialogResult.Cancel) return;
				var destinationFileName = Path.Combine(Path.GetTempPath(), PowerPointLink.NameWithoutExtension + ".pdf");

				MainController.Instance.ProcessManager.Run(
					"Saving as PDF...",
					cancellationToken =>
					{
						PowerPointSingleton.Instance.OpenSlideSourcePresentation(_tempCopy);
						PowerPointSingleton.Instance.ExportSlideAsPdf(wholeFile ? -1 : SelectedThumbnail.Index, destinationFileName);

					});

				LinkManager.SaveFile("Save PDF as", new FileInfo(destinationFileName));
			}
		}

		public void OpenInQuickView()
		{
			if (MainController.Instance.Settings.LinkLaunchSettings.OldStyleQuickView)
				LinkManager.ViewPresentationOld(PowerPointLink);
			else
				LinkManager.ViewPresentation(PowerPointLink);
		}
		#endregion

		#region GUI Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxPreview.Image = SelectedThumbnail.SlideImage;
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

		private void pnNavigationArea_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}

		private void pictureBoxPreview_DoubleClick(object sender, EventArgs e)
		{
			OpenInQuickView();
		}
		#endregion
	}
}