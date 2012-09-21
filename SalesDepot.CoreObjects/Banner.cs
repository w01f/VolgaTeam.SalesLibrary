using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects
{
    public class BannerProperties
    {
        private DateTime _lastChanged = DateTime.Now;

        public ISyncObject Parent { get; private set; }
        public bool Configured { get; set; }
        public Guid Identifier { get; set; }

        private bool _enable = false;
        private Image _image = null;
        private bool _showText = false;
        private Alignment _imageAlignement = Alignment.Left;
        private string _text = string.Empty;
        private Color _foreColor = Color.Black;
        private Font _font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);

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

        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                    this.LastChanged = DateTime.Now;
                _image = value;
            }
        }

        public bool ShowText
        {
            get
            {
                return _showText;
            }
            set
            {
                if (_showText != value)
                    this.LastChanged = DateTime.Now;
                _showText = value;
            }
        }

        public Alignment ImageAlignement
        {
            get
            {
                return _imageAlignement;
            }
            set
            {
                if (_imageAlignement != value)
                    this.LastChanged = DateTime.Now;
                _imageAlignement = value;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                    this.LastChanged = DateTime.Now;
                _text = value;
            }
        }

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
                if (_font != value)
                    this.LastChanged = DateTime.Now;
                _font = value;
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

        public BannerProperties(ISyncObject parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid();
        }

        public string Serialize()
        {
            FontConverter fontConverter = new FontConverter();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier.ToString() + @"</Identifier>");
            result.AppendLine(@"<Enable>" + _enable.ToString() + @"</Enable>");
            result.AppendLine(@"<Image>" + Convert.ToBase64String((byte[])converter.ConvertTo(_image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Image>");
            result.AppendLine(@"<ImageAligement>" + ((int)_imageAlignement).ToString() + @"</ImageAligement>");
            result.AppendLine(@"<ShowText>" + _showText.ToString() + @"</ShowText>");
            result.AppendLine(@"<Text>" + _text.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Text>");
            result.AppendLine(@"<Font>" + fontConverter.ConvertToString(_font) + @"</Font>");
            result.AppendLine(@"<ForeColor>" + _foreColor.ToArgb() + @"</ForeColor>");
            result.AppendLine(@"<LastChanged>" + _lastChanged.ToString() + @"</LastChanged>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            FontConverter converter = new FontConverter();
            int tempInt = 0;
            bool tempBool = false;
            Guid tempGuid;
            DateTime tempDateTime;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Identifier":
                        if (Guid.TryParse(childNode.InnerText, out tempGuid))
                            this.Identifier = tempGuid;
                        break;
                    case "Enable":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _enable = tempBool;
                        break;
                    case "Image":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            _image = null;
                        else
                            _image = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "ImageAligement":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _imageAlignement = (Alignment)tempInt;
                        break;
                    case "ShowText":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            _showText = tempBool;
                        break;
                    case "Text":
                        _text = childNode.InnerText;
                        break;
                    case "Font":
                        try
                        {
                            _font = converter.ConvertFromString(childNode.InnerText) as Font;
                        }
                        catch
                        {
                        }
                        break;
                    case "ForeColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            _foreColor = Color.FromArgb(tempInt);
                        break;
                    case "LastChanged":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            _lastChanged = tempDateTime;
                        break;
                }
            }
            this.Configured = true;
        }
    }
}
