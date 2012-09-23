using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class LibraryPage : ISyncObject
    {
        private string _name = string.Empty;
        private bool _enable = false;
        private int _order = 0;
        private bool _enableColumnTitles = false;
        private bool _applyForAllColumnTitles = false;

        public ILibrary Parent { get; set; }
        public Guid Identifier { get; set; }
        public List<LibraryFolder> Folders { get; set; }
        public List<ColumnTitle> ColumnTitles { get; set; }
        public DateTime LastChanged { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                    this.LastChanged = DateTime.Now;
                _name = value;
            }
        }

        public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                if (_enable != value)
                    this.LastChanged = DateTime.Now;
                _enable = value;
            }
        }

        public int Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (_order != value)
                    this.LastChanged = DateTime.Now;
                _order = value;
            }
        }

        public bool EnableColumnTitles
        {
            get
            {
                return _enableColumnTitles;
            }
            set
            {
                if (_enableColumnTitles != value)
                    this.LastChanged = DateTime.Now;
                _enableColumnTitles = value;
            }
        }

        public bool ApplyForAllColumnTitles
        {
            get
            {
                return _applyForAllColumnTitles;
            }
            set
            {
                if (_applyForAllColumnTitles != value)
                    this.LastChanged = DateTime.Now;
                _applyForAllColumnTitles = value;
            }
        }

        public int Index
        {
            get
            {
                return _order + 1;
            }
        }

        public LibraryPage(ILibrary parent, bool isHome = false)
        {
            this.Parent = parent;
            _name = isHome ? "Page 1" : string.Format("Page {0}", this.Parent.Pages.Count + 1);
            _enable = isHome;
            this.Identifier = Guid.NewGuid();
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
            result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Order>" + _order + @"</Order>");
            result.AppendLine(@"<EnableColumnTitles>" + _enableColumnTitles + @"</EnableColumnTitles>");
            result.AppendLine(@"<ApplyForAllColumnTitles>" + _applyForAllColumnTitles + @"</ApplyForAllColumnTitles>");
            result.AppendLine(@"<LastChanged>" + this.LastChanged.ToString() + @"</LastChanged>");
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
            Guid tempGuid;
            DateTime tempDateTime;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        _name = childNode.InnerText;
                        break;
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _enable = tempBool;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _order = tempInt;
                        break;
                    case "EnableColumnTitles":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _enableColumnTitles = tempBool;
                        break;
                    case "ApplyForAllColumnTitles":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _applyForAllColumnTitles = tempBool;
                        break;
                    case "LastChanged":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.LastChanged = tempDateTime;
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

        public ILibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (LibraryFolder folder in this.Folders)
                searchFiles.AddRange(folder.SearchByTags(searchCriteria));
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (LibraryFolder folder in this.Folders)
                searchFiles.AddRange(folder.SearchByName(template, fullMatchOnly, type));
            return searchFiles.ToArray();
        }

        public ILibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (LibraryFolder folder in this.Folders)
                searchFiles.AddRange(folder.SearchByDate(startDate, endDate));
            return searchFiles.ToArray();
        }
    }
}
