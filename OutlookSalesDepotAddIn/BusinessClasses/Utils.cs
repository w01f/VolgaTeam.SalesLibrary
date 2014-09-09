using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.CoreObjects.InteropClasses;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class Utils
	{
		#region Delegates
		public delegate void NoParamDelegate();
		public delegate void SingleParamDelegate(object parameter);
		#endregion

		private static readonly Utils _instance = new Utils();
		public static Utils Manager
		{
			get { return _instance; }
		}

		public DirectoryInfo TempFolder { get; set; }

		private Utils()
		{
			TempFolder = new DirectoryInfo(SettingsManager.Instance.TempPath);
		}

		~Utils()
		{
			DeleteFolder(TempFolder);
			if (!Directory.Exists(SettingsManager.Instance.TempPath))
				Directory.CreateDirectory(SettingsManager.Instance.TempPath);
		}

		#region Static Methods
		public static void ShowInfo(string text)
		{
			MessageBox.Show(text, SettingsManager.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void ShowWarning(string text)
		{
			MessageBox.Show(text, SettingsManager.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, SettingsManager.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public static DialogResult ShowInfoQuestion(string text)
		{
			return MessageBox.Show(text, SettingsManager.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public static void ReleaseComObject(object o)
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

		public static void ActivateForm(IntPtr handle, bool maximized, bool topMost)
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

		private static void DeleteFolder(DirectoryInfo folder)
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
		#endregion
	}
}
