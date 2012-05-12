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

        public TabPages.TabHomeControl TabHome { get; set; }
        public TabPages.TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }

        private FormMain()
        {
            this.TabHome = new TabPages.TabHomeControl();
            this.TabOvernightsCalendar = new TabPages.TabOvernightsCalendarControl();
            InitializeComponent();

            comboBoxItemPackages.SelectedIndexChanged += new System.EventHandler(this.TabHome.comboBoxItemPackages_SelectedIndexChanged);
            comboBoxItemStations.SelectedIndexChanged += new System.EventHandler(this.TabHome.comboBoxItemStations_SelectedIndexChanged);
            comboBoxItemPages.SelectedIndexChanged += new System.EventHandler(this.TabHome.comboBoxItemPages_SelectedIndexChanged);
            buttonItemHomeClassicView.Click += new EventHandler(this.TabHome.ChangeView_Click);
            buttonItemHomeListView.Click += new EventHandler(this.TabHome.ChangeView_Click);
            buttonItemHomeSolutionView.Click += new EventHandler(this.TabHome.ChangeView_Click);
            buttonItemLargerText.Click += new EventHandler(this.TabHome.buttonItemLargerText_Click);
            buttonItemSmallerText.Click += new EventHandler(this.TabHome.buttonItemSmallerText_Click);
            buttonItemEmailBin.CheckedChanged += new EventHandler(this.TabHome.buttonItemEmailBin_CheckedChanged);
            buttonItemHomeSearchByTags.Click += new EventHandler(this.TabHome.buttonItemHomeSearchMode_Click);
            buttonItemHomeSearchByFileName.Click += new EventHandler(this.TabHome.buttonItemHomeSearchMode_Click);
            buttonItemHomeSearchRecentFiles.Click += new EventHandler(this.TabHome.buttonItemHomeSearchMode_Click);
            buttonItemHomeSearchByTags.CheckedChanged += new EventHandler(this.TabHome.buttonItemHomeSearchMode_CheckedChanged);
            buttonItemHomeSearchByFileName.CheckedChanged += new EventHandler(this.TabHome.buttonItemHomeSearchMode_CheckedChanged);
            buttonItemHomeSearchRecentFiles.CheckedChanged += new EventHandler(this.TabHome.buttonItemHomeSearchMode_CheckedChanged);
            buttonItemHomeAddSlide.Click += new EventHandler(this.TabHome.buttonItemHomeAddSlide_Click);
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

            buttonItemCalendarFontSizeLarger.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFontLarger_Click);
            buttonItemCalendarFontSizeSmaler.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFontSmaller_Click);
            buttonItemCalendarHelp.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemHelp_Click);
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

        private void buttonItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
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
        }

        #region Form Event Handlers
        private void FormMain_Load(object sender, EventArgs e)
        {
            ActivityRecorder.Instance.StartRecording();
            SDRecorder.Instance.StartRecording();
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.IconPath))
                this.Icon = new Icon(ConfigurationClasses.SettingsManager.Instance.IconPath);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            ribbonControl.Enabled = false;
            this.TabHome.InitPage();
            ribbonControl_SelectedRibbonTabChanged(null, null);
            ribbonControl.Enabled = true;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActivityRecorder.Instance.StopRecording();
            SDRecorder.Instance.StopRecording();
            InteropClasses.PowerPointHelper.Instance.Disconnect();
            InteropClasses.ExcelHelper.Instance.Close();
            InteropClasses.WordHelper.Instance.Close();
        }
        #endregion
    }
}
