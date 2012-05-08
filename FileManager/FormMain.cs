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
            buttonItemSettingsBranding.Click += new EventHandler(this.TabHome.buttonItemSettingsBranding_Click);
            buttonItemSettingsSync.Click += new EventHandler(this.TabHome.buttonItemSettingsSync_Click);
            buttonItemSettingsPages.Click += new EventHandler(this.TabHome.buttonItemSettingsPages_Click);
            buttonItemSettingsColumns.Click += new EventHandler(this.TabHome.buttonItemSettingsColumns_Click);
            buttonItemSettingsAutoWidgets.Click += new EventHandler(this.TabHome.buttonItemSettingsAutoWidgets_Click);
            buttonItemSettingsDeadLinks.Click += new EventHandler(this.TabHome.buttonItemSettingsDeadLinks_Click);
            buttonItemSettingsEmailList.Click += new EventHandler(this.TabHome.buttonItemSettingsEmailList_Click);
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
                form.laProgress.Text = BusinessClasses.LibraryManager.Instance.OldStyleProceed ? "Upgrading your Sales Library to Version 6..." : "Load Libraries...";
                form.TopMost = true;

                ribbonControl_SelectedRibbonTabChanged(null, null);

                this.TabHome.InitPage(form);

                ribbonControl.Enabled = true;
            }
        }

        private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            Control parent = pnContainer.Parent;
            pnContainer.Parent = null;
            pnContainer.Controls.Clear();
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome || ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
            {
                pnContainer.Controls.Add(this.TabHome);
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemClipart)
            {
                pnContainer.Controls.Add(this.TabClipart);
                this.TabClipart.UpdateView();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
            {
                pnContainer.Controls.Add(this.TabOvernightsCalendar);
            }
            pnContainer.Parent = parent;
            pnContainer.BringToFront();
        }
        #endregion
    }
}
