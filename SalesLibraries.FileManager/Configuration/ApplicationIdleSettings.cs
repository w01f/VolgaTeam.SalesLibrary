using System;
using System.Xml;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.FileManager.Configuration
{
	class ApplicationIdleSettings
	{
		public bool Enabled { get; private set; }
		public int InactivityTimeout { get; private set; }
		public bool SyncOnClose { get; private set; }

		public ApplicationIdleSettings()
		{
			Enabled = false;
		}

		public void Init(StorageFile settingsFile)
		{
			if (!settingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(settingsFile.LocalPath);

			Enabled = Boolean.Parse(document.SelectSingleNode(@"//config/EnableAutoClose")?.InnerText ?? "false");
			InactivityTimeout = Int32.Parse(document.SelectSingleNode(@"//config/InactivityTime")?.InnerText ?? "0") * 60 * 1000;
			SyncOnClose = Boolean.Parse(document.SelectSingleNode(@"//config/SyncOnClose")?.InnerText ?? "false");
		}
	}
}
