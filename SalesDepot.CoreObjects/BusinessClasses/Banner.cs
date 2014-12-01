using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class BannerProperties
	{
		private bool _enable;
		private Font _font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
		private Color _foreColor = Color.Black;
		private Image _image;
		private Alignment _imageAlignement = Alignment.Left;
		private DateTime _lastChanged = DateTime.Now;
		private bool _showText;
		private string _text = string.Empty;

		public BannerProperties(ISyncObject parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid();
		}

		public ISyncObject Parent { get; private set; }
		public bool Configured { get; set; }
		public Guid Identifier { get; set; }

		public bool Enable
		{
			get { return _enable; }
			set
			{
				if (_enable != value)
					LastChanged = DateTime.Now;
				_enable = value;
			}
		}

		public Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
					LastChanged = DateTime.Now;
				_image = value;
			}
		}

		public bool ShowText
		{
			get { return _showText; }
			set
			{
				if (_showText != value)
					LastChanged = DateTime.Now;
				_showText = value;
			}
		}

		public Alignment ImageAlignement
		{
			get { return _imageAlignement; }
			set
			{
				if (_imageAlignement != value)
					LastChanged = DateTime.Now;
				_imageAlignement = value;
			}
		}

		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
					LastChanged = DateTime.Now;
				_text = value;
			}
		}

		public Color ForeColor
		{
			get { return _foreColor; }
			set
			{
				if (_foreColor != value)
					LastChanged = DateTime.Now;
				_foreColor = value;
			}
		}

		public Font Font
		{
			get { return _font; }
			set { _font = value; }
		}

		public DateTime LastChanged
		{
			get { return _lastChanged; }
			set { _lastChanged = value; }
		}

		public BannerProperties Clone(ISyncObject parent)
		{
			var banner = new BannerProperties(parent);
			banner.Configured = Configured;
			banner.Enable = Enable;
			banner.Image = Image != null ? (Image)Image.Clone() : null;
			banner.ShowText = ShowText;
			banner.ImageAlignement = ImageAlignement;
			banner.Text = Text;
			banner.ForeColor = ForeColor;
			banner.Font = Font != null ? (Font)Font.Clone() : null;
			return banner;
		}

		public string Serialize()
		{
			var fontConverter = new FontConverter();
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier + @"</Identifier>");
			result.AppendLine(@"<Enable>" + _enable + @"</Enable>");
			result.AppendLine(@"<Image>" + Convert.ToBase64String((byte[])converter.ConvertTo(_image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Image>");
			result.AppendLine(@"<ImageAligement>" + ((int)_imageAlignement) + @"</ImageAligement>");
			result.AppendLine(@"<ShowText>" + _showText + @"</ShowText>");
			result.AppendLine(@"<Text>" + _text.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Text>");
			result.AppendLine(@"<Font>" + fontConverter.ConvertToString(_font) + @"</Font>");
			result.AppendLine(@"<ForeColor>" + _foreColor.ToArgb() + @"</ForeColor>");
			result.AppendLine(@"<LastChanged>" + _lastChanged + @"</LastChanged>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			var converter = new FontConverter();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				int tempInt;
				bool tempBool;
				switch (childNode.Name)
				{
					case "Identifier":
						Guid tempGuid;
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
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
						catch { }
						break;
					case "ForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_foreColor = Color.FromArgb(tempInt);
						break;
					case "LastChanged":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							_lastChanged = tempDateTime;
						break;
				}
			}
			Configured = true;
		}
	}
}