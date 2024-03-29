﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.ToolForms;
using SalesLibraries.ServiceConnector.Services.Soap;

namespace SalesLibraries.SiteManager.Controllers
{
	public class MainController
	{
		private static MainController _instance;
		private readonly Dictionary<TabPageEnum, IPageController> _controllers = new Dictionary<TabPageEnum, IPageController>();

		#region Sites
		public event EventHandler<SiteChangedEventArgs> SiteChanged;
		public void ChangeSite(SoapServiceConnection site)
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
		public LinkConfigProfilesController LinkConfigProfilesController { get; private set; }
		public LibraryFilesController LibraryFilesController { get; private set; }
		public InactiveUsersController InactiveUsersController { get; private set; }
		public QBuilderController QBuilderController { get; private set; }
		public DataQueryCacheController DataQueryCacheController { get; private set; }
		public UtilitiesController UtilitiesController { get; private set; }
		#endregion

		private MainController()
		{
		}

		public static MainController Instance => _instance ?? (_instance = new MainController());

		private void InitializePresentationLayer()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				FormMain.Instance.ribbonControl.Enabled = false;
				FormMain.Instance.pnMain.Controls.Clear();
				var thread = new Thread(delegate ()
					{
						FormMain.Instance.Invoke((MethodInvoker)delegate
						{
							formProgress.laProgress.Text = "Loading data...";
							Application.DoEvents();
							UsersController.InitController();
							Application.DoEvents();
							ActivitiesController.InitController();
							Application.DoEvents();
							LinkConfigProfilesController.InitController();
							Application.DoEvents();
							LibraryFilesController.InitController();
							Application.DoEvents();
							InactiveUsersController.InitController();
							Application.DoEvents();
							QBuilderController.InitController();
							Application.DoEvents();
							DataQueryCacheController.InitController();
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
			LinkConfigProfilesController = new LinkConfigProfilesController();
			_controllers.Add(TabPageEnum.LinkConfigProfiles, LinkConfigProfilesController);
			LibraryFilesController = new LibraryFilesController();
			_controllers.Add(TabPageEnum.LibraryFiles, LibraryFilesController);
			InactiveUsersController = new InactiveUsersController();
			_controllers.Add(TabPageEnum.InactiveUsers, InactiveUsersController);
			QBuilderController = new QBuilderController();
			_controllers.Add(TabPageEnum.QBuilder, QBuilderController);
			DataQueryCacheController = new DataQueryCacheController();
			_controllers.Add(TabPageEnum.DataQueryCache, DataQueryCacheController);
			UtilitiesController = new UtilitiesController();
			_controllers.Add(TabPageEnum.Utilities, UtilitiesController);
		}

		public void LoadDataAndGUI()
		{
			SiteChanged = null;
			InitializePresentationLayer();
			ChangeSite(WebSiteManager.Instance.SelectedSite);
		}

		#region Common Page Functionality
		public void ShowTab(TabPageEnum tabPage)
		{
			if (!_controllers.ContainsKey(tabPage)) return;

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
		LinkConfigProfiles,
		LibraryFiles,
		InactiveUsers,
		QBuilder,
		DataQueryCache,
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
		public SoapServiceConnection SelectedSite { get; set; }
	}
}