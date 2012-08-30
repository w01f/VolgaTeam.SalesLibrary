using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
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
        public PresentationProperties PresentationProperties { get; set; }
        public LineBreakProperties LineBreakProperties { get; set; }
        public BannerProperties BannerProperties { get; set; }

        public IPreviewContainer UniversalPreviewContainer { get; set; }
        #region Compatibility with desktop version of Sales Depot
        public PresentationPreviewContainer PreviewContainer { get; set; }
        #endregion

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
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
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
                        this.LineBreakProperties = new LineBreakProperties();
                        this.LineBreakProperties.Font = new Font(this.Parent.WindowFont, this.Parent.WindowFont.Style);
                        this.LineBreakProperties.BoldFont = new Font(this.Parent.WindowFont, FontStyle.Bold);
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

            SetProperties();
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
                case FileTypes.BuggyPresentation:
                case FileTypes.FriendlyPresentation:
                case FileTypes.QuickTimeVideo:
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
}
