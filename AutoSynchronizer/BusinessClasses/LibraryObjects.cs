using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AutoSynchronizer.BusinessClasses
{
    public enum FileTypes
    {
        FriendlyPresentation = 0,
        OtherPresentation,
        BuggyPresentation,
        MediaPlayerVideo,
        QuickTimeVideo,
        Folder,
        LineBreak,
        Other,
        Url,
        Network
    }

    public enum Alignment
    {
        Left = 0,
        Center,
        Right
    }

    public class Library
    {
        private RootFolder _rootFolder = null;

        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public DirectoryInfo Folder { get; set; }
        public bool UseDirectAccess { get; set; }
        public DateTime DirectAccessFileBottomDate { get; set; }
        public string BrandingText { get; set; }
        public DateTime SyncDate { get; set; }

        public bool ApplyAppearanceForAllWindows { get; set; }
        public bool ApplyWidgetForAllWindows { get; set; }
        public bool ApplyBannerForAllWindows { get; set; }
        public bool MinimizeOnSync { get; set; }
        public bool CloseAfterSync { get; set; }
        public bool ShowProgressDuringSync { get; set; }
        public bool EnableInactiveLinks { get; set; }
        public bool InactiveLinksBoldWarning { get; set; }
        public bool ReplaceInactiveLinksWithLineBreak { get; set; }
        public bool InactiveLinksMessageAtStartup { get; set; }
        public bool SendEmail { get; set; }

        public bool IsConfigured { get; set; }

        public List<RootFolder> ExtraFolders { get; private set; }
        public List<LibraryPage> Pages { get; private set; }
        public List<string> EmailList { get; private set; }
        public List<LibraryFile> DeadLinks { get; private set; }
        public List<LibraryFile> ExpiredLinks { get; private set; }
        public List<AutoWidget> AutoWidgets { get; private set; }

        #region Auto Sync Settings
        public bool EnableAutoSync { get; set; }
        public List<SyncScheduleRecord> SyncScheduleRecords { get; private set; }

        //Obsolte, using for compatibility with old versions
        public List<TimePoint> SyncTimes { get; private set; }
        #endregion

        public OvernightsCalendar OvernightsCalendar { get; set; }

        public RootFolder RootFolder
        {
            get
            {
                if (_rootFolder == null)
                {
                    _rootFolder = new RootFolder(this);
                    _rootFolder.RootId = Guid.Empty;
                    _rootFolder.Folder = this.Folder;
                }
                return _rootFolder;
            }
        }

        public Library(string name, DirectoryInfo folder)
        {
            this.Identifier = Guid.NewGuid();
            this.Folder = folder;
            this.Name = name;
            this.IsConfigured = false;
            this.ExtraFolders = new List<RootFolder>();
            this.Pages = new List<LibraryPage>();
            this.EmailList = new List<string>();
            this.DeadLinks = new List<LibraryFile>();
            this.ExpiredLinks = new List<LibraryFile>();
            this.AutoWidgets = new List<AutoWidget>();

            #region Auto Sync Settings
            this.SyncScheduleRecords = new List<SyncScheduleRecord>();

            //Obsolte, using for compatibility with old versions
            this.SyncTimes = new List<TimePoint>();
            #endregion

            this.OvernightsCalendar = new OvernightsCalendar(this);

            Init();
        }

        public void Init()
        {
            Load();
            ProceedDeadLinks();
            ProceedExpiredLinks();
        }

        private void Load()
        {
            DateTime tempDate = DateTime.Now;
            bool tempBool = false;

            this.BrandingText = string.Empty;
            this.SyncDate = DateTime.Now;
            this.MinimizeOnSync = true;
            this.CloseAfterSync = true;
            this.ShowProgressDuringSync = true;
            this.EnableInactiveLinks = true;
            this.InactiveLinksBoldWarning = true;
            this.ReplaceInactiveLinksWithLineBreak = false;
            this.InactiveLinksMessageAtStartup = true;
            this.SendEmail = false;
            this.Pages.Clear();
            this.EmailList.Clear();
            this.AutoWidgets.Clear();
            this.EnableAutoSync = false;
            this.SyncScheduleRecords.Clear();
            this.ExtraFolders.Clear();

            bool fileBusy = true;
            string file = Path.Combine(this.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName);
            do
            {
                try
                {
                    if (File.Exists(file))
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(file);
                        fileBusy = false;

                        XmlNode node = document.SelectSingleNode(@"/Library/Name");
                        if (node != null)
                            this.Name = node.InnerText;
                        node = document.SelectSingleNode(@"/Library/UseDirectAccess");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.UseDirectAccess = tempBool;
                        if (this.UseDirectAccess)
                        {
                            node = document.SelectSingleNode(@"/Library/RootFolder");
                            if (node != null)
                                this.Folder = new DirectoryInfo(node.InnerText);
                            node = document.SelectSingleNode(@"/Library/DirectAccessFileBottomDate");
                            if (node != null)
                                if (DateTime.TryParse(node.InnerText, out tempDate))
                                    this.DirectAccessFileBottomDate = tempDate;
                        }
                        node = document.SelectSingleNode(@"/Library/BrandingText");
                        if (node != null)
                            this.BrandingText = node.InnerText;
                        node = document.SelectSingleNode(@"/Library/SyncDate");
                        if (node != null)
                            if (DateTime.TryParse(node.InnerText, out tempDate))
                                this.SyncDate = tempDate;
                        node = document.SelectSingleNode(@"/Library/ApplyAppearanceForAllWindows");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.ApplyAppearanceForAllWindows = tempBool;
                        node = document.SelectSingleNode(@"/Library/ApplyWidgetForAllWindows");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.ApplyWidgetForAllWindows = tempBool;
                        node = document.SelectSingleNode(@"/Library/ApplyBannerForAllWindows");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.ApplyBannerForAllWindows = tempBool;
                        node = document.SelectSingleNode(@"/Library/MinimizeOnSync");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.MinimizeOnSync = tempBool;
                        node = document.SelectSingleNode(@"/Library/CloseAfterSync");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.CloseAfterSync = tempBool;
                        node = document.SelectSingleNode(@"/Library/ShowProgressDuringSync");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.ShowProgressDuringSync = tempBool;
                        node = document.SelectSingleNode(@"/Library/EnableInactiveLinks");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.EnableInactiveLinks = tempBool;
                        node = document.SelectSingleNode(@"/Library/InactiveLinksBoldWarning");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.InactiveLinksBoldWarning = tempBool;
                        node = document.SelectSingleNode(@"/Library/ReplaceInactiveLinksWithLineBreak");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.ReplaceInactiveLinksWithLineBreak = tempBool;
                        node = document.SelectSingleNode(@"/Library/InactiveLinksMessageAtStartup");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.InactiveLinksMessageAtStartup = tempBool;
                        node = document.SelectSingleNode(@"/Library/SendEmail");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.SendEmail = tempBool;

                        node = document.SelectSingleNode(@"/Library/ExtraRoots");
                        if (node != null)
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                RootFolder folder = new RootFolder(this);
                                folder.Deserialize(childNode);
                                this.ExtraFolders.Add(folder);
                            }
                        node = document.SelectSingleNode(@"/Library/Pages");
                        if (node != null)
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                LibraryPage page = new LibraryPage(this);
                                page.Deserialize(childNode);
                                this.Pages.Add(page);
                            }
                        node = document.SelectSingleNode(@"/Library/EmailList");
                        if (node != null)
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                if (childNode.Name.Equals("Email"))
                                    this.EmailList.Add(childNode.InnerText);
                            }
                        node = document.SelectSingleNode(@"/Library/AutoWidgets");
                        if (node != null)
                            foreach (XmlNode childNode in node.ChildNodes)
                            {
                                AutoWidget autoWidget = new AutoWidget();
                                autoWidget.Deserialize(childNode);
                                this.AutoWidgets.Add(autoWidget);
                            }

                        #region Auto Sync Settings
                        node = document.SelectSingleNode(@"/Library/EnableAutoSync");
                        if (node != null)
                            if (bool.TryParse(node.InnerText, out tempBool))
                                this.EnableAutoSync = tempBool;
                        node = document.SelectSingleNode(@"/Library/SyncSchedule");
                        if (node != null)
                            foreach (XmlNode syncTimeNode in node.ChildNodes)
                            {
                                if (syncTimeNode.Name.Equals("SyncScheduleRecord"))
                                {
                                    SyncScheduleRecord synctTime = new SyncScheduleRecord();
                                    synctTime.Deserialize(syncTimeNode);
                                    this.SyncScheduleRecords.Add(synctTime);
                                }
                            }
                        //Obsolte, using for compatibility with old versions
                        node = document.SelectSingleNode(@"/Library/AutoSyncTimes");
                        if (node != null)
                        {
                            foreach (XmlNode syncTimeNode in node.ChildNodes)
                            {
                                if (syncTimeNode.Name.Equals("SyncTime"))
                                {
                                    TimePoint synctTime = new TimePoint();
                                    synctTime.Deserialize(syncTimeNode);
                                    this.SyncTimes.Add(synctTime);
                                }
                            }
                            if (this.SyncTimes.Count > 0)
                            {
                                DateTime[] syncTimes = this.SyncTimes.Select(x => new DateTime(1, 1, 1, x.Time.Hour, x.Time.Minute, 0)).Distinct().ToArray();
                                foreach (DateTime syncTime in syncTimes)
                                {
                                    SyncScheduleRecord syncScheduleRecord = new SyncScheduleRecord();
                                    syncScheduleRecord.Time = syncTime;

                                    DayOfWeek[] days = this.SyncTimes.Where(x => x.Time.Hour.Equals(syncTime.Hour) && x.Time.Minute.Equals(syncTime.Minute)).Select(x => x.Day).ToArray();
                                    foreach (DayOfWeek day in days)
                                    {
                                        switch (day)
                                        {
                                            case DayOfWeek.Monday:
                                                syncScheduleRecord.Monday = true;
                                                break;
                                            case DayOfWeek.Tuesday:
                                                syncScheduleRecord.Tuesday = true;
                                                break;
                                            case DayOfWeek.Wednesday:
                                                syncScheduleRecord.Wednesday = true;
                                                break;
                                            case DayOfWeek.Thursday:
                                                syncScheduleRecord.Thursday = true;
                                                break;
                                            case DayOfWeek.Friday:
                                                syncScheduleRecord.Friday = true;
                                                break;
                                            case DayOfWeek.Saturday:
                                                syncScheduleRecord.Saturday = true;
                                                break;
                                            case DayOfWeek.Sunday:
                                                syncScheduleRecord.Sunday = true;
                                                break;
                                        }
                                    }
                                    this.SyncScheduleRecords.Add(syncScheduleRecord);
                                }
                            }
                        }
                        #endregion

                        node = document.SelectSingleNode(@"/Library/OvernightsCalendar");
                        if (node != null)
                            this.OvernightsCalendar.Deserialize(node);
                        this.IsConfigured = true;
                    }
                }
                catch
                {
                    fileBusy = true;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            while (fileBusy);

            if (this.Pages.Count == 0)
                this.Pages.Add(new LibraryPage(this));
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<Library>");
            xml.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            xml.AppendLine(@"<UseDirectAccess>" + this.UseDirectAccess + @"</UseDirectAccess>");
            xml.AppendLine(@"<DirectAccessFileBottomDate>" + this.DirectAccessFileBottomDate.ToString() + @"</DirectAccessFileBottomDate>");
            xml.AppendLine(@"<RootFolder>" + this.Folder.FullName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RootFolder>");
            xml.AppendLine(@"<BrandingText>" + this.BrandingText.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</BrandingText>");
            xml.AppendLine(@"<SyncDate>" + this.SyncDate + @"</SyncDate>");
            xml.AppendLine(@"<ApplyAppearanceForAllWindows>" + this.ApplyAppearanceForAllWindows + @"</ApplyAppearanceForAllWindows>");
            xml.AppendLine(@"<ApplyWidgetForAllWindows>" + this.ApplyWidgetForAllWindows + @"</ApplyWidgetForAllWindows>");
            xml.AppendLine(@"<ApplyBannerForAllWindows>" + this.ApplyBannerForAllWindows + @"</ApplyBannerForAllWindows>");
            xml.AppendLine(@"<MinimizeOnSync>" + this.MinimizeOnSync + @"</MinimizeOnSync>");
            xml.AppendLine(@"<CloseAfterSync>" + this.CloseAfterSync + @"</CloseAfterSync>");
            xml.AppendLine(@"<ShowProgressDuringSync>" + this.ShowProgressDuringSync + @"</ShowProgressDuringSync>");
            xml.AppendLine(@"<EnableInactiveLinks>" + this.EnableInactiveLinks + @"</EnableInactiveLinks>");
            xml.AppendLine(@"<InactiveLinksBoldWarning>" + this.InactiveLinksBoldWarning + @"</InactiveLinksBoldWarning>");
            xml.AppendLine(@"<ReplaceInactiveLinksWithLineBreak>" + this.ReplaceInactiveLinksWithLineBreak + @"</ReplaceInactiveLinksWithLineBreak>");
            xml.AppendLine(@"<InactiveLinksMessageAtStartup>" + this.InactiveLinksMessageAtStartup + @"</InactiveLinksMessageAtStartup>");
            xml.AppendLine(@"<SendEmail>" + this.SendEmail + @"</SendEmail>");
            xml.AppendLine("<ExtraRoots>");
            foreach (RootFolder folder in this.ExtraFolders)
                xml.AppendLine(@"<ExtraRoot>" + folder.Serialize() + @"</ExtraRoot>");
            xml.AppendLine("</ExtraRoots>");
            xml.AppendLine("<Pages>");
            foreach (LibraryPage page in this.Pages)
                xml.AppendLine(@"<Page>" + page.Serialize() + @"</Page>");
            xml.AppendLine("</Pages>");
            xml.AppendLine("<EmailList>");
            foreach (string email in this.EmailList)
                xml.AppendLine(@"<Email>" + email.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Email>");
            xml.AppendLine("</EmailList>");
            xml.AppendLine("<AutoWidgets>");
            foreach (AutoWidget autoWidget in this.AutoWidgets)
                xml.AppendLine(@"<AutoWidget>" + autoWidget.Serialize() + @"</AutoWidget>");
            xml.AppendLine("</AutoWidgets>");

            #region Auto Sync Settings
            xml.AppendLine(@"<EnableAutoSync>" + this.EnableAutoSync.ToString() + @"</EnableAutoSync>");
            xml.AppendLine(@"<SyncSchedule>");
            foreach (SyncScheduleRecord syncTime in this.SyncScheduleRecords)
                xml.AppendLine(@"<SyncScheduleRecord>" + syncTime.Serialize() + @"</SyncScheduleRecord>");
            xml.AppendLine(@"</SyncSchedule>");
            #endregion

            xml.AppendLine(@"<OvernightsCalendar>" + this.OvernightsCalendar.Serialize() + @"</OvernightsCalendar>");
            xml.AppendLine(@"</Library>");

            using (StreamWriter sw = new StreamWriter(Path.Combine(this.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName), false))
            {
                sw.Write(xml.ToString());
                sw.Flush();
            }
        }

        public void PrepareForSynchronize()
        {
            this.SyncDate = DateTime.Now;
            if (this.IsConfigured)
                Save();
            if (!this.UseDirectAccess)
            {
                ProceedPresentationLinks();
                NotifyAboutExpiredLinks();
            }
            Archive();
        }

        private void ProceedDeadLinks()
        {
            this.DeadLinks.Clear();
            foreach (LibraryPage page in this.Pages)
                foreach (LibraryFolder folder in page.Folders)
                {
                    foreach (LibraryFile file in folder.Files)
                        file.CheckIfDead();
                    this.DeadLinks.AddRange(folder.Files.Where(x => x.IsDead));
                }
        }

        private void ProceedExpiredLinks()
        {
            this.ExpiredLinks.Clear();
            foreach (LibraryPage page in this.Pages)
                foreach (LibraryFolder folder in page.Folders)
                    this.ExpiredLinks.AddRange(folder.Files.Where(x => x.IsExpired));
        }

        public void ProceedPresentationProperties()
        {
            if (InteropClasses.PowerPointHelper.Instance.Connect())
            {
                foreach (LibraryPage page in this.Pages)
                    foreach (LibraryFolder folder in page.Folders)
                        foreach (LibraryFile file in folder.Files.Where(x => (x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.OtherPresentation) && (x.PresentationProperties == null || File.GetLastWriteTime(x.FullPath) > x.PresentationProperties.LastUpdate)))
                            file.GetPresentationPrperties();
                InteropClasses.PowerPointHelper.Instance.Disconnect();
                this.Save();
            }
        }

        private void ProceedPresentationLinks()
        {
            foreach (LibraryPage page in this.Pages)
                foreach (LibraryFolder folder in page.Folders)
                {
                    foreach (LibraryFile file in folder.Files.Where(x => x.Type == FileTypes.BuggyPresentation || x.Type == FileTypes.FriendlyPresentation || x.Type == FileTypes.OtherPresentation))
                    {
                        if (file.PreviewContainer == null)
                            file.PreviewContainer = new PresentationPreviewContainer(file);
                        file.PreviewContainer.UpdatePreviewImages();
                    }
                }
            this.Save();
        }

        public void DeleteDeadLinks(Guid[] deadLinkIdentifiers)
        {
            foreach (LibraryFile link in this.DeadLinks.Where(x => deadLinkIdentifiers.Contains(x.Identifier)))
                link.RemoveFromCollection();
            ProceedDeadLinks();
        }

        public void DeleteExpiredLinks(Guid[] expiredLinkIdentifiers)
        {
            foreach (LibraryFile link in this.ExpiredLinks.Where(x => expiredLinkIdentifiers.Contains(x.Identifier)))
                link.RemoveFromCollection();
            ProceedExpiredLinks();
        }

        public void NotifyAboutExpiredLinks()
        {
            ProceedExpiredLinks();
            //if (this.ExpiredLinks.Where(x => x.ExpirationDateOptions.SendEmailWhenSync).Count() > 0)
            //{
            //    if (InteropClasses.OutlookHelper.Instance.Connect())
            //    {
            //        InteropClasses.OutlookHelper.Instance.CreateMessage(this.EmailList.ToArray(), string.Join(Environment.NewLine, this.ExpiredLinks.Where(x => x.ExpirationDateOptions.SendEmailWhenSync).Select(y => y.FullPath)));
            //        InteropClasses.OutlookHelper.Instance.Disconnect();
            //    }
            //    else
            //        AppManager.Instance.ShowWarning("Cannot open Outlook");
            //}
        }

        public void Archive()
        {
            DateTime archiveDateTime = DateTime.Now;
            string archiveFolder = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ArhivePath, archiveDateTime.ToString("MMddyy") + "-" + archiveDateTime.ToString("hhmmsstt"));
            try
            {
                if (!Directory.Exists(archiveFolder))
                    Directory.CreateDirectory(archiveFolder);

                foreach (FileInfo file in this.Folder.GetFiles("*.xml"))
                    file.CopyTo(Path.Combine(archiveFolder, file.Name), true);
            }
            catch
            {
            }
        }

        public void AddPage()
        {
            LibraryPage page = new LibraryPage(this);
            page.Order = this.Pages.Count;
            this.Pages.Add(page);
        }

        public void UpPage(int position)
        {
            if (position > 0)
            {
                this.Pages[position].Order--;
                this.Pages[position - 1].Order++;
                this.Pages.Sort((x, y) => x.Order.CompareTo(y.Order));
            }
        }

        public void DownPage(int position)
        {
            if (position < this.Pages.Count - 1)
            {
                this.Pages[position].Order++;
                this.Pages[position + 1].Order--;
                this.Pages.Sort((x, y) => x.Order.CompareTo(y.Order));
            }
        }

        public void RebuildPagesOrder()
        {
            for (int i = 0; i < this.Pages.Count; i++)
                this.Pages[i].Order = i;
        }

        public RootFolder GetRootFolder(Guid folderId)
        {
            RootFolder folder = this.ExtraFolders.Where(x => x.RootId.Equals(folderId)).FirstOrDefault();
            if (folder != null)
                return folder;
            else
                return this.RootFolder;
        }

        public void AddExtraRoot()
        {
            RootFolder folder = new RootFolder(this);
            folder.RootId = Guid.NewGuid();
            folder.Order = this.ExtraFolders.Count;
            this.ExtraFolders.Add(folder);
        }


        public void UpExtraRoot(int position)
        {
            if (position > 0)
            {
                this.ExtraFolders[position].Order--;
                this.ExtraFolders[position - 1].Order++;
                this.ExtraFolders.Sort((x, y) => x.Order.CompareTo(y.Order));
            }
        }

        public void DownExtraRoot(int position)
        {
            if (position < this.ExtraFolders.Count - 1)
            {
                this.ExtraFolders[position].Order++;
                this.ExtraFolders[position + 1].Order--;
                this.ExtraFolders.Sort((x, y) => x.Order.CompareTo(y.Order));
            }
        }

        public void RebuildExtraFoldersOrder()
        {
            for (int i = 0; i < this.ExtraFolders.Count; i++)
                this.ExtraFolders[i].Order = i;
        }
    }

    public class LibraryPage
    {
        public Library Parent { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool EnableColumnTitles { get; set; }
        public bool ApplyForAllColumnTitles { get; set; }
        public List<LibraryFolder> Folders { get; set; }
        public List<ColumnTitle> ColumnTitles { get; set; }

        public int Index
        {
            get
            {
                return this.Order + 1;
            }
        }

        public LibraryPage(Library parent)
        {
            this.Parent = parent;
            this.Name = string.Format("Page {0}", this.Parent.Pages.Count + 1);
            this.Order = 0;
            this.EnableColumnTitles = false;
            this.ApplyForAllColumnTitles = false;
            this.Folders = new List<LibraryFolder>();
            this.ColumnTitles = new List<ColumnTitle>();

            ColumnTitle column = new ColumnTitle(this);
            column.Name = "Column 1";
            column.ColumnOrder = 0;
            this.ColumnTitles.Add(column);
            column = new ColumnTitle(this);
            column.Name = "Column 2";
            column.ColumnOrder = 1;
            this.ColumnTitles.Add(column);
            column = new ColumnTitle(this);
            column.Name = "Column 3";
            column.ColumnOrder = 2;
            this.ColumnTitles.Add(column);
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            result.AppendLine(@"<EnableColumnTitles>" + this.EnableColumnTitles + @"</EnableColumnTitles>");
            result.AppendLine(@"<ApplyForAllColumnTitles>" + this.ApplyForAllColumnTitles + @"</ApplyForAllColumnTitles>");
            result.AppendLine("<Folders>");
            foreach (LibraryFolder folder in this.Folders)
                result.AppendLine(@"<Folder>" + folder.Serialize() + @"</Folder>");
            result.AppendLine("</Folders>");
            result.AppendLine("<ColumnTitles>");
            foreach (ColumnTitle columnTitle in this.ColumnTitles)
                result.AppendLine(@"<ColumnTitle>" + columnTitle.Serialize() + @"</ColumnTitle>");
            result.AppendLine("</ColumnTitles>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt = 0;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        this.Name = childNode.InnerText;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Order = tempInt;
                        break;
                    case "EnableColumnTitles":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColumnTitles = tempBool;
                        break;
                    case "ApplyForAllColumnTitles":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllColumnTitles = tempBool;
                        break;
                    case "Folders":
                        this.Folders.Clear();
                        foreach (XmlNode folderNode in childNode.ChildNodes)
                        {
                            LibraryFolder folder = new LibraryFolder(this);
                            folder.Deserialize(folderNode);
                            this.Folders.Add(folder);
                        }
                        break;
                    case "ColumnTitles":
                        this.ColumnTitles.Clear();
                        foreach (XmlNode columnTitleNode in childNode.ChildNodes)
                        {
                            ColumnTitle columnTitle = new ColumnTitle(this);
                            columnTitle.Deserialize(columnTitleNode);
                            this.ColumnTitles.Add(columnTitle);
                        }
                        break;
                }
            }
        }
    }

    public class ColumnTitle
    {
        public LibraryPage Parent { get; set; }
        public string Name { get; set; }
        public int ColumnOrder { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForeColor { get; set; }
        public Font HeaderFont { get; set; }
        public bool EnableText { get; set; }
        public Alignment HeaderAlignment { get; set; }
        public bool EnableWidget { get; set; }
        public Image Widget { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public ColumnTitle(LibraryPage parent)
        {
            this.Parent = parent;
            this.Name = string.Empty;
            this.ColumnOrder = 0;
            this.BackgroundColor = Color.White;
            this.ForeColor = Color.Black;
            this.HeaderFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            this.EnableText = true;
            this.HeaderAlignment = Alignment.Center;
            this.BannerProperties = new BannerProperties();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<ColumnOrder>" + this.ColumnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BackgroundColor>" + this.BackgroundColor.ToArgb() + @"</BackgroundColor>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            result.AppendLine(@"<EnableText>" + this.EnableText + @"</EnableText>");
            result.AppendLine(@"<HeaderAligment>" + ((int)this.HeaderAlignment).ToString() + @"</HeaderAligment>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(this.Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
            bool tempBool;
            FontConverter converter = new FontConverter();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        this.Name = childNode.InnerText;
                        break;
                    case "ColumnOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColumnOrder = tempInt;
                        break;
                    case "BackgroundColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeColor = Color.FromArgb(tempInt);
                        break;
                    case "HeaderFont":
                        try
                        {
                            this.HeaderFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "EnableText":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableText = tempBool;
                        break;
                    case "HeaderAligment":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.HeaderAlignment = (Alignment)tempInt;
                        break;
                    case "EnableWidget":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableWidget = tempBool;
                        break;
                    case "Widget":
                        if (!string.IsNullOrEmpty(childNode.InnerText))
                            this.Widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "BannerProperties":
                        this.BannerProperties.Deserialize(childNode);
                        break;
                }
            }
        }
    }

    public class LibraryFolder
    {
        public Guid Identifier { get; set; }
        public LibraryPage Parent { get; set; }
        public string Name { get; set; }
        public double RowOrder { get; set; }
        public int ColumnOrder { get; set; }
        public Color BorderColor { get; set; }
        public Color BackgroundWindowColor { get; set; }
        public Color ForeWindowColor { get; set; }
        public Color BackgroundHeaderColor { get; set; }
        public Color ForeHeaderColor { get; set; }
        public Font WindowFont { get; set; }
        public Font HeaderFont { get; set; }
        public Alignment HeaderAlignment { get; set; }
        public bool EnableWidget { get; set; }
        public Image Widget { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public List<LibraryFile> Files { get; set; }

        public LibraryFolder(LibraryPage parent)
        {
            this.Identifier = Guid.NewGuid();
            this.Parent = parent;
            this.Name = string.Empty;
            this.RowOrder = 0;
            this.ColumnOrder = 0;
            this.BorderColor = Color.Black;
            this.BackgroundWindowColor = Color.White;
            this.ForeWindowColor = Color.Black;
            this.BackgroundHeaderColor = Color.White;
            this.ForeHeaderColor = Color.Black;
            this.WindowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            this.HeaderFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.HeaderAlignment = Alignment.Center;

            this.BannerProperties = new BannerProperties();
            this.BannerProperties.Font = this.HeaderFont;
            this.BannerProperties.ForeColor = this.ForeHeaderColor;

            this.Files = new List<LibraryFile>();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<RowOrder>" + this.RowOrder + @"</RowOrder>");
            result.AppendLine(@"<ColumnOrder>" + this.ColumnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BorderColor>" + this.BorderColor.ToArgb() + @"</BorderColor>");
            result.AppendLine(@"<BackgroundWindowColor>" + this.BackgroundWindowColor.ToArgb() + @"</BackgroundWindowColor>");
            result.AppendLine(@"<ForeWindowColor>" + this.ForeWindowColor.ToArgb() + @"</ForeWindowColor>");
            result.AppendLine(@"<BackgroundHeaderColor>" + this.BackgroundHeaderColor.ToArgb() + @"</BackgroundHeaderColor>");
            result.AppendLine(@"<ForeHeaderColor>" + this.ForeHeaderColor.ToArgb() + @"</ForeHeaderColor>");
            result.AppendLine(@"<WindowFont>" + converter.ConvertToString(this.WindowFont) + @"</WindowFont>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            result.AppendLine(@"<HeaderAligment>" + ((int)this.HeaderAlignment).ToString() + @"</HeaderAligment>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(this.Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
            result.AppendLine("<Files>");
            foreach (LibraryFile file in this.Files)
                result.AppendLine(@"<File>" + file.Serialize() + @"</File>");
            result.AppendLine("</Files>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
            bool tempBool;
            FontConverter converter = new FontConverter();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        this.Name = childNode.InnerText;
                        break;
                    case "RowOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.RowOrder = tempInt;
                        break;
                    case "ColumnOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColumnOrder = tempInt;
                        break;
                    case "BorderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BorderColor = Color.FromArgb(tempInt);
                        break;
                    case "BackgroundWindowColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundWindowColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeWindowColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeWindowColor = Color.FromArgb(tempInt);
                        break;
                    case "BackgroundHeaderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundHeaderColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeHeaderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeHeaderColor = Color.FromArgb(tempInt);
                        break;
                    case "WindowFont":
                        try
                        {
                            this.WindowFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "HeaderFont":
                        try
                        {
                            this.HeaderFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "HeaderAligment":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.HeaderAlignment = (Alignment)tempInt;
                        break;
                    case "EnableWidget":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableWidget = tempBool;
                        break;
                    case "Widget":
                        if (!string.IsNullOrEmpty(childNode.InnerText))
                            this.Widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "BannerProperties":
                        this.BannerProperties.Deserialize(childNode);
                        break;
                    case "Files":
                        this.Files.Clear();
                        foreach (XmlNode fileNode in childNode.ChildNodes)
                        {
                            LibraryFile file = new LibraryFile(this);
                            file.Deserialize(fileNode);
                            this.Files.Add(file);
                        }
                        break;
                }
            }
            if (!this.BannerProperties.Configured)
            {
                this.BannerProperties.Text = this.Name;
                this.BannerProperties.Font = this.HeaderFont;
                this.BannerProperties.ForeColor = this.ForeHeaderColor;
            }
        }

        public void RemoveFromParent()
        {
            this.Parent.Folders.Remove(this);
        }
    }

    public class LibraryFile
    {
        private string _note = string.Empty;
        private Image _widget = null;

        #region Compatibility with old versions
        private bool _oldEnableBanner;
        private Image _oldBanner;
        #endregion

        private string _linkLocalPath = string.Empty;

        public string Name { get; set; }
        public LibraryFolder Parent { get; set; }
        public Guid RootId { get; set; }
        public Guid Identifier { get; set; }
        public string RelativePath { get; set; }
        public FileTypes Type { get; set; }
        public string Format;
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsDead { get; set; }
        public DateTime AddDate { get; set; }
        public bool EnableWidget { get; set; }

        public LibraryFileSearchTags SearchTags { get; set; }
        public ExpirationDateOptions ExpirationDateOptions { get; set; }
        public PresentationPreviewContainer PreviewContainer { get; set; }
        public PresentationProperties PresentationProperties { get; set; }
        public LineBreakProperties LineBreakProperties { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_linkLocalPath))
                {
                    if (this.Type == FileTypes.Url || this.Type == FileTypes.Network)
                        return this.RelativePath;
                    else if (this.Type == FileTypes.LineBreak)
                        return string.Empty;
                    else
                        return ((this.Parent != null ? this.Parent.Parent.Parent.GetRootFolder(this.RootId).Folder.FullName : string.Empty) + @"\" + this.RelativePath).Replace(@"\\", @"\").Replace(@"\\", @"\");
                }
                else
                    return _linkLocalPath;
            }
            set
            {
                _linkLocalPath = value;
            }
        }

        public string DisplayName
        {
            get
            {
                if (this.IsDead && this.Parent.Parent.Parent.EnableInactiveLinks)
                {
                    if (this.Parent.Parent.Parent.InactiveLinksBoldWarning)
                    {
                        if (!this.Name.Contains("INACTIVE!"))
                            return "INACTIVE! " + this.Name;
                        else
                            return this.Name;
                    }
                    else if (this.Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak)
                        return string.Empty;
                    else
                        return this.Name;
                }
                else if (this.ExpirationDateOptions.EnableExpirationDate && this.ExpirationDateOptions.LabelLinkWhenExpired && this.IsExpired)
                    return "EXPIRED! " + this.Name;
                else
                    return this.Name;
            }
        }

        public string PropertiesName
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder || this.Type == FileTypes.LineBreak)
                    return this.Name;
                else
                    return Path.GetFileName(this.FullPath);
            }
        }

        public string NameWithoutExtesion
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder)
                    return this.Name;
                else if (this.Type == FileTypes.LineBreak)
                    return string.Empty;
                else
                {
                    return Path.GetFileNameWithoutExtension(this.FullPath);
                }
            }
        }

        private string Extension
        {
            get
            {
                switch (this.Type)
                {
                    case FileTypes.BuggyPresentation:
                    case FileTypes.FriendlyPresentation:
                    case FileTypes.MediaPlayerVideo:
                    case FileTypes.Other:
                    case FileTypes.OtherPresentation:
                    case FileTypes.QuickTimeVideo:
                        return Path.GetExtension(this.FullPath);
                    default:
                        return string.Empty;
                }
            }
        }


        public string Note
        {
            get
            {
                if (this.IsDead && this.Parent.Parent.Parent.EnableInactiveLinks && (this.Parent.Parent.Parent.InactiveLinksBoldWarning || this.Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak))
                    return string.Empty;
                else
                    return _note;

            }
            set
            {
                _note = value;
            }
        }

        public bool DisplayAsBold
        {
            get
            {
                if (this.IsDead && this.Parent.Parent.Parent.EnableInactiveLinks && this.Parent.Parent.Parent.InactiveLinksBoldWarning)
                    return true;
                else if (this.ExpirationDateOptions.EnableExpirationDate && this.IsExpired && this.ExpirationDateOptions.LabelLinkWhenExpired)
                    return true;
                else
                    return this.IsBold;
            }
        }

        public bool IsExpired
        {
            get
            {
                if (this.ExpirationDateOptions.EnableExpirationDate && this.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
                    return ((long)this.ExpirationDateOptions.ExpirationDate.Subtract(DateTime.Now).TotalMilliseconds) < 0;
                else
                    return false;
            }

        }

        public Image Widget
        {
            get
            {
                if (this.EnableWidget && _widget != null)
                    return _widget;
                else if (this.Parent != null)
                    return this.Parent.Parent.Parent.AutoWidgets.Where(x => x.Extension.ToLower().Equals(!string.IsNullOrEmpty(this.Extension) ? this.Extension.Substring(1).ToLower() : string.Empty)).Select(y => y.Widget).FirstOrDefault();
                else
                    return null;
            }
            set
            {
                _widget = value;
            }
        }

        public LibraryFile(LibraryFolder parent)
        {
            this.Name = string.Empty;
            this.Parent = parent;
            this.RootId = Guid.Empty;
            this.Identifier = Guid.NewGuid();
            this.RelativePath = string.Empty;
            this.Type = FileTypes.Other;
            this.Format = string.Empty;
            this.Order = 0;
            this.IsBold = false;
            this.IsDead = false;
            this.AddDate = DateTime.Now;
            this.SearchTags = new LibraryFileSearchTags();
            this.ExpirationDateOptions = new BusinessClasses.ExpirationDateOptions();
            SetProperties();
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<DisplayName>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</DisplayName>");
            result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            result.AppendLine(@"<IsBold>" + this.IsBold + @"</IsBold>");
            result.AppendLine(@"<IsDead>" + this.IsDead + @"</IsDead>");
            result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
            result.AppendLine(@"<LocalPath>" + _linkLocalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalPath>");
            result.AppendLine(@"<RelativePath>" + this.RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
            result.AppendLine(@"<Type>" + (int)this.Type + @"</Type>");
            result.AppendLine(@"<Format>" + this.Format.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Format>");
            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            result.AppendLine(@"<AddDate>" + this.AddDate + @"</AddDate>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.Append(this.SearchTags.Serialize());
            result.AppendLine(@"<ExpirationDateOptions>" + this.ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");
            if (this.PreviewContainer != null)
                result.AppendLine(@"<PreviewContainer>" + this.PreviewContainer.Serialize() + @"</PreviewContainer>");
            if (this.PresentationProperties != null)
                result.AppendLine(@"<PresentationProperties>" + this.PresentationProperties.Serialize() + @"</PresentationProperties>");
            if (this.LineBreakProperties != null)
                result.AppendLine(@"<LineBreakProperties>" + this.LineBreakProperties.Serialize() + @"</LineBreakProperties>");
            if (this.BannerProperties != null && this.BannerProperties.Configured)
            {
                result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
                #region Compatibility with old versions
                result.AppendLine(@"<EnableBanner>" + this.BannerProperties.Enable.ToString() + @"</EnableBanner>");
                result.AppendLine(@"<Banner>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.BannerProperties.Image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Banner>");
                #endregion
            }
            else
            {
                #region Compatibility with old versions
                result.AppendLine(@"<EnableBanner>" + _oldEnableBanner.ToString() + @"</EnableBanner>");
                result.AppendLine(@"<Banner>" + Convert.ToBase64String((byte[])converter.ConvertTo(_oldBanner, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Banner>");
                #endregion
            }
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt = 0;
            DateTime tempDate = DateTime.Now;
            Guid tempGuid;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "DisplayName":
                        this.Name = childNode.InnerText;
                        break;
                    case "Note":
                        _note = childNode.InnerText;
                        break;
                    case "IsBold":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.IsBold = tempBool;
                        break;
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "LocalPath":
                        _linkLocalPath = childNode.InnerText;
                        break;
                    case "RelativePath":
                        this.RelativePath = childNode.InnerText;
                        break;
                    case "Type":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                        {
                            this.Type = (FileTypes)tempInt;
                            if (this.Type == FileTypes.LineBreak)
                                this.LineBreakProperties = new LineBreakProperties();
                        }
                        break;
                    case "Format":
                        this.Format = childNode.InnerText;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Order = tempInt;
                        break;
                    case "AddDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.AddDate = tempDate;
                        break;
                    case "EnableWidget":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableWidget = tempBool;
                        break;
                    case "Widget":
                        if (string.IsNullOrEmpty(childNode.InnerText) && this.EnableWidget)
                            _widget = null;
                        else if (!string.IsNullOrEmpty(childNode.InnerText))
                            _widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "SearchTags":
                        this.SearchTags.Deserialize(childNode);
                        break;
                    case "ExpirationDateOptions":
                        this.ExpirationDateOptions.Deserialize(childNode);
                        break;
                    case "PreviewContainer":
                        this.PreviewContainer = new PresentationPreviewContainer(this);
                        this.PreviewContainer.Deserialize(childNode);
                        break;
                    case "PresentationProperties":
                        this.PresentationProperties = new PresentationProperties();
                        this.PresentationProperties.Deserialize(childNode);
                        break;
                    case "LineBreakProperties":
                        this.LineBreakProperties = new LineBreakProperties();
                        this.LineBreakProperties.Font = new Font(this.Parent.WindowFont, this.Parent.WindowFont.Style);
                        this.LineBreakProperties.Deserialize(childNode);
                        break;
                    case "BannerProperties":
                        this.BannerProperties = new BannerProperties();
                        this.BannerProperties.Deserialize(childNode);
                        break;
                    #region Compatibility with old versions
                    case "EnableBanner":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _oldEnableBanner = tempBool;
                        break;
                    case "Banner":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            _oldBanner = null;
                        else
                            _oldBanner = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    #endregion
                }
            }

            if (this.BannerProperties == null)
                InitBannerProperties();
        }

        public void InitBannerProperties()
        {
            this.BannerProperties = new BannerProperties();
            this.BannerProperties.Font = new Font(this.Parent.WindowFont, this.Parent.WindowFont.Style);
            this.BannerProperties.ForeColor = this.Parent.ForeWindowColor;
            this.BannerProperties.Text = this.DisplayName;

            this.BannerProperties.Enable = _oldEnableBanner;
            this.BannerProperties.Image = _oldBanner;
            if (this.LineBreakProperties != null)
            {
                this.BannerProperties.Enable |= this.LineBreakProperties.EnableBanner;
                if (this.LineBreakProperties.Banner != null)
                    this.BannerProperties.Image = this.LineBreakProperties.Banner;
            }
        }

        public void SetProperties()
        {
            if (this.Type != FileTypes.Folder && this.Type != FileTypes.LineBreak && this.Type != FileTypes.Url && this.Type != FileTypes.Network)
            {
                switch (this.Extension.ToUpper())
                {
                    case ".PPT":
                    case ".PPTX":
                        this.Type = FileTypes.OtherPresentation;
                        break;
                    case ".MPEG":
                    case ".WMV":
                    case ".AVI":
                    case ".WMZ":
                        this.Type = FileTypes.MediaPlayerVideo;
                        break;
                    case ".ASF":
                    case ".MOV":
                    case ".MP4":
                    case ".MPG":
                    case ".M4V":
                    case ".FLV":
                    case ".OGV":
                    case ".OGM":
                    case ".OGX":
                        this.Type = FileTypes.QuickTimeVideo;
                        break;
                    default:
                        this.Type = FileTypes.Other;
                        break;
                }
            }
        }

        public void GetPresentationPrperties()
        {
            InteropClasses.PowerPointHelper.Instance.GetPresentationProperties(this);
        }

        public void CheckIfDead()
        {
            switch (this.Type)
            {
                case FileTypes.MediaPlayerVideo:
                case FileTypes.Other:
                case FileTypes.OtherPresentation:
                    if (!File.Exists(this.FullPath))
                        this.IsDead = true;
                    else
                        this.IsDead = false;
                    break;
                case FileTypes.Folder:
                    if (!Directory.Exists(this.FullPath))
                        this.IsDead = true;
                    else
                        this.IsDead = false;
                    break;
            }
        }

        public void RemoveFromCollection()
        {
            this.Parent.Files.Remove(this);
        }
    }

    public class LibraryFileSearchTags
    {
        public List<ConfigurationClasses.SearchGroup> SearchGroups { get; set; }

        public string AllTags
        {
            get
            {
                List<string> allTags = new List<string>();
                foreach (ConfigurationClasses.SearchGroup group in this.SearchGroups)
                    allTags.AddRange(group.Tags);
                return string.Join(", ", allTags.ToArray());
            }
        }

        public LibraryFileSearchTags()
        {
            this.SearchGroups = new List<ConfigurationClasses.SearchGroup>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<SearchTags>");
            foreach (ConfigurationClasses.SearchGroup group in this.SearchGroups)
            {
                result.Append(@"<Category ");
                result.Append("Name = \"" + group.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                result.AppendLine(@">");
                foreach (string tag in group.Tags)
                {
                    result.Append(@"<Tag ");
                    result.Append("Value = \"" + tag.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                    result.AppendLine(@"/>");
                }
                result.AppendLine(@"</Category>");
            }
            result.AppendLine(@"</SearchTags>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            this.SearchGroups.Clear();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Category":
                        ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                        foreach (XmlAttribute attribute in childNode.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "Name":
                                    group.Name = attribute.Value;
                                    break;
                            }
                        }
                        foreach (XmlNode tagNode in childNode.ChildNodes)
                        {
                            switch (tagNode.Name)
                            {
                                case "Tag":
                                    foreach (XmlAttribute attribute in tagNode.Attributes)
                                    {
                                        switch (attribute.Name)
                                        {
                                            case "Value":
                                                if (!string.IsNullOrEmpty(attribute.Value))
                                                    group.Tags.Add(attribute.Value);
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
                            this.SearchGroups.Add(group);
                        break;
                }
            }
        }
    }

    public class ExpirationDateOptions
    {
        public bool EnableExpirationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool LabelLinkWhenExpired { get; set; }
        public bool SendEmailWhenSync { get; set; }

        public ExpirationDateOptions()
        {
            this.EnableExpirationDate = false;
            this.ExpirationDate = DateTime.MinValue;
            this.LabelLinkWhenExpired = true;
            this.SendEmailWhenSync = false;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<EnableExpirationDate>" + this.EnableExpirationDate + @"</EnableExpirationDate>");
            result.AppendLine(@"<ExpirationDate>" + this.ExpirationDate + @"</ExpirationDate>");
            result.AppendLine(@"<LabelLinkWhenExpired>" + this.LabelLinkWhenExpired + @"</LabelLinkWhenExpired>");
            result.AppendLine(@"<SendEmailWhenSync>" + this.SendEmailWhenSync + @"</SendEmailWhenSync>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            DateTime tempDate = DateTime.Now;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "EnableExpirationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableExpirationDate = tempBool;
                        break;
                    case "ExpirationDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.ExpirationDate = tempDate;
                        break;
                    case "LabelLinkWhenExpired":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.LabelLinkWhenExpired = tempBool;
                        break;
                    case "SendEmailWhenSync":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.SendEmailWhenSync = tempBool;
                        break;
                }
            }
        }
    }

    public class PresentationPreviewContainer
    {
        private string _folderName = Guid.NewGuid().ToString();
        public string PreviewStorageFolder { get; set; }
        private LibraryFile _parent = null;

        public PresentationPreviewContainer(LibraryFile parent)
        {
            _parent = parent;
            this.PreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName, _folderName);
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<FolderName>" + _folderName + @"</FolderName>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderName":
                        _folderName = childNode.InnerText;
                        this.PreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName, _folderName);
                        break;
                }
            }
        }

        public void UpdatePreviewImages()
        {
            ClearOldPreviewImages();

            FileInfo parentFile = new FileInfo(_parent.FullPath);
            DirectoryInfo previewFolder = new DirectoryInfo(this.PreviewStorageFolder);
            bool needToUpdate = false;
            if (!previewFolder.Exists)
                needToUpdate = true;
            else if (parentFile.LastWriteTime > previewFolder.CreationTime)
                needToUpdate = true;
            else
                needToUpdate = false;
            if (needToUpdate)
            {
                if (!Directory.Exists(Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName)))
                    Directory.CreateDirectory(Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName));
                if (previewFolder.Exists)
                    ToolClasses.SyncManager.Instance.DeleteFolder(previewFolder);
                Directory.CreateDirectory(this.PreviewStorageFolder);
                InteropClasses.PowerPointHelper.Instance.ExportPresentationAsImages(_parent.FullPath, this.PreviewStorageFolder);
            }
        }

        public void ClearOldPreviewImages()
        {
            if (_parent.Parent != null)
            {
                DirectoryInfo folder = new FileInfo(_parent.FullPath).Directory;
                if (folder.Exists && folder.GetDirectories("*" + ConfigurationClasses.SettingsManager.OldPreviewFolderPrefix + "*").Length > 0)
                    ToolClasses.SyncManager.Instance.DeleteFolder(folder, ConfigurationClasses.SettingsManager.OldPreviewFolderPrefix);
            }
        }

        public void ClearPreviewImages()
        {
            DirectoryInfo previewFolder = new DirectoryInfo(this.PreviewStorageFolder);
            if (previewFolder.Exists)
                ToolClasses.SyncManager.Instance.DeleteFolder(previewFolder);
        }
    }

    public class PresentationProperties
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public DateTime LastUpdate { get; set; }

        public string Orientation
        {
            get
            {
                if (this.Height < this.Width)
                    return "Landscape";
                else
                    return "Portrait";
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Height>" + this.Height + @"</Height>");
            result.AppendLine(@"<Width>" + this.Width + @"</Width>");
            result.AppendLine(@"<LastUpdate>" + this.LastUpdate + @"</LastUpdate>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            double tempDouble = 0;
            DateTime tempDateTime = DateTime.MinValue;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Height":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Height = tempDouble;
                        break;
                    case "Width":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Width = tempDouble;
                        break;
                    case "LastUpdate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.LastUpdate = tempDateTime;
                        break;
                }
            }
        }
    }

    public class LineBreakProperties
    {
        public Color ForeColor { get; set; }
        public Font Font { get; set; }
        public bool EnableBanner { get; set; }
        public Image Banner { get; set; }
        public string Note { get; set; }

        public LineBreakProperties()
        {
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.Note = string.Empty;
        }

        public string Serialize()
        {
            FontConverter fontConverter = new FontConverter();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Font>" + fontConverter.ConvertToString(this.Font) + @"</Font>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<Note>" + this.Note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            FontConverter converter = new FontConverter();
            int tempInt = 0;
            bool tempBool = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Font":
                        try
                        {
                            this.Font = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeColor = Color.FromArgb(tempInt);
                        break;
                    case "EnableBanner":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBanner = tempBool;
                        break;
                    case "Banner":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Banner = null;
                        else
                            this.Banner = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "Note":
                        this.Note = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class BannerProperties
    {
        public bool Configured { get; set; }

        public bool Enable { get; set; }
        public Image Image { get; set; }
        public bool ShowText { get; set; }
        public Alignment ImageAlignement { get; set; }
        public string Text { get; set; }
        public Color ForeColor { get; set; }
        public Font Font { get; set; }

        public BannerProperties()
        {
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.Text = string.Empty;
        }

        public string Serialize()
        {
            FontConverter fontConverter = new FontConverter();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Enable>" + this.Enable.ToString() + @"</Enable>");
            result.AppendLine(@"<Image>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Image>");
            result.AppendLine(@"<ImageAligement>" + ((int)this.ImageAlignement).ToString() + @"</ImageAligement>");
            result.AppendLine(@"<ShowText>" + this.ShowText.ToString() + @"</ShowText>");
            result.AppendLine(@"<Text>" + this.Text.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Text>");
            result.AppendLine(@"<Font>" + fontConverter.ConvertToString(this.Font) + @"</Font>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            FontConverter converter = new FontConverter();
            int tempInt = 0;
            bool tempBool = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enable = tempBool;
                        break;
                    case "Image":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Image = null;
                        else
                            this.Image = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "ImageAligement":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ImageAlignement = (Alignment)tempInt;
                        break;
                    case "ShowText":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowText = tempBool;
                        break;
                    case "Text":
                        this.Text = childNode.InnerText;
                        break;
                    case "Font":
                        try
                        {
                            this.Font = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ForeColor = Color.FromArgb(tempInt);
                        break;
                }
            }
            this.Configured = true;
        }
    }

    public class AutoWidget
    {
        public string Extension { get; set; }
        public Image Widget { get; set; }

        public AutoWidget()
        {
            this.Extension = string.Empty;
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Extension>" + this.Extension + @"</Extension>");
            result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Extension":
                        this.Extension = childNode.InnerText;
                        break;
                    case "Widget":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Widget = null;
                        else
                            this.Widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                }
            }
        }
    }

    public class SyncScheduleRecord
    {
        public DateTime Time { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public string DayString
        {
            get
            {
                List<string> result = new List<string>();
                if (this.Monday)
                    result.Add("Mo");
                if (this.Tuesday)
                    result.Add("Tu");
                if (this.Wednesday)
                    result.Add("We");
                if (this.Thursday)
                    result.Add("Th");
                if (this.Friday)
                    result.Add("Fr");
                if (this.Saturday)
                    result.Add("Sa");
                if (this.Sunday)
                    result.Add("Su");

                return string.Join(",", result.ToArray());
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Time>" + this.Time.ToString() + @"</Time>");
            result.AppendLine(@"<Monday>" + this.Monday.ToString() + @"</Monday>");
            result.AppendLine(@"<Tuesday>" + this.Tuesday.ToString() + @"</Tuesday>");
            result.AppendLine(@"<Wednesday>" + this.Wednesday.ToString() + @"</Wednesday>");
            result.AppendLine(@"<Thursday>" + this.Thursday.ToString() + @"</Thursday>");
            result.AppendLine(@"<Friday>" + this.Friday.ToString() + @"</Friday>");
            result.AppendLine(@"<Saturday>" + this.Saturday.ToString() + @"</Saturday>");
            result.AppendLine(@"<Sunday>" + this.Sunday.ToString() + @"</Sunday>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;
            DateTime tempDateTime;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Time":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.Time = tempDateTime;
                        break;
                    case "Monday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Monday = tempBool;
                        break;
                    case "Tuesday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Tuesday = tempBool;
                        break;
                    case "Wednesday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Wednesday = tempBool;
                        break;
                    case "Thursday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Thursday = tempBool;
                        break;
                    case "Friday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Friday = tempBool;
                        break;
                    case "Saturday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Saturday = tempBool;
                        break;
                    case "Sunday":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Sunday = tempBool;
                        break;
                }
            }
        }
    }

    #region Obsolete, Using for compatibility with old versions
    public class TimePoint
    {
        public DayOfWeek Day { get; set; }
        public DateTime Time { get; set; }

        public string DayString
        {
            get
            {
                switch (this.Day)
                {
                    case DayOfWeek.Sunday:
                        return "Sunday";
                    case DayOfWeek.Monday:
                        return "Monday";
                    case DayOfWeek.Tuesday:
                        return "Tuesday";
                    case DayOfWeek.Wednesday:
                        return "Wednesday";
                    case DayOfWeek.Thursday:
                        return "Thursday";
                    case DayOfWeek.Friday:
                        return "Friday";
                    case DayOfWeek.Saturday:
                        return "Saturday";
                    default:
                        return "Sunday";
                }
            }
            set
            {
                switch (value)
                {
                    case "Sunday":
                        this.Day = DayOfWeek.Sunday;
                        break;
                    case "Monday":
                        this.Day = DayOfWeek.Monday;
                        break;
                    case "Tuesday":
                        this.Day = DayOfWeek.Tuesday;
                        break;
                    case "Wednesday":
                        this.Day = DayOfWeek.Wednesday;
                        break;
                    case "Thursday":
                        this.Day = DayOfWeek.Thursday;
                        break;
                    case "Friday":
                        this.Day = DayOfWeek.Friday;
                        break;
                    case "Saturday":
                        this.Day = DayOfWeek.Saturday;
                        break;
                    default:
                        this.Day = DayOfWeek.Sunday;
                        break;
                }
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Day>" + ((int)this.Day).ToString() + @"</Day>");
            result.AppendLine(@"<Time>" + this.Time.ToString() + @"</Time>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt;
            DateTime tempDateTime;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Day":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Day = (DayOfWeek)tempInt;
                        break;
                    case "Time":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.Time = tempDateTime;
                        break;
                }
            }
        }
    }
    #endregion

    public class FolderLink
    {
        public Guid RootId { get; set; }
        public DirectoryInfo Folder { get; set; }

        public FolderLink()
        {
            this.RootId = Guid.Empty;
        }

        public bool IsDrive
        {
            get
            {
                return this.Folder.FullName.Equals(this.Folder.Root.FullName);
            }
        }

        public virtual string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (this.Folder != null)
            {
                result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
                result.AppendLine(@"<Folder>" + this.Folder.FullName.ToString() + @"</Folder>");
            }
            return result.ToString();
        }

        public virtual void Deserialize(XmlNode node)
        {
            Guid tempGuid = Guid.Empty;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "Folder":
                        this.Folder = new DirectoryInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }

    public class RootFolder : FolderLink
    {
        public Library Parent { get; private set; }
        public int Order { get; set; }

        public int Index
        {
            get
            {
                return this.Order + 1;
            }
        }

        public string Path
        {
            get
            {
                return this.Folder != null ? this.Folder.FullName : null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && Directory.Exists(value))
                    this.Folder = new DirectoryInfo(value);
            }
        }

        public RootFolder(Library parent)
        {
            this.Parent = parent;
            this.Order = 0;
        }

        public override string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (this.Folder != null)
            {
                result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
                result.AppendLine(@"<Order>" + this.Order.ToString() + @"</Order>");
                result.AppendLine(@"<Folder>" + this.Folder.FullName.ToString() + @"</Folder>");
            }
            return result.ToString();
        }

        public override void Deserialize(XmlNode node)
        {
            Guid tempGuid = Guid.Empty;
            int tempInt;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Order = tempInt;
                        break;
                    case "Folder":
                        this.Folder = new DirectoryInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }

    public class FileLink
    {
        public Guid RootId { get; set; }
        public FileInfo File { get; set; }

        public FileLink()
        {
            this.RootId = Guid.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (this.File != null)
            {
                result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
                result.AppendLine(@"<File>" + this.File.FullName.ToString() + @"</File>");
            }
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            Guid tempGuid = Guid.Empty;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "File":
                        this.File = new FileInfo(childNode.InnerText);
                        break;
                }
            }
        }
    }
}
