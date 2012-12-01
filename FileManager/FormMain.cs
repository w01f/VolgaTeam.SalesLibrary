using System;
using System.Windows.Forms;
using FileManager.TabPages;

namespace FileManager
{
	public partial class FormMain : Form
	{
		private static FormMain _instance = null;

		public TabPages.TabHomeControl TabHome { get; set; }
		public TabPages.TabClipartControl TabClipart { get; set; }
		public TabPages.TabOvernightsCalendarControl TabOvernightsCalendar { get; set; }
		public TabPages.TabIPadManagerControl TabIPadManager { get; set; }
		public TabPages.TabIPadUsersControl TabIPadUsers { get; set; }
		private Control _currentTab = null;

		private FormMain()
		{
			InitializeComponent();
			TabHome = new TabPages.TabHomeControl();
			FormClosing += TabHome.FormClosing;
			comboBoxEditLibraries.EditValueChanged += TabHome.comboBoxEditLibraries_EditValueChanged;
			comboBoxEditLibraries.EditValueChanging += TabHome.comboBoxEditLibraries_EditValueChanging;
			comboBoxEditPages.SelectedIndexChanged += TabHome.comboBoxEditPages_SelectedIndexChanged;
			buttonItemHomeFileTreeView.CheckedChanged += TabHome.buttonItemHomeFileTreeView_CheckedChanged;
			buttonItemSettingsPaths.Click += TabHome.btPathSettings_Click;
			buttonItemSettingsExtraRoots.Click += TabHome.btExtraRoot_Click;
			buttonItemSettingsBranding.Click += TabHome.buttonItemSettingsBranding_Click;
			buttonItemSettingsSync.Click += TabHome.buttonItemSettingsSync_Click;
			buttonItemSettingsPages.Click += TabHome.buttonItemSettingsPages_Click;
			buttonItemSettingsColumns.Click += TabHome.buttonItemSettingsColumns_Click;
			buttonItemSettingsAutoWidgets.Click += TabHome.buttonItemSettingsAutoWidgets_Click;
			buttonItemSettingsDeadLinks.Click += TabHome.buttonItemSettingsDeadLinks_Click;
			buttonItemSettingsEmailList.Click += TabHome.buttonItemSettingsEmailList_Click;
			buttonItemSettingsAutoSync.Click += TabHome.buttonItemSettingsAutoSync_Click;
			buttonItemHomeAddLineBreak.Click += TabHome.btLineBreak_Click;
			buttonItemHomeAddNetworkShare.Click += TabHome.btAddNeworkShare_Click;
			buttonItemHomeAddUrl.Click += TabHome.btAddUrl_Click;
			buttonItemHomeFontDown.Click += TabHome.btFontDown_Click;
			buttonItemHomeFontUp.Click += TabHome.btFontUp_Click;
			buttonItemHomeNudgeDown.Click += TabHome.btDownLink_Click;
			buttonItemHomeNudgeUp.Click += TabHome.btUpLink_Click;
			buttonItemHomeDelete.Click += TabHome.btDeleteLink_Click;
			buttonItemHomeOpen.Click += TabHome.btOpenLink_Click;
			buttonItemHomeProperties.Click += TabHome.buttonItemHomeProperties_Click;
			buttonItemHomeSave.Click += TabHome.btSave_Click;
			buttonItemHomeSync.Click += TabHome.btSync_Click;
			buttonItemHomeExit.Click += TabHome.btExit_Click;

			buttonItemProgramManagerSyncDisabled.Click += TabHome.buttonItemProgramManagerSync_Click;
			buttonItemProgramManagerSyncEnabled.Click += TabHome.buttonItemProgramManagerSync_Click;
			buttonItemProgramManagerSyncDisabled.CheckedChanged += TabHome.buttonItemProgramManagerSync_CheckedChanged;
			buttonItemProgramManagerSyncEnabled.CheckedChanged += TabHome.buttonItemProgramManagerSync_CheckedChanged;
			buttonEditProgramManagerLocation.ButtonClick += TabHome.buttonEditProgramManagerLocation_ButtonClick;

			TabClipart = new TabPages.TabClipartControl();
			buttonItemClipartClientLogos.Click += TabClipart.buttonItemClipart_Click;
			buttonItemClipartSalesGallery.Click += TabClipart.buttonItemClipart_Click;
			buttonItemClipartWebArt.Click += TabClipart.buttonItemClipart_Click;
			buttonItemClipartClientLogos.CheckedChanged += TabClipart.buttonItemClipart_CheckedChanged;
			buttonItemClipartSalesGallery.CheckedChanged += TabClipart.buttonItemClipart_CheckedChanged;
			buttonItemClipartWebArt.CheckedChanged += TabClipart.buttonItemClipart_CheckedChanged;

			TabOvernightsCalendar = new TabPages.TabOvernightsCalendarControl();
			buttonItemCalendarSyncStatusDisabled.Click += TabOvernightsCalendar.buttonItemCalendarSyncStatus_Click;
			buttonItemCalendarSyncStatusEnabled.Click += TabOvernightsCalendar.buttonItemCalendarSyncStatus_Click;
			buttonItemCalendarSyncStatusDisabled.CheckedChanged += TabOvernightsCalendar.buttonItemCalendarSyncStatus_CheckedChanged;
			buttonItemCalendarSyncStatusEnabled.CheckedChanged += TabOvernightsCalendar.buttonItemCalendarSyncStatus_CheckedChanged;
			buttonEditCalendarLocation.EditValueChanged += TabOvernightsCalendar.buttonEditCalendarLocation_EditValueChanged;
			buttonEditCalendarLocation.ButtonClick += TabOvernightsCalendar.buttonEditCalendarLocation_ButtonClick;
			buttonItemCalendarSettings.Click += TabOvernightsCalendar.buttonItemCalendarSettings_Click;
			buttonItemCalendarFontUp.Click += TabOvernightsCalendar.buttonItemCalendarFontUp_Click;
			buttonItemCalendarFontDown.Click += TabOvernightsCalendar.buttonItemCalendarFontDown_Click;
			buttonItemCalendarEmailGrabber.Click += TabOvernightsCalendar.buttonItemCalendarEmailGrabber_Click;
			buttonItemCalendarFileGrabber.Click += TabOvernightsCalendar.buttonItemCalendarFileGrabber_Click;

			TabIPadManager = new TabPages.TabIPadManagerControl();
			buttonItemIPadSyncDisabled.Click += TabIPadManager.buttonItemIPadSyncStatus_Click;
			buttonItemIPadSyncEnabled.Click += TabIPadManager.buttonItemIPadSyncStatus_Click;
			buttonItemIPadSyncDisabled.CheckedChanged += TabIPadManager.buttonItemIPadSyncStatus_CheckedChanged;
			buttonItemIPadSyncEnabled.CheckedChanged += TabIPadManager.buttonItemIPadSyncStatus_CheckedChanged;
			buttonEditIPadLocation.EditValueChanged += TabIPadManager.buttonEditIPadLocation_EditValueChanged;
			buttonEditIPadLocation.ButtonClick += TabIPadManager.buttonEditIPadLocation_ButtonClick;
			buttonEditIPadLocation.Enter += Editor_Enter;
			buttonEditIPadLocation.MouseUp += Editor_MouseUp;
			buttonEditIPadLocation.MouseDown += Editor_MouseDown;
			buttonEditIPadSite.EditValueChanged += TabIPadManager.buttonEditIPadSite_EditValueChanged;
			buttonEditIPadSite.Enter += Editor_Enter;
			buttonEditIPadSite.MouseUp += Editor_MouseUp;
			buttonEditIPadSite.MouseDown += Editor_MouseDown;
			buttonEditIPadLogin.EditValueChanged += TabIPadManager.buttonEditIPadSite_EditValueChanged;
			buttonEditIPadLogin.Enter += Editor_Enter;
			buttonEditIPadLogin.MouseUp += Editor_MouseUp;
			buttonEditIPadLogin.MouseDown += Editor_MouseDown;
			buttonEditIPadPassword.EditValueChanged += TabIPadManager.buttonEditIPadSite_EditValueChanged;
			buttonEditIPadPassword.Enter += Editor_Enter;
			buttonEditIPadPassword.MouseUp += Editor_MouseUp;
			buttonEditIPadPassword.MouseDown += Editor_MouseDown;
			buttonItemIPadVideoConvert.Click += TabIPadManager.buttonItemIPadVideo_Click;
			buttonItemIPadSyncFiles.Click += TabIPadManager.buttonItemIPadSyncFiles_Click;

			TabIPadUsers = new TabIPadUsersControl();
			buttonItemIPadUsersAdd.Click += TabIPadUsers.buttonItemIPadUsersAdd_Click;
			buttonItemIPadUsersEdit.Click += TabIPadUsers.buttonItemIPadUsersEdit_Click;
			buttonItemIPadUsersDelete.Click += TabIPadUsers.buttonItemIPadUsersDelete_Click;
			buttonItemIPadUsersRefresh.Click += TabIPadUsers.buttonItemIPadUsersRefresh_Click;
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
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPadUsers)
				{
					if (!pnContainer.Controls.Contains(this.TabIPadUsers))
						pnContainer.Controls.Add(this.TabIPadUsers);
					this.TabIPadUsers.BringToFront();
					_currentTab = this.TabIPadUsers;
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
