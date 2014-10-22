using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Core;
using SalesDepot.BusinessClasses;
using SalesDepot.CommonGUI.Floater;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;
using PowerPointHelper = SalesDepot.InteropClasses.PowerPointHelper;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormPowerPointQuickViewOld : MetroForm
	{
		private FileInfo _originalFile;
		private double _scaleK;
		private FileInfo _viewedFile;

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

		public LibraryLink SelectedFile { get; set; }

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
			if (SelectedFile != null)
			{
				_originalFile = new FileInfo(SelectedFile.LocalPath);
				_viewedFile = _originalFile.CopyTo(Path.Combine(AppManager.Instance.TempFolder.FullName, DateTime.Now.ToString("yyyyMMdd-hmmsstt.PPT")), true);
				Text = "QuickView - " + SelectedFile.NameWithExtension;
				laFileInfo.Text = "File Added: " + SelectedFile.AddDate.ToString("MM/dd/yy");
				FormMain.Instance.TopMost = true;
				PowerPointHelper.Instance.PowerPointVisible = true;
				if (IsLargeFont())
					_scaleK = 1.67;
				else
					_scaleK = 1.35;
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading the presentation...";

					IntPtr containerHandle = pnPreview.Handle;
					int containerHeight = pnPreview.Height;
					int containerWidth = pnPreview.Width;

					var thread = new Thread(delegate()
					{
						PowerPointHelper.Instance.OpenSlideSourcePresentation(_viewedFile);
						if (SelectedFile.PresentationProperties == null && PowerPointHelper.Instance.SlideSourcePresentation != null)
						{
							SelectedFile.PresentationProperties = new PresentationProperties();
							SelectedFile.PresentationProperties.Height = PowerPointHelper.Instance.SlideSourcePresentation.PageSetup.SlideHeight / 72;
							SelectedFile.PresentationProperties.Width = PowerPointHelper.Instance.SlideSourcePresentation.PageSetup.SlideWidth / 72;
							;
							SelectedFile.PresentationProperties.LastUpdate = DateTime.Now;
						}
						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							if (SelectedFile.PresentationProperties != null)
							{
								var themeManager = new ThemeManager(String.Format(ThemeManager.RootTemplate, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), SelectedFile.PresentationProperties.Size));
								if (themeManager.Themes.Any())
								{
									pnSlideTemplate.Visible = true;
									comboBoxEditSlideTemplate.Properties.Items.Clear();
									comboBoxEditSlideTemplate.Properties.Items.AddRange(themeManager.Themes);
									comboBoxEditSlideTemplate.SelectedIndex = 0;
								}
								else
									pnSlideTemplate.Visible = false;
							}
							else
							{
								pnSlideTemplate.Visible = false;
							}
						});
						PowerPointHelper.Instance.ViewSlideShow();
						PowerPointHelper.Instance.ResizeSlideShow(containerHandle, (int)(containerHeight / _scaleK), (int)(containerWidth / _scaleK));
					});
					thread.Start();
					form.Show();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}

				if (PowerPointHelper.Instance.SlideSourcePresentation != null)
				{
					comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
					comboBoxEditSlides.Properties.Items.Clear();
					for (int i = 1; i <= PowerPointHelper.Instance.SlideSourcePresentation.Slides.Count; i++)
						comboBoxEditSlides.Properties.Items.Add(i.ToString());
					if (comboBoxEditSlides.Properties.Items.Count > 0)
						comboBoxEditSlides.SelectedIndex = 0;
					barLargeButtonItemAddAllSlides.Enabled = comboBoxEditSlides.Properties.Items.Count > 1;
					comboBoxEditSlides_SelectedIndexChanged(null, null);
					comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
					barLargeButtonItemPDF.Enabled = !PowerPointHelper.Instance.Is2003;
					barLargeButtonItemEmail.Enabled = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayQuickView) == EmailButtonsDisplayOptions.DisplayQuickView;
					barLargeButtonItemQuickSiteEmail.Visibility = barLargeButtonItemQuickSiteAdd.Visibility = SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
				}
				FormMain.Instance.TopMost = false;
				TopMost = true;
				TopMost = false;
			}
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = false;
		}

		private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Closing the presentation...";
				form.TopMost = true;
				var thread = new Thread(delegate() { PowerPointHelper.Instance.ExitSlideShow(); });
				thread.Start();
				form.Show();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}

			try
			{
				_viewedFile.Delete();
			}
			catch { }
		}

		private void FormQuickView_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
			FormMain.Instance.TopMost = true;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Resizing the presentation...";
				form.TopMost = true;
				IntPtr containerHandle = pnPreview.Handle;
				int containerHeight = pnPreview.Height;
				int containerWidth = pnPreview.Width;
				var thread = new Thread(delegate() { PowerPointHelper.Instance.ResizeSlideShow(containerHandle, (int)(containerHeight / _scaleK), (int)(containerWidth / _scaleK)); });
				thread.Start();
				form.Show();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			FormMain.Instance.TopMost = false;
			TopMost = true;
			TopMost = false;
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			LinkManager.Instance.OpenCopyOfFile(SelectedFile);
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			LinkManager.Instance.SaveFile("Save copy of the presentation as", SelectedFile);
		}

		private void barButtonItemSaveAsPDF_ItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormSaveAsPDF())
			{
				DialogResult result = form.ShowDialog();
				bool wholeFile = form.WholeFile;

				if (result == DialogResult.Cancel) return;
				string destinationFileName = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(_originalFile.FullName) + ".pdf");
				int selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
				using (var progressForm = new FormProgress())
				{
					progressForm.laProgress.Text = "Saving as PDF...";
					progressForm.TopMost = true;
					var thread = new Thread(delegate() { PowerPointHelper.Instance.ExportPresentationAsPDF(wholeFile ? -1 : selectedIndex, destinationFileName); });
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

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (SelectedFile == null) return;
			using (var form = new FormEmailPresentation())
			{
				form.SelectedFile = SelectedFile;
				form.ActiveSlide = comboBoxEditSlides.SelectedIndex + 1;
				form.ShowDialog();
			}
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			AppManager.Instance.ActivityManager.AddLinkAccessActivity("Print Link", SelectedFile.Name, SelectedFile.Type.ToString(), SelectedFile.OriginalPath, SelectedFile.Parent.Parent.Parent.Name, SelectedFile.Parent.Parent.Name);
			PowerPointHelper.Instance.PrintPresentation(comboBoxEditSlides.SelectedIndex + 1);
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

		private void simpleButtonSettings_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.FileLocationSettings();
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
			PowerPointHelper.Instance.SlideShowWindow.View.GotoSlide(comboBoxEditSlides.SelectedIndex + 1, MsoTriState.msoFalse);
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
			if (SelectedFile == null) return;
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
				int selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Inserting slides...";
					FloaterManager.Instance.ShowFloater(this, SettingsManager.Instance.SalesDepotName, FormMain.Instance.FloaterLogo, () =>
					{
						form.TopMost = true;
						var thread = new Thread(delegate()
						{
							AppManager.Instance.ActivityManager.AddLinkAccessActivity("Insert Slide", SelectedFile.Name, SelectedFile.Type.ToString(), SelectedFile.OriginalPath, SelectedFile.Parent.Parent.Parent.Name, SelectedFile.Parent.Parent.Name);
							var selectedTheme = comboBoxEditSlideTemplate.EditValue as Theme;
							PowerPointHelper.Instance.AppendSlide(allSlides ? -1 : selectedIndex, checkEditChangeSlideTemplate.Checked && selectedTheme != null ? selectedTheme.ThemeFilePath : String.Empty);
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

		private bool IsLargeFont()
		{
			Graphics g = base.CreateGraphics();
			return g.DpiX > 96;
		}
		#endregion
	}
}