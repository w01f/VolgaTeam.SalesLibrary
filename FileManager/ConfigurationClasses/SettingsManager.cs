using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace FileManager.ConfigurationClasses
{
    public class SettingsManager
    {
        public const string UserSettingsFileName = @"SalesDepotUserSettings.xml";
        public const string StorageFileName = @"SalesDepotCache.xml";
        public const string StyleFileName = @"SalesDepotStyle.xml";
        public const string WholeDriveFilesStorage = @"Primary Root";
        public const string RegularPreviewContainersRootFolderName = @"!QV";
        public const string FtpPreviewContainersRootFolderName = @"!WV";
        public const string OldPreviewFolderPrefix = @"!PNG_";
        public const string LibraryLogoFolder = @"!SD-Graphics";
        public const string OvernightsCalendarRootFolderName = @"!OC";
        public const string ProgramManagerRootFolderName = @"!PM";
        public const string ExtraFoldersRootFolderName = @"!Extra Roots";
        public const string SweepPeriodsFileName = @"SweepPeriods.xml";

        private static SettingsManager _instance = new SettingsManager();

        private string _settingsFilePath = string.Empty;
        private string _autoSyncSettingsPath = string.Empty;
        public string ApplicationRootsPath { get; private set; }
        public string ArhivePath { get; set; }
        public string LogRootPath { get; set; }
        private string _syncSettingsFilePath = string.Empty;
        public string ClientLogosRootPath { get; set; }
        public string SalesGalleryRootPath { get; set; }
        public string WebArtRootPath { get; set; }
        public string AdSpecsSamplesRootPath { get; set; }
        public string ScreenshotLibraryRootPath { get; set; }
        public string AutoFMSyncShorcutPath { get; set; }
        public string VideoConverterPath { get; set; }

        #region FM Settings
        public string BackupPath { get; set; }
        public string NetworkPath { get; set; }
        public bool UseDirectAccessToFiles { get; set; }
        public int DirectAccessFileAgeLimit { get; set; }
        public string SelectedLibrary { get; set; }
        public string SelectedPage { get; set; }
        public int SelectedCalendarYear { get; set; }
        public int FontSize { get; set; }
        public int CalendarFontSize { get; set; }
        public bool TreeViewVisible { get; set; }
        public bool TreeViewDocked { get; set; }
        public bool MultitabView { get; set; }
        #endregion

        public int DestinationPathLength { get; private set; }

        public List<string> HiddenObjects { get; private set; }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SettingsManager()
        {
            this.ApplicationRootsPath = Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location);

            this.DestinationPathLength = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries\Primary Root", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)).Length;

            string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\file_manager", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (!Directory.Exists(settingsFolderPath))
                Directory.CreateDirectory(settingsFolderPath);
            _settingsFilePath = Path.Combine(settingsFolderPath, "LocalSettings.xml");
            _autoSyncSettingsPath = Path.Combine(settingsFolderPath, "AutoSyncSchedule.xml");

            this.ArhivePath = Path.Combine(settingsFolderPath, "Archives");
            if (Directory.Exists(this.ArhivePath))
                Directory.CreateDirectory(this.ArhivePath);
            _syncSettingsFilePath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings\syncfile.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            this.LogRootPath = Path.Combine(settingsFolderPath, "Log");
            if (!Directory.Exists(this.LogRootPath))
                Directory.CreateDirectory(this.LogRootPath);

            this.ClientLogosRootPath = string.Empty;
            this.SalesGalleryRootPath = string.Empty;
            this.WebArtRootPath = string.Empty;
            this.AdSpecsSamplesRootPath = string.Empty;
            this.ScreenshotLibraryRootPath = string.Empty;

            this.AutoFMSyncShorcutPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Startup), "AutoFMSync.exe - Shortcut.lnk");
            this.VideoConverterPath = Path.Combine(this.ApplicationRootsPath, "video converter");

            #region FM Settings
            this.BackupPath = string.Empty;
            this.NetworkPath = string.Empty;
            this.UseDirectAccessToFiles = false;
            this.SelectedLibrary = string.Empty;
            this.SelectedPage = string.Empty;
            this.FontSize = 12;
            this.CalendarFontSize = 10;
            this.TreeViewVisible = false;
            this.TreeViewDocked = true;
            this.MultitabView = true;
            #endregion

            this.HiddenObjects = new List<string>();
            this.HiddenObjects.Add("!Old");
            this.HiddenObjects.Add(RegularPreviewContainersRootFolderName);
            this.HiddenObjects.Add(FtpPreviewContainersRootFolderName);
            this.HiddenObjects.Add(OvernightsCalendarRootFolderName);
            this.HiddenObjects.Add(ProgramManagerRootFolderName);
            this.HiddenObjects.Add(ExtraFoldersRootFolderName);
            this.HiddenObjects.Add("thumbs.db");
            this.HiddenObjects.Add("SalesDepotCache.xml");
        }

        public void Load()
        {
            XmlNode node;
            int tempInt = 0;
            bool tempBool = false;
            if (File.Exists(_settingsFilePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_settingsFilePath);

                #region FM Settings
                node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
                if (node != null)
                    this.BackupPath = node.InnerText;
                node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
                if (node != null)
                    this.NetworkPath = node.InnerText;
                node = document.SelectSingleNode(@"/LocalSettings/UseDirectAccessToFiles");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.UseDirectAccessToFiles = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/DirectAccessFileAgeLimit");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.DirectAccessFileAgeLimit = tempInt;
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
                node = document.SelectSingleNode(@"/LocalSettings/TreeViewVisible");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.TreeViewVisible = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/TreeViewDocked");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.TreeViewDocked = tempBool;
                node = document.SelectSingleNode(@"/LocalSettings/MultitabView");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.MultitabView = tempBool;
                #endregion
            }
            LoadClipartPath();
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<LocalSettings>");

            #region FM Settings
            xml.AppendLine(@"<BackupPath>" + this.BackupPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BackupPath>");
            xml.AppendLine(@"<NetworkPath>" + this.NetworkPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</NetworkPath>");
            xml.AppendLine(@"<UseDirectAccessToFiles>" + this.UseDirectAccessToFiles.ToString() + @"</UseDirectAccessToFiles>");
            xml.AppendLine(@"<DirectAccessFileAgeLimit>" + this.DirectAccessFileAgeLimit.ToString() + @"</DirectAccessFileAgeLimit>");
            xml.AppendLine(@"<SelectedLibrary>" + this.SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
            xml.AppendLine(@"<SelectedPage>" + this.SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
            xml.AppendLine(@"<SelectedCalendarYear>" + this.SelectedCalendarYear.ToString() + @"</SelectedCalendarYear>");
            xml.AppendLine(@"<FontSize>" + this.FontSize.ToString() + @"</FontSize>");
            xml.AppendLine(@"<CalendarFontSize>" + this.CalendarFontSize.ToString() + @"</CalendarFontSize>");
            xml.AppendLine(@"<TreeViewVisible>" + this.TreeViewVisible.ToString() + @"</TreeViewVisible>");
            xml.AppendLine(@"<TreeViewDocked>" + this.TreeViewDocked.ToString() + @"</TreeViewDocked>");
            xml.AppendLine(@"<MultitabView>" + this.MultitabView.ToString() + @"</MultitabView>");
            #endregion

            xml.AppendLine(@"</LocalSettings>");

            using (StreamWriter sw = new StreamWriter(_settingsFilePath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }

        private void LoadClipartPath()
        {
            XmlNode node;
            if (File.Exists(_syncSettingsFilePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_syncSettingsFilePath);

                node = document.SelectSingleNode(@"/Settings/MediaProperty/Path");
                if (node != null)
                {
                    string path = node.InnerText.Replace("\"", string.Empty).Trim();
                    this.ClientLogosRootPath = Path.Combine(path, @"outgoing\gallery\client logos");
                    this.SalesGalleryRootPath = Path.Combine(path, @"outgoing\gallery\sales gallery");
                    this.WebArtRootPath = Path.Combine(path, @"outgoing\gallery\web art");
                    this.AdSpecsSamplesRootPath = Path.Combine(path, @"outgoing\gallery\web art\Ad Specs-Samples");
                    this.ScreenshotLibraryRootPath = Path.Combine(path, @"outgoing\gallery\web art\Screenshot Library");
                }
            }
        }

        public void SaveAutoSyncSettings(string settings)
        {
            using (StreamWriter sw = new StreamWriter(_autoSyncSettingsPath, false))
            {
                sw.Write(settings.ToString());
                sw.Flush();
            }
        }
    }
}
