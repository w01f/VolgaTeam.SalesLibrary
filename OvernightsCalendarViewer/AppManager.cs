using System;
using System.Diagnostics;
using System.Windows.Forms;
using OvernightsCalendarViewer.InteropClasses;

namespace OvernightsCalendarViewer
{
	public class AppManager
	{
		#region Delegates
		public delegate void NoParamDelegate();

		public delegate void SingleParamDelegate(object parameter);
		#endregion

		private static readonly AppManager _instance = new AppManager();

		private AppManager() { }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		private void ShowMainForm()
		{
			Application.Run(FormMain.Instance);
		}

		public void RunForm()
		{
			ShowMainForm();
		}

		public void ActivateMainForm()
		{
			IntPtr handle = FormMain.Instance.Handle;
			if (handle.Equals(IntPtr.Zero))
			{
				Process[] proc = Process.GetProcessesByName("Overnights");
				if (proc.Length > 0)
					handle = proc[0].MainWindowHandle;
			}
			ActivateForm(handle, true, false);
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

		public void ShowInfo(string text)
		{
			MessageBox.Show(text, "Overnights", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, "Overnights", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, "Overnights", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowInfoQuestion(string text)
		{
			return MessageBox.Show(text, "Overnights", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
	}
}