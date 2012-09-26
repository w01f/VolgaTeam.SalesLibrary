using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.BusinessClasses
{
    public class LibraryFile : ILibraryFile
    {
        private string _note = string.Empty;
        private Image _widget = null;

        #region Compatibility with old versions
        private bool _oldEnableBanner;
        private Image _oldBanner;
        #endregion

        private bool _linkAvailabilityChecked = false;
        private bool _linkAvailabel = false;

        private string _linkRemotePath = string.Empty;
        private string _linkLocalPath = string.Empty;

        public string Name { get; set; }
        public LibraryFolder Parent { get; set; }
        public Guid RootId { get; set; }
        public Guid Identifier { get; set; }
        public string RelativePath { get; set; }
        public FileTypes Type { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsDead { get; set; }
        public bool EnableWidget { get; set; }
        public string CriteriaOverlap { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastChanged { get; set; }

        public LibraryFileSearchTags SearchTags { get; set; }
        public ExpirationDateOptions ExpirationDateOptions { get; set; }
        public PresentationPreviewContainer PreviewContainer { get; set; }
        public IPreviewContainer UniversalPreviewContainer { get; set; }
        public PresentationProperties PresentationProperties { get; set; }
        public LineBreakProperties LineBreakProperties { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public string OriginalPath
        {
            get
            {
                if (string.IsNullOrEmpty(_linkRemotePath))
                {
                    if (this.Type == FileTypes.Url || this.Type == FileTypes.Network)
                        return this.RelativePath;
                    else if (this.Type == FileTypes.LineBreak)
                        return string.Empty;
                    else
                        return ((this.Parent != null ? this.Parent.Parent.Parent.GetRootFolder(this.RootId).Folder.FullName : string.Empty) + @"\" + this.RelativePath).Replace(@"\\", @"\").Replace(@"\\", @"\");
                }
                else
                    return _linkRemotePath;
            }
            set
            {
                _linkRemotePath = value;
            }
        }

        public string LocalPath
        {
            get
            {
                if (string.IsNullOrEmpty(_linkLocalPath) && this.LinkAvailable)
                    GetLocalCopy();
                return _linkLocalPath;
            }
        }

        public bool LinkAvailable
        {
            get
            {
                if (!_linkAvailabilityChecked)
                {
                    switch (this.Type)
                    {
                        case FileTypes.BuggyPresentation:
                        case FileTypes.Excel:
                        case FileTypes.FriendlyPresentation:
                        case FileTypes.MediaPlayerVideo:
                        case FileTypes.Other:
                        case FileTypes.Presentation:
                        case FileTypes.PDF:
                        case FileTypes.QuickTimeVideo:
                        case FileTypes.Word:
                        case FileTypes.OvernightsLink:
                            _linkAvailabel = File.Exists(this.OriginalPath);
                            break;
                        case FileTypes.Folder:
                            _linkAvailabel = Directory.Exists(this.OriginalPath);
                            break;
                        default:
                            _linkAvailabel = true;
                            break;
                    }
                    _linkAvailabilityChecked = true;
                }
                return _linkAvailabel;
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
            set
            {
                this.Name = value;
            }
        }

        public string NameWithExtension
        {
            get
            {
                if (this.Type == FileTypes.Url || this.Type == FileTypes.Network || this.Type == FileTypes.Folder)
                    return this.Name;
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
                    return this.Name;
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
                    case FileTypes.BuggyPresentation:
                    case FileTypes.FriendlyPresentation:
                    case FileTypes.MediaPlayerVideo:
                    case FileTypes.Other:
                    case FileTypes.Presentation:
                    case FileTypes.QuickTimeVideo:
                    case FileTypes.Excel:
                    case FileTypes.PDF:
                    case FileTypes.Word:
                    case FileTypes.OvernightsLink:
                        return Path.GetExtension(this.OriginalPath);
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
            this.Order = 0;
            this.IsBold = false;
            this.IsDead = false;
            this.CriteriaOverlap = string.Empty;
            this.SearchTags = new LibraryFileSearchTags();
            this.ExpirationDateOptions = new ExpirationDateOptions();
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
            result.AppendLine(@"<RootId>" + this.RootId.ToString() + @"</RootId>");
            result.AppendLine(@"<LocalPath>" + _linkRemotePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalPath>");
            result.AppendLine(@"<RelativePath>" + this.RelativePath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RelativePath>");
            result.AppendLine(@"<Type>" + (int)this.Type + @"</Type>");
            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            result.AppendLine(@"<EnableWidget>" + this.EnableWidget + @"</EnableWidget>");
            result.Append(@"<Widget>" + Convert.ToBase64String((byte[])converter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
            result.AppendLine(this.SearchTags.Serialize());
            result.AppendLine(@"<ExpirationDateOptions>" + this.ExpirationDateOptions.Serialize() + @"</ExpirationDateOptions>");
            if (this.PreviewContainer != null)
                result.AppendLine(@"<PreviewContainer>" + this.PreviewContainer.Serialize() + @"</PreviewContainer>");
            if (this.PresentationProperties != null)
                result.AppendLine(@"<PresentationProperties>" + this.PresentationProperties.Serialize() + @"</PresentationProperties>");
            if (this.LineBreakProperties != null)
                result.AppendLine(@"<LineBreakProperties>" + this.LineBreakProperties.Serialize() + @"</LineBreakProperties>");
            if (this.BannerProperties != null && this.BannerProperties.Configured)
                result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
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
                    case "IsDead":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.IsDead = tempBool;
                        break;
                    case "RootId":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.RootId = tempGuid;
                        break;
                    case "LocalPath":
                        _linkRemotePath = childNode.InnerText;
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
                            this.Order = tempInt;
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
                    case "AddDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.AddDate = tempDate;
                        break;
                    case "LastChanged":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.LastChanged = tempDate;
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
                        this.LineBreakProperties = new LineBreakProperties(this);
                        this.LineBreakProperties.Font = new Font(this.Parent.WindowFont, this.Parent.WindowFont.Style);
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

            if (this.Type == FileTypes.Other || this.Type == FileTypes.MediaPlayerVideo || this.Type == FileTypes.QuickTimeVideo)
                SetProperties();
        }

        public void InitBannerProperties()
        {
            this.BannerProperties = new BannerProperties(this);
            try
            {
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
            catch { }
        }

        public void SetProperties()
        {
            switch (this.Extension.ToUpper())
            {
                case ".PPT":
                case ".PPTX":
                    this.Type = FileTypes.Presentation;
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
                case ".MPG":
                    this.Type = FileTypes.MediaPlayerVideo;
                    break;
                case ".ASF":
                case ".MOV":
                case ".MP4":
                case ".M4V":
                case ".FLV":
                case ".OGV":
                case ".OGM":
                case ".OGX":
                    this.Type = FileTypes.QuickTimeVideo;
                    break;
                case ".URL":
                    this.Type = FileTypes.Url;
                    break;
                default:
                    this.Type = FileTypes.Other;
                    break;
            }
        }

        public void RemoveFromCollection()
        {
            this.Parent.Files.Remove(this);
        }

        private void GetLocalCopy()
        {
            if (this.LinkAvailable)
            {
                if (ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection)
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        switch (this.Type)
                        {
                            case FileTypes.BuggyPresentation:
                            case FileTypes.Excel:
                            case FileTypes.FriendlyPresentation:
                            case FileTypes.MediaPlayerVideo:
                            case FileTypes.Other:
                            case FileTypes.Presentation:
                            case FileTypes.PDF:
                            case FileTypes.QuickTimeVideo:
                            case FileTypes.Word:
                            case FileTypes.OvernightsLink:
                                _linkLocalPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LocalLibraryCacheFolder, this.NameWithExtension);
                                try
                                {
                                    File.Copy(this.OriginalPath, _linkLocalPath, true);
                                }
                                catch
                                {
                                    _linkLocalPath = string.Empty;
                                }
                                break;
                            case FileTypes.Folder:
                                _linkLocalPath = this.OriginalPath;
                                break;
                            default:
                                _linkLocalPath = string.Empty;
                                break;
                        }

                    }));
                    thread.Start();
                    Application.DoEvents();
                    while (thread.IsAlive)
                        Application.DoEvents();
                }
                else
                    _linkLocalPath = this.OriginalPath;
            }
            else
                _linkLocalPath = string.Empty;
        }

        public IPreviewGenerator GetPreviewGenerator()
        {
            return null;
        }
    }
}
