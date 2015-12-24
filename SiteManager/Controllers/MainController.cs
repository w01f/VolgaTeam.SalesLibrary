﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.Services;
using SalesDepot.SiteManager.BusinessClasses;
using SalesDepot.SiteManager.ConfigurationClasses;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.Controllers
{
	public class MainController
	{
		private static MainController _instance;
		private readonly Dictionary<TabPageEnum, IPageController> _controllers = new Dictionary<TabPageEnum, IPageController>();

		#region Sites
		public event EventHandler<SiteChangedEventArgs> SiteChanged;
		public void ChangeSite(SiteClient site)
		{
			WebSiteManager.Instance.SelectSite(site);
			if (SiteChanged != null)
			{
				var siteChangedEventArgs = new SiteChangedEventArgs();
				siteChangedEventArgs.SelectedSite = site;
				SiteChanged(this, new SiteChangedEventArgs());
			}
		}
		#endregion

		#region Controllers
		public UsersController UsersController { get; private set; }
		public ActivitiesController ActivitiesController { get; private set; }
		public InactiveUsersController InactiveUsersController { get; private set; }
		public QBuilderController QBuilderController { get; private set; }
		public UtilitiesController UtilitiesController { get; private set; }
		#endregion

		private MainController()
		{
		}

		public static MainController Instance
		{
			get { return _instance ?? (_instance = new MainController()); }
		}

		private void InitializePresentationLayer()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				FormMain.Instance.ribbonControl.Enabled = false;
				FormMain.Instance.pnMain.Controls.Clear();
				var thread = new Thread(delegate()
					{
						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							formProgress.laProgress.Text = "Loading data...";
							Application.DoEvents();
							UsersController.InitController();
							Application.DoEvents();
							ActivitiesController.InitController();
							Application.DoEvents();
							InactiveUsersController.InitController();
							Application.DoEvents();
							QBuilderController.InitController();
							Application.DoEvents();
							UtilitiesController.InitController();
							Application.DoEvents();
						});
					});
				formProgress.Show();
				Application.DoEvents();
				thread.Start();
				Application.DoEvents();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
				FormMain.Instance.ribbonControl.Enabled = true;
				FormMain.Instance.pnMain.BringToFront();
			}
		}

		public void InitializeControllers()
		{
			_controllers.Clear();
			UsersController = new UsersController();
			_controllers.Add(TabPageEnum.Users, UsersController);
			ActivitiesController = new ActivitiesController();
			_controllers.Add(TabPageEnum.Activities, ActivitiesController);
			InactiveUsersController = new InactiveUsersController();
			_controllers.Add(TabPageEnum.InactiveUsers, InactiveUsersController);
			QBuilderController = new QBuilderController();
			_controllers.Add(TabPageEnum.QBuilder, QBuilderController);
			UtilitiesController = new UtilitiesController();
			_controllers.Add(TabPageEnum.Utilities, UtilitiesController);
		}

		public void LoadDataAndGUI()
		{
			SiteChanged = null;
			InitializePresentationLayer();

			ChangeSite(WebSiteManager.Instance.SelectedSite);

			_activeTab = (TabPageEnum)SettingsManager.Instance.SelectedTab;
			ShowTab(_activeTab);

			switch (_activeTab)
			{
				case TabPageEnum.Users:
					FormMain.Instance.ribbonControl.SelectedRibbonTabItem = FormMain.Instance.ribbonTabItemUsers;
					break;
				case TabPageEnum.Activities:
					FormMain.Instance.ribbonControl.SelectedRibbonTabItem = FormMain.Instance.ribbonTabItemActivities;
					break;
				case TabPageEnum.InactiveUsers:
					FormMain.Instance.ribbonControl.SelectedRibbonTabItem = FormMain.Instance.ribbonTabItemInactiveUsers;
					break;
				case TabPageEnum.QBuilder:
					FormMain.Instance.ribbonControl.SelectedRibbonTabItem = FormMain.Instance.ribbonTabItemQBuilder;
					break;
				case TabPageEnum.Utilities:
					FormMain.Instance.ribbonControl.SelectedRibbonTabItem = FormMain.Instance.ribbonTabItemUtilities;
					break;
			}
		}

		#region Common Page Functionality
		private TabPageEnum _activeTab;
		public void ShowTab(TabPageEnum tabPage)
		{
			if (!_controllers.ContainsKey(tabPage)) return;
			_activeTab = tabPage;

			SettingsManager.Instance.SelectedTab = (int)tabPage;
			SettingsManager.Instance.Save();

			_controllers.Values.ToList().ForEach(controller => controller.IsActive = false);
			_controllers[tabPage].PrepareTab(tabPage);
			_controllers[tabPage].ShowTab();
		}
		#endregion
	}

	public enum TabPageEnum
	{
		Users,
		Activities,
		InactiveUsers,
		QBuilder,
		Utilities
	}

	public interface IPageController
	{
		bool IsActive { get; set; }
		bool NeedToUpdate { get; set; }
		void InitController();
		void PrepareTab(TabPageEnum tabPage);
		void ShowTab();
	}

	public class SiteChangedEventArgs : EventArgs
	{
		public SiteClient SelectedSite { get; set; }
	}
}