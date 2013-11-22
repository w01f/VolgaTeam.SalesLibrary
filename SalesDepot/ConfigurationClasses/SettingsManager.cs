using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.ToolForms.Settings;

namespace SalesDepot.ConfigurationClasses
{
	public enum LinkLaunchOptions
	{
		Viewer = 0,
		Menu,
		Launch
	}

	[Flags]
	public enum EmailButtonsDisplayOptions
	{
		DisplayNone = 0x00,
		DisplayEmailBin = 0x02,
		DisplayQuickView = 0x04,
		DisplayViewOptions = 0x08
	}

	public enum BrowseType
	{
		Day = 0,
		Week,
		Month
	}

	public class SettingsManager
	{
		public const string ContentsSlideName = @"WizContents.ppt";
		public const string NoLogoFileName = @"no_logo.png";
		public const string PageLogoFileTemplate = @"page{0}.*";
		private static readonly SettingsManager _instance = new SettingsManager();
		private readonly string _appIDFile = string.Empty;
		private readonly string _configurationPath = string.Empty;
		private readonly string _defaultSettingsFilePath = string.Empty;
		private readonly string _defaultViewPath = string.Empty;

		private readonly string _defaultOpenFilePath = string.Empty;
		private readonly string _defaultSaveFilePath = string.Empty;
		private string _openFilePath = string.Empty;
		private string _saveFilePath = string.Empty;


		private readonly string _localLibraryLogoFolder = string.Empty;
		private readonly string _localLibraryRootFolder = string.Empty;
		private readonly string _localSettingsFilePath = string.Empty;
		private readonly string _remoteSettingsFilePath = string.Empty;
		private readonly string _viewButtonsPath = string.Empty;
		private string _remoteLibraryLogoFolder = string.Empty;
		private string _remoteLibraryRootFolder = string.Empty;

		private SettingsManager()
		{
			string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\sales depot\Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_localSettingsFilePath = Path.Combine(settingsFolderPath, "ApplicationSettings.xml");
			_remoteSettingsFilePath = Path.Combine(settingsFolderPath, "RemoteApplicationSettings.xml");
			_defaultSettingsFilePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ResetSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_defaultViewPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\defaultview.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_viewButtonsPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\viewbuttons.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_configurationPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Remote Libraries\Config.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_localLibraryRootFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_localLibraryLogoFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\!SD-Graphics\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_defaultOpenFilePath = string.Format(@"{0}\newlocaldirect.com\Sync\Temp", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_defaultSaveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			LocalLibraryCacheFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Remote Libraries\Local Cache", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DefaultWizardFileName = string.Format(@"{0}\newlocaldirect.com\New Biz Wizard\settings\DefaultWizard.ini", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ContentsSlidePath = string.Format(@"{0}\newlocaldirect.com\01. file sync\Master Wizards\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TempPath = _defaultOpenFilePath;
			IconPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdicon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LibraryLogoFolder = string.Empty;
			CalendarLogoPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\oc_logo.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DisclaimerPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Nielsen Permissible Use.pdf", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			PowerPointLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\PowerPointLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LogFilePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ApplicationLog.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			PermissionsFilePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Library_Security.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			DefaultWizard = string.Empty;
			SalesDepotName = string.Empty;
			KeyWordFilters = new KeyWordFileFilters();

			LoadAppID();

			ActivityFolder = string.Format(@"{0}\newlocaldirect.com\sync\outgoing\AppID-{1}\user_data\sales_library", new[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), AppID.ToString() });
			if (!Directory.Exists(ActivityFolder))
				Directory.CreateDirectory(ActivityFolder);

			#region Program Manager Settings
			ProgramScheduleShowInfo = true;
			ProgramScheduleOutputSettings = new ProgramOutputSettings();
			OutputCache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Program Schedules");
			#endregion

			QBuilderSettings = new QBuilderSettings();

			HiddenObjects = new List<string>();
			HiddenObjects.Add("!Old");
			HiddenObjects.Add(Constants.RegularPreviewContainersRootFolderName);
			HiddenObjects.Add(Constants.OvernightsCalendarRootFolderName);
			HiddenObjects.Add(Constants.ProgramManagerRootFolderName);
			HiddenObjects.Add(Constants.ExtraFoldersRootFolderName);
			HiddenObjects.Add(Constants.AttachmentsRootFolderName);
			HiddenObjects.Add("thumbs.db");
			HiddenObjects.Add("SalesDepotCache.xml");
		}

