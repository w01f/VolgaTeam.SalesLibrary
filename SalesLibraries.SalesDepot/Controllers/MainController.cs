using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Common.Authorization;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.SalesDepot.Business.Services;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.PresentationLayer.Settings;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.SalesDepot.Controllers
{
	class MainController
	{
		private static readonly MainController _instance = new MainController();
		private readonly Dictionary<TabPageEnum, IPageController> _tabPages = new Dictionary<TabPageEnum, IPageController>();

		public static MainController Instance
		{
			get { return _instance; }
		}

		public TabPageEnum ActiveTab { get; private set; }

		public SettingsManager Settings { get; private set; }
		public ListManager Lists { get; private set; }
		public EmailBinManager EmailBin { get; private set; }

		public WallbinManager Wallbin { get; private set; }

		public ActivityManager ActivityManager { get; private set; }
		public HelpManager HelpManager { get; private set; }

		public ViewManager WallbinViews { get; private set; }

		public FormMain MainForm { get; private set; }
		public BackgroundProcessManager ProcessManager { get; private set; }
		public PopupMessageHelper PopupMessages { get; private set; }

		#region Tab Pages
		private WallbinPage TabWallbin { get; set; }
		private CalendarPage TabCalendar { get; set; }
		private ProgramScheduleViewPage TabProgramScheduleView { get; set; }
		private ProgramScheduleSearchPage TabProgramScheduleSearch { get; set; }
		private SettingsPage TabSettings { get; set; }
		private Gallery1Page TabGallery1 { get; set; }
		private Gallery2Page TabGallery2 { get; set; }
		#endregion

		private MainController()
		{
			Settings = new SettingsManager();
			Lists = new ListManager();

			Wallbin = new WallbinManager();

			HelpManager = new HelpManager();
			EmailBin = new EmailBinManager();

			WallbinViews = new ViewManager();

			MainForm = new FormMain();
			ProcessManager = new BackgroundProcessManager(MainForm, Settings.SalesDepotName);
			PopupMessages = new PopupMessageHelper(Settings.SalesDepotName);
		}
		public void RunApplication()
		{
			var stopRun = false;

			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.SalesDepot);

			FileStorageManager.Instance.UsingLocalMode += (o, e) =>
			{
				if (FileStorageManager.Instance.UseLocalMode) return;
				ProcessManager.SuspendProcess();
				if (FileStorageManager.Instance.DataState != DataActualityState.Updated)
				{
					PopupMessages.ShowWarning("Server is not available. Application will be closed");
					stopRun = true;
					Application.Exit();
					return;
				}
				if (PopupMessages.ShowWarningQuestion("Server is not available. Do you want to continue to work in local mode?") != DialogResult.Yes)
				{
					stopRun = true;
					Application.Exit();
				}
				ProcessManager.ResumeProcess();
			};

			FileStorageManager.Instance.Authorizing += (o, e) =>
			{
				var authManager = new AuthManager();
				authManager.Init();
				authManager.Auth(e);
			};

			ProcessManager.RunStartProcess(
				"Connecting to adSALEScloud…",
				cancellationToken => AsyncHelper.RunSync(FileStorageManager.Instance.Init));

			if (stopRun) return;

			var appReady = FileStorageManager.Instance.Activated;
			if (appReady)
			{
				var progressTitle = String.Empty;
				switch (FileStorageManager.Instance.DataState)
				{
					case DataActualityState.NotExisted:
						progressTitle = "Syncing adSALEScloud for the 1st time…";
						break;
					case DataActualityState.Outdated:
						progressTitle = "Refreshing data from adSALEScloud…";
						break;
					default:
						progressTitle = "Loading data...";
						break;
				}

				ProcessManager.RunStartProcess(
					progressTitle,
					cancellationToken => AsyncHelper.RunSync(InitBusinessObjects));

				ProcessManager.RunStartProcess(
					FileStorageManager.Instance.DataState == DataActualityState.NotExisted ?
						"Syncing Libraries for the 1st time…" :
						"Syncing Sales Libraries…",
					cancellationToken =>
					{
						LibrariesSyncHelper.Sync(
							File.ReadAllLines(Configuration.RemoteResourceManager.Instance.NetworkPathFile.LocalPath),
							Configuration.RemoteResourceManager.Instance.LocalLibraryFolder.LocalPath
							);
						Wallbin.LoadLibraries(Configuration.RemoteResourceManager.Instance.LocalLibraryFolder.LocalPath);
						EmailBin.Load();
					});

				appReady &= Wallbin.Libraries.Any();
				if (appReady)
				{
					if (Settings.RunPowerPointWhenNeeded.HasValue && Settings.RunPowerPointWhenNeeded.Value)
						PowerPointManager.Instance.RunPowerPointLoader();

					ActivityManager.AddUserActivity("Application started");

					LoadControllers();

					MainForm.InitForm();

					Application.Run(MainForm);

				}
			}
			if (!appReady)
				PopupMessages.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		public void ActivateApplication()
		{
			var handle = RegistryHelper.SalesDepotHandle;
			if (handle.Equals(IntPtr.Zero))
				handle = MainForm.Handle;
			Utils.ActivateForm(handle, RegistryHelper.MaximizeSalesDepot, false);
		}

		public async Task InitBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();
			await Configuration.RemoteResourceManager.Instance.Load();

			PowerPointManager.Instance.Init();

			Settings.LoadSettings();
			Lists.Load();
			SelectActiveTab();

			ActivityManager = ActivityManager.OpenStorage<ActivityManager>();

			HelpManager.LoadHelpLinks();
		}

		private void LoadControllers()
		{
			_tabPages.Clear();
			ProcessManager.Run("Loading Controls...", cancelationToken => MainForm.Invoke(new MethodInvoker(() =>
			{
				TabWallbin = new WallbinPage();
				_tabPages.Add(TabPageEnum.Home, TabWallbin);
				Application.DoEvents();

				TabCalendar = new CalendarPage();
				_tabPages.Add(TabPageEnum.Calendar, TabCalendar);
				Application.DoEvents();

				TabProgramScheduleView = new ProgramScheduleViewPage();
				_tabPages.Add(TabPageEnum.ProgramsView, TabProgramScheduleView);
				Application.DoEvents();

				TabProgramScheduleSearch = new ProgramScheduleSearchPage();
				_tabPages.Add(TabPageEnum.ProgramsSearch, TabProgramScheduleSearch);
				Application.DoEvents();

				TabSettings = new SettingsPage();
				_tabPages.Add(TabPageEnum.Settings, TabSettings);
				Application.DoEvents();

				TabGallery1 = new Gallery1Page();
				_tabPages.Add(TabPageEnum.Gallery1, TabGallery1);
				Application.DoEvents();

				TabGallery2 = new Gallery2Page();
				_tabPages.Add(TabPageEnum.Gallery2, TabGallery2);
				Application.DoEvents();

				TabWallbin.InitController();
				Application.DoEvents();
				TabCalendar.InitController();
				Application.DoEvents();
				TabProgramScheduleView.InitController();
				Application.DoEvents();
				TabProgramScheduleSearch.InitController();
				Application.DoEvents();
				TabSettings.InitController();
				Application.DoEvents();
				TabGallery1.InitController();
				Application.DoEvents();
				TabGallery2.InitController();
				Application.DoEvents();
			})));
		}

		public void LoadWallbinViews()
		{
			MainForm.pnContainer.Controls.Clear();
			ProcessManager.RunInQueue("Loading Wallbin...",
					() => MainForm.Invoke(new MethodInvoker(() => WallbinViews.Load())),
					() => MainForm.Invoke(new MethodInvoker(() =>
					{
						ShowTab();
						ProcessManager.RunInQueue(
							"",
							() => MainForm.Invoke(new MethodInvoker(() =>
							{
								if (!PowerPointSingleton.Instance.IsLinkedWithApplication &&
									Settings.LinkLaunchSettings.PowerPoint == LinkLaunchOptionsEnum.Viewer)
								{
									if (!Settings.RunPowerPointWhenNeeded.HasValue)
										using (var form = new FormPowerPointWarning())
										{
											if (form.ShowDialog(MainForm) == DialogResult.OK)
												CheckPowerPointRunning();
											TabSettings.InitController();
										}
								}
							})),
							null,
							false);
					})));
		}

		public void ShowTab(TabPageEnum tabPage = TabPageEnum.None)
		{
			MainForm.ribbonControl.Enabled = true;
			if (tabPage == TabPageEnum.None)
				tabPage = ActiveTab;

			foreach (var pageController in _tabPages.Where(pageController => pageController.Key == ActiveTab))
				pageController.Value.IsActive = false;

			ActiveTab = tabPage;
			SaveActiveTab();
			if (!_tabPages.ContainsKey(tabPage)) return;
			_tabPages[tabPage].ShowPage(tabPage);
		}

		public void SelectActiveTab()
		{
			if (Settings.WallbinViewSettings.HomeView)
				ActiveTab = TabPageEnum.Home;
			else if (Settings.WallbinViewSettings.SearchView)
				ActiveTab = TabPageEnum.Search;
			else if (Settings.WallbinViewSettings.CalendarView)
				ActiveTab = TabPageEnum.Calendar;
		}

		public void SaveActiveTab()
		{
			switch (ActiveTab)
			{
				case TabPageEnum.Home:
					Settings.WallbinViewSettings.SelectHomePage();
					break;
				case TabPageEnum.Search:
					Settings.WallbinViewSettings.SelectSearchPage();
					break;
				case TabPageEnum.Calendar:
					Settings.WallbinViewSettings.SelectCalendarPage();
					break;
			}
			Settings.SaveSettings();
		}

		public bool CheckPowerPointRunning(Func<bool> beforeRun = null)
		{
			if (PowerPointSingleton.Instance.IsLinkedWithApplication) return true;
			if (beforeRun != null && !beforeRun()) return false;
			FloaterManager.Instance.ShowFloater(
				MainForm,
				Settings.SalesDepotName,
				MainForm.FloaterLogo,
				PowerPointManager.Instance.RunPowerPointLoader);
			return false;
		}
	}
}
