using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabHomeControl : UserControl
    {
        private bool _allowToSave = false;

        public AppManager.SingleParamDelegate StationChanged;
        public AppManager.SingleParamDelegate PageChanged;

        public PresentationClasses.WallBin.ClassicViewControl ClassicViewControl { get; private set; }
        private DevComponents.DotNetBar.SuperTooltipInfo _classicToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Column View", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _listToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Sales Library List View", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _emailToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about how to EMAIL files from this Sales Library", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        public PresentationClasses.WallBin.SolutionViewControl SolutionViewControl { get; private set; }
        private DevComponents.DotNetBar.SuperTooltipInfo _targetToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by qualified target criteria", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _titleToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by title or file name", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _dateToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by date range", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        public TabHomeControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        #region Methods
        public void InitPage()
        {
            FormMain.Instance.ribbonBarHomeClassicView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.ClassicTitle) ? ConfigurationClasses.SettingsManager.Instance.ClassicTitle :FormMain.Instance.ribbonBarHomeClassicView.Text;
            FormMain.Instance.ribbonBarHomeListView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.ListTitle) ? ConfigurationClasses.SettingsManager.Instance.ListTitle : FormMain.Instance.ribbonBarHomeListView.Text;
            FormMain.Instance.ribbonBarHomeSolutionView.Text = !string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SolutionTitle) ? ConfigurationClasses.SettingsManager.Instance.SolutionTitle : FormMain.Instance.ribbonBarHomeSolutionView.Text;

            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsLaunchPowerPoint.Checked = ConfigurationClasses.SettingsManager.Instance.LaunchPPT;
            FormMain.Instance.buttonItemSettingsMultitab.Checked = ConfigurationClasses.SettingsManager.Instance.MultitabView;
            switch (ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    FormMain.Instance.buttonItemSettingsPDFViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    FormMain.Instance.buttonItemSettingsPDFMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    FormMain.Instance.buttonItemSettingsPDFLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    FormMain.Instance.buttonItemSettingsWordViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    FormMain.Instance.buttonItemSettingsWordMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    FormMain.Instance.buttonItemSettingsWordLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    FormMain.Instance.buttonItemSettingsExcelViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    FormMain.Instance.buttonItemSettingsExcelMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    FormMain.Instance.buttonItemSettingsExcelLaunch.Checked = true;
                    break;
            }
            switch (ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions)
            {
                case ConfigurationClasses.LinkLaunchOptions.Viewer:
                    FormMain.Instance.buttonItemSettingsVideoViewer.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Menu:
                    FormMain.Instance.buttonItemSettingsVideoMenu.Checked = true;
                    break;
                case ConfigurationClasses.LinkLaunchOptions.Launch:
                    FormMain.Instance.buttonItemSettingsVideoLaunch.Checked = true;
                    break;
            }
            FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = !ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView;
            FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView;
            _allowToSave = true;

            UpdateSalesDepot();
            FormMain.Instance.buttonItemHomeSolutionView.Enabled = !BusinessClasses.LibraryManager.Instance.OldFormatDetected;
            FormMain.Instance.buttonItemHomeClassicView.Checked = ConfigurationClasses.SettingsManager.Instance.ClassicView;
            FormMain.Instance.buttonItemHomeListView.Checked = ConfigurationClasses.SettingsManager.Instance.ListView;
            FormMain.Instance.buttonItemHomeSolutionView.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionView;
            if (ConfigurationClasses.SettingsManager.Instance.SolutionView)
            {
                FormMain.Instance.buttonItemHomeSearchByFileName.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionTitleView;
                FormMain.Instance.buttonItemHomeSearchByTags.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionTagsView;
                FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked = ConfigurationClasses.SettingsManager.Instance.SolutionDateView;
            }
            else
                FormMain.Instance.buttonItemHomeSearchByTags.Checked = true;
            UpdateView();
            FormMain.Instance.buttonItemHomeClassicView.CheckedChanged += new EventHandler(ChangeView_CheckedChanged);
            FormMain.Instance.buttonItemHomeSolutionView.CheckedChanged += new EventHandler(ChangeView_CheckedChanged);
            FormMain.Instance.buttonItemHomeListView.CheckedChanged += new EventHandler(ChangeView_CheckedChanged);
        }

        #region Load Sales Depot Methods
        private void FillPackages()
        {
            FormMain.Instance.comboBoxItemPackages.Items.Clear();
            foreach (BusinessClasses.LibraryPackage salesDepot in BusinessClasses.LibraryManager.Instance.LibraryPackageCollection)
            {
                FormMain.Instance.comboBoxItemPackages.Items.Add(salesDepot.Name);
                DevComponents.DotNetBar.LabelItem packageLable = new DevComponents.DotNetBar.LabelItem();
                packageLable.Text = salesDepot.Name;
                packageLable.TextAlignment = StringAlignment.Near;
                while (packageLable.Text.Length < 20)
                    packageLable.Text += " ";
            }

            FormMain.Instance.comboBoxItemPackages.Enabled = true;
            FormMain.Instance.comboBoxItemStations.Enabled = true;
            FormMain.Instance.comboBoxItemPages.Enabled = !ConfigurationClasses.SettingsManager.Instance.MultitabView;

            if (FormMain.Instance.comboBoxItemPackages.Items.Count > 0)
            {
                int previousSelectedPackageIndex = FormMain.Instance.comboBoxItemPackages.Items.IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedPackage);
                if (previousSelectedPackageIndex >= 0)
                    FormMain.Instance.comboBoxItemPackages.SelectedIndex = previousSelectedPackageIndex;
                else
                    FormMain.Instance.comboBoxItemPackages.SelectedIndex = 0;
                FormMain.Instance.comboBoxItemPackages.Enabled = FormMain.Instance.comboBoxItemPackages.Items.Count > 1;
            }
            else
            {
                FormMain.Instance.comboBoxItemPackages.Enabled = false;
                FormMain.Instance.comboBoxItemStations.Enabled = false;
                FormMain.Instance.comboBoxItemPages.Enabled = false;
            }
        }

        public void UpdateSalesDepot()
        {
            IntPtr handle = this.Handle;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Loading Sales Libraries...";
                form.TopMost = true;
                //pnEmpty.BringToFront();
                //this.Enabled = false;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    ConfigurationClasses.SettingsManager.Instance.GetSalesDepotName();
                    ConfigurationClasses.SettingsManager.Instance.GetDefaultWizard();
                    ConfigurationClasses.ListManager.Instance.Init();
                    BusinessClasses.LibraryManager.Instance.LoadSalesDepotsPackages(new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.SalesDepotRootFolder));
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildPackageViewers();
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.BuildOvernightsCalendars();
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
                this.ClassicViewControl = new PresentationClasses.WallBin.ClassicViewControl();
                Application.DoEvents();
                this.SolutionViewControl = new PresentationClasses.WallBin.SolutionViewControl();
                Application.DoEvents();
                thread.Start();
                while (thread.ThreadState == System.Threading.ThreadState.Running)
                    Application.DoEvents();
                //this.Enabled = true;
                //pnEmpty.SendToBack();
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
            FormMain.Instance.buttonItemHomeSolutionView.Enabled = !BusinessClasses.LibraryManager.Instance.OldFormatDetected;
            this.ClassicViewControl.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView;
            FormMain.Instance.comboBoxItemPages.Visible = true;
            if (!BusinessClasses.LibraryManager.Instance.OldFormatDetected)
            {
                this.SolutionViewControl.Visible = FormMain.Instance.buttonItemHomeSolutionView.Checked;

                FormMain.Instance.ribbonBarExit.Visible = true;
                FormMain.Instance.ribbonBarHomeHelp.Visible = true;
                FormMain.Instance.ribbonBarViewSettings.Visible = true;
                FormMain.Instance.ribbonBarEmailBin.Visible = true;
                FormMain.Instance.ribbonBarHomeAddSlide.Visible = true;
                FormMain.Instance.ribbonBarHomeSearchMode.Visible = true;
                FormMain.Instance.ribbonBarEmailBin.BringToFront();
                FormMain.Instance.ribbonBarViewSettings.BringToFront();
                FormMain.Instance.ribbonBarHomeSearchMode.BringToFront();
                FormMain.Instance.ribbonBarHomeAddSlide.BringToFront();
                FormMain.Instance.ribbonBarHomeHelp.BringToFront();
                FormMain.Instance.ribbonBarExit.BringToFront();

                FormMain.Instance.ribbonBarHomeHelp.Visible = false;
                FormMain.Instance.ribbonBarExit.Visible = false;
                FormMain.Instance.ribbonBarViewSettings.Visible = false;
                FormMain.Instance.ribbonBarEmailBin.Visible = false;
                FormMain.Instance.ribbonBarHomeAddSlide.Visible = false;
                FormMain.Instance.ribbonBarHomeSearchMode.Visible = false;

                FormMain.Instance.ribbonBarEmailBin.Visible = (ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView) & (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin;
                FormMain.Instance.ribbonBarEmailBin.BringToFront();
                FormMain.Instance.buttonItemEmailBin.Checked = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin ? ConfigurationClasses.SettingsManager.Instance.ShowEmailBin : false;
                FormMain.Instance.ribbonBarViewSettings.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView;
                FormMain.Instance.ribbonBarViewSettings.BringToFront();

                FormMain.Instance.ribbonBarHomeSearchMode.Visible = FormMain.Instance.buttonItemHomeSolutionView.Checked;
                FormMain.Instance.ribbonBarHomeSearchMode.BringToFront();
                FormMain.Instance.ribbonBarHomeAddSlide.Visible = FormMain.Instance.buttonItemHomeSolutionView.Checked;
                FormMain.Instance.ribbonBarHomeAddSlide.BringToFront();

                FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1 & (ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView);
                FormMain.Instance.comboBoxItemPages.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView | ConfigurationClasses.SettingsManager.Instance.ListView;
                FormMain.Instance.ribbonBarStations.RecalcLayout();

                FormMain.Instance.ribbonBarHomeHelp.Visible = true;
                FormMain.Instance.ribbonBarHomeHelp.BringToFront();
                FormMain.Instance.ribbonBarExit.Visible = true;
                FormMain.Instance.ribbonBarExit.BringToFront();
            }

            if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _classicToolTip);
            else if (ConfigurationClasses.SettingsManager.Instance.ListView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _listToolTip);
            else
            {
                if (FormMain.Instance.buttonItemHomeSearchByFileName.Checked)
                    FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _titleToolTip);
                else if (FormMain.Instance.buttonItemHomeSearchByTags.Checked)
                    FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetToolTip);
                else if (FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked)
                    FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _dateToolTip);
            }

            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.UpdateView();
            pnMain.BringToFront();
        }
        #endregion

        #region Classic View Methods
        private void UpdateFontButtonStatus()
        {
            FormMain.Instance.buttonItemLargerText.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize < 20;
            FormMain.Instance.buttonItemSmallerText.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize > 8;
        }
        #endregion
        #endregion

        #region Button's Click Event Handlers
        #region Base View Button's Click Event Handlers
        public void ChangeView_Click(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemHomeClassicView.Checked = false;
            FormMain.Instance.buttonItemHomeListView.Checked = false;
            FormMain.Instance.buttonItemHomeSolutionView.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        private void ChangeView_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as DevComponents.DotNetBar.ButtonItem).Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.ClassicView = FormMain.Instance.buttonItemHomeClassicView.Checked;
                ConfigurationClasses.SettingsManager.Instance.ListView = FormMain.Instance.buttonItemHomeListView.Checked;
                ConfigurationClasses.SettingsManager.Instance.SolutionTitleView = FormMain.Instance.buttonItemHomeSearchByFileName.Checked;
                ConfigurationClasses.SettingsManager.Instance.SolutionTagsView = FormMain.Instance.buttonItemHomeSearchByTags.Checked;
                ConfigurationClasses.SettingsManager.Instance.SolutionDateView = FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                UpdateView();
            }
        }

        public void buttonItemHomeHelp_Click(object sender, EventArgs e)
        {
            if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
            {
                if (FormMain.Instance.buttonItemEmailBin.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("email");
                else
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("classic");
            }
            else if (ConfigurationClasses.SettingsManager.Instance.ListView)
            {
                if (FormMain.Instance.buttonItemEmailBin.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("email");
                else
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("list");
            }
            else
            {
                if (FormMain.Instance.buttonItemHomeSearchByFileName.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("title");
                else if (FormMain.Instance.buttonItemHomeSearchByTags.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("target");
                else if (FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked)
                    BusinessClasses.HelpManager.Instance.OpenHelpLink("date");
            }
        }
        #endregion

        #region Classic View Button's Click Event Handlers
        public void buttonItemLargerText_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize += 2;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            UpdateFontButtonStatus();
            if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers.Count)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
        }

        public void buttonItemSmallerText_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize -= 2;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            UpdateFontButtonStatus();
            if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers.Count)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
        }

        public void buttonItemEmailBin_CheckedChanged(object sender, EventArgs e)
        {
            this.ClassicViewControl.splitContainerControl.PanelVisibility = FormMain.Instance.buttonItemEmailBin.Checked ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : (FormMain.Instance.buttonItemHomeClassicView.Checked ? _classicToolTip : _listToolTip));
            ConfigurationClasses.SettingsManager.Instance.ShowEmailBin = FormMain.Instance.buttonItemEmailBin.Checked;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();

        }
        #endregion

        #region Solution View Button's Click Event Handlers
        public void buttonItemHomeSearchMode_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem buttonItem = sender as DevComponents.DotNetBar.ButtonItem;
            if (buttonItem != null)
            {
                FormMain.Instance.buttonItemHomeSearchByTags.Checked = false;
                FormMain.Instance.buttonItemHomeSearchByFileName.Checked = false;
                FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked = false;
                buttonItem.Checked = true;
            }
        }

        public void buttonItemHomeSearchMode_CheckedChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem buttonItem = sender as DevComponents.DotNetBar.ButtonItem;
            if (buttonItem != null)
            {
                if (buttonItem.Checked)
                {
                    this.SolutionViewControl.ClearSolutionControl();
                    if (buttonItem == FormMain.Instance.buttonItemHomeSearchByTags)
                    {
                        this.SolutionViewControl.xtraTabControlSolutionModes.SelectedTabPage = this.SolutionViewControl.xtraTabPageSearchTags;
                        FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetToolTip);
                    }
                    else if (buttonItem == FormMain.Instance.buttonItemHomeSearchByFileName)
                    {
                        this.SolutionViewControl.xtraTabControlSolutionModes.SelectedTabPage = this.SolutionViewControl.xtraTabPageKeyWords;
                        FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _titleToolTip);
                    }
                    else if (buttonItem == FormMain.Instance.buttonItemHomeSearchRecentFiles)
                    {
                        this.SolutionViewControl.xtraTabControlSolutionModes.SelectedTabPage = this.SolutionViewControl.xtraTabPageAddDate;
                        FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _dateToolTip);
                    }
                    ConfigurationClasses.SettingsManager.Instance.SolutionTitleView = FormMain.Instance.buttonItemHomeSearchByFileName.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionTagsView = FormMain.Instance.buttonItemHomeSearchByTags.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionDateView = FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                }
            }
        }

        public void buttonItemHomeAddSlide_Click(object sender, EventArgs e)
        {
            this.SolutionViewControl.InsertSlide();
        }
        #endregion

        #region Settings Button's Click Event Handlers
        public void buttonItemSettingsLaunchPowerPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.LaunchPPT = FormMain.Instance.buttonItemSettingsLaunchPowerPoint.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.MultitabView = FormMain.Instance.buttonItemSettingsMultitab.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                StationChanged(FormMain.Instance.comboBoxItemStations);
            }
        }

        public void buttonItemSettingsPowerPointSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked = false;
            FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked = false;
            FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemSettingsPowerPointSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PowerPointLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsPDFSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsPDFViewer.Checked = false;
            FormMain.Instance.buttonItemSettingsPDFMenu.Checked = false;
            FormMain.Instance.buttonItemSettingsPDFLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemSettingsPDFSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (FormMain.Instance.buttonItemSettingsPDFViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (FormMain.Instance.buttonItemSettingsPDFMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (FormMain.Instance.buttonItemSettingsPDFLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.PDFLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsWordSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsWordViewer.Checked = false;
            FormMain.Instance.buttonItemSettingsWordMenu.Checked = false;
            FormMain.Instance.buttonItemSettingsWordLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemSettingsWordSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (FormMain.Instance.buttonItemSettingsWordViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (FormMain.Instance.buttonItemSettingsWordMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (FormMain.Instance.buttonItemSettingsWordLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.WordLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsExcelSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsExcelViewer.Checked = false;
            FormMain.Instance.buttonItemSettingsExcelMenu.Checked = false;
            FormMain.Instance.buttonItemSettingsExcelLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemSettingsExcelSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (FormMain.Instance.buttonItemSettingsExcelViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (FormMain.Instance.buttonItemSettingsExcelMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (FormMain.Instance.buttonItemSettingsExcelLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ExcelLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsVideoSettings_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsVideoViewer.Checked = false;
            FormMain.Instance.buttonItemSettingsVideoMenu.Checked = false;
            FormMain.Instance.buttonItemSettingsVideoLaunch.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemSettingsVideoSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (FormMain.Instance.buttonItemSettingsVideoViewer.Checked)
                    ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Viewer;
                else if (FormMain.Instance.buttonItemSettingsVideoMenu.Checked)
                    ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Menu;
                else if (FormMain.Instance.buttonItemSettingsVideoLaunch.Checked)
                    ConfigurationClasses.SettingsManager.Instance.VideoLaunchOptions = ConfigurationClasses.LinkLaunchOptions.Launch;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsQuickView_Click(object sender, EventArgs e)
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = false;
            FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = false;
            _allowToSave = true;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemSettingsQuickViewSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.OldStyleQuickView = FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemSettingsEmail_Click(object sender, EventArgs e)
        {
            using (ToolForms.Settings.FormEmailSettings form = new ToolForms.Settings.FormEmailSettings())
            {
                form.ShowDialog();
                this.ClassicViewControl.LoadOptions();
            }
            UpdateView();
        }

        public void buttonItemSettingsHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("settings");
        }
        #endregion
        #endregion

        #region Comboboxes Event Handlers
        public void comboBoxItemPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers.Count)
            {
                this.SolutionViewControl.ClearSolutionControl();
                ConfigurationClasses.SettingsManager.Instance.SelectedPackage = FormMain.Instance.comboBoxItemPackages.SelectedItem.ToString();
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Apply();
                this.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
                FormMain.Instance.ribbonBarStations.Text = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].Name;
                FormMain.Instance.ribbonBarStations.RecalcLayout();
                FormMain.Instance.ribbonPanelHome.PerformLayout();
            }
            else
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer = null;
        }

        public void comboBoxItemStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            StationChanged(sender);
        }

        public void comboBoxItemPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageChanged(sender);
        }
        #endregion
    }
}