		public bool IsConfigured { get; set; }
		public bool UseRemoteConnection { get; set; }

		public string DefaultWizardFileName { get; set; }
		public string LocalLibraryCacheFolder { get; set; }
		public string ContentsSlidePath { get; set; }
		public string TempPath { get; set; }
		public string DefaultWizard { get; set; }
		public string SalesDepotName { get; set; }
		public string IconPath { get; set; }
		public string LibraryRootFolder { get; set; }
		public string LibraryLogoFolder { get; set; }
		public string CalendarLogoPath { get; set; }
		public string DisclaimerPath { get; set; }
		public string PowerPointLoaderPath { get; set; }
		public string LogFilePath { get; private set; }
		public string ActivityFolder { get; private set; }
		public string PermissionsFilePath { get; private set; }

		public string SelectedPackage { get; set; }
		public string SelectedLibrary { get; set; }
		public string SelectedPage { get; set; }
		public int SelectedCalendarYear { get; set; }
		public int FontSize { get; set; }
		public int RowSpace { get; set; }
		public int CalendarFontSize { get; set; }
		public bool ShowEmailBin { get; set; }
		public bool EmailBinSendAsPdf { get; set; }
		public bool EmailBinSendAsZip { get; set; }
		public bool EnablePdfConverting { get; set; }
		public bool OldStyleQuickView { get; set; }
		public LinkLaunchOptions PowerPointLaunchOptions { get; set; }
		public LinkLaunchOptions PDFLaunchOptions { get; set; }
		public LinkLaunchOptions WordLaunchOptions { get; set; }
		public LinkLaunchOptions ExcelLaunchOptions { get; set; }
		public LinkLaunchOptions VideoLaunchOptions { get; set; }
		public LinkLaunchOptions FolderLaunchOptions { get; set; }
		public EmailButtonsDisplayOptions EmailButtons { get; set; }

		public bool MultitabView { get; set; }
		public bool HomeView { get; set; }
		public bool ClassicView { get; set; }
		public bool ListView { get; set; }
		public bool AccordionView { get; set; }
		public bool SearchView { get; set; }
		public bool SolutionTitleView { get; set; }
		public bool SolutionDateView { get; set; }
		public bool SolutionTagsView { get; set; }
		public bool CalendarView { get; set; }
		public bool LastViewed { get; set; }
		public string ClassicTitle { get; set; }
		public string ClassicDescription { get; set; }
		public string ListTitle { get; set; }
		public string ListDescription { get; set; }
		public string AccordionTitle { get; set; }
		public string AccordionDescription { get; set; }
		public string SolutionTitle { get; set; }
		public string SolutionDescription { get; set; }
		public KeyWordFileFilters KeyWordFilters { get; private set; }

		public Guid AppID { get; set; }

		public List<string> HiddenObjects { get; private set; }

		public QBuilderSettings QBuilderSettings { get; private set; }

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


		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		#region Program Schedule Settings
		public string ProgramScheduleSelectedStation { get; set; }
		public BrowseType ProgramScheduleBrowseType { get; set; }
		public bool ProgramScheduleShowInfo { get; set; }
		public ProgramOutputSettings ProgramScheduleOutputSettings { get; private set; }
		public string OutputCache { get; private set; }
		#endregion

