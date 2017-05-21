using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.BatchTagger.BusinessClasses.Dictionaries;
using SalesLibraries.BatchTagger.BusinessClasses.Helpers;
using SalesLibraries.BatchTagger.Configuration;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.BatchTagger
{
	public class AppManager
	{
		public ResourceManager Resources { get; private set; }
		public ConnectionManager Connections { get;  }
		public SearchTagList SearchTagList { get; }
		public FormMain MainForm { get; private set; }

		public static AppManager Instance { get; } = new AppManager();

		private AppManager()
		{
			Resources = new ResourceManager();
			Connections = new ConnectionManager();
			SearchTagList = new SearchTagList();
		}

		public void RunForm()
		{
			MainForm = new FormMain();
			Application.Run(MainForm);
		}

		public void LoadData()
		{
			Connections.Load();
			SearchTagList.Load();
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
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("batchtagger")))
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
			MessageBox.Show(text, "Batch Tagger", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, "Batch Tagger", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowQuestion(string text)
		{
			return MessageBox.Show(text, "Batch Tagger", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, "Batch Tagger", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}
	}
}
