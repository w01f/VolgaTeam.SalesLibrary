using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.Controllers;

namespace SalesLibraries.SiteManager
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		public FormMain()
		{
			InitializeComponent();

			if (SettingsManager.Instance.AppWindowSettings.Existed)
			{
				StartPosition = FormStartPosition.Manual;
				WindowState = SettingsManager.Instance.AppWindowSettings.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
				Top = SettingsManager.Instance.AppWindowSettings.Top;
				Left = SettingsManager.Instance.AppWindowSettings.Left;
				Height = SettingsManager.Instance.AppWindowSettings.Height;
				Width = SettingsManager.Instance.AppWindowSettings.Width;
			}
		}

		public static FormMain Instance
		{
			get { return _instance ?? (_instance = new FormMain()); }
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);
			if (File.Exists(SettingsManager.Instance.LogoPath))
			{
				var image = new Bitmap(SettingsManager.Instance.LogoPath);
				labelItemUsersLogo.Image = image;
				ribbonBarUsersLogo.RecalcLayout();
				ribbonPanelUsers.PerformLayout();
				
				labelItemActivitiesLogo.Image = image;
				ribbonBarActivitiesLogo.RecalcLayout();
				ribbonPanelActivities.PerformLayout();

				labelItemLinkConfigProfilesLogo.Image = image;
				ribbonBarLinkConfigProfilesLogo.RecalcLayout();
				ribbonPanelLinkConfigProfiles.PerformLayout();

				labelItemLibrariesLogo.Image = image;
				ribbonBarLibrariesLogo.RecalcLayout();
				ribbonPanelLibraries.PerformLayout();

				labelItemInactiveUsersLogo.Image = image;
				ribbonBarInactiveUsersLogo.RecalcLayout();
				ribbonPanelInactiveUsers.PerformLayout();

				labelItemQBuilderLogo.Image = image;
				ribbonBarQBuilderLogo.RecalcLayout();
				ribbonPanelQBuilder.PerformLayout();

				labelItemUtilitiesLogo.Image = image;
				ribbonBarUtilitiesLogo.RecalcLayout();
				ribbonPanelUtilities.PerformLayout();
			}

			MainController.Instance.InitializeControllers();
			MainController.Instance.LoadDataAndGUI();

			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			var key = TabPageEnum.Users;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemUsers)
				key = TabPageEnum.Users;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemActivities)
				key = TabPageEnum.Activities;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemLinkConfigProfiles)
				key = TabPageEnum.LinkConfigProfiles;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemLibraries)
				key = TabPageEnum.LibraryFiles;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemInactiveUsers)
				key = TabPageEnum.InactiveUsers;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemQBuilder)
				key = TabPageEnum.QBuilder;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemUtilities)
				key = TabPageEnum.Utilities;
			MainController.Instance.ShowTab(key);
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			StartPosition = FormStartPosition.Manual;
			SettingsManager.Instance.AppWindowSettings.Maximized = WindowState == FormWindowState.Maximized;
			SettingsManager.Instance.AppWindowSettings.Top = Top;
			SettingsManager.Instance.AppWindowSettings.Left = Left;
			SettingsManager.Instance.AppWindowSettings.Height = Height;
			SettingsManager.Instance.AppWindowSettings.Width = Width;
			SettingsManager.Instance.AppWindowSettings.Save();
		}

		#region Select All in Editor Handlers
		private bool _enter;
		private bool _needSelect;

		public void Editor_Enter(object sender, EventArgs e)
		{
			_enter = true;
			BeginInvoke(new MethodInvoker(ResetEnterFlag));
		}

		public void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (_needSelect)
			{
				(sender as BaseEdit).SelectAll();
			}
		}

		public void Editor_MouseDown(object sender, MouseEventArgs e)
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