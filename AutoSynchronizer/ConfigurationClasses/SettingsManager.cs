using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace AutoSynchronizer.ConfigurationClasses
{
    public class SettingsManager
    {
        public const string UserSettingsFileName = @"SalesDepotUserSettings.xml";
        public const string StorageFileName = @"SalesDepotCache.xml";
        public const string StyleFileName = @"SalesDepotStyle.xml";
        public const string WholeDriveFilesStorage = @"Primary Root";
        public const string PreviewContainersRootFolderName = @"!QV";
        public const string OldPreviewFolderPrefix = @"!PNG_";
        public const string LibraryLogoFolder = @"!SD-Graphics";
        public const string OvernightsCalendarRootFolderName = @"!OC";
        public const string ProgramManagerRootFolderName = @"!PM";
        public const string ExtraFoldersRootFolderName = @"!Extra Roots";
        public const string SweepPeriodsFileName = @"SweepPeriods.xml";

        private static SettingsManager _instance = new SettingsManager();

        private string _settingsFilePath = string.Empty;
        public string ArhivePath { get; set; }
        public string LogRootPath { get; set; }

        #region FM Settings
        public string BackupPath { get; set; }
        public string NetworkPath { get; set; }
        public bool UseDirectAccessToFiles { get; set; }
        public int DirectAccessFileAgeLimit { get; set; }
        public string SelectedLibrary { get; set; }
        public string SelectedPage { get; set; }
        public int FontSize { get; set; }
        public int CalendarFontSize { get; set; }
        public bool TreeViewVisible { get; set; }
        public bool TreeViewDocked { get; set; }
        public bool MultitabView { get; set; }
        #endregion

        public int DestinationPathLength { get; private set; }

        public List<string> HiddenFolders { get; private set; }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SettingsManager()
        {
            this.DestinationPathLength = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries\Primary Root", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)).Length;

            string settingsFolderPath = string.Format(@"{0}\newlocaldirect.com\xml\file_manager", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (!Directory.Exists(settingsFolderPath))
                Directory.CreateDirectory(settingsFolderPath);
            _settingsFilePath = Path.Combine(settingsFolderPath, "LocalSettings.xml");
            this.ArhivePath = Path.Combine(settingsFolderPath, "Archives");
            if (Directory.Exists(this.ArhivePath))
                Directory.CreateDirectory(this.ArhivePath);
            this.LogRootPath = Path.Combine(settingsFolderPath, "Log");
            if (!Directory.Exists(this.LogRootPath))
                Directory.CreateDirectory(this.LogRootPath);


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

            this.HiddenFolders = new List<string>();
            this.HiddenFolders.Add(LibraryLogoFolder);
            this.HiddenFolders.Add(OldPreviewFolderPrefix);
            this.HiddenFolders.Add("!Old");
            this.HiddenFolders.Add(PreviewContainersRootFolderName);
            this.HiddenFolders.Add(OvernightsCalendarRootFolderName);
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
    }

    public class SearchGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }
        public List<string> Tags { get; set; }

        public SearchGroup()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Tags = new List<string>();
        }
    }
}
