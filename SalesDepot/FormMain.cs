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

        private bool _allowToSave = false;

        private List<BusinessClasses.PackageDecorator> _packageViewers = new List<BusinessClasses.PackageDecorator>();

        public AppManager.SingleParamDelegate StationChanged;
        public AppManager.SingleParamDelegate PageChanged;

        public BusinessClasses.PackageDecorator CurrentPackage { get; set; }

        public CustomControls.ClassicViewControl ClassicViewControl { get; private set; }
        private DevComponents.DotNetBar.SuperTooltipInfo _classicToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Column View", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _listToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Sales Library List View", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _emailToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about how to EMAIL files from this Sales Library", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        public CustomControls.SolutionViewControl SolutionViewControl { get; private set; }
        private DevComponents.DotNetBar.SuperTooltipInfo _targetToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by qualified target criteria", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _titleToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by title or file name", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _dateToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by date range", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        private FormMain()
        {
            InitializeComponent();
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

        #region Methods
        #region Load Sales Depot Methods
        private void BuildPackageViewers()
        {
            _packageViewers.Clear();
            foreach (BusinessClasses.LibraryPackage package in BusinessClasses.LibraryManager.Instance.LibraryPackageCollection)
                _packageViewers.Add(new BusinessClasses.PackageDecorator(package));
        }

        private void FillPackages()
        {
            comboBoxItemPackages.Items.Clear();
            foreach (BusinessClasses.LibraryPackage salesDepot in BusinessClasses.LibraryManager.Instance.LibraryPackageCollection)
            {
                comboBoxItemPackages.Items.Add(salesDepot.Name);
                DevComponents.DotNetBar.LabelItem packageLable = new DevComponents.DotNetBar.LabelItem();
                packageLable.Text = salesDepot.Name;
                packageLable.TextAlignment = StringAlignment.Near;
                while (packageLable.Text.Length < 20)
                    packageLable.Text += " ";
            }

            comboBoxItemPackages.Enabled = true;
            comboBoxItemStations.Enabled = true;
            comboBoxItemPages.Enabled = !ConfigurationClasses.SettingsManager.Instance.MultitabView;

            if (comboBoxItemPackages.Items.Count > 0)
            {
                int previousSelectedPackageIndex = comboBoxItemPackages.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedPackage);
                if (previousSelectedPackageIndex >= 0)
                    comboBoxItemPackages.SelectedIndex = previousSelectedPackageIndex;
                else
                    comboBoxItemPackages.SelectedIndex = 0;
                comboBoxItemPackages.Enabled = comboBoxItemPackages.Items.Count > 1;
            }
            else
            {
                comboBoxItemPackages.Enabled = false;
                comboBoxItemStations.Enabled = false;
                comboBoxItemPages.Enabled = false;
            }
        }

        public void UpdateSalesDepot()
        {
            IntPtr handle = this.Handle;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Loading Sales Libraries...";
                form.TopMost = true;
                pnEmpty.BringToFront();
                this.Enabled = false;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    ConfigurationClasses.SettingsManager.Instance.GetSalesDepotName();
                    ConfigurationClasses.SettingsManager.Instance.GetDefaultWizard();
                    ConfigurationClasses.ListManager.Instance.Init();
                    BusinessClasses.LibraryManager.Instance.LoadSalesDepotsPackages(new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.SalesDepotRootFolder));
                    BuildPackageViewers();
                    this.Invoke((MethodInvoker)delegate()
                    {
                        pnMain.Controls.Add(this.ClassicViewControl);
                        pnMain.Controls.Add(this.SolutionViewControl);
                        FillPackages();
                    });
                }));
                thread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                thread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                form.Show();
                Application.DoEvents();
                this.ClassicViewControl = new CustomControls.ClassicViewControl();
                Application.DoEvents();
                this.SolutionViewControl = new CustomControls.SolutionViewControl();
                this.SolutionViewControl.Load += new EventHandler(SolutionViewControl_Load);
                Application.DoEvents();
                thread.Start();
                while (thread.ThreadState == System.Threading.ThreadState.Running)
                    Application.DoEvents();
                this.Enabled = true;
                pnEmpty.SendToBack();
                form.Close();
            }

            UpdateFontButtonStatus();

            AppManager.Instance.ActivatePowerPoint();
            AppManager.Instance.ActivateMainForm();
            ConfigurationClasses.RegistryHelper.SalesDepotHandle = handle;
            ConfigurationClasses.RegistryHelper.MaximizeSalesDepot = true;
        }

        private void UpdateView()
        {
            pnEmpty.BringToFront();
            buttonItemHomeSolutionView.Enabled = !BusinessClasses.LibraryManager.Instance.OldFormatDetected;
            this.ClassicViewControl.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView;
            comboBoxItemPages.Visible = true;
            if (!BusinessClasses.LibraryManager.Instance.OldFormatDetected)
            {
                this.SolutionViewControl.Visible = buttonItemHomeSolutionView.Checked;

                ribbonBarExit.Visible = true;
                ribbonBarHomeHelp.Visible = true;
                ribbonBarViewSettings.Visible = true;
                ribbonBarEmailBin.Visible = true;
                ribbonBarHomeAddSlide.Visible = true;
                ribbonBarHomeSearchMode.Visible = true;
                ribbonBarEmailBin.BringToFront();
                ribbonBarViewSettings.BringToFront();
                ribbonBarHomeSearchMode.BringToFront();
                ribbonBarHomeAddSlide.BringToFront();
                ribbonBarHomeHelp.BringToFront();
                ribbonBarExit.BringToFront();

                ribbonBarHomeHelp.Visible = false;
                ribbonBarExit.Visible = false;
                ribbonBarViewSettings.Visible = false;
                ribbonBarEmailBin.Visible = false;
                ribbonBarHomeAddSlide.Visible = false;
                ribbonBarHomeSearchMode.Visible = false;

                ribbonBarEmailBin.Visible = (ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView) & (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin;
                ribbonBarEmailBin.BringToFront();
                buttonItemEmailBin.Checked = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin ? ConfigurationClasses.SettingsManager.Instance.ShowEmailBin : false;
                ribbonBarViewSettings.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView;
                ribbonBarViewSettings.BringToFront();

                ribbonBarHomeSearchMode.Visible = buttonItemHomeSolutionView.Checked;
                ribbonBarHomeSearchMode.BringToFront();
                ribbonBarHomeAddSlide.Visible = buttonItemHomeSolutionView.Checked;
                ribbonBarHomeAddSlide.BringToFront();

                comboBoxItemStations.Visible = comboBoxItemStations.Items.Count > 1 & (ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView);
                comboBoxItemPages.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView;
                ribbonBarStations.RecalcLayout();

                ribbonBarHomeHelp.Visible = true;
                ribbonBarHomeHelp.BringToFront();
                ribbonBarExit.Visible = true;
                ribbonBarExit.BringToFront();
            }

            if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
                superTooltip.SetSuperTooltip(buttonItemHomeHelp, buttonItemEmailBin.Checked ? _emailToolTip : _classicToolTip);
            else if (ConfigurationClasses.SettingsManager.Instance.ListView)
                superTooltip.SetSuperTooltip(buttonItemHomeHelp, buttonItemEmailBin.Checked ? _emailToolTip : _listToolTip);
            else
            {
                if (buttonItemHomeSearchByFileName.Checked)
                    superTooltip.SetSuperTooltip(buttonItemHomeHelp, _titleToolTip);
                else if (buttonItemHomeSearchByTags.Checked)
                    superTooltip.SetSuperTooltip(buttonItemHomeHelp, _targetToolTip);
                else if (buttonItemHomeSearchRecentFiles.Checked)
                    superTooltip.SetSuperTooltip(buttonItemHomeHelp, _dateToolTip);
            }

            if (this.CurrentPackage != null)
                this.CurrentPackage.UpdateView();
            pnMain.BringToFront();
        }
        #endregion

        #region Classic View Methods
        private void UpdateFontButtonStatus()
        {
            buttonItemLargerText.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize < 20;
            buttonItemSmallerText.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize > 8;
        }
        #endregion
        #endregion

        #region Button's Click Event Handlers
        #region Base View Button's Click Event Handlers
        private void ChangeView_Click(object sender, EventArgs e)
        {
            buttonItemHomeClassicView.Checked = false;
            buttonItemHomeListView.Checked = false;
            buttonItemHomeSolutionView.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void ChangeView_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as DevComponents.DotNetBar.ButtonItem).Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.ClassicView = buttonItemHomeClassicView.Checked;
                ConfigurationClasses.SettingsManager.Instance.ListView = buttonItemHomeListView.Checked;
                ConfigurationClasses.SettingsManager.Instance.SolutionTitleView = buttonItemHomeSearchByFileName.Checked;
                ConfigurationClasses.SettingsManager.Instance.SolutionTagsView = buttonItemHomeSearchByTags.Checked;
                ConfigurationClasses.SettingsManager.Instance.SolutionDateView = buttonItemHomeSearchRecentFiles.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                UpdateView();
            }
        }

        private void buttonItemHomeHelp_Click(object sender, EventArgs e)
        {
            if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
            {
                if (buttonItemEmailBin.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("email");
                else
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("classic");
            }
            else if (ConfigurationClasses.SettingsManager.Instance.ListView)
            {
                if (buttonItemEmailBin.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("email");
                else
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("list");
            }
            else
            {
                if (buttonItemHomeSearchByFileName.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("title");
                else if (buttonItemHomeSearchByTags.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("target");
                else if (buttonItemHomeSearchRecentFiles.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("date");
            }
        }

        private void buttonItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Classic View Button's Click Event Handlers
        private void buttonItemLargerText_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize += 2;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            UpdateFontButtonStatus();
            if (comboBoxItemPackages.SelectedIndex >= 0 && comboBoxItemPackages.SelectedIndex < _packageViewers.Count)
                _packageViewers[comboBoxItemPackages.SelectedIndex].Format();
        }

        private void buttonItemSmallerText_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize -= 2;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            UpdateFontButtonStatus();
            if (comboBoxItemPackages.SelectedIndex >= 0 && comboBoxItemPackages.SelectedIndex < _packageViewers.Count)
                _packageViewers[comboBoxItemPackages.SelectedIndex].Format();
        }

        private void buttonItemEmailBin_CheckedChanged(object sender, EventArgs e)
        {
            this.ClassicViewControl.splitContainerControl.PanelVisibility = buttonItemEmailBin.Checked ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            superTooltip.SetSuperTooltip(buttonItemHomeHelp, buttonItemEmailBin.Checked ? _emailToolTip : (buttonItemHomeClassicView.Checked ? _classicToolTip : _listToolTip));
            ConfigurationClasses.SettingsManager.Instance.ShowEmailBin = buttonItemEmailBin.Checked;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();

        }
        #endregion

        #region Solution View Button's Click Event Handlers
        private void buttonItemHomeSearchMode_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem buttonItem = sender as DevComponents.DotNetBar.ButtonItem;
            if (buttonItem != null)
            {
                buttonItemHomeSearchByTags.Checked = false;
                buttonItemHomeSearchByFileName.Checked = false;
                buttonItemHomeSearchRecentFiles.Checked = false;
                buttonItem.Checked = true;
            }
        }

        private void buttonItemHomeSearchMode_CheckedChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem buttonItem = sender as DevComponents.DotNetBar.ButtonItem;
            if (buttonItem != null)
            {
                if (buttonItem.Checked)
                {
                    this.SolutionViewControl.ClearSolutionControl();
                    if (buttonItem == buttonItemHomeSearchByTags)
                    {
                        this.SolutionViewControl.xtraTabControlSolutionModes.SelectedTabPage = this.SolutionViewControl.xtraTabPageSearchTags;
                        superTooltip.SetSuperTooltip(buttonItemHomeHelp, _targetToolTip);
                    }
                    else if (buttonItem == buttonItemHomeSearchByFileName)
                    {
                        this.SolutionViewControl.xtraTabControlSolutionModes.SelectedTabPage = this.SolutionViewControl.xtraTabPageKeyWords;
                        superTooltip.SetSuperTooltip(buttonItemHomeHelp, _titleToolTip);
                    }
                    else if (buttonItem == buttonItemHomeSearchRecentFiles)
                    {
                        this.SolutionViewControl.xtraTabControlSolutionModes.SelectedTabPage = this.SolutionViewControl.xtraTabPageAddDate;
                        superTooltip.SetSuperTooltip(buttonItemHomeHelp, _dateToolTip);
                    }
                    ConfigurationClasses.SettingsManager.Instance.SolutionTitleView = buttonItemHomeSearchByFileName.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionTagsView = buttonItemHomeSearchByTags.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionDateView = buttonItemHomeSearchRecentFiles.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                }
            }
        }

        private void buttonItemHomeAddSlide_Click(object sender, EventArgs e)
        {
            this.SolutionViewControl.InsertSlide();
        }
        #endregion

        #region Settings Button's Click Event Handlers
        private void buttonItemSettingsLaunchPowerPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.LaunchPPT = buttonItemSettingsLaunchPowerPoint.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.MultitabView = buttonItemSettingsMultitab.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                StationChanged(comboBoxItemStations);
            }
        }

        private void buttonItemSettingsPowerPointSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            buttonItemSettingsPowerPointViewer.Checked = false;
            buttonItemSettingsPowerPointMenu.Checked = false;
            buttonItemSettingsPowerPointLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemSettingsPowerPointSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if(buttonItemSettingsPowerPointViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (buttonItemSettingsPowerPointMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (buttonItemSettingsPowerPointLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsPDFSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            buttonItemSettingsPDFViewer.Checked = false;
            buttonItemSettingsPDFMenu.Checked = false;
            buttonItemSettingsPDFLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemSettingsPDFSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (buttonItemSettingsPDFViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (buttonItemSettingsPDFMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (buttonItemSettingsPDFLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsWordSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            buttonItemSettingsWordViewer.Checked = false;
            buttonItemSettingsWordMenu.Checked = false;
            buttonItemSettingsWordLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemSettingsWordSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (buttonItemSettingsWordViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (buttonItemSettingsWordMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (buttonItemSettingsWordLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsExcelSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            buttonItemSettingsExcelViewer.Checked = false;
            buttonItemSettingsExcelMenu.Checked = false;
            buttonItemSettingsExcelLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemSettingsExcelSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (buttonItemSettingsExcelViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (buttonItemSettingsExcelMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (buttonItemSettingsExcelLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsVideoSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            buttonItemSettingsVideoViewer.Checked = false;
            buttonItemSettingsVideoMenu.Checked = false;
            buttonItemSettingsVideoLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemSettingsVideoSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (buttonItemSettingsVideoViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (buttonItemSettingsVideoMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (buttonItemSettingsVideoLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsQuickView_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            buttonItemSettingsQuickViewImages.Checked = false;
            buttonItemSettingsQuickViewSlides.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void buttonItemSettingsQuickViewSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView = buttonItemSettingsQuickViewSlides.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void buttonItemSettingsEmail_Click(object sender, EventArgs e)
        {
            using (ConfigurationClasses.FormEmailSettings form = new ConfigurationClasses.FormEmailSettings())
            {
                form.ShowDialog();
                this.ClassicViewControl.LoadOptions();
            }
            UpdateView();
        }

        private void buttonItemSettingsHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("settings");
        }
        #endregion
        #endregion

        #region Other GUI Event Handlers
        #region Form Event Handlers
        private void FormMain_Load(object sender, EventArgs e)
        {
            ActivityRecorder.Instance.StartRecording();
            SDRecorder.Instance.StartRecording();
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.IconPath))
                this.Icon = new Icon(ConfigurationClasses.SettingsManager.Instance.IconPath);
            ribbonBarHomeClassicView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.ClassicTitle) ? ConfigurationClasses.SettingsManager.Instance.ClassicTitle : ribbonBarHomeClassicView.Text;
            ribbonBarHomeListView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.ListTitle) ? ConfigurationClasses.SettingsManager.Instance.ListTitle : ribbonBarHomeListView.Text;
            ribbonBarHomeSolutionView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SolutionTitle) ? ConfigurationClasses.SettingsManager.Instance.SolutionTitle : ribbonBarHomeSolutionView.Text;

            _allowToSave = false;
            buttonItemSettingsLaunchPowerPoint.Checked = ConfigurationClasses.SettingsManager.Instance.LaunchPPT;
            buttonItemSettingsMultitab.Checked = ConfigurationClasses.SettingsManager.Instance.MultitabView;
            switch (ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions)
            { 
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    buttonItemSettingsPowerPointViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    buttonItemSettingsPowerPointMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    buttonItemSettingsPowerPointLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    buttonItemSettingsPDFViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    buttonItemSettingsPDFMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    buttonItemSettingsPDFLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    buttonItemSettingsWordViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    buttonItemSettingsWordMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    buttonItemSettingsWordLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    buttonItemSettingsExcelViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    buttonItemSettingsExcelMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    buttonItemSettingsExcelLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    buttonItemSettingsVideoViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    buttonItemSettingsVideoMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    buttonItemSettingsVideoLaunch.Checked = true;
                    break;
            }
            buttonItemSettingsQuickViewImages.Checked = !ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView;
            buttonItemSettingsQuickViewSlides.Checked = ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView;
            _allowToSave = true;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            UpdateSalesDepot();
            buttonItemHomeSolutionView.Enabled = !BusinessClasses.LibraryManager.Instance.OldFormatDetected;
            buttonItemHomeClassicView.Checked = ConfigurationClasses.SettingsManager.Instance.ClassicView;
            buttonItemHomeListView.Checked = ConfigurationClasses.SettingsManager.Instance.ListView;
            buttonItemHomeSolutionView.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionView;
            if (ConfigurationClasses.SettingsManager.Instance.SolutionView)
            {
                buttonItemHomeSearchByFileName.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionTitleView;
                buttonItemHomeSearchByTags.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionTagsView;
                buttonItemHomeSearchRecentFiles.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionDateView;
            }
            else
                buttonItemHomeSearchByTags.Checked = true;
            UpdateView();
            buttonItemHomeClassicView.CheckedChanged += new EventHandler(ChangeView_CheckedChanged);
            buttonItemHomeSolutionView.CheckedChanged += new EventHandler(ChangeView_CheckedChanged);
            buttonItemHomeListView.CheckedChanged += new EventHandler(ChangeView_CheckedChanged);
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

        #region Comboboxes Event Handlers
        private void comboBoxItemPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemPackages.SelectedIndex >= 0 && comboBoxItemPackages.SelectedIndex < _packageViewers.Count)
            {
                this.SolutionViewControl.ClearSolutionControl();
                ConfigurationClasses.SettingsManager.Instance.SelectedPackage = comboBoxItemPackages.SelectedItem.ToString();
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                this.CurrentPackage = _packageViewers[comboBoxItemPackages.SelectedIndex];
                this.CurrentPackage.Apply();
                this.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
                ribbonBarStations.Text = _packageViewers[comboBoxItemPackages.SelectedIndex].Name;
                ribbonBarStations.RecalcLayout();
                ribbonPanelHome.PerformLayout();
            }
            else
                this.CurrentPackage = null;
        }

        private void comboBoxItemStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            StationChanged(sender);
        }

        private void comboBoxItemPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageChanged(sender);
        }
        #endregion

        #region Solution View Event Handlers
        private void SolutionViewControl_Load(object sender, EventArgs e)
        {
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 0)
            {
                this.SolutionViewControl.navBarGroup1.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0];
                this.SolutionViewControl.navBarGroup1.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Description;
                this.SolutionViewControl.navBarGroup1.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup1.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup1.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 1)
            {
                this.SolutionViewControl.navBarGroup2.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1];
                this.SolutionViewControl.navBarGroup2.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Description;
                this.SolutionViewControl.navBarGroup2.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup2.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup2.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 2)
            {
                this.SolutionViewControl.navBarGroup3.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2];
                this.SolutionViewControl.navBarGroup3.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Description;
                this.SolutionViewControl.navBarGroup3.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup3.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup3.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 3)
            {
                this.SolutionViewControl.navBarGroup4.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3];
                this.SolutionViewControl.navBarGroup4.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Description;
                this.SolutionViewControl.navBarGroup4.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup4.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup4.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 4)
            {
                this.SolutionViewControl.navBarGroup5.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4];
                this.SolutionViewControl.navBarGroup5.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Description;
                this.SolutionViewControl.navBarGroup5.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup5.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup5.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 5)
            {
                this.SolutionViewControl.navBarGroup6.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5];
                this.SolutionViewControl.navBarGroup6.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Description;
                this.SolutionViewControl.navBarGroup6.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup6.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup6.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 6)
            {
                this.SolutionViewControl.navBarGroup7.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6];
                this.SolutionViewControl.navBarGroup7.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Description;
                this.SolutionViewControl.navBarGroup7.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Logo;
                this.SolutionViewControl.checkedListBoxControlGroup7.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Tags.ToArray());
            }
            else
                this.SolutionViewControl.navBarGroup7.Visible = false;
        }
        #endregion
        #endregion
    }
}
