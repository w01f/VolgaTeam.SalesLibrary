using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.BusinessClasses
{
    public class LibraryPackage
    {
        private string _name;
        public DirectoryInfo Folder { get; set; }
        private List<Library> _libraryCollection = new List<Library>();

        public List<Library> SalesDepotCollection
        {
            get
            {
                return _libraryCollection;
            }
        }

        public string Name
        {
            get
            {
                if (_name.Equals(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage))
                {
                    if (_libraryCollection.Count > 0)
                        return _libraryCollection[0].BrandingText;
                    else
                        return ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
                }
                else
                    return _name;
            }
        }

        public LibraryPackage(string name, DirectoryInfo folder)
        {
            this.Folder = folder;
            _name = name;
            LoadSalesDepots();
        }

        private void LoadSalesDepots()
        {
            Library library = null;
            if (this.Folder.GetFiles("*.xml").Length > 0 && !this.Folder.Name.ToLower().Equals("_gsdata_"))
            {
                library = new Library(this, this.Folder.Name.Equals(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage) ? this.Folder.Parent.Name : this.Folder.Name, this.Folder);
                if (library != null && (ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Count == 0 || ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Contains(library.Name.ToLower())))
                    _libraryCollection.Add(library);
            }
            else
                foreach (DirectoryInfo subFolder in this.Folder.GetDirectories())
                    if (!subFolder.Name.StartsWith("!") && !subFolder.Name.ToLower().Equals("_gsdata_"))
                    {
                        DirectoryInfo primaryRootFolder = new DirectoryInfo(Path.Combine(subFolder.FullName, ConfigurationClasses.SettingsManager.WholeDriveFilesStorage));
                        if (primaryRootFolder.Exists)
                            library = new Library(this,primaryRootFolder.Parent.Name, primaryRootFolder);
                        else
                            library = new Library(this, subFolder.Name, subFolder);
                        if (library != null && (ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Count == 0 || ConfigurationClasses.SettingsManager.Instance.ApprovedLibraries.Contains(library.Name.ToLower())))
                            _libraryCollection.Add(library);
                    }
        }

        public LibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (Library library in _libraryCollection)
                searchFiles.AddRange(library.SearchByTags(searchCriteria));
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (Library library in _libraryCollection)
                searchFiles.AddRange(library.SearchByName(template, fullMatchOnly, type));
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (Library library in _libraryCollection)
                searchFiles.AddRange(library.SearchByDate(startDate, endDate));
            return searchFiles.ToArray();
        }
    }

    public class Library
    {
        private string _name;

        public LibraryPackage Parent { get; private set; }
        public Guid Identifier { get; set; }
        public DirectoryInfo Folder { get; set; }
        public string BrandingText { get; set; }
        public DateTime SyncDate { get; set; }

        public bool ApplyForAllWindows { get; set; }
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

        public List<LibraryPage> Pages { get; set; }
        public List<string> EmailList { get; set; }
        public List<AutoWidget> AutoWidgets { get; set; }

        public OvernightsCalendar OvernightsCalendar { get; set; }

        public string Name 
        {
            get
            {
                if (_name.Equals(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage))
                    return this.Parent.Name;
                else
                    return _name;
            } 
        }

        public Library(LibraryPackage parent,string name, DirectoryInfo folder)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
            this.Folder = folder;
            _name = name;
            this.IsConfigured = false;
            this.Pages = new List<LibraryPage>();
            this.EmailList = new List<string>();
            this.AutoWidgets = new List<AutoWidget>();
            this.OvernightsCalendar = new OvernightsCalendar(this);
            Load();
        }

        private void Load()
        {
            DateTime tempDate = DateTime.Now;
            bool tempBool = false;

            this.BrandingText = string.Empty;
            this.SyncDate = DateTime.Now;
            this.SyncLinkedFiles = true;
            this.ApplyForAllWindows = false;
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

            string file = Path.Combine(this.Folder.FullName, ConfigurationClasses.SettingsManager.StorageFileName);
            if (File.Exists(file))
            {
                XmlDocument document = new XmlDocument();
                document.Load(file);
                XmlNode node = document.SelectSingleNode(@"/Cache");
                if (node != null)
                {
                    FileManager.BusinessClasses.OldFormatLibrary oldFormatLibrary = new FileManager.BusinessClasses.OldFormatLibrary(_name, this.Folder);
                    document.LoadXml(oldFormatLibrary.SerializeInNewFormat());
                    LibraryManager.Instance.OldFormatDetected = true;
                }
                else
                    LibraryManager.Instance.OldFormatDetected = false;
                node = document.SelectSingleNode(@"/Library/Name");
                if (node != null)
                    _name = node.InnerText;
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
                node = document.SelectSingleNode(@"/Library/ApplyForAllWindows");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ApplyForAllWindows = tempBool;
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
                this.IsConfigured = true;
            }
            if (this.Pages.Count == 0)
                this.Pages.Add(new LibraryPage(this, true));
        }

        public LibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryPage page in this.Pages)
                searchFiles.AddRange(page.SearchByTags(searchCriteria));
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryPage page in this.Pages)
                searchFiles.AddRange(page.SearchByName(template, fullMatchOnly, type));
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryPage page in this.Pages)
                searchFiles.AddRange(page.SearchByDate(startDate, endDate));
            return searchFiles.ToArray();
        }
    }

    public class LibraryPage
    {
        public Library Parent { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public int Order { get; set; }
        public bool EnableColumnTitles { get; set; }
        public bool ApplyForAllColumnTitles { get; set; }
        public List<LibraryFolder> Folders { get; set; }
        public List<ColumnTitle> ColumnTitles { get; set; }

        public LibraryPage(Library parent, bool isHome = false)
        {
            this.Parent = parent;
            this.Name = isHome ? "Page 1" : string.Empty;
            this.Enable = isHome;
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
            result.AppendLine(@"<Enable>" + this.Enable + @"</Enable>");
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
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Enable = tempBool;
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

        public LibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryFolder folder in this.Folders)
                searchFiles.AddRange(folder.SearchByTags(searchCriteria));
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryFolder folder in this.Folders)
                searchFiles.AddRange(folder.SearchByName(template, fullMatchOnly, type));
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryFolder folder in this.Folders)
                searchFiles.AddRange(folder.SearchByDate(startDate, endDate));
            return searchFiles.ToArray();
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

        public ColumnTitle(LibraryPage parent)
        {
            this.Parent = parent;
            this.Name = string.Empty;
            this.ColumnOrder = 0;
            this.BackgroundColor = Color.White;
            this.ForeColor = Color.Black;
            this.HeaderFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<ColumnOrder>" + this.ColumnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BackgroundColor>" + this.BackgroundColor.ToArgb() + @"</BackgroundColor>");
            result.AppendLine(@"<ForeColor>" + this.ForeColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
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
        public Color BackgroundWindowColor { get; set; }
        public Color ForeWindowColor { get; set; }
        public Color BackgroundHeaderColor { get; set; }
        public Color ForeHeaderColor { get; set; }
        public Font WindowFont { get; set; }
        public Font HeaderFont { get; set; }

        public List<LibraryFile> Files { get; set; }

        public double AbsoluteRowOrder
        {
            get
            {
                return this.Parent.Folders.Where(x => x.ColumnOrder < this.ColumnOrder).Count() + this.Parent.Folders.Where(x => x.ColumnOrder == this.ColumnOrder).ToList().IndexOf(this);
            }
        }

        public LibraryFolder(LibraryPage parent)
        {
            this.Identifier = Guid.NewGuid();
            this.Parent = parent;
            this.Name = string.Empty;
            this.RowOrder = 0;
            this.ColumnOrder = 0;
            this.BackgroundWindowColor = Color.White;
            this.ForeWindowColor = Color.Black;
            this.BackgroundHeaderColor = Color.White;
            this.ForeHeaderColor = Color.Black;
            this.WindowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            this.HeaderFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            this.Files = new List<LibraryFile>();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<RowOrder>" + this.RowOrder + @"</RowOrder>");
            result.AppendLine(@"<ColumnOrder>" + this.ColumnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BackgroundWindowColor>" + this.BackgroundWindowColor.ToArgb() + @"</BackgroundWindowColor>");
            result.AppendLine(@"<ForeWindowColor>" + this.ForeWindowColor.ToArgb() + @"</ForeWindowColor>");
            result.AppendLine(@"<BackgroundHeaderColor>" + this.BackgroundHeaderColor.ToArgb() + @"</BackgroundHeaderColor>");
            result.AppendLine(@"<ForeHeaderColor>" + this.ForeHeaderColor.ToArgb() + @"</ForeHeaderColor>");
            result.AppendLine(@"<WindowFont>" + converter.ConvertToString(this.WindowFont) + @"</WindowFont>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(this.HeaderFont) + @"</HeaderFont>");
            result.AppendLine("<Files>");
            foreach (LibraryFile file in this.Files)
                result.AppendLine(@"<File>" + file.Serialize() + @"</File>");
            result.AppendLine("</Files>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
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
        }

        public LibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = true;
                bool partialMatch = false;
                foreach (ConfigurationClasses.SearchGroup group in searchCriteria.SearchGroups)
                {
                    ConfigurationClasses.SearchGroup fileSearchGroup = file.SearchTags.SearchGroups.Where(x => x.Name.Equals(group.Name)).FirstOrDefault();
                    if (fileSearchGroup != null)
                    {
                        foreach (string tag in group.Tags)
                            if (fileSearchGroup.Tags.Contains(tag))
                            {
                                partialMatch = true;
                                fullMatch = fullMatch & true;
                            }
                            else
                            {
                                fullMatch = fullMatch & false;
                            }
                    }
                    else
                        fullMatch = fullMatch & false;
                }
                if (partialMatch)
                {
                    file.CriteriaOverlap = fullMatch ? "meet ALL of your Search Criteria" : "meet SOME of your Search Criteria";
                    searchFiles.Add(file);
                }
                else
                    file.CriteriaOverlap = string.Empty;
            }
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            string[] templateParts = template.Split(' ');
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = false;
                bool partialMatch = false;

                if (file.Name.ToLower().Equals(template) && !string.IsNullOrEmpty(template) && ((type == file.Type) || (type == FileTypes.Other)))
                {
                    fullMatch = true;
                    partialMatch = true;
                }
                else if (!string.IsNullOrEmpty(template) && ((type == file.Type) || (type == FileTypes.Other)))
                {
                    if (templateParts.Length > 1)
                    {
                        foreach (string templatePart in templateParts)
                            if (file.Name.ToLower().Contains(templatePart.Trim().ToLower()))
                            {
                                fullMatch = false;
                                partialMatch = true;
                                break;
                            }
                    }
                    else if (file.Name.ToLower().Contains(template))
                    {
                        fullMatch = false;
                        partialMatch = true;
                    }
                }

                if ((partialMatch && !fullMatchOnly) || fullMatch)
                {
                    file.CriteriaOverlap = fullMatch ? "meet ALL of your Search Criteria" : "meet SOME of your Search Criteria";
                    searchFiles.Add(file);
                }
            }
            return searchFiles.ToArray();
        }

        public LibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<LibraryFile> searchFiles = new List<LibraryFile>();
            foreach (LibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = false;

                if (file.AddDate >= startDate && file.AddDate <= endDate)
                    fullMatch = true;

                if (fullMatch)
                {
                    file.CriteriaOverlap = "meet ALL of your Search Criteria";
                    searchFiles.Add(file);
                }
                else
                    file.CriteriaOverlap = string.Empty;
            }
            return searchFiles.ToArray();
        }
    }

    public class LibraryFile
    {
        private string _note = string.Empty;
        private Image _widget = null;

        public string Name { get; set; }
        public LibraryFolder Parent { get; set; }
        public Guid Identifier { get; set; }
        public string RelativePath { get; set; }
        public FileTypes Type { get; set; }
        public string Format;
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsDead { get; set; }
        public DateTime AddDate { get; set; }
        public bool EnableWidget { get; set; }
        public bool EnableBanner { get; set; }
        public Image Banner { get; set; }
        public string CriteriaOverlap { get; set; }

        public LibraryFileSearchTags SearchTags { get; set; }
        public ExpirationDateOptions ExpirationDateOptions { get; set; }
        public PresentationPreviewContainer PreviewContainer { get; set; }
        public PresentationProperties PresentationProperties { get; set; }
        public LineBreakProperties LineBreakProperties { get; set; }

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
            set
            {
                this.Name = value;
            }
        }

        public string PropertiesName
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder)
                    return this.Name;
                else if (this.Type == FileTypes.LineBreak)
                    return string.Empty;
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

        public string FullPath
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network)
                    return this.RelativePath;
                else if (this.Type == FileTypes.LineBreak)
                    return string.Empty;
                else
                    return ((this.Parent != null ? this.Parent.Parent.Parent.Folder.FullName : string.Empty) + @"\" + this.RelativePath).Replace(@"\\", @"\").Replace(@"\\", @"\");
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
                    case FileTypes.Excel:
                    case FileTypes.PDF:
                    case FileTypes.Word:
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
            this.Identifier = Guid.NewGuid();
            this.RelativePath = string.Empty;
            this.Type = FileTypes.Other;
            this.Format = string.Empty;
            this.Order = 0;
            this.IsBold = false;
            this.IsDead = false;
            this.AddDate = DateTime.Now;
            this.CriteriaOverlap = string.Empty;
            this.SearchTags = new LibraryFileSearchTags();
            this.ExpirationDateOptions = new BusinessClasses.ExpirationDateOptions();
            this.PreviewContainer = null;
            SetProperties();
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<DisplayName>" + this.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</DisplayName>");
            result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            result.AppendLine(@"<IsDead>" + this.IsDead + @"</IsDead>");
            result.AppendLine(@"<IsBold>" + this.IsBold + @"</IsBold>");
            result.AppendLine(@"<RelativePath>" + this.RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
            result.AppendLine(@"<Type>" + (int)this.Type + @"</Type>");
            result.AppendLine(@"<Format>" + this.Format.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Format>");
            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            result.AppendLine(@"<AddDate>" + this.AddDate + @"</AddDate>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<EnableBanner>" + this.EnableBanner + @"</EnableBanner>");
            result.AppendLine(@"<Banner>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Banner, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Banner>");
            result.AppendLine(this.SearchTags.Serialize());
            result.AppendLine(@"<ExpirationDateOptions>" + this.ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");
            if (this.PreviewContainer != null)
                result.AppendLine(@"<PreviewContainer>" + this.PreviewContainer.Serialize() + @"</PreviewContainer>");
            if (this.PresentationProperties != null)
                result.AppendLine(@"<PresentationProperties>" + this.PresentationProperties.Serialize() + @"</PresentationProperties>");
            if (this.LineBreakProperties != null)
                result.AppendLine(@"<LineBreakProperties>" + this.LineBreakProperties.Serialize() + @"</LineBreakProperties>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt = 0;
            DateTime tempDate = DateTime.Now;

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
                    case "IsDead":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.IsDead = tempBool;
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
                        this.EnableBanner |= this.LineBreakProperties.EnableBanner;
                        if (this.LineBreakProperties.Banner != null)
                            this.Banner = this.LineBreakProperties.Banner;
                        break;
                }
            }
            if (this.Type == FileTypes.Other || this.Type == FileTypes.MediaPlayerVideo || this.Type == FileTypes.QuickTimeVideo)
                SetProperties();
        }

        public void SetProperties()
        {
            switch (this.Extension.ToUpper())
            {
                case ".PPT":
                case ".PPTX":
                    this.Type = FileTypes.OtherPresentation;
                    break;
                case ".DOC":
                case ".DOCX":
                    this.Type = FileTypes.Word;
                    break;
                case ".XLS":
                case ".XLSX":
                    this.Type = FileTypes.Excel;
                    break;
                case ".PDF":
                    this.Type = FileTypes.PDF;
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
        private LibraryFile _parent = null;
        private string _folderName = Guid.NewGuid().ToString();
        public string PreviewStorageFolder { get; set; }
        public List<PresentationPreviewSlide> Slides { get; set; }
        public int SelectedIndex { get; set; }

        public PresentationPreviewContainer(LibraryFile parent)
        {
            _parent = parent;
            if (_parent.Parent != null)
                this.PreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName, _folderName);
            Slides = new List<PresentationPreviewSlide>();
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
                        if (_parent.Parent != null)
                            this.PreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.PreviewContainersRootFolderName, _folderName);
                        break;
                }
            }
        }

        public void ReleasePreviewImages()
        {
            foreach (PresentationPreviewSlide slide in this.Slides)
                slide.PreviewImage.Dispose();
            this.Slides.Clear();
        }

        public Image SelectedSlide
        {
            get
            {
                if (this.SelectedIndex >= 0 && this.SelectedIndex < this.Slides.Count)
                    return this.Slides[this.SelectedIndex].PreviewImage;
                else
                    return null;
            }
        }

        public void GetPreviewImages()
        {
            if (Directory.Exists(this.PreviewStorageFolder))
            {
                this.Slides.Clear();
                string[] previewImages = Directory.GetFiles(this.PreviewStorageFolder, "*.png");
                Array.Sort(previewImages, (x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                for (int i = 0; i < previewImages.Length; i++)
                {
                    PresentationPreviewSlide slide = new PresentationPreviewSlide();
                    slide.Index = i;
                    slide.PreviewImage = new System.Drawing.Bitmap(previewImages[i], true);
                    this.Slides.Add(slide);
                }
            }
        }

        public bool CheckPreviewImages()
        {
            bool result = false;
            if (Directory.Exists(this.PreviewStorageFolder))
                result = Directory.GetFiles(this.PreviewStorageFolder, "*.png").Length > 0;
            return result;
        }
    }

    public class PresentationPreviewSlide
    {
        public int Index { get; set; }
        public Bitmap PreviewImage { get; set; }
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
        Network,
        PDF,
        Excel,
        Word
    }
}
