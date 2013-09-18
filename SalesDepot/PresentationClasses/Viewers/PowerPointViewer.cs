using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Core;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.Floater;
using SalesDepot.InteropClasses;
using SalesDepot.ToolForms;
using SalesDepot.ToolForms.WallBin;

namespace SalesDepot.PresentationClasses.Viewers
{
	[ToolboxItem(false)]
	public partial class PowerPointViewer : UserControl, IFileViewer
	{
		private readonly FileInfo _tempCopy;

		#region Properties
		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return File.DisplayName; }
		}

		public string CriteriaOverlap
		{
			get { return File.CriteriaOverlap; }
		}

		public Image Widget
		{
			get { return File.Widget; }
		}
		#endregion

		public PowerPointViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			if (System.IO.File.Exists(File.LocalPath))
			{
				string tempPath = Path.Combine(AppManager.Instance.TempFolder.FullName, DateTime.Now.ToString("yyyyMMdd-hmmsstt") + Path.GetExtension(File.LocalPath));
				System.IO.File.Copy(File.LocalPath, tempPath, true);
				_tempCopy = new FileInfo(tempPath);
			}

			pictureBoxPreview.Image = null;
			laSlideNumber.Text = string.Empty;

			if (File.PreviewContainer != null)
			{
				laFileInfo.Text = "File Added: " + File.AddDate.ToString("MM/dd/yy");
				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				comboBoxEditSlides.Properties.Items.AddRange(File.PreviewContainer.Slides.Select(x => x.Index + 1).ToArray());
				if (File.PreviewContainer.Slides.Count > 0)
					comboBoxEditSlides.SelectedIndex = 0;
				comboBoxEditSlides_SelectedIndexChanged(null, null);
				comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
			}
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			pictureBoxPreview.Image = null;
			if (File.PreviewContainer != null)
				File.PreviewContainer.ReleasePreviewImages();
		}

		public void Open()
		{
			LinkManager.Instance.OpenCopyOfFile(File);
		}

		public void Save()
		{
			LinkManager.Instance.SaveFile("Save copy of the presentation as", File);
		}

		public void Email()
		{
			PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
			using (var form = new FormEmailPresentation())
			{
				form.SelectedFile = File;
				form.ActiveSlide = File.PreviewContainer.SelectedIndex + 1;
				form.ShowDialog();
			}
		}

		public void Print()
		{
			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Print Link", File.Name, File.Type.ToString(), File.OriginalPath, File.Parent.Parent.Parent.Name, File.Parent.Parent.Name);
			PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
			PowerPointHelper.Instance.PrintPresentation(File.PreviewContainer.SelectedIndex + 1);
		}

		public void EmailLinkToQuickSite()
		{
			LinkManager.Instance.EmailLinkToQuickSite(File);
		}

		public void AddLinkToQuickSite()
		{
			LinkManager.Instance.AddLinkToQuickSite(File);
		}
		#endregion

		#region PowerPoint Methods
		public void InsertSlide()
		{
			if (PowerPointHelper.Instance.GetActiveSlideIndex() != -1)
			{
				if (File.PresentationProperties != null)
				{
					AppManager.Instance.ActivateMainForm();
					if ((PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == MsoOrientation.msoOrientationHorizontal && File.PresentationProperties.Orientation.Equals("Portrait")) ||
						(PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == MsoOrientation.msoOrientationVertical && File.PresentationProperties.Orientation.Equals("Landscape")))
						if (AppManager.Instance.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
							return;
				}
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Inserting selected slide...";
					FloaterManager.Instance.ShowFloater(FormMain.Instance, () =>
					{
						form.TopMost = true;
						var thread = new Thread(delegate()
						{
							AppManager.Instance.ActivatePowerPoint();
							AppManager.Instance.ActivateMiniBar();
							AppManager.Instance.ActivityManager.AddLinkAccessActivity("Insert Slide", File.Name, File.Type.ToString(), File.OriginalPath, File.Parent.Parent.Parent.Name, File.Parent.Parent.Name);
							PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
							PowerPointHelper.Instance.AppendSlide(File.PreviewContainer.SelectedIndex + 1);
						});
						thread.Start();
						form.Show();
						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					});
				}
			}
			else
			{
				using (var warningForm = new FormSelectSlideWarning())
					warningForm.ShowDialog();
			}
		}

		public void SaveAsPDF()
		{
			using (var form = new FormSaveAsPDF())
			{
				DialogResult result = form.ShowDialog();
				bool wholeFile = form.WholeFile;

				if (result != DialogResult.Cancel)
				{
					string destinationFileName = Path.Combine(Path.GetTempPath(), File.NameWithoutExtesion + ".pdf");

					using (var progressForm = new FormProgress())
					{
						progressForm.laProgress.Text = "Saving as PDF...";
						progressForm.TopMost = true;
						var thread = new Thread(delegate()
													{
														PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
														PowerPointHelper.Instance.ExportPresentationAsPDF(wholeFile ? -1 : (File.PreviewContainer.SelectedIndex + 1), destinationFileName);
													});
						thread.Start();
						progressForm.Show();

						while (thread.IsAlive)
							Application.DoEvents();

						progressForm.Close();

						AppManager.Instance.ActivityManager.AddLinkAccessActivity("Save Link as PDF", File.Name, File.Type.ToString(), File.OriginalPath, File.Parent.Parent.Parent.Name, File.Parent.Parent.Name);
						LinkManager.Instance.SaveFile("Save PDF as", new FileInfo(destinationFileName), false);
					}
				}
			}
		}

		public void OpenInQuickView()
		{
			if (SettingsManager.Instance.OldStyleQuickView)
				LinkManager.Instance.ViewPresentationOld(File);
			else
				LinkManager.Instance.ViewPresentation(File);
		}
		#endregion

		#region GUI Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (File != null)
			{
				if (File.PreviewContainer != null)
				{
					File.PreviewContainer.SelectedIndex = comboBoxEditSlides.SelectedIndex;
					pictureBoxPreview.Image = File.PreviewContainer.SelectedSlide;
					laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (File.PreviewContainer.SelectedIndex + 1).ToString(), File.PreviewContainer.Slides.Count.ToString() });
				}
			}
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (File != null)
			{
				if (File.PreviewContainer != null)
				{
					if (e.Button.Index == 1)
					{
						File.PreviewContainer.SelectedIndex++;
						if (File.PreviewContainer.SelectedIndex >= File.PreviewContainer.Slides.Count)
							File.PreviewContainer.SelectedIndex = 0;
						comboBoxEditSlides.SelectedIndex = File.PreviewContainer.SelectedIndex;
					}
					else if (e.Button.Index == 2)
					{
						File.PreviewContainer.SelectedIndex--;
						if (File.PreviewContainer.SelectedIndex < 0)
							File.PreviewContainer.SelectedIndex = File.PreviewContainer.Slides.Count - 1;
						comboBoxEditSlides.SelectedIndex = File.PreviewContainer.SelectedIndex;
					}
				}
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