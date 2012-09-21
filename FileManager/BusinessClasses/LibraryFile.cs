using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects;

namespace FileManager.BusinessClasses
{
    public class LibraryFile : ILibraryFile
    {
        private string _linkLocalPath = string.Empty;
        private string _name = string.Empty;
        private string _note = string.Empty;
        private int _order = 0;
        private bool _isBold = false;
        private bool _isDead = false;
        private bool _enableWidget = false;
        private Image _widget = null;

        #region Compatibility with old versions
        private bool _oldEnableBanner;
        private Image _oldBanner;
        #endregion

        private DateTime _lastChanged =DateTime.MinValue;

        public LibraryFolder Parent { get; set; }
        public Guid RootId { get; set; }
        public Guid Identifier { get; set; }
        public string RelativePath { get; set; }
        public FileTypes Type { get; set; }
        public DateTime AddDate { get; set; }
        public string CriteriaOverlap { get; set; }

        public LibraryFileSearchTags SearchTags { get; set; }
        public ExpirationDateOptions ExpirationDateOptions { get; set; }
        public PresentationProperties PresentationProperties { get; set; }
        public LineBreakProperties LineBreakProperties { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public IPreviewContainer UniversalPreviewContainer { get; set; }
        #region Compatibility with desktop version of Sales Depot
        public PresentationPreviewContainer PreviewContainer { get; set; }
        #endregion

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

        public string Note
        {
            get
            {
                if (_isDead && this.Parent.Parent.Parent.EnableInactiveLinks && (this.Parent.Parent.Parent.InactiveLinksBoldWarning || this.Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak))
                    return string.Empty;
                else
                    return _note;

            }
            set
            {
                if (_note != value)
                    this.LastChanged = DateTime.Now;
                _note = value;
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

        public bool IsBold
        {
            get
            {
                return _isBold;
            }
            set
            {
                if (_isBold != value)
                    this.LastChanged = DateTime.Now;
                _isBold = value;
            }
        }

        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                if (_isDead != value)
                    this.LastChanged = DateTime.Now;
                _isDead = value;
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
                if (_enableWidget && _widget != null)
                    return _widget;
                else if (this.Parent != null)
                    return this.Parent.Parent.Parent.AutoWidgets.Where(x => x.Extension.ToLower().Equals(!string.IsNullOrEmpty(this.Extension) ? this.Extension.Substring(1).ToLower() : string.Empty)).Select(y => y.Widget).FirstOrDefault();
                else
                    return null;
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

        public string OriginalPath
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
                if (_isDead && this.Parent.Parent.Parent.EnableInactiveLinks)
                {
                    if (this.Parent.Parent.Parent.InactiveLinksBoldWarning)
                    {
                        if (!_name.Contains("INACTIVE!"))
                            return "INACTIVE! " + _name;
                        else
                            return _name;
                    }
                    else if (this.Parent.Parent.Parent.ReplaceInactiveLinksWithLineBreak)
                        return string.Empty;
                    else
                        return _name;
                }
                else if (this.ExpirationDateOptions.EnableExpirationDate && this.ExpirationDateOptions.LabelLinkWhenExpired && this.IsExpired)
                    return "EXPIRED! " + _name;
                else
                    return _name;
            }
        }

        public string PropertiesName
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder || this.Type == FileTypes.LineBreak)
                    return _name;
                else
                    return Path.GetFileName(this.OriginalPath);
            }
        }

        public string NameWithExtension
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder)
                    return _name;
                else if (this.Type == FileTypes.LineBreak)
                    return string.Empty;
                else
                    return Path.GetFileName(this.OriginalPath);
            }
        }

        public string NameWithoutExtesion
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder)
                    return _name;
                else if (this.Type == FileTypes.LineBreak)
                    return string.Empty;
                else
                {
                    return Path.GetFileNameWithoutExtension(this.OriginalPath);
                }
            }
        }

        public string Extension
        {
            get
            {
                switch (this.Type)
                {
                    case FileTypes.Presentation:
                    case FileTypes.BuggyPresentation:
                    case FileTypes.FriendlyPresentation:
                    case FileTypes.MediaPlayerVideo:
                    case FileTypes.Other:
                    case FileTypes.QuickTimeVideo:
                        return Path.GetExtension(this.OriginalPath);
                    default:
                        return string.Empty;
                }
            }
        }

        public bool DisplayAsBold
        {
            get
            {
                if (_isDead && this.Parent.Parent.Parent.EnableInactiveLinks && this.Parent.Parent.Parent.InactiveLinksBoldWarning)
                    return true;
                else if (this.ExpirationDateOptions.EnableExpirationDate && this.IsExpired && this.ExpirationDateOptions.LabelLinkWhenExpired)
                    return true;
                else
                    return _isBold;
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

        public string Content
        {
            get
            {
                if (this.UniversalPreviewContainer != null)
                    return this.UniversalPreviewContainer.GetTextContent();
                else
                    return string.Empty;
            }
        }

        public LibraryFile(LibraryFolder parent)
        {
            this.Parent = parent;
            this.RootId = Guid.Empty;
            this.Identifier = Guid.NewGuid();
            this.RelativePath = string.Empty;
            this.Type = FileTypes.Other;
            this.AddDate = DateTime.Now;
            this.SearchTags = new LibraryFileSearchTags();
            this.ExpirationDateOptions = new ExpirationDateOptions();
            SetProperties();
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<DisplayName>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</DisplayName>");
            result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            result.AppendLine(@"<IsBold>" + _isBold + @"</IsBold>");
            result.AppendLine(@"<IsDead>" + _isDead + @"</IsDead>");
            result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
            result.AppendLine(@"<LocalPath>" + _linkLocalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalPath>");
            result.AppendLine(@"<RelativePath>" + this.RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
            result.AppendLine(@"<Type>" + (int)this.Type + @"</Type>");
            result.AppendLine(@"<Order>" + _order + @"</Order>");
            result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
            result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(@"<AddDate>" + this.AddDate.ToString() + @"</AddDate>");
            result.AppendLine(@"<LastChanged>" + (_lastChanged != DateTime.MinValue ? _lastChanged.ToString() : DateTime.Now.ToString()) + @"</LastChanged>");
            result.Append(this.SearchTags.Serialize());
            result.AppendLine(@"<ExpirationDateOptions>" + this.ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");
            #region Compatibility with desktop version of Sales Depot
            if (this.PreviewContainer != null)
                result.AppendLine(@"<PreviewContainer>" + this.PreviewContainer.Serialize() + @"</PreviewContainer>");
            #endregion
            if (this.UniversalPreviewContainer != null)
                result.AppendLine(@"<UniversalPreviewContainer>" + this.UniversalPreviewContainer.Serialize() + @"</UniversalPreviewContainer>");
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
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "DisplayName":
                        _name = childNode.InnerText;
                        break;
                    case "Note":
                        _note = childNode.InnerText;
                        break;
                    case "IsBold":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _isBold = tempBool;
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
                                this.LineBreakProperties = new LineBreakProperties(this);
                        }
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _order = tempInt;
                        break;
                    case "EnableWidget":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _enableWidget = tempBool;
                        break;
                    case "Widget":
                        if (string.IsNullOrEmpty(childNode.InnerText) && _enableWidget)
                            _widget = null;
                        else if (!string.IsNullOrEmpty(childNode.InnerText))
                            _widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "AddDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.AddDate = tempDate;
                        break;
                    case "LastChanged":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            _lastChanged = tempDate;
                        break;
                    case "SearchTags":
                        this.SearchTags.Deserialize(childNode);
                        break;
                    case "ExpirationDateOptions":
                        this.ExpirationDateOptions.Deserialize(childNode);
                        break;
                    #region Compatibility with desktop version of Sales Depot
                    case "PreviewContainer":
                        this.PreviewContainer = new PresentationPreviewContainer(this);
                        this.PreviewContainer.Deserialize(childNode);
                        break;
                    #endregion
                    case "UniversalPreviewContainer":
                        this.UniversalPreviewContainer = new UniversalPreviewContainer(this);
                        this.UniversalPreviewContainer.Deserialize(childNode);
                        break;
                    case "PresentationProperties":
                        this.PresentationProperties = new PresentationProperties();
                        this.PresentationProperties.Deserialize(childNode);
                        break;
                    case "LineBreakProperties":
                        this.LineBreakProperties = new LineBreakProperties(this);
                        this.LineBreakProperties.Font = new Font(this.Parent.WindowFont, this.Parent.WindowFont.Style);
                        this.LineBreakProperties.BoldFont = new Font(this.Parent.WindowFont, FontStyle.Bold);
                        this.LineBreakProperties.Deserialize(childNode);
                        break;
                    case "BannerProperties":
                        this.BannerProperties = new BannerProperties(this);
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

            SetProperties();
        }

        public void InitBannerProperties()
        {
            this.BannerProperties = new BannerProperties(this);
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
                        this.Type = FileTypes.Presentation;
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
                case FileTypes.BuggyPresentation:
                case FileTypes.FriendlyPresentation:
                case FileTypes.Presentation:
                case FileTypes.QuickTimeVideo:
                case FileTypes.MediaPlayerVideo:
                case FileTypes.Other:
                    if (!File.Exists(this.OriginalPath))
                        this.IsDead = true;
                    else
                        this.IsDead = false;
                    break;
                case FileTypes.Folder:
                    if (!Directory.Exists(this.OriginalPath))
                        this.IsDead = true;
                    else
                        this.IsDead = false;
                    break;
            }
        }

        public void RemoveFromCollection()
        {
            this.Parent.Files.Remove(this);
            this.Parent.LastChanged = DateTime.Now;
        }
    }
}
