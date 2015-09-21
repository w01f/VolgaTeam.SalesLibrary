using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FileManager.BusinessClasses;
using FileManager.ToolForms.Settings;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;

namespace FileManager
{
	internal class AppManager
	{
		private static readonly AppManager instance = new AppManager();

		public HelpManager HelpManager { get; private set; }

		private AppManager()
		{
			HelpManager = new HelpManager(String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\FileManagerHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
		}

		public static AppManager Instance
		{
			get { return instance; }
		}

		private bool Init()
		{
			var result = false;
			
			AppModeManager.Instance.Load();
			ServiceConnector.Instance.Init();
			SettingsManager.Instance.LoadLocalSettings();
			
			if (string.IsNullOrEmpty(SettingsManager.Instance.BackupPath) || !Directory.Exists(SettingsManager.Instance.BackupPath))
			{
				Instance.ShowWarning("Primary Backup Root is not set or unavailable.\nYou need to configure application");
				using (var form = new FormPaths())
				{
					if (form.ShowDialog() == DialogResult.Cancel)
					{
						Instance.ShowWarning("Application is not configured and will be closed");
					}
					else
						result = true;
				}
			}
			else
				result = true;
			return result;
		}

		public void RunForm()
		{
			if (!Init()) return;
			FormMain.Instance.ShowInTaskbar = true;
			Application.Run(FormMain.Instance);
		}

		public void RunSilent()
		{
			AppModeManager.Instance.Load();
			ServiceConnector.Instance.Init();
			CloudResorcesManager.Instance.LoadResorces();
			SettingsManager.Instance.LoadLocalSettings();
			ListManager.Instance.Init();
			if (String.IsNullOrEmpty(SettingsManager.Instance.BackupPath) || 
				!Directory.Exists(SettingsManager.Instance.BackupPath)) return;
			SilentSyncManager.Run();
		}

		public void ActivateMainForm()
		{
			IntPtr mainFormHandle = IntPtr.Zero;
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.Contains("FileManager")))
			{
				if (process.MainWindowHandle.ToInt32() != 0)
				{
					mainFormHandle = process.MainWindowHandle;
					break;
				}
			}
			if (mainFormHandle.ToInt32() != 0)
			{
				WinAPIHelper.ShowWindow(mainFormHandle, WindowShowStyle.ShowMaximized);
				WinAPIHelper.MakeTopMost(mainFormHandle);
				WinAPIHelper.MakeNormal(mainFormHandle);
				uint lpdwProcessId = 0;
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
				WinAPIHelper.SetForegroundWindow(mainFormHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			}
		}

		public void ShowInfo(string text)
		{
			MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowQuestion(string text)
		{
			return MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		#region Nested type: NoParamsDelegate
		public delegate void NoParamsDelegate();
		#endregion
	}
}