using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Core;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.InteropClasses;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormPowerPointQuickView : Form
	{
		private FileInfo _tempCopy;

		public FormPowerPointQuickView()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				checkEditChangeSlideTemplate.Font = new Font(checkEditChangeSlideTemplate.Font.FontFamily, checkEditChangeSlideTemplate.Font.Size - 1, checkEditChangeSlideTemplate.Font.Style);
				checkEditKeepSlideTemplate.Font = new Font(checkEditKeepSlideTemplate.Font.FontFamily, checkEditKeepSlideTemplate.Font.Size - 1, checkEditKeepSlideTemplate.Font.Style);
				labelControlSlideTemplate.Font = new Font(labelControlSlideTemplate.Font.FontFamily, labelControlSlideTemplate.Font.Size - 2, labelControlSlideTemplate.Font.Style);
			}
		}

		#region Form GUI Event Habdlers
		private void FormQuickView_Shown(object sender, EventArgs e)
		{
			if (SelectedFile != null)
			{
				if (File.Exists(SelectedFile.LocalPath))
				{
					string tempPath = Path.Combine(AppManager.Instance.TempFolder.FullName, DateTime.Now.ToString("yyyyMMdd-hmmsstt") + Path.GetExtension(SelectedFile.LocalPath));
					File.Copy(SelectedFile.LocalPath, tempPath, true);
					_tempCopy = new FileInfo(tempPath);
				}

				Text = "QuickView - " + SelectedFile.NameWithExtension;
				laFileInfo.Text = "Added: " + SelectedFile.AddDate.ToString("MM/dd/yy h:mm:ss tt") + Environment.NewLine + (SelectedFile.ExpirationDateOptions.EnableExpirationDate && SelectedFile.ExpirationDateOptions.ExpirationDate != DateTime.MinValue ? ("Expires: " + SelectedFile.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt")) : "No Expiration Date");
				if (SelectedFile.PresentationProperties != null)
					laSlideSize.Text = string.Format("{0} {1} x {2}", new object[] { SelectedFile.PresentationProperties.Orientation, SelectedFile.PresentationProperties.Width.ToString("#.##"), SelectedFile.PresentationProperties.Height.ToString("#.##") });
				if (SelectedFile.PresentationProperties.Width == 10 && SelectedFile.PresentationProperties.Height == 7.5 && SelectedFile.PresentationProperties.Orientation.Equals("Landscape") && MasterWizardManager.Instance.MasterWizards.Count > 1)
				{
					pnSlideTemplate.Visible = true;
					comboBoxEditSlideTemplate.Properties.Items.AddRange(MasterWizardManager.Instance.MasterWizards.Keys);
					comboBoxEditSlideTemplate.SelectedIndex = 0;
				}
				else
					pnSlideTemplate.Visible = false;

				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Presentation...";
					var thread = new Thread(delegate()
												{
													if (SelectedFile.PreviewContainer != null)
														SelectedFile.PreviewContainer.GetPreviewImages();
												});
					thread.Start();

					form.Show();

					while (thread.IsAlive)
						Application.DoEvents();

					form.Close();
				}

				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				comboBoxEditSlides.Properties.Items.AddRange(SelectedFile.PreviewContainer.Slides.Select(x => x.Index + 1).ToArray());
				if (SelectedFile.PreviewContainer.Slides.Count > 0)
					comboBoxEditSlides.SelectedIndex = 0;
				barLargeButtonItemAddAllSlides.Enabled = SelectedFile.PreviewContainer.Slides.Count > 1;
				comboBoxEditSlides_SelectedIndexChanged(null, null);
				comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;

				barLargeButtonItemPDF.Enabled = !PowerPointHelper.Instance.Is2003;
				barLargeButtonItemEmail.Enabled = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayQuickView) == EmailButtonsDisplayOptions.DisplayQuickView;
			}
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = false;
		}

		private void FormPowerPointQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			pictureBoxPreview.Image = null;
		}

		private void FormQuickView_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
			laFileInfo.Width = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
				LinkManager.Instance.OpenCopyOfFile(SelectedFile);
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Save Link", SelectedFile.Name, SelectedFile.Type.ToString(), SelectedFile.OriginalPath, SelectedFile.Parent.Parent.Parent.Name, SelectedFile.Parent.Parent.Name);
				LinkManager.Instance.SaveFile("Save copy of the presentation as", new FileInfo(SelectedFile.LocalPath));
			}
		}

		private void barButtonItemSaveAsPDF_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
			{
				using (var form = new FormSaveAsPDF())
				{
					DialogResult result = form.ShowDialog();
					bool wholeFile = form.WholeFile;

					if (result != DialogResult.Cancel)
					{
						string destinationFileName = Path.Combine(Path.GetTempPath(), SelectedFile.NameWithoutExtesion + ".pdf");

						using (var progressForm = new FormProgress())
						{
							progressForm.laProgress.Text = "Saving as PDF...";
							progressForm.TopMost = true;
							var thread = new Thread(delegate()
														{
															PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
															PowerPointHelper.Instance.ExportPresentationAsPDF(wholeFile ? -1 : (SelectedFile.PreviewContainer.SelectedIndex + 1), destinationFileName);
														});
							thread.Start();
							progressForm.Show();

							while (thread.IsAlive)
								Application.DoEvents();

							progressForm.Close();

							AppManager.Instance.ActivityManager.AddLinkAccessActivity("Save Link as PDF", SelectedFile.Name, SelectedFile.Type.ToString(), SelectedFile.OriginalPath, SelectedFile.Parent.Parent.Parent.Name, SelectedFile.Parent.Parent.Name);
							LinkManager.Instance.SaveFile("Save PDF as", new FileInfo(destinationFileName), false);
						}
					}
				}
			}
		}

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
			{
				PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
				using (var form = new FormEmailPresentation())
				{
					form.SelectedFile = SelectedFile;
					form.ActiveSlide = SelectedFile.PreviewContainer.SelectedIndex + 1;
					form.ShowDialog();
				}
			}
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
			{
				AppManager.Instance.ActivityManager.AddLinkAccessActivity("Print Link", SelectedFile.Name, SelectedFile.Type.ToString(), SelectedFile.OriginalPath, SelectedFile.Parent.Parent.Parent.Name, SelectedFile.Parent.Parent.Name);
				PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
				PowerPointHelper.Instance.PrintPresentation(SelectedFile.PreviewContainer.SelectedIndex + 1);
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

		private void barLargeButtonItemQuickSiteEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
				LinkManager.Instance.EmailLinkToQuickSite(SelectedFile);
		}

		private void barLargeButtonItemQuickSiteAdd_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile != null)
				LinkManager.Instance.AddLinkToQuickSite(SelectedFile);
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("qv");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		#region Other Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedFile != null)
			{
				if (SelectedFile.PreviewContainer != null)
				{
					SelectedFile.PreviewContainer.SelectedIndex = comboBoxEditSlides.SelectedIndex;
					pictureBoxPreview.Image = SelectedFile.PreviewContainer.SelectedSlide;
					laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (SelectedFile.PreviewContainer.SelectedIndex + 1).ToString(), SelectedFile.PreviewContainer.Slides.Count.ToString() });
				}
			}
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (SelectedFile != null)
			{
				if (SelectedFile.PreviewContainer != null)
				{
					if (e.Button.Index == 1)
					{
						SelectedFile.PreviewContainer.SelectedIndex++;
						if (SelectedFile.PreviewContainer.SelectedIndex >= SelectedFile.PreviewContainer.Slides.Count)
							SelectedFile.PreviewContainer.SelectedIndex = 0;
						comboBoxEditSlides.SelectedIndex = SelectedFile.PreviewContainer.SelectedIndex;
					}
					else if (e.Button.Index == 2)
					{
						SelectedFile.PreviewContainer.SelectedIndex--;
						if (SelectedFile.PreviewContainer.SelectedIndex < 0)
							SelectedFile.PreviewContainer.SelectedIndex = SelectedFile.PreviewContainer.Slides.Count - 1;
						comboBoxEditSlides.SelectedIndex = SelectedFile.PreviewContainer.SelectedIndex;
					}
				}
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
			if (SelectedFile != null)
			{
				if (PowerPointHelper.Instance.GetActiveSlideIndex() != -1)
				{
					AppManager.Instance.ActivatePowerPoint();
					AppManager.Instance.ActivateMainForm();
					if (SelectedFile.PresentationProperties != null)
					{
						if ((PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == MsoOrientation.msoOrientationHorizontal && SelectedFile.PresentationProperties.Orientation.Equals("Portrait")) ||
							(PowerPointHelper.Instance.ActivePresentation.PageSetup.SlideOrientation == MsoOrientation.msoOrientationVertical && SelectedFile.PresentationProperties.Orientation.Equals("Landscape")))
							if (AppManager.Instance.ShowWarningQuestion("This slide is not the same size as your presentation.\nDo you still want to add it?") != DialogResult.Yes)
								return;
					}
					using (var form = new FormProgress())
					{
						form.laProgress.Text = "Inserting slides...";
						form.TopMost = true;
						var thread = new Thread(delegate()
													{
														AppManager.Instance.ActivityManager.AddLinkAccessActivity("Insert Slide", SelectedFile.Name, SelectedFile.Type.ToString(), SelectedFile.OriginalPath, SelectedFile.Parent.Parent.Parent.Name, SelectedFile.Parent.Parent.Name);
														PowerPointHelper.Instance.OpenSlideSourcePresentation(_tempCopy);
														PowerPointHelper.Instance.AppendSlide(allSlides ? -1 : (SelectedFile.PreviewContainer.SelectedIndex + 1), checkEditChangeSlideTemplate.Checked && comboBoxEditSlideTemplate.EditValue != null ? MasterWizardManager.Instance.MasterWizards[comboBoxEditSlideTemplate.EditValue.ToString()].TemplatePath : string.Empty);
													});
						thread.Start();
						form.Show();
						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
					using (var form = new FormSlideOutput())
					{
						DialogResult result = form.ShowDialog();
						switch (result)
						{
							case DialogResult.Cancel:
								AppManager.Instance.ActivateMainForm();
								Activate();
								Focus();
								break;
							case DialogResult.Abort:
								Application.Exit();
								break;
						}
					}
				}
				else
				{
					using (var warningForm = new FormSelectSlideWarning())
						warningForm.ShowDialog();
				}
			}
		}
		#endregion

		public LibraryLink SelectedFile { get; set; }
	}
}