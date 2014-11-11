using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesDepot.CommonGUI.Floater;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.CoreObjects.ToolClasses;
using SalesDepot.ToolClasses;
using PowerPointHelper = SalesDepot.InteropClasses.PowerPointHelper;

namespace SalesDepot
{
	public class AppManager
	{
		#region Delegates
		public delegate void NoParamDelegate();

		public delegate void SingleParamDelegate(object parameter);
		#endregion

		private static readonly AppManager _instance = new AppManager();

		private AppManager()
		{
			if (!Directory.Exists(SettingsManager.Instance.TempPath))
				Directory.CreateDirectory(SettingsManager.Instance.TempPath);
			TempFolder = new DirectoryInfo(SettingsManager.Instance.TempPath);
			Log = new ApplicationLog(SettingsManager.Instance.LogFilePath);
			ActivityManager = new ActivityManager();
			HelpManager = new HelpManager(String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\SalesLibraryHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
		}

		public ApplicationLog Log { get; private set; }
		public DirectoryInfo TempFolder { get; set; }
		public ActivityManager ActivityManager { get; private set; }
		public HelpManager HelpManager { get; private set; }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		~AppManager()
		{
			DeleteFolder(TempFolder);
			if (!Directory.Exists(SettingsManager.Instance.TempPath))
				Directory.CreateDirectory(SettingsManager.Instance.TempPath);

			DeleteFolder(new DirectoryInfo(SettingsManager.Instance.LocalLibraryCacheFolder));
			if (!Directory.Exists(SettingsManager.Instance.LocalLibraryCacheFolder) && SettingsManager.Instance.UseRemoteConnection)
				Directory.CreateDirectory(SettingsManager.Instance.LocalLibraryCacheFolder);
		}

		private void ShowMainForm()
		{
			Application.Run(FormMain.Instance);
		}

		public void RunForm()
		{
			Instance.ActivityManager.AddUserActivity("Application run");
			if (!PowerPointHelper.Instance.IsLinkedWithApplication &&
				SettingsManager.Instance.PowerPointLaunchOptions == LinkLaunchOptions.Viewer &&
				SettingsManager.Instance.RunPowerPointWhenNeeded.HasValue &&
				SettingsManager.Instance.RunPowerPointWhenNeeded.Value)
				RunPowerPointLoader();
			ListManager.Instance.Init();
			ShowMainForm();
		}

		public void MinimizeForm(IntPtr handle)
		{
			WinAPIHelper.ShowWindow(handle, WindowShowStyle.Minimize);
		}

		public void ActivateMainForm()
		{
			IntPtr handle = RegistryHelper.SalesDepotHandle;
			if (handle.Equals(IntPtr.Zero))
			{
				handle = FormMain.Instance.Handle;
				if (handle.Equals(IntPtr.Zero))
				{
					Process[] proc = Process.GetProcessesByName("RemoteLibrary");
					if (proc.Length > 0)
						handle = proc[0].MainWindowHandle;
				}
			}
			Utils.ActivateForm(handle, RegistryHelper.MaximizeSalesDepot, false);
		}

		public void ActivatePowerPoint()
		{
			if (PowerPointHelper.Instance.PowerPointObject != null)
			{
				var powerPointHandle = new IntPtr(PowerPointHelper.Instance.PowerPointObject.HWND);
				WinAPIHelper.ShowWindow(powerPointHandle, WindowShowStyle.ShowMaximized);
				uint lpdwProcessId = 0;
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
				WinAPIHelper.SetForegroundWindow(powerPointHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			}
		}

		private void RunPowerPointLoader()
		{
			if (File.Exists(SettingsManager.Instance.PowerPointLoaderPath))
			{
				var process = new Process();
				process.StartInfo.FileName = SettingsManager.Instance.PowerPointLoaderPath;
				process.Start();
			}
			else
				ShowWarning("Couldn't find PowerPointLoader app");
		}

		public bool CheckPowerPointRunning(Func<bool> beforeRun = null)
		{
			if (PowerPointHelper.Instance.IsLinkedWithApplication) return true;
			if (beforeRun != null && !beforeRun()) return false;
			FloaterManager.Instance.ShowFloater(FormMain.Instance, SettingsManager.Instance.SalesDepotName, FormMain.Instance.FloaterLogo, RunPowerPointLoader);
			return false;
		}


		public void ShowInfo(string text)
		{
			MessageBox.Show(text, SettingsManager.Instance.UseRemoteConnection ? "Remote Sales Libraries" : "Sales Libraries", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, SettingsManager.Instance.UseRemoteConnection ? "Remote Sales Libraries" : "Sales Libraries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, SettingsManager.Instance.UseRemoteConnection ? "Remote Sales Libraries" : "Sales Libraries", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowInfoQuestion(string text)
		{
			return MessageBox.Show(text, SettingsManager.Instance.UseRemoteConnection ? "Remote Sales Libraries" : "Sales Libraries", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public void ReleaseComObject(object o)
		{
			try
			{
				Marshal.FinalReleaseComObject(o);
			}
			catch { }
			finally
			{
				o = null;
			}
		}

		private void DeleteFolder(DirectoryInfo folder)
		{
			try
			{
				foreach (DirectoryInfo subFolder in folder.GetDirectories())
					DeleteFolder(subFolder);
				FileInfo[] files = folder.GetFiles();
				foreach (FileInfo file in files)
				{
					try
					{
						if (File.Exists(file.FullName))
						{
							File.SetAttributes(file.FullName, FileAttributes.Normal);
							File.Delete(file.FullName);
						}
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName))
								File.Delete(file.FullName);
						}
						catch { }
					}
				}
				try
				{
					if (Directory.Exists(folder.FullName))
						Directory.Delete(folder.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName))
							Directory.Delete(folder.FullName, false);
					}
					catch { }
				}
			}
			catch { }
		}
	}
}