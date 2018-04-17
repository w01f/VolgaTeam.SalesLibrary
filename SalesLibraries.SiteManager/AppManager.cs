using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Common.Authorization;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.ServiceConnector.Services.Soap;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager
{
	public class AppManager
	{
		public bool UseRemoteMode { get; }

		public BackgroundProcessManager ProcessManager { get; }
		public PopupMessageHelper PopupMessages { get; }
		public static AppManager Instance { get; } = new AppManager();

		private AppManager()
		{
			UseRemoteMode = File.Exists(Path.Combine(Path.GetDirectoryName(typeof(AppManager).Assembly.Location), "client.txt"));
			ProcessManager = new BackgroundProcessManager(() => FormMain.Instance);
			PopupMessages = new PopupMessageHelper("Site Manager");
		}

		public void RunForm()
		{
			bool appReady;
			if (UseRemoteMode)
			{
				var stopRun = false;
				appReady = false;
				string authUrl = "";

				AppProfileManager.Instance.InitApplication(AppTypeEnum.SiteManager);

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
					if (PopupMessages.ShowWarningQuestion("Server is not available. Do you want to continue to work in local mode?") !=
						DialogResult.Yes)
					{
						stopRun = true;
						Application.Exit();
					}
					ProcessManager.ResumeProcess();
				};

				FileStorageManager.Instance.Authorizing += (o, e) =>
				{
					e.Authorized = true;
					authUrl = e.AuthServer;
				};

				using (var form = new FormStart(PopupMessages.Title, false))
				{
					ProcessManager.RunWithProgress(form, false, (cancelletionToken, formProgress) =>
					{
						form.Invoke(new MethodInvoker(() => form.SetText("Connecting to adSALEScloud…")));

						AsyncHelper.RunSync(FileStorageManager.Instance.Init);

						if (stopRun) return;

						appReady = FileStorageManager.Instance.Activated;
						if (appReady)
						{
							var progressTitle = "Loading data...";
							form.Invoke(new MethodInvoker(() => form.SetText(progressTitle)));

							AsyncHelper.RunSync(InitRemoteBusinessObjects);
						}
					});
				}

				if (!String.IsNullOrEmpty(authUrl))
				{
					using (var authForm = new FormLogin(authUrl, SettingsManager.Instance.ApprovedUsers))
					{
						authForm.Logining += (o, e) =>
						{
							var authorized = false;
							var thread = new Thread(() =>
							{
								try
								{
									var connection = new SoapServiceConnection();
									connection.Load(authUrl);
									authorized = connection.IsAuthenticated(e.Login, e.Password);
									UsersEditPermissionsManager.Instance.LoadCurrentUserConfiguration(e.Login);
								}
								catch
								{
								}
							});
							thread.Start();
							while (thread.IsAlive)
								Application.DoEvents();
							e.Accepted = authorized;
						};
						appReady = authForm.ShowDialog() == DialogResult.OK;
					}
				}
				else
				{
					appReady = false;
				}
			}
			else
			{
				appReady = true;
				SettingsManager.Instance.Load();
				WebSiteManager.Instance.Load(SettingsManager.Instance.SitesListPath);
			}

			if (appReady)
				Application.Run(FormMain.Instance);
			else
				PopupMessages.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		private async Task InitRemoteBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();
			await ConfigurationClasses.RemoteResourceManager.Instance.Load();
			SettingsManager.Instance.Load();
			WebSiteManager.Instance.Load(ConfigurationClasses.RemoteResourceManager.Instance.SettingsFile.LocalPath);
			UsersEditPermissionsManager.Instance.Load(ConfigurationClasses.RemoteResourceManager.Instance.UsersEditPermissionsSettingsFile.LocalPath);

			if (ConfigurationClasses.RemoteResourceManager.Instance.SettingsFile.ExistsLocal())
				try
				{
					File.Delete(ConfigurationClasses.RemoteResourceManager.Instance.SettingsFile.LocalPath);
				}
				catch { }
		}

		public void ActivateMainForm()
		{
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("sitemanager")))
			{
				if (process.MainWindowHandle.ToInt32() == 0) continue;
				Utils.ActivateForm(process.MainWindowHandle, true, false);
				break;
			}
		}
	}
}
