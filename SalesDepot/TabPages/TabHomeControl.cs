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

        public PresentationClasses.WallBin.IWallBinView SelectedView { get; private set; }
        public PresentationClasses.WallBin.ClassicViewControl ClassicViewControl { get; private set; }
        public PresentationClasses.WallBin.SolutionViewControl SolutionViewControl { get; private set; }

        public TabHomeControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.ClassicViewControl = new PresentationClasses.WallBin.ClassicViewControl();
            this.SolutionViewControl = new PresentationClasses.WallBin.SolutionViewControl();
        }

        #region Methods
        public void LoadPage()
        {
            _allowToSave = false;
            LoadWallBinSettings();
            LoadPackages();
            this.ClassicViewControl.UpdateFontButtonStatus();
            ApplySelectedView();
            ApplySelectedDecorator();
            _allowToSave = true;
        }

        private void LoadWallBinSettings()
        {
            #region Wall Bin Last Saved State
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
            else if (ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection)
                FormMain.Instance.buttonItemHomeSearchByFileName.Checked = true;
            else
                FormMain.Instance.buttonItemHomeSearchByTags.Checked = true;

            if (ConfigurationClasses.SettingsManager.Instance.ClassicView || ConfigurationClasses.SettingsManager.Instance.ListView || BusinessClasses.LibraryManager.Instance.OldFormatDetected)
                this.SelectedView = this.ClassicViewControl;
            else if (ConfigurationClasses.SettingsManager.Instance.SolutionView)
                this.SelectedView = this.SolutionViewControl;
            #endregion

            #region Wall Bin Configuration
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
            #endregion
        }

        private void LoadPackages()
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
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];
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
            pnEmpty.BringToFront();
            Application.DoEvents();
            this.SelectedView.ApplyView();
            if (!pnMain.Controls.Contains(this.SelectedView as Control))
                pnMain.Controls.Add(this.SelectedView as Control);
            (this.SelectedView as Control).BringToFront();
            Application.DoEvents();
            pnMain.BringToFront();
            Application.DoEvents();
        }

        private void ApplySelectedDecorator()
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer != null)
            {
                FormMain.Instance.ribbonBarStations.Text = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Name;
                FormMain.Instance.ribbonBarStations.RecalcLayout();
                FormMain.Instance.ribbonPanelHome.PerformLayout();

                pnEmpty.BringToFront();
                Application.DoEvents();
                if (!this.ClassicViewControl.pnRemoteLibraryContainer.Controls.Contains(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Container))
                    this.ClassicViewControl.pnRemoteLibraryContainer.Controls.Add(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Container);
                pnEmpty.BringToFront();
                Application.DoEvents();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Apply();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Container.BringToFront();
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
            FormMain.Instance.buttonItemHomeSolutionView.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void ChangeView_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if ((sender as DevComponents.DotNetBar.ButtonItem).Checked)
                {
                    ConfigurationClasses.SettingsManager.Instance.ClassicView = FormMain.Instance.buttonItemHomeClassicView.Checked;
                    ConfigurationClasses.SettingsManager.Instance.ListView = FormMain.Instance.buttonItemHomeListView.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionTitleView = FormMain.Instance.buttonItemHomeSearchByFileName.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionTagsView = FormMain.Instance.buttonItemHomeSearchByTags.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionDateView = FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();

                    if (ConfigurationClasses.SettingsManager.Instance.ClassicView || ConfigurationClasses.SettingsManager.Instance.ListView || BusinessClasses.LibraryManager.Instance.OldFormatDetected)
                        this.SelectedView = this.ClassicViewControl;
                    else if (ConfigurationClasses.SettingsManager.Instance.SolutionView)
                        this.SelectedView = this.SolutionViewControl;

                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Loading Page...";
                        form.TopMost = true;
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            FormMain.Instance.Invoke((MethodInvoker)delegate()
                            {
                                ApplySelectedView();
                            });
                        }));
                        form.Show();
                        Application.DoEvents();
                        thread.Start();
                        while (thread.IsAlive)
                            Application.DoEvents();
                        form.Close();
                    }
                }
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
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Loading Page...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        ApplySelectedView();
                    });
                }));
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
            BusinessClasses.HelpManager.Instance.OpenHelpLink("settings");
        }
        #endregion
        #endregion

        #region Comboboxes Event Handlers
        public void comboBoxItemPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers.Count)
                {
                    ConfigurationClasses.SettingsManager.Instance.SelectedPackage = FormMain.Instance.comboBoxItemPackages.SelectedItem.ToString();
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];
                    ApplySelectedDecorator();
                    this.SolutionViewControl.ClearSolutionControl();
                }
                else
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer = null;
            }
        }

        public void comboBoxItemStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = string.Format("Loading {0}...", PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer != null ? PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Name : "Library");
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
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
                }));
                form.Show();
                Application.DoEvents();
                thread.Start();
                while (thread.IsAlive)
                    Application.DoEvents();
                form.Close();
            }
        }

        public void comboBoxItemPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Loading Page...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
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
                }));
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
