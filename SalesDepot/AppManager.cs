using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesDepot.ConfigurationClasses;
using SalesDepot.InteropClasses;
using SalesDepot.ToolClasses;

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
		}

		public ApplicationLog Log { get; private set; }
		public DirectoryInfo TempFolder { get; set; }
		public ActivityManager ActivityManager { get; private set; }

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
			ListManager.Instance.Init();
			ShowMainForm();
		}

		public void ActivateForm(IntPtr handle, bool maximized, bool topMost)
		{
			WinAPIHelper.ShowWindow(handle, maximized ? WindowShowStyle.ShowMaximized : WindowShowStyle.ShowNormal);
			uint lpdwProcessId = 0;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				WinAPIHelper.MakeTopMost(handle);
			else
				WinAPIHelper.MakeNormal(handle);
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
			ActivateForm(handle, RegistryHelper.MaximizeSalesDepot, false);
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

		public void ActivateTaskbar()
		{
			var taskBarHandle = WinAPIHelper.FindWindow("Shell_traywnd", "");
			WinAPIHelper.ShowWindow(taskBarHandle, WindowShowStyle.Show);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(taskBarHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		public void RunPowerPointLoader()
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