using System;
using System.Windows.Forms;
using FileManager.Controllers;
using FileManager.PresentationClasses.TabPages;

namespace FileManager
{
	public partial class FormMain : Form
	{
		private static FormMain _instance = null;

		public TabIPadUsersControl TabIPadUsers { get; set; }

		private FormMain()
		{
			InitializeComponent();

			pnMain.Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;

			buttonEditIPadLocation.Enter += EditorEnter;
			buttonEditIPadLocation.MouseUp += EditorMouseUp;
			buttonEditIPadLocation.MouseDown += EditorMouseDown;
			buttonEditIPadSite.Enter += EditorEnter;
			buttonEditIPadSite.MouseUp += EditorMouseUp;
			buttonEditIPadSite.MouseDown += EditorMouseDown;
			buttonEditIPadLogin.Enter += EditorEnter;
			buttonEditIPadLogin.MouseUp += EditorMouseUp;
			buttonEditIPadLogin.MouseDown += EditorMouseDown;
			buttonEditIPadPassword.Enter += EditorEnter;
			buttonEditIPadPassword.MouseUp += EditorMouseUp;
			buttonEditIPadPassword.MouseDown += EditorMouseDown;
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

		#region GUI Event Handlers
		private void Form_Load(object sender, EventArgs e)
		{
			ribbonTabItemCalendar.Enabled = ConfigurationClasses.SettingsManager.Instance.EnableOvernightsCalendarTab;
			ribbonTabItemClipart.Enabled = ConfigurationClasses.SettingsManager.Instance.EnableClipartTab && (System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.ClientLogosRootPath) || System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.SalesGalleryRootPath) || System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.WebArtRootPath));
			ribbonTabItemProgramManager.Enabled = ConfigurationClasses.SettingsManager.Instance.EnableProgramManagerTab;
			ribbonTabItemIPad.Enabled = ConfigurationClasses.SettingsManager.Instance.EnableIPadSettingsTab;
			ribbonTabItemIPadUsers.Enabled = ConfigurationClasses.SettingsManager.Instance.EnableIPadUsersTab;
			ribbonTabItemTags.Enabled = ConfigurationClasses.SettingsManager.Instance.EnableTagsTab;

			MainController.Instance.InitializeControllers();
			MainController.Instance.LoadDataAndGUI();

			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void Form_Closing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !MainController.Instance.SaveLibraryWarning();
		}

		private void btExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			var key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
				key = TabPageEnum.Home;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTags)
				key = TabPageEnum.Tags;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPreferences)
				key = TabPageEnum.Preferences;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
				key = TabPageEnum.Settings;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemClipart)
				key = TabPageEnum.Clipart;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProgramManager)
				key = TabPageEnum.ProgramManager;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				key = TabPageEnum.Calendar;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPad)
				key = TabPageEnum.IPadContent;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPadUsers)
				key = TabPageEnum.IPadUsers;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHelp)
				key = TabPageEnum.Help;
			MainController.Instance.ShowTab(key);
		}
		#endregion

		#region Select All in Editor Handlers
		private bool _enter;
		private bool _needSelect;

		public void EditorEnter(object sender, EventArgs e)
		{
			_enter = true;
			BeginInvoke(new MethodInvoker(ResetEnterFlag));
		}

		public void EditorMouseUp(object sender, MouseEventArgs e)
		{
			if (_needSelect)
			{
				var baseEdit = sender as DevExpress.XtraEditors.BaseEdit;
				if (baseEdit != null) baseEdit.SelectAll();
			}
		}

		public void EditorMouseDown(object sender, MouseEventArgs e)
		{
			_needSelect = _enter;
		}

		private void ResetEnterFlag()
		{
			_enter = false;
		}
		#endregion
	}
}
