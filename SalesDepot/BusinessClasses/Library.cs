using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SalesDepot.CoreObjects;

namespace SalesDepot.BusinessClasses
{
    public class Library : ILibrary
    {
        private string _name;
        private RootFolder _rootFolder = null;

        public LibraryPackage Parent { get; private set; }
        public Guid Identifier { get; set; }
        public DirectoryInfo StorageFolder { get; set; }
        public DirectoryInfo Folder { get; set; }
        public bool UseDirectAccess { get; set; }
        public DateTime DirectAccessFileBottomDate { get; set; }
        public string BrandingText { get; set; }
        public DateTime SyncDate { get; set; }

        public bool ApplyAppearanceForAllWindows { get; set; }
        public bool ApplyWidgetForAllWindows { get; set; }
        public bool ApplyBannerForAllWindows { get; set; }
        public bool SyncLinkedFiles { get; set; }
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
        public List<LibraryPage> Pages { get; set; }
        public List<string> EmailList { get; set; }
        public List<AutoWidget> AutoWidgets { get; set; }

        public OvernightsCalendar OvernightsCalendar { get; set; }
        public ProgramScheduleManager ProgramManager { get; set; }

        public string Name
        {
            get
            {
                if (_name.Equals(CoreObjects.Constants.WholeDriveFilesStorage))
                    return this.Parent.Name;
                else
                    return _name;
            }
        }

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

        public Library(LibraryPackage parent, string name, DirectoryInfo folder)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
            this.StorageFolder = folder;
            this.Folder = folder;
            _name = name;
            this.IsConfigured = false;
            this.ExtraFolders = new List<RootFolder>();
            this.Pages = new List<LibraryPage>();
            this.EmailList = new List<string>();
            this.AutoWidgets = new List<AutoWidget>();
            this.OvernightsCalendar = new OvernightsCalendar(this);
            this.ProgramManager = new BusinessClasses.ProgramScheduleManager(this);
            Load();
        }

        private void Load()
        {
            DateTime tempDate = DateTime.Now;
            bool tempBool = false;

            this.BrandingText = string.Empty;
            this.UseDirectAccess = false;
            this.SyncDate = DateTime.Now;
            this.SyncLinkedFiles = true;
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

            string file = Path.Combine(this.StorageFolder.FullName, CoreObjects.Constants.StorageFileName);
            if (File.Exists(file))
            {
                XmlDocument document = new XmlDocument();
                document.Load(file);
                XmlNode node = document.SelectSingleNode(@"/Cache");
                if (node != null)
                {
                    OldFormatLibrary oldFormatLibrary = new OldFormatLibrary(_name, this.Folder);
                    document.LoadXml(oldFormatLibrary.SerializeInNewFormat());
                    LibraryManager.Instance.OldFormatDetected = true;
                }
                else
                    LibraryManager.Instance.OldFormatDetected = false;
                node = document.SelectSingleNode(@"/Library/Name");
                if (node != null)
                    _name = node.InnerText;
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
                node = document.SelectSingleNode(@"/Library/SyncLinkedFiles");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.SyncLinkedFiles = tempBool;
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

                node = document.SelectSingleNode(@"/Library/OvernightsCalendar");
                if (node != null)
                    this.OvernightsCalendar.Deserialize(node);

                if (this.UseDirectAccess && !this.Folder.Exists)
                    this.IsConfigured = false;
                else
                    this.IsConfigured = true;
            }
            if (this.Pages.Count == 0)
                this.Pages.Add(new LibraryPage(this, true));
        }

        public ILibraryFile GetLinkInstance(LibraryFolder parentFolder)
        {
            return new LibraryFile(parentFolder);
        }

        public RootFolder GetRootFolder(Guid folderId)
        {
            RootFolder folder = this.ExtraFolders.Where(x => x.RootId.Equals(folderId)).FirstOrDefault();
            if (folder != null)
                return folder;
            else
                return this.RootFolder;
        }

        public ILibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (LibraryPage page in this.Pages)
                searchFiles.AddRange(page.SearchByTags(searchCriteria));
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (LibraryPage page in this.Pages)
                searchFiles.AddRange(page.SearchByName(template, fullMatchOnly, type));
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (LibraryPage page in this.Pages)
                searchFiles.AddRange(page.SearchByDate(startDate, endDate));
            return searchFiles.ToArray();
        }
    }
}