		private void LoadDefaultViewSettings()
		{
			XmlNode node;
			bool tempBool = false;

			if (File.Exists(_defaultViewPath))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(_defaultViewPath);
					IsConfigured = true;
				}
				catch { }

				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/Classic");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ClassicView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/List");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ListView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/Accordion");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						AccordionView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiontarget");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SolutionTagsView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiontitle");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SolutionTitleView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiondate");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SolutionDateView = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/lastviewed");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						LastViewed = tempBool;
				node = document.SelectSingleNode(@"/defaultview/SalesLibrary/emailbinpdf");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EmailBinSendAsPdf = tempBool;

				HomeView = ClassicView || ListView || AccordionView;
				SearchView = SolutionTagsView || SolutionDateView || SolutionTitleView;
			}
			if (LastViewed)
				ClassicView = true;
		}

		private void LoadViewButtonsSettings()
		{
			XmlNode node;

			if (File.Exists(_viewButtonsPath))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(_viewButtonsPath);
					IsConfigured = true;
				}
				catch { }

				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn1");
				if (node != null)
					ClassicTitle = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn1tooltip");
				if (node != null)
					ClassicDescription = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn2");
				if (node != null)
					ListTitle = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn2tooltip");
				if (node != null)
					ListDescription = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn3");
				if (node != null)
					SolutionTitle = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn3tooltip");
				if (node != null)
					SolutionDescription = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn4");
				if (node != null)
					AccordionTitle = node.InnerText;
				node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn4tooltip");
				if (node != null)
					AccordionDescription = node.InnerText;
			}
		}

		private void LoadConfiguration()
		{
			if (UseRemoteConnection)
			{
				XmlNode node;

				if (File.Exists(_configurationPath))
				{
					var document = new XmlDocument();
					try
					{
						document.Load(_configurationPath);
					}
					catch { }
					node = document.SelectSingleNode(@"/Config/Connection/Path");
					if (node != null)
						if (Directory.Exists(node.InnerText))
						{
							_remoteLibraryRootFolder = Path.Combine(node.InnerText, "Libraries");
							_remoteLibraryLogoFolder = Path.Combine(node.InnerText, "Graphics");
						}
				}
				if (!Directory.Exists(LocalLibraryCacheFolder))
					Directory.CreateDirectory(LocalLibraryCacheFolder);
			}
		}

		public void UpdateSetingsAccordingConfiguration()
		{
			LibraryRootFolder = UseRemoteConnection ? _remoteLibraryRootFolder : _localLibraryRootFolder;
			LibraryLogoFolder = UseRemoteConnection ? _remoteLibraryLogoFolder : _localLibraryLogoFolder;

			SolutionTagsView &= !UseRemoteConnection;
			SolutionDateView &= !UseRemoteConnection;
			SolutionTitleView |= (UseRemoteConnection & !ClassicView & !ListView & !AccordionView);

			SalesDepotName = UseRemoteConnection ? " Remote Sales Libraries" : SalesDepotName;
		}

		public void LoadSettings()
		{
			XmlNode node;
			int tempInt = 0;
			bool tempBool = false;
			LinkLaunchOptions tempLaunchOptions;
			EmailButtonsDisplayOptions tempEmailButtons;

			HomeView = true;
			ClassicView = true;
			ListView = false;
			AccordionView = false;
			SearchView = false;
			SolutionDateView = false;
			SolutionTagsView = false;
			SolutionTitleView = false;
			LastViewed = false;
			ClassicTitle = string.Empty;
			SolutionTitle = string.Empty;
			ListTitle = string.Empty;
			SelectedPackage = string.Empty;
			SelectedLibrary = string.Empty;
			SelectedPage = string.Empty;
			SelectedCalendarYear = 0;
			FontSize = 12;
			RowSpace = 1;
			CalendarFontSize = 10;
			EmailBinSendAsPdf = false;
			EmailBinSendAsZip = false;
			EnablePdfConverting = true;
			OldStyleQuickView = false;
			PowerPointLaunchOptions = LinkLaunchOptions.Viewer;
			PDFLaunchOptions = LinkLaunchOptions.Viewer;
			WordLaunchOptions = LinkLaunchOptions.Menu;
			ExcelLaunchOptions = LinkLaunchOptions.Menu;
			VideoLaunchOptions = LinkLaunchOptions.Viewer;
			FolderLaunchOptions = LinkLaunchOptions.Viewer;
			EmailButtons = EmailButtonsDisplayOptions.DisplayEmailBin | EmailButtonsDisplayOptions.DisplayQuickView | EmailButtonsDisplayOptions.DisplayViewOptions;
			MultitabView = true;

			LoadDefaultViewSettings();
			LoadViewButtonsSettings();
			LoadConfiguration();

			string settingsPath = UseRemoteConnection ? _remoteSettingsFilePath : _localSettingsFilePath;
			if (File.Exists(settingsPath))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(settingsPath);
					IsConfigured = true;
				}
				catch { }

				node = document.SelectSingleNode(@"/LocalSettings/SelectedPackage");
				if (node != null)
					SelectedPackage = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedLibrary");
				if (node != null)
					SelectedLibrary = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedPage");
				if (node != null)
					SelectedPage = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SelectedCalendarYear");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						SelectedCalendarYear = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/FontSize");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						FontSize = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/RowSpace");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						RowSpace = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/CalendarFontSize");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						CalendarFontSize = tempInt;
				node = document.SelectSingleNode(@"/LocalSettings/ShowEmailBin");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowEmailBin = tempBool;
				node = document.SelectSingleNode(@"/LocalSettings/EmailBinSendAsZip");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EmailBinSendAsZip = tempBool;
				node = document.SelectSingleNode(@"/LocalSettings/HomeView");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						HomeView = (LastViewed && tempBool) || (!LastViewed && HomeView);
				node = document.SelectSingleNode(@"/LocalSettings/SearchView");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SearchView = (LastViewed && tempBool) || (!LastViewed && SearchView);
				node = document.SelectSingleNode(@"/LocalSettings/CalendarView");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						CalendarView = tempBool;

				#region Program Shedule Settings
				node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleSelectedStation");
				if (node != null)
				{
					ProgramScheduleSelectedStation = node.InnerText;
				}

				node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleShowInfo");
				if (node != null)
				{
					if (bool.TryParse(node.InnerText, out tempBool))
						ProgramScheduleShowInfo = tempBool;
				}

				node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleBrowseType");
				if (node != null)
				{
					if (int.TryParse(node.InnerText, out tempInt))
						ProgramScheduleBrowseType = (BrowseType)tempInt;
				}

				node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleOutputSettings");
				if (node != null)
				{
					ProgramScheduleOutputSettings.Deserialize(node);
				}
				#endregion

				if (LastViewed || UseRemoteConnection)
				{
					node = document.SelectSingleNode(@"/LocalSettings/ClassicView");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							ClassicView = tempBool;
					node = document.SelectSingleNode(@"/LocalSettings/ListView");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							ListView = tempBool;
					node = document.SelectSingleNode(@"/LocalSettings/AccordionView");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							AccordionView = tempBool;
					node = document.SelectSingleNode(@"/LocalSettings/SolutionDateView");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							SolutionDateView = tempBool;
					node = document.SelectSingleNode(@"/LocalSettings/SolutionTagsView");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							SolutionTagsView = tempBool;
					node = document.SelectSingleNode(@"/LocalSettings/SolutionTitleView");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							SolutionTitleView = tempBool;
					node = document.SelectSingleNode(@"/LocalSettings/KeyWordFilters");
					if (node != null)
						KeyWordFilters.Deserialize(node);
				}

				if (File.Exists(_defaultSettingsFilePath))
				{
					var defaultDocument = new XmlDocument();
					try
					{
						defaultDocument.Load(_defaultSettingsFilePath);
						document = defaultDocument;
					}
					catch { }
				}

				node = document.SelectSingleNode(@"/LocalSettings/OldStyleQuickView");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						OldStyleQuickView = tempBool;
				node = document.SelectSingleNode(@"/LocalSettings/PowerPointLaunchOptions");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempLaunchOptions))
						PowerPointLaunchOptions = tempLaunchOptions;
				node = document.SelectSingleNode(@"/LocalSettings/PDFLaunchOptions");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempLaunchOptions))
						PDFLaunchOptions = tempLaunchOptions;
				node = document.SelectSingleNode(@"/LocalSettings/WordLaunchOptions");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempLaunchOptions))
						WordLaunchOptions = tempLaunchOptions;
				node = document.SelectSingleNode(@"/LocalSettings/ExcelLaunchOptions");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempLaunchOptions))
						ExcelLaunchOptions = tempLaunchOptions;
				node = document.SelectSingleNode(@"/LocalSettings/VideoLaunchOptions");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempLaunchOptions))
						VideoLaunchOptions = tempLaunchOptions;
				node = document.SelectSingleNode(@"/LocalSettings/FolderLaunchOptions");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempLaunchOptions))
						FolderLaunchOptions = tempLaunchOptions;
				node = document.SelectSingleNode(@"/LocalSettings/EmailButtons");
				if (node != null)
					if (Enum.TryParse(node.InnerText, out tempEmailButtons))
						EmailButtons = tempEmailButtons;
				node = document.SelectSingleNode(@"/LocalSettings/MultitabView");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						MultitabView = tempBool;
				node = document.SelectSingleNode(@"/LocalSettings/OpenFilePath");
				if (node != null)
					OpenFilePath = node.InnerText;
				node = document.SelectSingleNode(@"/LocalSettings/SaveFilePath");
				if (node != null)
					SaveFilePath = node.InnerText;
			}

			UpdateSetingsAccordingConfiguration();
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");
			xml.AppendLine(@"<SelectedPackage>" + SelectedPackage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPackage>");
			xml.AppendLine(@"<SelectedLibrary>" + SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
			xml.AppendLine(@"<SelectedPage>" + SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
			xml.AppendLine(@"<SelectedCalendarYear>" + SelectedCalendarYear.ToString() + @"</SelectedCalendarYear>");
			xml.AppendLine(@"<FontSize>" + FontSize.ToString() + @"</FontSize>");
			xml.AppendLine(@"<RowSpace>" + RowSpace.ToString() + @"</RowSpace>");
			xml.AppendLine(@"<CalendarFontSize>" + CalendarFontSize.ToString() + @"</CalendarFontSize>");
			xml.AppendLine(@"<ShowEmailBin>" + ShowEmailBin.ToString() + @"</ShowEmailBin>");
			xml.AppendLine(@"<EmailBinSendAsZip>" + EmailBinSendAsZip.ToString() + @"</EmailBinSendAsZip>");
			xml.AppendLine(@"<OldStyleQuickView>" + OldStyleQuickView.ToString() + @"</OldStyleQuickView>");
			xml.AppendLine(@"<PowerPointLaunchOptions>" + PowerPointLaunchOptions.ToString() + @"</PowerPointLaunchOptions>");
			xml.AppendLine(@"<PDFLaunchOptions>" + PDFLaunchOptions.ToString() + @"</PDFLaunchOptions>");
			xml.AppendLine(@"<WordLaunchOptions>" + WordLaunchOptions.ToString() + @"</WordLaunchOptions>");
			xml.AppendLine(@"<ExcelLaunchOptions>" + ExcelLaunchOptions.ToString() + @"</ExcelLaunchOptions>");
			xml.AppendLine(@"<VideoLaunchOptions>" + VideoLaunchOptions.ToString() + @"</VideoLaunchOptions>");
			xml.AppendLine(@"<FolderLaunchOptions>" + FolderLaunchOptions.ToString() + @"</FolderLaunchOptions>");
			xml.AppendLine(@"<EmailButtons>" + EmailButtons.ToString() + @"</EmailButtons>");
			xml.AppendLine(@"<MultitabView>" + MultitabView.ToString() + @"</MultitabView>");
			xml.AppendLine(@"<HomeView>" + HomeView.ToString() + @"</HomeView>");
			xml.AppendLine(@"<SearchView>" + SearchView.ToString() + @"</SearchView>");
			xml.AppendLine(@"<CalendarView>" + CalendarView.ToString() + @"</CalendarView>");
			if (!String.IsNullOrEmpty(_openFilePath))
				xml.AppendLine(@"<OpenFilePath>" + _openFilePath + @"</OpenFilePath>");
			if (!String.IsNullOrEmpty(_saveFilePath))
				xml.AppendLine(@"<SaveFilePath>" + _saveFilePath + @"</SaveFilePath>");
			if (LastViewed || UseRemoteConnection)
			{
				xml.AppendLine(@"<ClassicView>" + ClassicView.ToString() + @"</ClassicView>");
				xml.AppendLine(@"<ListView>" + ListView + @"</ListView>");
				xml.AppendLine(@"<AccordionView>" + AccordionView + @"</AccordionView>");
				xml.AppendLine(@"<SolutionDateView>" + SolutionDateView + @"</SolutionDateView>");
				xml.AppendLine(@"<SolutionTagsView>" + SolutionTagsView + @"</SolutionTagsView>");
				xml.AppendLine(@"<SolutionTitleView>" + SolutionTitleView + @"</SolutionTitleView>");
				xml.AppendLine(@"<KeyWordFilters>" + KeyWordFilters.Serialize() + @"</KeyWordFilters>");
			}

			#region Program Schedule Settings
			if (!string.IsNullOrEmpty(ProgramScheduleSelectedStation))
				xml.AppendLine(@"<ProgramScheduleSelectedStation>" + ProgramScheduleSelectedStation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ProgramScheduleSelectedStation>");
			xml.AppendLine(@"<ProgramScheduleShowInfo>" + ProgramScheduleShowInfo.ToString() + @"</ProgramScheduleShowInfo>");
			xml.AppendLine(@"<ProgramScheduleBrowseType>" + ((int)ProgramScheduleBrowseType).ToString() + @"</ProgramScheduleBrowseType>");
			xml.AppendLine(@"<ProgramScheduleOutputSettings>" + ProgramScheduleOutputSettings.Serialize() + @"</ProgramScheduleOutputSettings>");
			#endregion

			xml.AppendLine(@"</LocalSettings>");

			string settingsPath = UseRemoteConnection ? _remoteSettingsFilePath : _localSettingsFilePath;
			using (var sw = new StreamWriter(settingsPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}

			IsConfigured = true;
		}

		public void GetDefaultWizard()
		{
			var defaultWizardFile = new FileInfo(DefaultWizardFileName);
			if (defaultWizardFile.Exists)
				using (var sr = new StreamReader(defaultWizardFile.FullName))
					if ((DefaultWizard = sr.ReadLine()) == null)
						DefaultWizard = string.Empty;
		}

		public void GetSalesDepotName()
		{
			SalesDepotName = "Sales Libraries";
			XmlNode node;
			string filePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\SDSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);

				node = document.SelectSingleNode(@"/Root/LocalName");
				if (node != null)
					SalesDepotName = node.InnerText;
			}
		}

		public bool CheckLibraries()
		{
			bool result = false;
			if (Directory.Exists(LibraryRootFolder))
				result = ((new DirectoryInfo(LibraryRootFolder)).GetDirectories()).Length > 0;
			return result;
		}

		public void CheckStaticFolders()
		{
			try
			{
				string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (!Directory.Exists(localSettingsFolder))
					Directory.CreateDirectory(localSettingsFolder);
				if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot")))
					Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot"));
				if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot", "Settings")))
					Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot", "Settings"));
			}
			catch { }
		}

		private void LoadAppID()
		{
			AppID = Guid.Empty;
			string appIDPath = _appIDFile;
			if (File.Exists(appIDPath))
			{
				var document = new XmlDocument();
				document.Load(appIDPath);

				XmlNode node = document.SelectSingleNode(@"/AppID");
				if (node != null)
					if (!string.IsNullOrEmpty(node.InnerText))
						AppID = new Guid(node.InnerText);
			}
		}

		public void FileLocationSettings()
		{
			using (var form = new FormFileSettings())
			{
				form.ShowDialog();
			}
		}
	}

	public class KeyWordFileFilters
	{
		public KeyWordFileFilters()
		{
			AllFiles = true;
			PowerPoint = true;
			PDF = true;
			Excel = true;
			Word = true;
			Video = true;
			Url = true;
			Network = true;
			Folder = true;
		}

		public bool AllFiles { get; set; }
		public bool PowerPoint { get; set; }
		public bool PDF { get; set; }
		public bool Excel { get; set; }
		public bool Word { get; set; }
		public bool Video { get; set; }
		public bool Url { get; set; }
		public bool Network { get; set; }
		public bool Folder { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<AllFiles>" + AllFiles.ToString() + @"</AllFiles>");
			result.AppendLine(@"<PowerPoint>" + PowerPoint.ToString() + @"</PowerPoint>");
			result.AppendLine(@"<PDF>" + PDF.ToString() + @"</PDF>");
			result.AppendLine(@"<Excel>" + Excel.ToString() + @"</Excel>");
			result.AppendLine(@"<Word>" + Word.ToString() + @"</Word>");
			result.AppendLine(@"<Video>" + Video.ToString() + @"</Video>");
			result.AppendLine(@"<Url>" + Url.ToString() + @"</Url>");
			result.AppendLine(@"<Network>" + Network.ToString() + @"</Network>");
			result.AppendLine(@"<Folder>" + Folder.ToString() + @"</Folder>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "AllFiles":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							AllFiles = tempBool;
						break;
					case "PowerPoint":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							PowerPoint = tempBool;
						break;
					case "PDF":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							PDF = tempBool;
						break;
					case "Excel":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Excel = tempBool;
						break;
					case "Word":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Word = tempBool;
						break;
					case "Video":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Video = tempBool;
						break;
					case "Url":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Url = tempBool;
						break;
					case "Network":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Network = tempBool;
						break;
					case "Folder":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Folder = tempBool;
						break;
				}
			}
		}
	}
}