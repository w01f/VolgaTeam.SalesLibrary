using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Business.Contexts.Wallbin.Cloud;
using SalesLibraries.CloudAdmin.Business.Services;
using SalesLibraries.CloudAdmin.Configuration;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views;
using SalesLibraries.Common.Authorization;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Connection;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin;
using SalesLibraries.ServiceConnector.Services.Rest;
using SalesLibraries.ServiceConnector.Services.Soap;

namespace SalesLibraries.CloudAdmin.Controllers
{
	class MainController
	{
		private readonly Dictionary<TabPageEnum, IPageController> _tabPages = new Dictionary<TabPageEnum, IPageController>();
		private TabPageEnum _activeTab;

		public static MainController Instance { get; } = new MainController();

		public SettingsManager Settings { get; }
		public ListManager Lists { get; }
		public AuthManager AuthManager { get; }
		public SoapServiceConnection SoapServiceConnection { get; }
		public RestServiceConnection RestServiceConnection { get; }
		public CloudWallbinManager Wallbin { get; private set; }
		public HelpManager HelpManager { get; }

		public ViewManager WallbinViews { get; }

		public FormMain MainForm { get; }

		public BackgroundProcessManager ProcessManager { get; }
		public PopupMessageHelper PopupMessages { get; }

		#region Tab Pages
		public WallbinPage TabWallbin { get; private set; }
		//protected VideoPage TabVideo { get; private set; }
		#endregion

		private MainController()
		{
			Settings = new SettingsManager();
			Lists = new ListManager();
			AuthManager = new AuthManager();
			SoapServiceConnection = new SoapServiceConnection();
			RestServiceConnection = new RestServiceConnection();
			Wallbin = new CloudWallbinManager(RestServiceConnection);
			HelpManager = new HelpManager();
			WallbinViews = new ViewManager();
			MainForm = new FormMain();
			ProcessManager = new BackgroundProcessManager(MainForm, "Site Admin");
			PopupMessages = new PopupMessageHelper("Site Admin");
		}

		public void RunApplication()
		{
			var stopRun = false;

			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.CloudAdmin);

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
				AuthManager.Init();
				AuthManager.Auth(e);
			};

			ProcessManager.RunStartProcess(
				"Connecting to adSALEScloud…",
				cancellationToken => AsyncHelper.RunSync(FileStorageManager.Instance.Init));

			if (stopRun) return;

			if (FileStorageManager.Instance.Activated)
			{
				string progressTitle;
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
			}
			else
			{
				PopupMessages.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
				return;
			}

			RestResponse connectionResponce = null;
			ProcessManager.RunStartProcess(
					String.Format("Connecting to {0}", Settings.SiteLibrary),
					cancellationToken =>
					{
						connectionResponce = RestServiceConnection.DoRequest(new ConnectionGetRequestData
						{
							RequestType = ConnectionRequestType.Connect,
							LibraryName = Settings.SiteLibrary,
							UserName = AuthManager.Settings.Login
						});
					});
			if (connectionResponce == null)
			{
				PopupMessages.ShowWarning("Connection Error. Contact adSALESapps Support (help@adSALESapps.com)");
				return;
			}
			if (connectionResponce.Result == ResponseResult.Error)
			{
				var error = connectionResponce.GetData<RestError>();
				PopupMessages.ShowWarning(error.Message);
				return;
			}
			var connectionInfo = connectionResponce.GetData<ConnectionInfo>();
			if (connectionInfo.State == ConnectionState.Busy)
			{
				PopupMessages.ShowWarning(
					String.Format("The library {0} is busy by user: {1}",
						Settings.SiteLibrary,
						connectionInfo.User
						));
				return;
			}

			var libraryLoaded = false;
			ProcessManager.RunStartProcess(
					"Loading Library",
					cancellationToken =>
					{
						try
						{
							Wallbin.Init(connectionInfo, Configuration.RemoteResourceManager.Instance.LocalLibraryCacheFolder.LocalPath);
							Wallbin.CheckoutData();
							libraryLoaded = true;
						}
						catch (CloudLibraryException ex)
						{
							PopupMessages.ShowWarning(ex.ServiceErrorMessage);
						}
					});
			if (!libraryLoaded)
				return;

			MainForm.Shown += (o, e) =>
				{
					MainForm.InitForm();
					LoadControllers();

					ProcessManager.RunInQueue("Loading Wallbin...",
						() => MainForm.Invoke(new MethodInvoker(() => WallbinViews.Load())),
						() => MainForm.Invoke(new MethodInvoker(() => ShowTab(TabPageEnum.Home))));
				};
			MainForm.Closing += (o, e) =>
				  {
					  ProcessChanges();
					  ProcessManager.RunStartProcess(
						  String.Format("Syncing changes with {0}", Settings.SiteLibrary),
						  cancellationToken =>
						  {
							  Wallbin.CheckinData();
						  });
					  ProcessManager.RunStartProcess(
						  String.Format("Closing Connection to {0}", Settings.SiteLibrary),
						  cancellationToken =>
						  {
							  connectionResponce = RestServiceConnection.DoRequest(new ConnectionGetRequestData
							  {
								  RequestType = ConnectionRequestType.Disconnect,
								  LibraryName = Settings.SiteLibrary,
								  UserName = AuthManager.Settings.Login
							  });
						  });
				  };
			Application.Run(MainForm);
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

		private async Task InitBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();
			await Configuration.RemoteResourceManager.Instance.LoadLocal();
			await Configuration.RemoteResourceManager.Instance.LoadRemote();

			Settings.Load();
			SoapServiceConnection.Load(Settings.WebServiceSite);
			RestServiceConnection.Load(Settings.WebServiceSite);
			Lists.Load();
			HelpManager.LoadHelpLinks();

			await FileStorageManager.Instance.FixDataState();
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
			_tabPages.Add(TabPageEnum.Bundles, TabWallbin);

			//TabVideo = new VideoPage();
			//_tabPages.Add(TabPageEnum.VideoManager, TabVideo);

			ProcessManager.Run("Loading Controls...", cancelationToken => MainForm.Invoke(new MethodInvoker(() =>
			{
				TabWallbin.InitController();
				//TabVideo.InitController();
			})));
		}
	}
}
