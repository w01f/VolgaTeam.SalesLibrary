using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Contexts.Wallbin.Local;
using SalesLibraries.Common.Authorization;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Business.Models.Connection;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Business.Synchronization;
using SalesLibraries.FileManager.Configuration;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.FileManager.Controllers
{
	class MainController
	{
		private readonly Dictionary<TabPageEnum, IPageController> _tabPages = new Dictionary<TabPageEnum, IPageController>();
		private TabPageEnum _activeTab;

		public static MainController Instance { get; } = new MainController();

		public SettingsManager Settings { get; }
		public ListManager Lists { get; }
		public RestServiceConnection RestServiceConnection { get; }
		public LocalWallbinManager Wallbin { get; }
		public HelpManager HelpManager { get; }
		public ImageResourcesManager ImageResources { get; }

		public ViewManager WallbinViews { get; }

		public FormMain MainForm { get; }

		public BackgroundProcessManager ProcessManager { get; }
		public PopupMessageHelper PopupMessages { get; }

		#region Tab Pages
		public WallbinPage TabWallbin { get; private set; }
		protected VideoPage TabVideo { get; private set; }
		protected CalendarPage TabCalendar { get; private set; }
		#endregion

		private MainController()
		{
			Settings = new SettingsManager();
			Lists = new ListManager();
			RestServiceConnection = new RestServiceConnection();
			Wallbin = new LocalWallbinManager();
			HelpManager = new HelpManager();
			ImageResources = new ImageResourcesManager();
			WallbinViews = new ViewManager();
			MainForm = new FormMain();
			ProcessManager = new BackgroundProcessManager(MainForm, "Site Admin");
			PopupMessages = new PopupMessageHelper("Site Admin");
		}

		public void RunApplication()
		{
			var stopRun = false;
			var appReady = false;

			LicenseHelper.Register();

			ImageResources.LoadLocal();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.FileManager);

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
				MainController.Instance.ImageResources.AppSplashLogo,
				(cancellationToken, formProgress) =>
				{
					formProgress.Invoke(new MethodInvoker(() =>
					{
						formProgress.ProcessConnectionStage();
						Application.DoEvents();
					}));

					AsyncHelper.RunSync(FileStorageManager.Instance.Init);

					if (stopRun) return;

					appReady = FileStorageManager.Instance.Activated;

					if (!appReady) return;

					formProgress.Invoke(new MethodInvoker(() =>
					{
						formProgress.ProcessSecurityStage();
						Application.DoEvents();
					}));

					AsyncHelper.RunSync(AppProfileManager.Instance.LoadProfile);
					AsyncHelper.RunSync(Configuration.RemoteResourceManager.Instance.LoadLocal);
					Settings.LoadLocal();

					if (!String.IsNullOrEmpty(Settings.BackupPath) && Directory.Exists(Settings.BackupPath))
					{
						var connectionState = DatabaseConnectionHelper.GetConnectionState(Settings.BackupPath);
						if (connectionState.Type == ConnectionStateType.Busy)
						{
							ProcessManager.SuspendProcess();
							PopupMessages.ShowWarning(
								String.Format(
									"{0} is currently updating the site.{1}Please try back again later, or ask the user to hurry up and finish…",
									connectionState.User,
									Environment.NewLine));
							appReady = false;
							stopRun = true;
							return;
						}
					}

					formProgress.Invoke(new MethodInvoker(() =>
					{
						formProgress.ProcessLoadFilesStage();
						Application.DoEvents();
					}));

					AsyncHelper.RunSync(Configuration.RemoteResourceManager.Instance.LoadRemote);
					Settings.LoadRemote();
					ImageResources.LoadRemote();
					InitBusinessObjects();
					AsyncHelper.RunSync(FileStorageManager.Instance.FixDataState);

					if (String.IsNullOrEmpty(Settings.BackupPath) || !Directory.Exists(Settings.BackupPath))
					{
						ProcessManager.SuspendProcess();
						formProgress.Invoke(new MethodInvoker(() =>
						{
							using (var form = new FormPaths())
							{
								if (form.ShowDialog() == DialogResult.OK)
								{
									Settings.Save();
								}
								else
									appReady = false;
							}
						}));
						ProcessManager.ResumeProcess();
					}

					if (appReady)
					{
						DatabaseConnectionHelper.Connect(Settings.BackupPath);
						Wallbin.LoadLibrary(Settings.BackupPath);
					}
				});

			if (stopRun) return;
			if (appReady)
			{
				FormStateHelper.Init(MainForm, Common.Helpers.RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Main-Form", true, true);
				MainForm.Shown += (o, e) =>
				{
					MainForm.InitForm();
					LoadControllers();
					ProcessManager.RunInQueue("Loading Wallbin...",
						() => MainForm.Invoke(new MethodInvoker(() => WallbinViews.Load())),
						() => MainForm.Invoke(new MethodInvoker(() => ShowTab(TabPageEnum.Home))));
				};
				Application.Run(MainForm);
			}
			else
				PopupMessages.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		public void RunConsole()
		{
			var stopRun = false;

			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.FileManager);

			FileStorageManager.Instance.UsingLocalMode += (o, e) =>
			{
				if (FileStorageManager.Instance.UseLocalMode) return;
				if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;
				stopRun = true;
			};

			FileStorageManager.Instance.Authorizing += (o, e) =>
			{
				var authManager = new AuthManager();
				authManager.Init();
				authManager.Auth(e);
			};

			AsyncHelper.RunSync(FileStorageManager.Instance.Init);

			if (stopRun) return;

			if (!FileStorageManager.Instance.Activated) return;

			AsyncHelper.RunSync(async () =>
			{
				await AppProfileManager.Instance.LoadProfile();
				await Configuration.RemoteResourceManager.Instance.LoadLocal();
				Settings.LoadLocal();
			});

			if (!String.IsNullOrEmpty(Settings.BackupPath) && Directory.Exists(Settings.BackupPath))
			{
				var connectionState = DatabaseConnectionHelper.GetConnectionState(Settings.BackupPath);
				if (connectionState.Type == ConnectionStateType.Busy)
				{
					PopupMessages.ShowWarning(
						String.Format(
							"{0} is currently updating the site.{1}Please try back again later, or ask the user to hurry up and finish…",
							connectionState.User,
							Environment.NewLine));
					return;
				}
			}

			AsyncHelper.RunSync(async () =>
			{
				await Configuration.RemoteResourceManager.Instance.LoadRemote();
				Settings.LoadRemote();
				InitBusinessObjects();
				await FileStorageManager.Instance.FixDataState();
			});
			Wallbin.LoadLibrary(Settings.BackupPath);
			SyncManager.SyncSilent();
		}

		public void ReloadData()
		{
			MainForm.pnContainer.Controls.Clear();
			DatabaseConnectionHelper.Connect(Settings.BackupPath);
			ProcessManager.Run("Loading Files...", (cancellationToken, formProgress) => Wallbin.LoadLibrary(Settings.BackupPath));
			ProcessManager.RunInQueue("Loading Wallbin...",
				() => MainForm.Invoke(new MethodInvoker(() => WallbinViews.Load())),
				() => MainForm.Invoke(new MethodInvoker(() => ShowTab())));
		}

		public void ReloadWallbinViews()
		{
			MainForm.pnContainer.Controls.Clear();
			ProcessManager.RunInQueue("Loading Wallbin...",
					() => MainForm.Invoke(new MethodInvoker(() => WallbinViews.Load())),
					() => MainForm.Invoke(new MethodInvoker(() => ShowTab())));
		}

		public void ShowTab(TabPageEnum tabPage = TabPageEnum.None)
		{
			MainForm.ribbonControl.Enabled = true;
			if (tabPage == TabPageEnum.None)
				tabPage = _activeTab;
			ProcessChanges();

			foreach (var pageController in _tabPages.Where(pageController => pageController.Key == _activeTab))
				pageController.Value.IsActive = false;

			_activeTab = tabPage;
			if (!_tabPages.ContainsKey(tabPage)) return;
			_tabPages[tabPage].ShowPage(tabPage);
		}

		public void ProcessChanges()
		{
			foreach (var pageController in _tabPages.Where(pageController => pageController.Key == _activeTab))
				pageController.Value.ProcessChanges();
		}

		public void ProcessClose()
		{
			ProcessChanges();
			DatabaseConnectionHelper.Disconnect(Settings.BackupPath);
		}

		public void ActivateApplication()
		{
			var mainFormHandle = IntPtr.Zero;
			foreach (var process in Process.GetProcesses().Where(x => x.ProcessName.Contains("FileManager")))
			{
				if (process.MainWindowHandle.ToInt32() == 0) continue;
				mainFormHandle = process.MainWindowHandle;
				break;
			}
			if (mainFormHandle.ToInt32() == 0) return;
			Utils.ActivateForm(mainFormHandle, true, false);
		}

		private void InitBusinessObjects()
		{
			RestServiceConnection.Load(Settings.WebServiceSite, "FileManagerData");
			Lists.Load();
			HelpManager.LoadHelpLinks();
		}

		private void LoadControllers()
		{
			_tabPages.Clear();
			TabWallbin = new WallbinPage();
			_tabPages.Add(TabPageEnum.Home, TabWallbin);
			_tabPages.Add(TabPageEnum.Tags, TabWallbin);
			_tabPages.Add(TabPageEnum.Security, TabWallbin);
			_tabPages.Add(TabPageEnum.Preferences, TabWallbin);
			_tabPages.Add(TabPageEnum.Settings, TabWallbin);
			_tabPages.Add(TabPageEnum.ProgramManager, TabWallbin);
			_tabPages.Add(TabPageEnum.Bundles, TabWallbin);

			TabVideo = new VideoPage();
			_tabPages.Add(TabPageEnum.VideoManager, TabVideo);

			TabCalendar = new CalendarPage();
			_tabPages.Add(TabPageEnum.Calendar, TabCalendar);

			ProcessManager.Run("Loading Controls...", (cancellationToken, formProgress) => MainForm.Invoke(new MethodInvoker(() =>
			{
				TabWallbin.InitController();
				TabVideo.InitController();
				TabCalendar.InitController();
			})));
		}
	}
}
