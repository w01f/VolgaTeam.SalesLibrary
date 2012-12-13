using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

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
			CoreObjects.InteropClasses.WinAPIHelper.ShowWindow(handle, maximized ? CoreObjects.InteropClasses.WindowShowStyle.ShowMaximized : CoreObjects.InteropClasses.WindowShowStyle.ShowNormal);
			uint lpdwProcessId;
			CoreObjects.InteropClasses.WinAPIHelper.AttachThreadInput(CoreObjects.InteropClasses.WinAPIHelper.GetCurrentThreadId(), CoreObjects.InteropClasses.WinAPIHelper.GetWindowThreadProcessId(CoreObjects.InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			CoreObjects.InteropClasses.WinAPIHelper.SetForegroundWindow(handle);
			CoreObjects.InteropClasses.WinAPIHelper.AttachThreadInput(CoreObjects.InteropClasses.WinAPIHelper.GetCurrentThreadId(), CoreObjects.InteropClasses.WinAPIHelper.GetWindowThreadProcessId(CoreObjects.InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				CoreObjects.InteropClasses.WinAPIHelper.MakeTopMost(handle);
			else
				CoreObjects.InteropClasses.WinAPIHelper.MakeNormal(handle);
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
