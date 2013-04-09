using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FileManager.Controllers;
using SalesDepot.SiteManager.ConfigurationClasses;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		private FormMain()
		{
			InitializeComponent();

			TabUsers = new TabUsersControl();
		}

		public TabUsersControl TabUsers { get; private set; }

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
				labelItemActivitiesLogo.Image = image;
				labelItemTickerLogo.Image = image;
				labelItemInactiveUsersLogo.Image = image;
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
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTicker)
				key = TabPageEnum.Ticker;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemInactiveUsers)
				key = TabPageEnum.InactiveUsers;
			MainController.Instance.ShowTab(key);
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
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