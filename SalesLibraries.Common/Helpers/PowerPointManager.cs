﻿using System;
using System.Diagnostics;
using System.IO;
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
		private PowerPointHelper _powerPointHelper;

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
			_powerPointHelper = PowerPointHelper.Instance;
			if (_powerPointHelper.IsLinkedWithApplication)
			{
				SettingsSource = SettingsSourceEnum.PowerPoint;
				GetActiveSettings();
			}
			else
			{
				SettingsSource = SettingsSourceEnum.Application;
				GetDefaultSettings();
			}
		}

		public void RunPowerPointLoader()
		{
			var launcherTemplate = new StorageFile(RemoteResourceManager.Instance.LauncherTemplatesFolder.RelativePathParts.Merge(SlideSettings.LauncherTemplateName));
			if (!launcherTemplate.ExistsLocal())
				throw new FileNotFoundException(String.Format("There is no {0} found", launcherTemplate.Name));

			var process = new Process();
			process.StartInfo.FileName = launcherTemplate.LocalPath;
			process.StartInfo.UseShellExecute = true;
			process.Start();
		}

		public void ActivatePowerPoint()
		{
			var powerPointHandle = _powerPointHelper.WindowHandle;
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

		private void GetActiveSettings()
		{
			SlideSettings = _powerPointHelper.GetSlideSettings() ?? SlideSettings;
		}
	}
}
