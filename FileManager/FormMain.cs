using System;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;

        public TabPages.TabHomeControl TabHome { get; set; }
        public TabPages.TabClipartControl TabClipart { get; set; }
        public TabPages.TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }
        public TabPages.TabIPadManagerControl TabIPadManager { get; set; }
        private Control _currentTab = null;

        private FormMain()
        {
            InitializeComponent();
            this.TabHome = new TabPages.TabHomeControl();
            this.FormClosing += new FormClosingEventHandler(this.TabHome.FormClosing);
            comboBoxEditLibraries.EditValueChanged += new EventHandler(this.TabHome.comboBoxEditLibraries_EditValueChanged);
            comboBoxEditLibraries.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.TabHome.comboBoxEditLibraries_EditValueChanging);
            comboBoxEditPages.SelectedIndexChanged += new EventHandler(this.TabHome.comboBoxEditPages_SelectedIndexChanged);
            buttonItemHomeFileTreeView.CheckedChanged += new EventHandler(this.TabHome.buttonItemHomeFileTreeView_CheckedChanged);
            buttonItemSettingsPaths.Click += new EventHandler(this.TabHome.btPathSettings_Click);
            buttonItemSettingsExtraRoots.Click += new EventHandler(this.TabHome.btExtraRoot_Click);
            buttonItemSettingsBranding.Click += new EventHandler(this.TabHome.buttonItemSettingsBranding_Click);
            buttonItemSettingsSync.Click += new EventHandler(this.TabHome.buttonItemSettingsSync_Click);
            buttonItemSettingsPages.Click += new EventHandler(this.TabHome.buttonItemSettingsPages_Click);
            buttonItemSettingsColumns.Click += new EventHandler(this.TabHome.buttonItemSettingsColumns_Click);
            buttonItemSettingsAutoWidgets.Click += new EventHandler(this.TabHome.buttonItemSettingsAutoWidgets_Click);
            buttonItemSettingsDeadLinks.Click += new EventHandler(this.TabHome.buttonItemSettingsDeadLinks_Click);
            buttonItemSettingsEmailList.Click += new EventHandler(this.TabHome.buttonItemSettingsEmailList_Click);
            buttonItemSettingsAutoSync.Click += new EventHandler(this.TabHome.buttonItemSettingsAutoSync_Click);
            buttonItemHomeAddLineBreak.Click += new EventHandler(this.TabHome.btLineBreak_Click);
            buttonItemHomeAddNetworkShare.Click += new EventHandler(this.TabHome.btAddNeworkShare_Click);
            buttonItemHomeAddUrl.Click += new EventHandler(this.TabHome.btAddUrl_Click);
            buttonItemHomeFontDown.Click += new EventHandler(this.TabHome.btFontDown_Click);
            buttonItemHomeFontUp.Click += new EventHandler(this.TabHome.btFontUp_Click);
            buttonItemHomeNudgeDown.Click += new EventHandler(this.TabHome.btDownLink_Click);
            buttonItemHomeNudgeUp.Click += new EventHandler(this.TabHome.btUpLink_Click);
            buttonItemHomeDelete.Click += new EventHandler(this.TabHome.btDeleteLink_Click);
            buttonItemHomeOpen.Click += new EventHandler(this.TabHome.btOpenLink_Click);
            buttonItemHomeProperties.Click += new EventHandler(this.TabHome.buttonItemHomeProperties_Click);
            buttonItemHomeSave.Click += new EventHandler(this.TabHome.btSave_Click);
            buttonItemHomeSync.Click += new EventHandler(this.TabHome.btSync_Click);
            buttonItemHomeExit.Click += new EventHandler(this.TabHome.btExit_Click);

            buttonItemProgramManagerSyncDisabled.Click += new EventHandler(this.TabHome.buttonItemProgramManagerSync_Click);
            buttonItemProgramManagerSyncEnabled.Click += new EventHandler(this.TabHome.buttonItemProgramManagerSync_Click);
            buttonItemProgramManagerSyncDisabled.CheckedChanged += new EventHandler(this.TabHome.buttonItemProgramManagerSync_CheckedChanged);
            buttonItemProgramManagerSyncEnabled.CheckedChanged += new EventHandler(this.TabHome.buttonItemProgramManagerSync_CheckedChanged);
            buttonEditProgramManagerLocation.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TabHome.buttonEditProgramManagerLocation_ButtonClick);

            this.TabClipart = new TabPages.TabClipartControl();
            buttonItemClipartClientLogos.Click += new EventHandler(this.TabClipart.buttonItemClipart_Click);
            buttonItemClipartSalesGallery.Click += new EventHandler(this.TabClipart.buttonItemClipart_Click);
            buttonItemClipartWebArt.Click += new EventHandler(this.TabClipart.buttonItemClipart_Click);
            buttonItemClipartClientLogos.CheckedChanged += new EventHandler(this.TabClipart.buttonItemClipart_CheckedChanged);
            buttonItemClipartSalesGallery.CheckedChanged += new EventHandler(this.TabClipart.buttonItemClipart_CheckedChanged);
            buttonItemClipartWebArt.CheckedChanged += new EventHandler(this.TabClipart.buttonItemClipart_CheckedChanged);

            this.TabOvernightsCalendar = new TabPages.TabOvernightsCalendarControl();
            buttonItemCalendarSyncStatusDisabled.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarSyncStatus_Click);
            buttonItemCalendarSyncStatusEnabled.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarSyncStatus_Click);
            buttonItemCalendarSyncStatusDisabled.CheckedChanged += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarSyncStatus_CheckedChanged);
            buttonItemCalendarSyncStatusEnabled.CheckedChanged += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarSyncStatus_CheckedChanged);
            buttonEditCalendarLocation.EditValueChanged += new EventHandler(this.TabOvernightsCalendar.buttonEditCalendarLocation_EditValueChanged);
            buttonEditCalendarLocation.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TabOvernightsCalendar.buttonEditCalendarLocation_ButtonClick);
            buttonItemCalendarSettings.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarSettings_Click);
            buttonItemCalendarFontUp.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFontUp_Click);
            buttonItemCalendarFontDown.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFontDown_Click);
            buttonItemCalendarEmailGrabber.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarEmailGrabber_Click);
            buttonItemCalendarFileGrabber.Click += new EventHandler(this.TabOvernightsCalendar.buttonItemCalendarFileGrabber_Click);

            this.TabIPadManager = new TabPages.TabIPadManagerControl();
            buttonEditIPadLocation.EditValueChanged += new EventHandler(this.TabIPadManager.buttonEditIPadLocation_EditValueChanged);
            buttonEditIPadLocation.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TabIPadManager.buttonEditIPadLocation_ButtonClick);
            buttonEditIPadLocation.Enter+=new EventHandler(Editor_Enter);
            buttonEditIPadLocation.MouseUp +=new MouseEventHandler(Editor_MouseUp);
            buttonEditIPadLocation.MouseDown += new MouseEventHandler(Editor_MouseDown);
            buttonEditIPadSite.EditValueChanged += new EventHandler(this.TabIPadManager.buttonEditIPadSite_EditValueChanged);
            buttonEditIPadSite.Enter += new EventHandler(Editor_Enter);
            buttonEditIPadSite.MouseUp += new MouseEventHandler(Editor_MouseUp);
            buttonEditIPadSite.MouseDown += new MouseEventHandler(Editor_MouseDown);
            buttonEditIPadLogin.EditValueChanged += new EventHandler(this.TabIPadManager.buttonEditIPadSite_EditValueChanged);
            buttonEditIPadLogin.Enter += new EventHandler(Editor_Enter);
            buttonEditIPadLogin.MouseUp += new MouseEventHandler(Editor_MouseUp);
            buttonEditIPadLogin.MouseDown += new MouseEventHandler(Editor_MouseDown);
            buttonEditIPadPassword.EditValueChanged += new EventHandler(this.TabIPadManager.buttonEditIPadSite_EditValueChanged);
            buttonEditIPadPassword.Enter += new EventHandler(Editor_Enter);
            buttonEditIPadPassword.MouseUp += new MouseEventHandler(Editor_MouseUp);
            buttonEditIPadPassword.MouseDown += new MouseEventHandler(Editor_MouseDown);
            buttonItemIPadVideoConvert.Click += new EventHandler(this.TabIPadManager.buttonItemIPadVideo_Click);
            buttonItemIPadSyncFiles.Click += new EventHandler(this.TabIPadManager.buttonItemIPadSyncFiles_Click);
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

        #region Button's States
        public bool AddLinkButton
        {
            set
            {
                buttonItemHomeAddUrl.Enabled = value;
                buttonItemHomeAddNetworkShare.Enabled = value;
            }
        }

        public bool LineBreakButton
        {
            set
            {
                buttonItemHomeAddLineBreak.Enabled = value;
            }
        }

        public bool UpLinkButton
        {
            set
            {
                buttonItemHomeNudgeUp.Enabled = value;
            }
        }

        public bool DownLinkButton
        {
            set
            {
                buttonItemHomeNudgeDown.Enabled = value;
            }
        }

        public bool LinkPropertiesButton
        {
            set
            {
                buttonItemHomeProperties.Enabled = value;
            }
        }

        public bool OpenLinkButton
        {
            set
            {
                buttonItemHomeOpen.Enabled = value;
            }
        }

        public bool DeleteLinkButton
        {
            set
            {
                buttonItemHomeDelete.Enabled = value;
            }
        }
        #endregion

        #region GUI Event Handlers
        private void Form_Load(object sender, EventArgs e)
        {
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                ribbonControl.Enabled = false;
                form.laProgress.Text = BusinessClasses.LibraryManager.Instance.OldStyleProceed ? "Upgrading your Sales Library to Version 6..." : "Loading Libraries...";
                form.TopMost = true;

                ribbonControl_SelectedRibbonTabChanged(null, null);

                this.TabHome.InitPage(form);

                ribbonTabItemClipart.Enabled = System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.ClientLogosRootPath) || System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.SalesGalleryRootPath) || System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.WebArtRootPath);

                ribbonControl.Enabled = true;

                ribbonControl.SelectedRibbonTabChanged += new System.EventHandler(ribbonControl_SelectedRibbonTabChanged);
            }
        }

        private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings || ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramManager)
            {
                if (_currentTab == this.TabIPadManager)
                    this.TabIPadManager.SaveIPadSettings();
                if (!pnContainer.Controls.Contains(this.TabHome))
                    pnContainer.Controls.Add(this.TabHome);
                this.TabHome.BringToFront();
                _currentTab = this.TabHome;
            }
            else
            {
                if (_currentTab == this.TabHome)
                    this.TabHome.SaveLibraryWarning();
                if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemClipart)
                {

                    if (!pnContainer.Controls.Contains(this.TabClipart))
                        pnContainer.Controls.Add(this.TabClipart);
                    this.TabClipart.BringToFront();
                    _currentTab = this.TabClipart;
                }
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
                {
                    if (!pnContainer.Controls.Contains(this.TabOvernightsCalendar))
                        pnContainer.Controls.Add(this.TabOvernightsCalendar);
                    this.TabOvernightsCalendar.BringToFront();
                    _currentTab = this.TabOvernightsCalendar;
                }
                else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPad)
                {
                    if (!pnContainer.Controls.Contains(this.TabIPadManager))
                        pnContainer.Controls.Add(this.TabIPadManager);
                    this.TabIPadManager.BringToFront();
                    _currentTab = this.TabIPadManager;
                }
            }
        }
        #endregion

        #region Select All in Editor Handlers
        private bool enter = false;
        private bool needSelect = false;

        public void Editor_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        public void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                (sender as DevExpress.XtraEditors.BaseEdit).SelectAll();
            }
        }

        public void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private void ResetEnterFlag()
        {
            enter = false;
        }
        #endregion
    }
}
