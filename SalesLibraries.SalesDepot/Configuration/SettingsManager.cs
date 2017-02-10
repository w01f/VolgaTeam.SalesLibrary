using System;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class SettingsManager
	{
		public const string NoLogoFileName = @"no_logo.png";
		public const string PageLogoFileTemplate = @"page{0}.*";

		private string _defaultOpenFilePath = String.Empty;
		private string _defaultSaveFilePath = String.Empty;
		private string _openFilePath = String.Empty;
		private string _saveFilePath = String.Empty;

		public event EventHandler<EventArgs> SettingsChanged;

		public WallbinViewSettings WallbinViewSettings { get; }
		public WallbinButtonsSettings WallbinButtonsSettings { get; }
		public CalendarViewSettings CalendarViewSettings { get; }
		public EmailBinSettings EmailBinSettings { get; private set; }
		public LinkLaunchSettings LinkLaunchSettings { get; private set; }
		public KeyWordFileFilters KeyWordFilters { get; private set; }

		public EmailButtonsDisplayOptionsEnum EmailButtons { get; set; }
		public bool? RunPowerPointWhenNeeded { get; set; }

		public string SalesDepotName => "Sales Libraries";

		public string OpenFilePath
		{
			get
			{
				if (!String.IsNullOrEmpty(_openFilePath) && Directory.Exists(_openFilePath))
					return _openFilePath;
				return _defaultOpenFilePath;
			}
			set { _openFilePath = value; }
		}

		public string SaveFilePath
		{
			get
			{
				if (!String.IsNullOrEmpty(_saveFilePath) && Directory.Exists(_saveFilePath))
					return _saveFilePath;
				return _defaultSaveFilePath;
			}
			set { _saveFilePath = value; }
		}

		public SettingsManager()
		{
			WallbinViewSettings = new WallbinViewSettings();
			WallbinButtonsSettings = new WallbinButtonsSettings();
			CalendarViewSettings = new CalendarViewSettings();
			EmailBinSettings = new EmailBinSettings();
			LinkLaunchSettings = new LinkLaunchSettings();
			KeyWordFilters = new KeyWordFileFilters();
		}

		public void LoadSettings()
		{
			_defaultOpenFilePath = Common.Helpers.RemoteResourceManager.Instance.TempFolder.LocalPath;
			_defaultSaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			WallbinViewSettings.LoadDefault();
			EmailBinSettings.LoadDefault();
			WallbinButtonsSettings.LoadDefault();

			EmailButtons = EmailButtonsDisplayOptionsEnum.DisplayEmailBin | EmailButtonsDisplayOptionsEnum.DisplayQuickView | EmailButtonsDisplayOptionsEnum.DisplayViewOptions;

			if (!Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;

			var document = new XmlDocument();
			try
			{
				document.Load(Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.LocalPath);
			}
			catch { }


			var node = document.SelectSingleNode(@"/LocalSettings/WallbinViewSettings");
			if (node != null)
				WallbinViewSettings.Deserialize(node);

			node = document.SelectSingleNode(@"/LocalSettings/CalendarViewSettings");
			if (node != null)
				CalendarViewSettings.Deserialize(node);

			node = document.SelectSingleNode(@"/LocalSettings/EmailBinSettings");
			if (node != null)
				EmailBinSettings.Deserialize(node);

			node = document.SelectSingleNode(@"/LocalSettings/LinkLaunchSettings");
			if (node != null)
				LinkLaunchSettings.Deserialize(node);

			node = document.SelectSingleNode(@"/LocalSettings/KeyWordFilters");
			if (node != null)
				KeyWordFilters.Deserialize(node);

			bool tempBool;
			node = document.SelectSingleNode(@"/LocalSettings/RunPowerPointWhenNeeded");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					RunPowerPointWhenNeeded = tempBool;

			node = document.SelectSingleNode(@"/LocalSettings/EmailButtons");
			if (node != null)
			{
				EmailButtonsDisplayOptionsEnum tempEmailButtons;
				if (Enum.TryParse(node.InnerText, out tempEmailButtons))
					EmailButtons = tempEmailButtons;
			}

			node = document.SelectSingleNode(@"/LocalSettings/OpenFilePath");
			if (node != null)
				OpenFilePath = node.InnerText;

			node = document.SelectSingleNode(@"/LocalSettings/SaveFilePath");
			if (node != null)
				SaveFilePath = node.InnerText;
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");

			xml.AppendLine(@"<WallbinViewSettings>" + WallbinViewSettings.Serialize() + @"</WallbinViewSettings>");
			xml.AppendLine(@"<CalendarViewSettings>" + CalendarViewSettings.Serialize() + @"</CalendarViewSettings>");
			xml.AppendLine(@"<EmailBinSettings>" + EmailBinSettings.Serialize() + @"</EmailBinSettings>");
			xml.AppendLine(@"<KeyWordFilters>" + KeyWordFilters.Serialize() + @"</KeyWordFilters>");
			xml.AppendLine(@"<LinkLaunchSettings>" + LinkLaunchSettings.Serialize() + @"</LinkLaunchSettings>");

			xml.AppendLine(@"<EmailButtons>" + EmailButtons + @"</EmailButtons>");
			if (RunPowerPointWhenNeeded.HasValue)
				xml.AppendLine(@"<RunPowerPointWhenNeeded>" + RunPowerPointWhenNeeded.Value + @"</RunPowerPointWhenNeeded>");
			if (!String.IsNullOrEmpty(_openFilePath))
				xml.AppendLine(@"<OpenFilePath>" + _openFilePath + @"</OpenFilePath>");
			if (!String.IsNullOrEmpty(_saveFilePath))
				xml.AppendLine(@"<SaveFilePath>" + _saveFilePath + @"</SaveFilePath>");

			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(Common.Helpers.RemoteResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}

			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
