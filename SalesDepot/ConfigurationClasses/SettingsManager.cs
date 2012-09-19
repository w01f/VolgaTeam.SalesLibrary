using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
        private static SettingsManager _instance = new SettingsManager();

        private const string DefaultUserName = "Default";
        public const string ContentsSlideName = @"WizContents.ppt";
        public const string NoLogoFileName = @"no_logo.png";
        public const string PageLogoFileTemplate = @"page{0}.*";

        private string _localSettingsFilePath = string.Empty;
        private string _remoteSettingsFilePath = string.Empty;
        private string _defaultSettingsFilePath = string.Empty;
        private string _defaultViewPath = string.Empty;
        private string _viewButtonsPath = string.Empty;
        private string _configurationPath = string.Empty;

        private string _localLibraryRootFolder = string.Empty;
        private string _remoteLibraryRootFolder = string.Empty;

        private string _localLibraryLogoFolder = string.Empty;
        private string _remoteLibraryLogoFolder = string.Empty;

        private string _appIDFile = string.Empty;
        private string _approvedLibrariesFile = string.Empty;

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

        public string SelectedPackage { get; set; }
        public string SelectedLibrary { get; set; }
        public string SelectedPage { get; set; }
        public int SelectedCalendarYear { get; set; }
        public int FontSize { get; set; }
        public int CalendarFontSize { get; set; }
        public bool ShowEmailBin { get; set; }
        public bool EmailBinSendAsPdf { get; set; }
        public bool EmailBinSendAsZip { get; set; }
        public bool EnablePdfConverting { get; set; }
        public bool LaunchPPT { get; set; }
        public bool OldStyleQuickView { get; set; }
        public LinkLaunchOptions PowerPointLaunchOptions { get; set; }
        public LinkLaunchOptions PDFLaunchOptions { get; set; }
        public LinkLaunchOptions WordLaunchOptions { get; set; }
        public LinkLaunchOptions ExcelLaunchOptions { get; set; }
        public LinkLaunchOptions VideoLaunchOptions { get; set; }
        public EmailButtonsDisplayOptions EmailButtons { get; set; }

        public bool MultitabView { get; set; }
        public bool ClassicView { get; set; }
        public bool ListView { get; set; }
        public bool SolutionTitleView { get; set; }
        public bool SolutionDateView { get; set; }
        public bool SolutionTagsView { get; set; }
        public bool CalendarView { get; set; }
        public bool LastViewed { get; set; }
        public string ClassicTitle { get; set; }
        public string ListTitle { get; set; }
        public string SolutionTitle { get; set; }
        public KeyWordFileFilters KeyWordFilters { get; private set; }

        #region Program Schedule Settings
        public string ProgramScheduleSelectedStation { get; set; }
        public BrowseType ProgramScheduleBrowseType { get; set; }
        public bool ProgramScheduleShowInfo { get; set; }
        public ProgramOutputSettings ProgramScheduleOutputSettings { get; private set; }
        public string OutputCache { get; private set; }
        #endregion

        public Guid AppID { get; set; }
        public List<string> ApprovedLibraries { get; private set; }

        public List<string> HiddenObjects { get; private set; }

        public bool SolutionView
        {
            get
            {
                return !(this.ClassicView | this.ListView);
            }
        }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SettingsManager()
        {
            string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\sales depot\Settings", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _localSettingsFilePath = Path.Combine(settingsFolderPath, "ApplicationSettings.xml");
            _remoteSettingsFilePath = Path.Combine(settingsFolderPath, "RemoteApplicationSettings.xml");
            _defaultSettingsFilePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ResetSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _defaultViewPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\defaultview.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _viewButtonsPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\viewbuttons.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _approvedLibrariesFile = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ApprovedLibraries.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _configurationPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Remote Libraries\Config.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            _localLibraryRootFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _localLibraryLogoFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\!SD-Graphics\libraries", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.LocalLibraryCacheFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Remote Libraries\Local Cache", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.DefaultWizardFileName = string.Format(@"{0}\newlocaldirect.com\New Biz Wizard\settings\DefaultWizard.ini", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.ContentsSlidePath = string.Format(@"{0}\newlocaldirect.com\01. file sync\Master Wizards\", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.TempPath = string.Format(@"{0}\newlocaldirect.com\Sync\Temp", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.IconPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdicon.ico", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.LibraryLogoFolder = string.Empty;
            this.CalendarLogoPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\oc_logo.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.DisclaimerPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Nielsen Permissible Use.pdf", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.PowerPointLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\PowerPointLoader.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.LogFilePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ApplicationLog.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            this.DefaultWizard = string.Empty;
            this.SalesDepotName = string.Empty;
            this.KeyWordFilters = new KeyWordFileFilters();

            this.ApprovedLibraries = new List<string>();
            LoadAppID();

            this.ActivityFolder = string.Format(@"{0}\newlocaldirect.com\sync\outgoing\AppID-{1}\user_data\sales_library", new string[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), this.AppID.ToString() });
            if (!Directory.Exists(this.ActivityFolder))
                Directory.CreateDirectory(this.ActivityFolder);

            #region Program Manager Settings
            this.ProgramScheduleShowInfo = true;
            this.ProgramScheduleOutputSettings = new ConfigurationClasses.ProgramOutputSettings();
            this.OutputCache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Program Schedules");
            #endregion

            this.HiddenObjects = new List<string>();
            this.HiddenObjects.Add("!Old");
            this.HiddenObjects.Add(CoreObjects.Constants.RegularPreviewContainersRootFolderName);
            this.HiddenObjects.Add(CoreObjects.Constants.OvernightsCalendarRootFolderName);
            this.HiddenObjects.Add(CoreObjects.Constants.ProgramManagerRootFolderName);
            this.HiddenObjects.Add(CoreObjects.Constants.ExtraFoldersRootFolderName);
            this.HiddenObjects.Add("thumbs.db");
            this.HiddenObjects.Add("SalesDepotCache.xml");
        }

        private void LoadDefaultViewSettings()
        {
            XmlNode node;
            bool tempBool = false;

            if (File.Exists(_defaultViewPath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_defaultViewPath);
                    this.IsConfigured = true;
                }
                catch
                {
                }

                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/Classic");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ClassicView = tempBool;
                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/List");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ListView = tempBool;
                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiontarget");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.SolutionTagsView = tempBool;
                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiontitle");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.SolutionTitleView = tempBool;
                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/solutiondate");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.SolutionDateView = tempBool;
                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/lastviewed");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.LastViewed = tempBool;
                node = document.SelectSingleNode(@"/defaultview/SalesLibrary/emailbinpdf");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.EmailBinSendAsPdf = tempBool;
            }
            if (this.LastViewed)
                this.ClassicView = true;
        }

        private void LoadViewButtonsSettings()
        {
            XmlNode node;

            if (File.Exists(_viewButtonsPath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_viewButtonsPath);
                    this.IsConfigured = true;
                }
                catch
                {
                }

                node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn1");
                if (node != null)
                    this.ClassicTitle = node.InnerText;
                node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn2");
                if (node != null)
                    this.ListTitle = node.InnerText;
                node = document.SelectSingleNode(@"/ViewButtons/ribbonlabel/btn3");
                if (node != null)
                    this.SolutionTitle = node.InnerText;
            }
        }

        private void LoadConfiguration()
        {
            if (this.UseRemoteConnection)
            {
                XmlNode node;

                if (File.Exists(_configurationPath))
                {
                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load(_configurationPath);
                    }
                    catch
                    {
                    }
                    node = document.SelectSingleNode(@"/Config/Connection/Path");
                    if (node != null)
                        if (Directory.Exists(node.InnerText))
                        {
                            _remoteLibraryRootFolder = Path.Combine(node.InnerText, "Libraries");
                            _remoteLibraryLogoFolder = Path.Combine(node.InnerText, "Graphics");
                        }
                }
                if (!Directory.Exists(this.LocalLibraryCacheFolder))
                    Directory.CreateDirectory(this.LocalLibraryCacheFolder);
            }
        }

        public void UpdateSetingsAccordingConfiguration()
        {
            this.LibraryRootFolder = this.UseRemoteConnection ? _remoteLibraryRootFolder : _localLibraryRootFolder;
            this.LibraryLogoFolder = this.UseRemoteConnection ? _remoteLibraryLogoFolder : _localLibraryLogoFolder;

            this.SolutionTagsView &= !this.UseRemoteConnection;
            this.SolutionDateView &= !this.UseRemoteConnection;
            this.SolutionTitleView |= (this.UseRemoteConnection & !this.ClassicView & !this.ListView);

            this.SalesDepotName = this.UseRemoteConnection ? " Remote Sales Libraries" : this.SalesDepotName;
        }

        public void LoadSettings()
        {
            XmlNode node;
            int tempInt = 0;
            bool tempBool = false;
            LinkLaunchOptions tempLaunchOptions;
            EmailButtonsDisplayOptions tempEmailButtons;

            this.ClassicView = true;
            this.ListView = false;
            this.SolutionDateView = false;
            this.SolutionTagsView = false;
            this.SolutionTitleView = false;
            this.LastViewed = false;
            this.ClassicTitle = string.Empty;
            this.SolutionTitle = string.Empty;
            this.ListTitle = string.Empty;
            this.SelectedPackage = string.Empty;
            this.SelectedLibrary = string.Empty;
            this.SelectedPage = string.Empty;
            this.SelectedCalendarYear = 0;
            this.FontSize = 12;
            this.CalendarFontSize = 10;
            this.EmailBinSendAsPdf = false;
            this.EmailBinSendAsZip = false;
            this.EnablePdfConverting = true;
            this.OldStyleQuickView = false;
            this.LaunchPPT = true;
            this.PowerPointLaunchOptions = LinkLaunchOptions.Viewer;
            this.PDFLaunchOptions = LinkLaunchOptions.Viewer;
            this.WordLaunchOptions = LinkLaunchOptions.Menu;
            this.ExcelLaunchOptions = LinkLaunchOptions.Menu;
            this.VideoLaunchOptions = LinkLaunchOptions.Viewer;
            this.EmailButtons = EmailButtonsDisplayOptions.DisplayEmailBin | EmailButtonsDisplayOptions.DisplayQuickView | EmailButtonsDisplayOptions.DisplayViewOptions;
            this.MultitabView = true;

            LoadDefaultViewSettings();
            LoadViewButtonsSettings();
            LoadConfiguration();
            LoadApprovedLibraries();

            string settingsPath = this.UseRemoteConnection ? _remoteSettingsFilePath : _localSettingsFilePath;
            if (File.Exists(settingsPath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(settingsPath);
                    this.IsConfigured = true;
                }
                catch
                {
                }

                node = document.SelectSingleNode(@"/LocalSettings/SelectedPackage");
                if (node != null)
                    this.SelectedPackage = node.InnerText;
                node = document.SelectSingleNode(@"/LocalSettings/SelectedLibrary");
                if (node != null)
                    this.SelectedLibrary = node.InnerText;
                node = document.SelectSingleNode(@"/LocalSettings/SelectedPage");
                if (node != null)
                    this.SelectedPage = node.InnerText;
                node = document.SelectSingleNode(@"/LocalSettings/SelectedCalendarYear");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.SelectedCalendarYear = tempInt;
                node = document.SelectSingleNode(@"/LocalSettings/FontSize");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.FontSize = tempInt;
                node = document.SelectSingleNode(@"/LocalSettings/CalendarFontSize");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.CalendarFontSize = tempInt;
                node = document.SelectSingleNode(@"/LocalSettings/ShowEmailBin");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ShowEmailBin = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/EmailBinSendAsZip");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.EmailBinSendAsZip = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/CalendarView");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.CalendarView = tempBool;

                #region Program Shedule Settings
                node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleSelectedStation");
                if (node != null)
                {
                    this.ProgramScheduleSelectedStation = node.InnerText;
                }

                node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleShowInfo");
                if (node != null)
                {
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ProgramScheduleShowInfo = tempBool;
                }

                node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleBrowseType");
                if (node != null)
                {
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.ProgramScheduleBrowseType = (BrowseType)tempInt;
                }

                node = document.SelectSingleNode(@"/LocalSettings/ProgramScheduleOutputSettings");
                if (node != null)
                {
                    this.ProgramScheduleOutputSettings.Deserialize(node);
                }
                #endregion

                if (this.LastViewed || this.UseRemoteConnection)
                {
                    node = document.SelectSingleNode(@"/LocalSettings/ClassicView");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.ClassicView = tempBool;
                    node = document.SelectSingleNode(@"/LocalSettings/ListView");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.ListView = tempBool;
                    node = document.SelectSingleNode(@"/LocalSettings/SolutionDateView");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.SolutionDateView = tempBool;
                    node = document.SelectSingleNode(@"/LocalSettings/SolutionTagsView");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.SolutionTagsView = tempBool;
                    node = document.SelectSingleNode(@"/LocalSettings/SolutionTitleView");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.SolutionTitleView = tempBool;
                    node = document.SelectSingleNode(@"/LocalSettings/KeyWordFilters");
                    if (node != null)
                        this.KeyWordFilters.Deserialize(node);
                }

                if (File.Exists(_defaultSettingsFilePath))
                {
                    XmlDocument defaultDocument = new XmlDocument();
                    try
                    {
                        defaultDocument.Load(_defaultSettingsFilePath);
                        document = defaultDocument;
                    }
                    catch
                    {
                    }
                }

                node = document.SelectSingleNode(@"/LocalSettings/OldStyleQuickView");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.OldStyleQuickView = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/LaunchPPT");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.LaunchPPT = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/PowerPointLaunchOptions");
                if (node != null)
                    if (Enum.TryParse<LinkLaunchOptions>(node.InnerText, out tempLaunchOptions))
                        this.PowerPointLaunchOptions = tempLaunchOptions;
                node = document.SelectSingleNode(@"/LocalSettings/PDFLaunchOptions");
                if (node != null)
                    if (Enum.TryParse<LinkLaunchOptions>(node.InnerText, out tempLaunchOptions))
                        this.PDFLaunchOptions = tempLaunchOptions;
                node = document.SelectSingleNode(@"/LocalSettings/WordLaunchOptions");
                if (node != null)
                    if (Enum.TryParse<LinkLaunchOptions>(node.InnerText, out tempLaunchOptions))
                        this.WordLaunchOptions = tempLaunchOptions;
                node = document.SelectSingleNode(@"/LocalSettings/ExcelLaunchOptions");
                if (node != null)
                    if (Enum.TryParse<LinkLaunchOptions>(node.InnerText, out tempLaunchOptions))
                        this.ExcelLaunchOptions = tempLaunchOptions;
                node = document.SelectSingleNode(@"/LocalSettings/VideoLaunchOptions");
                if (node != null)
                    if (Enum.TryParse<LinkLaunchOptions>(node.InnerText, out tempLaunchOptions))
                        this.VideoLaunchOptions = tempLaunchOptions;
                node = document.SelectSingleNode(@"/LocalSettings/EmailButtons");
                if (node != null)
                    if (Enum.TryParse<EmailButtonsDisplayOptions>(node.InnerText, out tempEmailButtons))
                        this.EmailButtons = tempEmailButtons;
                node = document.SelectSingleNode(@"/LocalSettings/MultitabView");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.MultitabView = tempBool;
            }

            UpdateSetingsAccordingConfiguration();
        }

        public void SaveSettings()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<LocalSettings>");
            xml.AppendLine(@"<SelectedPackage>" + this.SelectedPackage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPackage>");
            xml.AppendLine(@"<SelectedLibrary>" + this.SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
            xml.AppendLine(@"<SelectedPage>" + this.SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
            xml.AppendLine(@"<SelectedCalendarYear>" + this.SelectedCalendarYear.ToString() + @"</SelectedCalendarYear>");
            xml.AppendLine(@"<FontSize>" + this.FontSize.ToString() + @"</FontSize>");
            xml.AppendLine(@"<CalendarFontSize>" + this.CalendarFontSize.ToString() + @"</CalendarFontSize>");
            xml.AppendLine(@"<ShowEmailBin>" + this.ShowEmailBin.ToString() + @"</ShowEmailBin>");
            xml.AppendLine(@"<EmailBinSendAsZip>" + this.EmailBinSendAsZip.ToString() + @"</EmailBinSendAsZip>");
            xml.AppendLine(@"<OldStyleQuickView>" + this.OldStyleQuickView.ToString() + @"</OldStyleQuickView>");
            xml.AppendLine(@"<LaunchPPT>" + this.LaunchPPT.ToString() + @"</LaunchPPT>");
            xml.AppendLine(@"<PowerPointLaunchOptions>" + this.PowerPointLaunchOptions.ToString() + @"</PowerPointLaunchOptions>");
            xml.AppendLine(@"<PDFLaunchOptions>" + this.PDFLaunchOptions.ToString() + @"</PDFLaunchOptions>");
            xml.AppendLine(@"<WordLaunchOptions>" + this.WordLaunchOptions.ToString() + @"</WordLaunchOptions>");
            xml.AppendLine(@"<ExcelLaunchOptions>" + this.ExcelLaunchOptions.ToString() + @"</ExcelLaunchOptions>");
            xml.AppendLine(@"<VideoLaunchOptions>" + this.VideoLaunchOptions.ToString() + @"</VideoLaunchOptions>");
            xml.AppendLine(@"<EmailButtons>" + this.EmailButtons.ToString() + @"</EmailButtons>");
            xml.AppendLine(@"<MultitabView>" + this.MultitabView.ToString() + @"</MultitabView>");
            xml.AppendLine(@"<CalendarView>" + this.CalendarView.ToString() + @"</CalendarView>");
            if (this.LastViewed || this.UseRemoteConnection)
            {
                xml.AppendLine(@"<ClassicView>" + this.ClassicView.ToString() + @"</ClassicView>");
                xml.AppendLine(@"<ListView>" + this.ListView.ToString() + @"</ListView>");
                xml.AppendLine(@"<SolutionDateView>" + this.SolutionDateView.ToString() + @"</SolutionDateView>");
                xml.AppendLine(@"<SolutionTagsView>" + this.SolutionTagsView.ToString() + @"</SolutionTagsView>");
                xml.AppendLine(@"<SolutionTitleView>" + this.SolutionTitleView.ToString() + @"</SolutionTitleView>");
                xml.AppendLine(@"<KeyWordFilters>" + this.KeyWordFilters.Serialize() + @"</KeyWordFilters>");
            }

            #region Program Schedule Settings
            if (!string.IsNullOrEmpty(this.ProgramScheduleSelectedStation))
                xml.AppendLine(@"<ProgramScheduleSelectedStation>" + this.ProgramScheduleSelectedStation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ProgramScheduleSelectedStation>");
            xml.AppendLine(@"<ProgramScheduleShowInfo>" + this.ProgramScheduleShowInfo.ToString() + @"</ProgramScheduleShowInfo>");
            xml.AppendLine(@"<ProgramScheduleBrowseType>" + ((int)this.ProgramScheduleBrowseType).ToString() + @"</ProgramScheduleBrowseType>");
            xml.AppendLine(@"<ProgramScheduleOutputSettings>" + this.ProgramScheduleOutputSettings.Serialize() + @"</ProgramScheduleOutputSettings>");
            #endregion

            xml.AppendLine(@"</LocalSettings>");

            string settingsPath = this.UseRemoteConnection ? _remoteSettingsFilePath : _localSettingsFilePath;
            using (StreamWriter sw = new StreamWriter(settingsPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }

            this.IsConfigured = true;
        }

        public void GetDefaultWizard()
        {
            FileInfo defaultWizardFile = new FileInfo(this.DefaultWizardFileName);
            if (defaultWizardFile.Exists)
                using (StreamReader sr = new StreamReader(defaultWizardFile.FullName))
                    if ((this.DefaultWizard = sr.ReadLine()) == null)
                        this.DefaultWizard = string.Empty;
        }

        public void GetSalesDepotName()
        {
            this.SalesDepotName = "Sales Libraries";
            XmlNode node;
            string filePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\SDName.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (File.Exists(filePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);

                node = document.SelectSingleNode(@"/SDName");
                if (node != null)
                    this.SalesDepotName = node.InnerText;
            }
        }

        public bool CheckLibraries()
        {
            bool result = false;
            if (Directory.Exists(this.LibraryRootFolder))
                result = ((new DirectoryInfo(this.LibraryRootFolder)).GetDirectories()).Length > 0;
            return result;
        }

        public void CheckStaticFolders()
        {
            try
            {
                string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (!Directory.Exists(localSettingsFolder))
                    Directory.CreateDirectory(localSettingsFolder);
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot", "Settings")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot", "Settings"));
            }
            catch
            {
            }
        }

        private void LoadAppID()
        {
            this.AppID = Guid.Empty;
            string appIDPath = _appIDFile;
            if (File.Exists(appIDPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(appIDPath);

                XmlNode node = document.SelectSingleNode(@"/AppID");
                if (node != null)
                    if (!string.IsNullOrEmpty(node.InnerText))
                        this.AppID = new Guid(node.InnerText);
            }
        }

        private void LoadApprovedLibraries()
        {
            bool userExisted = false;
            this.ApprovedLibraries.Clear();
            List<string> defaultApprovedLibraries = new List<string>();
            if (File.Exists(_approvedLibrariesFile))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_approvedLibrariesFile);

                XmlNode node = document.SelectSingleNode(@"/ApprovedLibraries");
                if (node != null)
                    foreach (XmlNode userNode in node.ChildNodes)
                        if (userNode.Name.Equals("User"))
                        {
                            string userName = string.Empty;
                            bool useRemoteLibraries = false;
                            foreach (XmlAttribute attribute in userNode.Attributes)
                            {
                                switch (attribute.Name)
                                {
                                    case "Name":
                                        userName = attribute.Value;
                                        break;
                                    case "UseRemoteLibraries":
                                        bool.TryParse(attribute.Value, out useRemoteLibraries);
                                        break;
                                }
                            }
                            if (userName.Equals(Environment.UserName) && ((this.UseRemoteConnection & useRemoteLibraries) || (!this.UseRemoteConnection)))
                            {
                                userExisted = true;
                                foreach (XmlNode libraryNode in userNode.ChildNodes)
                                    if (libraryNode.Name.Equals(this.UseRemoteConnection ? "RemoteLibrary" : "LocalLibrary"))
                                        this.ApprovedLibraries.Add(libraryNode.InnerText.ToLower());
                            }
                            else if (userName.Equals(DefaultUserName) && ((this.UseRemoteConnection & useRemoteLibraries) || (!this.UseRemoteConnection)))
                            {
                                foreach (XmlNode libraryNode in userNode.ChildNodes)
                                    if (libraryNode.Name.Equals(this.UseRemoteConnection ? "RemoteLibrary" : "LocalLibrary"))
                                        defaultApprovedLibraries.Add(libraryNode.InnerText.ToLower());
                            }
                        }
                if (this.ApprovedLibraries.Count == 0)
                {
                    if (defaultApprovedLibraries.Count > 0 && !userExisted)
                        this.ApprovedLibraries.AddRange(defaultApprovedLibraries);
                    else
                        this.ApprovedLibraries.Add("None");
                }
            }
        }
    }

    public class KeyWordFileFilters
    {
        public bool AllFiles { get; set; }
        public bool PowerPoint { get; set; }
        public bool PDF { get; set; }
        public bool Excel { get; set; }
        public bool Word { get; set; }
        public bool Video { get; set; }
        public bool Url { get; set; }
        public bool Network { get; set; }
        public bool Folder { get; set; }

        public KeyWordFileFilters()
        {
            this.AllFiles = true;
            this.PowerPoint = true;
            this.PDF = true;
            this.Excel = true;
            this.Word = true;
            this.Video = true;
            this.Url = true;
            this.Network = true;
            this.Folder = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<AllFiles>" + this.AllFiles.ToString() + @"</AllFiles>");
            result.AppendLine(@"<PowerPoint>" + this.PowerPoint.ToString() + @"</PowerPoint>");
            result.AppendLine(@"<PDF>" + this.PDF.ToString() + @"</PDF>");
            result.AppendLine(@"<Excel>" + this.Excel.ToString() + @"</Excel>");
            result.AppendLine(@"<Word>" + this.Word.ToString() + @"</Word>");
            result.AppendLine(@"<Video>" + this.Video.ToString() + @"</Video>");
            result.AppendLine(@"<Url>" + this.Url.ToString() + @"</Url>");
            result.AppendLine(@"<Network>" + this.Network.ToString() + @"</Network>");
            result.AppendLine(@"<Folder>" + this.Folder.ToString() + @"</Folder>");
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
                            this.AllFiles = tempBool;
                        break;
                    case "PowerPoint":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.PowerPoint = tempBool;
                        break;
                    case "PDF":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.PDF = tempBool;
                        break;
                    case "Excel":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Excel = tempBool;
                        break;
                    case "Word":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Word = tempBool;
                        break;
                    case "Video":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Video = tempBool;
                        break;
                    case "Url":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Url = tempBool;
                        break;
                    case "Network":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Network = tempBool;
                        break;
                    case "Folder":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Folder = tempBool;
                        break;
                }
            }
        }
    }
}
