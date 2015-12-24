using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.SiteManager.InteropClasses;

namespace SalesDepot.SiteManager
{
	public class AppManager
	{
		private static AppManager _instance = new AppManager();

		private AppManager()
		{
		}

		public static AppManager Instance
		{
			get
			{
				return _instance;
			}
		}

		public void RunForm()
		{
			Application.Run(FormMain.Instance);
		}

		public void ActivateForm(IntPtr handle, bool maximized, bool topMost)
		{
			WinAPIHelper.ShowWindow(handle, maximized ? WindowShowStyle.ShowMaximized : WindowShowStyle.ShowNormal);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				WinAPIHelper.MakeTopMost(handle);
			else
				WinAPIHelper.MakeNormal(handle);
		}

		public void ActivateMainForm()
		{
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("sitemanager")))
			{
				if (process.MainWindowHandle.ToInt32() != 0)
				{
					ActivateForm(process.MainWindowHandle, true, false);
					break;
				}
			}
		}

		public void ShowInfo(string text)
		{
			MessageBox.Show(text, "Site Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, "Site Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowQuestion(string text)
		{
			return MessageBox.Show(text, "Site Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, "Site Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}
	}
}
