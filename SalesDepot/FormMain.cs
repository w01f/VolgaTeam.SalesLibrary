using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;

        private bool _alowToSave = false;

        private int _floaterPositionX = int.MinValue;
        private int _floaterPositionY = int.MinValue;

        public TabPages.TabHomeControl TabHome { get; set; }
        public TabPages.TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }
        public TabPages.TabProgramSchedule TabProgramSchedule { get; set; }
        public TabPages.TabProgramSearch TabProgramSearch { get; set; }

        private FormMain()
        {
            InitializeComponent();

            this.TabHome = new TabPages.TabHomeControl();
            comboBoxItemPackages.SelectedIndexChanged += new System.EventHandler(this.TabHome.comboBoxItemPackages_SelectedIndexChanged);
            comboBoxItemStations.SelectedIndexChanged += new System.EventHandler(this.TabHome.comboBoxItemStations_SelectedIndexChanged);
            comboBoxItemPages.SelectedIndexChanged += new System.EventHandler(this.TabHome.comboBoxItemPages_SelectedIndexChanged);
            buttonItemHomeClassicView.Click += new EventHandler(this.TabHome.ChangeView_Click);
            buttonItemHomeListView.Click += new EventHandler(this.TabHome.ChangeView_Click);
            buttonItemHomeSolutionView.Click += new EventHandler(this.TabHome.ChangeView_Click);
            buttonItemLargerText.Click += new EventHandler(this.TabHome.ClassicViewControl.buttonItemLargerText_Click);
            buttonItemSmallerText.Click += new EventHandler(this.TabHome.ClassicViewControl.buttonItemSmallerText_Click);
            buttonItemEmailBin.CheckedChanged += new EventHandler(this.TabHome.ClassicViewControl.buttonItemEmailBin_CheckedChanged);
            buttonItemHomeSearchByTags.Click += new EventHandler(this.TabHome.SolutionViewControl.buttonItemHomeSearchMode_Click);
            buttonItemHomeSearchByFileName.Click += new EventHandler(this.TabHome.SolutionViewControl.buttonItemHomeSearchMode_Click);
            buttonItemHomeSearchRecentFiles.Click += new EventHandler(this.TabHome.SolutionViewControl.buttonItemHomeSearchMode_Click);
            buttonItemHomeSearchByTags.CheckedChanged += new EventHandler(this.TabHome.SolutionViewControl.buttonItemHomeSearchMode_CheckedChanged);
            buttonItemHomeSearchByFileName.CheckedChanged += new EventHandler(this.TabHome.SolutionViewControl.buttonItemHomeSearchMode_CheckedChanged);
            buttonItemHomeSearchRecentFiles.CheckedChanged += new EventHandler(this.TabHome.SolutionViewControl.buttonItemHomeSearchMode_CheckedChanged);
            buttonItemHomeHelp.Click += new EventHandler(this.TabHome.buttonItemHomeHelp_Click);

            buttonItemSettingsLaunchPowerPoint.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsLaunchPowerPoint_CheckedChanged);
            buttonItemSettingsMultitab.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsMultitab_CheckedChanged);
            buttonItemSettingsPowerPointLaunch.Click += new EventHandler(this.TabHome.buttonItemSettingsPowerPointSettings_Click);
            buttonItemSettingsPowerPointMenu.Click += new EventHandler(this.TabHome.buttonItemSettingsPowerPointSettings_Click);
            buttonItemSettingsPowerPointViewer.Click += new EventHandler(this.TabHome.buttonItemSettingsPowerPointSettings_Click);
            buttonItemSettingsPowerPointLaunch.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsPowerPointSettings_CheckedChanged);
            buttonItemSettingsPowerPointMenu.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsPowerPointSettings_CheckedChanged);
            buttonItemSettingsPowerPointViewer.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsPowerPointSettings_CheckedChanged);
            buttonItemSettingsPDFLaunch.Click += new EventHandler(this.TabHome.buttonItemSettingsPDFSettings_Click);
            buttonItemSettingsPDFMenu.Click += new EventHandler(this.TabHome.buttonItemSettingsPDFSettings_Click);
            buttonItemSettingsPDFViewer.Click += new EventHandler(this.TabHome.buttonItemSettingsPDFSettings_Click);
            buttonItemSettingsPDFLaunch.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsPDFSettings_CheckedChanged);
            buttonItemSettingsPDFMenu.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsPDFSettings_CheckedChanged);
            buttonItemSettingsPDFViewer.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsPDFSettings_CheckedChanged);
            buttonItemSettingsWordLaunch.Click += new EventHandler(this.TabHome.buttonItemSettingsWordSettings_Click);
            buttonItemSettingsWordMenu.Click += new EventHandler(this.TabHome.buttonItemSettingsWordSettings_Click);
            buttonItemSettingsWordViewer.Click += new EventHandler(this.TabHome.buttonItemSettingsWordSettings_Click);
            buttonItemSettingsWordLaunch.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsWordSettings_CheckedChanged);
            buttonItemSettingsWordMenu.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsWordSettings_CheckedChanged);
            buttonItemSettingsWordViewer.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsWordSettings_CheckedChanged);
            buttonItemSettingsExcelLaunch.Click += new EventHandler(this.TabHome.buttonItemSettingsExcelSettings_Click);
            buttonItemSettingsExcelMenu.Click += new EventHandler(this.TabHome.buttonItemSettingsExcelSettings_Click);
            buttonItemSettingsExcelViewer.Click += new EventHandler(this.TabHome.buttonItemSettingsExcelSettings_Click);
            buttonItemSettingsExcelLaunch.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsExcelSettings_CheckedChanged);
            buttonItemSettingsExcelMenu.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsExcelSettings_CheckedChanged);
            buttonItemSettingsExcelViewer.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsExcelSettings_CheckedChanged);
            buttonItemSettingsVideoLaunch.Click += new EventHandler(this.TabHome.buttonItemSettingsVideoSettings_Click);
            buttonItemSettingsVideoMenu.Click += new EventHandler(this.TabHome.buttonItemSettingsVideoSettings_Click);
            buttonItemSettingsVideoViewer.Click += new EventHandler(this.TabHome.buttonItemSettingsVideoSettings_Click);
            buttonItemSettingsVideoLaunch.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsVideoSettings_CheckedChanged);
            buttonItemSettingsVideoMenu.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsVideoSettings_CheckedChanged);
            buttonItemSettingsVideoViewer.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsVideoSettings_CheckedChanged);
            buttonItemSettingsQuickViewImages.Click += new EventHandler(this.TabHome.buttonItemSettingsQuickView_Click);
            buttonItemSettingsQuickViewSlides.Click += new EventHandler(this.TabHome.buttonItemSettingsQuickView_Click);
            buttonItemSettingsQuickViewImages.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsQuickViewSettings_CheckedChanged);
            buttonItemSettingsQuickViewSlides.CheckedChanged += new EventHandler(this.TabHome.buttonItemSettingsQuickViewSettings_CheckedChanged);
            buttonItemSettingsEmail.Click += new EventHandler(this.TabHome.buttonItemSettingsEmail_Click);
            buttonItemSettingsHelp.Click += new EventHandler(this.TabHome.buttonItemSettingsHelp_Click);

            this.TabOvernightsCalendar = new TabPages.TabOvernightsCalendarControl();
            labelItemCalendarDisclaimerLogo.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarDisclaimer_Click);
            buttonItemCalendarFontSizeLarger.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFontLarger_Click);
            buttonItemCalendarFontSizeSmaler.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFontSmaller_Click);
            buttonItemCalendarHelp.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemHelp_Click);

            this.TabProgramSchedule = new TabPages.TabProgramSchedule();
            comboBoxEditProgramScheduleStation.EditValueChanged+=new EventHandler(this.TabProgramSchedule.comboBoxEditScheduleStation_EditValueChanged);
            dateEditProgramScheduleDay.EditValueChanged+=new EventHandler(this.TabProgramSchedule.dateEditScheduleDay_EditValueChanged);
            buttonItemProgramScheduleInfo.CheckedChanged+=new EventHandler(this.TabProgramSchedule.buttonItemScheduleInfo_CheckedChanged);
            buttonItemProgramScheduleBrowseDay.Click+=new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseType_Click);
            buttonItemProgramScheduleBrowseMonth.Click += new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseType_Click);
            buttonItemProgramScheduleBrowseWeek.Click += new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseType_Click);
            buttonItemProgramScheduleBrowseDay.CheckedChanged += new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseType_CheckedChanged);
            buttonItemProgramScheduleBrowseMonth.CheckedChanged += new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseType_CheckedChanged);
            buttonItemProgramScheduleBrowseWeek.CheckedChanged += new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseType_CheckedChanged);
            buttonItemProgramScheduleBrowseForward.Click+=new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseButton_Click);
            buttonItemProgramScheduleBrowseBackward.Click += new EventHandler(this.TabProgramSchedule.buttonItemScheduleBrowseButton_Click);
            buttonItemProgramScheduleOutputExcel.Click+=new EventHandler(this.TabProgramSchedule.buttonItemScheduleOutputExcel_Click);
            buttonItemProgramScheduleOutputPDF.Click += new EventHandler(this.TabProgramSchedule.buttonItemScheduleOutputPDF_Click);
            buttonItemProgramScheduleHelp.Click += new EventHandler(this.TabProgramSchedule.buttonItemHelp_Click);

            this.TabProgramSearch = new TabPages.TabProgramSearch();
            comboBoxEditProgramSearchStation.EditValueChanged += new EventHandler(this.TabProgramSearch.comboBoxEditSearchStation_EditValueChanged);
            comboBoxEditProgramSearchPrograms.KeyDown += new KeyEventHandler(this.TabProgramSearch.comboBoxEditProgramSearchPrograms_KeyDown);
            dateEditProgramSearchDateStart.EditValueChanged += new EventHandler(this.TabProgramSearch.dateEditProgramSearchDate_EditValueChanged);
            dateEditProgramSearchDateEnd.EditValueChanged += new EventHandler(this.TabProgramSearch.dateEditProgramSearchDate_EditValueChanged);
            buttonItemProgramSearchRun.Click+=new EventHandler(this.TabProgramSearch.buttonItemSearchRun_Click);
            buttonItemProgramSearchOutputExcel.Click += new EventHandler(this.TabProgramSearch.buttonItemSearchOutputExcel_Click);
            buttonItemProgramSearchOutputPDF.Click += new EventHandler(this.TabProgramSearch.buttonItemSearchOutputPDF_Click);
            buttonItemProgramSearchHelp.Click += new EventHandler(this.TabProgramSearch.buttonItemHelp_Click);
        }

        public static FormMain Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FormMain();
                return _instance;
            }
        }

        private void LoadApplicationSettings()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.IconPath))
                this.Icon = new Icon(ConfigurationClasses.SettingsManager.Instance.IconPath);
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.CalendarLogoPath))
                labelItemCalendarLogo.Image = new Bitmap(ConfigurationClasses.SettingsManager.Instance.CalendarLogoPath);
            this.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;

            buttonItemHomeClassicView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.ClassicTitle) ? ConfigurationClasses.SettingsManager.Instance.ClassicTitle : FormMain.Instance.buttonItemHomeClassicView.Text;
            buttonItemHomeListView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.ListTitle) ? ConfigurationClasses.SettingsManager.Instance.ListTitle : FormMain.Instance.buttonItemHomeListView.Text;
            buttonItemHomeSolutionView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SolutionTitle) ? ConfigurationClasses.SettingsManager.Instance.SolutionTitle : FormMain.Instance.buttonItemHomeSolutionView.Text;
            ribbonBarHomeView.RecalcLayout();

            buttonItemProgramScheduleOutputPDF.Enabled = !InteropClasses.PowerPointHelper.Instance.Is2003;
            buttonItemProgramSearchOutputPDF.Enabled = !InteropClasses.PowerPointHelper.Instance.Is2003;
        }

        private void buttonItemFloater_Click(object sender, EventArgs e)
        {
            FormMain.Instance.Opacity = 0;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = false;

            Image floaterLogo = null;
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
                floaterLogo = labelItemPackageLogo.Image;
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
                floaterLogo = labelItemCalendarLogo.Image;
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule || ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
                floaterLogo = labelItemProgramScheduleStationLogo.Image;

            using (FormFloater form = new FormFloater(this.Left + this.Width - 50, this.Top + 50, _floaterPositionX, _floaterPositionY, floaterLogo, ribbonBarStations.Text))
            {
                if (form.ShowDialog() != System.Windows.Forms.DialogResult.No)
                {
                    _floaterPositionY = form.Top;
                    _floaterPositionX = form.Left;
                    FormMain.Instance.Opacity = 1;
                    ConfigurationClasses.RegistryHelper.SalesDepotHandle = this.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = true;
                    AppManager.Instance.ActivateMainForm();
                }
                else
                    this.Close();
            }
        }

        private void buttonItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
            {
                if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
                {
                    if (!pnContainer.Controls.Contains(this.TabHome))
                        pnContainer.Controls.Add(this.TabHome);
                    this.TabHome.BringToFront();
                }
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
                {
                    if (!pnContainer.Controls.Contains(this.TabOvernightsCalendar))
                        pnContainer.Controls.Add(this.TabOvernightsCalendar);
                    this.TabOvernightsCalendar.BringToFront();
                }
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSchedule)
                {
                    if (!pnContainer.Controls.Contains(this.TabProgramSchedule))
                        pnContainer.Controls.Add(this.TabProgramSchedule);
                    this.TabProgramSchedule.BringToFront();
                    this.TabProgramSchedule.Focus();
                }
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramSearch)
                {
                    if (!pnContainer.Controls.Contains(this.TabProgramSearch))
                        pnContainer.Controls.Add(this.TabProgramSearch);
                    this.TabProgramSearch.BringToFront();
                    this.TabProgramSearch.Focus();
                }
                ConfigurationClasses.SettingsManager.Instance.CalendarView = ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        #region Form Event Handlers
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadApplicationSettings();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = this.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = true;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection ? "Loading Remote Sales Libraries..." : "Loading Sales Libraries...";
                form.TopMost = true;
                ribbonControl.Visible = false;
                pnEmpty.BringToFront();
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    BusinessClasses.LibraryManager.Instance.LoadLibraryPackages(new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.LibraryRootFolder));
                    if (BusinessClasses.LibraryManager.Instance.LibraryPackageCollection.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildPackageViewers();
                            Application.DoEvents();
                            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildOvernightsCalendars();
                            Application.DoEvents();
                            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildProgramManagers();
                            Application.DoEvents();
                        });
                    }
                }));
                form.Show();
                Application.DoEvents();
                thread.Start();
                while (thread.IsAlive)
                    Application.DoEvents();
                form.Close();

                if (BusinessClasses.LibraryManager.Instance.LibraryPackageCollection.Count > 0)
                {
                    thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            this.TabHome.LoadPage();
                            Application.DoEvents();
                            _alowToSave = true;
                            if (ConfigurationClasses.SettingsManager.Instance.CalendarView)
                                ribbonTabItemCalendar.Select();
                            else
                                ribbonControl_SelectedRibbonTabChanged(null, null);
                            Application.DoEvents();
                        });
                    }));
                    thread.Start();
                    while (thread.IsAlive)
                        Application.DoEvents();
                }

                ribbonControl.Visible = true;
                pnContainer.BringToFront();
            }
            AppManager.Instance.ActivateMainForm();
            if (BusinessClasses.LibraryManager.Instance.LibraryPackageCollection.Count == 0)
            {
                ribbonBarStations.Enabled = false;
                ribbonBarHomeView.Enabled = false;
                ribbonTabItemSettings.Enabled = false;
                AppManager.Instance.ShowWarning("Library is not available...\nCheck your network connections....");
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            ToolClasses.ActivityRecorder.Instance.StopRecording();
            ToolClasses.SDRecorder.Instance.StopRecording();
            InteropClasses.PowerPointHelper.Instance.Disconnect();
            InteropClasses.WordHelper.Instance.Close();
        }
        #endregion
    }
}
