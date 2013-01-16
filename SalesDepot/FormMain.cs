using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.InteropClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.TabPages;
using SalesDepot.ToolForms;

namespace SalesDepot
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		private bool _alowToSave;

		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		public TabHomeControl TabHome { get; set; }
		public TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }
		public TabProgramSchedule TabProgramSchedule { get; set; }
		public TabProgramSearch TabProgramSearch { get; set; }

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		private FormMain()
		{
			InitializeComponent();

			TabHome = new TabHomeControl();
			comboBoxItemPackages.SelectedIndexChanged += TabHome.comboBoxItemPackages_SelectedIndexChanged;
			comboBoxItemStations.SelectedIndexChanged += TabHome.comboBoxItemStations_SelectedIndexChanged;
			comboBoxItemPages.SelectedIndexChanged += TabHome.comboBoxItemPages_SelectedIndexChanged;
			buttonItemHomeClassicView.Click += TabHome.ChangeView_Click;
			buttonItemHomeListView.Click += TabHome.ChangeView_Click;
			buttonItemHomeAccordionView.Click += TabHome.ChangeView_Click;
			buttonItemHomeSolutionView.Click += TabHome.ChangeView_Click;
			buttonItemLargerText.Click += TabHome.ClassicViewControl.buttonItemLargerText_Click;
			buttonItemSmallerText.Click += TabHome.ClassicViewControl.buttonItemSmallerText_Click;
			buttonItemEmailBin.CheckedChanged += TabHome.ClassicViewControl.buttonItemEmailBin_CheckedChanged;
			buttonItemHomeHelp.Click += TabHome.buttonItemHomeHelp_Click;

			buttonItemSettingsLaunchPowerPoint.CheckedChanged += TabHome.buttonItemSettingsLaunchPowerPoint_CheckedChanged;
			buttonItemSettingsMultitab.CheckedChanged += TabHome.buttonItemSettingsMultitab_CheckedChanged;
			buttonItemSettingsPowerPointLaunch.Click += TabHome.buttonItemSettingsPowerPointSettings_Click;
			buttonItemSettingsPowerPointMenu.Click += TabHome.buttonItemSettingsPowerPointSettings_Click;
			buttonItemSettingsPowerPointViewer.Click += TabHome.buttonItemSettingsPowerPointSettings_Click;
			buttonItemSettingsPowerPointLaunch.CheckedChanged += TabHome.buttonItemSettingsPowerPointSettings_CheckedChanged;
			buttonItemSettingsPowerPointMenu.CheckedChanged += TabHome.buttonItemSettingsPowerPointSettings_CheckedChanged;
			buttonItemSettingsPowerPointViewer.CheckedChanged += TabHome.buttonItemSettingsPowerPointSettings_CheckedChanged;
			buttonItemSettingsPDFLaunch.Click += TabHome.buttonItemSettingsPDFSettings_Click;
			buttonItemSettingsPDFMenu.Click += TabHome.buttonItemSettingsPDFSettings_Click;
			buttonItemSettingsPDFViewer.Click += TabHome.buttonItemSettingsPDFSettings_Click;
			buttonItemSettingsPDFLaunch.CheckedChanged += TabHome.buttonItemSettingsPDFSettings_CheckedChanged;
			buttonItemSettingsPDFMenu.CheckedChanged += TabHome.buttonItemSettingsPDFSettings_CheckedChanged;
			buttonItemSettingsPDFViewer.CheckedChanged += TabHome.buttonItemSettingsPDFSettings_CheckedChanged;
			buttonItemSettingsWordLaunch.Click += TabHome.buttonItemSettingsWordSettings_Click;
			buttonItemSettingsWordMenu.Click += TabHome.buttonItemSettingsWordSettings_Click;
			buttonItemSettingsWordViewer.Click += TabHome.buttonItemSettingsWordSettings_Click;
			buttonItemSettingsWordLaunch.CheckedChanged += TabHome.buttonItemSettingsWordSettings_CheckedChanged;
			buttonItemSettingsWordMenu.CheckedChanged += TabHome.buttonItemSettingsWordSettings_CheckedChanged;
			buttonItemSettingsWordViewer.CheckedChanged += TabHome.buttonItemSettingsWordSettings_CheckedChanged;
			buttonItemSettingsExcelLaunch.Click += TabHome.buttonItemSettingsExcelSettings_Click;
			buttonItemSettingsExcelMenu.Click += TabHome.buttonItemSettingsExcelSettings_Click;
			buttonItemSettingsExcelViewer.Click += TabHome.buttonItemSettingsExcelSettings_Click;
			buttonItemSettingsExcelLaunch.CheckedChanged += TabHome.buttonItemSettingsExcelSettings_CheckedChanged;
			buttonItemSettingsExcelMenu.CheckedChanged += TabHome.buttonItemSettingsExcelSettings_CheckedChanged;
			buttonItemSettingsExcelViewer.CheckedChanged += TabHome.buttonItemSettingsExcelSettings_CheckedChanged;
			buttonItemSettingsVideoLaunch.Click += TabHome.buttonItemSettingsVideoSettings_Click;
			buttonItemSettingsVideoMenu.Click += TabHome.buttonItemSettingsVideoSettings_Click;
			buttonItemSettingsVideoViewer.Click += TabHome.buttonItemSettingsVideoSettings_Click;
			buttonItemSettingsVideoLaunch.CheckedChanged += TabHome.buttonItemSettingsVideoSettings_CheckedChanged;
			buttonItemSettingsVideoMenu.CheckedChanged += TabHome.buttonItemSettingsVideoSettings_CheckedChanged;
			buttonItemSettingsVideoViewer.CheckedChanged += TabHome.buttonItemSettingsVideoSettings_CheckedChanged;
			buttonItemSettingsQuickViewImages.Click += TabHome.buttonItemSettingsQuickView_Click;
			buttonItemSettingsQuickViewSlides.Click += TabHome.buttonItemSettingsQuickView_Click;
			buttonItemSettingsQuickViewImages.CheckedChanged += TabHome.buttonItemSettingsQuickViewSettings_CheckedChanged;
			buttonItemSettingsQuickViewSlides.CheckedChanged += TabHome.buttonItemSettingsQuickViewSettings_CheckedChanged;
			buttonItemSettingsEmail.Click += TabHome.buttonItemSettingsEmail_Click;
			buttonItemSettingsHelp.Click += TabHome.buttonItemSettingsHelp_Click;

			TabOvernightsCalendar = new TabOvernightsCalendarControl();
			labelItemCalendarDisclaimerLogo.Click += TabOvernightsCalendar.buttonItemCalendarDisclaimer_Click;
			buttonItemCalendarFontSizeLarger.Click += TabOvernightsCalendar.buttonItemCalendarFontLarger_Click;
			buttonItemCalendarFontSizeSmaler.Click += TabOvernightsCalendar.buttonItemCalendarFontSmaller_Click;
			buttonItemCalendarHelp.Click += TabOvernightsCalendar.buttonItemHelp_Click;

			TabProgramSchedule = new TabProgramSchedule();
			comboBoxEditProgramScheduleStation.EditValueChanged += TabProgramSchedule.comboBoxEditScheduleStation_EditValueChanged;
			dateEditProgramScheduleDay.EditValueChanged += TabProgramSchedule.dateEditScheduleDay_EditValueChanged;
			buttonItemProgramScheduleInfo.CheckedChanged += TabProgramSchedule.buttonItemScheduleInfo_CheckedChanged;
			buttonItemProgramScheduleBrowseDay.Click += TabProgramSchedule.buttonItemScheduleBrowseType_Click;
			buttonItemProgramScheduleBrowseMonth.Click += TabProgramSchedule.buttonItemScheduleBrowseType_Click;
			buttonItemProgramScheduleBrowseWeek.Click += TabProgramSchedule.buttonItemScheduleBrowseType_Click;
			buttonItemProgramScheduleBrowseDay.CheckedChanged += TabProgramSchedule.buttonItemScheduleBrowseType_CheckedChanged;
			buttonItemProgramScheduleBrowseMonth.CheckedChanged += TabProgramSchedule.buttonItemScheduleBrowseType_CheckedChanged;
			buttonItemProgramScheduleBrowseWeek.CheckedChanged += TabProgramSchedule.buttonItemScheduleBrowseType_CheckedChanged;
			buttonItemProgramScheduleBrowseForward.Click += TabProgramSchedule.buttonItemScheduleBrowseButton_Click;
			buttonItemProgramScheduleBrowseBackward.Click += TabProgramSchedule.buttonItemScheduleBrowseButton_Click;
			buttonItemProgramScheduleOutputExcel.Click += TabProgramSchedule.buttonItemScheduleOutputExcel_Click;
			buttonItemProgramScheduleOutputPDF.Click += TabProgramSchedule.buttonItemScheduleOutputPDF_Click;
			buttonItemProgramScheduleHelp.Click += TabProgramSchedule.buttonItemHelp_Click;

			TabProgramSearch = new TabProgramSearch();
			comboBoxEditProgramSearchStation.EditValueChanged += TabProgramSearch.comboBoxEditSearchStation_EditValueChanged;
			comboBoxEditProgramSearchPrograms.KeyDown += TabProgramSearch.comboBoxEditProgramSearchPrograms_KeyDown;
			dateEditProgramSearchDateStart.EditValueChanged += TabProgramSearch.dateEditProgramSearchDate_EditValueChanged;
			dateEditProgramSearchDateEnd.EditValueChanged += TabProgramSearch.dateEditProgramSearchDate_EditValueChanged;
			buttonItemProgramSearchRun.Click += TabProgramSearch.buttonItemSearchRun_Click;
			buttonItemProgramSearchOutputExcel.Click += TabProgramSearch.buttonItemSearchOutputExcel_Click;
			buttonItemProgramSearchOutputPDF.Click += TabProgramSearch.buttonItemSearchOutputPDF_Click;
			buttonItemProgramSearchHelp.Click += TabProgramSearch.buttonItemHelp_Click;
		}

		private void LoadApplicationSettings()
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);
			if (File.Exists(SettingsManager.Instance.CalendarLogoPath))
				labelItemCalendarLogo.Image = new Bitmap(SettingsManager.Instance.CalendarLogoPath);
			Text = string.Format("{0} - User: {1}", SettingsManager.Instance.SalesDepotName, Environment.UserName);

			buttonItemHomeClassicView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.ClassicTitle) ? SettingsManager.Instance.ClassicTitle : Instance.buttonItemHomeClassicView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeClassicView, new SuperTooltipInfo(SettingsManager.Instance.ClassicTitle, "", SettingsManager.Instance.ClassicDescription, null, null, eTooltipColor.Gray));

			buttonItemHomeListView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.ListTitle) ? SettingsManager.Instance.ListTitle : Instance.buttonItemHomeListView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeListView, new SuperTooltipInfo(SettingsManager.Instance.ListTitle, "", SettingsManager.Instance.ListDescription, null, null, eTooltipColor.Gray));

			buttonItemHomeAccordionView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.AccordionTitle) ? SettingsManager.Instance.AccordionTitle : Instance.buttonItemHomeAccordionView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeAccordionView, new SuperTooltipInfo(SettingsManager.Instance.AccordionTitle, "", SettingsManager.Instance.AccordionDescription, null, null, eTooltipColor.Gray));

			buttonItemHomeSolutionView.Text = !string.IsNullOrEmpty(SettingsManager.Instance.SolutionTitle) ? SettingsManager.Instance.SolutionTitle : Instance.buttonItemHomeSolutionView.Text;
			superTooltip.SetSuperTooltip(buttonItemHomeSolutionView, new SuperTooltipInfo(SettingsManager.Instance.SolutionTitle, "", SettingsManager.Instance.SolutionDescription, null, null, eTooltipColor.Gray));

			ribbonBarHomeView.RecalcLayout();

			buttonItemProgramScheduleOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;
			buttonItemProgramSearchOutputPDF.Enabled = !PowerPointHelper.Instance.Is2003;
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			Instance.Opacity = 0;
			RegistryHelper.MaximizeSalesDepot = false;

			Image floaterLogo = null;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				floaterLogo = labelItemPackageLogo.Image;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				floaterLogo = labelItemCalendarLogo.Image;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule || ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
				floaterLogo = labelItemProgramScheduleStationLogo.Image;

			using (var form = new FormFloater(Left + Width - 50, Top + 50, _floaterPositionX, _floaterPositionY, floaterLogo, ribbonBarStations.Text))
			{
				if (form.ShowDialog() != DialogResult.No)
				{
					_floaterPositionY = form.Top;
					_floaterPositionX = form.Left;
					Instance.Opacity = 1;
					RegistryHelper.SalesDepotHandle = Handle;
					RegistryHelper.MaximizeSalesDepot = true;
					AppManager.Instance.ActivateMainForm();
				}
				else
					Close();
			}
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (_alowToSave)
			{
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				{
					if (!pnContainer.Controls.Contains(TabHome))
						pnContainer.Controls.Add(TabHome);
					TabHome.BringToFront();
					AppManager.Instance.ActivityManager.AddUserActivity("Wall Bin selected");
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				{
					if (!pnContainer.Controls.Contains(TabOvernightsCalendar))
						pnContainer.Controls.Add(TabOvernightsCalendar);
					TabOvernightsCalendar.BringToFront();
					AppManager.Instance.ActivityManager.AddUserActivity("Overnights Calendar selected");
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule)
				{
					if (!pnContainer.Controls.Contains(TabProgramSchedule))
						pnContainer.Controls.Add(TabProgramSchedule);
					TabProgramSchedule.BringToFront();
					TabProgramSchedule.Focus();
					AppManager.Instance.ActivityManager.AddUserActivity("Program Schedule selected");
				}
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
				{
					if (!pnContainer.Controls.Contains(TabProgramSearch))
						pnContainer.Controls.Add(TabProgramSearch);
					TabProgramSearch.BringToFront();
					TabProgramSearch.Focus();
					AppManager.Instance.ActivityManager.AddUserActivity("Program Search selected");
				}
				SettingsManager.Instance.CalendarView = ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar;
				SettingsManager.Instance.SaveSettings();
			}
		}

		#region Form Event Handlers
		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadApplicationSettings();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			RegistryHelper.SalesDepotHandle = Handle;
			RegistryHelper.MaximizeSalesDepot = true;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = SettingsManager.Instance.UseRemoteConnection ? "Loading Remote Sales Libraries..." : "Loading Sales Libraries...";
				form.TopMost = true;
				ribbonControl.Visible = false;
				pnEmpty.BringToFront();
				var thread = new Thread(delegate()
											{
												LibraryManager.Instance.LoadLibraryPackages(new DirectoryInfo(SettingsManager.Instance.LibraryRootFolder));
												if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
												{
													Invoke((MethodInvoker)delegate
																			  {
																				  DecoratorManager.Instance.BuildPackageViewers();
																				  Application.DoEvents();
																				  DecoratorManager.Instance.BuildOvernightsCalendars();
																				  Application.DoEvents();
																				  DecoratorManager.Instance.BuildProgramManagers();
																				  Application.DoEvents();
																			  });
												}
											});
				form.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();

				if (LibraryManager.Instance.LibraryPackageCollection.Count > 0)
				{
					thread = new Thread(delegate()
											{
												Invoke((MethodInvoker)delegate
																		  {
																			  TabHome.LoadPage();
																			  Application.DoEvents();
																			  _alowToSave = true;
																			  if (SettingsManager.Instance.CalendarView)
																				  ribbonTabItemCalendar.Select();
																			  else
																				  ribbonControl_SelectedRibbonTabChanged(null, null);
																			  Application.DoEvents();
																		  });
											});
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
				}

				ribbonControl.Visible = true;
				pnContainer.BringToFront();
			}
			AppManager.Instance.ActivateMainForm();
			if (LibraryManager.Instance.LibraryPackageCollection.Count == 0)
			{
				ribbonBarStations.Enabled = false;
				ribbonBarHomeView.Enabled = false;
				ribbonTabItemSettings.Enabled = false;
				AppManager.Instance.ShowWarning("Library is not available...\nCheck your network connections....");
			}
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			PowerPointHelper.Instance.Disconnect();
			WordHelper.Instance.Close();
		}
		#endregion
	}
}