using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.PowerPoint;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.Common.OfficeInterops;

namespace SalesLibraries.Common.Helpers
{
	public class PowerPointManager
	{
		private static readonly PowerPointManager _instance = new PowerPointManager();

		public static PowerPointManager Instance
		{
			get { return _instance; }
		}

		public SettingsSourceEnum SettingsSource { get; private set; }
		public SlideSettings SlideSettings { get; private set; }

		public event EventHandler<SlideSettingsChangingEventArgs> SettingsChanging;
		public event EventHandler<EventArgs> SettingsChanged;

		private PowerPointManager()
		{
			SlideSettings = new SlideSettings();
		}

		public void Init()
		{
			if (PowerPointSingleton.Instance.Connect(false))
			{
				SettingsSource = SettingsSourceEnum.PowerPoint;
				SlideSettings = PowerPointSingleton.Instance.GetSlideSettings() ?? SlideSettings;
			}
			else
			{
				SettingsSource = SettingsSourceEnum.Application;
				GetDefaultSettings();
			}
		}

		public void RunPowerPointLoader()
		{
			Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("POWERPNT")).ToList().ForEach(p => p.Kill());

			var launcherTemplate = new StorageFile(RemoteResourceManager.Instance.LauncherTemplatesFolder.RelativePathParts.Merge(SlideSettings.LauncherTemplateName));
			if (!launcherTemplate.ExistsLocal())
				throw new FileNotFoundException(String.Format("There is no {0} found", launcherTemplate.Name));

			var process = new Process();
			process.StartInfo.FileName = launcherTemplate.LocalPath;
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			process.Start();
		}

		public void ActivatePowerPoint()
		{
			var powerPointHandle = PowerPointSingleton.Instance.WindowHandle;
			Utils.ActivateForm(powerPointHandle, true, false);
			WinAPIHelper.ShowWindow(powerPointHandle, WindowShowStyle.ShowMaximized);
			uint lpdwProcessId = 0;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(powerPointHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		private void GetDefaultSettings()
		{
			if (!RemoteResourceManager.Instance.DefaultSlideSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.DefaultSlideSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/Settings/Size");
			if (node != null)
				SlideSettings = SlideSettings.ReadFromString(node.InnerText.Trim());
		}
	}
}
