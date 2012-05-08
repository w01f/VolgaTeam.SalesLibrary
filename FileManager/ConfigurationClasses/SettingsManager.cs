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
        public const string PreviewContainersRootFolderName = @"!QV";
        public const string OldPreviewFolderPrefix = @"!PNG_";
        public const string LibraryLogoFolder = @"!SD-Graphics";
        public const string OvernightsCalendarRootFolderName = @"!OC";

        private static SettingsManager _instance = new SettingsManager();

        private string _settingsFilePath = string.Empty;
        public string ArhivePath { get; set; }
        private string _syncSettingsFilePath = string.Empty;
        public string ClientLogosRootPath { get; set; }
        public string SalesGalleryRootPath { get; set; }
        public string WebArtRootPath { get; set; }
        public string AdSpecsSamplesRootPath { get; set; }
        public string ScreenshotLibraryRootPath { get; set; }

        public string BackupPath { get; set; }
        public string NetworkPath { get; set; }
        public string SelectedLibrary { get; set; }
        public string SelectedPage { get; set; }
        public int FontSize { get; set; }
        public bool TreeViewVisible { get; set; }
        public bool TreeViewDocked { get; set; }
        public bool MultitabView { get; set; }

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
            _syncSettingsFilePath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings\syncfile.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            
            this.ClientLogosRootPath = string.Empty;
            this.SalesGalleryRootPath = string.Empty;
            this.WebArtRootPath = string.Empty;
            this.AdSpecsSamplesRootPath = string.Empty;
            this.ScreenshotLibraryRootPath = string.Empty;

            this.BackupPath = string.Empty;
            this.NetworkPath = string.Empty;
            this.SelectedLibrary = string.Empty;
            this.SelectedPage = string.Empty;
            this.FontSize = 12;
            this.TreeViewVisible = false;
            this.TreeViewDocked = true;
            this.MultitabView = true;

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

                node = document.SelectSingleNode(@"/LocalSettings/BackupPath");
                if (node != null)
                    this.BackupPath = node.InnerText;
                node = document.SelectSingleNode(@"/LocalSettings/NetworkPath");
                if (node != null)
                    this.NetworkPath = node.InnerText;
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
            }
            LoadClipartPath();
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<LocalSettings>");
            xml.AppendLine(@"<BackupPath>" + this.BackupPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BackupPath>");
            xml.AppendLine(@"<NetworkPath>" + this.NetworkPath.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</NetworkPath>");
            xml.AppendLine(@"<SelectedLibrary>" + this.SelectedLibrary.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedLibrary>");
            xml.AppendLine(@"<SelectedPage>" + this.SelectedPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedPage>");
            xml.AppendLine(@"<FontSize>" + this.FontSize.ToString() + @"</FontSize>");
            xml.AppendLine(@"<TreeViewVisible>" + this.TreeViewVisible.ToString() + @"</TreeViewVisible>");
            xml.AppendLine(@"<TreeViewDocked>" + this.TreeViewDocked.ToString() + @"</TreeViewDocked>");
            xml.AppendLine(@"<MultitabView>" + this.MultitabView.ToString() + @"</MultitabView>");
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

    }
}
