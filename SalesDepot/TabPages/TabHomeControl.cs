using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.PresentationClasses.WallBin;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.ToolForms;
using SalesDepot.ToolForms.Settings;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl
	{
		public IWallBinView SelectedView { get; private set; }
		public ClassicViewControl ClassicViewControl { get; private set; }
		public SolutionViewControl SolutionViewControl { get; private set; }
		public AppManager.SingleParamDelegate PageChanged;
		public AppManager.SingleParamDelegate StationChanged;
		private bool _allowToSave;

		public TabHomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			ClassicViewControl = new ClassicViewControl();
			SolutionViewControl = new SolutionViewControl();
		}

		#region Methods
		public void LoadPage()
		{
			_allowToSave = false;
			LoadWallBinSettings();
			LoadPackages();
			ClassicViewControl.UpdateFontButtonStatus();
			ApplySelectedDecorator();
			ApplySelectedView();
			_allowToSave = true;
		}

		private void LoadWallBinSettings()
		{
			#region Wall Bin Last Saved State
			FormMain.Instance.buttonItemHomeSolutionView.Enabled = !LibraryManager.Instance.OldFormatDetected;

			FormMain.Instance.buttonItemHomeClassicView.Checked = SettingsManager.Instance.ClassicView;
			FormMain.Instance.buttonItemHomeListView.Checked = SettingsManager.Instance.ListView;
			FormMain.Instance.buttonItemHomeAccordionView.Checked = SettingsManager.Instance.AccordionView;
			FormMain.Instance.buttonItemHomeSolutionView.Checked = SettingsManager.Instance.SolutionView;

			if (SettingsManager.Instance.ClassicView || SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView || LibraryManager.Instance.OldFormatDetected)
				SelectedView = ClassicViewControl;
			else if (SettingsManager.Instance.SolutionView)
				SelectedView = SolutionViewControl;

			FormMain.Instance.buttonItemHomeClassicView.CheckedChanged += ChangeView_CheckedChanged;
			FormMain.Instance.buttonItemHomeSolutionView.CheckedChanged += ChangeView_CheckedChanged;
			FormMain.Instance.buttonItemHomeListView.CheckedChanged += ChangeView_CheckedChanged;
			FormMain.Instance.buttonItemHomeAccordionView.CheckedChanged += ChangeView_CheckedChanged;
			#endregion

			#region Wall Bin Configuration
			FormMain.Instance.buttonItemSettingsLaunchPowerPoint.Checked = SettingsManager.Instance.LaunchPPT;
			FormMain.Instance.buttonItemSettingsMultitab.Checked = SettingsManager.Instance.MultitabView;
			switch (SettingsManager.Instance.PowerPointLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.PDFLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsPDFViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsPDFMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsPDFLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.WordLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsWordViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsWordMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsWordLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.ExcelLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsExcelViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsExcelMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsExcelLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.VideoLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsVideoViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsVideoMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsVideoLaunch.Checked = true;
					break;
			}
			FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = !SettingsManager.Instance.OldStyleQuickView;
			FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = SettingsManager.Instance.OldStyleQuickView;
			#endregion
		}

		private void LoadPackages()
		{
			FormMain.Instance.comboBoxItemPackages.Items.Clear();
			foreach (LibraryPackage salesDepot in LibraryManager.Instance.LibraryPackageCollection)
			{
				FormMain.Instance.comboBoxItemPackages.Items.Add(salesDepot.Name);
				var packageLable = new LabelItem();
				packageLable.Text = salesDepot.Name;
				packageLable.TextAlignment = StringAlignment.Near;
				while (packageLable.Text.Length < 20)
					packageLable.Text += " ";
			}

			FormMain.Instance.comboBoxItemPackages.Enabled = true;
			FormMain.Instance.comboBoxItemStations.Enabled = true;
			FormMain.Instance.comboBoxItemPages.Enabled = !SettingsManager.Instance.MultitabView;

			if (FormMain.Instance.comboBoxItemPackages.Items.Count > 0)
			{
				int previousSelectedPackageIndex = FormMain.Instance.comboBoxItemPackages.Items.IndexOf(SettingsManager.Instance.SelectedPackage);
				if (previousSelectedPackageIndex >= 0)
					FormMain.Instance.comboBoxItemPackages.SelectedIndex = previousSelectedPackageIndex;
				else
					FormMain.Instance.comboBoxItemPackages.SelectedIndex = 0;
				FormMain.Instance.comboBoxItemPackages.Enabled = FormMain.Instance.comboBoxItemPackages.Items.Count > 1;
				DecoratorManager.Instance.ActivePackageViewer = DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];
			}
			else
			{
				FormMain.Instance.comboBoxItemPackages.Enabled = false;
				FormMain.Instance.comboBoxItemStations.Enabled = false;
				FormMain.Instance.comboBoxItemPages.Enabled = false;
			}
		}

		private void ApplySelectedView()
		{
			SelectedView.ApplyView();
			if (!pnMain.Controls.Contains(SelectedView as Control))
				pnMain.Controls.Add(SelectedView as Control);
			(SelectedView as Control).BringToFront();
		}

		private void ApplySelectedDecorator()
		{
			if (DecoratorManager.Instance.ActivePackageViewer != null)
			{
				FormMain.Instance.ribbonBarStations.Text = DecoratorManager.Instance.ActivePackageViewer.Name;
				FormMain.Instance.ribbonBarStations.RecalcLayout();
				FormMain.Instance.ribbonPanelHome.PerformLayout();

				pnEmpty.BringToFront();
				Application.DoEvents();
				DecoratorManager.Instance.ActivePackageViewer.Apply();
				if (!ClassicViewControl.pnRemoteLibraryContainer.Controls.Contains(DecoratorManager.Instance.ActivePackageViewer.Container))
					ClassicViewControl.pnRemoteLibraryContainer.Controls.Add(DecoratorManager.Instance.ActivePackageViewer.Container);
				DecoratorManager.Instance.ActivePackageViewer.Container.BringToFront();
				pnMain.BringToFront();
				Application.DoEvents();
			}
		}
		#endregion

		#region Button's Click Event Handlers

		#region Wall Bin Button's Click Event Handlers
		public void ChangeView_Click(object sender, EventArgs e)
		{
			FormMain.Instance.buttonItemHomeClassicView.Checked = false;
			FormMain.Instance.buttonItemHomeListView.Checked = false;
			FormMain.Instance.buttonItemHomeAccordionView.Checked = false;
			FormMain.Instance.buttonItemHomeSolutionView.Checked = false;
			(sender as ButtonItem).Checked = true;

			if (_allowToSave)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Page...";
					form.TopMost = true;
					var thread = new Thread(delegate()
												{
													FormMain.Instance.Invoke((MethodInvoker)delegate()
																								{
																									pnEmpty.BringToFront();
																									Application.DoEvents();
																									ApplySelectedView();
																									Application.DoEvents();
																									DecoratorManager.Instance.ActivePackageViewer.UpdateView();
																									Application.DoEvents();
																									pnMain.BringToFront();
																									Application.DoEvents();
																								});
												});
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
			else
			{
				ApplySelectedView();
				DecoratorManager.Instance.ActivePackageViewer.UpdateView();
			}
		}

		public void ChangeView_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as ButtonItem).Checked)
			{
				SettingsManager.Instance.ClassicView = FormMain.Instance.buttonItemHomeClassicView.Checked;
				SettingsManager.Instance.ListView = FormMain.Instance.buttonItemHomeListView.Checked;
				SettingsManager.Instance.AccordionView = FormMain.Instance.buttonItemHomeAccordionView.Checked;
				SettingsManager.Instance.SaveSettings();

				if (SettingsManager.Instance.ClassicView || SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView || LibraryManager.Instance.OldFormatDetected)
					SelectedView = ClassicViewControl;
				else if (SettingsManager.Instance.SolutionView)
					SelectedView = SolutionViewControl;
			}
		}

		public void buttonItemHomeHelp_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Instance.ClassicView)
			{
				HelpManager.Instance.OpenHelpLink(FormMain.Instance.buttonItemEmailBin.Checked ? "email" : "classic");
			}
			else if (SettingsManager.Instance.ListView)
			{
				HelpManager.Instance.OpenHelpLink(FormMain.Instance.buttonItemEmailBin.Checked ? "email" : "list");
			}
			else if (SettingsManager.Instance.AccordionView)
			{
				HelpManager.Instance.OpenHelpLink(FormMain.Instance.buttonItemEmailBin.Checked ? "email" : "accordion");
			}
			else
			{
				if (SettingsManager.Instance.SolutionTitleView)
					HelpManager.Instance.OpenHelpLink("title");
				else if (SettingsManager.Instance.SolutionTagsView)
					HelpManager.Instance.OpenHelpLink("target");
				else if (SettingsManager.Instance.SolutionDateView)
					HelpManager.Instance.OpenHelpLink("date");
			}
		}
		#endregion

		#region Settings Button's Click Event Handlers
		public void buttonItemSettingsLaunchPowerPoint_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.LaunchPPT = FormMain.Instance.buttonItemSettingsLaunchPowerPoint.Checked;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.MultitabView = FormMain.Instance.buttonItemSettingsMultitab.Checked;
				SettingsManager.Instance.SaveSettings();
				pnEmpty.BringToFront();
				StationChanged(FormMain.Instance.comboBoxItemStations);
				pnMain.BringToFront();
			}
		}

		public void buttonItemSettingsPowerPointSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsPowerPointSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked)
					SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked)
					SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked)
					SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsPDFSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsPDFViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsPDFMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsPDFLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsPDFSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsPDFViewer.Checked)
					SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsPDFMenu.Checked)
					SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsPDFLaunch.Checked)
					SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsWordSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsWordViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsWordMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsWordLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsWordSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsWordViewer.Checked)
					SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsWordMenu.Checked)
					SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsWordLaunch.Checked)
					SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsExcelSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsExcelViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsExcelMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsExcelLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsExcelSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsExcelViewer.Checked)
					SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsExcelMenu.Checked)
					SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsExcelLaunch.Checked)
					SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsVideoSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsVideoViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsVideoMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsVideoLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsVideoSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsVideoViewer.Checked)
					SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsVideoMenu.Checked)
					SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsVideoLaunch.Checked)
					SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsQuickView_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = false;
			FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsQuickViewSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.OldStyleQuickView = FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsEmail_Click(object sender, EventArgs e)
		{
			using (var form = new FormEmailSettings())
			{
				form.ShowDialog();
				ClassicViewControl.LoadOptions();
			}
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading Page...";
				form.TopMost = true;
				var thread = new Thread(delegate() { FormMain.Instance.Invoke((MethodInvoker)delegate() { ApplySelectedView(); }); });
				form.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}

		public void buttonItemSettingsHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("settings");
		}
		#endregion

		#endregion

		#region Comboboxes Event Handlers
		public void comboBoxItemPackages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < DecoratorManager.Instance.PackageViewers.Count)
				{
					_allowToSave = false;
					SettingsManager.Instance.SelectedPackage = FormMain.Instance.comboBoxItemPackages.SelectedItem.ToString();
					SettingsManager.Instance.SaveSettings();
					DecoratorManager.Instance.ActivePackageViewer = DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];

					using (var form = new FormProgress())
					{
						form.laProgress.Text = string.Format("Loading {0}...", DecoratorManager.Instance.ActivePackageViewer != null ? DecoratorManager.Instance.ActivePackageViewer.Name : "Library");
						form.TopMost = true;
						var thread = new Thread(delegate()
							                        {
								                        FormMain.Instance.Invoke((MethodInvoker)delegate()
									                                                                {
										                                                                SolutionViewControl.ClearSolutionControl();
										                                                                Application.DoEvents();
										                                                                ApplySelectedDecorator();
										                                                                Application.DoEvents();
									                                                                });
							                        });
						form.Show();
						Application.DoEvents();
						thread.Start();
						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
					_allowToSave = true;
				}
				else
					DecoratorManager.Instance.ActivePackageViewer = null;
			}
		}

		public void comboBoxItemStations_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = string.Format("Loading {0}...", DecoratorManager.Instance.ActivePackageViewer != null ? DecoratorManager.Instance.ActivePackageViewer.Name : "Library");
					form.TopMost = true;
					var thread = new Thread(delegate()
						                        {
							                        FormMain.Instance.Invoke((MethodInvoker)delegate()
								                                                                {
									                                                                pnEmpty.BringToFront();
									                                                                Application.DoEvents();
									                                                                StationChanged(sender);
									                                                                Application.DoEvents();
									                                                                pnMain.BringToFront();
									                                                                Application.DoEvents();
								                                                                });
						                        });
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
		}

		public void comboBoxItemPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading Page...";
				form.TopMost = true;
				var thread = new Thread(delegate()
					                        {
						                        FormMain.Instance.Invoke((MethodInvoker)delegate()
							                                                                {
								                                                                pnEmpty.BringToFront();
								                                                                Application.DoEvents();
								                                                                PageChanged(sender);
								                                                                Application.DoEvents();
								                                                                pnMain.BringToFront();
								                                                                Application.DoEvents();
							                                                                });
					                        });
				form.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}
		#endregion
	}
}