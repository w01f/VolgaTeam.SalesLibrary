using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class LibraryFolder : ISyncObject
    {
        public Guid Identifier { get; set; }
        public LibraryPage Parent { get; set; }
        private string _name = string.Empty;
        private double _rowOrder = 0;
        private int _columnOrder = 0;
        private Color _borderColor = Color.Black;
        private Color _backgroundWindowColor = Color.White;
        private Color _foreWindowColor = Color.Black;
        private Color _backgroundHeaderColor = Color.White;
        private Color _foreHeaderColor = Color.Black;
        private Font _windowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        private Font _headerFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        private Alignment _headerAlignment = Alignment.Center;
        private bool _enableWidget = false;
        private Image _widget = null;
        private DateTime _lastChanged = DateTime.MinValue;

        public DateTime AddDate { get; set; }
        public BannerProperties BannerProperties { get; set; }
        public List<ILibraryFile> Files { get; set; }

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

        public double RowOrder
        {
            get
            {
                return _rowOrder;
            }
            set
            {
                if (_rowOrder != value)
                    this.LastChanged = DateTime.Now;
                _rowOrder = value;
            }
        }

        public int ColumnOrder
        {
            get
            {
                return _columnOrder;
            }
            set
            {
                if (_columnOrder != value)
                    this.LastChanged = DateTime.Now;
                _columnOrder = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if (_borderColor != value)
                    this.LastChanged = DateTime.Now;
                _borderColor = value;
            }
        }

        public Color BackgroundWindowColor
        {
            get
            {
                return _backgroundWindowColor;
            }
            set
            {
                if (_backgroundWindowColor != value)
                    this.LastChanged = DateTime.Now;
                _backgroundWindowColor = value;
            }
        }

        public Color ForeWindowColor
        {
            get
            {
                return _foreWindowColor;
            }
            set
            {
                if (_foreWindowColor != value)
                    this.LastChanged = DateTime.Now;
                _foreWindowColor = value;
            }
        }

        public Color BackgroundHeaderColor
        {
            get
            {
                return _backgroundHeaderColor;
            }
            set
            {
                if (_backgroundHeaderColor != value)
                    this.LastChanged = DateTime.Now;
                _backgroundHeaderColor = value;
            }
        }

        public Color ForeHeaderColor
        {
            get
            {
                return _foreHeaderColor;
            }
            set
            {
                if (_foreHeaderColor != value)
                    this.LastChanged = DateTime.Now;
                _foreHeaderColor = value;
            }
        }

        public Font WindowFont
        {
            get
            {
                return _windowFont;
            }
            set
            {
                if (_windowFont != value)
                    this.LastChanged = DateTime.Now;
                _windowFont = value;
            }
        }

        public Font HeaderFont
        {
            get
            {
                return _headerFont;
            }
            set
            {
                if (_headerFont != value)
                    this.LastChanged = DateTime.Now;
                _headerFont = value;
            }
        }

        public Alignment HeaderAlignment
        {
            get
            {
                return _headerAlignment;
            }
            set
            {
                if (_headerAlignment != value)
                    this.LastChanged = DateTime.Now;
                _headerAlignment = value;
            }
        }

        public bool EnableWidget
        {
            get
            {
                return _enableWidget;
            }
            set
            {
                if (_enableWidget != value)
                    this.LastChanged = DateTime.Now;
                _enableWidget = value;
            }
        }

        public Image Widget
        {
            get
            {
                return _widget;
            }
            set
            {
                if (_widget != value)
                    this.LastChanged = DateTime.Now;
                _widget = value;
            }
        }

        public DateTime LastChanged
        {
            get
            {
                return _lastChanged;
            }
            set
            {
                _lastChanged = value;
                this.Parent.LastChanged = _lastChanged;
            }
        }

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
            this.AddDate = DateTime.Now;

            this.BannerProperties = new BannerProperties(this);
            this.BannerProperties.Font = _headerFont;
            this.BannerProperties.ForeColor = _foreHeaderColor;

            this.Files = new List<ILibraryFile>();
        }

        public string Serialize()
        {
            FontConverter converter = new FontConverter();
            TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<RowOrder>" + _rowOrder + @"</RowOrder>");
            result.AppendLine(@"<ColumnOrder>" + _columnOrder + @"</ColumnOrder>");
            result.AppendLine(@"<BorderColor>" + _borderColor.ToArgb() + @"</BorderColor>");
            result.AppendLine(@"<BackgroundWindowColor>" + _backgroundWindowColor.ToArgb() + @"</BackgroundWindowColor>");
            result.AppendLine(@"<ForeWindowColor>" + _foreWindowColor.ToArgb() + @"</ForeWindowColor>");
            result.AppendLine(@"<BackgroundHeaderColor>" + _backgroundHeaderColor.ToArgb() + @"</BackgroundHeaderColor>");
            result.AppendLine(@"<ForeHeaderColor>" + _foreHeaderColor.ToArgb() + @"</ForeHeaderColor>");
            result.AppendLine(@"<WindowFont>" + converter.ConvertToString(_windowFont) + @"</WindowFont>");
            result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(_headerFont) + @"</HeaderFont>");
            result.AppendLine(@"<HeaderAligment>" + ((int)_headerAlignment).ToString() + @"</HeaderAligment>");
            result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
            if (_widget != null)
                result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
            result.AppendLine(@"<AddDate>" + this.AddDate.ToString() + @"</AddDate>");
            result.AppendLine(@"<LastChanged>" + (_lastChanged != DateTime.MinValue ? _lastChanged.ToString() : DateTime.Now.ToString()) + @"</LastChanged>");
            result.AppendLine("<Files>");
            foreach (ILibraryFile file in this.Files)
                result.AppendLine(@"<File>" + file.Serialize() + @"</File>");
            result.AppendLine("</Files>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt = 0;
            bool tempBool;
            FontConverter converter = new FontConverter();
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
                    case "RowOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _rowOrder = tempInt;
                        break;
                    case "ColumnOrder":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _columnOrder = tempInt;
                        break;
                    case "BorderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _borderColor = Color.FromArgb(tempInt);
                        break;
                    case "BackgroundWindowColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _backgroundWindowColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeWindowColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _foreWindowColor = Color.FromArgb(tempInt);
                        break;
                    case "BackgroundHeaderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _backgroundHeaderColor = Color.FromArgb(tempInt);
                        break;
                    case "ForeHeaderColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _foreHeaderColor = Color.FromArgb(tempInt);
                        break;
                    case "WindowFont":
                        try
                        {
                            _windowFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "HeaderFont":
                        try
                        {
                            _headerFont = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "HeaderAligment":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _headerAlignment = (Alignment)tempInt;
                        break;
                    case "EnableWidget":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _enableWidget = tempBool;
                        break;
                    case "Widget":
                        if (!string.IsNullOrEmpty(childNode.InnerText))
                            _widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "BannerProperties":
                        this.BannerProperties.Deserialize(childNode);
                        break;
                    case "AddDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.AddDate = tempDateTime;
                        break;
                    case "LastChanged":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            _lastChanged = tempDateTime;
                        break;
                    case "Files":
                        this.Files.Clear();
                        foreach (XmlNode fileNode in childNode.ChildNodes)
                        {
                            ILibraryFile file = this.Parent.Parent.GetLinkInstance(this);
                            file.Deserialize(fileNode);
                            this.Files.Add(file);
                        }

                        #region Order Bug Fix
                        if (this.Files.Count > 0)
                        {
                            int maxOrder = this.Files.Select(x => x.Order).Max();
                            if (maxOrder == 0)
                                for (int i = 0; i < this.Files.Count; i++)
                                    if (this.Files[i].Order != i)
                                        this.Files[i].Order = i;
                        }
                        #endregion

                        this.Files.Sort((x, y) => x.Order.CompareTo(y.Order));
                        break;
                }
            }
            if (!this.BannerProperties.Configured)
            {
                this.BannerProperties.Text = _name;
                this.BannerProperties.Font = _headerFont;
                this.BannerProperties.ForeColor = _foreHeaderColor;
            }
        }

        public void RemoveFromParent()
        {
            this.Parent.Folders.Remove(this);
            this.Parent.LastChanged = DateTime.Now;
        }

        public ILibraryFile[] SearchByTags(LibraryFileSearchTags searchCriteria)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (ILibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
            {
                bool fullMatch = true;
                bool partialMatch = false;
                foreach (SearchGroup group in searchCriteria.SearchGroups)
                {
                    SearchGroup fileSearchGroup = file.SearchTags.SearchGroups.Where(x => x.Name.Equals(group.Name)).FirstOrDefault();
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

        public ILibraryFile[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
        {
            string[] templateParts = template.Split(' ');
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (ILibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
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

        public ILibraryFile[] SearchByDate(DateTime startDate, DateTime endDate)
        {
            List<ILibraryFile> searchFiles = new List<ILibraryFile>();
            foreach (ILibraryFile file in this.Files.Where(x => x.Type != FileTypes.LineBreak))
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
}
