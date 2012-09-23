using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class LineBreakProperties
    {
        private DateTime _lastChanged = DateTime.Now;

        public ILibraryFile Parent { get; private set; }
        public Guid Identifier { get; set; }
        private Color _foreColor = Color.Black;
        private Font _font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        private Font _boldFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
        private bool _enableBanner = false;
        private Image _banner = null;
        private string _note = string.Empty;

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                if (_foreColor != value)
                    this.LastChanged = DateTime.Now;
                _foreColor = value;
            }
        }

        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        public Font BoldFont
        {
            get
            {
                return _boldFont;
            }
            set
            {
                _boldFont = value;
            }
        }

        public bool EnableBanner
        {
            get
            {
                return _enableBanner;
            }
            set
            {
                if (_enableBanner != value)
                    this.LastChanged = DateTime.Now;
                _enableBanner = value;
            }
        }

        public Image Banner
        {
            get
            {
                return _banner;
            }
            set
            {
                _banner = value;
            }
        }

        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                if (_note != value)
                    this.LastChanged = DateTime.Now;
                _note = value;
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

        public LineBreakProperties(ILibraryFile parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
        }

        public string Serialize()
        {
            FontConverter fontConverter = new FontConverter();
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Font>" + fontConverter.ConvertToString(_font) + @"</Font>");
            result.AppendLine(@"<ForeColor>" + _foreColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            FontConverter converter = new FontConverter();
            int tempInt = 0;
            bool tempBool = false;
            Guid tempGuid;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Font":
                        try
                        {
                            _font = converter.ConvertFromString(childNode.InnerText) as Font;
                            _boldFont = new Font(_font.Name, _font.Size, FontStyle.Bold);
                        }
                        catch
                        {
                        }
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _foreColor = Color.FromArgb(tempInt);
                        break;
                    case "Note":
                        _note = childNode.InnerText;
                        break;

                    #region Compatibility with old versions
                    case "EnableBanner":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _enableBanner = tempBool;
                        break;
                    case "Banner":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            _banner = null;
                        else
                            _banner = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    #endregion
                }
            }
        }
    }
}
